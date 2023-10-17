using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCaroAccountBook
{
    public class HisCaroAccountBookViewFilterQuery : HisCaroAccountBookViewFilter
    {
        public HisCaroAccountBookViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_CARO_ACCOUNT_BOOK, bool>>> listVHisCaroAccountBookExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_CARO_ACCOUNT_BOOK, bool>>>();

        

        internal HisCaroAccountBookSO Query()
        {
            HisCaroAccountBookSO search = new HisCaroAccountBookSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisCaroAccountBookExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisCaroAccountBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisCaroAccountBookExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisCaroAccountBookExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisCaroAccountBookExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisCaroAccountBookExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisCaroAccountBookExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisCaroAccountBookExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisCaroAccountBookExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisCaroAccountBookExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisCaroAccountBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ACCOUNT_BOOK_ID.HasValue)
                {
                    listVHisCaroAccountBookExpression.Add(o => o.ACCOUNT_BOOK_ID == this.ACCOUNT_BOOK_ID.Value);
                }
                if (this.ACCOUNT_BOOK_IDs != null)
                {
                    listVHisCaroAccountBookExpression.Add(o => this.ACCOUNT_BOOK_IDs.Contains(o.ACCOUNT_BOOK_ID));
                }
                if (this.CASHIER_ROOM_ID.HasValue)
                {
                    listVHisCaroAccountBookExpression.Add(o => o.CASHIER_ROOM_ID == this.CASHIER_ROOM_ID.Value);
                }
                if (this.CASHIER_ROOM_IDs != null)
                {
                    listVHisCaroAccountBookExpression.Add(o => this.CASHIER_ROOM_IDs.Contains(o.CASHIER_ROOM_ID));
                }
                
                if (!String.IsNullOrWhiteSpace(this.ACCOUNT_BOOK_CODE__EXACT))
                {
                    listVHisCaroAccountBookExpression.Add(o => o.ACCOUNT_BOOK_CODE == this.ACCOUNT_BOOK_CODE__EXACT);
                }
                
                if (this.FOR_BILL.HasValue)
                {
                    if (this.FOR_BILL.Value)
                    {
                        listVHisCaroAccountBookExpression.Add(o => o.IS_FOR_BILL.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisCaroAccountBookExpression.Add(o => o.IS_FOR_BILL.Value != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.FOR_DEPOSIT.HasValue)
                {
                    if (this.FOR_DEPOSIT.Value)
                    {
                        listVHisCaroAccountBookExpression.Add(o => o.IS_FOR_DEPOSIT.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisCaroAccountBookExpression.Add(o => o.IS_FOR_DEPOSIT.Value != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.FOR_REPAY.HasValue)
                {
                    if (this.FOR_REPAY.Value)
                    {
                        listVHisCaroAccountBookExpression.Add(o => o.IS_FOR_REPAY.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisCaroAccountBookExpression.Add(o => o.IS_FOR_REPAY.Value != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }

                if (this.ACCOUNT_BOOK_IS_ACTIVE.HasValue && this.ACCOUNT_BOOK_IS_ACTIVE.Value)
                {
                    listVHisCaroAccountBookExpression.Add(o => o.ACCOUNT_BOOK_IS_ACTIVE == Constant.IS_TRUE);
                }
                if (this.ACCOUNT_BOOK_IS_ACTIVE.HasValue && !this.ACCOUNT_BOOK_IS_ACTIVE.Value)
                {
                    listVHisCaroAccountBookExpression.Add(o => !o.ACCOUNT_BOOK_IS_ACTIVE.HasValue || o.ACCOUNT_BOOK_IS_ACTIVE != Constant.IS_TRUE);
                }
                if (this.IS_OUT_OF_BILL.HasValue)
                {
                    if (this.IS_OUT_OF_BILL.Value)
                    {
                        listVHisCaroAccountBookExpression.Add(o => o.CURRENT_NUM_ORDER >= (o.FROM_NUM_ORDER + o.TOTAL - 1));
                    }
                    else
                    {
                        listVHisCaroAccountBookExpression.Add(o => o.CURRENT_NUM_ORDER < (o.FROM_NUM_ORDER + o.TOTAL - 1));
                    }
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisCaroAccountBookExpression.Add(o => o.ACCOUNT_BOOK_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ACCOUNT_BOOK_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.ACCOUNT_BOOK_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ACCOUNT_BOOK_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.CASHIER_ROOM_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.CASHIER_ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisCaroAccountBookExpression.AddRange(listVHisCaroAccountBookExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisCaroAccountBookExpression.Clear();
                search.listVHisCaroAccountBookExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
