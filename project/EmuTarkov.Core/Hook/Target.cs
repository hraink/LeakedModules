using NLog.Targets;
using EmuTarkov.Common.Utils.Hook;

namespace EmuTarkov.Core.Hook
{
	[Target("EmuTarkov.Core")]
	public sealed class Target : TargetWithLayout
	{
		public Target()
		{
			Loader<Instance>.Load();
		}
	}
}
