using ACS.MANAGER.Core.AcsRole.Create;
using ACS.MANAGER.Core.AcsRole.RoleBase.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRole
{
    partial class AcsRoleRoleBaseUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleRoleBaseUpdate(CommonParam param, object data)
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
                    IAcsRoleRoleBaseUpdate behavior = AcsRoleRoleBaseUpdateBehaviorFactory.MakeIAcsRoleRoleBaseUpdate(param, entity);
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
