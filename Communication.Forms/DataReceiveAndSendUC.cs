using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Communication.Tcp;
using Communication.Core;
using System.Threading;

namespace Communication.Forms
{
    public partial class DataReceiveAndSendUC : UserControl
    {
        private BackgroundWorker _backgroundWorker = new BackgroundWorker();

        public DataReceiveAndSendUC()
        {
            InitializeComponent();
        }

        private void DataReceiveAndSendUC_Load(object sender, EventArgs e)
        {
            this.txtReceiveLog.ReadOnly = true;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorkerDoWork);
        }

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

        public event Func<byte[], bool> Send;

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

            bool result = Send.Invoke(sendBytes);

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
            this.txtReceiveLog.Invoke(new Action(() =>
            {
                //_receiveLog.AppendLine(sendText);
                //this.txtReceiveLog.Text = _receiveLog.ToString();

                this.txtReceiveLog.AppendText(msg);
                this.txtReceiveLog.AppendText(Environment.NewLine);
            }));

        }

        public void Close()
        {
            _backgroundWorker.CancelAsync();
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

        private void txtReceiveLog_TextChanged(object sender, EventArgs e)
        {
            //this.txtReceiveLog.SelectionStart = this.txtReceiveLog.Text.Length;
            //this.txtReceiveLog.SelectionLength = 0;
            //this.txtReceiveLog.ScrollToCaret();
        }

        private void txtSendData_TextChanged(object sender, EventArgs e)
        {
            //this.txtSendData.SelectionStart = this.txtSendData.Text.Length;
            //this.txtSendData.SelectionLength = 0;
            //this.txtSendData.ScrollToCaret();
        }

        private void chkIsAutoSend_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkIsAutoSend.Checked && !_backgroundWorker.IsBusy)
            {
                _backgroundWorker.RunWorkerAsync();
            }
            else
            {
                _backgroundWorker.CancelAsync();
            }
        }

    }
}
