using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.Check
{
    class SarPrintTypeCfgCheckVerifyValidData
    {
        internal static bool Verify(CommonParam param, SAR_PRINT_TYPE_CFG data)
        {
            bool result = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
            }
            catch (ArgumentNullException ex)
            {
                SAR.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal static bool Verify(CommonParam param, List<SAR_PRINT_TYPE_CFG> datas)
        {
            bool result = true;
            try
            {
                if (datas == null) throw new ArgumentNullException("datas");
                foreach (var data in datas)
                {
                    result = result && Verify(param, data);
                    if (!result) break;
                }
            }
            catch (ArgumentNullException ex)
            {
                SAR.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = false;
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
