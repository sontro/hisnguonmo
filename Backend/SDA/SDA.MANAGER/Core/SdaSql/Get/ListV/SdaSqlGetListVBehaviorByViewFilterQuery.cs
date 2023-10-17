using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaSql.Get.ListV
{
    class SdaSqlGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaSqlGetListV
    {
        SdaSqlViewFilterQuery filterQuery;

        internal SdaSqlGetListVBehaviorByViewFilterQuery(CommonParam param, SdaSqlViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_SQL> ISdaSqlGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaSqlDAO.GetView(filterQuery.Query(), param);
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
