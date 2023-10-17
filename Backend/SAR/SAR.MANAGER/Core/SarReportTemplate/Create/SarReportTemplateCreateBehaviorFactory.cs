using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;
using SAR.SDO;

namespace SAR.MANAGER.Core.SarReportTemplate.Create
{
    class SarReportTemplateCreateBehaviorFactory
    {
        internal static ISarReportTemplateCreate MakeISarReportTemplateCreate(CommonParam param, object data)
        {
            ISarReportTemplateCreate result = null;
            try
            {
                if (data.GetType() == typeof(SAR_REPORT_TEMPLATE))
                {
                    result = new SarReportTemplateCreateBehaviorEv(param, (SAR_REPORT_TEMPLATE)data);
                }
                //else if (data.GetType() == typeof(SarReportTemplateSDO))
                //{
                //    result = new SarReportTemplateCreateBehaviorSDO(param, (SarReportTemplateSDO)data);
                //}
                else if (data.GetType() == typeof(List<SAR_REPORT_TEMPLATE>))
                {
                    result = new SarReportTemplateCreateBehaviorListEv(param, (List<SAR_REPORT_TEMPLATE>)data);
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
