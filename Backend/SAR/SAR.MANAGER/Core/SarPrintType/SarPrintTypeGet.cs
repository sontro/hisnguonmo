using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarPrintType.Get.Ev;
using SAR.MANAGER.Core.SarPrintType.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintType
{
    partial class SarPrintTypeGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SarPrintTypeGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SAR_PRINT_TYPE>))
                {
                    ISarPrintTypeGetListEv behavior = SarPrintTypeGetListEvBehaviorFactory.MakeISarPrintTypeGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SAR_PRINT_TYPE))
                {
                    ISarPrintTypeGetEv behavior = SarPrintTypeGetEvBehaviorFactory.MakeISarPrintTypeGetEv(param, entity);
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
