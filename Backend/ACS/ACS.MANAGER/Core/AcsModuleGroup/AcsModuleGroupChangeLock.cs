using ACS.MANAGER.Core.AcsModuleGroup.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleGroup
{
    partial class AcsModuleGroupChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsModuleGroupChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsModuleGroup.Contains(entity.GetType()))
                {
                    IAcsModuleGroupChangeLock behavior = AcsModuleGroupChangeLockBehaviorFactory.MakeIAcsModuleGroupChangeLock(param, entity);
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
