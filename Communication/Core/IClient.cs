using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Communication.Core
{
    public interface IClient
    {
        bool Start();

        bool Stop();
        bool Connect();
        bool Send(byte[] buffer, int offset, int size);
        /// <summary>
        /// 检测是否有效连接
        /// </summary>
        /// <returns></returns>
        bool IsConnected();
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
