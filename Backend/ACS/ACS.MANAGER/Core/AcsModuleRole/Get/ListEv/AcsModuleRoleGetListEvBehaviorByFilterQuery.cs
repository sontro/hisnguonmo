using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModuleRole.Get.ListEv
{
    class AcsModuleRoleGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsModuleRoleGetListEv
    {
        AcsModuleRoleFilterQuery filterQuery;

        internal AcsModuleRoleGetListEvBehaviorByFilterQuery(CommonParam param, AcsModuleRoleFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_MODULE_ROLE> IAcsModuleRoleGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsModuleRoleDAO.Get(filterQuery.Query(), param);
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
