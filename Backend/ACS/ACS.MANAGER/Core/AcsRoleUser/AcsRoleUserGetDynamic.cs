using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsRoleUser.Get.ListDynamic;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser
{
    partial class AcsRoleUserGetDynamic : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsRoleUserGetDynamic(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<object>))
                {
                    IAcsRoleUserGetListDynamic behavior = AcsRoleUserGetListDynamicBehaviorFactory.MakeIAcsRoleUserGetListV(param, entity);
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
