using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaSqlParam.Get.ListEv
{
    class SdaSqlParamGetListEvBehaviorByFilterQuery : BeanObjectBase, ISdaSqlParamGetListEv
    {
        SdaSqlParamFilterQuery filterQuery;

        internal SdaSqlParamGetListEvBehaviorByFilterQuery(CommonParam param, SdaSqlParamFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SDA_SQL_PARAM> ISdaSqlParamGetListEv.Run()
        {
            try
            {
                return DAOWorker.SdaSqlParamDAO.Get(filterQuery.Query(), param);
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
