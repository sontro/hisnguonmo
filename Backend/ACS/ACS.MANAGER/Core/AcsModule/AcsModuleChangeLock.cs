using ACS.MANAGER.Core.AcsModule.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModule
{
    partial class AcsModuleChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsModuleChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsModule.Contains(entity.GetType()))
                {
                    IAcsModuleChangeLock behavior = AcsModuleChangeLockBehaviorFactory.MakeIAcsModuleChangeLock(param, entity);
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
