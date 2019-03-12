using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Communication.Core
{
    public interface ISocketListener
    {
        /// <summary>
        /// 握手完成
        /// 握手完成时触发
        /// </summary>
        event Func<int, byte[], int, int, EventResult> OnHandShake;
        /// <summary>
        /// 接受连接请求
        /// 客户端连接请求到达时触发
        /// </summary>
        event Func<int, Socket, EventResult> OnAccept;
        /// <summary>
        /// 数据已发送
        /// 数据发送成功后触发
        /// </summary>
        event Func<int, byte[], int, int, EventResult> OnSend;
        /// <summary>
        /// 数据到达
        /// 接受到数据时触发
        /// </summary>
        event Func<int, byte[], int, int, EventResult> OnReceive;
        /// <summary>
        /// 连接断开
        /// 连接断开后触发
        /// </summary>
        event Func<int, EventResult> OnDisconnected;

        ///// <summary>
        ///// 接收到TCP消息时激发该事件
        ///// </summary>
        //event Func<IEventArgs, EventResult> TCPMessageReceived;
        ///// <summary>
        ///// 客户端连入服务器时激发该事件
        ///// </summary>
        //event Func<IEventArgs, EventResult> TCPClientConnected;
        ///// <summary>
        ///// 客户端断开服务器时激发该事件
        ///// </summary>
        //event Func<IEventArgs, EventResult> TCPClientDisConnected;
        ///// <summary>
        ///// 在心跳检测发现断线时激发该事件
        ///// </summary>
        //event Func<IEventArgs, EventResult> TCPClientDisConnected4Pulse;


    }
}
