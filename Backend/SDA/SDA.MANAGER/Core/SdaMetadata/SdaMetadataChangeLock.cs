using SDA.MANAGER.Core.SdaMetadata.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaMetadata
{
    partial class SdaMetadataChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaMetadataChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaMetadata.Contains(entity.GetType()))
                {
                    ISdaMetadataChangeLock behavior = SdaMetadataChangeLockBehaviorFactory.MakeISdaMetadataChangeLock(param, entity);
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
