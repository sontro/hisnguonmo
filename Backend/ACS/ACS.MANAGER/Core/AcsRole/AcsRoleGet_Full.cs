using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsRole.Get.Ev;
using ACS.MANAGER.Core.AcsRole.Get.ListEv;
using ACS.MANAGER.Core.AcsRole.Get.ListV;
using ACS.MANAGER.Core.AcsRole.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRole
{
    partial class AcsRoleGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsRoleGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_ROLE>))
                {
                    IAcsRoleGetListEv behavior = AcsRoleGetListEvBehaviorFactory.MakeIAcsRoleGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_ROLE))
                {
                    IAcsRoleGetEv behavior = AcsRoleGetEvBehaviorFactory.MakeIAcsRoleGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_ACS_ROLE>))
                {
                    IAcsRoleGetListV behavior = AcsRoleGetListVBehaviorFactory.MakeIAcsRoleGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_ACS_ROLE))
                {
                    IAcsRoleGetV behavior = AcsRoleGetVBehaviorFactory.MakeIAcsRoleGetV(param, entity);
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
