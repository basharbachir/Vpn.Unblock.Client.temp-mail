using System;
using System.Collections.Generic;
using System.Xml;

namespace VPN
{
	// Token: 0x02000002 RID: 2
	internal class ServersXML
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public List<Servers> LoadServers(string sURL)
		{
			List<Servers> list = new List<Servers>();
			XmlReader xmlReader = new XmlTextReader(sURL);
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.ToLower() == "server")
				{
					string attribute = xmlReader.GetAttribute("Country");
					string attribute2 = xmlReader.GetAttribute("IP");
					string attribute3 = xmlReader.GetAttribute("AlternativeIP");
					string attribute4 = xmlReader.GetAttribute("SSTPAddress");
					string attribute5 = xmlReader.GetAttribute("Name");
					string attribute6 = xmlReader.GetAttribute("Short");
					string attribute7 = xmlReader.GetAttribute("Location");
					string attribute8 = xmlReader.GetAttribute("Priority");
					int priority;
					int.TryParse(attribute8, out priority);
					Servers item = new Servers(attribute, attribute2, attribute3, attribute4, attribute5, attribute6, attribute7, priority);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000021A4 File Offset: 0x000003A4
		public List<Servers> GetServersByCountryPriority(List<Servers> serverList, string sCountry)
		{
			List<Servers> list = serverList.FindAll((Servers sl) => sl.country.ToUpper() == sCountry.ToUpper());
			list.Sort((Servers x, Servers y) => x.priority.CompareTo(y.priority));
			list.Reverse();
			return list;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000222C File Offset: 0x0000042C
		public List<Servers> GetServersByPriority(List<Servers> serverList)
		{
			serverList.Sort((Servers x, Servers y) => x.priority.CompareTo(y.priority));
			serverList.Reverse();
			return serverList;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002270 File Offset: 0x00000470
		public List<Servers> GeDefaulttServers()
		{
			return new List<Servers>
			{
				new Servers("UK", "77.75.120.94", "77.75.120.92", "sstp-eu11.finevpn.com", "eu11.finevpn.com", "eu11", "United Kingdom, London", 51),
				new Servers("US", "173.239.9.86", "209.200.4.134", "sstp-us6.finevpn.com", "us6.finevpn.com", "us6", "New York, NY", 50),
				new Servers("NL", "193.138.220.6", "193.138.220.3", "sstp-eu10.finevpn.com", "eu10.finevpn.com", "eu10", "Netherlands, Amsterdam", 49),
				new Servers("SE", "178.73.198.198", "178.73.198.196", "sstp-eu9.finevpn.com", "eu9.finevpn.com", "eu9", "Sweden,Stockholm", 48),
				new Servers("CZ", "93.190.55.253", "93.190.55.239", "sstp-eu3.finevpn.com", "eu3.finevpn.com", "eu3", "Czech Republic, Prague", 47),
				new Servers("UK", "188.227.162.109", "188.227.162.107", "sstp-eu8.finevpn.com", "eu8.finevpn.com", "eu8", "United Kingdom, Gosport", 46),
				new Servers("CZ", "81.0.217.77", "81.0.217.51", "sstp-eu.finevpn.com", "eu.finevpn.com", "eu", "Czech Republic, Prague", 46),
				new Servers("US", "173.239.5.206", "50.115.224.188", "", "us2.finevpn.com", "us2", "New York, NY", 45),
				new Servers("SK", "92.240.235.130", "", "sstp-eu2.finevpn.com", "eu2.finevpn.com", "eu2", "Slovakia, Bratislava", 45),
				new Servers("UK", "94.229.69.110", "94.229.69.108", "sstp-eu7.finevpn.com", "eu7.finevpn.com", "eu7", "United Kingdom, London", 41),
				new Servers("US", "173.225.127.22", "50.115.238.134", "sstp-us5.finevpn.com", "us5.finevpn.com", "us5", "Los Angeles, CA", 40),
				new Servers("UK", "109.200.15.125", "109.200.15.123", "sstp-eu6.finevpn.com", "eu6.finevpn.com", "eu6", "United Kingdom, Gosport", 36),
				new Servers("US", "173.208.17.118", "173.208.17.116", "sstp-us3.finevpn.com", "us3.finevpn.com", "us3", "Dallas, TX", 35),
				new Servers("UK", "77.74.192.190", "77.74.192.188", "sstp-eu5.finevpn.com", "eu5.finevpn.com", "eu5", "United Kingdom, London", 31),
				new Servers("US", "67.159.29.174", "67.159.29.172", "sstp-us4.finevpn.com", "us4.finevpn.com", "us4", "Chicago, IL", 30),
				new Servers("US", "77.75.120.94", "77.75.120.92", "sstp-us.finevpn.com", "us.finevpn.com", "us", "Denver, CO", 25),
				new Servers("UK", "77.245.69.82", "", "", "eu4.finevpn.com", "eu4", "United Kingdom, Gosport", 24)
			};
		}
	}
}
