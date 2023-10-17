using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransaction
{
    public class HisTransactionViewFilterQuery : HisTransactionViewFilter
    {
        public HisTransactionViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TRANSACTION, bool>>> listVHisTransactionExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TRANSACTION, bool>>>();



        internal HisTransactionSO Query()
        {
            HisTransactionSO search = new HisTransactionSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisTransactionExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listVHisTransactionExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listVHisTransactionExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listVHisTransactionExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.ACCOUNT_BOOK_ID.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.ACCOUNT_BOOK_ID == this.ACCOUNT_BOOK_ID.Value);
                }

                if (this.TRANSACTION_TYPE_IDs != null)
                {
                    search.listVHisTransactionExpression.Add(o => this.TRANSACTION_TYPE_IDs.Contains(o.TRANSACTION_TYPE_ID));
                }
                if (this.TREATMENT_IDs != null)
                {
                    search.listVHisTransactionExpression.Add(o => o.TREATMENT_ID.HasValue && this.TREATMENT_IDs.Contains(o.TREATMENT_ID.Value));
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.TREATMENT_ID.HasValue && o.TREATMENT_ID.Value == this.TREATMENT_ID.Value);
                }
                if (this.IS_CANCEL.HasValue && this.IS_CANCEL.Value)
                {
                    search.listVHisTransactionExpression.Add(o => o.IS_CANCEL.HasValue && o.IS_CANCEL.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_CANCEL.HasValue && !this.IS_CANCEL.Value)
                {
                    search.listVHisTransactionExpression.Add(o => !o.IS_CANCEL.HasValue || o.IS_CANCEL.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    search.listVHisTransactionExpression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.ACCOUNT_BOOK_CODE__EXACT))
                {
                    search.listVHisTransactionExpression.Add(o => o.ACCOUNT_BOOK_CODE == this.ACCOUNT_BOOK_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TRANSACTION_CODE__EXACT))
                {
                    listVHisTransactionExpression.Add(o => o.TRANSACTION_CODE == this.TRANSACTION_CODE__EXACT);
                }
                if (this.CASHOUT_IDs != null)
                {
                    search.listVHisTransactionExpression.Add(o => o.CASHOUT_ID.HasValue && this.CASHOUT_IDs.Contains(o.CASHOUT_ID.Value));
                }
                if (this.CASHOUT_ID.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.CASHOUT_ID.HasValue && o.CASHOUT_ID.Value == this.CASHOUT_ID.Value);
                }
                if (this.ACCOUNT_BOOK_IDs != null)
                {
                    search.listVHisTransactionExpression.Add(o => this.ACCOUNT_BOOK_IDs.Contains(o.ACCOUNT_BOOK_ID));
                }
                if (this.NUM_ORDER_FROM.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.NUM_ORDER >= this.NUM_ORDER_FROM.Value);
                }
                if (this.NUM_ORDER_TO.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.NUM_ORDER <= this.NUM_ORDER_TO.Value);
                }
                if (this.NUM_ORDER__EQUAL.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.NUM_ORDER == this.NUM_ORDER__EQUAL.Value);
                }
                if (this.TRANSACTION_TIME_FROM.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.TRANSACTION_TIME >= this.TRANSACTION_TIME_FROM.Value);
                }
                if (this.TRANSACTION_TIME_TO.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.TRANSACTION_TIME <= this.TRANSACTION_TIME_TO.Value);
                }
                if (this.HAS_CASHOUT.HasValue && this.HAS_CASHOUT.Value)
                {
                    search.listVHisTransactionExpression.Add(o => o.CASHOUT_ID.HasValue);
                }
                if (this.HAS_CASHOUT.HasValue && !this.HAS_CASHOUT.Value)
                {
                    search.listVHisTransactionExpression.Add(o => !o.CASHOUT_ID.HasValue);
                }
                if (!String.IsNullOrEmpty(this.NATIONAL_TRANSACTION_CODE__EXACT))
                {
                    search.listVHisTransactionExpression.Add(o => o.NATIONAL_TRANSACTION_CODE == this.NATIONAL_TRANSACTION_CODE__EXACT);
                }
                if (this.HAS_NATIONAL_TRANSACTION_CODE.HasValue)
                {
                    if (this.HAS_NATIONAL_TRANSACTION_CODE.Value)
                    {
                        listVHisTransactionExpression.Add(o => o.NATIONAL_TRANSACTION_CODE != null);
                    }
                    else
                    {
                        listVHisTransactionExpression.Add(o => o.NATIONAL_TRANSACTION_CODE == null);
                    }
                }
                if (this.BILL_TYPE_ID.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.BILL_TYPE_ID.HasValue && o.BILL_TYPE_ID.Value == this.BILL_TYPE_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.SYMBOL_CODE__EXACT))
                {
                    search.listVHisTransactionExpression.Add(o => o.SYMBOL_CODE == this.SYMBOL_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TEMPLATE_CODE__EXACT))
                {
                    search.listVHisTransactionExpression.Add(o => o.TEMPLATE_CODE == this.TEMPLATE_CODE__EXACT);
                }

                if (this.BILL_TYPE_IS_NULL_OR_EQUAL_1.HasValue)
                {
                    if (this.BILL_TYPE_IS_NULL_OR_EQUAL_1.Value)
                    {
                        listVHisTransactionExpression.Add(o => !o.BILL_TYPE_ID.HasValue || (o.BILL_TYPE_ID.HasValue && o.BILL_TYPE_ID.Value == (long)1));
                    }
                    else
                    {
                        listVHisTransactionExpression.Add(o => o.BILL_TYPE_ID.HasValue && o.BILL_TYPE_ID.Value != (long)1);
                    }
                }
                if (this.SALE_TYPE_ID.HasValue)
                {
                    listVHisTransactionExpression.Add(o => o.SALE_TYPE_ID.HasValue && o.SALE_TYPE_ID.Value == this.SALE_TYPE_ID.Value);
                }
                if (this.HAS_SALE_TYPE_ID.HasValue)
                {
                    if (this.HAS_SALE_TYPE_ID.Value)
                    {
                        listVHisTransactionExpression.Add(o => o.SALE_TYPE_ID.HasValue);
                    }
                    else
                    {
                        listVHisTransactionExpression.Add(o => !o.SALE_TYPE_ID.HasValue);
                    }
                }
                if (this.HAS_DEBT_BILL_ID.HasValue)
                {
                    if (this.HAS_DEBT_BILL_ID.Value)
                    {
                        listVHisTransactionExpression.Add(o => o.DEBT_BILL_ID.HasValue);
                    }
                    else
                    {
                        listVHisTransactionExpression.Add(o => !o.DEBT_BILL_ID.HasValue);
                    }
                }
                if (this.DEBT_BILL_ID.HasValue)
                {
                    listVHisTransactionExpression.Add(o => o.DEBT_BILL_ID.HasValue && o.DEBT_BILL_ID.Value == this.DEBT_BILL_ID.Value);
                }
                if (this.DEBT_BILL_IDs != null)
                {
                    listVHisTransactionExpression.Add(o => o.DEBT_BILL_ID.HasValue && this.DEBT_BILL_IDs.Contains(o.DEBT_BILL_ID.Value));
                }

                if (this.IS_DEBT_COLLECTION.HasValue)
                {
                    if (this.IS_DEBT_COLLECTION.Value)
                    {
                        listVHisTransactionExpression.Add(o => o.IS_DEBT_COLLECTION.HasValue && o.IS_DEBT_COLLECTION.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisTransactionExpression.Add(o => !o.IS_DEBT_COLLECTION.HasValue || o.IS_DEBT_COLLECTION.Value != Constant.IS_TRUE);
                    }
                }
                if (!String.IsNullOrWhiteSpace(this.ACCOUNT_BOOK_NAME))
                {
                    listVHisTransactionExpression.Add(o => o.ACCOUNT_BOOK_NAME.ToLower() == this.ACCOUNT_BOOK_NAME.ToLower());
                }

                if (this.HAS_INVOICE_CODE.HasValue && this.HAS_INVOICE_CODE.Value)
                {
                    listVHisTransactionExpression.Add(o => o.INVOICE_CODE != null);
                }
                if (this.HAS_INVOICE_CODE.HasValue && !this.HAS_INVOICE_CODE.Value)
                {
                    listVHisTransactionExpression.Add(o => o.INVOICE_CODE == null);
                }
                if (!String.IsNullOrWhiteSpace(this.INVOICE_CODE__EXACT))
                {
                    listVHisTransactionExpression.Add(o => o.INVOICE_CODE == this.INVOICE_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TRANS_REQ_CODE__EXACT))
                {
                    listVHisTransactionExpression.Add(o => o.TRANS_REQ_CODE == this.TRANS_REQ_CODE__EXACT);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    search.listVHisTransactionExpression.Add(o => o.TRANSACTION_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ACCOUNT_BOOK_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ACCOUNT_BOOK_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TRANSACTION_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.PAY_FORM_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.NATIONAL_TRANSACTION_CODE.ToLower().Contains(this.KEY_WORD));
                }
                if (this.CASHIER_ROOM_ID.HasValue)
                {
                    search.listVHisTransactionExpression.Add(o => o.CASHIER_ROOM_ID == this.CASHIER_ROOM_ID.Value);
                }
                search.listVHisTransactionExpression.AddRange(listVHisTransactionExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
                search.DynamicColumns = this.ColumnParams;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTransactionExpression.Clear();
                search.listVHisTransactionExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
