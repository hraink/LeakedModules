using System.Collections.Generic;

namespace EmuTarkov.RuntimeBundles.Utils.Bundles
{
	public static class BundlesUtil
	{
		public static List<Manifest> Manifests;

		static BundlesUtil()
		{
			Manifests = new List<Manifest>();
		}
	}
}
