using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaMetadata.Get.Ev;
using SDA.MANAGER.Core.SdaMetadata.Get.ListEv;
using SDA.MANAGER.Core.SdaMetadata.Get.ListV;
using SDA.MANAGER.Core.SdaMetadata.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaMetadata
{
    partial class SdaMetadataGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaMetadataGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_METADATA>))
                {
                    ISdaMetadataGetListEv behavior = SdaMetadataGetListEvBehaviorFactory.MakeISdaMetadataGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_METADATA))
                {
                    ISdaMetadataGetEv behavior = SdaMetadataGetEvBehaviorFactory.MakeISdaMetadataGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SDA_METADATA>))
                {
                    ISdaMetadataGetListV behavior = SdaMetadataGetListVBehaviorFactory.MakeISdaMetadataGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SDA_METADATA))
                {
                    ISdaMetadataGetV behavior = SdaMetadataGetVBehaviorFactory.MakeISdaMetadataGetV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }
    }
}
