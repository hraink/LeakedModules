using Diz.Resources;
using EmuTarkov.Common.Utils.Patching;
using EmuTarkov.RuntimeBundles.Utils.Bundles;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace EmuTarkov.RuntimeBundles.Patches
{
	public class GetDependenciesPatch : AbstractPatch
	{
		public override void Apply()
		{
			PatcherUtil.PatchPrefix<GetDependenciesPatch>(TargetMethod());
		}

		public override MethodInfo TargetMethod()
		{
			return PatcherUtil.GetOriginalMethod<EasyBundle>("GetDirectDependencies");
		}

		public static void Postfix(EasyAssets __instance, string key, ref string[] __result)
		{
			foreach (Manifest manifest in BundlesUtil.Manifests)
			{
				if (key == manifest.key.Split(new char[] { ':' })[1])
				{
					List<string> first = (__result == null) ? new List<string>() : __result.ToList();
					__result = first.Union(manifest.dependencyKeys).ToList().ToArray<string>();
				}
			}
		}
	}
}
