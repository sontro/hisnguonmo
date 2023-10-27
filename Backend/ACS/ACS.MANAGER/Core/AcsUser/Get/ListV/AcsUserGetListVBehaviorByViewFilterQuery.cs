using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsUser.Get.ListV
{
    class AcsUserGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsUserGetListV
    {
        AcsUserViewFilterQuery filterQuery;

        internal AcsUserGetListVBehaviorByViewFilterQuery(CommonParam param, AcsUserViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_USER> IAcsUserGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsUserDAO.GetView(filterQuery.Query(), param);
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
