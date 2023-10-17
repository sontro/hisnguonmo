using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrint.Get.ListV
{
    class SarPrintGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarPrintGetListV
    {
        SarPrintViewFilterQuery filterQuery;

        internal SarPrintGetListVBehaviorByViewFilterQuery(CommonParam param, SarPrintViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_PRINT> ISarPrintGetListV.Run()
        {
            try
            {
                return DAOWorker.SarPrintDAO.GetView(filterQuery.Query(), param);
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
