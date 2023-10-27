using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.Check
{
    class AcsAppOtpTypeCheckVerifyValidData
    {
        internal static bool Verify(CommonParam param, ACS_APP_OTP_TYPE data)
        {
            bool result = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
            }
            catch (ArgumentNullException ex)
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
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

        internal static bool Verify(CommonParam param, List<ACS_APP_OTP_TYPE> datas)
        {
            bool result = true;
            try
            {
                if (datas == null) throw new ArgumentNullException("datas");
                foreach (var data in datas)
                {
                    if (data.APPLICATION_ID == 0 || data.OTP_TYPE_ID == 0)
                    {
                        throw new ArgumentNullException("APPLICATION_ID or OTP_TYPE_ID is null");
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                ACS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__ThieuThongTinBatBuoc);
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
