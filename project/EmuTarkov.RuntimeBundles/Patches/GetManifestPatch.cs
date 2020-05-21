using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Diz.Resources;
using EmuTarkov.Common.Utils.Patching;
using EmuTarkov.RuntimeBundles.Utils.Bundles;

namespace EmuTarkov.RuntimeBundles.Patches
{
	public class GetManifestPatch : AbstractPatch
	{
		public override void Apply()
		{
			PatcherUtil.PatchPrefix<GetManifestPatch>(TargetMethod());
		}

		public override MethodInfo TargetMethod()
		{
			return PatcherUtil.GetOriginalMethod<EasyAssets>(".ctor");
		}

		public static bool Prefix(ref EasyAssets __instance, ref Task __result)
		{
			string text = File.ReadAllText(PatcherConstants.ExecutingAssemblyDirectory + "/manifest.json");
			string[] splittedPath = PatcherConstants.ExecutingAssemblyDirectory.Split(new char[] { '\\' });
			string entry = splittedPath[splittedPath.Length - 1];
			
			List<Manifest> manifests = JsonConvert.DeserializeObject<RootObject>(text).manifest;

			foreach (Manifest manifest in manifests)
			{
				manifest.key = entry + ":" + manifest.key;
				BundlesUtil.Manifests.Add(manifest);
			}

			__result = Task.CompletedTask;

			return true;
		}
	}
}
