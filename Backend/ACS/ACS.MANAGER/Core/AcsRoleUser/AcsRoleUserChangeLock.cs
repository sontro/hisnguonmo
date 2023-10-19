using ACS.MANAGER.Core.AcsRoleUser.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser
{
    partial class AcsRoleUserChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleUserChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsRoleUser.Contains(entity.GetType()))
                {
                    IAcsRoleUserChangeLock behavior = AcsRoleUserChangeLockBehaviorFactory.MakeIAcsRoleUserChangeLock(param, entity);
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
