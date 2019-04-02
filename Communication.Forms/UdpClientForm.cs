using Communication.Core;
using Communication.Udp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Communication.Forms
{
    public partial class UdpClientForm : Form
    {
        private AsyncUdpClient udpClient = null;
        private string _remoteIpAddress = string.Empty;
        private int _remotePort = 0;
        private IPEndPoint _remoteEndPoint = null;
        private int _localPort = 0;
        public UdpClientForm()
        {
            InitializeComponent();
        }

        private void UdpForm_Load(object sender, EventArgs e)
        {
            udpClient = new AsyncUdpClient();
            udpClient.OnConnected += OnConnected;
            udpClient.OnDisconnected += OnDisconnected;
            udpClient.OnReceive += OnReceive;

            this.dataReceiveAndSendUC1.Send += Send;
        }

        public EventResult OnConnected(AsyncUdpClient sender)
        {
            this.dataReceiveAndSendUC1.WriteLog(sender.LocalEndPoint.ToString(), "已启动");
            return EventResult.Ok;
        }
        public EventResult OnDisconnected(AsyncUdpClient sender)
        {
            this.dataReceiveAndSendUC1.WriteLog(sender.LocalEndPoint.ToString(), "已停止");
            this.Invoke(new Action(() =>
            {
                Stoped();
            }));
            return EventResult.Ok;
        }

        private List<int> ConnIdList = new List<int>();
        public EventResult OnReceive(AsyncUdpClient sender, byte[] buffer)
        {
            int connId = sender.RemoteEndPoint.GetHashCode();
            var remoteEndPoint = sender.RemoteEndPoint.ToString();
            this.dataReceiveAndSendUC1.WriteLog(remoteEndPoint, buffer);
            if (!ConnIdList.Contains(connId))
            {
                ConnIdList.Add(connId);
                this.Invoke(new Action(() =>
                {
                    //可以自定义数据结构来存储在线列表
                    this.dgvClientList.Rows.Add(connId, remoteEndPoint);
                    //this.dgvClientList.Rows[this.dgvClientList.Rows.Count - 1].Tag = sender.RemoteEndPoint;
                }));
            }
            return EventResult.Ok;
        }
        public bool Send(byte[] buffer)
        {
            try
            {
                udpClient.Send(buffer);
                return true;
            }
            catch (Exception ex)
            {
                this.dataReceiveAndSendUC1.WriteLog(null, ex.Message);
            }
            return false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                _remoteIpAddress = txtIpAddress.Text;
                _remotePort = Convert.ToInt32(txtPort.Value);
                _remoteEndPoint = new IPEndPoint(IPAddress.Parse(_remoteIpAddress), _remotePort);
                _localPort = Convert.ToInt32(txtLocalPort.Value);

                udpClient.SetLocalPort(_localPort);
                var flag = udpClient.Connect(_remoteIpAddress, _remotePort);
                if (flag)
                {
                    this.Started();
                }
            }
            catch (Exception ex)
            {
                this.dataReceiveAndSendUC1.WriteLog(null, ex.Message);
            }
        }

        private void Started()
        {
            this.txtIpAddress.Enabled = false;
            this.txtPort.Enabled = false;
            this.txtLocalPort.Enabled = false;
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                //结束服务器侦听
                var flag = udpClient.Disconnect();
                if (flag)
                {
                    this.Stoped();
                }
            }
            catch (Exception ex)
            {
                this.dataReceiveAndSendUC1.WriteLog(null, ex.Message);
            }
        }
        private void Stoped()
        {
            this.txtIpAddress.Enabled = true;
            this.txtPort.Enabled = true;
            this.txtLocalPort.Enabled = true;
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
        }
        private void TcpServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //结束服务器侦听
            udpClient.Disconnect();
        }

    }
}
