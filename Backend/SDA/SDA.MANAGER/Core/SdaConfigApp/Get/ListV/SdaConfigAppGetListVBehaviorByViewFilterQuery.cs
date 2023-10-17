using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfigApp.Get.ListV
{
    class SdaConfigAppGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaConfigAppGetListV
    {
        SdaConfigAppViewFilterQuery filterQuery;

        internal SdaConfigAppGetListVBehaviorByViewFilterQuery(CommonParam param, SdaConfigAppViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_CONFIG_APP> ISdaConfigAppGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaConfigAppDAO.GetView(filterQuery.Query(), param);
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
