using ACS.MANAGER.Core.AcsRole.Create;
using ACS.MANAGER.Core.AcsRole.RoleBase.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRole
{
    partial class AcsRoleRoleBaseDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleRoleBaseDelete(CommonParam param, object data)
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
                    IAcsRoleRoleBaseDelete behavior = AcsRoleRoleBaseDeleteBehaviorFactory.MakeIAcsRoleRoleBaseDelete(param, entity);
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
