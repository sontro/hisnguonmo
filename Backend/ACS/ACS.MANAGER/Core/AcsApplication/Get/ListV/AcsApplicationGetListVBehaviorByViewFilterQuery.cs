using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplication.Get.ListV
{
    class AcsApplicationGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsApplicationGetListV
    {
        AcsApplicationViewFilterQuery filterQuery;

        internal AcsApplicationGetListVBehaviorByViewFilterQuery(CommonParam param, AcsApplicationViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_APPLICATION> IAcsApplicationGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsApplicationDAO.GetView(filterQuery.Query(), param);
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
