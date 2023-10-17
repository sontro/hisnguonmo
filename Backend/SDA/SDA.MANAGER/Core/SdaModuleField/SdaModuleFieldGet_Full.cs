using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaModuleField.Get.Ev;
using SDA.MANAGER.Core.SdaModuleField.Get.ListEv;
using SDA.MANAGER.Core.SdaModuleField.Get.ListV;
using SDA.MANAGER.Core.SdaModuleField.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaModuleField
{
    partial class SdaModuleFieldGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaModuleFieldGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_MODULE_FIELD>))
                {
                    ISdaModuleFieldGetListEv behavior = SdaModuleFieldGetListEvBehaviorFactory.MakeISdaModuleFieldGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_MODULE_FIELD))
                {
                    ISdaModuleFieldGetEv behavior = SdaModuleFieldGetEvBehaviorFactory.MakeISdaModuleFieldGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SDA_MODULE_FIELD>))
                {
                    ISdaModuleFieldGetListV behavior = SdaModuleFieldGetListVBehaviorFactory.MakeISdaModuleFieldGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SDA_MODULE_FIELD))
                {
                    ISdaModuleFieldGetV behavior = SdaModuleFieldGetVBehaviorFactory.MakeISdaModuleFieldGetV(param, entity);
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
