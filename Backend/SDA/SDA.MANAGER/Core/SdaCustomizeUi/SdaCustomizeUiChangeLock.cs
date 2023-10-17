using SDA.MANAGER.Core.SdaCustomizeUi.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCustomizeUi
{
    partial class SdaCustomizeUiChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaCustomizeUiChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaCustomizeUi.Contains(entity.GetType()))
                {
                    ISdaCustomizeUiChangeLock behavior = SdaCustomizeUiChangeLockBehaviorFactory.MakeISdaCustomizeUiChangeLock(param, entity);
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
