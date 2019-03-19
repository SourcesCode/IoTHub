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
        /// 启动服务后触发,服务端收到事件
        /// </summary>
        EventResult OnStarted(T sender, Socket socket);
        /// <summary>
        /// 关闭服务
        /// 关闭服务后触发,服务端收到事件
        /// </summary>
        EventResult OnStoped(T sender);
        /// <summary>
        /// 接受连接请求
        /// 发起连接请求时触发
        /// </summary>
        EventResult OnAccept(T sender, int connId, Socket socket);

    }
}
