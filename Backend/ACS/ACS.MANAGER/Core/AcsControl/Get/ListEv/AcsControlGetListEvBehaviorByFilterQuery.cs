using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsControl.Get.ListEv
{
    class AcsControlGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsControlGetListEv
    {
        AcsControlFilterQuery filterQuery;

        internal AcsControlGetListEvBehaviorByFilterQuery(CommonParam param, AcsControlFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_CONTROL> IAcsControlGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsControlDAO.Get(filterQuery.Query(), param);
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
