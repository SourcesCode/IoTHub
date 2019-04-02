namespace Communication.Forms
{
    partial class DataReceiveAndSendUC
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClearReceiveLog = new System.Windows.Forms.Button();
            this.chkIsHexDisplay = new System.Windows.Forms.CheckBox();
            this.txtReceiveLog = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkIsHexSend = new System.Windows.Forms.CheckBox();
            this.btnClearSendLog = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numInterval = new System.Windows.Forms.NumericUpDown();
            this.chkIsAutoSend = new System.Windows.Forms.CheckBox();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClearReceiveLog);
            this.groupBox1.Controls.Add(this.chkIsHexDisplay);
            this.groupBox1.Controls.Add(this.txtReceiveLog);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 174);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日志";
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
            this.txtReceiveLog.TextChanged += new System.EventHandler(this.txtReceiveLog_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkIsHexSend);
            this.groupBox2.Controls.Add(this.btnClearSendLog);
            this.groupBox2.Controls.Add(this.btnSend);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numInterval);
            this.groupBox2.Controls.Add(this.chkIsAutoSend);
            this.groupBox2.Controls.Add(this.txtSendData);
            this.groupBox2.Location = new System.Drawing.Point(3, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(514, 178);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "s/次";
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
            this.txtSendData.TextChanged += new System.EventHandler(this.txtSendData_TextChanged);
            // 
            // DataReceiveAndSendUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DataReceiveAndSendUC";
            this.Size = new System.Drawing.Size(521, 369);
            this.Load += new System.EventHandler(this.DataReceiveAndSendUC_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkIsHexDisplay;
        private System.Windows.Forms.TextBox txtReceiveLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.CheckBox chkIsAutoSend;
        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.Button btnClearReceiveLog;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClearSendLog;
        private System.Windows.Forms.CheckBox chkIsHexSend;
    }
}
