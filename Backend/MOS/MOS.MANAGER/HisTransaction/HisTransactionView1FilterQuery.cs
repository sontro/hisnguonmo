using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransaction
{
    public partial class HisTransactionView1FilterQuery : HisTransactionView1Filter
    {
        public HisTransactionView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TRANSACTION_1, bool>>> listVHisTransaction1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TRANSACTION_1, bool>>>();



        internal HisTransactionSO Query()
        {
            HisTransactionSO search = new HisTransactionSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTransaction1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisTransaction1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTransaction1Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTransaction1Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTransaction1Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTransaction1Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTransaction1Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTransaction1Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTransaction1Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTransaction1Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.ACCOUNT_BOOK_ID.HasValue)
                {
                    listVHisTransaction1Expression.Add(o => o.ACCOUNT_BOOK_ID == this.ACCOUNT_BOOK_ID.Value);
                }
                if (this.TRANSACTION_TYPE_IDs != null)
                {
                    listVHisTransaction1Expression.Add(o => this.TRANSACTION_TYPE_IDs.Contains(o.TRANSACTION_TYPE_ID));
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisTransaction1Expression.Add(o => o.TREATMENT_ID.HasValue && this.TREATMENT_IDs.Contains(o.TREATMENT_ID.Value));
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisTransaction1Expression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.IS_CANCEL.HasValue && this.IS_CANCEL.Value)
                {
                    listVHisTransaction1Expression.Add(o => o.IS_CANCEL.HasValue && o.IS_CANCEL.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_CANCEL.HasValue && !this.IS_CANCEL.Value)
                {
                    listVHisTransaction1Expression.Add(o => !o.IS_CANCEL.HasValue || o.IS_CANCEL.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (!String.IsNullOrEmpty(this.TRANSACTION_CODE__EXACT))
                {
                    listVHisTransaction1Expression.Add(o => o.TRANSACTION_CODE == this.TRANSACTION_CODE__EXACT);
                }
                if (this.HAS_DEBT_BILL_ID.HasValue && this.HAS_DEBT_BILL_ID.Value)
                {
                    listVHisTransaction1Expression.Add(o => o.DEBT_BILL_ID.HasValue);
                }
                if (this.HAS_DEBT_BILL_ID.HasValue && !this.HAS_DEBT_BILL_ID.Value)
                {
                    listVHisTransaction1Expression.Add(o => !o.DEBT_BILL_ID.HasValue);
                }
                if (this.TRANSACTION_TIME_FROM.HasValue)
                {
                    listVHisTransaction1Expression.Add(o => o.TRANSACTION_TIME >= this.TRANSACTION_TIME_FROM.Value);
                }
                if (this.TRANSACTION_TIME_TO.HasValue)
                {
                    listVHisTransaction1Expression.Add(o => o.TRANSACTION_TIME <= this.TRANSACTION_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTransaction1Expression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisTransaction1Expression.Add(o => o.CASHIER_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.CASHIER_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TIG_TRANSACTION_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TRANSACTION_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.DEBT_COLLECTION_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.DEBT_COLLECTION_USERNAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisTransaction1Expression.AddRange(listVHisTransaction1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTransaction1Expression.Clear();
                search.listVHisTransaction1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
