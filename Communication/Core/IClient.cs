using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Communication.Core
{
    public interface IClient
    {
        bool Connect(string remoteIp, int remotePort);
        bool Disconnect();
        int ReceiveBufferSize { get; set; }
        int SendBufferSize { get; set; }
        /// <summary>
        /// 检测是否有效连接
        /// </summary>
        /// <returns></returns>
        bool Connected { get; set; }
        Socket Client { get; set; }



        bool Send(byte[] buffer, int offset, int size);

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
