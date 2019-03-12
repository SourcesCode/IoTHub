using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Udp
{
    /// <summary>
    /// 说明：
    /// UDP客户端的代理
    /// 信息：
    /// 周智 2015.07.20
    /// </summary>
    public class UDPClientManager
    {
        private string _client_id; //要代理的客户端ID

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="client_id">要代理的客户端ID，若不存在，则使用该client_id创建</param>
        public UDPClientManager(string client_id)
        {
            _client_id = client_id;
            if (!UDPClientOld.UDPClients.ContainsKey(_client_id))
            {
                UDPClientOld.UDPClients.Add(_client_id, new UDPClientOld(_client_id));
            }
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="port">监听端口号</param>
        public void Start(int port)
        {
            UDPClientOld.UDPClients[_client_id].Start(port);
        }
        /// <summary>
        /// 接收到消息时激发该事件
        /// </summary>
        public event Action<UDPMessageReceivedEventArgs> UDPMessageReceived
        {
            add
            {
                UDPClientOld.UDPClients[_client_id].UDPMessageReceived += value;
            }
            remove
            {
                UDPClientOld.UDPClients[_client_id].UDPMessageReceived -= value;
            }
        }
        /// <summary>
        /// 检查客户端是否存在
        /// </summary>
        /// <param name="client_id">要检查的客户端ID</param>
        /// <returns></returns>
        public static bool ClientExist(string client_id)
        {
            return UDPClientOld.UDPClients.ContainsKey(client_id);
        }
        /// <summary>
        /// 同步发送数据
        /// </summary>
        /// <param name="msg">消息类型</param>
        /// <param name="data">数据正文</param>
        /// <param name="remoteIP">远程IP</param>
        /// <param name="remotePort">远程端口</param>
        public void SendTo(Msg msg, byte[] data, string remoteIP, int remotePort)
        {
            UDPClientOld.UDPClients[_client_id].SendTo(msg, data, remoteIP, remotePort);
        }
        /// <summary>
        /// 客户端端口监听状态
        /// </summary>
        public bool Runing
        {
            get
            {
                return UDPClientOld.UDPClients[_client_id].Runing;
            }
        }
        /// <summary>
        /// 停止监听端口
        /// </summary>
        public void Stop()
        {
            UDPClientOld.UDPClients[_client_id].Stop();
        }
    }
}
