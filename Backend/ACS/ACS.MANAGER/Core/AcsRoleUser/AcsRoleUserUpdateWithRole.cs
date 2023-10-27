using ACS.MANAGER.Core.AcsRoleUser.Update;
using ACS.MANAGER.Core.AcsRoleUser.UpdateWithRole;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser
{
    partial class AcsRoleUserUpdateWithRole : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleUserUpdateWithRole(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsRoleUser.Contains(entity.GetType()))
                {
                    IAcsRoleUserUpdateWithRole behavior = AcsRoleUserUpdateWithRoleBehaviorFactory.MakeIAcsRoleUserUpdateWithRole(param, entity);
                    result = behavior != null ? behavior.Run() : false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
