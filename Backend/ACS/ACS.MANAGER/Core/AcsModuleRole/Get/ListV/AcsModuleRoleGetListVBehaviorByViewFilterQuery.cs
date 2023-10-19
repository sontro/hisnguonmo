using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModuleRole.Get.ListV
{
    class AcsModuleRoleGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsModuleRoleGetListV
    {
        AcsModuleRoleViewFilterQuery filterQuery;

        internal AcsModuleRoleGetListVBehaviorByViewFilterQuery(CommonParam param, AcsModuleRoleViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_MODULE_ROLE> IAcsModuleRoleGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsModuleRoleDAO.GetView(filterQuery.Query(), param);
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
