using ACS.MANAGER.Core.AcsModuleRole.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleRole
{
    partial class AcsModuleRoleChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsModuleRoleChangeLock(CommonParam param, object data)
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
                    IAcsModuleRoleChangeLock behavior = AcsModuleRoleChangeLockBehaviorFactory.MakeIAcsModuleRoleChangeLock(param, entity);
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
