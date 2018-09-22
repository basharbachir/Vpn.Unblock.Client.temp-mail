using System;
using System.Collections.Generic;
using System.Threading;

namespace VPN
{
	// Token: 0x02000004 RID: 4
	internal class ServerRouting
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000273C File Offset: 0x0000093C
		public ServerRouting()
		{
			this.lstConnection = new List<ServerConnection>();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002754 File Offset: 0x00000954
		public List<ServerConnection> GetRoutingServerList()
		{
			List<ServerConnection> list = new List<ServerConnection>();
			Monitor.Enter(this.lstConnection);
			foreach (ServerConnection item in this.lstConnection)
			{
				list.Add(item);
			}
			Monitor.Exit(this.lstConnection);
			return list;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027D8 File Offset: 0x000009D8
		public void GenerateNewRoutingList(List<Servers> lstServer, List<string> lstProtocol)
		{
			Monitor.Enter(this.lstConnection);
			this.lstConnection.Clear();
			foreach (Servers servers in lstServer)
			{
				foreach (string protocol in lstProtocol)
				{
					ServerConnection serverConnection = new ServerConnection();
					serverConnection.ip = servers.ip;
					serverConnection.protocol = protocol;
					serverConnection.name = servers.name;
					serverConnection.autoTried = false;
					serverConnection.failed = false;
					this.lstConnection.Add(serverConnection);
				}
			}
			Monitor.Exit(this.lstConnection);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028D8 File Offset: 0x00000AD8
		public void ClearRoutingList()
		{
			Monitor.Enter(this.lstConnection);
			foreach (ServerConnection serverConnection in this.lstConnection)
			{
				serverConnection.autoTried = false;
				serverConnection.failed = false;
			}
			Monitor.Exit(this.lstConnection);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002954 File Offset: 0x00000B54
		public bool SetNextAutoTry(out string ip, out string protocol, out string name)
		{
			Monitor.Enter(this.lstConnection);
			bool result = false;
			ip = "";
			protocol = "";
			name = "";
			foreach (ServerConnection serverConnection in this.lstConnection)
			{
				if (!serverConnection.autoTried)
				{
					serverConnection.autoTried = true;
					result = true;
					ip = serverConnection.ip;
					protocol = serverConnection.protocol;
					name = serverConnection.name;
					break;
				}
			}
			Monitor.Exit(this.lstConnection);
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002A10 File Offset: 0x00000C10
		public bool SetNextFailed()
		{
			Monitor.Enter(this.lstConnection);
			bool result = false;
			foreach (ServerConnection serverConnection in this.lstConnection)
			{
				if (serverConnection.autoTried && !serverConnection.failed)
				{
					serverConnection.failed = true;
					result = true;
					break;
				}
			}
			Monitor.Exit(this.lstConnection);
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002AAC File Offset: 0x00000CAC
		public bool IsAllAutoTried()
		{
			Monitor.Enter(this.lstConnection);
			bool result = true;
			foreach (ServerConnection serverConnection in this.lstConnection)
			{
				if (!serverConnection.autoTried)
				{
					result = false;
				}
			}
			Monitor.Exit(this.lstConnection);
			return result;
		}

		// Token: 0x0400000B RID: 11
		private List<ServerConnection> lstConnection;
	}
}
