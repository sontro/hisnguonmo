using SDA.MANAGER.Core.SdaHideControl.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaHideControl
{
    partial class SdaHideControlChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaHideControlChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaHideControl.Contains(entity.GetType()))
                {
                    ISdaHideControlChangeLock behavior = SdaHideControlChangeLockBehaviorFactory.MakeISdaHideControlChangeLock(param, entity);
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
