namespace VPN
{
	// Token: 0x0200000A RID: 10
	public partial class frmMain : global::System.Windows.Forms.Form
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003168 File Offset: 0x00001368
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000031A0 File Offset: 0x000013A0
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblStsConnection = new System.Windows.Forms.Label();
            this.edtLogin = new System.Windows.Forms.TextBox();
            this.edtPassword = new System.Windows.Forms.TextBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.picCompany = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.cmbProtocol = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblProtocol = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lnkCreateAccount = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnConnect.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(302, 154);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(126, 51);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.connect_Click);
            // 
            // lblStsConnection
            // 
            this.lblStsConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStsConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStsConnection.Location = new System.Drawing.Point(277, 304);
            this.lblStsConnection.Name = "lblStsConnection";
            this.lblStsConnection.Size = new System.Drawing.Size(100, 13);
            this.lblStsConnection.TabIndex = 12;
            this.lblStsConnection.Text = "Connected";
            this.lblStsConnection.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // edtLogin
            // 
            this.edtLogin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtLogin.Location = new System.Drawing.Point(69, 344);
            this.edtLogin.Name = "edtLogin";
            this.edtLogin.Size = new System.Drawing.Size(159, 21);
            this.edtLogin.TabIndex = 13;
            this.edtLogin.Text = "svobi.";
            this.edtLogin.TextChanged += new System.EventHandler(this.edtLogin_TextChanged);
            // 
            // edtPassword
            // 
            this.edtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.edtPassword.Location = new System.Drawing.Point(69, 370);
            this.edtPassword.Name = "edtPassword";
            this.edtPassword.PasswordChar = '•';
            this.edtPassword.Size = new System.Drawing.Size(159, 20);
            this.edtPassword.TabIndex = 14;
            this.edtPassword.Text = "5274fb1954bcc";
            this.edtPassword.TextChanged += new System.EventHandler(this.edtPassword_TextChanged);
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.Location = new System.Drawing.Point(4, 347);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(63, 13);
            this.lblLogin.TabIndex = 15;
            this.lblLogin.Text = "Username";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(4, 373);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(61, 13);
            this.lblPassword.TabIndex = 16;
            this.lblPassword.Text = "Password";
            // 
            // picLoading
            // 
            this.picLoading.Location = new System.Drawing.Point(379, 304);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(20, 20);
            this.picLoading.TabIndex = 17;
            this.picLoading.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStatus.Location = new System.Drawing.Point(4, 229);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(443, 19);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "sts";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSettings
            // 
            this.btnSettings.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.Image = global::VPN.Properties.Resources.options;
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.Location = new System.Drawing.Point(147, 297);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(124, 30);
            this.btnSettings.TabIndex = 26;
            this.btnSettings.Text = "Account Settings";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // picCompany
            // 
            this.picCompany.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCompany.Image = global::VPN.Properties.Resources._112121;
            this.picCompany.Location = new System.Drawing.Point(37, 0);
            this.picCompany.Name = "picCompany";
            this.picCompany.Size = new System.Drawing.Size(374, 148);
            this.picCompany.TabIndex = 29;
            this.picCompany.TabStop = false;
            this.picCompany.Click += new System.EventHandler(this.picCompany_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cmbLocation
            // 
            this.cmbLocation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(64, 154);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(207, 21);
            this.cmbLocation.TabIndex = 32;
            // 
            // cmbProtocol
            // 
            this.cmbProtocol.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProtocol.FormattingEnabled = true;
            this.cmbProtocol.Location = new System.Drawing.Point(64, 184);
            this.cmbProtocol.Name = "cmbProtocol";
            this.cmbProtocol.Size = new System.Drawing.Size(207, 21);
            this.cmbProtocol.TabIndex = 33;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(9, 157);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(55, 13);
            this.lblLocation.TabIndex = 34;
            this.lblLocation.Text = "Location";
            // 
            // lblProtocol
            // 
            this.lblProtocol.AutoSize = true;
            this.lblProtocol.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProtocol.Location = new System.Drawing.Point(11, 187);
            this.lblProtocol.Name = "lblProtocol";
            this.lblProtocol.Size = new System.Drawing.Size(54, 13);
            this.lblProtocol.TabIndex = 35;
            this.lblProtocol.Text = "Protocol";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(260, 370);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 31);
            this.btnSave.TabIndex = 36;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lnkCreateAccount
            // 
            this.lnkCreateAccount.AutoSize = true;
            this.lnkCreateAccount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCreateAccount.LinkColor = System.Drawing.Color.Black;
            this.lnkCreateAccount.Location = new System.Drawing.Point(46, 393);
            this.lnkCreateAccount.Name = "lnkCreateAccount";
            this.lnkCreateAccount.Size = new System.Drawing.Size(182, 16);
            this.lnkCreateAccount.TabIndex = 30;
            this.lnkCreateAccount.TabStop = true;
            this.lnkCreateAccount.Text = "Generate Random Account";
            this.lnkCreateAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCreateAccount_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(234, 357);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 217);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(195, 61);
            this.pictureBox2.TabIndex = 38;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(234, 217);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(204, 64);
            this.pictureBox3.TabIndex = 39;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(450, 540);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblProtocol);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.cmbProtocol);
            this.Controls.Add(this.cmbLocation);
            this.Controls.Add(this.lnkCreateAccount);
            this.Controls.Add(this.picCompany);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.lblStsConnection);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.edtPassword);
            this.Controls.Add(this.edtLogin);
            this.Controls.Add(this.btnConnect);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unblock Closed Ports -Free Unlimited Vpn Accounts";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		// Token: 0x0400002D RID: 45
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400002E RID: 46
		private global::System.Windows.Forms.Button btnConnect;

		// Token: 0x0400002F RID: 47
		private global::System.Windows.Forms.Label lblStsConnection;

		// Token: 0x04000030 RID: 48
		private global::System.Windows.Forms.TextBox edtLogin;

		// Token: 0x04000031 RID: 49
		private global::System.Windows.Forms.TextBox edtPassword;

		// Token: 0x04000032 RID: 50
		private global::System.Windows.Forms.Label lblLogin;

		// Token: 0x04000033 RID: 51
		private global::System.Windows.Forms.Label lblPassword;

		// Token: 0x04000034 RID: 52
		private global::System.Windows.Forms.PictureBox picLoading;

		// Token: 0x04000035 RID: 53
		private global::System.Windows.Forms.Button btnSettings;

		// Token: 0x04000036 RID: 54
		private global::System.Windows.Forms.PictureBox picCompany;

		// Token: 0x04000037 RID: 55
		private global::System.Windows.Forms.Label lblStatus;

		// Token: 0x0400003A RID: 58
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x0400003B RID: 59
		private global::System.Windows.Forms.ComboBox cmbLocation;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.ComboBox cmbProtocol;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.Label lblLocation;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.Label lblProtocol;

		// Token: 0x0400003F RID: 63
		private global::System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.LinkLabel lnkCreateAccount;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}
