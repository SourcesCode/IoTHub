using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Linq;

namespace Communication.Tcp
{
    public class TcpServer : ITcpServer
    {
        /// <summary>
        /// 本地端口号
        /// </summary>
        private int _localPort = 0;
        /// <summary>
        /// The maximum length of the pending connections queue.
        /// 挂起连接队列的最大长度。
        /// </summary>
        private int backlog = 1024;
        public EndPoint LocalEndpoint { get; private set; }
        /// <summary>
        /// 服务端监听socket
        /// </summary>
        public Socket Server { get; private set; }
        /// <summary>
        /// 服务器状态
        /// </summary>
        protected bool Active { get; private set; }

        private Dictionary<int, Socket> _clientConnections { get; set; }
        private int _connId = 0;
        private int _receiveBufferSize = 0;

        public TcpServer(int port)
        {
            _localPort = port;
            _receiveBufferSize = 1024;
        }

        /// <summary>
        /// Creates a new TcpServer instance to listen on the specified port.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static TcpServer Create(int port)
        {
            return new TcpServer(port);
        }

        //public event Func<TcpServer, int, Socket, EventResult> OnPrepareConnect;
        //public event Func<TcpServer, int, EventResult> OnConnected;

        public event Func<TcpServer, Socket, EventResult> OnStarted;
        //public event Func<TcpServer, int, EventResult> OnHandShake;
        public event Func<TcpServer, int, Socket, EventResult> OnAccept;
        public event Func<TcpServer, int, byte[], int, int, EventResult> OnSend;
        public event Func<TcpServer, int, byte[], int, int, EventResult> OnReceive;
        public event Func<TcpServer, int, EventResult> OnDisconnected;
        public event Func<TcpServer, EventResult> OnShutdown;

        public bool Disconnect(int connId, bool isForce)
        {
            if (!Active)
                throw new InvalidProgramException("This TCP Scoket server has not been started.");

            Socket client = _clientConnections[connId];
            if (client == null)
                throw new ArgumentNullException("client");
            _clientConnections.Remove(connId);

            client.Disconnect(true);

            return true;
        }

        public int[] GetAllConnectionIDs()
        {
            return _clientConnections.Keys.ToArray();
        }

        public int GetConnectionCount()
        {
            return _clientConnections.Count;
        }

        public EndPoint GetLocalEndPoint(int connId)
        {
            throw new NotImplementedException();
        }

        public EndPoint GetRemoteEndPoint(int connId)
        {
            throw new NotImplementedException();
        }

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public bool Receive(int connId, byte[] buffer, int offset, int size)
        {
            return false;
        }

        public bool Send(int connId, byte[] buffer, int offset, int size)
        {
            if (!Active)
                throw new InvalidProgramException("This TCP Scoket server has not been started.");

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

        public bool SetHeartBeatInterval(int second)
        {
            throw new NotImplementedException();
        }

        public bool SetMaxConnectionCount(int count)
        {
            throw new NotImplementedException();
        }

        public bool SetSocketBufferSize(int size)
        {
            throw new NotImplementedException();
        }

        public bool SetSocketListenQueueLength(int length)
        {
            throw new NotImplementedException();
        }

        public bool SetWorkerThreadCount(int count)
        {
            throw new NotImplementedException();
        }

        public bool Start()
        {
            if (Active) return false;
            if (Server == null)
            {
                Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            string ip = Helper.GetLocalIpV4Helper();
            LocalEndpoint = new IPEndPoint(IPAddress.Parse(ip), _localPort);
            Server.Bind(LocalEndpoint);
            Server.Listen(backlog);
            //开始第一次接受客户端连接
            Server.BeginAccept(new AsyncCallback(AcceptAsyncCallback), Server);
            Active = true;

            OnStarted?.Invoke(this, Server);
            return true;
        }

        /// <summary>
        /// 客户端连入回调方法
        /// </summary>
        /// <param name="ar"></param>
        private void AcceptAsyncCallback(IAsyncResult ar)
        {
            try
            {
                Socket server = (Socket)ar.AsyncState;
                Socket client = server.EndAccept(ar);
                _connId++;
                _clientConnections.Add(_connId, client);

                //TCPEndPoint end = new TCPEndPoint();
                //end.Socket = client;
                //end.UID = _connId++;

                SocketCallbackState socketCallbackState = new SocketCallbackState
                {
                    ConnId = _connId,
                    Socket = client,
                    Buffer = new byte[_receiveBufferSize]
                };

                //开始第一次数据接收
                client.BeginReceive(socketCallbackState.Buffer, 0, socketCallbackState.Buffer.Length, SocketFlags.None,
                    new AsyncCallback(ReceiveAsyncCallback), socketCallbackState);
                //开始接受下一次客户端连接
                server.BeginAccept(new AsyncCallback(AcceptAsyncCallback), ar.AsyncState);
                //_pulse_time.TryAdd(end.UID, Pulse); //加入心跳检测

                //TCPClientConnectedEventArgs args = new TCPClientConnectedEventArgs();
                //args.CsID = _server_id;
                //args.End = end;
                //args.Time = DateTime.Now;
                ////通知新客户端连入
                //TCPClientConnected?.Invoke(args);

                OnAccept?.Invoke(this, _connId, client);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 接收数据回调方法
        /// </summary>
        /// <param name="ar">回调参数</param>
        private void ReceiveAsyncCallback(IAsyncResult ar)
        {

            if (!Active)
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

        public bool Stop()
        {
            if (!Active) return false;
            if (Server == null) return false;
            //关闭数据的接受和发送
            Server.Shutdown(SocketShutdown.Both);
            //清理资源
            Server.Close();
            Server = null;
            Active = false;
            return true;
        }

    }
}
