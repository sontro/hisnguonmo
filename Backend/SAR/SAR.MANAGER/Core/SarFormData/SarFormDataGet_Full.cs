using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarFormData.Get.Ev;
using SAR.MANAGER.Core.SarFormData.Get.ListEv;
using SAR.MANAGER.Core.SarFormData.Get.ListV;
using SAR.MANAGER.Core.SarFormData.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarFormData
{
    partial class SarFormDataGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SarFormDataGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SAR_FORM_DATA>))
                {
                    ISarFormDataGetListEv behavior = SarFormDataGetListEvBehaviorFactory.MakeISarFormDataGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SAR_FORM_DATA))
                {
                    ISarFormDataGetEv behavior = SarFormDataGetEvBehaviorFactory.MakeISarFormDataGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SAR_FORM_DATA>))
                {
                    ISarFormDataGetListV behavior = SarFormDataGetListVBehaviorFactory.MakeISarFormDataGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SAR_FORM_DATA))
                {
                    ISarFormDataGetV behavior = SarFormDataGetVBehaviorFactory.MakeISarFormDataGetV(param, entity);
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
