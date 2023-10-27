using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsRoleBase.Get.Ev;
using ACS.MANAGER.Core.AcsRoleBase.Get.ListEv;
using ACS.MANAGER.Core.AcsRoleBase.Get.ListV;
using ACS.MANAGER.Core.AcsRoleBase.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleBase
{
    partial class AcsRoleBaseGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsRoleBaseGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_ROLE_BASE>))
                {
                    IAcsRoleBaseGetListEv behavior = AcsRoleBaseGetListEvBehaviorFactory.MakeIAcsRoleBaseGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_ROLE_BASE))
                {
                    IAcsRoleBaseGetEv behavior = AcsRoleBaseGetEvBehaviorFactory.MakeIAcsRoleBaseGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_ACS_ROLE_BASE>))
                {
                    IAcsRoleBaseGetListV behavior = AcsRoleBaseGetListVBehaviorFactory.MakeIAcsRoleBaseGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_ACS_ROLE_BASE))
                {
                    IAcsRoleBaseGetV behavior = AcsRoleBaseGetVBehaviorFactory.MakeIAcsRoleBaseGetV(param, entity);
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
