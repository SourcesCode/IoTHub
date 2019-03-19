using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Core
{
    public interface IAgent : ISocket
    {
        /// <summary>
        /// 客户端调用Start()方法启动Agent组件
        /// </summary>
        /// <returns></returns>
        bool Start();
        /// <summary>
        /// 客户端向服务端发起连接请求,如果成功则返回true,并且会先后收到OnPrepareConnect,OnConnected,OnHandShake事件
        /// </summary>
        /// <param name="connId"></param>
        /// <returns></returns>
        bool Connect(int connId);
        /// <summary>
        /// 客户端调用Stop()方法关闭Agent组件,如果成功则返回true,并且收到OnStoped事件
        /// </summary>
        /// <returns></returns>
        bool Stop();

    }
}
