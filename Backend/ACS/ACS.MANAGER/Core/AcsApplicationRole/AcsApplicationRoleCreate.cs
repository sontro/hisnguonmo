using ACS.MANAGER.Core.AcsApplicationRole.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplicationRole
{
    partial class AcsApplicationRoleCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsApplicationRoleCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsApplicationRole.Contains(entity.GetType()))
                {
                    IAcsApplicationRoleCreate behavior = AcsApplicationRoleCreateBehaviorFactory.MakeIAcsApplicationRoleCreate(param, entity);
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
