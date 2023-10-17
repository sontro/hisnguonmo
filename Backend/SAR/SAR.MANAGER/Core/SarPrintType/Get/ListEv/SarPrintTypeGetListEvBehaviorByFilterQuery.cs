using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintType.Get.ListEv
{
    class SarPrintTypeGetListEvBehaviorByFilterQuery : BeanObjectBase, ISarPrintTypeGetListEv
    {
        SarPrintTypeFilterQuery filterQuery;

        internal SarPrintTypeGetListEvBehaviorByFilterQuery(CommonParam param, SarPrintTypeFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SAR_PRINT_TYPE> ISarPrintTypeGetListEv.Run()
        {
            try
            {
                return DAOWorker.SarPrintTypeDAO.Get(filterQuery.Query(), param);
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
