using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LAN.Core;
public class UltraServer
{
    public IPAddress IP { get; private set; }
    public int Port { get; private set; }

    public Hashtable ClientsList = new();
    private bool IsRunning = true;

    public TcpListener ServerSocket;
    public void TryCloseServer()
    {
        IsRunning = false;
    }
    public void Host(IPAddress address, int port)
    {
        IP = address;
        Port = port;

        IsRunning = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        ServerSocket = new TcpListener(address, port);
        ServerSocket.Start();

        var t = new Thread(Reciver);
        t.Start();

    }
    public void BroadcastAll(byte[] broadcastBytes)
    {
        foreach (DictionaryEntry Item in ClientsList)
        {
            TcpClient broadcastSocket;
            broadcastSocket = (TcpClient)Item.Value;

            if (broadcastSocket.Connected)
            {
                NetworkStream broadcastStream = broadcastSocket.GetStream();

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }
    }
    private int ClientsCount = 0;
    private string memb = "";
    private void Reciver()
    {
        //TcpClient clientSocket = default;
        //while (IsRunning)
        //{
        //    clientSocket = ServerSocket.AcceptTcpClient();

        //    byte[] bytesFrom = new byte[10025];
        //    Data dataFromClient;

        //    NetworkStream networkStream = clientSocket.GetStream();
        //    networkStream.Read(bytesFrom, 0, bytesFrom.Length);
        //    dataFromClient = bytesFrom.ToData();

        //    if (dataFromClient.DataType == DataType.InfoMessage)
        //    {
        //        if (dataFromClient.InfoCode == InfoCodes.Join)
        //        {
        //            ClientsCount++;
        //            memb += dataFromClient.Message + ",";

        //            var client = new ClientHandler(clientSocket, dataFromClient.ClientName, ClientsCount, false, dataFromClient.GUID);
        //            client.Disconnected += (sender, e) =>
        //            {
        //                try
        //                {
        //                    foreach (DictionaryEntry Item in ClientsList)
        //                    {
        //                        if ((Guid)Item.Key == ((ClientHandler)sender).ClientId)
        //                        {
        //                            ClientsList.Remove(Item);
        //                            return;
        //                        }
        //                    }
        //                }
        //                catch { }
        //                BroadcastAll(new Data(((ClientHandler)sender).ClientName, ((ClientHandler)sender).ClientName + " Left.", dataType: DataType.InfoMessage, infoCode: InfoCodes.Left));
        //            };
        //            client.BytesRecieved += (sender, e) => BroadcastAll(e.Bytes, e.Length);
        //            ClientsList.Add(dataFromClient.GUID, clientSocket);
        //            client.Start();

        //            var channelPorts = Channels.Select(x => x.Port.ToString());
        //            BroadcastAll(string.Join(",", channelPorts.ToArray()), ServerName, DataType.InfoMessage, MsgCode: InfoCodes.AddChannels);

        //        }
        //        else if (dataFromClient.InfoCode == InfoCodes.ChannelsRequest)
        //        {
        //            var channelPorts = Channels.Select(x => x.Port.ToString());
        //            BroadcastAll(string.Join(",", channelPorts.ToArray()), ServerName, DataType.InfoMessage, MsgCode: InfoCodes.AddChannels);
        //        }
        //    }
        //}
        //clientSocket.Close();
        //ServerSocket.Stop();
    }
    public void Broadcast(TcpClient broadcastSocket, byte[] broadcastBytes)
    {
        if (broadcastSocket.Connected)
        {
            NetworkStream broadcastStream = broadcastSocket.GetStream();

            broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
            broadcastStream.Flush();

        }

    }
}