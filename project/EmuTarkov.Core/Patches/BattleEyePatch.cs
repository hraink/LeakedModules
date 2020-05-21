using System.Reflection;
using System.Threading.Tasks;
using EmuTarkov.Common.Utils.Patching;

namespace EmuTarkov.Core.Patches
{
	public class BattleEyePatch : AbstractPatch
	{
		public BattleEyePatch()
		{
			methodName = "method_0";
			flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
		}

		public override void Apply()
		{
			PatcherUtil.PatchPrefix<BattleEyePatch>(TargetMethod());
		}

		public override MethodInfo TargetMethod()
		{
			return PatcherConstants.MainApplicationType.GetMethod(methodName, flags);
		}

		public static bool Prefix(ref Task __result)
		{
			__result = Task.CompletedTask;

			return false;
		}
	}
}
