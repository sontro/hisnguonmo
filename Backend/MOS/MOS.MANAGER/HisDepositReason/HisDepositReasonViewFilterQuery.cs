using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepositReason
{
    public class HisDepositReasonViewFilterQuery : HisDepositReasonViewFilter
    {
        public HisDepositReasonViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_DEPOSIT_REASON, bool>>> listVHisDepositReasonExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DEPOSIT_REASON, bool>>>();

        

        internal HisDepositReasonSO Query()
        {
            HisDepositReasonSO search = new HisDepositReasonSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisDepositReasonExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisDepositReasonExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisDepositReasonExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisDepositReasonExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisDepositReasonExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisDepositReasonExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisDepositReasonExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisDepositReasonExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisDepositReasonExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisDepositReasonExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisDepositReasonExpression.AddRange(listVHisDepositReasonExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisDepositReasonExpression.Clear();
                search.listVHisDepositReasonExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
