using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsControl.Get.ListV
{
    class AcsControlGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsControlGetListV
    {
        AcsControlViewFilterQuery filterQuery;

        internal AcsControlGetListVBehaviorByViewFilterQuery(CommonParam param, AcsControlViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_CONTROL> IAcsControlGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsControlDAO.GetView(filterQuery.Query(), param);
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
