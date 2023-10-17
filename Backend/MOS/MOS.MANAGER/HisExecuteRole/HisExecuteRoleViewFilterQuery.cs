using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteRole
{
    public class HisExecuteRoleViewFilterQuery : HisExecuteRoleViewFilter
    {
        public HisExecuteRoleViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXECUTE_ROLE, bool>>> listVHisExecuteRoleExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXECUTE_ROLE, bool>>>();

        

        internal HisExecuteRoleSO Query()
        {
            HisExecuteRoleSO search = new HisExecuteRoleSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisExecuteRoleExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisExecuteRoleExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisExecuteRoleExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisExecuteRoleExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisExecuteRoleExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisExecuteRoleExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisExecuteRoleExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisExecuteRoleExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisExecuteRoleExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisExecuteRoleExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisExecuteRoleExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisExecuteRoleExpression.AddRange(listVHisExecuteRoleExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExecuteRoleExpression.Clear();
                search.listVHisExecuteRoleExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
