using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Collections.Concurrent;

namespace Communication.Tcp
{
    public class AsyncTcpServer : ITcpServer
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
        private EndPoint LocalEndpoint { get; set; }
        /// <summary>
        /// 服务端监听socket
        /// </summary>
        public Socket Server { get; private set; }
        /// <summary>
        /// 服务器状态
        /// </summary>
        public bool IsRuning { get; private set; }

        private ConcurrentDictionary<int, Socket> _clientConnections { get; set; }
        private int _connId = 0;
        private int _receiveBufferSize = 0;

        public event Func<AsyncTcpServer, EventResult> OnStarted;
        public event Func<AsyncTcpServer, int, EventResult> OnConnected;
        public event Func<AsyncTcpServer, int, byte[], EventResult> OnSend;
        public event Func<AsyncTcpServer, int, byte[], EventResult> OnReceive;
        public event Func<AsyncTcpServer, int, EventResult> OnDisconnected;
        public event Func<AsyncTcpServer, EventResult> OnStoped;

        public AsyncTcpServer(int port)
        {
            _clientConnections = new ConcurrentDictionary<int, Socket>();
            _localPort = port;
            _receiveBufferSize = 1024 * 64;
        }

        /// <summary>
        /// Creates a new TcpServer instance to listen on the specified port.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static AsyncTcpServer Create(int port)
        {
            return new AsyncTcpServer(port);
        }

        public bool Start()
        {
            if (IsRuning) return false;
            if (Server == null)
            {
                Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            //string ip = Helper.GetLocalIpV4Helper();
            IPAddress ipAddress = IPAddress.Any;
            LocalEndpoint = new IPEndPoint(ipAddress, _localPort);
            Server.Bind(LocalEndpoint);
            Server.Listen(backlog);
            //开始第一次接受客户端连接
            Server.BeginAccept(new AsyncCallback(AcceptAsyncCallback), null);
            IsRuning = true;
            OnStarted?.Invoke(this);
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
                Socket client = Server.EndAccept(ar);

                var socketCallbackState = new SocketCallbackState
                {
                    ConnId = _connId++,
                    Socket = client,
                    Buffer = new byte[_receiveBufferSize]
                };

                _clientConnections.TryAdd(socketCallbackState.ConnId, client);

                //开始第一次数据接收
                client.BeginReceive(socketCallbackState.Buffer, 0, socketCallbackState.Buffer.Length, SocketFlags.None,
                    new AsyncCallback(ReceiveAsyncCallback), socketCallbackState);
                //开始接受下一次客户端连接
                Server.BeginAccept(new AsyncCallback(AcceptAsyncCallback), null);
                //_pulse_time.TryAdd(end.UID, Pulse); //加入心跳检测
                //通知新客户端连入
                OnConnected?.Invoke(this, socketCallbackState.ConnId);
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
                Disconnect(currentState.ConnId);
                return;
            }
            //写入消息缓冲区
            byte[] receiveBuffer = currentState.Read(0, numberOfReadBytes);
            //激发事件，通知事件注册者处理消息
            OnReceive?.BeginInvoke(this, currentState.ConnId, receiveBuffer, null, null);
            //开始下一次数据接收
            currentState.Socket.BeginReceive(currentState.Buffer, 0, currentState.Buffer.Length, SocketFlags.None,
                new AsyncCallback(ReceiveAsyncCallback), ar.AsyncState);

        }

        public bool Stop()
        {
            if (!IsRuning) return false;
            if (Server == null) return false;
            if (_clientConnections != null && _clientConnections.Count > 0)
            {
                foreach (var item in _clientConnections)
                {
                    ////item.Value?.Disconnect(false);
                    item.Value?.Close();
                    item.Value?.Dispose();
                }
                _clientConnections.Clear();
            }

            //关闭数据的接受和发送
            //Server.Shutdown(SocketShutdown.Both);
            //清理资源
            Server.Close();
            Server?.Dispose();
            Server = null;
            IsRuning = false;
            OnStoped?.Invoke(this);
            return true;
        }

        public bool Send(int connId, byte[] buffer)
        {
            if (Server == null || !IsRuning)
            {
                throw new Exception("尚未启动");
            }
            if (!_clientConnections.TryGetValue(connId, out Socket client))
            {
                throw new Exception("连接不存在");
            }
            int sendCount = client.Send(buffer, SocketFlags.None);
            var flag = sendCount == buffer.Length;
            if (flag)
            {
                OnSend?.Invoke(this, connId, buffer);
            }
            return flag;
        }

        public bool BeginSend(int connId, byte[] buffer)
        {
            if (!IsRuning) return false;
            if (buffer == null) return false;
            if (_clientConnections.TryGetValue(connId, out Socket client))
            {
                var socketCallbackState = new SocketCallbackState
                {
                    ConnId = connId,
                    Socket = client,
                    Buffer = buffer
                };

                client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None,
                    new AsyncCallback(SendAsyncCallback), socketCallbackState);
                return true;
            }
            return false;
        }
        private void SendAsyncCallback(IAsyncResult ar)
        {
            var currentState = ar.AsyncState as SocketCallbackState;
            int sendCount = currentState.Socket.EndSend(ar);
            OnSend?.Invoke(this, currentState.ConnId, currentState.Buffer);
        }

        public bool Disconnect(int connId, bool reuseSocket = false)
        {
            if (!IsRuning) return false;
            if (_clientConnections.TryGetValue(connId, out Socket client))
            {
                client.Close();
                _clientConnections.TryRemove(connId, out _);
            }
            //通知客户端断开
            OnDisconnected?.Invoke(this, connId);
            return true;
        }

        public bool IsConnected
        {
            get { return Server.Connected; }
        }

        public int[] GetAllConnectionIDs()
        {
            return _clientConnections.Keys.ToArray();
        }

        public int GetConnectionCount()
        {
            return _clientConnections.Count;
        }

        public EndPoint GetLocalEndPoint()
        {
            return LocalEndpoint;
        }

        public EndPoint GetRemoteEndPoint(int connId)
        {
            if (!IsRuning) return null;
            if (_clientConnections.TryGetValue(connId, out Socket client))
            {
                if (client.Connected)
                {
                    return client.RemoteEndPoint;
                }
            }
            return null;
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

    }
}
