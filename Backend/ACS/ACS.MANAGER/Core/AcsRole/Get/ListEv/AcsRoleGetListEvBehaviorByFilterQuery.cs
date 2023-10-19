using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsApplicationRole.Get;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.AcsRole.Get.ListEv
{
    class AcsRoleGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsRoleGetListEv
    {
        AcsRoleFilterQuery filterQuery;

        internal AcsRoleGetListEvBehaviorByFilterQuery(CommonParam param, AcsRoleFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_ROLE> IAcsRoleGetListEv.Run()
        {
            try
            {
                if (filterQuery.APPLICATION_ID > 0)
                {
                    //Lay danh sach role da duoc phan quyen cua 1 ung dung
                    AcsApplicationRoleViewFilterQuery applicationRoleViewFilterQuery = new AcsApplicationRole.Get.AcsApplicationRoleViewFilterQuery();
                    applicationRoleViewFilterQuery.APPLICATION_ID = filterQuery.APPLICATION_ID;
                    var appRoles = DAOWorker.AcsApplicationRoleDAO.GetView(applicationRoleViewFilterQuery.Query(), param);
                    if (appRoles != null)
                    {
                        filterQuery.IDs = appRoles.Select(o => o.ROLE_ID).Distinct().ToList();
                    }
                    else
                    {
                        filterQuery.ID = -1;
                    }
                }

                return DAOWorker.AcsRoleDAO.Get(filterQuery.Query(), param);
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
