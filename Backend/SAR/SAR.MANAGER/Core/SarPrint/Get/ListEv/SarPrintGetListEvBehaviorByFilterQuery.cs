using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrint.Get.ListEv
{
    class SarPrintGetListEvBehaviorByFilterQuery : BeanObjectBase, ISarPrintGetListEv
    {
        SarPrintFilterQuery filterQuery;

        internal SarPrintGetListEvBehaviorByFilterQuery(CommonParam param, SarPrintFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SAR_PRINT> ISarPrintGetListEv.Run()
        {
            try
            {
                return DAOWorker.SarPrintDAO.Get(filterQuery.Query(), param);
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
