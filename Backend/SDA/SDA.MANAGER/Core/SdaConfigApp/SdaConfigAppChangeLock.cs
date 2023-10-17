using SDA.MANAGER.Core.SdaConfigApp.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigApp
{
    partial class SdaConfigAppChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaConfigAppChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaConfigApp.Contains(entity.GetType()))
                {
                    ISdaConfigAppChangeLock behavior = SdaConfigAppChangeLockBehaviorFactory.MakeISdaConfigAppChangeLock(param, entity);
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
