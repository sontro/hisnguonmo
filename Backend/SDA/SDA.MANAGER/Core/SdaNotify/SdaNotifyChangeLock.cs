using SDA.MANAGER.Core.SdaNotify.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNotify
{
    partial class SdaNotifyChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaNotifyChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaNotify.Contains(entity.GetType()))
                {
                    ISdaNotifyChangeLock behavior = SdaNotifyChangeLockBehaviorFactory.MakeISdaNotifyChangeLock(param, entity);
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
