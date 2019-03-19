using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Core
{
    public interface IServer : ISocket
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        bool Start();

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        bool Stop();

        /// <summary>
        /// 设置心跳检测包发送间隔
        /// </summary>
        /// <param name="second">秒</param>
        /// <returns></returns>
        bool SetHeartBeatInterval(int second);
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
