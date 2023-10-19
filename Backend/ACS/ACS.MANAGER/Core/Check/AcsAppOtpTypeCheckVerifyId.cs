using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsAppOtpType;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.Check
{
    class AcsAppOtpTypeCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                ACS_APP_OTP_TYPE raw = new AcsAppOtpTypeBO().Get<ACS_APP_OTP_TYPE>(id);
                if (raw == null)
                {
                    result = false;
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal static bool Verify(CommonParam param, long id, ref ACS_APP_OTP_TYPE raw)
        {
            bool result = true;
            try
            {
                raw = new AcsAppOtpTypeBO().Get<ACS_APP_OTP_TYPE>(id);
                if (raw == null)
                {
                    result = false;
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                }
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
