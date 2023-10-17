using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarUserReportType.Get.Ev;
using SAR.MANAGER.Core.SarUserReportType.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;
using SAR.MANAGER.Core.SarUserReportType.Get.ListV;
using SAR.MANAGER.Core.SarUserReportType.Get.V;

namespace SAR.MANAGER.Core.SarUserReportType
{
    partial class SarUserReportTypeGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SarUserReportTypeGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SAR_USER_REPORT_TYPE>))
                {
                    ISarUserReportTypeGetListEv behavior = SarUserReportTypeGetListEvBehaviorFactory.MakeISarUserReportTypeGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SAR_USER_REPORT_TYPE))
                {
                    ISarUserReportTypeGetEv behavior = SarUserReportTypeGetEvBehaviorFactory.MakeISarUserReportTypeGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                if (typeof(T) == typeof(List<V_SAR_USER_REPORT_TYPE>))
                {
                    ISarUserReportTypeGetListV behavior = SarUserReportTypeGetListVBehaviorFactory.MakeISarUserReportTypeGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SAR_USER_REPORT_TYPE))
                {
                    ISarUserReportTypeGetV behavior = SarUserReportTypeGetVBehaviorFactory.MakeISarUserReportTypeGetV(param, entity);
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
