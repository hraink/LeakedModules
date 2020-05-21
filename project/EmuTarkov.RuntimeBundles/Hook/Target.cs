using NLog.Targets;
using EmuTarkov.Common.Utils.Hook;

namespace EmuTarkov.RuntimeBundles.Hook
{
	[Target("EmuTarkov.RuntimeBundles")]
	public sealed class Target : TargetWithLayout
	{
		public Target()
		{
			Loader<Instance>.Load();
		}
	}
}
