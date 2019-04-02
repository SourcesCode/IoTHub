using System.Net;
using System.Net.Sockets;

namespace Communication.Core
{
    public interface IClient
    {
        EndPoint RemoteEndPoint { get; }
        EndPoint LocalEndPoint { get; }
        int MaxBufferSize { get; set; }
        /// <summary>
        /// 检测是否有效连接
        /// </summary>
        /// <returns></returns>
        bool IsRuning { get; }
        Socket Client { get; set; }
        /// <summary>
        /// 客户端调用Connect()方法向服务端发起连接请求,如果成功则返回true,并且会先后收到OnPrepareConnect,OnConnected,OnHandShake事件
        /// </summary>
        /// <param name="remoteIp"></param>
        /// <param name="remotePort"></param>
        /// <returns></returns>
        bool Connect(string remoteIp, int remotePort);
        bool Connect(EndPoint remoteEP);
        /// <summary>
        /// 客户端调用Disconnect()方法断开连接,如果成功则返回true,并且会收到OnDisconnected事件
        /// </summary>
        /// <param name="reuseSocket"></param>
        /// <returns></returns>
        bool Disconnect();
        /// <summary>
        /// 客户端调用Send()方法向服务端发送数据后,如果成功则返回true,并且会收到OnSend事件
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        bool Send(byte[] buffer);

        //服务端向客户端发送数据时,客户端会收到OnReceive事件
        //服务端向客户端断开连接时,并且会收到OnDisconnected事件



    }
}
