using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintType.Get.ListV
{
    class SarPrintTypeGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarPrintTypeGetListV
    {
        SarPrintTypeViewFilterQuery filterQuery;

        internal SarPrintTypeGetListVBehaviorByViewFilterQuery(CommonParam param, SarPrintTypeViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_PRINT_TYPE> ISarPrintTypeGetListV.Run()
        {
            try
            {
                return DAOWorker.SarPrintTypeDAO.GetView(filterQuery.Query(), param);
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
