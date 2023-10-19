using ACS.MANAGER.Core.AcsModuleRole.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleRole
{
    partial class AcsModuleRoleUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsModuleRoleUpdate(CommonParam param, object data)
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
                    IAcsModuleRoleUpdate behavior = AcsModuleRoleUpdateBehaviorFactory.MakeIAcsModuleRoleUpdate(param, entity);
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
