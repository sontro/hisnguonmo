using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaMetadata.Get.ListV
{
    class SdaMetadataGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaMetadataGetListV
    {
        SdaMetadataViewFilterQuery filterQuery;

        internal SdaMetadataGetListVBehaviorByViewFilterQuery(CommonParam param, SdaMetadataViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_METADATA> ISdaMetadataGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaMetadataDAO.GetView(filterQuery.Query(), param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
