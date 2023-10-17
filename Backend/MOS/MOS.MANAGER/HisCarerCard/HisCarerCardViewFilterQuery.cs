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
    public class HisCarerCardViewFilterQuery : HisCarerCardViewFilter
    {
        public HisCarerCardViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_CARER_CARD, bool>>> listVHisCarerCardExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_CARER_CARD, bool>>>();

        

        internal HisCarerCardSO Query()
        {
            HisCarerCardSO search = new HisCarerCardSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisCarerCardExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisCarerCardExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisCarerCardExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisCarerCardExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisCarerCardExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisCarerCardExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisCarerCardExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisCarerCardExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisCarerCardExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisCarerCardExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.IS_LOST.HasValue)
                {
                    if (this.IS_LOST.Value)
                    {
                        listVHisCarerCardExpression.Add(o => o.IS_LOST.HasValue && o.IS_LOST == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisCarerCardExpression.Add(o => !o.IS_LOST.HasValue || o.IS_LOST != Constant.IS_TRUE);
                    }
                }
                if (this.IS_BORROWED.HasValue)
                {
                    if (this.IS_BORROWED.Value)
                    {
                        listVHisCarerCardExpression.Add(o => o.IS_BORROWED.HasValue && o.IS_BORROWED == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisCarerCardExpression.Add(o => !o.IS_BORROWED.HasValue || o.IS_BORROWED != Constant.IS_TRUE);
                    }
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisCarerCardExpression.Add(o =>
                        o.CARER_CARD_NUMBER.ToLower().Contains(this.KEY_WORD)
                        );
                }
                
                search.listVHisCarerCardExpression.AddRange(listVHisCarerCardExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisCarerCardExpression.Clear();
                search.listVHisCarerCardExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
