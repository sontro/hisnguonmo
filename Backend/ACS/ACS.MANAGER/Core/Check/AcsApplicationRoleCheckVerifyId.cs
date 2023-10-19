using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsApplicationRole;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.Check
{
    class AcsApplicationRoleCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                ACS_APPLICATION_ROLE raw = new AcsApplicationRoleBO().Get<ACS_APPLICATION_ROLE>(id);
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

        internal static bool Verify(CommonParam param, long id, ref ACS_APPLICATION_ROLE raw)
        {
            bool result = true;
            try
            {
                raw = new AcsApplicationRoleBO().Get<ACS_APPLICATION_ROLE>(id);
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
