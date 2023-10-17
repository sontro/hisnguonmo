using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckSummary
{
    public class HisMrCheckSummaryViewFilterQuery : HisMrCheckSummaryViewFilter
    {
        public HisMrCheckSummaryViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MR_CHECK_SUMMARY, bool>>> listVHisMrCheckSummaryExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MR_CHECK_SUMMARY, bool>>>();

        

        internal HisMrCheckSummarySO Query()
        {
            HisMrCheckSummarySO search = new HisMrCheckSummarySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMrCheckSummaryExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisMrCheckSummaryExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMrCheckSummaryExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMrCheckSummaryExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMrCheckSummaryExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMrCheckSummaryExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMrCheckSummaryExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMrCheckSummaryExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMrCheckSummaryExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMrCheckSummaryExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisMrCheckSummaryExpression.AddRange(listVHisMrCheckSummaryExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMrCheckSummaryExpression.Clear();
                search.listVHisMrCheckSummaryExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
