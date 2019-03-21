using Communication.Core;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Communication.Tcp
{
    public class TcpServerFactory
    {
        private static int _serverId = -1;
        /// <summary>
        /// 服务器列表
        /// </summary>
        public static Dictionary<int, AsyncTcpServer> TcpServers { get; private set; }

        static TcpServerFactory()
        {
            TcpServers = new Dictionary<int, AsyncTcpServer>();
        }

        public static void CreateAndStart(int port)
        {
            _serverId++;
            var tcpServer = AsyncTcpServer.Create(port);
            tcpServer.Start();
            TcpServers.Add(_serverId, tcpServer);
        }

        public static void Send(int serverId, int connId, byte[] data)
        {
            var tcpServer = TcpServers[serverId];
            tcpServer.Send(connId, data, 0, data.Length);

        }

        public static void SetOnStarted(int serverId, Func<AsyncTcpServer, Socket, EventResult> func)
        {
            var tcpServer = TcpServers[serverId];
            tcpServer.OnStarted += func;
        }
        public static void SetOnConnected(int serverId, Func<AsyncTcpServer, int, Socket, EventResult> func)
        {
            var tcpServer = TcpServers[serverId];
            tcpServer.OnConnected += func;
        }
        public static void SetOnSend(int serverId, Func<AsyncTcpServer, int, byte[], int, int, EventResult> func)
        {
            var tcpServer = TcpServers[serverId];
            tcpServer.OnSend += func;
        }
        public static void SetOnReceive(int serverId, Func<AsyncTcpServer, int, byte[], int, int, EventResult> func)
        {
            var tcpServer = TcpServers[serverId];
            tcpServer.OnReceive += func;
        }
        public static void SetOnDisconnected(int serverId, Func<AsyncTcpServer, int, EventResult> func)
        {
            var tcpServer = TcpServers[serverId];
            tcpServer.OnDisconnected += func;
        }
        public static void SetOnStoped(int serverId, Func<AsyncTcpServer, EventResult> func)
        {
            var tcpServer = TcpServers[serverId];
            tcpServer.OnStoped += func;
        }


    }
}
