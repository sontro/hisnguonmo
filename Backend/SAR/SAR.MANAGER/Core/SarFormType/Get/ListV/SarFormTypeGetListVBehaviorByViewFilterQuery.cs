using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarFormType.Get.ListV
{
    class SarFormTypeGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarFormTypeGetListV
    {
        SarFormTypeViewFilterQuery filterQuery;

        internal SarFormTypeGetListVBehaviorByViewFilterQuery(CommonParam param, SarFormTypeViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_FORM_TYPE> ISarFormTypeGetListV.Run()
        {
            try
            {
                return DAOWorker.SarFormTypeDAO.GetView(filterQuery.Query(), param);
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
