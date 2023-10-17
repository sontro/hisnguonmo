using SDA.MANAGER.Core.SdaModuleField.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaModuleField
{
    partial class SdaModuleFieldChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaModuleFieldChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaModuleField.Contains(entity.GetType()))
                {
                    ISdaModuleFieldChangeLock behavior = SdaModuleFieldChangeLockBehaviorFactory.MakeISdaModuleFieldChangeLock(param, entity);
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
