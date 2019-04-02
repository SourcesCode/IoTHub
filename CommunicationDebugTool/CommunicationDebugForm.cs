using Communication.Core;
using Communication.Tcp;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunicationDebugTool
{
    public partial class CommunicationDebugForm : Form
    {
        private int _communicationWay = 1;
        private AsyncTcpServer tcpServer = null;
        private AsyncTcpClient asyncTcpClient = null;
        private AsyncUdpClient udpClient = null;
        private string _remoteIpAddress = string.Empty;
        private int _remotePort = 0;
        private IPEndPoint _remoteEndPoint = null;
        private int _localPort = 0;
        public CommunicationDebugForm()
        {
            InitializeComponent();
        }

        private void CommunicationDebugForm_Load(object sender, EventArgs e)
        {
            InitTcpServer();
            this.txtReceiveLog.ReadOnly = true;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorkerDoWork);
        }

        private BackgroundWorker _backgroundWorker = new BackgroundWorker();

        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            while (!_backgroundWorker.CancellationPending)
            {
                string sendText = this.txtSendData.Text;
                DoSend(sendText);
                int seconds = Convert.ToInt32(this.numInterval.Value);
                Thread.Sleep(seconds * 1000);
            }
        }

        private void DoSend(string sendText)
        {
            if (string.IsNullOrWhiteSpace(sendText))
            {
                WriteLog(null, "发送内容不能为空");
                return;
            }
            byte[] sendBytes = null;
            if (this.chkIsHexSend.Checked)
            {
                sendBytes = ByteHelper.ToBytesFromHexString(sendText);
                if (sendBytes == null || sendBytes.Length == 0)
                {
                    WriteLog(null, "无效的16进制字符串");
                    return;
                }
            }
            else
            {
                sendBytes = Encoding.UTF8.GetBytes(sendText);
            }
            try
            {
                switch (_communicationWay)
                {
                    case 1://Tcp Server
                        SendTcpServer(sendBytes);
                        break;
                    case 2://Tcp Client
                        asyncTcpClient.Send(sendBytes);
                        break;
                    case 3://Udp
                        udpClient.Send(sendBytes);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.WriteLog(null, ex.Message);
            }

        }
        public bool SendTcpServer(byte[] buffer)
        {
            if (this.dgvClientList.SelectedRows.Count == 0)
            {
                this.WriteLog(null, "请选中至少一个客户端");
                return true;
            }

            foreach (DataGridViewRow row in this.dgvClientList.SelectedRows)
            {
                int connId = (int)row.Tag;
                tcpServer.Send(connId, buffer);
            }
            return true;
        }

        public void WriteLog(string endPoint, string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            AppendReceiveLog(endPoint, data, 0, data.Length, false);
        }

        public void WriteLog(string endPoint, byte[] buffer)
        {
            AppendReceiveLog(endPoint, buffer, 0, buffer.Length, this.chkIsHexDisplay.Checked);
        }

        private void AppendReceiveLog(string endPoint, byte[] buffer, int offset, int size, bool isHexDisplay)
        {
            if (buffer == null || buffer.Length == 0)
            {
                return;
            }
            var receiveText = string.Empty;
            if (isHexDisplay)
            {
                receiveText = ByteHelper.ToHexString(buffer, offset, size);
            }
            else
            {
                receiveText = Encoding.UTF8.GetString(buffer, offset, size);
            }
            string msg = string.Empty;
            if (string.IsNullOrWhiteSpace(endPoint))
            {
                msg = $"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")}]:{receiveText}";
            }
            else
            {
                msg = $"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")},{endPoint}]:{receiveText}";
            }
            this.Invoke(new Action(() =>
            {
                //_receiveLog.AppendLine(sendText);
                //this.txtReceiveLog.Text = _receiveLog.ToString();

                this.txtReceiveLog.AppendText(msg);
                this.txtReceiveLog.AppendText(Environment.NewLine);
            }));

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _remoteIpAddress = txtIpAddress.Text;
            _remotePort = Convert.ToInt32(txtRemotePort.Value);
            _remoteEndPoint = new IPEndPoint(IPAddress.Parse(_remoteIpAddress), _remotePort);
            _localPort = Convert.ToInt32(txtLocalPort.Value);

            try
            {
                var flag = false;
                switch (_communicationWay)
                {
                    case 1://Tcp Server
                        flag = StartTcpServer();
                        break;
                    case 2://Tcp Client
                        flag = StartTcpClient();
                        break;
                    case 3://Udp
                        flag = StartUdp();
                        break;
                }
                if (flag)
                {
                    Started();
                }
            }
            catch (Exception ex)
            {
                this.WriteLog(null, ex.Message);
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                var flag = false;
                //结束服务器侦听
                switch (_communicationWay)
                {
                    case 1://Tcp Server
                        flag = tcpServer.Stop();
                        break;
                    case 2://Tcp Client
                        flag = asyncTcpClient.Disconnect();
                        break;
                    case 3://Udp
                        //结束服务器侦听
                        flag = udpClient.Disconnect();
                        break;
                }
                if (flag)
                {
                    Stoped();
                }
            }
            catch (Exception ex)
            {
                this.WriteLog(null, ex.Message);
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string sendText = this.txtSendData.Text;
            DoSend(sendText);
        }

        private void btnClearReceiveLog_Click(object sender, EventArgs e)
        {
            txtReceiveLog.Clear();
        }

        private void btnClearSendLog_Click(object sender, EventArgs e)
        {
            txtSendData.Clear();
        }

        private void chkIsAutoSend_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkIsAutoSend.Checked)
            {
                if (!_backgroundWorker.IsBusy)
                {
                    _backgroundWorker.RunWorkerAsync();
                }
            }
            else
            {
                _backgroundWorker.CancelAsync();
            }
        }

        private void rdoCommunicationWayGroup_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = sender as RadioButton;
            if (!rdo.Checked)
            {
                return;
            }
            string sText = string.Empty;
            _communicationWay = Convert.ToInt32(rdo.Tag);
            switch (_communicationWay)
            {
                case 1://Tcp Server
                    InitTcpServer();
                    break;
                case 2://Tcp Client
                    InitTcpClient();
                    break;
                case 3://Udp
                    InitUdp();
                    break;
            }

        }

        private void InitTcpServer()
        {
            this.txtRemotePort.Text = "";
            this.txtRemotePort.Enabled = false;
            this.txtLocalPort.Text = "6666";
            this.txtLocalPort.Enabled = true;
            this.btnStart.Text = "监听";
        }
        private void InitTcpClient()
        {
            this.txtRemotePort.Text = "6666";
            this.txtRemotePort.Enabled = true;
            this.txtLocalPort.Text = "";
            this.txtLocalPort.Enabled = false;
            this.btnStart.Text = "连接";
        }
        private void InitUdp()
        {
            this.txtRemotePort.Text = "6666";
            this.txtRemotePort.Enabled = true;
            this.txtLocalPort.Text = "6666";
            this.txtLocalPort.Enabled = true;
            this.btnStart.Text = "启动";
        }

        private void Started()
        {
            this.dgvClientList.Rows.Clear();
            this.ConnIdList.Clear();
            this.rdoTcpServer.Enabled = false;
            this.rdoTcpClient.Enabled = false;
            this.rdoUdp.Enabled = false;
            this.txtIpAddress.Enabled = false;
            this.txtRemotePort.Enabled = false;
            this.txtLocalPort.Enabled = false;
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;

        }

        private void Stoped()
        {
            this.rdoTcpServer.Enabled = true;
            this.rdoTcpClient.Enabled = true;
            this.rdoUdp.Enabled = true;
            this.chkIsAutoSend.Checked = false;
            this.txtIpAddress.Enabled = true;
            this.txtRemotePort.Enabled = true;
            this.txtLocalPort.Enabled = true;
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
        }

        #region StartTcpServer
        private bool StartTcpServer()
        {
            var result = false;
            tcpServer = AsyncTcpServer.Create(_localPort);
            tcpServer.OnConnected += OnConnected;
            tcpServer.OnDisconnected += OnDisconnected;
            tcpServer.OnReceive += OnReceive;
            result = tcpServer.Start();
            return result;
        }

        public EventResult OnConnected(AsyncTcpServer sender, int connId)
        {
            var remoteEndPoint = sender.GetRemoteEndPoint(connId).ToString();
            this.WriteLog(remoteEndPoint, "已连接");
            this.Invoke(new Action(() =>
            {
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
                if (removeIndex > -1)
                {
                    dgvClientList.Rows.RemoveAt(removeIndex);
                }
                this.WriteLog(remoteEndPoint, "已断开");
            }));
            return EventResult.Ok;
        }
        public EventResult OnReceive(AsyncTcpServer sender, int connId, byte[] buffer)
        {
            var remoteEndPoint = sender.GetRemoteEndPoint(connId).ToString();
            this.WriteLog(remoteEndPoint, buffer);
            return EventResult.Ok;
        }
        #endregion

        #region StartTcpClient
        private bool StartTcpClient()
        {
            var result = false;
            asyncTcpClient = new AsyncTcpClient();

            asyncTcpClient.OnReceive += OnReceive;
            asyncTcpClient.OnConnected += OnConnected;
            asyncTcpClient.OnDisconnected += OnDisconnected;
            result = asyncTcpClient.Connect(_remoteEndPoint);
            return result;
        }

        private EventResult OnConnected(AsyncTcpClient sender)
        {
            this.WriteLog(sender.RemoteEndPoint.ToString(), "已连接");
            return EventResult.Ok;
        }
        private EventResult OnDisconnected(AsyncTcpClient sender)
        {
            this.WriteLog(sender.RemoteEndPoint.ToString(), "已断开");
            this.Invoke(new Action(() =>
            {
                Stoped();
            }));
            return EventResult.Ok;
        }
        public EventResult OnReceive(AsyncTcpClient sender, byte[] buffer)
        {
            this.WriteLog(_remoteEndPoint.ToString(), buffer);
            return EventResult.Ok;
        }
        #endregion

        #region StartUdp
        private bool StartUdp()
        {
            var result = false;
            udpClient = new AsyncUdpClient();
            udpClient.OnConnected += OnConnected;
            udpClient.OnDisconnected += OnDisconnected;
            udpClient.OnReceive += OnReceive;

            udpClient.SetLocalPort(_localPort);
            result = udpClient.Connect(_remoteIpAddress, _remotePort);
            return result;
        }

        public EventResult OnConnected(AsyncUdpClient sender)
        {
            this.WriteLog(sender.LocalEndPoint.ToString(), "已启动");
            return EventResult.Ok;
        }
        public EventResult OnDisconnected(AsyncUdpClient sender)
        {
            this.WriteLog(sender.LocalEndPoint.ToString(), "已停止");
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
            this.WriteLog(remoteEndPoint, buffer);
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

        #endregion

    }
}
