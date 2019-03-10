using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Tcp.EventArgs
{
    /// <summary>
    /// TCP消息参数
    /// </summary>
    public class TCPMessageReceivedEventArgs : BaseEventArgs
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public Msg Msg
        {
            get;
            set;
        }
        /// <summary>
        /// 消息数据
        /// </summary>
        public byte[] Data
        {
            set;
            get;
        }
        /// <summary>
        /// 终端
        /// </summary>
        public TCPEndPoint End
        {
            get;
            set;
        }
        /// <summary>
        /// 消息接收时间
        /// </summary>
        public DateTime Time
        {
            get;
            set;
        }
    }
}
