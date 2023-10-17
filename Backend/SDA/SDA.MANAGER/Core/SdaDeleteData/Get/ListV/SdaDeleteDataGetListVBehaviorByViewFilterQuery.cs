using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaDeleteData.Get.ListV
{
    class SdaDeleteDataGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaDeleteDataGetListV
    {
        SdaDeleteDataViewFilterQuery filterQuery;

        internal SdaDeleteDataGetListVBehaviorByViewFilterQuery(CommonParam param, SdaDeleteDataViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_DELETE_DATA> ISdaDeleteDataGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaDeleteDataDAO.GetView(filterQuery.Query(), param);
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
