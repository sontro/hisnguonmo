using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaHideControl;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaHideControlCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_HIDE_CONTROL raw = new SdaHideControlBO().Get<SDA_HIDE_CONTROL>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_HIDE_CONTROL raw)
        {
            bool result = true;
            try
            {
                raw = new SdaHideControlBO().Get<SDA_HIDE_CONTROL>(id);
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
