using System.Text;
using UnityEngine.Networking;
using ComponentAce.Compression.Libs.zlib;

namespace EmuTarkov.Common.Utils.HTTP
{
	public class HttpUtils
	{
		private static string host;
		private static string session;

		public HttpUtils(string backendUrl, string phpSession = null)
		{
			host = backendUrl;
			session = phpSession;
		}

		public void Send(string url, string json = "", bool compression = false)
		{
			string requestMethod = (string.IsNullOrEmpty(json)) ? UnityWebRequest.kHttpVerbGET : UnityWebRequest.kHttpVerbPUT;

			UnityWebRequest request = new UnityWebRequest(host + url, requestMethod)
			{
				certificateHandler = new Certificate(),
				downloadHandler = new DownloadHandlerBuffer()
			};

			if (!string.IsNullOrEmpty(json))
			{
				byte[] data = compression ? SimpleZlib.CompressToBytes(json, 9) : Encoding.UTF8.GetBytes(json);

				request.uploadHandler = new UploadHandlerRaw(data);
				request.SetRequestHeader("Content-Type", "application/json");
			}

			if (!string.IsNullOrEmpty(session))
			{
				request.SetRequestHeader("Cookie", $"PHPSESSID={session}");
				request.SetRequestHeader("SessionId", session);
			}

			request.SendWebRequest();
		}
	}

	public abstract class ServerResponse<T>
	{
		public int err;
		public string errmsg;
		public T data;
		public uint crc;
	}
}