using ACS.MANAGER.Core.AcsRole.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRole
{
    partial class AcsRoleChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleChangeLock(CommonParam param, object data)
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
                    IAcsRoleChangeLock behavior = AcsRoleChangeLockBehaviorFactory.MakeIAcsRoleChangeLock(param, entity);
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
