using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.AcsRoleUser.Get.ListDynamic
{
    class AcsRoleUserGetListDynamicBehaviorByViewFilterQuery : DynamicBase, IAcsRoleUserGetListDynamic
    {
        AcsRoleUserViewFilterQuery filterQuery;

        internal AcsRoleUserGetListDynamicBehaviorByViewFilterQuery(CommonParam param, AcsRoleUserViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<object> IAcsRoleUserGetListDynamic.Run()
        {
            List<object> result = new List<object>();
            try
            {
                result = this.RunBase("V_ACS_ROLE_USER", filterQuery);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
            return result;
        }

        protected override string ProcessFilterQuery()
        {
            string strFilterCondition = "";
            if (filterQuery.USER_ID.HasValue)
            {
                strFilterCondition += string.Format(" AND USER_ID = {0}", filterQuery.USER_ID.Value);
            }
            if (filterQuery.ROLE_ID.HasValue)
            {
                strFilterCondition += string.Format(" AND ROLE_ID = {0}", filterQuery.ROLE_ID.Value);
            }
            if (!String.IsNullOrEmpty(filterQuery.LOGINNAME))
            {
                strFilterCondition += string.Format(" AND LOGINNAME = \"{0}\"", filterQuery.LOGINNAME);
            }
            if (!String.IsNullOrEmpty(filterQuery.ROLE_CODE))
            {
                strFilterCondition += string.Format(" AND ROLE_CODE = \"{0}\"", filterQuery.ROLE_CODE);
            }
            return strFilterCondition;
        }
    }
}
