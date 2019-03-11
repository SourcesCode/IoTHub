using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Core
{
    public interface ISocketListener
    {
        event Func<int, byte[], int, EventResult> OnSend;

        event Func<int, byte[], int, EventResult> OnReceive;

        /// <summary>
        /// 接收到TCP消息时激发该事件
        /// </summary>
        event Func<IEventArgs, EventResult> TCPMessageReceived;
        /// <summary>
        /// 客户端连入服务器时激发该事件
        /// </summary>
        event Func<IEventArgs, EventResult> TCPClientConnected;
        /// <summary>
        /// 客户端断开服务器时激发该事件
        /// </summary>
        event Func<IEventArgs, EventResult> TCPClientDisConnected;
        /// <summary>
        /// 在心跳检测发现断线时激发该事件
        /// </summary>
        event Func<IEventArgs, EventResult> TCPClientDisConnected4Pulse;


    }
}
