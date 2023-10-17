using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaHideControl.Get.Ev;
using SDA.MANAGER.Core.SdaHideControl.Get.ListEv;
using SDA.MANAGER.Core.SdaHideControl.Get.ListV;
using SDA.MANAGER.Core.SdaHideControl.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaHideControl
{
    partial class SdaHideControlGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaHideControlGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_HIDE_CONTROL>))
                {
                    ISdaHideControlGetListEv behavior = SdaHideControlGetListEvBehaviorFactory.MakeISdaHideControlGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_HIDE_CONTROL))
                {
                    ISdaHideControlGetEv behavior = SdaHideControlGetEvBehaviorFactory.MakeISdaHideControlGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SDA_HIDE_CONTROL>))
                {
                    ISdaHideControlGetListV behavior = SdaHideControlGetListVBehaviorFactory.MakeISdaHideControlGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SDA_HIDE_CONTROL))
                {
                    ISdaHideControlGetV behavior = SdaHideControlGetVBehaviorFactory.MakeISdaHideControlGetV(param, entity);
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
