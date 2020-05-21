using UnityEngine;
using EmuTarkov.Core.Patches;

namespace EmuTarkov.Core
{
	public class Instance : MonoBehaviour
	{
		private void Start()
		{
			Debug.LogError("EmuTarkov.Core: Loaded");

			new BattleEyePatch().Apply();
			new SslCertificatePatch().Apply();
			new HttpRequestPatch().Apply();
		}
	}
}
