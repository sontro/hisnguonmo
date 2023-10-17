using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarReportTypeGroup;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.Check
{
    class SarReportTypeGroupCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SAR_REPORT_TYPE_GROUP raw = new SarReportTypeGroupBO().Get<SAR_REPORT_TYPE_GROUP>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SAR_REPORT_TYPE_GROUP raw)
        {
            bool result = true;
            try
            {
                raw = new SarReportTypeGroupBO().Get<SAR_REPORT_TYPE_GROUP>(id);
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
