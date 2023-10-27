using ACS.MANAGER.Core.AcsActivityLog.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityLog
{
    partial class AcsActivityLogChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsActivityLogChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsActivityLog.Contains(entity.GetType()))
                {
                    IAcsActivityLogChangeLock behavior = AcsActivityLogChangeLockBehaviorFactory.MakeIAcsActivityLogChangeLock(param, entity);
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
