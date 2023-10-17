using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarPrintTypeCfg.Get.Ev;
using SAR.MANAGER.Core.SarPrintTypeCfg.Get.ListEv;
using SAR.MANAGER.Core.SarPrintTypeCfg.Get.ListV;
using SAR.MANAGER.Core.SarPrintTypeCfg.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintTypeCfg
{
    partial class SarPrintTypeCfgGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SarPrintTypeCfgGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SAR_PRINT_TYPE_CFG>))
                {
                    ISarPrintTypeCfgGetListEv behavior = SarPrintTypeCfgGetListEvBehaviorFactory.MakeISarPrintTypeCfgGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SAR_PRINT_TYPE_CFG))
                {
                    ISarPrintTypeCfgGetEv behavior = SarPrintTypeCfgGetEvBehaviorFactory.MakeISarPrintTypeCfgGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SAR_PRINT_TYPE_CFG>))
                {
                    ISarPrintTypeCfgGetListV behavior = SarPrintTypeCfgGetListVBehaviorFactory.MakeISarPrintTypeCfgGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SAR_PRINT_TYPE_CFG))
                {
                    ISarPrintTypeCfgGetV behavior = SarPrintTypeCfgGetVBehaviorFactory.MakeISarPrintTypeCfgGetV(param, entity);
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
