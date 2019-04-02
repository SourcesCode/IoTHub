using Communication.Core;
using Communication.Tcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Communication.Forms
{
    public partial class TcpServerForm : Form
    {
        private AsyncTcpServer tcpServer = null;
        public TcpServerForm()
        {
            InitializeComponent();
        }

        private void TcpServerForm_Load(object sender, EventArgs e)
        {
            var port = Convert.ToInt32(txtPort.Value);
            tcpServer = TcpServerContainer.Create(port);
            tcpServer.OnConnected += OnConnected;
            tcpServer.OnDisconnected += OnDisconnected;
            tcpServer.OnReceive += OnReceive;

            this.dataReceiveAndSendUC1.Send += Send;
        }
        public EventResult OnConnected(AsyncTcpServer sender, int connId)
        {
            this.Invoke(new Action(() =>
            {
                var remoteEndPoint = sender.GetRemoteEndPoint(connId).ToString();
                this.dataReceiveAndSendUC1.WriteLog(remoteEndPoint, "已连接");
                //可以自定义数据结构来存储在线列表
                this.dgvClientList.Rows.Add(connId, remoteEndPoint);
                this.dgvClientList.Rows[this.dgvClientList.Rows.Count - 1].Tag = connId;
            }));
            return EventResult.Ok;
        }
        public EventResult OnDisconnected(AsyncTcpServer sender, int connId)
        {
            if (this.IsDisposed) return EventResult.Ok;
            this.Invoke(new Action(() =>
            {
                var remoteEndPoint = string.Empty;
                int removeIndex = -1;
                foreach (DataGridViewRow row in this.dgvClientList.Rows)
                {
                    if ((int)row.Tag == connId)
                    {
                        remoteEndPoint = row.Cells[1].Value.ToString();
                        removeIndex = row.Index;
                        break;
                    }
                }
                if (removeIndex >= 0)
                {
                    dgvClientList.Rows.RemoveAt(removeIndex);
                }
                this.dataReceiveAndSendUC1.WriteLog(remoteEndPoint, "已断开");
            }));
            return EventResult.Ok;
        }
        public EventResult OnReceive(AsyncTcpServer sender, int connId, byte[] buffer)
        {
            var remoteEndPoint = sender.GetRemoteEndPoint(connId).ToString();
            this.dataReceiveAndSendUC1.WriteLog(remoteEndPoint, buffer);
            return EventResult.Ok;
        }
        public bool Send(byte[] buffer)
        {
            if (this.dgvClientList.SelectedRows.Count == 0)
            {
                this.dataReceiveAndSendUC1.WriteLog(null, "请选中至少一个客户端");
                return true;
            }

            foreach (DataGridViewRow row in this.dgvClientList.SelectedRows)
            {
                int connId = (int)row.Tag;
                tcpServer.Send(connId, buffer);
            }
            return true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.dgvClientList.Rows.Clear();
            try
            {
                var flag = tcpServer.Start();
                if (flag)
                {
                    this.txtIpAddress.Enabled = false;
                    this.txtPort.Enabled = false;
                    this.btnStart.Enabled = false;
                    this.btnStop.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.dataReceiveAndSendUC1.WriteLog(null, ex.Message);
            }
            //if (TCPServerManager.ServerExist(textBox2.Text))
            //{
            //    MessageBox.Show("服务器已存在!");
            //    return;
            //}

            //TCPServerManager manager = new TCPServerManager(textBox2.Text);  //创建服务器
            //manager.Start(int.Parse(textBox1.Text)); //启动服务器

            //frmTCPServer frmtcpserver = new frmTCPServer(textBox2.Text, int.Parse(textBox1.Text));
            //frmtcpserver.Show();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //结束服务器侦听
            tcpServer.Stop();
            this.txtIpAddress.Enabled = true;
            this.txtPort.Enabled = true;
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;

        }

        private void TcpServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //结束服务器侦听
            tcpServer.Stop();
        }

    }
}
