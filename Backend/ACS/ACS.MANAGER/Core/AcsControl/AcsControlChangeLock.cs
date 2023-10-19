using ACS.MANAGER.Core.AcsControl.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControl
{
    partial class AcsControlChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsControlChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsControl.Contains(entity.GetType()))
                {
                    IAcsControlChangeLock behavior = AcsControlChangeLockBehaviorFactory.MakeIAcsControlChangeLock(param, entity);
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
