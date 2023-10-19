using ACS.MANAGER.Core.AcsActivityType.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityType
{
    partial class AcsActivityTypeChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsActivityTypeChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsActivityType.Contains(entity.GetType()))
                {
                    IAcsActivityTypeChangeLock behavior = AcsActivityTypeChangeLockBehaviorFactory.MakeIAcsActivityTypeChangeLock(param, entity);
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
