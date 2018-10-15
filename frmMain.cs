using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Threading;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using DotRas;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using VPN.Properties;
using Keys = System.Windows.Forms.Keys;

namespace VPN
{

	// Token: 0x0200000A RID: 10
	public partial class frmMain : Form
	{

		// Token: 0x06000043 RID: 67 RVA: 0x00003D9A File Offset: 0x00001F9A
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

        // Token: 0x06000044 RID: 68 RVA: 0x00003DA8 File Offset: 0x00001FA8
        public frmMain()
        {
            this.InitializeComponent();
            this.rm = new ResourceManager(typeof(Resources));
            this.lstServerRouting = new ServerRouting();
            this.vpn = new ConnectorVPN();
            Settings st = this.LoadSettings("settings.xml");
            this.LoadServers();
            this.SetDefaultStrings();
            this.FillProtocol();
            this.GetLastConfiguration(st);
            try 
            {
                this.SetDialer();
            }
            catch (Exception ex)
            {
            }
        }

        // Token: 0x06000045 RID: 69 RVA: 0x00003E80 File Offset: 0x00002080
        private void frmMain_Load(object sender, EventArgs e)
        {

            this.AutoUpdate();
        }

        // Token: 0x06000046 RID: 70 RVA: 0x00003E8A File Offset: 0x0000208A
        private void AutoUpdate()
        {
            AutoUpdater.LetUserSelectRemindLater = false;
            AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Minutes;
            AutoUpdater.RemindLaterAt = 2;
            AutoUpdater.Start("http://unblockvpn.com/data/autoupdate.xml");
        }

        // Token: 0x06000047 RID: 71 RVA: 0x00003EAC File Offset: 0x000020AC
        public void SetDialer()
        {
            Helper helper = new Helper();
            this.pbPath = string.Format("{0}\\{1}\\{2}", helper.GetSharedDocsFolder(), "UnblockVPN", "CustomPhoneBook.pbk");
            this.pb = new RasPhoneBook();
            this.pb.Open(this.pbPath);
            if (RasConnection.GetActiveConnections().Count > 0)
            {
                this.SetDefaultState(true);
            }
            else
            {
                this.SetDefaultState(false);
            }
            this.dialer = new RasDialer(this.components);
            this.dialer.Credentials = null;
            this.dialer.EapOptions = new RasEapOptions(false, false, false);
            this.dialer.HangUpPollingInterval = 0;
            this.dialer.Options = new RasDialOptions(false, false, false, false, false, false, false, false, false, false);
            this.dialer.SynchronizingObject = this;
            this.dialer.DialCompleted += this.Dialer_DialCompleted;
            this.watcher = new RasConnectionWatcher();
            this.watcher.SynchronizingObject = this;
            this.watcher.Disconnected += this.watcher_Disconnected;
            this.watcher.Connected += this.watcher_Connected;
            this.watcher.Handle = this.handle;
            this.watcher.EnableRaisingEvents = true;
        }

        // Token: 0x06000048 RID: 72 RVA: 0x00004010 File Offset: 0x00002210
        private void SetDefaultStrings()
        {
            this.lblStsConnection.Text = this.rm.GetString("Disconnected");
            this.btnConnect.Text = this.rm.GetString("btnOn");
            this.lblStatus.Text = "";
            this.ShowSettings(false);
            this.lblLogin.Text = this.rm.GetString("username");
            this.lblPassword.Text = this.rm.GetString("password");
            this.lnkCreateAccount.Text = this.rm.GetString("createAccount");
            this.Text = "UnblockVPN";
        }

        // Token: 0x06000049 RID: 73 RVA: 0x000040EC File Offset: 0x000022EC
        private void ShowSettings(bool bShow)
        {
            Graphics graphics = base.CreateGraphics();
            if (bShow)
            {
                base.Height = (int)((double)(480f * graphics.DpiY) / 100.0);
            }
            else
            {
                base.Height = (int)((double)(400f * graphics.DpiY) / 100.0);
            }
            this.lblLogin.Visible = bShow;
            this.lblPassword.Visible = bShow;
            this.edtLogin.Visible = bShow;
            this.edtPassword.Visible = bShow;
            this.bLoginVisible = bShow;
        }

        // Token: 0x0600004A RID: 74 RVA: 0x00004188 File Offset: 0x00002388
        private void LoadServers()
        {
            ServersXML serversXML = new ServersXML();
            try
            {
                string xmladdressByName = this.GetXMLaddressByName("https://1cpanel.com/members/xmlservers.php", this.edtLogin.Text);
                this.serverList = serversXML.LoadServers(xmladdressByName);
            }
            catch (Exception ex)
            {
                try
                {
                    this.serverList = serversXML.LoadServers("https://albert.vimjak.cz/data/unblockvpn-xmlservers.xml");
                }
                catch
                {
                    try
                    {
                        this.serverList = serversXML.LoadServers("http://en.vimjak.cz/data/unblockvpn-xmlservers.xml");
                    }
                    catch
                    {
                        this.serverList = serversXML.GeDefaulttServers();
                    }
                }
            }
            this.serverList = serversXML.GetServersByPriority(this.serverList);
            this.cmbLocation.Items.Clear();
            foreach (Servers servers in this.serverList)
            {
                this.cmbLocation.Items.Add(string.Format("{0} - {1}", servers.shortName, servers.location));
            }
            if (this.cmbLocation.Items.Count > 0)
            {
                this.cmbLocation.SelectedIndex = 0;
            }
            if (this.cmbProtocol.Items.Count > 0)
            {
                this.cmbProtocol.SelectedIndex = 0;
            }
        }

        // Token: 0x0600004B RID: 75 RVA: 0x0000431C File Offset: 0x0000251C
        private string GetXMLaddressByName(string sPath, string sUserName)
        {
            if (sUserName.Length > 0)
            {
                sPath += string.Format("?username={0}", sUserName);
            }
            return sPath;
        }

        // Token: 0x0600004C RID: 76 RVA: 0x00004354 File Offset: 0x00002554
        private void FillProtocol()
        {
            this.cmbProtocol.Items.Clear();
            List<string> availableProtocols = this.vpn.GetAvailableProtocols();
            if (availableProtocols.Count > 1)
            {
                this.cmbProtocol.Items.Add("AUTO");
                foreach (string item in availableProtocols)
                {
                    this.cmbProtocol.Items.Add(item);
                }
            }
            else if (availableProtocols.Count == 1)
            {
                this.cmbProtocol.Items.Add(availableProtocols[0]);
            }
            if (this.cmbProtocol.Items.Count > 0)
            {
                this.cmbProtocol.SelectedIndex = 0;
            }
        }

        // Token: 0x0600004D RID: 77 RVA: 0x00004450 File Offset: 0x00002650
        private void GetLastConfiguration(Settings st)
        {
            for (int i = 0; i < this.cmbProtocol.Items.Count; i++)
            {
                if (this.cmbProtocol.Items[i].ToString().ToLower() == st.protocol.ToLower())
                {
                    this.cmbProtocol.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < this.cmbLocation.Items.Count; i++)
            {
                if (this.cmbLocation.Items[i].ToString().ToLower() == st.serverName.ToLower())
                {
                    this.cmbLocation.SelectedIndex = i;
                    break;
                }
            }
        }

        // Token: 0x0600004E RID: 78 RVA: 0x00004524 File Offset: 0x00002724
        private void ShowStatus(string sStatus, bool bError)
        {
            this.lblStatus.Text = sStatus;
            if (bError)
            {
                this.lblStatus.ForeColor = Color.Red;
            }
            else
            {
                this.lblStatus.ForeColor = Color.Green;
            }
        }

        // Token: 0x0600004F RID: 79 RVA: 0x00004570 File Offset: 0x00002770
        private void connect_Click(object sender, EventArgs e)
        {
            this.lblStatus.Text = "";
            if (this.isConnected)
            {
                this.DisconnectFromVPN();
            }
            else
            {
                if (this.cmbProtocol.SelectedItem.ToString() == "AUTO")
                {
                    this.GetSelectedServerAuto();
                }
                this.ConnectToVPN(this.edtLogin.Text, this.edtPassword.Text);
            }
        }

        // Token: 0x06000050 RID: 80 RVA: 0x000045F0 File Offset: 0x000027F0
        private void DisconnectFromVPN()
        {
            this.lblStsConnection.Text = this.rm.GetString("Disconnecting");
            this.picLoading.Image = Resources.loading;
            Application.DoEvents();
            this.vpn.DisconnectFromVPN(this.dialer, this.pb, "UVPN");
        }

        // Token: 0x06000051 RID: 81 RVA: 0x00004650 File Offset: 0x00002850
        private void ConnectToVPN(string sLogin, string sPass)
        {
            Servers selectedServer = this.GetSelectedServer();
            if (sLogin.Length > 0 && sPass.Length > 0 && this.dialer != null && this.pb != null && selectedServer != null)
            {
                this.isConnected = true;
                this.picLoading.Image = Resources.loading;
                this.lblStsConnection.Text = this.rm.GetString("Connecting");
                this.btnConnect.Text = this.rm.GetString("btnOff");
                this.lblStatus.Text = "";
                this.btnSettings.Enabled = false;
                this.cmbLocation.Enabled = false;
                this.cmbProtocol.Enabled = false;
                sLogin = this.GetUserName(sLogin);
                this.ShowSettings(false);
                string text = this.cmbProtocol.SelectedItem.ToString();
                string text2 = text;
                if (text2 != null)
                {
                    if (!(text2 == "AUTO"))
                    {
                        if (!(text2 == "PPTP"))
                        {
                            if (!(text2 == "L2TP"))
                            {
                                if (text2 == "SSTP")
                                {
                                    this.bAutoMode = false;
                                    this.handle = this.vpn.ConnectVPN_SSTP(this.dialer, this.pb, selectedServer, "UVPN", sLogin, sPass, this.pbPath);
                                }
                            }
                            else
                            {
                                this.bAutoMode = false;
                                this.handle = this.vpn.ConnectVPN_L2TP(this.dialer, this.pb, selectedServer, "UVPN", sLogin, sPass, this.pbPath, "xunblock4me");
                            }
                        }
                        else
                        {
                            this.bAutoMode = false;
                            this.handle = this.vpn.ConnectVPN_PPTP(this.dialer, this.pb, selectedServer, "UVPN", sLogin, sPass, this.pbPath);
                        }
                    }
                    else
                    {
                        this.bAutoMode = true;
                        this.handle = this.vpn.ConnectVPN(this.dialer, this.pb, this.lstServerRouting, "UVPN", sLogin, sPass, this.pbPath, "xunblock4me");
                    }
                }
                if (this.handle == null)
                {
                    this.SetDefaultState(false);
                }
            }
            else
            {
                this.ShowStatus(this.rm.GetString("wrongLogin"), true);
                this.ShowSettings(true);
            }
        }

        // Token: 0x06000052 RID: 82 RVA: 0x000048B0 File Offset: 0x00002AB0
        private Servers GetSelectedServer()
        {
            Servers result = null;
            foreach (Servers servers in this.serverList)
            {
                string value = string.Format("{0} - {1}", servers.shortName, servers.location);
                if (this.cmbLocation.SelectedItem.ToString().Contains(value))
                {
                    result = servers;
                    break;
                }
            }
            return result;
        }

        // Token: 0x06000053 RID: 83 RVA: 0x0000494C File Offset: 0x00002B4C
        private void GetSelectedServerAuto()
        {
            List<Servers> list = new List<Servers>();
            this.lstServerRouting.ClearRoutingList();
            foreach (Servers servers in this.serverList)
            {
                string value = string.Format("{0} - {1}", servers.shortName, servers.location);
                if (this.cmbLocation.SelectedItem.ToString().Contains(value))
                {
                    list.Add(servers);
                    this.lstServerRouting.GenerateNewRoutingList(list, this.vpn.GetAvailableProtocols());
                    break;
                }
            }
        }

        // Token: 0x06000054 RID: 84 RVA: 0x00004A10 File Offset: 0x00002C10
        private string GetUserName(string sLogin)
        {
            string text = sLogin;
            if (!text.EndsWith(""))
            {
                text = (text ?? "");
            }
            return text;
        }

        // Token: 0x06000055 RID: 85 RVA: 0x00004A44 File Offset: 0x00002C44
        private void Dialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.SetDefaultState(false);
                this.lstServerRouting.ClearRoutingList();
            }
            else if (e.TimedOut)
            {
                if (this.bAutoMode)
                {
                    if (!this.lstServerRouting.IsAllAutoTried())
                    {
                        this.lstServerRouting.SetNextFailed();
                        this.ConnectToVPN(this.edtLogin.Text, this.edtPassword.Text);
                    }
                    else
                    {
                        this.ShowStatus(this.rm.GetString("noServerAvailable"), true);
                        this.lstServerRouting.ClearRoutingList();
                        this.SetDefaultState(false);
                    }
                }
                else
                {
                    this.ShowStatus(this.rm.GetString("errTimeOut"), true);
                    this.lstServerRouting.ClearRoutingList();
                    this.SetDefaultState(false);
                }
            }
            else if (e.Error != null)
            {
                if (((RasDialException)e.Error).ErrorCode == 691)
                {
                    this.ShowStatus(this.rm.GetString("wrongLogin"), true);
                    this.lstServerRouting.ClearRoutingList();
                    this.SetDefaultState(false);
                    this.ShowSettings(true);
                }
                else if (this.bAutoMode)
                {
                    if (!this.lstServerRouting.IsAllAutoTried())
                    {
                        this.lstServerRouting.SetNextFailed();
                        this.ConnectToVPN(this.edtLogin.Text, this.edtPassword.Text);
                    }
                    else
                    {
                        this.ShowStatus(this.rm.GetString("noServerAvailable"), true);
                        this.lstServerRouting.ClearRoutingList();
                        this.SetDefaultState(false);
                    }
                }
                else
                {
                    this.ShowStatus(this.rm.GetString("errConnectionOther"), true);
                    this.lstServerRouting.ClearRoutingList();
                    this.SetDefaultState(false);
                }
            }
            else if (e.Connected)
            {
                this.SetDefaultState(true);
                this.lstServerRouting.ClearRoutingList();
            }
        }

        // Token: 0x06000056 RID: 86 RVA: 0x00004C78 File Offset: 0x00002E78
        private void SaveSettings(string sFileName)
        {
            Config config = new Config();
            Settings settings = new Settings();
            settings.login = this.edtLogin.Text.Trim();
            settings.password = this.edtPassword.Text.Trim();
            settings.manualMode = false;
            settings.protocol = this.cmbProtocol.SelectedItem.ToString();
            settings.serverName = this.cmbLocation.SelectedItem.ToString();
            Helper helper = new Helper();
            string sharedDocsFolder = helper.GetSharedDocsFolder();
            string sFileName2 = string.Format("{0}\\{1}\\{2}", sharedDocsFolder, "UnblockVPN", sFileName);
            config.SaveCfg(sFileName2, settings);
        }

        // Token: 0x06000057 RID: 87 RVA: 0x00004D20 File Offset: 0x00002F20
        private Settings LoadSettings(string sFileName)
        {
            Config config = new Config();
            Helper helper = new Helper();
            string sharedDocsFolder = helper.GetSharedDocsFolder();
            string sFileName2 = string.Format("{0}\\{1}\\{2}", sharedDocsFolder, "UnblockVPN", sFileName);
            Settings settings = config.LoadCfg(sFileName2);
            this.edtLogin.Text = (this.sOrigLogin = settings.login);
            this.edtPassword.Text = (this.sOrigPass = settings.password);
            return settings;
        }

        // Token: 0x06000058 RID: 88 RVA: 0x00004DA0 File Offset: 0x00002FA0
        private void SetDefaultState(bool bConnected)
        {
            if (bConnected)
            {
                this.lblStsConnection.Text = this.rm.GetString("Connected");
                this.isConnected = true;
                this.btnConnect.Text = this.rm.GetString("btnOff");
                this.picLoading.Image = Resources.stsConnected;
                this.btnSettings.Enabled = false;
                this.cmbLocation.Enabled = false;
                this.cmbProtocol.Enabled = false;
            }
            else
            {
                this.lblStsConnection.Text = this.rm.GetString("Disconnected");
                this.isConnected = false;
                this.btnConnect.Text = this.rm.GetString("btnOn");
                this.picLoading.Image = Resources.stsDisconnected;
                this.btnSettings.Enabled = true;
                this.cmbLocation.Enabled = true;
                this.cmbProtocol.Enabled = true;
            }
        }

        // Token: 0x06000059 RID: 89 RVA: 0x00004EAA File Offset: 0x000030AA
        private void watcher_Disconnected(object sender, RasConnectionEventArgs e)
        {
            this.lstServerRouting.ClearRoutingList();
            this.SetDefaultState(false);
            this.lblStatus.Text = "";
        }

        // Token: 0x0600005A RID: 90 RVA: 0x00004ED2 File Offset: 0x000030D2
        private void watcher_Connected(object sender, RasConnectionEventArgs e)
        {
            this.SetDefaultState(true);
        }

        // Token: 0x0600005B RID: 91 RVA: 0x00004EDD File Offset: 0x000030DD
        private void lnkCompany_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.unblockvpn.com");
        }

        // Token: 0x0600005C RID: 92 RVA: 0x00004EEB File Offset: 0x000030EB
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveSettings("settings.xml");
            this.DisconnectFromVPN();
        }

        // Token: 0x0600005D RID: 93 RVA: 0x00004F01 File Offset: 0x00003101
        private void picCompany_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.unblockvpn.com");
        }

        // Token: 0x0600005E RID: 94 RVA: 0x00004F0F File Offset: 0x0000310F
        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.ShowSettings(!this.bLoginVisible);
        }

        // Token: 0x0600005F RID: 95 RVA: 0x00004F22 File Offset: 0x00003122
        private void lnkCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		    try
		    {
                this.pictureBox1.Image = Resources.loading;

                var str = string.Empty;
                str += new Random().Next(435473, 835472) + "bashar" + new Random().Next(223, 435472) + "bachir" + new Random().Next(1, 1000000) + new Random().Next(1000, 100000);

                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                var options = new ChromeOptions();
                options.AddArgument("headless");

                var driver = new ChromeDriver(service, options);
                driver.Navigate().GoToUrl("https://unblockvpn.com/create-account.php");
                driver.FindElement(By.Id("full-name")).SendKeys(str);
                driver.FindElement(By.Id("country")).SendKeys("g");
                driver.FindElement(By.Id("email")).SendKeys(str + "@rupayamail.com");
                driver.FindElement(By.Name("submit")).Submit();
                driver.Close();


                var options2 = new ChromeOptions();
                options2.AddArgument("headless");

                var driver2 = new ChromeDriver(service, options);
                driver2.Navigate().GoToUrl("https://temp-mail.org/en/option/change/");
                driver2.FindElement(By.ClassName("form-control")).SendKeys(str);
                driver2.FindElement(By.Id("postbut")).Submit();
                driver2.FindElement(By.XPath(@"//a[@id='click-to-refresh']/span")).Click();
                driver2.FindElement(By.LinkText(@"UnblockVPN: Account Detai")).SendKeys(OpenQA.Selenium.Keys.Enter);

                var strr = new WebClient().DownloadString(driver2.Url);

                edtLogin.Text = str + @"@rupayamail.com";
                string toBeSearched = @"<br><strong>Password</strong>: ";
                edtPassword.Text =
                    strr.Substring(strr.IndexOf(toBeSearched, StringComparison.Ordinal) + toBeSearched.Length)
                        .Substring(0, 8);
                this.SaveSettings("settings.xml");
                this.LoadServers();
                this.btnSave.Enabled = false;
                driver2.Close();
                this.pictureBox1.Image = Resources.stsConnected;
                lblStsConnection.Text = @"Created";
             
                Process.Start("https://www.youtube.com/user/ITPROPROFESSIONAL/videos?view_as=subscriber");
		    }
		    catch (Exception exception)
		    {
		        foreach (var process in Process.GetProcessesByName("chromedriver"))
		        {
		            process.Kill();
		        }
		        MessageBox.Show(exception.ToString());
		    }
		  
		}


        // Token: 0x06000060 RID: 96 RVA: 0x00004F30 File Offset: 0x00003130
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveSettings("settings.xml");
            this.LoadServers();
            this.btnSave.Enabled = false;
        }

        // Token: 0x06000061 RID: 97 RVA: 0x00004F53 File Offset: 0x00003153
        private void edtPassword_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = true;
        }

        // Token: 0x06000062 RID: 98 RVA: 0x00004F63 File Offset: 0x00003163
        private void edtLogin_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = true;
        }

        // Token: 0x0400001E RID: 30
        private const int MAX_HEIGHT = 480;

        // Token: 0x0400001F RID: 31
        private const int MIN_HEIGHT = 400;

        // Token: 0x04000020 RID: 32
        private const string sFileNamePBK = "CustomPhoneBook.pbk";

        // Token: 0x04000021 RID: 33
        private const string sFileNameSettings = "settings.xml";

        // Token: 0x04000022 RID: 34
        private const string sPathServersXML1 = "https://1cpanel.com/members/xmlservers.php";

        // Token: 0x04000023 RID: 35
        private const string sPathServersXML2 = "https://albert.vimjak.cz/data/unblockvpn-xmlservers.xml";

        // Token: 0x04000024 RID: 36
        private const string sPathServersXML3 = "http://en.vimjak.cz/data/unblockvpn-xmlservers.xml";

        // Token: 0x04000025 RID: 37
        private const string sCompanyFolderName = "UnblockVPN";

        // Token: 0x04000026 RID: 38
        private const string sCompanyLoginPostfix = "";

        // Token: 0x04000027 RID: 39
        private const string sCompanyLink = "http://www.unblockvpn.com";

        // Token: 0x04000028 RID: 40
        private const string sCompanAutoUpdateXML = "http://unblockvpn.com/data/autoupdate.xml";

        // Token: 0x04000029 RID: 41
        private const string sCompanyLinkCreateAccount = "http://unblockvpn.com/purchase.php";

        // Token: 0x0400002A RID: 42
        private const string sCompanyProductName = "UnblockVPN";

        // Token: 0x0400002B RID: 43
        private const string vpnPrefix = "UVPN";

        // Token: 0x0400002C RID: 44
        private const string sSharedKey = "xunblock4me";

        // Token: 0x04000040 RID: 64
        private bool isConnected = false;

        // Token: 0x04000041 RID: 65
        private string pbPath = "";

        // Token: 0x04000042 RID: 66
        private RasPhoneBook pb;

        // Token: 0x04000043 RID: 67
        private RasHandle handle = null;

        // Token: 0x04000044 RID: 68
        private RasDialer dialer;

        // Token: 0x04000045 RID: 69
        private RasConnectionWatcher watcher;

        // Token: 0x04000046 RID: 70
        private ConnectorVPN vpn;

        // Token: 0x04000047 RID: 71
        private static ErrorLog logger;

        // Token: 0x04000048 RID: 72
        private List<Servers> serverList;

        // Token: 0x04000049 RID: 73
        private ServerRouting lstServerRouting;

        // Token: 0x0400004A RID: 74
        private ResourceManager rm;

        // Token: 0x0400004B RID: 75
        private string sOrigLogin = "";

        // Token: 0x0400004C RID: 76
        private string sOrigPass = "";

        // Token: 0x0400004D RID: 77
        private bool bLoginVisible = false;

        // Token: 0x0400004E RID: 78
        private bool bAutoMode = false;
    
private void pictureBox2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("16LX4zazVRgj9fwDExns4oxFEDtm1NH6Zz");
            MessageBox.Show(@"Thanks: Address Copied To Your Clipboard");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=Z4FL2NNFDD8TA");
        }
    }
}
