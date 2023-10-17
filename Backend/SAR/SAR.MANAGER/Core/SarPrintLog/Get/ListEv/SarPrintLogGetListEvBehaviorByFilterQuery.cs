using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintLog.Get.ListEv
{
    class SarPrintLogGetListEvBehaviorByFilterQuery : BeanObjectBase, ISarPrintLogGetListEv
    {
        SarPrintLogFilterQuery filterQuery;

        internal SarPrintLogGetListEvBehaviorByFilterQuery(CommonParam param, SarPrintLogFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SAR_PRINT_LOG> ISarPrintLogGetListEv.Run()
        {
            try
            {
                return DAOWorker.SarPrintLogDAO.Get(filterQuery.Query(), param);
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
