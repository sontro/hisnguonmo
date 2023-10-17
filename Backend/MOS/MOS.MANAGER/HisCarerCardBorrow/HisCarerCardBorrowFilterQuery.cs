using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    public class HisCarerCardBorrowFilterQuery : HisCarerCardBorrowFilter
    {
        public HisCarerCardBorrowFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_CARER_CARD_BORROW, bool>>> listHisCarerCardBorrowExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CARER_CARD_BORROW, bool>>>();



        internal HisCarerCardBorrowSO Query()
        {
            HisCarerCardBorrowSO search = new HisCarerCardBorrowSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisCarerCardBorrowExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisCarerCardBorrowExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisCarerCardBorrowExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisCarerCardBorrowExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisCarerCardBorrowExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisCarerCardBorrowExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisCarerCardBorrowExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisCarerCardBorrowExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisCarerCardBorrowExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisCarerCardBorrowExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.CARER_CARD_ID.HasValue)
                {
                    listHisCarerCardBorrowExpression.Add(o => o.CARER_CARD_ID == this.CARER_CARD_ID.Value);
                }
                if (this.CARER_CARD_IDs != null)
                {
                    listHisCarerCardBorrowExpression.Add(o => this.CARER_CARD_IDs.Contains(o.CARER_CARD_ID));
                }

                if (this.TREATMENT_ID.HasValue)
                {
                    listHisCarerCardBorrowExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listHisCarerCardBorrowExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.HAS_GIVE_BACK_TIME.HasValue)
                {
                    if (this.HAS_GIVE_BACK_TIME.Value)
                    {
                        listHisCarerCardBorrowExpression.Add(o => o.GIVE_BACK_TIME.HasValue);
                    }
                    else
                    {
                        listHisCarerCardBorrowExpression.Add(o => !o.GIVE_BACK_TIME.HasValue);
                    }
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisCarerCardBorrowExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.GIVING_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.GIVING_USERNAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (this.HAS_NO__OR__LOWER_THAN_GIVE_BACK_TIME.HasValue)
                {
                    listHisCarerCardBorrowExpression.Add(o => !o.GIVE_BACK_TIME.HasValue || o.GIVE_BACK_TIME.Value > this.HAS_NO__OR__LOWER_THAN_GIVE_BACK_TIME);
                }

                search.listHisCarerCardBorrowExpression.AddRange(listHisCarerCardBorrowExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisCarerCardBorrowExpression.Clear();
                search.listHisCarerCardBorrowExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
