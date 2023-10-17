using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarForm.Get.ListV
{
    class SarFormGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarFormGetListV
    {
        SarFormViewFilterQuery filterQuery;

        internal SarFormGetListVBehaviorByViewFilterQuery(CommonParam param, SarFormViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_FORM> ISarFormGetListV.Run()
        {
            try
            {
                return DAOWorker.SarFormDAO.GetView(filterQuery.Query(), param);
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
