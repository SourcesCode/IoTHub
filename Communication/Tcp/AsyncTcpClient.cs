using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Communication.Tcp
{
    /// <summary>
    /// 
    /// </summary>
    public class AsyncTcpClient : ITcpClient
    {
        public AsyncTcpClient()
        {
            var sss = new System.Net.Sockets.TcpListener(new IPAddress(1111), 1111);
            var ccc = new System.Net.Sockets.TcpClient();

            MaxBufferSize = 1024;
        }

        public EndPoint RemoteEndPoint { get; private set; }
        public EndPoint LocalEndPoint { get; private set; }
        public int MaxBufferSize { get; set; }
        public bool Connected
        {
            get
            {
                if (Client == null) return false;
                return Client.Connected;
            }
        }
        public Socket Client { get; set; }

        public event Func<AsyncTcpClient, EventResult> OnConnected;
        public event Func<AsyncTcpClient, int, byte[], int, int, EventResult> OnSend;
        public event Func<AsyncTcpClient, int, byte[], int, int, EventResult> OnReceive;
        public event Func<AsyncTcpClient, EventResult> OnDisconnected;
        public event Func<AsyncTcpClient, EventResult> OnClose;

        public bool Connect(string remoteIp, int remotePort)
        {
            if (Connected) return false;
            if (Client == null)
            {
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            RemoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);
            //EventResult? onPrepareConnectResult = OnPrepareConnect?.Invoke(this, _connId, Client);
            Client.Connect(RemoteEndPoint);

            LocalEndPoint = Client.LocalEndPoint;
            EventResult? onConnectedResult = OnConnected?.Invoke(this);
            if (onConnectedResult.HasValue && onConnectedResult == EventResult.Error)
            {
                Disconnect();
            }

            SocketCallbackState socketCallbackState = new SocketCallbackState
            {
                //ConnId = _connId,
                Socket = Client,
                Buffer = new byte[MaxBufferSize]
            };
            Client.BeginReceive(socketCallbackState.Buffer, 0, socketCallbackState.Buffer.Length, SocketFlags.None,
                new AsyncCallback(ReceiveAsyncCallback), socketCallbackState);

            return true;
        }

        /// <summary>
        /// 接收数据回调方法
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveAsyncCallback(IAsyncResult ar)
        {
            if (!Connected) return;
            SocketCallbackState currentState = ar.AsyncState as SocketCallbackState;
            try
            {
                int numberOfReadBytes = currentState.Socket.EndReceive(ar);

                byte[] receiveBuffer = currentState.Read(0, numberOfReadBytes);
                //激发事件，通知事件注册者处理消息
                OnReceive?.BeginInvoke(this, currentState.ConnId,
                    receiveBuffer, 0, receiveBuffer.Length, null, null);

                currentState.Socket.BeginReceive(currentState.Buffer, 0, currentState.Buffer.Length, SocketFlags.None,
                    new AsyncCallback(ReceiveAsyncCallback), ar.AsyncState);
            }
            catch
            {
                Disconnect();
            }
        }

        public bool Send(byte[] buffer, int offset, int size)
        {
            //同步
            int sendCount = Client.Send(buffer, offset, size, SocketFlags.None);
            OnSend(this, 0, buffer, offset, size);
            return sendCount == size;
        }

        public bool Disconnect(bool reuseSocket = false)
        {
            if (!Connected) return false;
            if (Client == null) return false;
            Client.Disconnect(reuseSocket);
            //通知客户端断开
            OnDisconnected(this);
            return true;
        }

        public bool Close()
        {
            if (!Connected) return false;
            if (Client == null) return false;
            Disconnect();
            //Client.Shutdown(SocketShutdown.Both);
            Client.Close();
            OnClose?.Invoke(this);
            return true;
        }

    }
}
