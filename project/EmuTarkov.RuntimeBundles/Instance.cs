using UnityEngine;
using EmuTarkov.RuntimeBundles.Patches;

namespace EmuTarkov.RuntimeBundles
{
	public class Instance : MonoBehaviour
	{
		private void Start()
		{
			new GetAllBundlesPatch().Apply();
			new GetManifestPatch().Apply();
			new GetDependenciesPatch().Apply();
			new GetAllBundlesPatch().Apply();

			Debug.LogError("EmuTarkov.RuntimeBundles: Loaded");
		}
	}
}
