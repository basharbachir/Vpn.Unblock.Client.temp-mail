using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace VPN
{
	// Token: 0x02000009 RID: 9
	internal static class Program
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000030F0 File Offset: 0x000012F0
		[STAThread]
		private static void Main()
		{
			string location = Assembly.GetExecutingAssembly().Location;
			FileSystemInfo fileSystemInfo = new FileInfo(location);
			string name = fileSystemInfo.Name;
			bool flag;
			Program.mutex = new Mutex(true, "Global\\" + name, out flag);
			if (flag)
			{
				Program.mutex.ReleaseMutex();
			}
			if (flag)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new frmMain());
			}
		}

		// Token: 0x0400001D RID: 29
		private static Mutex mutex;
	}
}
