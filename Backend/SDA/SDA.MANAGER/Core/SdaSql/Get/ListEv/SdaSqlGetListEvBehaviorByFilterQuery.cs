using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaSql.Get.ListEv
{
    class SdaSqlGetListEvBehaviorByFilterQuery : BeanObjectBase, ISdaSqlGetListEv
    {
        SdaSqlFilterQuery filterQuery;

        internal SdaSqlGetListEvBehaviorByFilterQuery(CommonParam param, SdaSqlFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SDA_SQL> ISdaSqlGetListEv.Run()
        {
            try
            {
                return DAOWorker.SdaSqlDAO.Get(filterQuery.Query(), param);
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
