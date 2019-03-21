using System.Net;

namespace Communication.Core
{
    public interface ISocket
    {
        bool Send(int connId, byte[] buffer, int offset, int size);
        //bool Receive(int connId, byte[] buffer, int offset, int size);
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
        int GetConnectionCount();
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
