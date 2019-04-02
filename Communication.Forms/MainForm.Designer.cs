namespace Communication.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiCreateTcpServer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCreateTcpClient = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCreateUdp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCreateTcpServer,
            this.tsmiCreateTcpClient,
            this.tsmiCreateUdp});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(607, 25);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // tsmiCreateTcpServer
            // 
            this.tsmiCreateTcpServer.Name = "tsmiCreateTcpServer";
            this.tsmiCreateTcpServer.Size = new System.Drawing.Size(106, 21);
            this.tsmiCreateTcpServer.Text = "创建Tcp Server";
            this.tsmiCreateTcpServer.Click += new System.EventHandler(this.tsmiCreateTcpServer_Click);
            // 
            // tsmiCreateTcpClient
            // 
            this.tsmiCreateTcpClient.Name = "tsmiCreateTcpClient";
            this.tsmiCreateTcpClient.Size = new System.Drawing.Size(101, 21);
            this.tsmiCreateTcpClient.Text = "创建Tcp Client";
            this.tsmiCreateTcpClient.Click += new System.EventHandler(this.tsmiCreateTcpClient_Click);
            // 
            // tsmiCreateUdp
            // 
            this.tsmiCreateUdp.Name = "tsmiCreateUdp";
            this.tsmiCreateUdp.Size = new System.Drawing.Size(69, 21);
            this.tsmiCreateUdp.Text = "创建Udp";
            this.tsmiCreateUdp.Click += new System.EventHandler(this.tsmiCreateUdp_Click);
            // 
            // MainForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 397);
            this.Controls.Add(this.menuStrip2);
            this.IsMdiContainer = true;
            this.Name = "MainForm2";
            this.Text = "MainForm2";
            this.Load += new System.EventHandler(this.MainForm2_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreateTcpServer;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreateTcpClient;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreateUdp;
    }
}