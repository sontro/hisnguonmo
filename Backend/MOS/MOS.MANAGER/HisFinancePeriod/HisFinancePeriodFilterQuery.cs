using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFinancePeriod
{
    public class HisFinancePeriodFilterQuery : HisFinancePeriodFilter
    {
        public HisFinancePeriodFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_FINANCE_PERIOD, bool>>> listHisFinancePeriodExpression = new List<System.Linq.Expressions.Expression<Func<HIS_FINANCE_PERIOD, bool>>>();

        

        internal HisFinancePeriodSO Query()
        {
            HisFinancePeriodSO search = new HisFinancePeriodSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisFinancePeriodExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisFinancePeriodExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisFinancePeriodExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisFinancePeriodExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisFinancePeriodExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisFinancePeriodExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisFinancePeriodExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisFinancePeriodExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisFinancePeriodExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisFinancePeriodExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisFinancePeriodExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listHisFinancePeriodExpression.AddRange(listHisFinancePeriodExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisFinancePeriodExpression.Clear();
                search.listHisFinancePeriodExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
