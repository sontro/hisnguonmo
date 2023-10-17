using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarForm.Get.Ev;
using SAR.MANAGER.Core.SarForm.Get.ListEv;
using SAR.MANAGER.Core.SarForm.Get.ListV;
using SAR.MANAGER.Core.SarForm.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarForm
{
    partial class SarFormGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SarFormGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SAR_FORM>))
                {
                    ISarFormGetListEv behavior = SarFormGetListEvBehaviorFactory.MakeISarFormGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SAR_FORM))
                {
                    ISarFormGetEv behavior = SarFormGetEvBehaviorFactory.MakeISarFormGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SAR_FORM>))
                {
                    ISarFormGetListV behavior = SarFormGetListVBehaviorFactory.MakeISarFormGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SAR_FORM))
                {
                    ISarFormGetV behavior = SarFormGetVBehaviorFactory.MakeISarFormGetV(param, entity);
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
