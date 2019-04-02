using System;
using System.Windows.Forms;

namespace Communication.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void tsmiCreateTcpServer_Click(object sender, EventArgs e)
        {
            ShowForm<TcpServerForm>("TcpServerForm");
        }

        private void tsmiCreateTcpClient_Click(object sender, EventArgs e)
        {
            ShowForm<TcpClientForm>("TcpClientForm");
        }

        private void tsmiCreateUdp_Click(object sender, EventArgs e)
        {
            ShowForm<UdpClientForm>("UdpForm");
        }

        private void MainForm2_Load(object sender, EventArgs e)
        {

        }

        private void ShowForm<TForm>(string id) where TForm : Form, new()
        {
            Form childForm = new TForm();
            childForm.MdiParent = this;
            childForm.Tag = id;
            childForm.Name = id + " - " + DateTime.Now.Millisecond.ToString();
            childForm.Text = childForm.Name;

            //  childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }


    }
}
