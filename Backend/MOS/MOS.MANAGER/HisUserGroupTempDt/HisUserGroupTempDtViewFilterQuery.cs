using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserGroupTempDt
{
    public class HisUserGroupTempDtViewFilterQuery : HisUserGroupTempDtViewFilter
    {
        public HisUserGroupTempDtViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_USER_GROUP_TEMP_DT, bool>>> listVHisUserGroupTempDtExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_USER_GROUP_TEMP_DT, bool>>>();

        

        internal HisUserGroupTempDtSO Query()
        {
            HisUserGroupTempDtSO search = new HisUserGroupTempDtSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisUserGroupTempDtExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisUserGroupTempDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisUserGroupTempDtExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisUserGroupTempDtExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisUserGroupTempDtExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisUserGroupTempDtExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisUserGroupTempDtExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisUserGroupTempDtExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisUserGroupTempDtExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisUserGroupTempDtExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.USER_GROUP_TEMP_ID.HasValue)
                {
                    listVHisUserGroupTempDtExpression.Add(o => o.USER_GROUP_TEMP_ID == this.USER_GROUP_TEMP_ID.Value);
                }
                if (this.EXECUTE_ROLE_ID.HasValue)
                {
                    listVHisUserGroupTempDtExpression.Add(o => o.EXECUTE_ROLE_ID == this.EXECUTE_ROLE_ID.Value);
                }

                search.listVHisUserGroupTempDtExpression.AddRange(listVHisUserGroupTempDtExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisUserGroupTempDtExpression.Clear();
                search.listVHisUserGroupTempDtExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
