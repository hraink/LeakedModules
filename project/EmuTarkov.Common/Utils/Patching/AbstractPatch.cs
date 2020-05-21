using System.Reflection;

namespace EmuTarkov.Common.Utils.Patching
{
	public abstract class AbstractPatch
	{
		public string methodName;
		public BindingFlags flags;

		public abstract MethodInfo TargetMethod();
		public abstract void Apply();
	}
}
