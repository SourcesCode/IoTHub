using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Communication.Tcp.EventArgs;

namespace Communication.Tcp
{
    public class TcpClient : ITcpClient, ITcpClientListener
    {
        private readonly ITcpClientListener _listener;
        public TcpClient(ITcpClientListener listener)
        {
            var sss = new System.Net.Sockets.TcpClient();
             
            listener.OnPrepareConnect += OnPrepareConnect;

            //OnPrepareConnect += listener.OnPrepareConnect;

            _listener = listener;
        }

        public int ReceiveBufferSize { get; set; }
        public int SendBufferSize { get; set; }
        public bool Connected { get; set; }
        public Socket Client { get; set; }

        private IPEndPoint _remoteEP = null;

        public event Func<int, EventResult> OnPrepareConnect;
        public event Func<int, EventResult> OnConnected;
        public event Func<int, byte[], int, int, EventResult> OnHandShake;
        public event Func<int, Socket, EventResult> OnAccept;
        public event Func<int, byte[], int, int, EventResult> OnSend;
        public event Func<int, byte[], int, int, EventResult> OnReceive;
        public event Func<int, EventResult> OnDisconnected;

        public bool Connect(string remoteIp, int remotePort)
        {
            if (Connected) return true;
            if (Client == null)
            {
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            _remoteEP = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);
            EventResult? onPrepareConnectResult = OnPrepareConnect?.Invoke(1);
            Client.Connect(_remoteEP);
            Connected = true;
            EventResult? onConnectedResult = OnConnected?.Invoke(1);
            if (onConnectedResult.HasValue && onConnectedResult == EventResult.Error)
            {
                Disconnect();
            }
            byte[] buffer = new byte[ReceiveBufferSize];
            Client.BeginReceive(buffer, 0, ReceiveBufferSize, SocketFlags.None, new AsyncCallback(BeginReceiveCallback), null);



            return true;
        }

        /// <summary>
        /// 接收数据回调方法
        /// </summary>
        /// <param name="ar"></param>
        private void BeginReceiveCallback(IAsyncResult ar)
        {
            TCPEndPoint end = ar.AsyncState as TCPEndPoint;
            try
            {
                byte[] buffer = new byte[1024];
                int real_recv = end.Socket.EndReceive(ar);
                EventResult? result = OnReceive?.Invoke(1, buffer, 0, ReceiveBufferSize);
                if (result.HasValue && result == EventResult.Error)
                {
                    Disconnect();
                }

                end.MStream.Write(end.Buffer, 0, real_recv); //写入消息缓冲区
                //尝试读取一条完整消息
                ZMessage msg;
                while (end.MStream.ReadMessage(out msg))
                {
                    //处理消息
                    TCPMessageReceivedEventArgs args = new TCPMessageReceivedEventArgs();
                    //args.CsID = _client_id;
                    args.Msg = (Msg)msg.head;
                    args.Time = DateTime.Now;
                    args.End = end;
                    args.Data = msg.content;

                    //激发事件，通知事件注册者处理消息
                    //TCPMessageReceived?.BeginInvoke(args, null, null);

                }
                end.Socket.BeginReceive(end.Buffer, 0, 1024, SocketFlags.None, new AsyncCallback(BeginReceiveCallback), end);
            }
            catch
            {
                TCPClientDisConnectedEventArgs args = new TCPClientDisConnectedEventArgs();
                //args.CsID = _client_id;
                args.End = end;
                args.Time = DateTime.Now;
                //通知客户端断开
                //TCPClientDisConnected?.Invoke(args);

            }
        }


        public bool Disconnect()
        {
            throw new NotImplementedException();
        }

        public int GetConnectionID()
        {
            throw new NotImplementedException();
        }

        public EndPoint GetLocalEndPoint()
        {
            throw new NotImplementedException();
        }

        public EndPoint GetRemoteEndPoint()
        {
            throw new NotImplementedException();
        }

        public bool Send(byte[] buffer, int offset, int size)
        {
            throw new NotImplementedException();
        }
    }
}
