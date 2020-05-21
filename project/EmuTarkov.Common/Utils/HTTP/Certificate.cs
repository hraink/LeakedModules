using UnityEngine.Networking;

namespace EmuTarkov.Common.Utils.HTTP
{
	public class Certificate : CertificateHandler
	{
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}
}
