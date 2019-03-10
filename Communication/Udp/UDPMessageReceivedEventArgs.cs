using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Udp
{
    /// <summary>
    /// UDP消息参数
    /// </summary>
    public class UDPMessageReceivedEventArgs : BaseEventArgs
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
            get;
            set;
        }
        /// <summary>
        /// 远程IP
        /// </summary>
        public string RemoteIP
        {
            set;
            get;
        }
        /// <summary>
        /// 远程端口
        /// </summary>
        public int RemotePort
        {
            set;
            get;
        }
        /// <summary>
        /// 消息接收时间
        /// </summary>
        public DateTime Time
        {
            set;
            get;
        }
    }
}
