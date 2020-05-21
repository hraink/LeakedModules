using NLog.Targets;
using EmuTarkov.Common.Utils.Hook;

namespace EmuTarkov.SinglePlayer.Hook
{
	[Target("EmuTarkov.SinglePlayer")]
	public sealed class Target : TargetWithLayout
	{
		public Target()
		{
			Loader<Instance>.Load();
		}
	}
}
