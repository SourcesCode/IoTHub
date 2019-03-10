using Communication.Udp;
using System;
using System.Windows.Forms;

namespace UDPClient
{
    public partial class frmCreateUDPClient : Form
    {
        public frmCreateUDPClient()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 创建客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (UDPClientManager.ClientExist(textBox2.Text))
            {
                MessageBox.Show("客户端已存在！");
                return;
            }
            UDPClientManager manager = new UDPClientManager(textBox2.Text);
            manager.Start(int.Parse(textBox1.Text));  //开启端口监听

            frmUDPClient frmudpclient = new frmUDPClient(textBox2.Text, int.Parse(textBox1.Text));
            frmudpclient.Show();
        }
    }
}
