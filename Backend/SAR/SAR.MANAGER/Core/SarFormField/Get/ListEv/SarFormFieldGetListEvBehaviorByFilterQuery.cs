using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarFormField.Get.ListEv
{
    class SarFormFieldGetListEvBehaviorByFilterQuery : BeanObjectBase, ISarFormFieldGetListEv
    {
        SarFormFieldFilterQuery filterQuery;

        internal SarFormFieldGetListEvBehaviorByFilterQuery(CommonParam param, SarFormFieldFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SAR_FORM_FIELD> ISarFormFieldGetListEv.Run()
        {
            try
            {
                return DAOWorker.SarFormFieldDAO.Get(filterQuery.Query(), param);
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
