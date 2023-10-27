using ACS.MANAGER.Core.AcsModuleRole.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleRole
{
    partial class AcsModuleRoleDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsModuleRoleDelete(CommonParam param, object data)
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
                    IAcsModuleRoleDelete behavior = AcsModuleRoleDeleteBehaviorFactory.MakeIAcsModuleRoleDelete(param, entity);
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
