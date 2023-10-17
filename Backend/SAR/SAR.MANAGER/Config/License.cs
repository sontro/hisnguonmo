using SDA.EFMODEL.DataModels;
using System;
using SAR.MANAGER.Base;

namespace SAR.MANAGER.Config
{
    public class License : BusinessBase
    {
        private const string decryptRsaKey = "<RSAKeyValue><Modulus>vx1J3M/TcESXJw2Jv8G43ZUHdFy26qS+r5Bsfx2QOQmPgn0P5WxmIh6zMKlBB0s/Wuhrqo5mqchmPWg7JUkViijz8sEpmxl8EkMxiYDWWDfY9w5JXgp28Kz7WpMDihhHdmWDUo5VoMkwO41Yn8F94Gx02JyFgKSsJNulGomRyU8=</Modulus><Exponent>AQAB</Exponent><P>/UjErI/thMB6dS+OfH6pfUCJLQuMkIIWkFenX+qHlSpqgaIrdARGXbobXT8JNU4Ow2lU8H7y/4cj8FBYIwv74Q==</P><Q>wSnfTac/C8kQJyqzkxakDA2tIFSR1oblF5r9bWYKF7l2CGt3spoWWnkT2CTd5dUcL64GMO1MI4C3RkUrFwfrLw==</Q><DP>goJBXQ1YfMPVFZbom3uEh9Z+GGjQCBIP4FZaFxE4xYKymJNgQoqFW3wu53A4pW/QKZ6XtsgjG8mdmhE8KOre4Q==</DP><DQ>Rv5zwx+gDV3VoP+RxpLAsmloYwSSIU2s75MYZ9fkB7ozRn7xHGSDqLbtcziBkdJUrLWCMwNUQ4wwUBRl9O4FIw==</DQ><InverseQ>qRX7IyU28TuSAeUDyGkAE/fpH2kkwdy6cyENy8GioPTb5VSA2WtPUFQR25KOC32uxaz/UteEfxlhJlpo1K0Yiw==</InverseQ><D>lbePHqHdBU1mkFyFtig/966BOYJMdcN78rOUm3yAeH5p45Kvuk5SMJaWUB+35svnJQOBHQsTmBx7I84cz5fSe5zcEee/EiLMxMJKUhiCGKkl390AnS8du94mtafcDcqrKEHVjwUUSAl7A4mAGIuR8irSt7OQ1IWIO31ImoPWnUE=</D></RSAKeyValue>";
        private const string decryptAesKey = "᷻臏閳囀�䛹捺⷏첯瀞穸䡷퐕멟韦湘";
        private const string decryptAesIv = "졻엃菱萯⇨蹋件";
        private const string APP_CODE = "SAR";
        private static bool validLicense = false;
        public static bool ValidLicense
        {
            get
            {
                return validLicense;
            }
        }

        public static void Check()
        {
            bool result = false;
            try
            {
                string snhdd = Inventec.Common.HardDrive.NetworkCard.GetMacAddress();
                string puk = System.Configuration.ConfigurationManager.AppSettings["License.PUK"];
                long time = long.Parse(System.DateTime.Now.ToString("yyyyMMddHHmmss"));

                if (String.IsNullOrWhiteSpace(snhdd) || String.IsNullOrWhiteSpace(puk) || time == 0)
                {
                    throw new Exception();
                }

                SDA.Filter.SdaLicenseFilter filter = new SDA.Filter.SdaLicenseFilter();
                filter.Time = time;
                filter.PUK = puk;
                filter.SN_HDD = snhdd;
                filter.APP_CODE = APP_CODE;

                var aro = new Inventec.Common.WebApiClient.ApiConsumer(System.Configuration.ConfigurationManager.AppSettings["License.Consumer.Uri"], APP_CODE).Get<Inventec.Core.ApiResultObject<SDA_LICENSE>>(System.Configuration.ConfigurationManager.AppSettings["License.Consumer.Uri.GetLast"], null, filter); //System.Configuration

                if (aro != null && aro.Data != null)
                {
                    SDA_LICENSE license = aro.Data;
                    string licenseDecrypt = Inventec.Common.LicenseCrypto.Execute.Decrypt(license.LICENSE, decryptRsaKey, decryptAesKey, decryptAesIv);
                    if (String.IsNullOrEmpty(licenseDecrypt)) throw new LicenseWrongDataException("Khong decrypt duoc license." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => licenseDecrypt), licenseDecrypt));
                    string[] licenseParam = licenseDecrypt.Split(',');
                    if (licenseParam.Length < 5) throw new LicenseWrongDataException("So luong param khong hop le (<5)." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => licenseDecrypt), licenseDecrypt));

                    ///Kiem tra thoi han hieu luc cung nhu thong tin so voi license (tranh fake thong tin khi lay license)
                    long? now = Inventec.Common.DateTime.Get.Now();
                    if (!now.HasValue || now.Value > long.Parse(licenseParam[3])) throw new LicenseExpireException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => now), now) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => licenseDecrypt), licenseDecrypt));
                    if (puk != licenseParam[1]) throw new LicenseWrongDataException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => puk), puk) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => licenseDecrypt), licenseDecrypt));
                    if (snhdd != licenseParam[2]) throw new LicenseWrongDataException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => snhdd), snhdd) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => licenseDecrypt), licenseDecrypt));
                    if (APP_CODE != licenseParam[0]) throw new LicenseWrongDataException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => APP_CODE), APP_CODE) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => licenseDecrypt), licenseDecrypt));
                    result = true;
                }
                else
                {
                    throw new LicenseNotFoundException("Khong lay duoc thong tin license tu server." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => time), time) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => puk), puk) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => snhdd), snhdd) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => APP_CODE), APP_CODE) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => aro), aro));
                }
            }
            catch (LicenseNotFoundException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("License khong hop le (khong tim thay).", ex);
                result = false;
            }
            catch (LicenseWrongDataException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("License khong hop le (du lieu loi).", ex);
                result = false;
            }
            catch (LicenseExpireException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("License khong hop le (het han).", ex);
                result = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error("License khong hop le (co exception).", ex);
                result = false;
            }

            if (result)
            {
                Inventec.Common.Logging.LogSystem.Info("License hop le.");
            }
            else
            {
                Inventec.Common.Logging.LogSystem.Info("License khong hop le.");
            }
            validLicense = result;
        }

        class LicenseWrongDataException : Exception { internal LicenseWrongDataException() { } internal LicenseWrongDataException(string message) : base(message) { } }
        class LicenseNotFoundException : Exception { internal LicenseNotFoundException() { } internal LicenseNotFoundException(string message) : base(message) { } }
        class LicenseExpireException : Exception { internal LicenseExpireException() { } internal LicenseExpireException(string message) : base(message) { } }
    }
}
