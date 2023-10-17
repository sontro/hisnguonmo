using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportStt.Create
{
    class SarReportSttCreateBehaviorFactory
    {
        internal static ISarReportSttCreate MakeISarReportSttCreate(CommonParam param, object data)
        {
            ISarReportSttCreate result = null;
            try
            {
                if (data.GetType() == typeof(SAR_REPORT_STT))
                {
                    result = new SarReportSttCreateBehaviorEv(param, (SAR_REPORT_STT)data);
                }
                else if (data.GetType() == typeof(List<SAR_REPORT_STT>))
                {
                    result = new SarReportSttCreateBehaviorListEv(param, (List<SAR_REPORT_STT>)data);
                }
                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__FactoryKhoiTaoDoiTuongThatBai);
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
                result = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
