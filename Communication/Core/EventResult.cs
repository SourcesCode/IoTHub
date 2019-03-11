using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Core
{
    public enum EventResult
    {
        Ok = 0,
        Ignore = 1,
        /// <summary>
        /// 立即中断连接
        /// </summary>
        Error = 2
    }
}
