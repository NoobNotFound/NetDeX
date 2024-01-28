using Solitaire.Games.Omi.Core.Helpers;
using Solitaire.Games.Omi.Enums;
using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Games.Omi.Core;

public class Game
{
    public event EventHandler? PlayersUpdated;
    private SimpleTcpServer Server { get; set; }
    private SimpleTcpClient Client { get; set; }
    private Engine Engine { get; set; }
    public bool IsHosted { get; private set; } = false;
    public (string ipPort, int place)[] Players { get; private set; } = [
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

        Server.Send("[ClientIp:Port]", "Hello, world!");
        Console.ReadKey();
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

        if (Players.Any(x => x.ipPort == ""))
            throw new Exception("Player(s) are not selected");
        

        Engine.NewGame();
    }
    private async Task BroadcastAll(Action act)
    {
        foreach (var item in Server.GetClients())
        {
           await Server.SendAsync(item, act.SerializeJSON());
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
                        if (Players[p - 1].ipPort == "")
                        {
                            Players[p - 1].ipPort = e.IpPort;
                            await Server.SendAsync(e.IpPort, new Action(ActionCodes.JoinPlayerSuccess, null, false).SerializeJSON());
                            await BroadcastAll(new Action(ActionCodes.UpdatePlayers, Players.SerializeJSON(), false));
                        }
                        else
                        {
                            await Server.SendAsync(e.IpPort, new Action(ActionCodes.JoinPlayerFailed, "Player was already choosen", false).SerializeJSON());
                        }
                    }
                }
                else if(data.Code == ActionCodes.ShuffleCards)
                {
                    var d = data.Data.DeserializeJSON<(int p, int times)>();
                    if(d.p == Engine.Data.WhoShared)
                    {
                        Engine.Shuffle(d.times);
                    }
                }
            }
            else
            {
                if(data.Code == ActionCodes.UpdatePlayers)
                {
                    data.Data.DeserializeJSON<(string ipPort, int place)[]>();
                    PlayersUpdated?.Invoke(this, new());
                }
                else if(data.Code == ActionCodes.TeamsWithPlayers)
                {
                    var D = data.Data.DeserializeJSON<Team[]>() ?? Engine.Teams;
                    for (int i = 0; i < 1; i++)
                    {
                        Engine.Teams[i].Draws = D[0].Draws;
                        Engine.Teams[i].Loses = D[0].Loses;
                        Engine.Teams[i].Name = D[0].Name;
                        Engine.Teams[i].Wins = D[0].Wins;

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
            }
        }
        catch { }
    }

    private void ClientDisconnected(object? sender, ConnectionEventArgs e)
    {
        
        throw new NotImplementedException();
    }


    private async void ClientConnected(object? sender, ConnectionEventArgs e)
    {
        if(Engine.IsInitialized)
        {
            await BroadcastAll(new Action(ActionCodes.UpdatePlayers, Players.SerializeJSON(), false));
        }
    }
}