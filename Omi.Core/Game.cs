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
        Engine.CurrentRound.CollectionChanged += CurrentDeck_Changed;
    }

    private void CurrentDeck_Changed(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        throw new NotImplementedException();
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
                    if(d.p == Engine.WhoShared)
                    {
                        Engine.Shuffle(d.times);
                        await BroadcastAll(new Action(ActionCodes.ShuffleCards, d.times.ToString(),false));
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