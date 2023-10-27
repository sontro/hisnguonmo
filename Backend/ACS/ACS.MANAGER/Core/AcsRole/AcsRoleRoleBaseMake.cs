using ACS.MANAGER.Core.AcsRole.Create;
using ACS.MANAGER.Core.AcsRole.RoleBase.Make;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRole
{
    partial class AcsRoleRoleBaseMake : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleRoleBaseMake(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsRole.Contains(entity.GetType()))
                {
                    IAcsRoleRoleBaseMake behavior = AcsRoleRoleBaseMakeBehaviorFactory.MakeIAcsRoleRoleBaseMake(param, entity);
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
