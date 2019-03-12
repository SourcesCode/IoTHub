using System.Net.Sockets;

namespace Communication.Core
{
    public interface ISocketListener<in T>
    {
        /// <summary>
        /// 握手完成
        /// 握手完成时触发
        /// </summary>
        EventResult OnHandShake(T sender, int connId);
        /// <summary>
        /// 接受连接请求
        /// 客户端连接请求到达时触发
        /// </summary>
        EventResult OnAccept(T sender, int connId, Socket socket);
        /// <summary>
        /// 数据已发送
        /// 数据发送成功后触发
        /// </summary>
        EventResult OnSend(T sender, int connId, byte[] buffer, int offset, int size);
        /// <summary>
        /// 数据到达
        /// 接受到数据时触发
        /// </summary>
        EventResult OnReceive(T sender, int connId, byte[] buffer, int offset, int size);
        /// <summary>
        /// 连接断开
        /// 连接断开后触发
        /// </summary>
        EventResult OnDisconnected(T sender, int connId);
        /// <summary>
        /// 连接关机
        /// 连接关机后触发
        /// </summary>
        EventResult OnShutdown(T sender);

        ///// <summary>
        ///// 接收到TCP消息时激发该事件
        ///// </summary>
        //event Func<IEventArgs, EventResult     TCPMessageReceived;
        ///// <summary>
        ///// 客户端连入服务器时激发该事件
        ///// </summary>
        //event Func<IEventArgs, EventResult     TCPClientConnected;
        ///// <summary>
        ///// 客户端断开服务器时激发该事件
        ///// </summary>
        //event Func<IEventArgs, EventResult     TCPClientDisConnected;
        ///// <summary>
        ///// 在心跳检测发现断线时激发该事件
        ///// </summary>
        //event Func<IEventArgs, EventResult     TCPClientDisConnected4Pulse;


    }
}
