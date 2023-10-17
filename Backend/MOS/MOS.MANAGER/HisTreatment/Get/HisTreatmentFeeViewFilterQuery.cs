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
    public class HisTreatmentFeeViewFilterQuery : HisTreatmentFeeViewFilter
    {
        public HisTreatmentFeeViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE, bool>>> listVHisTreatmentFeeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE, bool>>>();



        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisTreatmentFeeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatmentFeeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatmentFeeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatmentFeeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.PATIENT_ID.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.VIR_PATIENT_NAME))
                {
                    listVHisTreatmentFeeExpression.Add(o => o.TDL_PATIENT_NAME.ToLower().Contains(this.VIR_PATIENT_NAME.ToLower().Trim()));
                }
                if (!String.IsNullOrEmpty(this.PATIENT_CODE))
                {
                    listVHisTreatmentFeeExpression.Add(o => o.TDL_PATIENT_CODE.ToLower().Contains(this.PATIENT_CODE.ToLower().Trim()));
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE))
                {
                    listVHisTreatmentFeeExpression.Add(o => o.TREATMENT_CODE.ToLower().Contains(this.TREATMENT_CODE.ToLower().Trim()));
                }
                if (this.IS_LOCK_HEIN.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == this.IS_LOCK_HEIN.Value);
                }
                if (this.TOTAL_HEIN_PRICE__GREATER_THAN.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.TOTAL_HEIN_PRICE.HasValue && o.TOTAL_HEIN_PRICE.Value > this.TOTAL_HEIN_PRICE__GREATER_THAN.Value);
                }
                if (this.PATIENT_IDs != null)
                {
                    listVHisTreatmentFeeExpression.Add(o => this.PATIENT_IDs.Contains(o.PATIENT_ID));
                }

                if (this.TDL_PATIENT_TYPE_ID.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && o.TDL_PATIENT_TYPE_ID == this.TDL_PATIENT_TYPE_ID.Value);
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisTreatmentFeeExpression.Add(o =>
                        o.STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.END_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.IN_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_HEIN_CARD_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_ADDRESS.ToLower().Contains(this.KEY_WORD) ||
                        o.OUT_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (this.TDL_TREATMENT_TYPE_ID.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && o.TDL_TREATMENT_TYPE_ID.Value == this.TDL_TREATMENT_TYPE_ID.Value);
                }
                if (this.END_DEPARTMENT_ID.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.END_DEPARTMENT_ID.HasValue && o.END_DEPARTMENT_ID.Value == this.END_DEPARTMENT_ID.Value);
                }
                if (this.BRANCH_ID.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }

                if (this.TDL_TREATMENT_TYPE_IDs != null)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && this.TDL_TREATMENT_TYPE_IDs.Contains(o.TDL_TREATMENT_TYPE_ID.Value));
                }
                if (this.END_DEPARTMENT_IDs != null)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.END_DEPARTMENT_ID.HasValue && this.END_DEPARTMENT_IDs.Contains(o.END_DEPARTMENT_ID.Value));
                }
                if (this.BRANCH_IDs != null)
                {
                    listVHisTreatmentFeeExpression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }
                if (this.IN_DATE_FROM.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.IN_DATE >= this.IN_DATE_FROM.Value);
                }
                if (this.IN_DATE_TO.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.IN_DATE <= this.IN_DATE_TO.Value);
                }
                if (this.OUT_TIME_FROM.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.OUT_TIME >= this.OUT_TIME_FROM.Value);
                }
                if (this.OUT_TIME_TO.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.OUT_TIME <= this.OUT_TIME_TO.Value);
                }
                if (this.OUT_DATE_FROM.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.OUT_DATE >= this.OUT_DATE_FROM.Value);
                }
                if (this.OUT_DATE_TO.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.OUT_DATE <= this.OUT_DATE_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTreatmentFeeExpression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.IN_CODE__EXACT))
                {
                    listVHisTreatmentFeeExpression.Add(o => o.IN_CODE == this.IN_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    listVHisTreatmentFeeExpression.Add(o => o.TDL_PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME))
                {
                    string keyword = this.KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME.Trim().ToLower();
                    listVHisTreatmentFeeExpression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(keyword)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(keyword)
                        || o.TREATMENT_CODE.ToLower().Contains(keyword)
                        || o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(keyword));
                }
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE == Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listVHisTreatmentFeeExpression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE != Constant.IS_TRUE);
                }

                if (this.HAS_HEIN_APPROVAL.HasValue && this.HAS_HEIN_APPROVAL.Value)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.COUNT_HEIN_APPROVAL.HasValue && o.COUNT_HEIN_APPROVAL.Value > 0);
                }
                if (this.HAS_HEIN_APPROVAL.HasValue && !this.HAS_HEIN_APPROVAL.Value)
                {
                    listVHisTreatmentFeeExpression.Add(o => !o.COUNT_HEIN_APPROVAL.HasValue || o.COUNT_HEIN_APPROVAL.Value <= 0);
                }
                if (this.HAS_LOCK_HEIN.HasValue && this.HAS_LOCK_HEIN.Value)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_LOCK_HEIN.HasValue && !this.HAS_LOCK_HEIN.Value)
                {
                    listVHisTreatmentFeeExpression.Add(o => !o.IS_LOCK_HEIN.HasValue || o.IS_LOCK_HEIN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_PAY.HasValue)
                {
                    if (this.HAS_PAY.Value)
                        listVHisTreatmentFeeExpression.Add(o => o.TOTAL_PATIENT_PRICE.HasValue && o.TOTAL_BILL_AMOUNT.HasValue && o.TOTAL_PATIENT_PRICE.Value - o.TOTAL_BILL_AMOUNT.Value > 0);
                    else
                        listVHisTreatmentFeeExpression.Add(o => o.TOTAL_PATIENT_PRICE.HasValue && o.TOTAL_BILL_AMOUNT.HasValue && o.TOTAL_PATIENT_PRICE.Value - o.TOTAL_BILL_AMOUNT.Value == 0);
                }

                if (this.IS_IN_DEBT.HasValue && this.IS_IN_DEBT.Value)
                {
                    //So tien BN can phai tra > 0
                    listVHisTreatmentFeeExpression.Add(o => (o.TOTAL_PATIENT_PRICE ?? 0) - (o.TOTAL_DEPOSIT_AMOUNT ?? 0) - (o.TOTAL_BILL_AMOUNT ?? 0) + (o.TOTAL_BILL_TRANSFER_AMOUNT ?? 0) + (o.TOTAL_REPAY_AMOUNT ?? 0) > 0);
                }
                if (this.IS_IN_DEBT.HasValue && !this.IS_IN_DEBT.Value)
                {
                    //So tien BN can phai tra > 0
                    listVHisTreatmentFeeExpression.Add(o => (o.TOTAL_PATIENT_PRICE ?? 0) - (o.TOTAL_DEPOSIT_AMOUNT ?? 0) - (o.TOTAL_BILL_AMOUNT ?? 0) + (o.TOTAL_BILL_TRANSFER_AMOUNT ?? 0) + (o.TOTAL_REPAY_AMOUNT ?? 0) <= 0);
                }
				if (this.LAST_DEPOSIT_TIME_FROM.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.LAST_DEPOSIT_TIME.HasValue && o.LAST_DEPOSIT_TIME >= this.LAST_DEPOSIT_TIME_FROM.Value);
                }
                if (this.LAST_DEPOSIT_TIME_TO.HasValue)
                {
                    listVHisTreatmentFeeExpression.Add(o => o.LAST_DEPOSIT_TIME.HasValue && o.LAST_DEPOSIT_TIME <= this.LAST_DEPOSIT_TIME_TO.Value);
                }

                search.listVHisTreatmentFeeExpression.AddRange(listVHisTreatmentFeeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatmentFeeExpression.Clear();
                search.listVHisTreatmentFeeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
