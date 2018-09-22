using System;
using System.IO;
using System.Text;
using System.Xml;

namespace VPN
{
	// Token: 0x02000007 RID: 7
	internal class Config
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002C20 File Offset: 0x00000E20
		public Settings LoadCfg(string sFileName)
		{
			Settings settings = new Settings();
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(sFileName, FileMode.Open, FileAccess.Read);
				StringBuilder stringBuilder = new StringBuilder();
				XmlReader xmlReader = new XmlTextReader(fileStream);
				xmlReader.ReadToFollowing("ManualMode");
				settings.manualMode = Convert.ToBoolean(xmlReader.ReadElementContentAsInt());
				xmlReader.ReadToFollowing("Login");
				settings.login = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("Pass");
				string cipherText = xmlReader.ReadElementContentAsString();
				settings.password = DataProtection.Decrypt(cipherText, this.passPhrase, this.saltValue, this.hashAlgorithm, this.passwordIterations, this.initVector, this.keySize);
				xmlReader.ReadToFollowing("Server");
				settings.serverName = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("Protocol");
				settings.protocol = xmlReader.ReadElementContentAsString();
				xmlReader.ReadToFollowing("Country");
				if (xmlReader.Name == "Country")
				{
					settings.country = xmlReader.ReadElementContentAsString();
				}
				else
				{
					settings.country = "UK";
				}
				fileStream.Close();
			}
			catch (FileNotFoundException ex)
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
				settings = this.SaveDefault(sFileName, false);
			}
			catch (DirectoryNotFoundException ex2)
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
				settings = this.SaveDefault(sFileName, true);
			}
			catch (Exception ex3)
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
				settings = this.SaveDefault(sFileName, true);
			}
			return settings;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002E04 File Offset: 0x00001004
		public Settings SaveDefault(string sFileName, bool bCreateDirectory)
		{
			Settings settings = new Settings();
			settings.login = "";
			settings.manualMode = false;
			settings.password = "";
			settings.protocol = "PPTP";
			settings.serverName = "eu6.finevpn.com";
			settings.country = "UK";
			if (bCreateDirectory)
			{
				string directoryName = Path.GetDirectoryName(sFileName);
				Directory.CreateDirectory(directoryName);
			}
			this.SaveCfg(sFileName, settings);
			return settings;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002E84 File Offset: 0x00001084
		public void SaveCfg(string sFileName, Settings settings)
		{
			FileStream fileStream = null;
			if (!Directory.Exists(Path.GetDirectoryName(sFileName)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(sFileName));
			}
			try
			{
				fileStream = new FileStream(sFileName, FileMode.Create);
				XmlWriter xmlWriter = XmlWriter.Create(fileStream, new XmlWriterSettings
				{
					Indent = true
				});
				xmlWriter.WriteStartElement("settings");
				xmlWriter.WriteElementString("ManualMode", Convert.ToInt32(settings.manualMode).ToString());
				xmlWriter.WriteElementString("Login", settings.login);
				string text = DataProtection.Encrypt(settings.password, this.passPhrase, this.saltValue, this.hashAlgorithm, this.passwordIterations, this.initVector, this.keySize);
				xmlWriter.WriteStartElement("Pass");
				xmlWriter.WriteString(text);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteElementString("Server", settings.serverName);
				xmlWriter.WriteElementString("Protocol", settings.protocol);
				xmlWriter.WriteElementString("Country", settings.country);
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
				fileStream.Close();
			}
			catch (Exception ex)
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x04000011 RID: 17
		private string passPhrase = "Pas5pr@se";

		// Token: 0x04000012 RID: 18
		private string saltValue = "s@1tValue";

		// Token: 0x04000013 RID: 19
		private string hashAlgorithm = "SHA1";

		// Token: 0x04000014 RID: 20
		private int passwordIterations = 2;

		// Token: 0x04000015 RID: 21
		private string initVector = "@1B2c3D4e5F6g7H8";

		// Token: 0x04000016 RID: 22
		private int keySize = 256;
	}
}
