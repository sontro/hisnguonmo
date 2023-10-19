using ACS.MANAGER.Core.AcsModuleRole.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleRole
{
    partial class AcsModuleRoleCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsModuleRoleCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsModuleRole.Contains(entity.GetType()))
                {
                    IAcsModuleRoleCreate behavior = AcsModuleRoleCreateBehaviorFactory.MakeIAcsModuleRoleCreate(param, entity);
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
