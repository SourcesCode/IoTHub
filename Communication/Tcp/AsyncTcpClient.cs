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
        public bool IsRuning { get; private set; }
        public EndPoint RemoteEndPoint { get; private set; }
        public EndPoint LocalEndPoint { get; private set; }
        public int MaxBufferSize { get; set; }
        public Socket Client { get; set; }
        public bool IsConnected
        {
            get
            {
                if (Client == null) return false;
                return Client.Connected;

                //return !((Client.Poll(1000, SelectMode.SelectRead) && (Client.Available == 0)) || !Client.Connected);

                /* The long, but simpler-to-understand version:

                        bool part1 = s.Poll(1000, SelectMode.SelectRead);
                        bool part2 = (s.Available == 0);
                        if ((part1 && part2 ) || !s.Connected)
                            return false;
                        else
                            return true;

                */

            }
        }

        public event Func<AsyncTcpClient, EventResult> OnConnected;
        public event Func<AsyncTcpClient, byte[], EventResult> OnSend;
        public event Func<AsyncTcpClient, byte[], EventResult> OnReceive;
        public event Func<AsyncTcpClient, EventResult> OnDisconnected;
        public AsyncTcpClient()
        {
            //var sss = new System.Net.Sockets.TcpListener(new IPAddress(1111), 1111);
            //var ccc = new System.Net.Sockets.TcpClient();

            MaxBufferSize = 1024 * 64;
        }

        public bool Connect(string remoteIp, int remotePort)
        {
            var remoteEP = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);
            return Connect(remoteEP);
        }
        public bool Connect(EndPoint remoteEP)
        {
            if (IsRuning) return false;
            if (Client == null)
            {
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            Client.Connect(remoteEP);
            RemoteEndPoint = remoteEP;
            LocalEndPoint = Client.LocalEndPoint;
            IsRuning = true;

            EventResult? onConnectedResult = OnConnected?.Invoke(this);
            if (onConnectedResult.HasValue && onConnectedResult == EventResult.Error)
            {
                Disconnect();
            }
            var socketCallbackState = new SocketCallbackState
            {
                //ConnId = _connId,
                Socket = Client,
                Buffer = new byte[MaxBufferSize]
            };
            Client.BeginReceive(socketCallbackState.Buffer, 0, socketCallbackState.Buffer.Length, SocketFlags.None,
                new AsyncCallback(ReceiveAsyncCallback), socketCallbackState);
            return true;
        }
        private void ConnectAsyncCallback(IAsyncResult ar)
        {
            Client.EndConnect(ar);
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
        }
        /// <summary>
        /// 接收数据回调方法
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveAsyncCallback(IAsyncResult ar)
        {
            if (!IsRuning) return;
            var currentState = ar.AsyncState as SocketCallbackState;
            int numberOfReadBytes = 0;
            try
            {
                numberOfReadBytes = currentState.Socket.EndReceive(ar);
            }
            catch
            {
                numberOfReadBytes = 0;
            }
            if (numberOfReadBytes == 0)
            {
                Disconnect();
                return;
            }
            //写入消息缓冲区
            byte[] receiveBuffer = currentState.Read(0, numberOfReadBytes);
            //激发事件，通知事件注册者处理消息
            OnReceive?.BeginInvoke(this, receiveBuffer, null, null);
            //开始下一次数据接收
            currentState.Socket.BeginReceive(currentState.Buffer, 0, currentState.Buffer.Length, SocketFlags.None,
                new AsyncCallback(ReceiveAsyncCallback), ar.AsyncState);
        }

        public bool Send(byte[] buffer)
        {
            if (Client == null || !IsRuning)
            {
                throw new InvalidProgramException("This client has not connected to server.");
            }
            //同步
            int sendCount = Client.Send(buffer, SocketFlags.None);
            var flag = sendCount == buffer.Length;
            if (flag)
            {
                OnSend?.Invoke(this, buffer);
            }
            return flag;
        }

        public bool SendAsync(byte[] buffer)
        {
            if (Client == null || !IsRuning)
            {
                throw new InvalidProgramException("This client has not connected to server.");
            }
            var socketCallbackState = new SocketCallbackState
            {
                //ConnId = connId,
                Socket = Client,
                Buffer = buffer
            };
            //异步
            Client.BeginSend(socketCallbackState.Buffer, 0, socketCallbackState.Buffer.Length, SocketFlags.None,
                new AsyncCallback(SendAsyncCallback), socketCallbackState);
            return true;
        }
        private void SendAsyncCallback(IAsyncResult ar)
        {
            var currentState = ar.AsyncState as SocketCallbackState;
            int sendCount = currentState.Socket.EndSend(ar);
            OnSend?.Invoke(this, currentState.Buffer);
        }
        public bool Disconnect()
        {
            if (!IsRuning) return false;
            if (Client == null) return false;
            //Client.Disconnect(false);
            //Client.Shutdown(SocketShutdown.Both);
            Client.Close();
            Client?.Dispose();
            Client = null;
            IsRuning = false;
            //通知客户端断开
            OnDisconnected?.Invoke(this);
            return true;
        }

    }
}
