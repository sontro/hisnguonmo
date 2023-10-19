using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsApplication.Get.Ev;
using ACS.MANAGER.Core.AcsApplication.Get.ListEv;
using ACS.MANAGER.Core.AcsApplication.Get.ListV;
using ACS.MANAGER.Core.AcsApplication.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplication
{
    partial class AcsApplicationGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsApplicationGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_APPLICATION>))
                {
                    IAcsApplicationGetListEv behavior = AcsApplicationGetListEvBehaviorFactory.MakeIAcsApplicationGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_APPLICATION))
                {
                    IAcsApplicationGetEv behavior = AcsApplicationGetEvBehaviorFactory.MakeIAcsApplicationGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_ACS_APPLICATION>))
                {
                    IAcsApplicationGetListV behavior = AcsApplicationGetListVBehaviorFactory.MakeIAcsApplicationGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_ACS_APPLICATION))
                {
                    IAcsApplicationGetV behavior = AcsApplicationGetVBehaviorFactory.MakeIAcsApplicationGetV(param, entity);
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
