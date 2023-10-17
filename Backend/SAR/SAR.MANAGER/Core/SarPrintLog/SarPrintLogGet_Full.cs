using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarPrintLog.Get.Ev;
using SAR.MANAGER.Core.SarPrintLog.Get.ListEv;
using SAR.MANAGER.Core.SarPrintLog.Get.ListV;
using SAR.MANAGER.Core.SarPrintLog.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintLog
{
    partial class SarPrintLogGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SarPrintLogGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SAR_PRINT_LOG>))
                {
                    ISarPrintLogGetListEv behavior = SarPrintLogGetListEvBehaviorFactory.MakeISarPrintLogGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SAR_PRINT_LOG))
                {
                    ISarPrintLogGetEv behavior = SarPrintLogGetEvBehaviorFactory.MakeISarPrintLogGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SAR_PRINT_LOG>))
                {
                    ISarPrintLogGetListV behavior = SarPrintLogGetListVBehaviorFactory.MakeISarPrintLogGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SAR_PRINT_LOG))
                {
                    ISarPrintLogGetV behavior = SarPrintLogGetVBehaviorFactory.MakeISarPrintLogGetV(param, entity);
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
