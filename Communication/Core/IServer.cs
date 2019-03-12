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

    }
}
