using ACS.MANAGER.Core.AcsRole.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRole
{
    partial class AcsRoleCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleCreate(CommonParam param, object data)
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
                    IAcsRoleCreate behavior = AcsRoleCreateBehaviorFactory.MakeIAcsRoleCreate(param, entity);
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
