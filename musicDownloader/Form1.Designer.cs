namespace musicDownloader
{
    partial class Form1
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
            this.loginIn = new System.Windows.Forms.TextBox();
            this.passIn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.downloadedLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginIn
            // 
            this.loginIn.Location = new System.Drawing.Point(67, 11);
            this.loginIn.Name = "loginIn";
            this.loginIn.Size = new System.Drawing.Size(185, 20);
            this.loginIn.TabIndex = 0;
            // 
            // passIn
            // 
            this.passIn.Location = new System.Drawing.Point(67, 37);
            this.passIn.Name = "passIn";
            this.passIn.PasswordChar = '*';
            this.passIn.Size = new System.Drawing.Size(184, 20);
            this.passIn.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 47);
            this.button1.TabIndex = 4;
            this.button1.Text = "LogIn";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 335);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(458, 264);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = ">>>Please provide your username and password.\r\n>>>Hi, %username%!";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(477, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 541);
            this.panel1.TabIndex = 8;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(86, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(113, 23);
            this.button5.TabIndex = 2;
            this.button5.Text = "Add to downloads";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox1.ForeColor = System.Drawing.Color.Black;
            this.checkBox1.Location = new System.Drawing.Point(205, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 24);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Select all";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Load music";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(675, 566);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(516, 33);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Location = new System.Drawing.Point(329, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(142, 47);
            this.panel2.TabIndex = 10;
            this.panel2.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(64, 21);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Friends";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 21);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(51, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Music";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Location = new System.Drawing.Point(13, 66);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(455, 263);
            this.panel3.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(529, 566);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Downloads stack";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(478, 586);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Total:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(560, 586);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Downloaded:";
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.Location = new System.Drawing.Point(509, 586);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(19, 13);
            this.totalLabel.TabIndex = 15;
            this.totalLabel.Text = "***";
            // 
            // downloadedLabel
            // 
            this.downloadedLabel.AutoSize = true;
            this.downloadedLabel.Location = new System.Drawing.Point(626, 586);
            this.downloadedLabel.Name = "downloadedLabel";
            this.downloadedLabel.Size = new System.Drawing.Size(19, 13);
            this.downloadedLabel.TabIndex = 16;
            this.downloadedLabel.Text = "***";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 611);
            this.Controls.Add(this.downloadedLabel);
            this.Controls.Add(this.totalLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passIn);
            this.Controls.Add(this.loginIn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "musicDownloader";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loginIn;
        private System.Windows.Forms.TextBox passIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Label downloadedLabel;
        private System.Windows.Forms.Button button5;
    }
}

