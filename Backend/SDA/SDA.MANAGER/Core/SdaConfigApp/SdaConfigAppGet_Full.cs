using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaConfigApp.Get.Ev;
using SDA.MANAGER.Core.SdaConfigApp.Get.ListEv;
using SDA.MANAGER.Core.SdaConfigApp.Get.ListV;
using SDA.MANAGER.Core.SdaConfigApp.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfigApp
{
    partial class SdaConfigAppGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaConfigAppGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_CONFIG_APP>))
                {
                    ISdaConfigAppGetListEv behavior = SdaConfigAppGetListEvBehaviorFactory.MakeISdaConfigAppGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_CONFIG_APP))
                {
                    ISdaConfigAppGetEv behavior = SdaConfigAppGetEvBehaviorFactory.MakeISdaConfigAppGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SDA_CONFIG_APP>))
                {
                    ISdaConfigAppGetListV behavior = SdaConfigAppGetListVBehaviorFactory.MakeISdaConfigAppGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SDA_CONFIG_APP))
                {
                    ISdaConfigAppGetV behavior = SdaConfigAppGetVBehaviorFactory.MakeISdaConfigAppGetV(param, entity);
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
