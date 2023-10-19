using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsRole.Get;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser.Get.ListForTree
{
    class AcsRoleUserGetListForTreeBehaviorByFilterQuery : BeanObjectBase, IAcsRoleUserGetListForTree
    {
        AcsRoleUserViewFilterQuery filterQuery;

        internal AcsRoleUserGetListForTreeBehaviorByFilterQuery(CommonParam param, AcsRoleUserViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<AcsRoleUserSDO> IAcsRoleUserGetListForTree.Run()
        {
            try
            {
                List<AcsRoleUserSDO> result = new List<AcsRoleUserSDO>();
                AcsRoleFilterQuery roleFilterQuery = new AcsRole.Get.AcsRoleFilterQuery();
                roleFilterQuery.KEY_WORD = filterQuery.KEY_WORD;
                var roles = DAOWorker.AcsRoleDAO.Get(roleFilterQuery.Query(), param);
                if (roles != null && roles.Count > 0)
                {
                    foreach (var item in roles)
                    {
                        AcsRoleUserSDO sdoRoleUser = new AcsRoleUserSDO();
                        sdoRoleUser.ROLE_ID = item.ID;
                        sdoRoleUser.ROLE_CODE = item.ROLE_CODE;
                        sdoRoleUser.ROLE_NAME = item.ROLE_NAME;
                        result.Add(sdoRoleUser);
                    }
                }
                var roleUsers = DAOWorker.AcsRoleUserDAO.GetView(filterQuery.Query(), param);
                if (roleUsers != null && roleUsers.Count > 0)
                {
                    foreach (var item in result)
                    {
                        var chkItem = roleUsers.Find(o => o.ROLE_ID == item.ROLE_ID);
                        if (chkItem != null)
                        {
                            item.USER_ID = chkItem.USER_ID;
                            item.USERNAME = chkItem.USERNAME;
                            item.LOGINNAME = chkItem.LOGINNAME;
                        }
                    }
                }

                return result;
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
