using SDA.MANAGER.Core.SdaConfigAppUser.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigAppUser
{
    partial class SdaConfigAppUserChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaConfigAppUserChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaConfigAppUser.Contains(entity.GetType()))
                {
                    ISdaConfigAppUserChangeLock behavior = SdaConfigAppUserChangeLockBehaviorFactory.MakeISdaConfigAppUserChangeLock(param, entity);
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
