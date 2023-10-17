using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNumOrderIssue
{
    public class HisNumOrderIssueFilterQuery : HisNumOrderIssueFilter
    {
        public HisNumOrderIssueFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_NUM_ORDER_ISSUE, bool>>> listHisNumOrderIssueExpression = new List<System.Linq.Expressions.Expression<Func<HIS_NUM_ORDER_ISSUE, bool>>>();

        

        internal HisNumOrderIssueSO Query()
        {
            HisNumOrderIssueSO search = new HisNumOrderIssueSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisNumOrderIssueExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisNumOrderIssueExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisNumOrderIssueExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisNumOrderIssueExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisNumOrderIssueExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisNumOrderIssueExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisNumOrderIssueExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisNumOrderIssueExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisNumOrderIssueExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisNumOrderIssueExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listHisNumOrderIssueExpression.AddRange(listHisNumOrderIssueExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisNumOrderIssueExpression.Clear();
                search.listHisNumOrderIssueExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
