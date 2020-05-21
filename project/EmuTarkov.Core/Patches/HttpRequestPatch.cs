using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using Comfort.Common;
using EmuTarkov.Common.Utils.HTTP;
using EmuTarkov.Common.Utils.Patching;

namespace EmuTarkov.Core.Patches
{
	public class HttpRequestPatch : AbstractPatch
	{
		public const string HTTPRequest = "Class135";
		public const string HTTPHandler = "Class136";

		public HttpRequestPatch()
		{
			methodName = "smethod_0";
			flags = BindingFlags.NonPublic | BindingFlags.Static;
		}

		public override void Apply()
		{
			PatcherUtil.PatchPrefix<HttpRequestPatch>(TargetMethod());
		}

		public override MethodInfo TargetMethod()
		{
			return PatcherConstants.TargetAssembly
				.GetTypes().Single(x => x.Name == HTTPRequest).GetNestedTypes(BindingFlags.NonPublic)
				.Single(x => x.Name == HTTPHandler).GetMethod(methodName, flags);
		}

		public static bool Prefix(string url, Callback<Texture2D> callback)
		{
			Result<Texture2D> result = default(Result<Texture2D>);
			UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url);
	
			unityWebRequest.certificateHandler = new Certificate();
			unityWebRequest.timeout = 1000;

			UnityWebRequestAsyncOperation request = unityWebRequest.SendWebRequest();
			Action<AsyncOperation> requestCompleted = null;

			requestCompleted = ao =>
			{
				request.completed -= requestCompleted;

				if (!unityWebRequest.isNetworkError && !unityWebRequest.isHttpError)
				{
					result.Value = DownloadHandlerTexture.GetContent(unityWebRequest);

					if (result.Value == null)
					{
						result.Error = "texture is null";
					}
					else
					{
						result.Value.name = url.Substring(url.LastIndexOf("/", StringComparison.InvariantCulture) + 1);
					}
				}
				else
				{
					result.Error = "cant load " + url + " because of error " + unityWebRequest.error;
				}

				unityWebRequest.Dispose();
				callback.Invoke(result);
			};

			request.completed += requestCompleted;

			return false;
		}
	}
}
