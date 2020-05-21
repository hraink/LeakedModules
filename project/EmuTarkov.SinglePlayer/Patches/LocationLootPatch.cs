using System.Reflection;
using EmuTarkov.Common.Utils.Patching;
using System.Threading.Tasks;
using System.Threading;
using HttpRequest = GClass1121;
using Location = GClass732.GClass734;
using LocationMatch = GClass732.GClass733;

namespace EmuTarkov.SinglePlayer.Patches
{
	public class LocationLootPatch : AbstractPatch
	{
		public static MethodBase resultMethod = null;
		public static PropertyInfo locationProperty = null;

		public LocationLootPatch()
		{
			methodName = "method_5";
			flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
		}

		public override void Apply()
		{
			PatcherUtil.PatchPrefix<LocationLootPatch>(TargetMethod());
		}

		public override MethodInfo TargetMethod()
		{
			var localGameBaseType = PatcherConstants.LocalGameType.BaseType;

			locationProperty = localGameBaseType.GetProperty("GClass734_0", BindingFlags.NonPublic | BindingFlags.Instance);
			resultMethod = localGameBaseType.GetMethod("smethod_3", BindingFlags.NonPublic | BindingFlags.Static);
			return localGameBaseType.GetMethod(methodName, flags);
		}

		/// <summary>
		/// Loads loot from EmuTarkov's server.
		/// Fallsback to the client's local location loot if it fails.
		/// </summary>
		public static bool Prefix(object __instance, string backendUrl, ref Task<Location> __result)
		{
			if (__instance.GetType() != PatcherConstants.LocalGameType)
			{
				// online match
				return true;
			}

			var location = (Location)locationProperty.GetValue(__instance);
			var locationLoot = HttpRequest.Download(backendUrl + "/api/location/" + location.Id, CancellationToken.None);

			if (locationLoot == null)
			{
				// failed to download loot
				return true;
			}

			__result = (Task<Location>)resultMethod.Invoke(null, new[] { new LocationMatch() {
				BackendUrl = backendUrl,
				Location = locationLoot
			}});

			return false;
		}
	}
}