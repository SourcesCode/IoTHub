using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Communication.Udp
{
    public class UdpClient : IUdpClient
    {
        public EndPoint RemoteEndpoint { get; private set; }

        private readonly IUdpClientListener _listener;
        public UdpClient(IUdpClientListener listener)
        {
            _listener = listener;
        }

        public int ReceiveBufferSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SendBufferSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Connected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Socket Client { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public event Func<TcpServer, int, Socket, EventResult> OnPrepareConnect;
        //public event Func<TcpServer, int, EventResult> OnConnected;

        public event Func<UdpClient, Socket, EventResult> OnStarted;
        //public event Func<UdpClient, int, EventResult> OnHandShake;
        public event Func<UdpClient, int, Socket, EventResult> OnAccept;
        public event Func<UdpClient, int, byte[], int, int, EventResult> OnSend;
        public event Func<UdpClient, int, byte[], int, int, EventResult> OnReceive;
        public event Func<UdpClient, int, EventResult> OnDisconnected;
        public event Func<UdpClient, EventResult> OnShutdown;

        public bool Connect(string remoteIp, int remotePort)
        {
            if (!Connected)
            {
                // start the async connect operation
                Client.BeginConnect(RemoteEndpoint,
                    new AsyncCallback(ConnectAsyncCallback), Client);
            }
            return true;
        }

        private void ConnectAsyncCallback(IAsyncResult ar)
        {
            if (!Connected)
                throw new InvalidProgramException("This TCP Scoket server has not been started.");

            Socket client = (Socket)ar.AsyncState;
            client.EndConnect(ar);
            //RaiseServerConnected(Addresses, Port);

            SocketCallbackState socketCallbackState = new SocketCallbackState
            {
                //ConnId = _connId,
                Socket = client,
                Buffer = new byte[ReceiveBufferSize]
            };

            client.BeginReceive(socketCallbackState.Buffer, 0, socketCallbackState.Buffer.Length, SocketFlags.None,
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
                OnDisconnected?.Invoke(this, currentState.ConnId);
                //int tmp;
                //_pulse_time.TryRemove(currentState.UID, out tmp);
                return;
            }

            //写入消息缓冲区
            byte[] receiveBuffer = currentState.Write(0, numberOfReadBytes);

            //激发事件，通知事件注册者处理消息
            OnReceive?.BeginInvoke(this, currentState.ConnId, receiveBuffer, 0, receiveBuffer.Length, null, null);

            //开始下一次数据接收
            currentState.Socket.BeginReceive(currentState.Buffer, 0, currentState.Buffer.Length, SocketFlags.None,
                new AsyncCallback(ReceiveAsyncCallback), ar.AsyncState);
            //TODO 处理已经读取的数据 ps:数据在session的RecvDataBuffer中
            //RaiseDataReceived(session);
            //TODO 触发数据接收事件


        }

        public bool Disconnect(int connId, bool isForce)
        {
            throw new NotImplementedException();
        }

        public int GetConnectionID()
        {
            throw new NotImplementedException();
        }

        public EndPoint GetLocalEndPoint()
        {
            throw new NotImplementedException();
        }

        public EndPoint GetRemoteEndPoint()
        {
            throw new NotImplementedException();
        }

        public bool Send(int connId, byte[] buffer, int offset, int size)
        {
            if (!Connected)
            {
                RaiseServerDisconnected(Addresses, Port);
                throw new InvalidProgramException(
                  "This client has not connected to server.");
            }

            Socket client = _clientConnections[connId];
            if (client == null)
                throw new ArgumentNullException("client");

            if (buffer == null)
                throw new ArgumentNullException("data");


            client.BeginSend(buffer, offset, size, SocketFlags.None,
                new AsyncCallback(SendAsyncCallback), client);

            return true;
        }
        private void SendAsyncCallback(IAsyncResult ar)
        {
            ((Socket)ar.AsyncState).EndSend(ar);
        }

    }
}
