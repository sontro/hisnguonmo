using SDA.MANAGER.Core.SdaGroup.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroup
{
    partial class SdaGroupChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaGroupChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaGroup.Contains(entity.GetType()))
                {
                    ISdaGroupChangeLock behavior = SdaGroupChangeLockBehaviorFactory.MakeISdaGroupChangeLock(param, entity);
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
