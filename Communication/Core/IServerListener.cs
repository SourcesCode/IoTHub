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
        /// 启动服务前触发
        /// </summary>
        EventResult OnPrepareStart(T sender, Socket socket);
        /// <summary>
        /// 关闭服务
        /// 关闭服务后触发
        /// </summary>
        EventResult OnStoped(T sender);

        /// <summary>
        /// 设置心跳检测包发送间隔
        /// </summary>
        /// <param name="tttt"></param>
        /// <returns></returns>
        bool SetHeartBeatInterval(int tttt);
        /// <summary>
        /// 设置Socket数据缓冲区大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        bool SetSocketBufferSize(int size);
        /// <summary>
        /// 设置监听Socket的等候队列长度
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        bool SetSocketListenQueueLength(int length);
        /// <summary>
        /// 设置工作线程数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        bool SetWorkerThreadCount(int count);
        /// <summary>
        /// 设置最大连接数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        bool SetMaxConnectionCount(int count);

    }
}
