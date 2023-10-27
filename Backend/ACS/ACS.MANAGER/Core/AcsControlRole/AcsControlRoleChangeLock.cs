using ACS.MANAGER.Core.AcsControlRole.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControlRole
{
    partial class AcsControlRoleChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsControlRoleChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsControlRole.Contains(entity.GetType()))
                {
                    IAcsControlRoleChangeLock behavior = AcsControlRoleChangeLockBehaviorFactory.MakeIAcsControlRoleChangeLock(param, entity);
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
