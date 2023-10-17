using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaConfigApp;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaConfigAppCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_CONFIG_APP raw = new SdaConfigAppBO().Get<SDA_CONFIG_APP>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_CONFIG_APP raw)
        {
            bool result = true;
            try
            {
                raw = new SdaConfigAppBO().Get<SDA_CONFIG_APP>(id);
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
