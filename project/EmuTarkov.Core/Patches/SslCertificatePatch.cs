using System.Linq;
using System.Reflection;
using EmuTarkov.Common.Utils.Patching;

namespace EmuTarkov.Core.Patches
{
	public class SslCertificatePatch : AbstractPatch
	{
		public SslCertificatePatch()
		{
			methodName = "ValidateCertificate";
			flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
		}

		public override void Apply()
		{
			PatcherUtil.PatchPrefix<SslCertificatePatch>(TargetMethod());
		}

		public override MethodInfo TargetMethod()
		{
			return PatcherConstants.TargetAssembly.GetTypes().Single(x => x.GetMethod(methodName, flags) != null).GetMethod(methodName, flags);
		}

		public static bool Prefix(ref bool __result)
		{
			__result = true;

			return false;
		}
	}
}
