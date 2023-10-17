using SDA.MANAGER.Core.SdaConfig.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfig
{
    partial class SdaConfigChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaConfigChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaConfig.Contains(entity.GetType()))
                {
                    ISdaConfigChangeLock behavior = SdaConfigChangeLockBehaviorFactory.MakeISdaConfigChangeLock(param, entity);
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
