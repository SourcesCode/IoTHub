using Communication;
using Communication.Tcp;
using Communication.Tcp.EventArgs;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TCPServer
{
    /// <summary>
    /// 服务器窗体   简单的使用datagridview维护在线列表
    /// </summary>
    public partial class frmTCPServer : Form
    {
        private string _server_id;
        private int _port;

        public frmTCPServer()
        {
            InitializeComponent();
        }
        public frmTCPServer(string server_id, int port)
            : this()
        {
            _server_id = server_id;
            _port = port;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTCPServer_Load(object sender, EventArgs e)
        {
            Text = "TCPServer " + _server_id + ":" + _port;

            //注册事件
            TCPServerManager manager = new TCPServerManager(_server_id);  //访问_server_id服务器
            manager.TCPMessageReceived += manager_TCPMessageReceived;
            manager.TCPClientConnected += manager_TCPClientConnected;
            manager.TCPClientDisConnected += manager_TCPClientDisConnected;
            manager.TCPClientDisConnected4Pulse += manager_TCPClientDisConnected4Pulse;
        }

        #region
        /// <summary>
        /// 心跳检测时用户断线
        /// </summary>
        /// <param name="csID"></param>
        /// <param name="uid"></param>
        void manager_TCPClientDisConnected4Pulse(TCPClientDisConnected4PulseEventArgs args)
        {
            this.Invoke((Action)delegate ()
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value.ToString() == args.Uid.ToString())
                    {
                        dataGridView1.Rows.Remove(row);
                        break;
                    }
                }
                textBox1.AppendText("未收到终端唯一标识为[" + args.Uid + "]的心跳包\n");
            });
        }
        /// <summary>
        /// 用户断线
        /// </summary>
        /// <param name="csID"></param>
        /// <param name="args"></param>
        void manager_TCPClientDisConnected(TCPClientDisConnectedEventArgs args)
        {
            this.Invoke((Action)delegate ()
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value.ToString() == args.End.UID.ToString())
                    {
                        dataGridView1.Rows.Remove(row);
                        break;
                    }
                }
                textBox1.AppendText(args.Time.ToLongTimeString() + " 终端唯一标识为[" + args.End.UID + "]的用户下线\n");

            });
        }
        //用户上线 这里简单的使用datagridview.tag属性维护在线用户列表
        void manager_TCPClientConnected(TCPClientConnectedEventArgs args)
        {
            this.Invoke((Action)delegate ()
            {
                textBox1.AppendText(args.Time.ToLongTimeString() + " " + args.End.RemoteIP + ":" + args.End.RemotePort + "连入\n");
                dataGridView1.Rows.Add(args.End.UID, args.End.RemoteIP, args.End.RemotePort, args.Time.ToLongTimeString());
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = args.End;  //可以自定义数据结构来存储在线列表
            });
        }
        //收到消息
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
                    Image image = Bitmap.FromStream(new MemoryStream(args.Data));
                    pictureBox1.Image = image;
                }
            });
        }
        #endregion

        /// <summary>
        /// 发送文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            TCPServerManager manager = new TCPServerManager(_server_id);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                manager.Send(Msg.Zmsg1, Encoding.Unicode.GetBytes(textBox2.Text), row.Tag as TCPEndPoint);  //给在线用户同步发送文本
            }
        }
        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
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

                        TCPServerManager manager = new TCPServerManager(_server_id);
                        foreach (DataGridViewRow r in dataGridView1.Rows)
                        {
                            manager.SendAsync(Msg.Zmsg2, ms.ToArray(), r.Tag as TCPEndPoint, null);  //给在线用户异步发送图片
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 关闭服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            TCPServerManager manager = new TCPServerManager(_server_id);
            manager.Stop();  //结束服务器侦听

            foreach (DataGridViewRow r in dataGridView1.Rows)  //断开每个终端 数据接收终止
            {
                (r.Tag as TCPEndPoint).TryClose();
            }
            //注销事件

            Close();
        }
    }
}
