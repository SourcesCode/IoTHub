namespace CommunicationDebugTool
{
    partial class CommunicationDebugForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRemotePort = new System.Windows.Forms.NumericUpDown();
            this.gbCommunicationWay = new System.Windows.Forms.GroupBox();
            this.rdoUdp = new System.Windows.Forms.RadioButton();
            this.rdoTcpServer = new System.Windows.Forms.RadioButton();
            this.rdoTcpClient = new System.Windows.Forms.RadioButton();
            this.txtLocalPort = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkIsHexSend = new System.Windows.Forms.CheckBox();
            this.btnClearSendLog = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numInterval = new System.Windows.Forms.NumericUpDown();
            this.chkIsAutoSend = new System.Windows.Forms.CheckBox();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnClearReceiveLog = new System.Windows.Forms.Button();
            this.chkIsHexDisplay = new System.Windows.Forms.CheckBox();
            this.txtReceiveLog = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvClientList = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemotePort)).BeginInit();
            this.gbCommunicationWay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocalPort)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientList)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(758, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 411);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(758, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRemotePort);
            this.groupBox1.Controls.Add(this.gbCommunicationWay);
            this.groupBox1.Controls.Add(this.txtLocalPort);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.txtIpAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 230);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // txtRemotePort
            // 
            this.txtRemotePort.Location = new System.Drawing.Point(82, 142);
            this.txtRemotePort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.txtRemotePort.Name = "txtRemotePort";
            this.txtRemotePort.Size = new System.Drawing.Size(55, 21);
            this.txtRemotePort.TabIndex = 34;
            this.txtRemotePort.Value = new decimal(new int[] {
            6666,
            0,
            0,
            0});
            // 
            // gbCommunicationWay
            // 
            this.gbCommunicationWay.Controls.Add(this.rdoUdp);
            this.gbCommunicationWay.Controls.Add(this.rdoTcpServer);
            this.gbCommunicationWay.Controls.Add(this.rdoTcpClient);
            this.gbCommunicationWay.Location = new System.Drawing.Point(24, 20);
            this.gbCommunicationWay.Name = "gbCommunicationWay";
            this.gbCommunicationWay.Size = new System.Drawing.Size(109, 86);
            this.gbCommunicationWay.TabIndex = 3;
            this.gbCommunicationWay.TabStop = false;
            this.gbCommunicationWay.Text = "通讯模式";
            // 
            // rdoUdp
            // 
            this.rdoUdp.AutoSize = true;
            this.rdoUdp.Location = new System.Drawing.Point(16, 64);
            this.rdoUdp.Name = "rdoUdp";
            this.rdoUdp.Size = new System.Drawing.Size(41, 16);
            this.rdoUdp.TabIndex = 36;
            this.rdoUdp.TabStop = true;
            this.rdoUdp.Tag = "3";
            this.rdoUdp.Text = "Udp";
            this.rdoUdp.UseVisualStyleBackColor = true;
            this.rdoUdp.CheckedChanged += new System.EventHandler(this.rdoCommunicationWayGroup_CheckedChanged);
            // 
            // rdoTcpServer
            // 
            this.rdoTcpServer.AutoSize = true;
            this.rdoTcpServer.Checked = true;
            this.rdoTcpServer.Location = new System.Drawing.Point(16, 20);
            this.rdoTcpServer.Name = "rdoTcpServer";
            this.rdoTcpServer.Size = new System.Drawing.Size(83, 16);
            this.rdoTcpServer.TabIndex = 34;
            this.rdoTcpServer.TabStop = true;
            this.rdoTcpServer.Tag = "1";
            this.rdoTcpServer.Text = "Tcp Server";
            this.rdoTcpServer.UseVisualStyleBackColor = true;
            this.rdoTcpServer.CheckedChanged += new System.EventHandler(this.rdoCommunicationWayGroup_CheckedChanged);
            // 
            // rdoTcpClient
            // 
            this.rdoTcpClient.AutoSize = true;
            this.rdoTcpClient.Location = new System.Drawing.Point(16, 42);
            this.rdoTcpClient.Name = "rdoTcpClient";
            this.rdoTcpClient.Size = new System.Drawing.Size(83, 16);
            this.rdoTcpClient.TabIndex = 35;
            this.rdoTcpClient.TabStop = true;
            this.rdoTcpClient.Tag = "2";
            this.rdoTcpClient.Text = "Tcp Client";
            this.rdoTcpClient.UseVisualStyleBackColor = true;
            this.rdoTcpClient.CheckedChanged += new System.EventHandler(this.rdoCommunicationWayGroup_CheckedChanged);
            // 
            // txtLocalPort
            // 
            this.txtLocalPort.Location = new System.Drawing.Point(82, 169);
            this.txtLocalPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.txtLocalPort.Name = "txtLocalPort";
            this.txtLocalPort.Size = new System.Drawing.Size(55, 21);
            this.txtLocalPort.TabIndex = 33;
            this.txtLocalPort.Value = new decimal(new int[] {
            6666,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 32;
            this.label3.Text = "本地Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "远程Port:";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(82, 196);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(46, 23);
            this.btnStop.TabIndex = 29;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(29, 196);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(47, 23);
            this.btnStart.TabIndex = 28;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(62, 115);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(75, 21);
            this.txtIpAddress.TabIndex = 27;
            this.txtIpAddress.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "Ip:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkIsHexSend);
            this.groupBox3.Controls.Add(this.btnClearSendLog);
            this.groupBox3.Controls.Add(this.btnSend);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.numInterval);
            this.groupBox3.Controls.Add(this.chkIsAutoSend);
            this.groupBox3.Controls.Add(this.txtSendData);
            this.groupBox3.Location = new System.Drawing.Point(179, 207);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(514, 178);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发送";
            // 
            // chkIsHexSend
            // 
            this.chkIsHexSend.AutoSize = true;
            this.chkIsHexSend.Location = new System.Drawing.Point(7, 150);
            this.chkIsHexSend.Name = "chkIsHexSend";
            this.chkIsHexSend.Size = new System.Drawing.Size(66, 16);
            this.chkIsHexSend.TabIndex = 5;
            this.chkIsHexSend.Text = "Hex发送";
            this.chkIsHexSend.UseVisualStyleBackColor = true;
            // 
            // btnClearSendLog
            // 
            this.btnClearSendLog.Location = new System.Drawing.Point(431, 146);
            this.btnClearSendLog.Name = "btnClearSendLog";
            this.btnClearSendLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearSendLog.TabIndex = 3;
            this.btnClearSendLog.Text = "清空";
            this.btnClearSendLog.UseVisualStyleBackColor = true;
            this.btnClearSendLog.Click += new System.EventHandler(this.btnClearSendLog_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(350, 146);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(224, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "s/次";
            // 
            // numInterval
            // 
            this.numInterval.Location = new System.Drawing.Point(157, 149);
            this.numInterval.Name = "numInterval";
            this.numInterval.Size = new System.Drawing.Size(61, 21);
            this.numInterval.TabIndex = 3;
            this.numInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkIsAutoSend
            // 
            this.chkIsAutoSend.AutoSize = true;
            this.chkIsAutoSend.Location = new System.Drawing.Point(79, 150);
            this.chkIsAutoSend.Name = "chkIsAutoSend";
            this.chkIsAutoSend.Size = new System.Drawing.Size(72, 16);
            this.chkIsAutoSend.TabIndex = 2;
            this.chkIsAutoSend.Text = "定时发送";
            this.chkIsAutoSend.UseVisualStyleBackColor = true;
            this.chkIsAutoSend.CheckedChanged += new System.EventHandler(this.chkIsAutoSend_CheckedChanged);
            // 
            // txtSendData
            // 
            this.txtSendData.Location = new System.Drawing.Point(6, 20);
            this.txtSendData.Multiline = true;
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(500, 120);
            this.txtSendData.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnClearReceiveLog);
            this.groupBox4.Controls.Add(this.chkIsHexDisplay);
            this.groupBox4.Controls.Add(this.txtReceiveLog);
            this.groupBox4.Location = new System.Drawing.Point(179, 27);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(514, 174);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "日志";
            // 
            // btnClearReceiveLog
            // 
            this.btnClearReceiveLog.Location = new System.Drawing.Point(431, 146);
            this.btnClearReceiveLog.Name = "btnClearReceiveLog";
            this.btnClearReceiveLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearReceiveLog.TabIndex = 2;
            this.btnClearReceiveLog.Text = "清空";
            this.btnClearReceiveLog.UseVisualStyleBackColor = true;
            this.btnClearReceiveLog.Click += new System.EventHandler(this.btnClearReceiveLog_Click);
            // 
            // chkIsHexDisplay
            // 
            this.chkIsHexDisplay.AutoSize = true;
            this.chkIsHexDisplay.Location = new System.Drawing.Point(7, 150);
            this.chkIsHexDisplay.Name = "chkIsHexDisplay";
            this.chkIsHexDisplay.Size = new System.Drawing.Size(66, 16);
            this.chkIsHexDisplay.TabIndex = 1;
            this.chkIsHexDisplay.Text = "Hex显示";
            this.chkIsHexDisplay.UseVisualStyleBackColor = true;
            // 
            // txtReceiveLog
            // 
            this.txtReceiveLog.Location = new System.Drawing.Point(6, 20);
            this.txtReceiveLog.Multiline = true;
            this.txtReceiveLog.Name = "txtReceiveLog";
            this.txtReceiveLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReceiveLog.Size = new System.Drawing.Size(500, 120);
            this.txtReceiveLog.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvClientList);
            this.groupBox5.Location = new System.Drawing.Point(12, 263);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(161, 141);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "客户端列表";
            // 
            // dgvClientList
            // 
            this.dgvClientList.AllowUserToAddRows = false;
            this.dgvClientList.AllowUserToDeleteRows = false;
            this.dgvClientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.address});
            this.dgvClientList.Location = new System.Drawing.Point(6, 20);
            this.dgvClientList.Name = "dgvClientList";
            this.dgvClientList.ReadOnly = true;
            this.dgvClientList.RowTemplate.Height = 23;
            this.dgvClientList.Size = new System.Drawing.Size(149, 102);
            this.dgvClientList.TabIndex = 1;
            // 
            // Id
            // 
            this.Id.FillWeight = 20F;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 20;
            // 
            // address
            // 
            this.address.HeaderText = "Address";
            this.address.Name = "address";
            this.address.ReadOnly = true;
            // 
            // CommunicationDebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 433);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CommunicationDebugForm";
            this.Text = "CommunicationDebugForm";
            this.Load += new System.EventHandler(this.CommunicationDebugForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemotePort)).EndInit();
            this.gbCommunicationWay.ResumeLayout(false);
            this.gbCommunicationWay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocalPort)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbCommunicationWay;
        private System.Windows.Forms.RadioButton rdoUdp;
        private System.Windows.Forms.RadioButton rdoTcpServer;
        private System.Windows.Forms.RadioButton rdoTcpClient;
        private System.Windows.Forms.NumericUpDown txtLocalPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkIsHexSend;
        private System.Windows.Forms.Button btnClearSendLog;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.CheckBox chkIsAutoSend;
        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnClearReceiveLog;
        private System.Windows.Forms.CheckBox chkIsHexDisplay;
        private System.Windows.Forms.TextBox txtReceiveLog;
        private System.Windows.Forms.NumericUpDown txtRemotePort;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvClientList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
    }
}