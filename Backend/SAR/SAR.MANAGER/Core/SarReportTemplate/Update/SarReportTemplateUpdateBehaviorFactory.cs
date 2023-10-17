using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using SAR.SDO;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportTemplate.Update
{
    class SarReportTemplateUpdateBehaviorFactory
    {
        internal static ISarReportTemplateUpdate MakeISarReportTemplateUpdate(CommonParam param, object data)
        {
            ISarReportTemplateUpdate result = null;
            try
            {
                if (data.GetType() == typeof(SAR_REPORT_TEMPLATE))
                {
                    result = new SarReportTemplateUpdateBehaviorEv(param, (SAR_REPORT_TEMPLATE)data);
                }
                else if (data.GetType() == typeof(List<SAR_REPORT_TEMPLATE>))
                {
                    result = new SarReportTemplateUpdateListBehaviorEv(param, (List<SAR_REPORT_TEMPLATE>)data);
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
