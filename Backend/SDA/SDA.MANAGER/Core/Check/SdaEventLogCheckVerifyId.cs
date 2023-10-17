using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaEventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaEventLogCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_EVENT_LOG raw = new SdaEventLogBO().Get<SDA_EVENT_LOG>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_EVENT_LOG raw)
        {
            bool result = true;
            try
            {
                raw = new SdaEventLogBO().Get<SDA_EVENT_LOG>(id);
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
