using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using DotRas;

namespace VPN
{
	// Token: 0x0200000D RID: 13
	internal class ConnectorVPN
	{
		// Token: 0x06000089 RID: 137 RVA: 0x0000599C File Offset: 0x00003B9C
		public int GetWindowsVersion()
		{
			int result = 0;
			int major = Environment.OSVersion.Version.Major;
			int minor = Environment.OSVersion.Version.Minor;
			if (major == 5)
			{
				result = this.VER_XP;
			}
			else if (major == 6)
			{
				if (minor == 0)
				{
					result = this.VER_VISTA;
				}
				else if (minor == 1)
				{
					result = this.VER_W7;
				}
				else if (minor >= 2)
				{
					result = this.VER_W8;
				}
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005A3C File Offset: 0x00003C3C
		public RasPhoneBook ClearPhoneBook(RasPhoneBook myPB, string sVPNPrefix)
		{
			try
			{
				foreach (RasEntry rasEntry in myPB.Entries)
				{
					if (rasEntry.Name.CompareTo(sVPNPrefix) == 0)
					{
						myPB.Entries.Remove(rasEntry);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				string path = myPB.Path;
				if (File.Exists(path))
				{
					File.Delete(path);
					myPB = new RasPhoneBook();
					myPB.Open(path);
				}
			}
			return myPB;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005B0C File Offset: 0x00003D0C
		public RasPhoneBook SetPhoneBookSSTP(string server, RasPhoneBook myPB, string sVPNPrefix)
		{
			myPB = this.ClearPhoneBook(myPB, sVPNPrefix);
			RasDevice sstpDevice = ConnectorVPN.GetSstpDevice(RasDevice.GetDevices());
			if (sstpDevice != null)
			{
				RasEntry item = RasEntry.CreateVpnEntry(sVPNPrefix, server, RasVpnStrategy.SstpOnly, sstpDevice, true);
				myPB.Entries.Add(item);
			}
			return myPB;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005B58 File Offset: 0x00003D58
		public RasPhoneBook SetPhoneBookPPTP(string server, RasPhoneBook myPB, string sVPNPrefix)
		{
			myPB = this.ClearPhoneBook(myPB, sVPNPrefix);
			RasDevice pptpDevice = ConnectorVPN.GetPptpDevice(RasDevice.GetDevices());
			if (pptpDevice != null)
			{
				RasEntry item = RasEntry.CreateVpnEntry(sVPNPrefix, server, RasVpnStrategy.PptpOnly, pptpDevice, true);
				myPB.Entries.Add(item);
			}
			return myPB;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public RasPhoneBook SetPhoneBookL2TP(string server, RasPhoneBook myPB, string sVPNPrefix, string sSharedKey)
		{
			myPB = this.ClearPhoneBook(myPB, sVPNPrefix);
			RasDevice l2tpDevice = ConnectorVPN.GetL2tpDevice(RasDevice.GetDevices());
			if (l2tpDevice != null)
			{
				RasEntry rasEntry = RasEntry.CreateVpnEntry(sVPNPrefix, server, RasVpnStrategy.L2tpOnly, l2tpDevice, true);
				rasEntry.Options.UsePreSharedKey = true;
				rasEntry.Options.UseLogOnCredentials = true;
				myPB.Entries.Add(rasEntry);
				rasEntry.UpdateCredentials(RasPreSharedKey.Client, sSharedKey);
			}
			return myPB;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005C14 File Offset: 0x00003E14
		private static RasDevice GetPptpDevice(ReadOnlyCollection<RasDevice> collDevice)
		{
			RasDevice result = null;
			foreach (RasDevice rasDevice in collDevice)
			{
				if (rasDevice.Name.Contains("PPTP") && rasDevice.DeviceType == RasDeviceType.Vpn)
				{
					result = rasDevice;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005C9C File Offset: 0x00003E9C
		private static RasDevice GetSstpDevice(ReadOnlyCollection<RasDevice> collDevice)
		{
			RasDevice result = null;
			foreach (RasDevice rasDevice in collDevice)
			{
				if (rasDevice.Name.Contains("SSTP") && rasDevice.DeviceType == RasDeviceType.Vpn)
				{
					result = rasDevice;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005D24 File Offset: 0x00003F24
		private static RasDevice GetL2tpDevice(ReadOnlyCollection<RasDevice> collDevice)
		{
			RasDevice result = null;
			foreach (RasDevice rasDevice in collDevice)
			{
				if (rasDevice.Name.Contains("L2TP") && rasDevice.DeviceType == RasDeviceType.Vpn)
				{
					result = rasDevice;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005DAC File Offset: 0x00003FAC
		public RasHandle ConnectVPN(RasDialer myDialer, RasPhoneBook myPB, ServerRouting lstServerRouting, string sVPNPrefix, string sLogin, string sPass, string sPath, string sSharedKey)
		{
			RasHandle result = null;
			bool flag = false;
			string text;
			string text2;
			string server;
			if (lstServerRouting.SetNextAutoTry(out text, out text2, out server))
			{
				string text3 = text2;
				if (text3 != null)
				{
					if (!(text3 == "PPTP"))
					{
						if (!(text3 == "L2TP"))
						{
							if (text3 == "SSTP")
							{
								myPB = this.SetPhoneBookSSTP(server, myPB, sVPNPrefix);
								flag = true;
							}
						}
						else
						{
							myPB = this.SetPhoneBookL2TP(text, myPB, sVPNPrefix, sSharedKey);
							flag = true;
						}
					}
					else
					{
						myPB = this.SetPhoneBookPPTP(text, myPB, sVPNPrefix);
						flag = true;
					}
				}
				if (flag && text.Length > 0)
				{
					myDialer.EntryName = sVPNPrefix;
					myDialer.PhoneBookPath = sPath;
					myDialer.AllowUseStoredCredentials = true;
					try
					{
						myDialer.Credentials = new NetworkCredential(sLogin, sPass);
						result = myDialer.DialAsync();
					}
					catch (Exception ex)
					{
					}
				}
			}
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005EB4 File Offset: 0x000040B4
		public RasHandle ConnectVPN_PPTP(RasDialer myDialer, RasPhoneBook myPB, Servers server, string sVPNPrefix, string sLogin, string sPass, string sPath)
		{
			RasHandle result = null;
			if (server.ip.Length > 0)
			{
				myPB = this.SetPhoneBookPPTP(server.ip, myPB, sVPNPrefix);
				myDialer.EntryName = sVPNPrefix;
				myDialer.PhoneBookPath = sPath;
				myDialer.AllowUseStoredCredentials = true;
				try
				{
					myDialer.Credentials = new NetworkCredential(sLogin, sPass);
					result = myDialer.DialAsync();
				}
				catch (Exception ex)
				{
				}
			}
			return result;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005F40 File Offset: 0x00004140
		public RasHandle ConnectVPN_L2TP(RasDialer myDialer, RasPhoneBook myPB, Servers server, string sVPNPrefix, string sLogin, string sPass, string sPath, string sSharedKey)
		{
			RasHandle result = null;
			if (server.ip.Length > 0)
			{
				myPB = this.SetPhoneBookL2TP(server.ip, myPB, sVPNPrefix, sSharedKey);
				myDialer.EntryName = sVPNPrefix;
				myDialer.PhoneBookPath = sPath;
				myDialer.AllowUseStoredCredentials = true;
				try
				{
					myDialer.Credentials = new NetworkCredential(sLogin, sPass);
					result = myDialer.DialAsync();
				}
				catch (Exception ex)
				{
				}
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005FD0 File Offset: 0x000041D0
		public RasHandle ConnectVPN_SSTP(RasDialer myDialer, RasPhoneBook myPB, Servers server, string sVPNPrefix, string sLogin, string sPass, string sPath)
		{
			RasHandle result = null;
			if (server.ip.Length > 0)
			{
				myPB = this.SetPhoneBookSSTP(server.sstpAddress, myPB, sVPNPrefix);
				myDialer.EntryName = sVPNPrefix;
				myDialer.PhoneBookPath = sPath;
				myDialer.AllowUseStoredCredentials = true;
				try
				{
					myDialer.Credentials = new NetworkCredential(sLogin, sPass);
					result = myDialer.DialAsync();
				}
				catch (Exception ex)
				{
				}
			}
			return result;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000605C File Offset: 0x0000425C
		public List<string> GetAvailableProtocols()
		{
			int windowsVersion = this.GetWindowsVersion();
			List<string> list = new List<string>();
			if (windowsVersion == this.VER_XP || windowsVersion == this.VER_VISTA)
			{
				list.Add("PPTP");
			}
			else if (windowsVersion == this.VER_W7 || windowsVersion == this.VER_W8)
			{
				list.Add("PPTP");
				list.Add("L2TP");
				list.Add("SSTP");
			}
			return list;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000060EC File Offset: 0x000042EC
		public RasHandle ConnectVPNbyPriority(RasDialer myDialer, RasPhoneBook myPB, string server, string sVPNPrefix, string sLogin, string sPass, string sPath)
		{
			int windowsVersion = this.GetWindowsVersion();
			RasHandle result = null;
			if (windowsVersion == this.VER_XP || windowsVersion == this.VER_VISTA)
			{
				myPB = this.SetPhoneBookPPTP(server, myPB, sVPNPrefix);
			}
			else if (windowsVersion == this.VER_W7 || windowsVersion == this.VER_W8)
			{
				myPB = this.SetPhoneBookPPTP(server, myPB, sVPNPrefix);
			}
			myDialer.EntryName = sVPNPrefix;
			myDialer.PhoneBookPath = sPath;
			myDialer.AllowUseStoredCredentials = true;
			try
			{
				myDialer.Credentials = new NetworkCredential(sLogin, sPass);
				result = myDialer.DialAsync();
			}
			catch (Exception ex)
			{
			}
			return result;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000061B4 File Offset: 0x000043B4
		public void DisconnectFromVPN(RasDialer myDialer, RasPhoneBook myPB, string sVPNPrefix)
		{
			if (myDialer.IsBusy)
			{
				myDialer.DialAsyncCancel();
			}
			else
			{
				foreach (RasConnection rasConnection in RasConnection.GetActiveConnections())
				{
					rasConnection.HangUp();
				}
			}
			this.ClearPhoneBook(myPB, sVPNPrefix);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006230 File Offset: 0x00004430
		public string GetIPAddress()
		{
			string result = "";
			using (IEnumerator<RasConnection> enumerator = RasConnection.GetActiveConnections().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					RasConnection rasConnection = enumerator.Current;
					RasIPInfo rasIPInfo = (RasIPInfo)rasConnection.GetProjectionInfo(RasProjectionType.IP);
					result = rasIPInfo.ServerIPAddress.ToString();
				}
			}
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000062B0 File Offset: 0x000044B0
		public bool TestConnection(string IP)
		{
			bool result = false;
			try
			{
				Ping ping = new Ping();
				result = (ping.Send(IP).Status == IPStatus.Success);
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x04000052 RID: 82
		public int VER_XP = 1;

		// Token: 0x04000053 RID: 83
		public int VER_VISTA = 2;

		// Token: 0x04000054 RID: 84
		public int VER_W7 = 3;

		// Token: 0x04000055 RID: 85
		public int VER_W8 = 4;
	}
}
