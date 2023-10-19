using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsRole.Get.Ev;
using ACS.MANAGER.Core.AcsRole.Get.ListEv;
using ACS.MANAGER.Core.AcsRole.RoleBase.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRole
{
    partial class AcsRoleRoleBaseGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsRoleRoleBaseGet(CommonParam param, object data)
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
                    IAcsRoleRoleBaseGetListEv behavior = AcsRoleRoleBaseGetListEvBehaviorFactory.MakeIAcsRoleGetListBase(param, entity);
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
