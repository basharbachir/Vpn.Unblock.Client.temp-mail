using System;

namespace VPN
{
	// Token: 0x02000003 RID: 3
	public class Servers
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000025D2 File Offset: 0x000007D2
		public Servers()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000025E0 File Offset: 0x000007E0
		public Servers(string _country, string _ip, string _alternativeIP, string _sstpAddress, string _name, string _shortName, string _location, int _priority)
		{
			this.country = _country;
			this.ip = _ip;
			this.alternativeIP = _alternativeIP;
			this.sstpAddress = _sstpAddress;
			this.name = _name;
			this.shortName = _shortName;
			this.location = _location;
			this.priority = _priority;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000263C File Offset: 0x0000083C
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002653 File Offset: 0x00000853
		public string country { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000265C File Offset: 0x0000085C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002673 File Offset: 0x00000873
		public string ip { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000267C File Offset: 0x0000087C
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002693 File Offset: 0x00000893
		public string alternativeIP { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000269C File Offset: 0x0000089C
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000026B3 File Offset: 0x000008B3
		public string sstpAddress { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000026BC File Offset: 0x000008BC
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000026D3 File Offset: 0x000008D3
		public string name { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000026DC File Offset: 0x000008DC
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000026F3 File Offset: 0x000008F3
		public string shortName { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000026FC File Offset: 0x000008FC
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002713 File Offset: 0x00000913
		public string location { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000271C File Offset: 0x0000091C
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002733 File Offset: 0x00000933
		public int priority { get; set; }
	}
}
