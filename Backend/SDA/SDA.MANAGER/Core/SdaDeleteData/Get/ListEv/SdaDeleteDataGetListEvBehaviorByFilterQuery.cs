using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaDeleteData.Get.ListEv
{
    class SdaDeleteDataGetListEvBehaviorByFilterQuery : BeanObjectBase, ISdaDeleteDataGetListEv
    {
        SdaDeleteDataFilterQuery filterQuery;

        internal SdaDeleteDataGetListEvBehaviorByFilterQuery(CommonParam param, SdaDeleteDataFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SDA_DELETE_DATA> ISdaDeleteDataGetListEv.Run()
        {
            try
            {
                return DAOWorker.SdaDeleteDataDAO.Get(filterQuery.Query(), param);
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
