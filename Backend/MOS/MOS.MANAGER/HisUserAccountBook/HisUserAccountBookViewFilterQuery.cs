using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserAccountBook
{
    public class HisUserAccountBookViewFilterQuery : HisUserAccountBookViewFilter
    {
        public HisUserAccountBookViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_USER_ACCOUNT_BOOK, bool>>> listVHisUserAccountBookExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_USER_ACCOUNT_BOOK, bool>>>();



        internal HisUserAccountBookSO Query()
        {
            HisUserAccountBookSO search = new HisUserAccountBookSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisUserAccountBookExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisUserAccountBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisUserAccountBookExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisUserAccountBookExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisUserAccountBookExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisUserAccountBookExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisUserAccountBookExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisUserAccountBookExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisUserAccountBookExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisUserAccountBookExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisUserAccountBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ACCOUNT_BOOK_ID.HasValue)
                {
                    listVHisUserAccountBookExpression.Add(o => o.ACCOUNT_BOOK_ID == this.ACCOUNT_BOOK_ID.Value);
                }
                if (this.ACCOUNT_BOOK_IDs != null)
                {
                    listVHisUserAccountBookExpression.Add(o => this.ACCOUNT_BOOK_IDs.Contains(o.ACCOUNT_BOOK_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.LOGINNAME__EXACT))
                {
                    listVHisUserAccountBookExpression.Add(o => o.LOGINNAME == this.LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.ACCOUNT_BOOK_CODE__EXACT))
                {
                    listVHisUserAccountBookExpression.Add(o => o.ACCOUNT_BOOK_CODE == this.ACCOUNT_BOOK_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TEMPLATE_CODE__EXACT))
                {
                    listVHisUserAccountBookExpression.Add(o => o.TEMPLATE_CODE == this.TEMPLATE_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.SYMBOL_CODE__EXACT))
                {
                    listVHisUserAccountBookExpression.Add(o => o.SYMBOL_CODE == this.SYMBOL_CODE__EXACT);
                }
                if (this.ACCOUNT_BOOK_IS_ACTIVE.HasValue)
                {
                    listVHisUserAccountBookExpression.Add(o => o.ACCOUNT_BOOK_IS_ACTIVE == this.ACCOUNT_BOOK_IS_ACTIVE.Value);
                }
                if (this.IS_OUT_OF_BILL.HasValue)
                {
                    if (this.IS_OUT_OF_BILL.Value)
                    {
                        listVHisUserAccountBookExpression.Add(o => o.CURRENT_NUM_ORDER >= (o.FROM_NUM_ORDER + o.TOTAL - 1));
                    }
                    else
                    {
                        listVHisUserAccountBookExpression.Add(o => o.CURRENT_NUM_ORDER < (o.FROM_NUM_ORDER + o.TOTAL - 1));
                    }
                }
                if (this.FOR_BILL.HasValue)
                {
                    if (this.FOR_BILL.Value)
                    {
                        listVHisUserAccountBookExpression.Add(o => o.IS_FOR_BILL.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisUserAccountBookExpression.Add(o => o.IS_FOR_BILL.Value != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.FOR_DEPOSIT.HasValue)
                {
                    if (this.FOR_DEPOSIT.Value)
                    {
                        listVHisUserAccountBookExpression.Add(o => o.IS_FOR_DEPOSIT.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisUserAccountBookExpression.Add(o => o.IS_FOR_DEPOSIT.Value != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.FOR_REPAY.HasValue)
                {
                    if (this.FOR_REPAY.Value)
                    {
                        listVHisUserAccountBookExpression.Add(o => o.IS_FOR_REPAY.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisUserAccountBookExpression.Add(o => o.IS_FOR_REPAY.Value != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisUserAccountBookExpression.Add(o => o.ACCOUNT_BOOK_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ACCOUNT_BOOK_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.DESCRIPTION.ToLower().Contains(this.KEY_WORD)
                        || o.LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.SYMBOL_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.TEMPLATE_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisUserAccountBookExpression.AddRange(listVHisUserAccountBookExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisUserAccountBookExpression.Clear();
                search.listVHisUserAccountBookExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
