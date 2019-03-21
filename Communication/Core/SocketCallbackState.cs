using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Communication.Core
{
    public class SocketCallbackState
    {
        public int ConnId { get; set; }
        public Socket Socket { get; set; }  //负责通信的Socket
        public byte[] Buffer { get; set; }  //接收数据系统缓冲区

        /// <summary>
        /// 将字节流写入缓冲区
        /// </summary>
        /// <param name="offset">写入字节流的开始位置</param>
        /// <param name="size">写入字节大小</param>
        public byte[] Read(int offset, int size)
        {
            int length = size - offset;
            byte[] receiveBuffer = new byte[size];
            System.Buffer.BlockCopy(Buffer, offset, receiveBuffer, 0, size);
            return receiveBuffer;
        }

    }
}
