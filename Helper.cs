using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VPN
{
	// Token: 0x02000006 RID: 6
	internal class Helper
	{
		// Token: 0x0600002C RID: 44
		[DllImport("shell32.dll")]
		private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, uint dwFlags, StringBuilder pszPath);

		// Token: 0x0600002D RID: 45 RVA: 0x00002BDC File Offset: 0x00000DDC
		public string GetSharedDocsFolder()
		{
			StringBuilder stringBuilder = new StringBuilder(259);
			int nFolder = 46;
			int num = Helper.SHGetFolderPath(IntPtr.Zero, nFolder, IntPtr.Zero, 0u, stringBuilder);
			return stringBuilder.ToString();
		}
	}
}
