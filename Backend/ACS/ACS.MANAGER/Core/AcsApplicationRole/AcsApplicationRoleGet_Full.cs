using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsApplicationRole.Get.Ev;
using ACS.MANAGER.Core.AcsApplicationRole.Get.ListEv;
using ACS.MANAGER.Core.AcsApplicationRole.Get.ListV;
using ACS.MANAGER.Core.AcsApplicationRole.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplicationRole
{
    partial class AcsApplicationRoleGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsApplicationRoleGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_APPLICATION_ROLE>))
                {
                    IAcsApplicationRoleGetListEv behavior = AcsApplicationRoleGetListEvBehaviorFactory.MakeIAcsApplicationRoleGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_APPLICATION_ROLE))
                {
                    IAcsApplicationRoleGetEv behavior = AcsApplicationRoleGetEvBehaviorFactory.MakeIAcsApplicationRoleGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_ACS_APPLICATION_ROLE>))
                {
                    IAcsApplicationRoleGetListV behavior = AcsApplicationRoleGetListVBehaviorFactory.MakeIAcsApplicationRoleGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_ACS_APPLICATION_ROLE))
                {
                    IAcsApplicationRoleGetV behavior = AcsApplicationRoleGetVBehaviorFactory.MakeIAcsApplicationRoleGetV(param, entity);
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
