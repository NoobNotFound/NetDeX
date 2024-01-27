using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LAN.Core
{
    public class UltraClient
    {
        public event EventHandler<byte[]>? DataRecieved;
        public TcpClient clientSocket { get; set; } = new TcpClient();
        private NetworkStream serverStream = default;
        public IPAddress IP { get; private set; }

        public async void Connect(IPAddress ip, int port)
        {
            IP = ip;
            await clientSocket.ConnectAsync(ip, port);
            serverStream = clientSocket.GetStream();
            var ctThread = new Thread(GetMessage);

            ctThread.Start();

        }
        public void Disconnect()
        {
            clientSocket.Close();
        }
        private void GetMessage()
        {
            while (clientSocket.Connected)
            {
                try
                {
                    serverStream = clientSocket.GetStream();

                    byte[] inStream = new byte[clientSocket.ReceiveBufferSize];
                    serverStream.Read(inStream, 0, inStream.Length);

                    DataRecieved?.Invoke(this, inStream);
                }
                catch { }
            }

        }
        public async Task SendMessage(byte[] outStream)
        {
            await serverStream.WriteAsync(outStream, 0, outStream.Length);
            serverStream.Flush();
            await serverStream.FlushAsync();

        }
    }
}