using Solitaire.Games.Enums;
using Solitaire.Games.Omi.Core.Helpers;
using Solitaire.Games.Omi.Enums;
using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Solitaire.Games.Omi.Core;

public class Game
{
    public event EventHandler<int>? JoinPlayerSuccess;
    private SimpleTcpServer Server { get; set; }
    private SimpleTcpClient Client { get; set; }
    public Engine Engine { get; private set; }
    public bool IsHosted { get; private set; } = false;
    public string ServerIPPort => Server == null ? "" : $"{Server.IpAddress}:{Server.Port}";
    public ObservableCollection<(string ipPort, int place)> Players { get; private set; } = [
        ("",1),
        ("",2),
        ("",3),
        ("",4)
        ];
    public Game()
    {
        Engine = new Engine(Enums.Players.Four);
    }

    public void Host(IPAddress ip,int port)
    {
        Server = new SimpleTcpServer(ip.ToString() , port);

        Server.Events.ClientConnected += ClientConnected;
        Server.Events.ClientDisconnected += ClientDisconnected;
        Server.Events.DataReceived += DataReceived;

        Server.Start();
        IsHosted = true;

        Engine = new Engine(Enums.Players.Four);
        Reset();

        Engine.TeamDataChanged += async (s, e) => await BroadcastAll(new Action(ActionCodes.TeamDataChanged, e.SerializeJSON(), false));
        Engine.Data.DataChanged += async (s, e) => await BroadcastAll(new Action(ActionCodes.EngineDataChanged, e.SerializeJSON(), false));

    }

    public void Join(IPAddress ip,int port)
    {
        Client = new SimpleTcpClient(ip.ToString() ,port);

        Client.Events.Connected += ClientConnected;
        Client.Events.Disconnected += ClientDisconnected;
        Client.Events.DataReceived += DataReceived;

        Engine = new Engine(Enums.Players.Four);
        Engine.Initialize();

        Client.Connect();
    }

    public async void Reset()
    {
        Engine.Initialize();
        await BroadcastAll(new Action(ActionCodes.UpdatePlayers, Players.SerializeJSON(), false));
    }
    public void NewGame()
    {
        if (!IsHosted)
            throw new InvalidOperationException("Game is not hosted");


        Engine.NewGame();
    }
    private async Task BroadcastAll(Action act)
    {
        if (IsHosted)
            foreach (var item in Server.GetClients())
            {
                await Server.SendAsync(item, act.SerializeJSON());
            }
        else
        {
            act.SendAll = true;
            Client.Send(act.SerializeJSON());
        }
    }
    private async Task SendMessage(Action act,string ipPort)
    {
        if (IsHosted)
            Server.Send(ipPort,act.SerializeJSON());
        else
            Client.Send(act.SerializeJSON());
    }
    public async Task RequestPlayer(int P)
    {
        if (!IsHosted)
            await SendMessage(new Action(ActionCodes.RequestJoinAsPlayer, P.ToString(), false), null);
        else
        {
            Players.Remove(Players.First(x => x.place == P));
            Players.Add((ServerIPPort, P));
            JoinPlayerSuccess?.Invoke(this, P);
            await BroadcastAll(new Action(ActionCodes.UpdatePlayers, Players.SerializeJSON(), false));
        }
    }
    private async void DataReceived(object? sender, DataReceivedEventArgs e)
    {
        try
        {
            var data = Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count).DeserializeJSON<Action>();

            if (IsHosted)
            {
                if (data.SendAll)
                {
                    data.SendAll = false;
                    await BroadcastAll(data);
                }

                if (data.Code == ActionCodes.RequestJoinAsPlayer)
                {
                    if (int.TryParse(data.Data, out int p))
                    {
                        if (Players.First(x=> x.place == p).ipPort == "")
                        {
                            Players.Remove(Players.First(x => x.place == p));
                            Players.Add((e.IpPort, p));
                            await Server.SendAsync(e.IpPort, new Action(ActionCodes.JoinPlayerSuccess, p.ToString(), false).SerializeJSON());
                            await BroadcastAll(new Action(ActionCodes.UpdatePlayers, Players.SerializeJSON(), false));
                        }
                        else
                        {
                            await Server.SendAsync(e.IpPort, new Action(ActionCodes.JoinPlayerFailed, "Player was already choosen", false).SerializeJSON());
                        }
                    }
                }
                else if (data.Code == ActionCodes.ShuffleCards)
                {
                    if (Players.Any(x => x.ipPort == e.IpPort) && int.TryParse(data.Data, out var t))
                        if (Players.FirstOrDefault(x => x.ipPort == e.IpPort).place == Engine.Data.WhoShared)
                            Engine.Shuffle(t);
                }
                else if (data.Code == ActionCodes.SetTrump)
                {
                    if (Players.Any(x => x.ipPort == e.IpPort) && Enum.TryParse<Types>(data.Data, out var t))
                        if (Players.FirstOrDefault(x => x.ipPort == e.IpPort).place == Engine.Data.WhoSaidTrump)
                            Engine.Data.Trump = t;
                }
                else if (data.Code == ActionCodes.ShareCards)
                {
                    if (Players.Any(x => x.ipPort == e.IpPort))
                        if (Players.FirstOrDefault(x => x.ipPort == e.IpPort).place == Engine.Data.WhoShared)
                            Engine.Share();
                }
                else if (data.Code == ActionCodes.PlaceCard)
                {
                    if (Players.Any(x => x.ipPort == e.IpPort))
                        if (Players.FirstOrDefault(x => x.ipPort == e.IpPort).place == Engine.Data.CurrentPlayerPosition)
                            Engine.PlaceCard(data.Data.DeserializeJSON<Card>(),Engine.Data.CurrentPlayerPosition);
                }
            }
            else
            {
                if(data.Code == ActionCodes.UpdatePlayers)
                {
                    Players.Clear();
                    Players.AddRange(data.Data.DeserializeJSON<(string ipPort, int place)[]>());
                }
                else if (data.Code == ActionCodes.JoinPlayerSuccess)
                {
                    JoinPlayerSuccess?.Invoke(this, int.Parse(data.Data));
                }
                else if(data.Code == ActionCodes.TeamDataChanged)
                {
                    var D = data.Data.DeserializeJSON<TeamsData>()?.Teams ?? Engine.Teams.Select(x=> x.ToSimpleTeam()).ToArray();
                    for (int i = 0; i <= 1; i++)
                    {
                        Engine.Teams[i].Draws = D[i].Draws;
                        Engine.Teams[i].Loses = D[i].Loses;
                        Engine.Teams[i].Name = D[i].Name;
                        Engine.Teams[i].Wins = D[i].Wins;

                        int x = 0;
                        foreach (var d in Engine.Teams[i].Players)
                        {
                            d.Loses = D[i].Players[x].Loses;
                            d.Wins = D[i].Players[x].Wins;
                            d.Name = D[i].Players[x].Name;
                            
                            d.Deck.Clear();
                            d.Deck.AddRange(D[i].Players[x].Deck);
                            x++;
                        }

                        Engine.Teams[i].TradesGiven.Clear();
                        Engine.Teams[i].TradesGiven.AddRange(D[i].TradesGiven);

                        Engine.Teams[i].TradesHave.Clear();
                        Engine.Teams[i].TradesHave.AddRange(D[i].TradesHave);

                        Engine.Teams[i].RoundsWon.Clear();
                        Engine.Teams[i].RoundsWon.AddRange(D[i].RoundsWon);
                    }
                }
                else if(data.Code == ActionCodes.EngineDataChanged)
                {
                    var D = data.Data.DeserializeJSON<EngineSimpleData>() ?? Engine.Data.ToSimpleData();

                    Engine.Data.OldGames.Clear();
                    Engine.Data.OldGames.AddRange(D.OldGames);

                    Engine.Data.OldRounds.Clear();
                    Engine.Data.OldRounds.AddRange(D.OldRounds);

                    Engine.Data.CurrentRound.Clear();
                    Engine.Data.CurrentRound.AddRange(D.CurrentRound);

                    Engine.Data.CurrentPlayerPosition = D.CurrentPlayerPosition;
                    Engine.Data.WhoSaidTrump = D.WhoSaidTrump;
                    Engine.Data.Trump = D.Trump;
                    Engine.Data.CurrentRoundType = D.CurrentRoundType;
                    Engine.Data.UnSharedCards = D.UnSharedCards;
                    Engine.Data.PlayersCount = D.PlayersCount;
                }
            }
        }
        catch { }
    }

    public async Task SetTrump(Types trump)
    {
        if (!IsHosted)
            await SendMessage(new Action(ActionCodes.SetTrump, trump.ToString(), false), null);
        else
        {
            if (Players.Any(x => x.ipPort == ServerIPPort))
                if (Players.FirstOrDefault(x => x.ipPort == ServerIPPort).place == Engine.Data.WhoSaidTrump)
                    Engine.Data.Trump = trump;
        }
    }
    public async Task Shuffle(int times)
    {
        if (!IsHosted)
            await SendMessage(new Action(ActionCodes.ShuffleCards, times.ToString(), false), null);
        else
        {
            if (Players.Any(x => x.ipPort == ServerIPPort))
                if (Players.FirstOrDefault(x => x.ipPort == ServerIPPort).place == Engine.Data.WhoShared)
                    Engine.Shuffle(times);
        }
    }
    public async Task Share()
    {
        if (!IsHosted)
            await SendMessage(new Action(ActionCodes.ShareCards, null, false), null);
        else
        {
            if (Players.Any(x => x.ipPort == ServerIPPort))
                if (Players.FirstOrDefault(x => x.ipPort == ServerIPPort).place == Engine.Data.WhoShared)
                    Engine.Share();
        }
    }
    public async Task PlaceCard(Card c)
    {
        if (!IsHosted)
            await SendMessage(new Action(ActionCodes.PlaceCard, c.SerializeJSON(), false), null);
        else
        {
            if (Players.Any(x => x.ipPort == ServerIPPort))
                if (Players.FirstOrDefault(x => x.ipPort == ServerIPPort).place == Engine.Data.CurrentPlayerPosition)
                    Engine.PlaceCard(c, Engine.Data.CurrentPlayerPosition);
        }
    }
    private void ClientDisconnected(object? sender, ConnectionEventArgs e)
    {
        if (IsHosted)
        {
        }
        else
        {
        }
    }


    private async void ClientConnected(object? sender, ConnectionEventArgs e)
    {
        if (IsHosted)
        {
            if (Engine.IsInitialized)
            {
                await SendMessage(new Action(ActionCodes.UpdatePlayers, Players.SerializeJSON(), false),e.IpPort);
            }
        }
        else
        {

        }
    }
}