using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarForm.Get.ListEv
{
    class SarFormGetListEvBehaviorByFilterQuery : BeanObjectBase, ISarFormGetListEv
    {
        SarFormFilterQuery filterQuery;

        internal SarFormGetListEvBehaviorByFilterQuery(CommonParam param, SarFormFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SAR_FORM> ISarFormGetListEv.Run()
        {
            try
            {
                return DAOWorker.SarFormDAO.Get(filterQuery.Query(), param);
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
