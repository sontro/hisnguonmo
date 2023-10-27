using ACS.MANAGER.Core.AcsApplication.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplication
{
    partial class AcsApplicationChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsApplicationChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsApplication.Contains(entity.GetType()))
                {
                    IAcsApplicationChangeLock behavior = AcsApplicationChangeLockBehaviorFactory.MakeIAcsApplicationChangeLock(param, entity);
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
