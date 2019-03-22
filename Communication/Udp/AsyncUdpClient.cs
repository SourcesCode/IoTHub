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
        public AsyncUdpClient()
        {

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

        //public event Func<TcpServer, int, Socket, EventResult> OnPrepareConnect;
        //public event Func<TcpServer, int, EventResult> OnConnected;

        public event Func<AsyncUdpClient, Socket, EventResult> OnStarted;
        //public event Func<UdpClient, int, EventResult> OnHandShake;
        public event Func<AsyncUdpClient, EventResult> OnConnected;
        public event Func<AsyncUdpClient, int, byte[], int, int, EventResult> OnSend;
        public event Func<AsyncUdpClient, int, byte[], int, int, EventResult> OnReceive;
        public event Func<AsyncUdpClient, EventResult> OnDisconnected;
        public event Func<AsyncUdpClient, EventResult> OnClose;

        public bool Connect(string remoteIp, int remotePort)
        {
            if (Connected) return false;
            if (Client == null)
            {
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            }
            RemoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);

            // start the async connect operation
            Client.BeginConnect(RemoteEndPoint, new AsyncCallback(ConnectAsyncCallback), null);

            return true;
        }

        private void ConnectAsyncCallback(IAsyncResult ar)
        {
            if (!Connected)
                throw new InvalidProgramException("This TCP Scoket server has not been started.");

            Client.EndConnect(ar);
            //RaiseServerConnected(Addresses, Port);
            OnConnected?.Invoke(this);
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
        /// <param name="ar">回调参数</param>
        private void ReceiveAsyncCallback(IAsyncResult ar)
        {
            if (!Connected)
                throw new InvalidProgramException("This TCP Scoket server has not been started.");

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
                //TCPClientDisConnectedEventArgs args = new TCPClientDisConnectedEventArgs();
                //args.CsID = _server_id;
                //args.End = currentState;
                //args.Time = DateTime.Now;
                //通知客户端断开
                //TCPClientDisConnected?.Invoke(args);
                //OnDisconnected?.Invoke(this );
                //int tmp;
                //_pulse_time.TryRemove(currentState.UID, out tmp);
                Disconnect();
                return;
            }

            //写入消息缓冲区
            byte[] receiveBuffer = currentState.Read(0, numberOfReadBytes);

            //激发事件，通知事件注册者处理消息
            OnReceive?.BeginInvoke(this, currentState.ConnId,
                receiveBuffer, 0, receiveBuffer.Length, null, null);

            //开始下一次数据接收
            currentState.Socket.BeginReceive(currentState.Buffer, 0, currentState.Buffer.Length, SocketFlags.None,
                new AsyncCallback(ReceiveAsyncCallback), ar.AsyncState);
            //TODO 处理已经读取的数据 ps:数据在session的RecvDataBuffer中
            //RaiseDataReceived(session);
            //TODO 触发数据接收事件

        }

        public bool Send(byte[] buffer, int offset, int size)
        {
            if (!Connected)
            {
                //RaiseServerDisconnected(Addresses, Port);
                throw new InvalidProgramException(
                  "This client has not connected to server.");
            }
            //同步
            int sendCount = Client.Send(buffer, offset, size, SocketFlags.None);
            OnSend(this, 0, buffer, offset, size);
            return sendCount == size;
            //Client.BeginSend(buffer, offset, size, SocketFlags.None,
            //    new AsyncCallback(SendAsyncCallback), null);

        }

        private void SendAsyncCallback(IAsyncResult ar)
        {
            ((Socket)ar.AsyncState).EndSend(ar);
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
