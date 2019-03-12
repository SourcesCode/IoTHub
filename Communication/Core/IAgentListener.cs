using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Communication.Core
{
    public interface IAgentListener<in T> : ISocketListener<T> where T : IAgent
    {
        /// <summary>
        /// 准备建立连接
        /// 准备建立连接前触发
        /// </summary>
        EventResult OnPrepareConnect(T sender, int connId, Socket socket);
        /// <summary>
        /// 成功建立连接
        /// 成功建立连接后触发
        /// </summary>
        EventResult OnConnected(T sender, int connId);

    }
}
