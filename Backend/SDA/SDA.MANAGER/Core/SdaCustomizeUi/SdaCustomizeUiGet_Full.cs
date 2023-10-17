using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaCustomizeUi.Get.Ev;
using SDA.MANAGER.Core.SdaCustomizeUi.Get.ListEv;
using SDA.MANAGER.Core.SdaCustomizeUi.Get.ListV;
using SDA.MANAGER.Core.SdaCustomizeUi.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaCustomizeUi
{
    partial class SdaCustomizeUiGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaCustomizeUiGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_CUSTOMIZE_UI>))
                {
                    ISdaCustomizeUiGetListEv behavior = SdaCustomizeUiGetListEvBehaviorFactory.MakeISdaCustomizeUiGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_CUSTOMIZE_UI))
                {
                    ISdaCustomizeUiGetEv behavior = SdaCustomizeUiGetEvBehaviorFactory.MakeISdaCustomizeUiGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SDA_CUSTOMIZE_UI>))
                {
                    ISdaCustomizeUiGetListV behavior = SdaCustomizeUiGetListVBehaviorFactory.MakeISdaCustomizeUiGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SDA_CUSTOMIZE_UI))
                {
                    ISdaCustomizeUiGetV behavior = SdaCustomizeUiGetVBehaviorFactory.MakeISdaCustomizeUiGetV(param, entity);
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
