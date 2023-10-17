using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.LibraryHein.Bhyt.HeinHasBirthCertificate
{
    public class HeinHasBirthCertificateStore
    {
        private static readonly Dictionary<string, HeinHasBirthCertificateData> HEIN_HAS_BIRTH_CERTIFICATE_STORE = new Dictionary<string, HeinHasBirthCertificateData>()
        {
            {HeinHasBirthCertificateCode.TRUE, new HeinHasBirthCertificateData(HeinHasBirthCertificateCode.TRUE, "Có chứng sinh")},
            {HeinHasBirthCertificateCode.FALSE, new HeinHasBirthCertificateData(HeinHasBirthCertificateCode.FALSE, "Không có chứng sinh")}
        };

        public static List<HeinHasBirthCertificateData> Get()
        {
            try
            {
                return HEIN_HAS_BIRTH_CERTIFICATE_STORE.Values.ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static HeinHasBirthCertificateData GetByCode(string code)
        {
            try
            {
                return code != null && HEIN_HAS_BIRTH_CERTIFICATE_STORE.ContainsKey(code) ? HEIN_HAS_BIRTH_CERTIFICATE_STORE[code] : null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static bool IsValidCode(string code)
        {
            if (GetByCode(code) == null)
            {
                LogSystem.Error("Ma 'Chứng sinh' khong hop le");
                return false;
            }
            return true;
        }
    }
}
