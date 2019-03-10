using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Tcp.EventArgs
{
    /// <summary>
    /// 表示处理在心跳检测时发现断线这一事件参数
    /// </summary>
    public class TCPClientDisConnected4PulseEventArgs : BaseEventArgs
    {
        public int Uid { get; set; }
    }
}
