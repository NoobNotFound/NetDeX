using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LAN.Core;
internal class ClientHandler
{
    public TcpClient ClientSocket;
    public event EventHandler<BytesRecievedEventArgs> BytesRecieved = delegate { };
    public event EventHandler Disconnected = delegate { };
    public ClientHandler(TcpClient clientSocket, bool start = false)
    {
        ClientSocket = clientSocket;
        if (start)
        {
            Thread ctThread = new(GetData);
            ctThread.Start();
        }
    }
    public void Start()
    {
        Thread ctThread = new(GetData);
        ctThread.Start();
    }
    private void GetData()
    {
        byte[] bytesFrom = new byte[10025];

        while (ClientSocket.Connected)
        {
            try
            {
                NetworkStream networkStream = ClientSocket.GetStream();
                int l = networkStream.Read(bytesFrom, 0, bytesFrom.Length);

                this.BytesRecieved(this, new BytesRecievedEventArgs(bytesFrom, l));

            }
            catch
            {
            }
        }
        Disconnected(this, new EventArgs());
    }
    internal class BytesRecievedEventArgs : EventArgs
    {
        public byte[] Bytes { get; set; }
        public int Length { get; set; }
        public BytesRecievedEventArgs(byte[] bytes, int length)
        {
            Bytes = bytes;
            Length = length;
        }
    }

}