using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaTranslate;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaTranslateCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_TRANSLATE raw = new SdaTranslateBO().Get<SDA_TRANSLATE>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_TRANSLATE raw)
        {
            bool result = true;
            try
            {
                raw = new SdaTranslateBO().Get<SDA_TRANSLATE>(id);
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
