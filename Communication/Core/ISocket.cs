using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Communication.Core
{
    public interface ISocket
    {
        //EndPoint LocalEndPoint { get; set; }
        //EndPoint RemoteEndPoint { get; set; }
        bool Close();
        bool Send(int connId, byte[] buffer, int offset, int size);
        bool Receive(int connId, byte[] buffer, int offset, int size);
        bool Disconnect(int connId, bool isForce);

        /// <summary>
        /// 检测是否有效连接
        /// </summary>
        /// <returns></returns>
        bool IsConnected();
        /// <summary>
        /// 获取连接数
        /// </summary>
        /// <returns></returns>
        bool GetConnectionCount();
        /// <summary>
        /// 获取所有连接的 CONNID
        /// </summary>
        /// <returns></returns>
        int[] GetAllConnectionIDs();
        /// <summary>
        /// 获取某个连接的本地地址
        /// </summary>
        /// <param name="connId"></param>
        /// <returns></returns>
        EndPoint GetLocalEndPoint(int connId);
        /// <summary>
        /// 获取某个连接的远程地址
        /// </summary>
        /// <param name="connId"></param>
        /// <returns></returns>
        EndPoint GetRemoteEndPoint(int connId);
    }
}
