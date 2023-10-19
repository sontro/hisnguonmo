using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.AcsUser.Get.ListDynamic
{
    class AcsUserGetListDynamicBehaviorByViewFilterQuery : DynamicBase, IAcsUserGetListDynamic
    {
        AcsUserFilterQuery filterQuery;

        internal AcsUserGetListDynamicBehaviorByViewFilterQuery(CommonParam param, AcsUserFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<object> IAcsUserGetListDynamic.Run()
        {
            List<object> result = new List<object>();
            try
            {
                result = this.RunBase("ACS_USER", filterQuery);
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
            
            //if (this.IDs != null && this.IDs.Count > 0)
            //{
            //    listExpression.Add(o => this.IDs.Contains(o.ID));
            //}

            //if (this.LOGINNAMEs != null && this.LOGINNAMEs.Count > 0)
            //{
            //    listExpression.Add(o => this.LOGINNAMEs.Contains(o.LOGINNAME));
            //}
            //if (this.LOGINNAME__OR__SUB_LOGINNAMEs != null && this.LOGINNAME__OR__SUB_LOGINNAMEs.Count > 0)
            //{
            //    listExpression.Add(o => this.LOGINNAME__OR__SUB_LOGINNAMEs.Contains(o.LOGINNAME) || (!String.IsNullOrEmpty(o.SUB_LOGINNAME) && this.LOGINNAME__OR__SUB_LOGINNAMEs.Contains(o.SUB_LOGINNAME)));
            //}
            //if (!String.IsNullOrEmpty(this.LOGINNAME))
            //{
            //    listExpression.Add(o => o.LOGINNAME.ToLower() == this.LOGINNAME.ToLower());
            //}
            //if (!String.IsNullOrEmpty(this.LOGINNAME__OR__SUB_LOGINNAME))
            //{
            //    listExpression.Add(o => ((o.LOGINNAME.ToLower() == this.LOGINNAME__OR__SUB_LOGINNAME.ToLower()) || (!String.IsNullOrEmpty(o.SUB_LOGINNAME) && o.SUB_LOGINNAME.ToLower() == this.LOGINNAME__OR__SUB_LOGINNAME.ToLower())));
            //}
            //if (!String.IsNullOrEmpty(this.KEY_WORD))
            //{
            //    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
            //    listExpression.Add(o => o.LOGINNAME.ToLower().Contains(this.KEY_WORD) || o.USERNAME.ToLower().Contains(this.KEY_WORD) || o.EMAIL.ToLower().Contains(this.KEY_WORD) || o.MOBILE.ToLower().Contains(this.KEY_WORD));
            //}
            //if (filterQuery.USER_ID.HasValue)
            //{
            //    strFilterCondition += string.Format(" AND USER_ID = {0}", filterQuery.USER_ID.Value);
            //}
            //if (filterQuery.ROLE_ID.HasValue)
            //{
            //    strFilterCondition += string.Format(" AND ROLE_ID = {0}", filterQuery.ROLE_ID.Value);
            //}
            //if (!String.IsNullOrEmpty(filterQuery.LOGINNAME))
            //{
            //    strFilterCondition += string.Format(" AND LOGINNAME = \"{0}\"", filterQuery.LOGINNAME);
            //}
            //if (!String.IsNullOrEmpty(filterQuery.ROLE_CODE))
            //{
            //    strFilterCondition += string.Format(" AND ROLE_CODE = \"{0}\"", filterQuery.ROLE_CODE);
            //}
            return strFilterCondition;
        }
    }
}
