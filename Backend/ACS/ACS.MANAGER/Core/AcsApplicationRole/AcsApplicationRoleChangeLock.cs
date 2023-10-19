using ACS.MANAGER.Core.AcsApplicationRole.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplicationRole
{
    partial class AcsApplicationRoleChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsApplicationRoleChangeLock(CommonParam param, object data)
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
                    IAcsApplicationRoleChangeLock behavior = AcsApplicationRoleChangeLockBehaviorFactory.MakeIAcsApplicationRoleChangeLock(param, entity);
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
