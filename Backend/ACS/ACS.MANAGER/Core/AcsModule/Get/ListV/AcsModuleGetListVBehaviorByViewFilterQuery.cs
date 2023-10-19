using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModule.Get.ListV
{
    class AcsModuleGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsModuleGetListV
    {
        AcsModuleViewFilterQuery filterQuery;

        internal AcsModuleGetListVBehaviorByViewFilterQuery(CommonParam param, AcsModuleViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_MODULE> IAcsModuleGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsModuleDAO.GetView(filterQuery.Query(), param);
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
