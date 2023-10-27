using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsModuleGroup;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.Check
{
    class AcsModuleGroupCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                ACS_MODULE_GROUP raw = new AcsModuleGroupBO().Get<ACS_MODULE_GROUP>(id);
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

        internal static bool Verify(CommonParam param, long id, ref ACS_MODULE_GROUP raw)
        {
            bool result = true;
            try
            {
                raw = new AcsModuleGroupBO().Get<ACS_MODULE_GROUP>(id);
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
