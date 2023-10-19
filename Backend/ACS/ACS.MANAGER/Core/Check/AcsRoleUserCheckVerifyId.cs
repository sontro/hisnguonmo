using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsRoleUser;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.Check
{
    class AcsRoleUserCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                ACS_ROLE_USER raw = new AcsRoleUserBO().Get<ACS_ROLE_USER>(id);
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

        internal static bool Verify(CommonParam param, long id, ref ACS_ROLE_USER raw)
        {
            bool result = true;
            try
            {
                raw = new AcsRoleUserBO().Get<ACS_ROLE_USER>(id);
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
