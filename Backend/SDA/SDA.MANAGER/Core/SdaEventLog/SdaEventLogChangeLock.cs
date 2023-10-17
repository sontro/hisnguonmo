using SDA.MANAGER.Core.SdaEventLog.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEventLog
{
    partial class SdaEventLogChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaEventLogChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaEventLog.Contains(entity.GetType()))
                {
                    ISdaEventLogChangeLock behavior = SdaEventLogChangeLockBehaviorFactory.MakeISdaEventLogChangeLock(param, entity);
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
