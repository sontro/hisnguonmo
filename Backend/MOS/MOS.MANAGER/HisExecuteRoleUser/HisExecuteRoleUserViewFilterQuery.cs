using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteRoleUser
{
    public class HisExecuteRoleUserViewFilterQuery : HisExecuteRoleUserViewFilter
    {
        public HisExecuteRoleUserViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXECUTE_ROLE_USER, bool>>> listVHisExecuteRoleUserExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXECUTE_ROLE_USER, bool>>>();

        

        internal HisExecuteRoleUserSO Query()
        {
            HisExecuteRoleUserSO search = new HisExecuteRoleUserSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisExecuteRoleUserExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisExecuteRoleUserExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisExecuteRoleUserExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisExecuteRoleUserExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisExecuteRoleUserExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisExecuteRoleUserExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisExecuteRoleUserExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisExecuteRoleUserExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisExecuteRoleUserExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisExecuteRoleUserExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisExecuteRoleUserExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisExecuteRoleUserExpression.AddRange(listVHisExecuteRoleUserExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExecuteRoleUserExpression.Clear();
                search.listVHisExecuteRoleUserExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
