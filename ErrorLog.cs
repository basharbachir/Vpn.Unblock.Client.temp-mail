using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace VPN
{
	// Token: 0x0200000B RID: 11
	internal class ErrorLog
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004F74 File Offset: 0x00003174
		public string LogPath
		{
			get
			{
				return this._LogPath;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004F8C File Offset: 0x0000318C
		public ErrorLog()
		{
			this._LogPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName), "Errors");
			if (!Directory.Exists(this._LogPath))
			{
				Directory.CreateDirectory(this._LogPath);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004FE0 File Offset: 0x000031E0
		public ErrorLog(string logPath)
		{
			this._LogPath = logPath;
			if (!Directory.Exists(this._LogPath))
			{
				Directory.CreateDirectory(this._LogPath);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005018 File Offset: 0x00003218
		public string LogError(Exception exception)
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			Process currentProcess = Process.GetCurrentProcess();
			string text = DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss") + ".txt";
			using (StreamWriter streamWriter = new StreamWriter(Path.Combine(this._LogPath, text)))
			{
				streamWriter.WriteLine("==============================================================================");
				streamWriter.WriteLine(entryAssembly.FullName);
				streamWriter.WriteLine("------------------------------------------------------------------------------");
				streamWriter.WriteLine("Application Information");
				streamWriter.WriteLine("------------------------------------------------------------------------------");
				streamWriter.WriteLine("Program      : " + entryAssembly.Location);
				streamWriter.WriteLine("Time         : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
				streamWriter.WriteLine("User         : " + Environment.UserName);
				streamWriter.WriteLine("Computer     : " + Environment.MachineName);
				streamWriter.WriteLine("OS           : " + Environment.OSVersion.ToString());
				streamWriter.WriteLine("Culture      : " + CultureInfo.CurrentCulture.Name);
				streamWriter.WriteLine("Processors   : " + Environment.ProcessorCount);
				streamWriter.WriteLine("Working Set  : " + Environment.WorkingSet);
				streamWriter.WriteLine("Framework    : " + Environment.Version);
				streamWriter.WriteLine("Run Time     : " + (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString());
				streamWriter.WriteLine("------------------------------------------------------------------------------");
				streamWriter.WriteLine("Exception Information");
				streamWriter.WriteLine("------------------------------------------------------------------------------");
				streamWriter.WriteLine("Source       : " + exception.Source.ToString().Trim());
				streamWriter.WriteLine("Method       : " + exception.TargetSite.Name.ToString());
				streamWriter.WriteLine("Type         : " + exception.GetType().ToString());
				streamWriter.WriteLine("Error        : " + this.GetExceptionStack(exception));
				streamWriter.WriteLine("Stack Trace  : " + exception.StackTrace.ToString().Trim());
				streamWriter.WriteLine("------------------------------------------------------------------------------");
				streamWriter.WriteLine("Loaded Modules");
				streamWriter.WriteLine("------------------------------------------------------------------------------");
				foreach (object obj in currentProcess.Modules)
				{
					ProcessModule processModule = (ProcessModule)obj;
					try
					{
						streamWriter.WriteLine(string.Concat(new object[]
						{
							processModule.FileName,
							" | ",
							processModule.FileVersionInfo.FileVersion,
							" | ",
							processModule.ModuleMemorySize
						}));
					}
					catch (FileNotFoundException)
					{
						streamWriter.WriteLine("File Not Found: " + processModule.ToString());
					}
					catch (Exception)
					{
					}
				}
				streamWriter.WriteLine("------------------------------------------------------------------------------");
				streamWriter.WriteLine(text);
				streamWriter.WriteLine("==============================================================================");
			}
			return text;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000540C File Offset: 0x0000360C
		private string GetExceptionStack(Exception e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(e.Message);
			while (e.InnerException != null)
			{
				e = e.InnerException;
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(e.Message);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400004F RID: 79
		private string _LogPath;
	}
}
