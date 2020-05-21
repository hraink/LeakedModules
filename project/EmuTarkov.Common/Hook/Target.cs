using NLog.Targets;
using EmuTarkov.Common.Utils.Hook;

namespace EmuTarkov.Common.Hook
{
	[Target("EmuTarkov.Common")]
	public sealed class Target : TargetWithLayout
	{
		public Target()
		{
			Loader<Instance>.Load();
		}
	}
}
