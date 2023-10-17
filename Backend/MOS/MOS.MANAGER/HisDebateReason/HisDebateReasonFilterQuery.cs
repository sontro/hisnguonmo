using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateReason
{
    public class HisDebateReasonFilterQuery : HisDebateReasonFilter
    {
        public HisDebateReasonFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_REASON, bool>>> listHisDebateReasonExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_REASON, bool>>>();

        

        internal HisDebateReasonSO Query()
        {
            HisDebateReasonSO search = new HisDebateReasonSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisDebateReasonExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisDebateReasonExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisDebateReasonExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisDebateReasonExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisDebateReasonExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisDebateReasonExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisDebateReasonExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisDebateReasonExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisDebateReasonExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisDebateReasonExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (!String.IsNullOrEmpty(this.DEBATE_REASON_CODE__EXACT))
                {
                    listHisDebateReasonExpression.Add(o => o.DEBATE_REASON_CODE == this.DEBATE_REASON_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.DEBATE_REASON_NAME__EXACT))
                {
                    listHisDebateReasonExpression.Add(o => o.DEBATE_REASON_NAME == this.DEBATE_REASON_NAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisDebateReasonExpression.Add(o =>
                        o.DEBATE_REASON_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DEBATE_REASON_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                search.listHisDebateReasonExpression.AddRange(listHisDebateReasonExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisDebateReasonExpression.Clear();
                search.listHisDebateReasonExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
