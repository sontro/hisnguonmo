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
    public partial class HisTransactionView5FilterQuery : HisTransactionView5Filter
    {
        public HisTransactionView5FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TRANSACTION_5, bool>>> listVHisTransaction5Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TRANSACTION_5, bool>>>();

        

        internal HisTransactionSO Query()
        {
            HisTransactionSO search = new HisTransactionSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTransaction5Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisTransaction5Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTransaction5Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTransaction5Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTransaction5Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTransaction5Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTransaction5Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTransaction5Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTransaction5Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTransaction5Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.ACCOUNT_BOOK_ID.HasValue)
                {
                    listVHisTransaction5Expression.Add(o => o.ACCOUNT_BOOK_ID == this.ACCOUNT_BOOK_ID.Value);
                }
                if (this.TRANSACTION_TYPE_IDs != null)
                {
                    listVHisTransaction5Expression.Add(o => this.TRANSACTION_TYPE_IDs.Contains(o.TRANSACTION_TYPE_ID));
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisTransaction5Expression.Add(o => o.TREATMENT_ID.HasValue && this.TREATMENT_IDs.Contains(o.TREATMENT_ID.Value));
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisTransaction5Expression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.IS_CANCEL.HasValue && this.IS_CANCEL.Value)
                {
                    listVHisTransaction5Expression.Add(o => o.IS_CANCEL.HasValue && o.IS_CANCEL.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_CANCEL.HasValue && !this.IS_CANCEL.Value)
                {
                    listVHisTransaction5Expression.Add(o => !o.IS_CANCEL.HasValue || o.IS_CANCEL.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (!String.IsNullOrEmpty(this.TRANSACTION_CODE__EXACT))
                {
                    listVHisTransaction5Expression.Add(o => o.TRANSACTION_CODE == this.TRANSACTION_CODE__EXACT);
                }
                if (this.IS_DEBT_COLLECTION.HasValue)
                {
                    if (this.IS_DEBT_COLLECTION.Value)
                    {
                        listVHisTransaction5Expression.Add(o => o.IS_DEBT_COLLECTION.HasValue && o.IS_DEBT_COLLECTION.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisTransaction5Expression.Add(o => !o.IS_DEBT_COLLECTION.HasValue || o.IS_DEBT_COLLECTION.Value != Constant.IS_TRUE);
                    }
                }
                if (this.HAS_DEBT_BILL_ID.HasValue)
                {
                    if (this.HAS_DEBT_BILL_ID.Value)
                    {
                        listVHisTransaction5Expression.Add(o => o.DEBT_BILL_ID.HasValue);
                    }
                    else
                    {
                        listVHisTransaction5Expression.Add(o => !o.DEBT_BILL_ID.HasValue);
                    }
                }

                search.listVHisTransaction5Expression.AddRange(listVHisTransaction5Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTransaction5Expression.Clear();
                search.listVHisTransaction5Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
