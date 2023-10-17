using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintLog.Get.ListV
{
    class SarPrintLogGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarPrintLogGetListV
    {
        SarPrintLogViewFilterQuery filterQuery;

        internal SarPrintLogGetListVBehaviorByViewFilterQuery(CommonParam param, SarPrintLogViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_PRINT_LOG> ISarPrintLogGetListV.Run()
        {
            try
            {
                return DAOWorker.SarPrintLogDAO.GetView(filterQuery.Query(), param);
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
