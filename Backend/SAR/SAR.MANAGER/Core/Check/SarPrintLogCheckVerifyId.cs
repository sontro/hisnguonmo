using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarPrintLog;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.Check
{
    class SarPrintLogCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SAR_PRINT_LOG raw = new SarPrintLogBO().Get<SAR_PRINT_LOG>(id);
                if (raw == null)
                {
                    result = false;
                    SAR.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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

        internal static bool Verify(CommonParam param, long id, ref SAR_PRINT_LOG raw)
        {
            bool result = true;
            try
            {
                raw = new SarPrintLogBO().Get<SAR_PRINT_LOG>(id);
                if (raw == null)
                {
                    result = false;
                    SAR.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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
