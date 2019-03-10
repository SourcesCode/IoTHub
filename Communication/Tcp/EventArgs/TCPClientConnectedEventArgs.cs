using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Tcp.EventArgs
{
    /// <summary>
    /// TCP通信时客户端连入事件参数
    /// </summary>
    public class TCPClientConnectedEventArgs : BaseEventArgs
    {
        /// <summary>
        /// 新连入的终端
        /// </summary>
        public TCPEndPoint End
        {
            get;
            set;
        }
        /// <summary>
        /// 连入时间
        /// </summary>
        public DateTime Time
        {
            get;
            set;
        }
    }
}
