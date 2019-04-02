
using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Linq;

namespace Communication.Udp
{
    public class AsyncUdpServer : IUdpServer
    {
        /// <summary>
        /// 本地端口号
        /// </summary>
        private int _localPort = 0;
        private EndPoint LocalEndpoint { get; set; }
        /// <summary>
        /// 服务端监听socket
        /// </summary>
        public Socket Server { get; private set; }
        /// <summary>
        /// 服务器状态
        /// </summary>
        public bool IsStarted { get; private set; }
        private ConcurrentDictionary<int, EndPoint> _remoteEndPoints { get; set; }
        private int _receiveBufferSize = 0;

        public event Func<AsyncUdpServer, EventResult> OnStarted;
        public event Func<AsyncUdpServer, int, byte[], EventResult> OnSend;
        public event Func<AsyncUdpServer, int, byte[], EventResult> OnReceive;
        public event Func<AsyncUdpServer, EventResult> OnStoped;

        public AsyncUdpServer(int port)
        {
            _remoteEndPoints = new ConcurrentDictionary<int, EndPoint>();
            _localPort = port;
            _receiveBufferSize = 1024 * 64;
        }

        /// <summary>
        /// Creates a new TcpServer instance to listen on the specified port.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static AsyncUdpServer Create(int port)
        {
            return new AsyncUdpServer(port);
        }
        public void SetLocalPort(int port)
        {
            _localPort = port;
        }
        public bool Start()
        {
            if (IsStarted) return false;
            if (Server == null)
            {
                Server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            }
            //string ip = Helper.GetLocalIpV4Helper();
            IPAddress ip = IPAddress.Any;
            LocalEndpoint = new IPEndPoint(ip, _localPort);
            Server.Bind(LocalEndpoint);

            var socketCallbackState = new SocketCallbackState
            {
                //ConnId = _connId++,
                Socket = Server,
                Buffer = new byte[_receiveBufferSize],
            };
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            //开始第一次数据接收
            Server.BeginReceiveFrom(socketCallbackState.Buffer, 0, socketCallbackState.Buffer.Length, SocketFlags.None,
                ref remoteEndPoint, new AsyncCallback(ReceiveAsyncCallback), socketCallbackState);
            IsStarted = true;
            OnStarted?.Invoke(this);
            return true;
        }

        /// <summary>
        /// 接收数据回调方法
        /// </summary>
        /// <param name="ar">回调参数</param>
        private void ReceiveAsyncCallback(IAsyncResult ar)
        {
            if (!IsStarted) return;
            var currentState = ar.AsyncState as SocketCallbackState;
            int numberOfReadBytes = 0;
            int connId = -1;
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                //如果两次开始了异步的接收,所以当客户端退出的时候
                //会两次执行EndReceive
                numberOfReadBytes = currentState.Socket.EndReceiveFrom(ar, ref remoteEndPoint);

                connId = remoteEndPoint.GetHashCode();
                _remoteEndPoints.TryAdd(connId, remoteEndPoint);
            }
            catch
            {
                numberOfReadBytes = 0;
            }
            if (numberOfReadBytes == 0)
            {
                Stop(connId);
                return;
            }
            //写入消息缓冲区
            byte[] receiveBuffer = currentState.Read(0, numberOfReadBytes);
            //激发事件，通知事件注册者处理消息
            OnReceive?.BeginInvoke(this, connId, receiveBuffer, null, null);
            //开始下一次数据接收
            currentState.Socket.BeginReceiveFrom(currentState.Buffer, 0, currentState.Buffer.Length, SocketFlags.None,
                ref remoteEndPoint, new AsyncCallback(ReceiveAsyncCallback), ar.AsyncState);

        }

        public bool Stop()
        {
            return Stop(-1);
        }

        public bool Stop(int connId)
        {
            if (!IsStarted) return false;
            if (Server == null) return false;
            _remoteEndPoints.Clear();

            //关闭数据的接受和发送
            //Server.Shutdown(SocketShutdown.Both);
            //清理资源
            Server.Close();
            Server?.Dispose();
            Server = null;
            IsStarted = false;
            OnStoped?.Invoke(this);
            return true;
        }

        public bool Send(int connId, byte[] buffer)
        {
            if (Server == null || !IsStarted)
            {
                throw new Exception("尚未启动");
            }
            if (!_remoteEndPoints.TryGetValue(connId, out EndPoint remoteEP))
            {
                throw new Exception("终端不存在");
            }
            int sendCount = Server.SendTo(buffer, SocketFlags.None, remoteEP);
            var flag = sendCount == buffer.Length;
            if (flag)
            {
                OnSend?.Invoke(this, connId, buffer);
            }
            return flag;
        }

        public bool IsConnected
        {
            get { return Server.Connected; }
        }

        public int[] GetAllConnectionIDs()
        {
            return _remoteEndPoints.Keys.ToArray();
        }

        public int GetConnectionCount()
        {
            return _remoteEndPoints.Count;
        }

        public EndPoint GetLocalEndPoint()
        {
            return LocalEndpoint;
        }

        public EndPoint GetRemoteEndPoint(int connId)
        {
            if (!IsStarted) return null;
            if (_remoteEndPoints.TryGetValue(connId, out EndPoint remoteEP))
            {
                return remoteEP;
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
