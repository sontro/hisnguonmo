using SDA.MANAGER.Core.SdaNational.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNational
{
    partial class SdaNationalChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaNationalChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaNational.Contains(entity.GetType()))
                {
                    ISdaNationalChangeLock behavior = SdaNationalChangeLockBehaviorFactory.MakeISdaNationalChangeLock(param, entity);
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
