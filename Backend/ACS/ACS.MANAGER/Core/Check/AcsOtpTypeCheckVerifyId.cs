using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsOtpType;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.Check
{
    class AcsOtpTypeCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                ACS_OTP_TYPE raw = new AcsOtpTypeBO().Get<ACS_OTP_TYPE>(id);
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

        internal static bool Verify(CommonParam param, long id, ref ACS_OTP_TYPE raw)
        {
            bool result = true;
            try
            {
                raw = new AcsOtpTypeBO().Get<ACS_OTP_TYPE>(id);
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
