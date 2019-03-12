using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Communication.Core
{
    public interface IClientListener : ISocketListener
    {
        /// <summary>
        /// 准备建立连接
        /// 准备建立连接前触发
        /// </summary>
        event Func<int, EventResult> OnPrepareConnect;
        /// <summary>
        /// 成功建立连接
        /// 成功建立连接后触发
        /// </summary>
        event Func<int, EventResult> OnConnected;
    }
}
