using System;
using System.IO;
using System.Linq;
using System.Reflection;
using EFT;

namespace EmuTarkov.Common.Utils.Patching
{
	public static class PatcherConstants
	{
		public static Assembly TargetAssembly = typeof(AbstractGame).Assembly;
		public static Type MainApplicationType = TargetAssembly.GetTypes().Single(x => x.Name == "MainApplication");
		public static Type LocalGameType = TargetAssembly.GetTypes().Single(x => x.Name == "LocalGame");
		public static Type MatchmakerOfflineRaidType = TargetAssembly.GetTypes().Single(x => x.Name == "MatchmakerOfflineRaid");
		public static string ExecutingAssemblyDirectory = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
	}
}
