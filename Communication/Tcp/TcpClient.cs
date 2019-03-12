using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Communication.Tcp.EventArgs;
using System.IO;

namespace Communication.Tcp
{
    public class TcpClient : ITcpClient
    {
        private readonly ITcpClientListener _listener;
        private readonly int _connId = 0;

        public TcpClient(ITcpClientListener listener)
        {
            var sss = new System.Net.Sockets.TcpClient();

            _listener = listener;
            OnPrepareConnect += listener.OnPrepareConnect;
            OnConnected += listener.OnConnected;
            OnHandShake += listener.OnHandShake;
            OnAccept += listener.OnAccept;
            OnSend += listener.OnSend;
            OnReceive += listener.OnReceive;
            OnDisconnected += listener.OnDisconnected;
            OnShutdown += listener.OnShutdown;


            ReceiveBufferSize = 1024;
            SendBufferSize = 1024;
        }

        public int ReceiveBufferSize { get; set; }
        public int SendBufferSize { get; set; }
        public bool Connected { get; set; }
        public Socket Client { get; set; }

        private IPEndPoint _remoteEP = null;
        private IPEndPoint _localEP = null;

        public event Func<TcpClient, int, Socket, EventResult> OnPrepareConnect;
        public event Func<TcpClient, int, EventResult> OnConnected;
        public event Func<TcpClient, int, EventResult> OnHandShake;
        public event Func<TcpClient, int, Socket, EventResult> OnAccept;
        public event Func<TcpClient, int, byte[], int, int, EventResult> OnSend;
        public event Func<TcpClient, int, byte[], int, int, EventResult> OnReceive;
        public event Func<TcpClient, int, EventResult> OnDisconnected;
        public event Func<TcpClient, EventResult> OnShutdown;


        public bool Connect(string remoteIp, int remotePort)
        {
            if (Connected) return true;
            if (Client == null)
            {
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            _remoteEP = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);
            EventResult? onPrepareConnectResult = OnPrepareConnect?.Invoke(this, _connId, Client);
            Client.Connect(_remoteEP);
            Connected = true;

            EventResult? onConnectedResult = OnConnected?.Invoke(this, _connId);
            if (onConnectedResult.HasValue && onConnectedResult == EventResult.Error)
            {
                Disconnect();
            }

            SocketCallbackState socketCallbackState = new SocketCallbackState
            {
                ConnId = _connId,
                Socket = Client,
                Buffer = new byte[ReceiveBufferSize]
            };
            Client.BeginReceive(socketCallbackState.Buffer, 0, ReceiveBufferSize, SocketFlags.None, new AsyncCallback(BeginReceiveCallback), socketCallbackState);

            return true;
        }

        /// <summary>
        /// 接收数据回调方法
        /// </summary>
        /// <param name="ar"></param>
        private void BeginReceiveCallback(IAsyncResult ar)
        {
            SocketCallbackState socketCallbackState = ar.AsyncState as SocketCallbackState;
            try
            {
                int real_recv = socketCallbackState.Socket.EndReceive(ar);
                //激发事件，通知事件注册者处理消息
                //TCPMessageReceived?.BeginInvoke(args, null, null);
                EventResult? result = OnReceive?.Invoke(this,
                    socketCallbackState.ConnId,
                    socketCallbackState.Buffer,
                    0,
                    real_recv);

                socketCallbackState.Socket.BeginReceive(socketCallbackState.Buffer, 0, ReceiveBufferSize, SocketFlags.None, new AsyncCallback(BeginReceiveCallback), socketCallbackState);
            }
            catch
            {
                Disconnect();
            }
        }


        public bool Disconnect()
        {
            if (!Connected) return false;
            if (Client == null) return false;
            Client.Close();
            OnDisconnected(this, _connId);
            return true;
        }

        public int GetConnectionID()
        {
            return _connId;
        }

        public EndPoint GetLocalEndPoint()
        {
            return _localEP;
        }

        public EndPoint GetRemoteEndPoint()
        {
            return _remoteEP;
        }

        public bool Send(byte[] buffer, int offset, int size)
        {
            byte[] buffer2send = new byte[size];  // 1 + 4 + data
            BinaryWriter sWriter = new BinaryWriter(new MemoryStream(buffer2send));

            //sWriter.Write((byte)msg);
            //sWriter.Write(data.Length);
            sWriter.Write(buffer, offset, size);
            sWriter.Close();

            int sendCount = Client.Send(buffer2send);  //同步

            return sendCount == size;
        }

    }
}
