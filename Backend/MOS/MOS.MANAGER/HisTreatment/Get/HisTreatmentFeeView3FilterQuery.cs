using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public class HisTreatmentFeeView3FilterQuery : HisTreatmentFeeView3Filter
    {
        public HisTreatmentFeeView3FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE_3, bool>>>  listVHisTreatmentFee3Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE_3, bool>>>();



        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisTreatmentFee3Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatmentFee3Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatmentFee3Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatmentFee3Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.PATIENT_ID.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.VIR_PATIENT_NAME))
                {
                    listVHisTreatmentFee3Expression.Add(o => o.TDL_PATIENT_NAME.ToLower().Contains(this.VIR_PATIENT_NAME.ToLower().Trim()));
                }
                if (this.IS_LOCK_HEIN.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == this.IS_LOCK_HEIN.Value);
                }
                if (this.TOTAL_HEIN_PRICE__GREATER_THAN.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.TOTAL_HEIN_PRICE.HasValue && o.TOTAL_HEIN_PRICE.Value > this.TOTAL_HEIN_PRICE__GREATER_THAN.Value);
                }
                if (this.PATIENT_IDs != null)
                {
                    listVHisTreatmentFee3Expression.Add(o => this.PATIENT_IDs.Contains(o.PATIENT_ID));
                }

                if (this.TDL_TREATMENT_TYPE_ID.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && o.TDL_TREATMENT_TYPE_ID.Value == this.TDL_TREATMENT_TYPE_ID.Value);
                }
                if (this.END_DEPARTMENT_ID.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.END_DEPARTMENT_ID.HasValue && o.END_DEPARTMENT_ID.Value == this.END_DEPARTMENT_ID.Value);
                }
                if (this.BRANCH_ID.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }

                if (this.TDL_TREATMENT_TYPE_IDs != null)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && this.TDL_TREATMENT_TYPE_IDs.Contains(o.TDL_TREATMENT_TYPE_ID.Value));
                }
                if (this.END_DEPARTMENT_IDs != null)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.END_DEPARTMENT_ID.HasValue && this.END_DEPARTMENT_IDs.Contains(o.END_DEPARTMENT_ID.Value));
                }
                if (this.BRANCH_IDs != null)
                {
                    listVHisTreatmentFee3Expression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }
                if (this.IN_DATE_FROM.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.IN_DATE >= this.IN_DATE_FROM.Value);
                }
                if (this.IN_DATE_TO.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.IN_DATE <= this.IN_DATE_TO.Value);
                }
                if (this.OUT_TIME_FROM.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.OUT_TIME >= this.OUT_TIME_FROM.Value);
                }
                if (this.OUT_TIME_TO.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.OUT_TIME <= this.OUT_TIME_TO.Value);
                }
                if (this.OUT_DATE_FROM.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.OUT_DATE >= this.OUT_DATE_FROM.Value);
                }
                if (this.OUT_DATE_TO.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.OUT_DATE <= this.OUT_DATE_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTreatmentFee3Expression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    listVHisTreatmentFee3Expression.Add(o => o.TDL_PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME))
                {
                    string keyword = this.KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME.Trim().ToLower();
                    listVHisTreatmentFee3Expression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(keyword)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(keyword)
                        || o.TREATMENT_CODE.ToLower().Contains(keyword)
                        || o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(keyword));
                }
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE == Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listVHisTreatmentFee3Expression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE != Constant.IS_TRUE);
                }

                if (this.FUND_ID.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.FUND_ID.HasValue && o.FUND_ID.Value == this.FUND_ID.Value);
                }
                if (this.FUND_IDs != null)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.FUND_ID.HasValue && this.FUND_IDs.Contains(o.FUND_ID.Value));
                }

                if (this.FUND_PAY_TIME_FROM.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.FUND_PAY_TIME.HasValue && o.FUND_PAY_TIME.Value >= this.FUND_PAY_TIME_FROM.Value);
                }
                if (this.FUND_PAY_TIME_TO.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.FUND_PAY_TIME.HasValue && o.FUND_PAY_TIME.Value <= this.FUND_PAY_TIME_TO.Value);
                }

                if (this.HAS_FUND_ID.HasValue)
                {
                    if (this.HAS_FUND_ID.Value)
                    {
                        listVHisTreatmentFee3Expression.Add(o => o.FUND_ID.HasValue);
                    }
                    else
                    {
                        listVHisTreatmentFee3Expression.Add(o => !o.FUND_ID.HasValue);
                    }
                }
                if (this.HAS_FUND_PAY_TIME.HasValue)
                {
                    if (this.HAS_FUND_PAY_TIME.Value)
                    {
                        listVHisTreatmentFee3Expression.Add(o => o.FUND_PAY_TIME.HasValue);
                    }
                    else
                    {
                        listVHisTreatmentFee3Expression.Add(o => !o.FUND_PAY_TIME.HasValue);
                    }
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisTreatmentFee3Expression.Add(o =>
                        o.STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_METHOD.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }
                if (this.LAST_DEPARTMENT_ID.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.LAST_DEPARTMENT_ID.HasValue && o.LAST_DEPARTMENT_ID.Value == this.LAST_DEPARTMENT_ID.Value);
                }
                if (this.LAST_DEPARTMENT_IDs != null)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.LAST_DEPARTMENT_ID.HasValue && this.LAST_DEPARTMENT_IDs.Contains(o.LAST_DEPARTMENT_ID.Value));
                }
                if (this.IN_DATE__EQUAL.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.IN_DATE == this.IN_DATE__EQUAL.Value);
                }
                if (this.OUT_DATE__EQUAL.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.OUT_DATE.HasValue && o.OUT_DATE.Value == this.OUT_DATE__EQUAL.Value);
                }

                if (this.IS_NEED_INVOICE.HasValue)
                {
                    if (this.IS_NEED_INVOICE.Value)
                    {
                        listVHisTreatmentFee3Expression.Add(o => (((o.TOTAL_PATIENT_PRICE ?? 0) > (o.TOTAL_BILL_AMOUNT ?? 0)) && ((o.TOTAL_DEPOSIT_AMOUNT ?? 0) > ((o.TOTAL_REPAY_AMOUNT ?? 0) + (o.TOTAL_BILL_TRANSFER_AMOUNT ?? 0))) && o.IS_ACTIVE == Constant.IS_TRUE) || (o.COUNT_TRANS_NOT_HAS_INVOICE.HasValue && o.COUNT_TRANS_NOT_HAS_INVOICE.Value > 0));
                    }
                    else
                    {
                        listVHisTreatmentFee3Expression.Add(o => (((o.TOTAL_PATIENT_PRICE ?? 0) <= (o.TOTAL_BILL_AMOUNT ?? 0)) || ((o.TOTAL_DEPOSIT_AMOUNT ?? 0) <= ((o.TOTAL_REPAY_AMOUNT ?? 0) + (o.TOTAL_BILL_TRANSFER_AMOUNT ?? 0))) || o.IS_ACTIVE != Constant.IS_TRUE) && (!o.COUNT_TRANS_NOT_HAS_INVOICE.HasValue || o.COUNT_TRANS_NOT_HAS_INVOICE.Value <= 0));
                    }
                }
                if (this.LAST_DEPOSIT_TIME_FROM.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.LAST_DEPOSIT_TIME >= this.LAST_DEPOSIT_TIME_FROM.Value);
                }
                if (this.LAST_DEPOSIT_TIME_TO.HasValue)
                {
                    listVHisTreatmentFee3Expression.Add(o => o.LAST_DEPOSIT_TIME <= this.LAST_DEPOSIT_TIME_TO.Value);
                }

                search.listVHisTreatmentFee3Expression.AddRange(listVHisTreatmentFee3Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatmentFee3Expression.Clear();
                search.listVHisTreatmentFee3Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
