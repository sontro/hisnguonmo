using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCard
{
    public class HisCarerCardFilterQuery : HisCarerCardFilter
    {
        public HisCarerCardFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_CARER_CARD, bool>>> listHisCarerCardExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CARER_CARD, bool>>>();



        internal HisCarerCardSO Query()
        {
            HisCarerCardSO search = new HisCarerCardSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisCarerCardExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisCarerCardExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisCarerCardExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisCarerCardExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisCarerCardExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisCarerCardExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisCarerCardExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisCarerCardExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisCarerCardExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisCarerCardExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.IS_LOST.HasValue)
                {
                    if (this.IS_LOST.Value)
                    {
                        listHisCarerCardExpression.Add(o => o.IS_LOST.HasValue && o.IS_LOST == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisCarerCardExpression.Add(o => !o.IS_LOST.HasValue || o.IS_LOST != Constant.IS_TRUE);
                    }
                }
                if (this.IS_BORROWED.HasValue)
                {
                    if (this.IS_BORROWED.Value)
                    {
                        listHisCarerCardExpression.Add(o => o.IS_BORROWED.HasValue && o.IS_BORROWED == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisCarerCardExpression.Add(o => !o.IS_BORROWED.HasValue || o.IS_BORROWED != Constant.IS_TRUE);
                    }
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisCarerCardExpression.Add(o =>
                        o.CARER_CARD_NUMBER.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (!String.IsNullOrEmpty(this.CARER_CARD_NUMBER__EXACT))
                {
                    listHisCarerCardExpression.Add(o => o.CARER_CARD_NUMBER == this.CARER_CARD_NUMBER__EXACT);
                }

                search.listHisCarerCardExpression.AddRange(listHisCarerCardExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisCarerCardExpression.Clear();
                search.listHisCarerCardExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
