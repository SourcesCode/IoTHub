using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Tcp.EventArgs
{
    /// <summary>
    /// TCP通信时客户端断开事件参数
    /// </summary>
    public class TCPClientDisConnectedEventArgs : BaseEventArgs
    {
        /// <summary>
        /// 断开服务器的终端（已经失效）
        /// </summary>
        public TCPEndPoint End
        {
            get;
            set;
        }
        /// <summary>
        /// 断开时间
        /// </summary>
        public DateTime Time
        {
            get;
            set;
        }
    }
}
