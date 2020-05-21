using UnityEngine;
using EmuTarkov.SinglePlayer.Patches;

namespace EmuTarkov.SinglePlayer
{
    public class Instance : MonoBehaviour
    {
		private void Start()
		{
			new LocationLootPatch().Apply();
			//new SaveLootPatch().Apply();

			Debug.LogError("EmuTarkov.SinglePlayer: Loaded");
		}
	}
}
