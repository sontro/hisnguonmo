using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsUser.Get.Ev;
using ACS.MANAGER.Core.AcsUser.Get.ListEv;
using ACS.MANAGER.Core.AcsUser.Get.ListV;
using ACS.MANAGER.Core.AcsUser.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsUserGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_USER>))
                {
                    IAcsUserGetListEv behavior = AcsUserGetListEvBehaviorFactory.MakeIAcsUserGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_USER))
                {
                    IAcsUserGetEv behavior = AcsUserGetEvBehaviorFactory.MakeIAcsUserGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_ACS_USER>))
                {
                    IAcsUserGetListV behavior = AcsUserGetListVBehaviorFactory.MakeIAcsUserGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_ACS_USER))
                {
                    IAcsUserGetV behavior = AcsUserGetVBehaviorFactory.MakeIAcsUserGetV(param, entity);
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
