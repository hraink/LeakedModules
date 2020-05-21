using UnityEngine;

namespace EmuTarkov.Common.Utils.Hook
{
	public class Loader<T> where T : MonoBehaviour
	{
		public static GameObject HookObject
		{
			get
			{
				GameObject result = GameObject.Find("EmuTarkov Instance");

				if (result == null)
				{
					result = new GameObject("EmuTarkov Instance");
					Object.DontDestroyOnLoad(result);
				}

				return result;
			}
		}

		public static void Load()
		{
			HookObject.GetOrAddComponent<T>();
		}
	}
}
