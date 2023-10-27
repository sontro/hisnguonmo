using ACS.MANAGER.Core.AcsUser.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsUserChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsUser.Contains(entity.GetType()))
                {
                    IAcsUserChangeLock behavior = AcsUserChangeLockBehaviorFactory.MakeIAcsUserChangeLock(param, entity);
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
