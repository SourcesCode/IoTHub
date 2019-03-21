using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Communication.Core
{
    public interface IClient
    {
        /// <summary>
        /// 客户端调用Connect()方法向服务端发起连接请求,如果成功则返回true,并且会先后收到OnPrepareConnect,OnConnected,OnHandShake事件
        /// </summary>
        /// <param name="remoteIp"></param>
        /// <param name="remotePort"></param>
        /// <returns></returns>
        bool Connect(string remoteIp, int remotePort);

        /// <summary>
        /// 客户端调用Send()方法向服务端发送数据后,如果成功则返回true,并且会收到OnSend事件
        /// </summary>
        /// <param name="connId"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        bool Send(int connId, byte[] buffer, int offset, int size);

        //服务端向客户端发送数据时,客户端会收到OnReceive事件
        //服务端向客户端断开连接时,并且会收到OnDisconnected事件

        /// <summary>
        /// 客户端调用Disconnect()方法断开连接,如果成功则返回true,并且会收到OnDisconnected事件
        /// </summary>
        /// <param name="connId"></param>
        /// <param name="isForce"></param>
        /// <returns></returns>
        bool Disconnect(int connId, bool isForce);


        int MaxBufferSize { get; set; }

        /// <summary>
        /// 检测是否有效连接
        /// </summary>
        /// <returns></returns>
        bool Connected { get; set; }
        Socket Client { get; set; }
        int GetConnectionID();
        /// <summary>
        /// 获取某个连接的本地地址
        /// </summary>
        /// <param name="connId"></param>
        /// <returns></returns>
        EndPoint GetLocalEndPoint();
        /// <summary>
        /// 获取某个连接的远程地址
        /// </summary>
        /// <param name="connId"></param>
        /// <returns></returns>
        EndPoint GetRemoteEndPoint();

    }
}
