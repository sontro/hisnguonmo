using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfig.Get.ListV
{
    class SdaConfigGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaConfigGetListV
    {
        SdaConfigViewFilterQuery filterQuery;

        internal SdaConfigGetListVBehaviorByViewFilterQuery(CommonParam param, SdaConfigViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_CONFIG> ISdaConfigGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaConfigDAO.GetView(filterQuery.Query(), param);
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
