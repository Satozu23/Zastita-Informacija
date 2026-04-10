namespace ZastitaProjekat
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            groupBox3 = new GroupBox();
            txtOutputPath = new TextBox();
            btnSelectOutput = new Button();
            cmbAlgorithm = new ComboBox();
            rtbLog = new RichTextBox();
            chkEnableFSW = new CheckBox();
            txtFolderPath = new TextBox();
            btnSelectFolder = new Button();
            tabPage2 = new TabPage();
            groupBox2 = new GroupBox();
            btnSelectFileToSend = new Button();
            txtTargetIP = new TextBox();
            label3 = new Label();
            groupBox1 = new GroupBox();
            lblServerStatus = new Label();
            btnStartServer = new Button();
            txtPort = new TextBox();
            label2 = new Label();
            txtMyIP = new TextBox();
            label1 = new Label();
            label4 = new Label();
            txtKey = new TextBox();
            label5 = new Label();
            txtIV = new TextBox();
            label6 = new Label();
            numRailfence = new NumericUpDown();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox3.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRailfence).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(107, 76);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(882, 420);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(txtOutputPath);
            tabPage1.Controls.Add(btnSelectOutput);
            tabPage1.Controls.Add(cmbAlgorithm);
            tabPage1.Controls.Add(rtbLog);
            tabPage1.Controls.Add(chkEnableFSW);
            tabPage1.Controls.Add(txtFolderPath);
            tabPage1.Controls.Add(btnSelectFolder);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(874, 392);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "FSW i Enkripcija";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(numRailfence);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(txtIV);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(txtKey);
            groupBox3.Controls.Add(label4);
            groupBox3.Location = new Point(211, 28);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(200, 261);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "Podešavanja Ključeva";
            // 
            // txtOutputPath
            // 
            txtOutputPath.Location = new Point(510, 105);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.ReadOnly = true;
            txtOutputPath.Size = new Size(334, 23);
            txtOutputPath.TabIndex = 6;
            // 
            // btnSelectOutput
            // 
            btnSelectOutput.Location = new Point(39, 105);
            btnSelectOutput.Name = "btnSelectOutput";
            btnSelectOutput.Size = new Size(121, 46);
            btnSelectOutput.TabIndex = 5;
            btnSelectOutput.Text = "Izaberi folder za čuvanje (Output)";
            btnSelectOutput.UseVisualStyleBackColor = true;
            btnSelectOutput.Click += btnSelectOutput_Click;
            // 
            // cmbAlgorithm
            // 
            cmbAlgorithm.FormattingEnabled = true;
            cmbAlgorithm.Items.AddRange(new object[] { "Railfence", "XXTEA (CBC)" });
            cmbAlgorithm.Location = new Point(39, 189);
            cmbAlgorithm.Name = "cmbAlgorithm";
            cmbAlgorithm.Size = new Size(121, 23);
            cmbAlgorithm.TabIndex = 4;
            // 
            // rtbLog
            // 
            rtbLog.Location = new Point(510, 189);
            rtbLog.Name = "rtbLog";
            rtbLog.Size = new Size(334, 159);
            rtbLog.TabIndex = 3;
            rtbLog.Text = "";
            // 
            // chkEnableFSW
            // 
            chkEnableFSW.AutoSize = true;
            chkEnableFSW.Location = new Point(58, 284);
            chkEnableFSW.Name = "chkEnableFSW";
            chkEnableFSW.Size = new Size(88, 19);
            chkEnableFSW.TabIndex = 2;
            chkEnableFSW.Text = "Uključi FSW";
            chkEnableFSW.UseVisualStyleBackColor = true;
            chkEnableFSW.Click += chkEnableFSW_CheckedChanged;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(510, 28);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(334, 23);
            txtFolderPath.TabIndex = 1;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(39, 28);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(121, 46);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "Izaberi folder za praćenje";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(874, 392);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "TCP Razmena";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnSelectFileToSend);
            groupBox2.Controls.Add(txtTargetIP);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(380, 30);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(320, 199);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Klijent (Slanje)";
            // 
            // btnSelectFileToSend
            // 
            btnSelectFileToSend.Location = new Point(97, 101);
            btnSelectFileToSend.Name = "btnSelectFileToSend";
            btnSelectFileToSend.Size = new Size(121, 42);
            btnSelectFileToSend.TabIndex = 2;
            btnSelectFileToSend.Text = "Izaberi .enc fajl i pošalji";
            btnSelectFileToSend.UseVisualStyleBackColor = true;
            btnSelectFileToSend.Click += btnSelectFileToSend_Click;
            // 
            // txtTargetIP
            // 
            txtTargetIP.Location = new Point(136, 27);
            txtTargetIP.Name = "txtTargetIP";
            txtTargetIP.Size = new Size(155, 23);
            txtTargetIP.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 30);
            label3.Name = "label3";
            label3.Size = new Size(97, 15);
            label3.TabIndex = 0;
            label3.Text = "IP Adresa klijenta";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblServerStatus);
            groupBox1.Controls.Add(btnStartServer);
            groupBox1.Controls.Add(txtPort);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtMyIP);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(26, 30);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(320, 199);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Server (Prijem)";
            // 
            // lblServerStatus
            // 
            lblServerStatus.AutoSize = true;
            lblServerStatus.ForeColor = Color.Red;
            lblServerStatus.Location = new Point(15, 152);
            lblServerStatus.Name = "lblServerStatus";
            lblServerStatus.Size = new Size(91, 15);
            lblServerStatus.TabIndex = 5;
            lblServerStatus.Text = "Status: Isključen";
            // 
            // btnStartServer
            // 
            btnStartServer.Location = new Point(15, 108);
            btnStartServer.Name = "btnStartServer";
            btnStartServer.Size = new Size(97, 28);
            btnStartServer.TabIndex = 4;
            btnStartServer.Text = "Pokreni Server";
            btnStartServer.UseVisualStyleBackColor = true;
            btnStartServer.Click += btnStartServer_Click;
            // 
            // txtPort
            // 
            txtPort.Location = new Point(123, 65);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(155, 23);
            txtPort.TabIndex = 3;
            txtPort.Text = "5000";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 68);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 2;
            label2.Text = "Port";
            // 
            // txtMyIP
            // 
            txtMyIP.Location = new Point(123, 27);
            txtMyIP.Name = "txtMyIP";
            txtMyIP.ReadOnly = true;
            txtMyIP.Size = new Size(155, 23);
            txtMyIP.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 30);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 0;
            label1.Text = "Moja IP adresa";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(34, 31);
            label4.Name = "label4";
            label4.Size = new Size(71, 15);
            label4.TabIndex = 0;
            label4.Text = "XXTEA Ključ";
            // 
            // txtKey
            // 
            txtKey.Location = new Point(34, 65);
            txtKey.Name = "txtKey";
            txtKey.Size = new Size(103, 23);
            txtKey.TabIndex = 1;
            txtKey.Text = "1234567890123456";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(34, 108);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 2;
            label5.Text = "XXTEA IV";
            // 
            // txtIV
            // 
            txtIV.Location = new Point(34, 138);
            txtIV.Name = "txtIV";
            txtIV.Size = new Size(100, 23);
            txtIV.TabIndex = 3;
            txtIV.Text = "inicijalni_vektr";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(34, 186);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 4;
            label6.Text = "Railfence";
            // 
            // numRailfence
            // 
            numRailfence.Location = new Point(34, 217);
            numRailfence.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numRailfence.Name = "numRailfence";
            numRailfence.Size = new Size(68, 23);
            numRailfence.TabIndex = 5;
            numRailfence.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1120, 560);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tabPage2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRailfence).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button btnSelectFolder;
        private TabPage tabPage2;
        private RichTextBox rtbLog;
        private CheckBox chkEnableFSW;
        private TextBox txtFolderPath;
        private ComboBox cmbAlgorithm;
        private Button btnSelectOutput;
        private TextBox txtOutputPath;
        private GroupBox groupBox1;
        private Button btnStartServer;
        private TextBox txtPort;
        private Label label2;
        private TextBox txtMyIP;
        private Label label1;
        private GroupBox groupBox2;
        private Button btnSelectFileToSend;
        private TextBox txtTargetIP;
        private Label label3;
        private Label lblServerStatus;
        private GroupBox groupBox3;
        private TextBox txtIV;
        private Label label5;
        private TextBox txtKey;
        private Label label4;
        private NumericUpDown numRailfence;
        private Label label6;
    }
}
