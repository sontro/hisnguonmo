using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsRoleUser.Get.Ev;
using ACS.MANAGER.Core.AcsRoleUser.Get.ListEv;
using ACS.MANAGER.Core.AcsRoleUser.Get.ListV;
using ACS.MANAGER.Core.AcsRoleUser.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser
{
    partial class AcsRoleUserGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsRoleUserGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_ROLE_USER>))
                {
                    IAcsRoleUserGetListEv behavior = AcsRoleUserGetListEvBehaviorFactory.MakeIAcsRoleUserGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_ROLE_USER))
                {
                    IAcsRoleUserGetEv behavior = AcsRoleUserGetEvBehaviorFactory.MakeIAcsRoleUserGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_ACS_ROLE_USER>))
                {
                    IAcsRoleUserGetListV behavior = AcsRoleUserGetListVBehaviorFactory.MakeIAcsRoleUserGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_ACS_ROLE_USER))
                {
                    IAcsRoleUserGetV behavior = AcsRoleUserGetVBehaviorFactory.MakeIAcsRoleUserGetV(param, entity);
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
