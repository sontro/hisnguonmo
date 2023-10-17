using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaCustomizeUi;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaCustomizeUiCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_CUSTOMIZE_UI raw = new SdaCustomizeUiBO().Get<SDA_CUSTOMIZE_UI>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_CUSTOMIZE_UI raw)
        {
            bool result = true;
            try
            {
                raw = new SdaCustomizeUiBO().Get<SDA_CUSTOMIZE_UI>(id);
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
