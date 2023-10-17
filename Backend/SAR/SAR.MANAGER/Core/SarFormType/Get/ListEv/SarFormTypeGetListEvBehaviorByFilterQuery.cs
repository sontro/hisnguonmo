using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarFormType.Get.ListEv
{
    class SarFormTypeGetListEvBehaviorByFilterQuery : BeanObjectBase, ISarFormTypeGetListEv
    {
        SarFormTypeFilterQuery filterQuery;

        internal SarFormTypeGetListEvBehaviorByFilterQuery(CommonParam param, SarFormTypeFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SAR_FORM_TYPE> ISarFormTypeGetListEv.Run()
        {
            try
            {
                return DAOWorker.SarFormTypeDAO.Get(filterQuery.Query(), param);
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
