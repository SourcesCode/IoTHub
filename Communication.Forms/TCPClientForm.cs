using Communication.Tcp;
using Communication.Tcp.EventArgs;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Communication.Forms
{
    public partial class TCPClientForm : Form
    {
        private string _client_id;
        public TCPClientForm()
        {
            InitializeComponent();
        }

        public TCPClientForm(string client_id)
            : this()
        {
            _client_id = client_id;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TCPClientForm_Load(object sender, EventArgs e)
        {
            Text = "TCPClient " + _client_id;

            //注册事件
            TCPClientManager manager = new TCPClientManager(_client_id); //访问客户端
            manager.TCPMessageReceived += manager_TCPMessageReceived;
            manager.TCPClientDisConnected4Pulse += manager_TCPClientDisConnected4Pulse;
            manager.TCPClientDisConnected += manager_TCPClientDisConnected;
        }

        /// <summary>
        /// 发送文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSendText_Click(object sender, EventArgs e)
        {
            TCPClientManager manager = new TCPClientManager(_client_id);
            manager.Send(Msg.Zmsg1, Encoding.Unicode.GetBytes(textBox2.Text)); //同步发送文本

        }

        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSendPicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "图片文件|*.jpg;*jpeg";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBox3.Text = ofd.FileName;
                    Image image = Image.FromFile(textBox3.Text);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, ImageFormat.Jpeg);

                        TCPClientManager manager = new TCPClientManager(_client_id);
                        manager.SendAsync(Msg.Zmsg2, ms.ToArray(), null);  //异步发送图片
                    }
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            TCPClientManager manager = new TCPClientManager(_client_id);
            manager.DisConnect();
            Close();
        }


        #region
        /// <summary>
        /// 断线
        /// </summary>
        /// <param name="csID"></param>
        /// <param name="args"></param>
        void manager_TCPClientDisConnected(TCPClientDisConnectedEventArgs args)
        {
            this.Invoke((Action)delegate ()
            {
                textBox1.AppendText(args.Time.ToLongTimeString() + " 与服务器断开连接\n");
            });
        }
        /// <summary>
        /// 心跳包发送失败
        /// </summary>
        /// <param name="csID"></param>
        /// <param name="uid"></param>
        void manager_TCPClientDisConnected4Pulse(TCPClientDisConnected4PulseEventArgs args)
        {
            this.Invoke((Action)delegate ()
            {
                textBox1.AppendText("发送心跳包失败\n");
            });
        }
        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="csID"></param>
        /// <param name="args"></param>
        void manager_TCPMessageReceived(TCPMessageReceivedEventArgs args)
        {
            this.Invoke((Action)delegate ()
            {
                if (args.Msg == Msg.Zmsg1)  //文本
                {
                    textBox1.AppendText(args.Time.ToLongTimeString() + " " + args.End.RemoteIP + ":" + args.End.RemotePort + " 发送文本:\n"
                        + Encoding.Unicode.GetString(args.Data) + "\n");
                }
                if (args.Msg == Msg.Zmsg2)  //图片
                {
                    textBox1.AppendText(args.Time.ToLongTimeString() + " " + args.End.RemoteIP + ":" + args.End.RemotePort + " 发送图片:\n"
                        + "见右方-->\n");
                    Image image = Image.FromStream(new MemoryStream(args.Data));
                    pictureBox1.Image = image;
                }
            });
        }
        #endregion
    }
}
