using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using EmuTarkov.Common.Utils.Patching;
using EmuTarkov.RuntimeBundles.Utils.Bundles;

namespace EmuTarkov.RuntimeBundles.Patches
{
	public class GetAllBundlesPatch : AbstractPatch
	{
		public override void Apply()
		{
			PatcherUtil.PatchPostfix<GetAllBundlesPatch>(TargetMethod());
		}

		public override MethodInfo TargetMethod()
		{
			return PatcherUtil.GetOriginalMethod<AssetBundleManifest>("GetAllAssetBundles");
		}

		public static void Postfix(AssetBundleRequest __instance, ref string[] __result)
		{
			List<string> first = __result.ToList<string>();
			List<string> list = new List<string>();

			foreach (Manifest manifest in BundlesUtil.Manifests)
			{
				string item = manifest.key.Split(new char[] {':'})[1];
				list.Add(item);
			}

			__result = first.Union(list).ToList<string>().ToArray<string>();
		}
	}
}
