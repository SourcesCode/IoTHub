using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Communication.Udp
{
    /// <summary>
    /// 
    /// </summary>
    public class AsyncUdpClient : IUdpClient
    {
        /// <summary>
        /// 本地端口号
        /// </summary>
        private int _localPort = 0;
        /// <summary>
        /// 服务器状态
        /// </summary>
        public bool IsRuning { get; private set; }
        public EndPoint RemoteEndPoint { get; private set; }
        public EndPoint LocalEndPoint { get; private set; }
        public int MaxBufferSize { get; set; }
        public Socket Client { get; set; }

        public event Func<AsyncUdpClient, EventResult> OnConnected;
        public event Func<AsyncUdpClient, byte[], EventResult> OnSend;
        public event Func<AsyncUdpClient, byte[], EventResult> OnReceive;
        public event Func<AsyncUdpClient, EventResult> OnDisconnected;
        public AsyncUdpClient()
        {
            MaxBufferSize = 1024 * 64;
        }

        public void SetLocalPort(int port)
        {
            _localPort = port;
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
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            }
            RemoteEndPoint = remoteEP;
            LocalEndPoint = new IPEndPoint(IPAddress.Any, _localPort);
            Client.Bind(LocalEndPoint);
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
        /// <summary>
        /// 接收数据回调方法
        /// </summary>
        /// <param name="ar">回调参数</param>
        private void ReceiveAsyncCallback(IAsyncResult ar)
        {
            if (!IsRuning) return;
            var currentState = ar.AsyncState as SocketCallbackState;
            int numberOfReadBytes = 0;
            try
            {
                //如果两次开始了异步的接收,所以当客户端退出的时候
                //会两次执行EndReceive
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
            return SendTo(buffer, RemoteEndPoint);
        }

        private bool SendTo(byte[] buffer, EndPoint remoteEP)
        {
            if (Client == null || !IsRuning)
            {
                throw new InvalidProgramException("This client has not connected to server.");
            }
            //同步
            int sendCount = Client.SendTo(buffer, SocketFlags.None, remoteEP);
            var flag = sendCount == buffer.Length;
            if (flag)
            {
                OnSend?.Invoke(this, buffer);
            }
            return flag;
        }

        public bool SendAsync(byte[] buffer, EndPoint remoteEP)
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
            Client.BeginSendTo(socketCallbackState.Buffer, 0, socketCallbackState.Buffer.Length, SocketFlags.None,
                remoteEP, new AsyncCallback(SendAsyncCallback), socketCallbackState);
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
