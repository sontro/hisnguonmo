using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarFormField.Get.ListV
{
    class SarFormFieldGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarFormFieldGetListV
    {
        SarFormFieldViewFilterQuery filterQuery;

        internal SarFormFieldGetListVBehaviorByViewFilterQuery(CommonParam param, SarFormFieldViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_FORM_FIELD> ISarFormFieldGetListV.Run()
        {
            try
            {
                return DAOWorker.SarFormFieldDAO.GetView(filterQuery.Query(), param);
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
