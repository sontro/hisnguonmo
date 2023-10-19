using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsModuleRole.Get.Ev;
using ACS.MANAGER.Core.AcsModuleRole.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModuleRole
{
    partial class AcsModuleRoleGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsModuleRoleGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_MODULE_ROLE>))
                {
                    IAcsModuleRoleGetListEv behavior = AcsModuleRoleGetListEvBehaviorFactory.MakeIAcsModuleRoleGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_MODULE_ROLE))
                {
                    IAcsModuleRoleGetEv behavior = AcsModuleRoleGetEvBehaviorFactory.MakeIAcsModuleRoleGetEv(param, entity);
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
