using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaLanguage;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaLanguageCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_LANGUAGE raw = new SdaLanguageBO().Get<SDA_LANGUAGE>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_LANGUAGE raw)
        {
            bool result = true;
            try
            {
                raw = new SdaLanguageBO().Get<SDA_LANGUAGE>(id);
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
