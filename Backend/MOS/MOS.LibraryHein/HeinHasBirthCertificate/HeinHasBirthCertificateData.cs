
namespace MOS.LibraryHein.Bhyt.HeinHasBirthCertificate
{
    public class HeinHasBirthCertificateData
    {
        public string HeinHasBirthCertificateName { get; set; }
        public string HeinHasBirthCertificateCode { get; set; }

        public HeinHasBirthCertificateData()
        {
        }

        public HeinHasBirthCertificateData(string heinHasBirthCertificateCode, string heinHasBirthCertificateName)
        {
            HeinHasBirthCertificateName = heinHasBirthCertificateName;
            HeinHasBirthCertificateCode = heinHasBirthCertificateCode;
        }
    }
}
