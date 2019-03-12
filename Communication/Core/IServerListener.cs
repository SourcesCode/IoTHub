using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Communication.Core
{
    public interface IServerListener : ISocketListener
    {
        /// <summary>
        /// 启动服务
        /// 启动服务前触发
        /// </summary>
        event Func<Socket, EventResult> OnPrepareStart;
        /// <summary>
        /// 关闭服务
        /// 关闭服务后触发
        /// </summary>
        event Func<Socket, EventResult> OnStoped;

    }
}
