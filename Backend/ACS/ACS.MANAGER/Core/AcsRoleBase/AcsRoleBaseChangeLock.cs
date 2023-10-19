using ACS.MANAGER.Core.AcsRoleBase.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase
{
    partial class AcsRoleBaseChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleBaseChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsRoleBase.Contains(entity.GetType()))
                {
                    IAcsRoleBaseChangeLock behavior = AcsRoleBaseChangeLockBehaviorFactory.MakeIAcsRoleBaseChangeLock(param, entity);
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
