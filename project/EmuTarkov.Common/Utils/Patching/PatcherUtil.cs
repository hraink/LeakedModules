using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace EmuTarkov.Common.Utils.Patching
{
	public static class PatcherUtil
	{
		private static Harmony harmony;

		static PatcherUtil()
		{
			harmony = new Harmony("com.emutarkov.common");
		}

		public static MethodInfo GetOriginalMethod<T>(string methodName)
		{
			return AccessTools.Method(typeof(T), methodName);
		}
		
		public static void PatchPrefix<T>(MethodInfo original)
		{
			harmony.Patch(original, prefix: new HarmonyMethod(typeof(T).GetMethod("Prefix")));
			Debug.LogError("EmuTarkov.Common: Applied prefix patch " + typeof(T).Name);
		}

		public static void PatchPostfix<T>(MethodInfo original)
		{
			harmony.Patch(original, postfix: new HarmonyMethod(typeof(T).GetMethod("Postfix")));
			Debug.LogError("EmuTarkov.Common: Applied postfix patch " + typeof(T).Name);
		}
	}
}
