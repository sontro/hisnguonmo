using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBranch;
using MOS.SDO;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.License
{
    public class VerifyLicenseCheck: BusinessBase
    {
        public class AppCode
        {
            public const string QR_PAYMENT = "00001";
            public const string ELECTRONIC_PAYMENT = "00003";
        }
        internal VerifyLicenseCheck()
            : base()
        {

        }

        internal VerifyLicenseCheck(CommonParam param)
            : base(param)
        {

        }
        internal bool VerifyLicense(string heinMediOrgCode, string code)
        {
            bool result = true;
            try
            {
                if (LicenseProcessor.LICENSE_LIST == null || LicenseProcessor.LICENSE_LIST.Count == 0)
                {
                    Inventec.Common.Logging.LogSystem.Error("LICENSE_LIST is null");
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.ChuaNhapMaKichHoatHoacMaHetHan);
                    return false;
                }

                if (String.IsNullOrWhiteSpace(heinMediOrgCode)) return result;

                long dateNow = Inventec.Common.TypeConvert.Parse.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
                if (dateNow == 0)
                {
                    dateNow = 99999999;
                }

                List<SdaLicenseSDO> licenses = LicenseProcessor.LICENSE_LIST != null ? LicenseProcessor.LICENSE_LIST.Where(o => (o.ExpiredDate >= dateNow) && (o.ClientCode == heinMediOrgCode) && (o.AppCode == code)).ToList() : null;
                if (licenses == null || licenses.Count == 0)
                {
                    Inventec.Common.Logging.LogSystem.Error("licenses is null");
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.ChuaNhapMaKichHoatHoacMaHetHan);
                    result = false;
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
