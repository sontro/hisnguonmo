using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsModuleGroup.Get.Ev;
using ACS.MANAGER.Core.AcsModuleGroup.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModuleGroup
{
    partial class AcsModuleGroupGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsModuleGroupGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_MODULE_GROUP>))
                {
                    IAcsModuleGroupGetListEv behavior = AcsModuleGroupGetListEvBehaviorFactory.MakeIAcsModuleGroupGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_MODULE_GROUP))
                {
                    IAcsModuleGroupGetEv behavior = AcsModuleGroupGetEvBehaviorFactory.MakeIAcsModuleGroupGetEv(param, entity);
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
