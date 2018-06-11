namespace WinTestJD
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_InRecognition = new System.Windows.Forms.Button();
            this.btn_InCross = new System.Windows.Forms.Button();
            this.btn_OutRecognition = new System.Windows.Forms.Button();
            this.btn_OutCross = new System.Windows.Forms.Button();
            this.btn_PayCheck = new System.Windows.Forms.Button();
            this.btn_ThirdCharging = new System.Windows.Forms.Button();
            this.txt_plateNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richText_Msg = new System.Windows.Forms.RichTextBox();
            this.txt_LogNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_WhiteList = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_EventType = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(199, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "更新车场车位总数";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 132);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 32);
            this.button2.TabIndex = 0;
            this.button2.Text = "更新车场剩余车位数";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 32);
            this.button3.TabIndex = 0;
            this.button3.Text = "启动服务心跳检测";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 185);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(199, 32);
            this.button4.TabIndex = 0;
            this.button4.Text = "同步设备状态";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(791, 499);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(689, 411);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基础业务";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.txt_LogNo);
            this.tabPage2.Controls.Add(this.richText_Msg);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txt_EventType);
            this.tabPage2.Controls.Add(this.txt_plateNumber);
            this.tabPage2.Controls.Add(this.btn_ThirdCharging);
            this.tabPage2.Controls.Add(this.btn_PayCheck);
            this.tabPage2.Controls.Add(this.btn_OutCross);
            this.tabPage2.Controls.Add(this.btn_OutRecognition);
            this.tabPage2.Controls.Add(this.btn_InCross);
            this.tabPage2.Controls.Add(this.btn_WhiteList);
            this.tabPage2.Controls.Add(this.btn_InRecognition);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(783, 473);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "车场业务";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_InRecognition
            // 
            this.btn_InRecognition.Location = new System.Drawing.Point(124, 6);
            this.btn_InRecognition.Name = "btn_InRecognition";
            this.btn_InRecognition.Size = new System.Drawing.Size(72, 27);
            this.btn_InRecognition.TabIndex = 0;
            this.btn_InRecognition.Text = "到达入口";
            this.btn_InRecognition.UseVisualStyleBackColor = true;
            this.btn_InRecognition.Click += new System.EventHandler(this.btn_InRecognition_Click);
            // 
            // btn_InCross
            // 
            this.btn_InCross.Location = new System.Drawing.Point(212, 6);
            this.btn_InCross.Name = "btn_InCross";
            this.btn_InCross.Size = new System.Drawing.Size(94, 30);
            this.btn_InCross.TabIndex = 0;
            this.btn_InCross.Text = "进入停车场";
            this.btn_InCross.UseVisualStyleBackColor = true;
            this.btn_InCross.Click += new System.EventHandler(this.btn_InCross_Click);
            // 
            // btn_OutRecognition
            // 
            this.btn_OutRecognition.Location = new System.Drawing.Point(321, 8);
            this.btn_OutRecognition.Name = "btn_OutRecognition";
            this.btn_OutRecognition.Size = new System.Drawing.Size(92, 28);
            this.btn_OutRecognition.TabIndex = 0;
            this.btn_OutRecognition.Text = "到达出口";
            this.btn_OutRecognition.UseVisualStyleBackColor = true;
            this.btn_OutRecognition.Click += new System.EventHandler(this.btn_OutRecognition_Click);
            // 
            // btn_OutCross
            // 
            this.btn_OutCross.Location = new System.Drawing.Point(436, 8);
            this.btn_OutCross.Name = "btn_OutCross";
            this.btn_OutCross.Size = new System.Drawing.Size(87, 26);
            this.btn_OutCross.TabIndex = 0;
            this.btn_OutCross.Text = "离开停车场";
            this.btn_OutCross.UseVisualStyleBackColor = true;
            this.btn_OutCross.Click += new System.EventHandler(this.btn_OutCross_Click);
            // 
            // btn_PayCheck
            // 
            this.btn_PayCheck.Location = new System.Drawing.Point(548, 9);
            this.btn_PayCheck.Name = "btn_PayCheck";
            this.btn_PayCheck.Size = new System.Drawing.Size(87, 27);
            this.btn_PayCheck.TabIndex = 0;
            this.btn_PayCheck.Text = "查询支付结果";
            this.btn_PayCheck.UseVisualStyleBackColor = true;
            this.btn_PayCheck.Click += new System.EventHandler(this.btn_PayCheck_Click);
            // 
            // btn_ThirdCharging
            // 
            this.btn_ThirdCharging.Location = new System.Drawing.Point(663, 9);
            this.btn_ThirdCharging.Name = "btn_ThirdCharging";
            this.btn_ThirdCharging.Size = new System.Drawing.Size(99, 28);
            this.btn_ThirdCharging.TabIndex = 0;
            this.btn_ThirdCharging.Text = "请求第三方计费";
            this.btn_ThirdCharging.UseVisualStyleBackColor = true;
            this.btn_ThirdCharging.Click += new System.EventHandler(this.btn_ThirdCharging_Click);
            // 
            // txt_plateNumber
            // 
            this.txt_plateNumber.Location = new System.Drawing.Point(103, 45);
            this.txt_plateNumber.Name = "txt_plateNumber";
            this.txt_plateNumber.Size = new System.Drawing.Size(112, 21);
            this.txt_plateNumber.TabIndex = 1;
            this.txt_plateNumber.Text = "粤B512NA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "车牌号";
            // 
            // richText_Msg
            // 
            this.richText_Msg.Location = new System.Drawing.Point(6, 82);
            this.richText_Msg.Name = "richText_Msg";
            this.richText_Msg.Size = new System.Drawing.Size(770, 383);
            this.richText_Msg.TabIndex = 3;
            this.richText_Msg.Text = "";
            // 
            // txt_LogNo
            // 
            this.txt_LogNo.Location = new System.Drawing.Point(569, 45);
            this.txt_LogNo.Name = "txt_LogNo";
            this.txt_LogNo.Size = new System.Drawing.Size(183, 21);
            this.txt_LogNo.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(499, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "LogNo";
            // 
            // btn_WhiteList
            // 
            this.btn_WhiteList.Location = new System.Drawing.Point(15, 8);
            this.btn_WhiteList.Name = "btn_WhiteList";
            this.btn_WhiteList.Size = new System.Drawing.Size(89, 27);
            this.btn_WhiteList.TabIndex = 0;
            this.btn_WhiteList.Text = "白名单检测";
            this.btn_WhiteList.UseVisualStyleBackColor = true;
            this.btn_WhiteList.Click += new System.EventHandler(this.btn_WhiteList_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "事件类型";
            // 
            // txt_EventType
            // 
            this.txt_EventType.Location = new System.Drawing.Point(301, 45);
            this.txt_EventType.Name = "txt_EventType";
            this.txt_EventType.Size = new System.Drawing.Size(112, 21);
            this.txt_EventType.TabIndex = 1;
            this.txt_EventType.Text = "BRUSHCARD";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 505);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_InRecognition;
        private System.Windows.Forms.Button btn_OutCross;
        private System.Windows.Forms.Button btn_OutRecognition;
        private System.Windows.Forms.Button btn_InCross;
        private System.Windows.Forms.Button btn_ThirdCharging;
        private System.Windows.Forms.Button btn_PayCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_plateNumber;
        private System.Windows.Forms.RichTextBox richText_Msg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_LogNo;
        private System.Windows.Forms.Button btn_WhiteList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_EventType;
    }
}

