using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsControlRole.Get.Ev;
using ACS.MANAGER.Core.AcsControlRole.Get.ListEv;
using ACS.MANAGER.Core.AcsControlRole.Get.ListV;
using ACS.MANAGER.Core.AcsControlRole.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsControlRole
{
    partial class AcsControlRoleGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsControlRoleGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_CONTROL_ROLE>))
                {
                    IAcsControlRoleGetListEv behavior = AcsControlRoleGetListEvBehaviorFactory.MakeIAcsControlRoleGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_CONTROL_ROLE))
                {
                    IAcsControlRoleGetEv behavior = AcsControlRoleGetEvBehaviorFactory.MakeIAcsControlRoleGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_ACS_CONTROL_ROLE>))
                {
                    IAcsControlRoleGetListV behavior = AcsControlRoleGetListVBehaviorFactory.MakeIAcsControlRoleGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_ACS_CONTROL_ROLE))
                {
                    IAcsControlRoleGetV behavior = AcsControlRoleGetVBehaviorFactory.MakeIAcsControlRoleGetV(param, entity);
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
