using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepositReason
{
    public class HisDepositReasonFilterQuery : HisDepositReasonFilter
    {
        public HisDepositReasonFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_DEPOSIT_REASON, bool>>> listHisDepositReasonExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEPOSIT_REASON, bool>>>();

        

        internal HisDepositReasonSO Query()
        {
            HisDepositReasonSO search = new HisDepositReasonSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisDepositReasonExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisDepositReasonExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisDepositReasonExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisDepositReasonExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisDepositReasonExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisDepositReasonExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisDepositReasonExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisDepositReasonExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisDepositReasonExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisDepositReasonExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.IS_COMMON.HasValue)
                {
                    listHisDepositReasonExpression.Add(o => o.IS_COMMON == this.IS_COMMON.Value);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisDepositReasonExpression.Add(o =>
                        o.ABBREVIATION.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPOSIT_REASON_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPOSIT_REASON_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisDepositReasonExpression.AddRange(listHisDepositReasonExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisDepositReasonExpression.Clear();
                search.listHisDepositReasonExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
