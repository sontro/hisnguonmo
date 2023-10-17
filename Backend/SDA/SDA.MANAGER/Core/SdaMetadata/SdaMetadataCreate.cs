using SDA.MANAGER.Core.SdaMetadata.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaMetadata
{
    partial class SdaMetadataCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaMetadataCreate(CommonParam param, object data)
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
                    ISdaMetadataCreate behavior = SdaMetadataCreateBehaviorFactory.MakeISdaMetadataCreate(param, entity);
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
