using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarPrint.Get.Ev;
using SAR.MANAGER.Core.SarPrint.Get.ListEv;
using SAR.MANAGER.Core.SarPrint.Get.ListV;
using SAR.MANAGER.Core.SarPrint.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrint
{
    partial class SarPrintGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SarPrintGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SAR_PRINT>))
                {
                    ISarPrintGetListEv behavior = SarPrintGetListEvBehaviorFactory.MakeISarPrintGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SAR_PRINT))
                {
                    ISarPrintGetEv behavior = SarPrintGetEvBehaviorFactory.MakeISarPrintGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SAR_PRINT>))
                {
                    ISarPrintGetListV behavior = SarPrintGetListVBehaviorFactory.MakeISarPrintGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SAR_PRINT))
                {
                    ISarPrintGetV behavior = SarPrintGetVBehaviorFactory.MakeISarPrintGetV(param, entity);
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
