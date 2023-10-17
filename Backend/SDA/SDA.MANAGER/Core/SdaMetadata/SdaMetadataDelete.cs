using SDA.MANAGER.Core.SdaMetadata.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaMetadata
{
    partial class SdaMetadataDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaMetadataDelete(CommonParam param, object data)
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
                    ISdaMetadataDelete behavior = SdaMetadataDeleteBehaviorFactory.MakeISdaMetadataDelete(param, entity);
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
