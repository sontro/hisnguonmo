using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarPrintTypeCfg;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.Check
{
    class SarPrintTypeCfgCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SAR_PRINT_TYPE_CFG raw = new SarPrintTypeCfgBO().Get<SAR_PRINT_TYPE_CFG>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SAR_PRINT_TYPE_CFG raw)
        {
            bool result = true;
            try
            {
                raw = new SarPrintTypeCfgBO().Get<SAR_PRINT_TYPE_CFG>(id);
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
