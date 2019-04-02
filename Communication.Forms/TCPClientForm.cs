using Communication.Core;
using Communication.Tcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Communication.Forms
{
    public partial class TcpClientForm : Form
    {
        private AsyncTcpClient asyncTcpClient = null;
        public TcpClientForm()
        {
            InitializeComponent();
        }

        private void TcpClientForm_Load(object sender, EventArgs e)
        {
            this.txtIpAddress.Enabled = true;
            this.txtPort.Enabled = true;
            this.btnConnect.Enabled = true;
            this.btnDisconnect.Enabled = false;
            asyncTcpClient = new AsyncTcpClient();
            //asyncTcpClient.OnSend += OnSend;
            asyncTcpClient.OnReceive += OnReceive;
            asyncTcpClient.OnConnected += OnConnected;
            asyncTcpClient.OnDisconnected += OnDisconnected;
            //asyncTcpClient.OnClose += OnClose;
            this.dataReceiveAndSendUC1.Send += Send;

        }

        private EventResult OnSend(AsyncTcpClient arg1, byte[] buffer, int offset, int size)
        {
            return EventResult.Ok;
        }
        public EventResult OnReceive(AsyncTcpClient sender, byte[] buffer)
        {
            //var remoteEndPoint = sender.RemoteEndPoint.ToString();
            this.dataReceiveAndSendUC1.WriteLog(null, buffer);
            return EventResult.Ok;
        }
        private EventResult OnConnected(AsyncTcpClient sender)
        {
            this.dataReceiveAndSendUC1.WriteLog(sender.RemoteEndPoint.ToString(), "已连接");
            return EventResult.Ok;
        }
        private EventResult OnDisconnected(AsyncTcpClient sender)
        {
            this.Invoke(new Action(() =>
            {
                Disconnected();
            }));
            this.dataReceiveAndSendUC1.WriteLog(sender.RemoteEndPoint.ToString(), "已断开");
            return EventResult.Ok;
        }
        public bool Send(byte[] buffer)
        {
            //int connId = 0;
            if (!asyncTcpClient.IsRuning)
            {
                this.dataReceiveAndSendUC1.WriteLog(null, "未连接");
                return false;
            }
            asyncTcpClient.Send(buffer);
            return true;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string ipAddress = txtIpAddress.Text;
            int port = Convert.ToInt32(txtPort.Value);
            this.Text = $"{this.Tag} - {ipAddress}:{port}";
            try
            {
                var flag = asyncTcpClient.Connect(ipAddress, port);
                //if (flag)
                //{
                //    Connected();
                //}
            }
            catch (Exception ex)
            {
                this.dataReceiveAndSendUC1.WriteLog(null, ex.Message);
            }

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                var flag = asyncTcpClient.Disconnect();
                //if (flag)
                //{
                //    Disconnected();
                //}
            }
            catch (Exception ex)
            {
                this.dataReceiveAndSendUC1.WriteLog(null, ex.Message);
            }

        }

        private void TcpClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dataReceiveAndSendUC1.Close();
        }

        private void Connected()
        {
            this.txtIpAddress.Enabled = false;
            this.txtPort.Enabled = false;
            this.btnConnect.Enabled = false;
            this.btnDisconnect.Enabled = true;
        }

        private void Disconnected()
        {
            this.txtIpAddress.Enabled = true;
            this.txtPort.Enabled = true;
            this.btnConnect.Enabled = true;
            this.btnDisconnect.Enabled = false;
        }


    }
}
