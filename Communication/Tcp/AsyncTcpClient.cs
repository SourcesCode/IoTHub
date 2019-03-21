using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Communication.Tcp
{
    public class AsyncTcpClient : ITcpClient
    {
        private readonly int _connId = 0;

        public AsyncTcpClient()
        {
            var sss = new System.Net.Sockets.TcpListener(new IPAddress(1111), 1111);
            var ccc = new System.Net.Sockets.TcpClient();

            MaxBufferSize = 1024;
        }

        public int MaxBufferSize { get; set; }

        public bool Connected { get; set; }
        public Socket Client { get; set; }

        private IPEndPoint _remoteEP = null;
        private IPEndPoint _localEP = null;

        //public event Func<AsyncTcpClient, int, Socket, EventResult> OnPrepareConnect;
        public event Func<AsyncTcpClient, int, EventResult> OnConnected;
        //public event Func<AsyncTcpClient, int, EventResult> OnHandShake;
        //public event Func<AsyncTcpClient, int, Socket, EventResult> OnAccept;
        public event Func<AsyncTcpClient, int, byte[], int, int, EventResult> OnSend;
        public event Func<AsyncTcpClient, int, byte[], int, int, EventResult> OnReceive;
        public event Func<AsyncTcpClient, int, EventResult> OnDisconnected;
        //public event Func<AsyncTcpClient, EventResult> OnShutdown;


        public bool Connect(string remoteIp, int remotePort)
        {
            if (Connected) return false;
            if (Client == null)
            {
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            _remoteEP = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);
            //EventResult? onPrepareConnectResult = OnPrepareConnect?.Invoke(this, _connId, Client);
            Client.Connect(_remoteEP);
            Connected = true;

            EventResult? onConnectedResult = OnConnected?.Invoke(this, _connId);
            if (onConnectedResult.HasValue && onConnectedResult == EventResult.Error)
            {
                Disconnect(1, true);
            }

            SocketCallbackState socketCallbackState = new SocketCallbackState
            {
                ConnId = _connId,
                Socket = Client,
                Buffer = new byte[MaxBufferSize]
            };
            Client.BeginReceive(socketCallbackState.Buffer, 0, socketCallbackState.Buffer.Length, SocketFlags.None,
                new AsyncCallback(BeginReceiveCallback), socketCallbackState);

            return true;
        }

        /// <summary>
        /// 接收数据回调方法
        /// </summary>
        /// <param name="ar"></param>
        private void BeginReceiveCallback(IAsyncResult ar)
        {
            SocketCallbackState currentState = ar.AsyncState as SocketCallbackState;
            try
            {
                int numberOfReadBytes = currentState.Socket.EndReceive(ar);

                byte[] receiveBuffer = currentState.Read(0, numberOfReadBytes);
                //激发事件，通知事件注册者处理消息

                OnReceive?.BeginInvoke(this, currentState.ConnId,
                    receiveBuffer, 0, receiveBuffer.Length, null, null);

                currentState.Socket.BeginReceive(currentState.Buffer, 0, currentState.Buffer.Length, SocketFlags.None,
                    new AsyncCallback(BeginReceiveCallback), ar.AsyncState);
            }
            catch
            {
                Disconnect(1, true);
            }
        }

        public bool Disconnect(int connId, bool isForce)
        {
            if (!Connected) return false;
            if (Client == null) return false;
            Client.Shutdown(SocketShutdown.Both);
            Client.Close();
            Connected = false;
            //通知客户端断开
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

        public bool Send(int connId, byte[] buffer, int offset, int size)
        {
            //同步
            int sendCount = Client.Send(buffer, offset, size, SocketFlags.None);
            return sendCount == size;
        }

    }
}
