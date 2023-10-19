using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsActivityLog;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.Check
{
    class AcsActivityLogCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                ACS_ACTIVITY_LOG raw = new AcsActivityLogBO().Get<ACS_ACTIVITY_LOG>(id);
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

        internal static bool Verify(CommonParam param, long id, ref ACS_ACTIVITY_LOG raw)
        {
            bool result = true;
            try
            {
                raw = new AcsActivityLogBO().Get<ACS_ACTIVITY_LOG>(id);
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
