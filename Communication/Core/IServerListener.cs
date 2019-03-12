using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Communication.Core
{
    public interface IServerListener<in T> : ISocketListener<T> where T : IServer
    {
        /// <summary>
        /// 启动服务
        /// 启动服务前触发
        /// </summary>
        EventResult OnPrepareStart(T sender, Socket socket);
        /// <summary>
        /// 关闭服务
        /// 关闭服务后触发
        /// </summary>
        EventResult OnStoped(T sender);

    }
}
