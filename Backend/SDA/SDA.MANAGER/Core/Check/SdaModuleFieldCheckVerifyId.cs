using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaModuleField;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaModuleFieldCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_MODULE_FIELD raw = new SdaModuleFieldBO().Get<SDA_MODULE_FIELD>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_MODULE_FIELD raw)
        {
            bool result = true;
            try
            {
                raw = new SdaModuleFieldBO().Get<SDA_MODULE_FIELD>(id);
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
