using SDA.MANAGER.Core.SdaMetadata.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaMetadata
{
    partial class SdaMetadataUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaMetadataUpdate(CommonParam param, object data)
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
                    ISdaMetadataUpdate behavior = SdaMetadataUpdateBehaviorFactory.MakeISdaMetadataUpdate(param, entity);
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
