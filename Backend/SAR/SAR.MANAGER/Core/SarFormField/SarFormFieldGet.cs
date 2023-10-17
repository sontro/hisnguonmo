using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarFormField.Get.Ev;
using SAR.MANAGER.Core.SarFormField.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarFormField
{
    partial class SarFormFieldGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SarFormFieldGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SAR_FORM_FIELD>))
                {
                    ISarFormFieldGetListEv behavior = SarFormFieldGetListEvBehaviorFactory.MakeISarFormFieldGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SAR_FORM_FIELD))
                {
                    ISarFormFieldGetEv behavior = SarFormFieldGetEvBehaviorFactory.MakeISarFormFieldGetEv(param, entity);
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
