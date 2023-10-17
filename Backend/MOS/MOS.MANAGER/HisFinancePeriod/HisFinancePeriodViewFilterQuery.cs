using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFinancePeriod
{
    public class HisFinancePeriodViewFilterQuery : HisFinancePeriodViewFilter
    {
        public HisFinancePeriodViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_FINANCE_PERIOD, bool>>> listVHisFinancePeriodExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_FINANCE_PERIOD, bool>>>();

        

        internal HisFinancePeriodSO Query()
        {
            HisFinancePeriodSO search = new HisFinancePeriodSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisFinancePeriodExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisFinancePeriodExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisFinancePeriodExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisFinancePeriodExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisFinancePeriodExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisFinancePeriodExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisFinancePeriodExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisFinancePeriodExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisFinancePeriodExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisFinancePeriodExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.PREVIOUS_ID.HasValue)
                {
                    listVHisFinancePeriodExpression.Add(o => o.PREVIOUS_ID.HasValue && o.PREVIOUS_ID == this.PREVIOUS_ID.Value);
                }
                if (this.BRANCH_ID.HasValue)
                {
                    listVHisFinancePeriodExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.PERIOD_TIME_FROM.HasValue)
                {
                    listVHisFinancePeriodExpression.Add(o => o.PERIOD_TIME >= this.PERIOD_TIME_FROM.Value);
                }
                if (this.PERIOD_TIME_TO.HasValue)
                {
                    listVHisFinancePeriodExpression.Add(o => o.PERIOD_TIME <= this.PERIOD_TIME_TO.Value);
                }
                if (this.PREVIOUS_PERIOD_TIME__NULL_OR_LESS.HasValue)
                {
                    listVHisFinancePeriodExpression.Add(o => !o.PREVIOUS_PERIOD_TIME.HasValue || o.PREVIOUS_PERIOD_TIME.Value < this.PREVIOUS_PERIOD_TIME__NULL_OR_LESS.Value);
                }

                search.listVHisFinancePeriodExpression.AddRange(listVHisFinancePeriodExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisFinancePeriodExpression.Clear();
                search.listVHisFinancePeriodExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
