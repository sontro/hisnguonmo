using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.Check
{
    class SarRetyFofiCheckVerifyValidData
    {
        internal static bool Verify(CommonParam param, SAR_RETY_FOFI data)
        {
            bool result = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.REPORT_TYPE_ID <= 0) throw new ArgumentNullException("REPORT_TYPE_ID");
                if (data.FORM_FIELD_ID <= 0) throw new ArgumentNullException("FORM_FIELD_ID");
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

        internal static bool Verify(CommonParam param, List<SAR_RETY_FOFI> datas)
        {
            bool result = true;
            try
            {
                if (datas == null) throw new ArgumentNullException("datas");
                foreach (var data in datas)
                {
                    result = result && Verify(param, data);
                    if (!result)
                    {
                        break;
                    }
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
