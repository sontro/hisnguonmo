using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaConfigAppUser;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaConfigAppUserCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_CONFIG_APP_USER raw = new SdaConfigAppUserBO().Get<SDA_CONFIG_APP_USER>(id);
                if (raw == null)
                {
                    result = false;
                    SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_CONFIG_APP_USER raw)
        {
            bool result = true;
            try
            {
                raw = new SdaConfigAppUserBO().Get<SDA_CONFIG_APP_USER>(id);
                if (raw == null)
                {
                    result = false;
                    SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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
