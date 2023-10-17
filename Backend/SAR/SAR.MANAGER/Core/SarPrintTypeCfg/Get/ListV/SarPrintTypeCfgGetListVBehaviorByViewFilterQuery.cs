using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Get.ListV
{
    class SarPrintTypeCfgGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarPrintTypeCfgGetListV
    {
        SarPrintTypeCfgViewFilterQuery filterQuery;

        internal SarPrintTypeCfgGetListVBehaviorByViewFilterQuery(CommonParam param, SarPrintTypeCfgViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_PRINT_TYPE_CFG> ISarPrintTypeCfgGetListV.Run()
        {
            try
            {
                return DAOWorker.SarPrintTypeCfgDAO.GetView(filterQuery.Query(), param);
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
