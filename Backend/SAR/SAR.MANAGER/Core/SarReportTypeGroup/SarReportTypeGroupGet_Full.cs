using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarReportTypeGroup.Get.Ev;
using SAR.MANAGER.Core.SarReportTypeGroup.Get.ListEv;
using SAR.MANAGER.Core.SarReportTypeGroup.Get.ListV;
using SAR.MANAGER.Core.SarReportTypeGroup.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportTypeGroup
{
    partial class SarReportTypeGroupGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SarReportTypeGroupGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SAR_REPORT_TYPE_GROUP>))
                {
                    ISarReportTypeGroupGetListEv behavior = SarReportTypeGroupGetListEvBehaviorFactory.MakeISarReportTypeGroupGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SAR_REPORT_TYPE_GROUP))
                {
                    ISarReportTypeGroupGetEv behavior = SarReportTypeGroupGetEvBehaviorFactory.MakeISarReportTypeGroupGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SAR_REPORT_TYPE_GROUP>))
                {
                    ISarReportTypeGroupGetListV behavior = SarReportTypeGroupGetListVBehaviorFactory.MakeISarReportTypeGroupGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SAR_REPORT_TYPE_GROUP))
                {
                    ISarReportTypeGroupGetV behavior = SarReportTypeGroupGetVBehaviorFactory.MakeISarReportTypeGroupGetV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }
    }
}
