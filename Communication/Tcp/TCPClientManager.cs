using Communication.Tcp.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Tcp
{
    /// <summary>
    /// 说明：
    /// TCP通信客户端的代理
    /// 信息：
    /// 周智 2015.07.20
    /// </summary>
    public class TCPClientManager
    {
        private string _client_id;  //要代理的客户端ID

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="client_id">要代理的客户端ID，若不存在，则使用该client_id创建</param>
        public TCPClientManager(string client_id)
        {
            _client_id = client_id;
            if (!TCPClientOld.TCPClients.ContainsKey(_client_id))
            {
                TCPClientOld.TCPClients.Add(_client_id, new TCPClientOld(_client_id));
            }
        }
        /// <summary>
        /// 客户端连接状态
        /// </summary>
        public bool Connected
        {
            get
            {
                return TCPClientOld.TCPClients[_client_id].Connected;
            }
        }
        /// <summary>
        /// 心跳包发送时间间隔，默认为3秒（应小于服务器端Pulse）
        /// </summary>
        public int Pulse
        {
            get
            {
                return TCPClientOld.TCPClients[_client_id].Pulse;
            }
            set
            {
                TCPClientOld.TCPClients[_client_id].Pulse = value;
            }
        }
        /// <summary>
        /// 检查指定客户端是否存在
        /// </summary>
        /// <param name="client_id"></param>
        /// <returns></returns>
        public static bool ClientExist(string client_id)
        {
            return TCPClientOld.TCPClients.ContainsKey(client_id);
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ip">服务器IP</param>
        /// <param name="port">服务器端口</param>
        public void Connect(string ip, int port)
        {
            TCPClientOld.TCPClients[_client_id].Connect(ip, port);
        }
        /// <summary>
        /// 断开服务器
        /// </summary>
        public void DisConnect()
        {
            TCPClientOld.TCPClients[_client_id].DisConnect();
        }
        /// <summary>
        /// 向服务器同步发送数据
        /// </summary>
        /// <param name="msg">消息类型</param>
        /// <param name="data">消息数据正文</param>
        public void Send(Msg msg, byte[] data)
        {
            TCPClientOld.TCPClients[_client_id].Send(msg, data);
        }
        /// <summary>
        /// 向服务器异步发送数据
        /// </summary>
        /// <param name="msg">消息类型</param>
        /// <param name="data">消息数据正文</param>
        /// <param name="callback">回调方法</param>
        public void SendAsync(Msg msg, byte[] data, AsyncCallback callback)
        {
            TCPClientOld.TCPClients[_client_id].SendAsync(msg, data, callback);
        }

        /// <summary>
        /// 接收到服务器的消息时激发该事件
        /// </summary>
        public event Action<TCPMessageReceivedEventArgs> TCPMessageReceived
        {
            add
            {
                TCPClientOld.TCPClients[_client_id].TCPMessageReceived += value;
            }
            remove
            {
                TCPClientOld.TCPClients[_client_id].TCPMessageReceived -= value;
            }
        }
        /// <summary>
        /// 客户端连入服务器时激发该事件
        /// </summary>
        public event Action<TCPClientConnectedEventArgs> TCPClientConnected
        {
            add
            {
                TCPClientOld.TCPClients[_client_id].TCPClientConnected += value;
            }
            remove
            {
                TCPClientOld.TCPClients[_client_id].TCPClientConnected -= value;
            }
        }
        /// <summary>
        /// 客户端断开服务器时激发该事件
        /// </summary>
        public event Action<TCPClientDisConnectedEventArgs> TCPClientDisConnected
        {
            add
            {
                TCPClientOld.TCPClients[_client_id].TCPClientDisConnected += value;
            }
            remove
            {
                TCPClientOld.TCPClients[_client_id].TCPClientDisConnected -= value;
            }
        }
        /// <summary>
        /// 在心跳包发送时检测出断线时激发该事件
        /// </summary>
        public event Action<TCPClientDisConnected4PulseEventArgs> TCPClientDisConnected4Pulse
        {
            add
            {
                TCPClientOld.TCPClients[_client_id].TCPClientDisConnected4Pulse += value;
            }
            remove
            {
                TCPClientOld.TCPClients[_client_id].TCPClientDisConnected4Pulse -= value;
            }
        }
    }
}
