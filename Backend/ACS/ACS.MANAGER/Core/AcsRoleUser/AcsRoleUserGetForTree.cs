using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsRoleUser.Get.Ev;
using ACS.MANAGER.Core.AcsRoleUser.Get.ListEv;
using ACS.MANAGER.Core.AcsRoleUser.Get.ListForTree;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser
{
    partial class AcsRoleUserGetForTree : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsRoleUserGetForTree(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<AcsRoleUserSDO>))
                {
                    IAcsRoleUserGetListForTree behavior = AcsRoleUserGetListForTreeBehaviorFactory.MakeIAcsRoleUserGetListForTree(param, entity);
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
