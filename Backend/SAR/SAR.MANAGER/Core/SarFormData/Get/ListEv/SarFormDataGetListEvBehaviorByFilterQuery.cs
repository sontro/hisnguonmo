using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarFormData.Get.ListEv
{
    class SarFormDataGetListEvBehaviorByFilterQuery : BeanObjectBase, ISarFormDataGetListEv
    {
        SarFormDataFilterQuery filterQuery;

        internal SarFormDataGetListEvBehaviorByFilterQuery(CommonParam param, SarFormDataFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SAR_FORM_DATA> ISarFormDataGetListEv.Run()
        {
            try
            {
                return DAOWorker.SarFormDataDAO.Get(filterQuery.Query(), param);
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
