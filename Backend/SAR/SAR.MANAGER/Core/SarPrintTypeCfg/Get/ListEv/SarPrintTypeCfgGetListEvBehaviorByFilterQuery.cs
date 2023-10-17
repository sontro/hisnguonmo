using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Get.ListEv
{
    class SarPrintTypeCfgGetListEvBehaviorByFilterQuery : BeanObjectBase, ISarPrintTypeCfgGetListEv
    {
        SarPrintTypeCfgFilterQuery filterQuery;

        internal SarPrintTypeCfgGetListEvBehaviorByFilterQuery(CommonParam param, SarPrintTypeCfgFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SAR_PRINT_TYPE_CFG> ISarPrintTypeCfgGetListEv.Run()
        {
            try
            {
                return DAOWorker.SarPrintTypeCfgDAO.Get(filterQuery.Query(), param);
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
