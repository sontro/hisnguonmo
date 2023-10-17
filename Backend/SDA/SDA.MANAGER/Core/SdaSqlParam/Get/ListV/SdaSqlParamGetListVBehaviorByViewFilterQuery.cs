using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaSqlParam.Get.ListV
{
    class SdaSqlParamGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaSqlParamGetListV
    {
        SdaSqlParamViewFilterQuery filterQuery;

        internal SdaSqlParamGetListVBehaviorByViewFilterQuery(CommonParam param, SdaSqlParamViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_SQL_PARAM> ISdaSqlParamGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaSqlParamDAO.GetView(filterQuery.Query(), param);
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
