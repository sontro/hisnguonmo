using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaMetadata.Get.ListEv
{
    class SdaMetadataGetListEvBehaviorByFilterQuery : BeanObjectBase, ISdaMetadataGetListEv
    {
        SdaMetadataFilterQuery filterQuery;

        internal SdaMetadataGetListEvBehaviorByFilterQuery(CommonParam param, SdaMetadataFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SDA_METADATA> ISdaMetadataGetListEv.Run()
        {
            try
            {
                return DAOWorker.SdaMetadataDAO.Get(filterQuery.Query(), param);
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
