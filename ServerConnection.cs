using System;

namespace VPN
{
	// Token: 0x02000005 RID: 5
	internal class ServerConnection
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002B34 File Offset: 0x00000D34
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002B4B File Offset: 0x00000D4B
		public string ip { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002B54 File Offset: 0x00000D54
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002B6B File Offset: 0x00000D6B
		public string protocol { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002B74 File Offset: 0x00000D74
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002B8B File Offset: 0x00000D8B
		public string name { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002B94 File Offset: 0x00000D94
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002BAB File Offset: 0x00000DAB
		public bool autoTried { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002BB4 File Offset: 0x00000DB4
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002BCB File Offset: 0x00000DCB
		public bool failed { get; set; }
	}
}
