using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarFormData.Get.ListV
{
    class SarFormDataGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarFormDataGetListV
    {
        SarFormDataViewFilterQuery filterQuery;

        internal SarFormDataGetListVBehaviorByViewFilterQuery(CommonParam param, SarFormDataViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_FORM_DATA> ISarFormDataGetListV.Run()
        {
            try
            {
                return DAOWorker.SarFormDataDAO.GetView(filterQuery.Query(), param);
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
