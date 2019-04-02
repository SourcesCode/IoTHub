using Communication.Core;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Communication.Tcp
{
    public class TcpServerContainer
    {
        //private static int _serverId = -1;
        /// <summary>
        /// 服务器列表
        /// </summary>
        public static Dictionary<int, AsyncTcpServer> TcpServers { get; private set; }

        static TcpServerContainer()
        {
            TcpServers = new Dictionary<int, AsyncTcpServer>();
        }

        public static AsyncTcpServer Create(int port)
        {
            //_serverId++;
            var tcpServer = AsyncTcpServer.Create(port);
            //tcpServer.Start();
            TcpServers.Add(port, tcpServer);
            return tcpServer;
        }

        public static bool Start(int serverId)
        {
            return TcpServers[serverId].Start();
        }

        public static bool Stop(int serverId)
        {
            return TcpServers[serverId].Stop();
        }

        public static void Send(int serverId, int connId, byte[] data)
        {
            var tcpServer = TcpServers[serverId];
            tcpServer.Send(connId, data);
        }

        public static void SetOnStarted(int serverId, Func<AsyncTcpServer, EventResult> func)
        {
            var tcpServer = TcpServers[serverId];
            tcpServer.OnStarted += func;
        }

        public static void SetOnConnected(int serverId, Func<AsyncTcpServer, int, EventResult> func)
        {
            var tcpServer = TcpServers[serverId];
            tcpServer.OnConnected += func;
        }

        public static void SetOnSend(int serverId, Func<AsyncTcpServer, int, byte[], EventResult> func)
        {
            var tcpServer = TcpServers[serverId];
            tcpServer.OnSend += func;
        }

        public static void SetOnReceive(int serverId, Func<AsyncTcpServer, int, byte[], EventResult> func)
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
