using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Diz.Resources;
using EmuTarkov.Common.Utils.Patching;
using EmuTarkov.RuntimeBundles.Utils.Bundles;
using HarmonyLib;

namespace EmuTarkov.RuntimeBundles.Patches
{
	public class LoadBundlesPatch : AbstractPatch
	{
		public override void Apply()
		{
			PatcherUtil.PatchPostfix<LoadBundlesPatch>(TargetMethod());
		}

		public override MethodInfo TargetMethod()
		{
			return PatcherUtil.GetOriginalMethod<EasyAssets>("Init");
		}

		public static async void Postfix(EasyAssets __instance, Task __result)
		{
			await __result;

			string resourcesRoot = "/StreamingAssets/Windows/";
			Dictionary<string, IEasyBundle> dictionary = new Dictionary<string, IEasyBundle>();
			IEasyBundle[] easyBundles = Traverse.Create(__instance).Field("_bundles").GetValue<IEasyBundle[]>();

			if (easyBundles == null)
			{
				return;
			}

			foreach (IEasyBundle easyBundle in easyBundles)
			{
				dictionary.Add(Traverse.Create(easyBundle).Property("Key", null).GetValue<string>(), easyBundle);
			}

			foreach (Manifest manifest in BundlesUtil.Manifests)
			{
				string entry = manifest.key.Split(new char[] { ':' })[0];
				string path = manifest.key.Split(new char[] { ':' })[1];
				IEasyBundle easyBundle;

				if (dictionary.TryGetValue(path, out easyBundle))
				{
					string filepath = new DirectoryInfo(PatcherConstants.ExecutingAssemblyDirectory).Parent.ToString() + "/" + entry;
					Traverse.Create(easyBundle).Field("_path").SetValue(filepath + resourcesRoot + path);
				}
			}
		}
	}
}
