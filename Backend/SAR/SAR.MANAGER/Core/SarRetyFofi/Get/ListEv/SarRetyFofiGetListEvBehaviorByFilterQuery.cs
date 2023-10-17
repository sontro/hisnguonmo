using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarRetyFofi.Get.ListEv
{
    class SarRetyFofiGetListEvBehaviorByFilterQuery : BeanObjectBase, ISarRetyFofiGetListEv
    {
        SarRetyFofiFilterQuery filterQuery;

        internal SarRetyFofiGetListEvBehaviorByFilterQuery(CommonParam param, SarRetyFofiFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SAR_RETY_FOFI> ISarRetyFofiGetListEv.Run()
        {
            try
            {
                return DAOWorker.SarRetyFofiDAO.Get(filterQuery.Query(), param);
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
