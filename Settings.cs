using System;

namespace VPN
{
	// Token: 0x02000008 RID: 8
	public class Settings
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003028 File Offset: 0x00001228
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000303F File Offset: 0x0000123F
		public bool manualMode { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003048 File Offset: 0x00001248
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000305F File Offset: 0x0000125F
		public string login { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003068 File Offset: 0x00001268
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000307F File Offset: 0x0000127F
		public string password { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00003088 File Offset: 0x00001288
		// (set) Token: 0x0600003A RID: 58 RVA: 0x0000309F File Offset: 0x0000129F
		public string serverName { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000030A8 File Offset: 0x000012A8
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000030BF File Offset: 0x000012BF
		public string protocol { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000030C8 File Offset: 0x000012C8
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000030DF File Offset: 0x000012DF
		public string country { get; set; }
	}
}
