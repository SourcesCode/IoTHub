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
    public partial class CreateTCPClientForm : Form
    {
        public CreateTCPClientForm()
        {
            InitializeComponent();
        }

        private void CreateTCPClientForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 创建客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (TCPClientManager.ClientExist(textBox3.Text))
            {
                MessageBox.Show("客户端已存在！");
                return;
            }

            TCPClientManager manager = new TCPClientManager(textBox3.Text);  //创建客户端
            manager.Connect(textBox1.Text, int.Parse(textBox2.Text));

            TCPClientForm frmtcpclient = new TCPClientForm(textBox3.Text);
            frmtcpclient.Show();
        }

    }
}
