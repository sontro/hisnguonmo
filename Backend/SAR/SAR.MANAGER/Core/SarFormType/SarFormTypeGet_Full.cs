using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarFormType.Get.Ev;
using SAR.MANAGER.Core.SarFormType.Get.ListEv;
using SAR.MANAGER.Core.SarFormType.Get.ListV;
using SAR.MANAGER.Core.SarFormType.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarFormType
{
    partial class SarFormTypeGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SarFormTypeGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SAR_FORM_TYPE>))
                {
                    ISarFormTypeGetListEv behavior = SarFormTypeGetListEvBehaviorFactory.MakeISarFormTypeGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SAR_FORM_TYPE))
                {
                    ISarFormTypeGetEv behavior = SarFormTypeGetEvBehaviorFactory.MakeISarFormTypeGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SAR_FORM_TYPE>))
                {
                    ISarFormTypeGetListV behavior = SarFormTypeGetListVBehaviorFactory.MakeISarFormTypeGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SAR_FORM_TYPE))
                {
                    ISarFormTypeGetV behavior = SarFormTypeGetVBehaviorFactory.MakeISarFormTypeGetV(param, entity);
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
