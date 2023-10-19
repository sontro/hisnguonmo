using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplication.Get.ListEv
{
    class AcsApplicationGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsApplicationGetListEv
    {
        AcsApplicationFilterQuery filterQuery;

        internal AcsApplicationGetListEvBehaviorByFilterQuery(CommonParam param, AcsApplicationFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_APPLICATION> IAcsApplicationGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsApplicationDAO.Get(filterQuery.Query(), param);
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
