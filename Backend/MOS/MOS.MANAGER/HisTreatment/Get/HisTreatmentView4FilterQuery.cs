using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.UTILITY;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public class HisTreatmentView4FilterQuery : HisTreatmentView4Filter
    {
        public HisTreatmentView4FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_4, bool>>> listVHisTreatment4Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_4, bool>>>();


        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisTreatment4Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatment4Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatment4Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatment4Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    //chi tim kiem theo 1 so truong thuong dung de tranh hieu nang
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisTreatment4Expression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_HEIN_CARD_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.IN_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.END_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_DOB_TEXT.ToLower().Contains(this.KEY_WORD));
                }

                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTreatment4Expression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_SOCIAL_INSURANCE_NUMBER__EXACT))
                {
                    listVHisTreatment4Expression.Add(o => o.TDL_SOCIAL_INSURANCE_NUMBER != null && o.TDL_SOCIAL_INSURANCE_NUMBER == this.TDL_SOCIAL_INSURANCE_NUMBER__EXACT);
                }
                if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    listVHisTreatment4Expression.Add(o => o.TDL_PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                if (this.IS_LOCK_HEIN.HasValue && this.IS_LOCK_HEIN.Value)
                {
                    listVHisTreatment4Expression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_LOCK_HEIN.HasValue && !this.IS_LOCK_HEIN.Value)
                {
                    listVHisTreatment4Expression.Add(o => !o.IS_LOCK_HEIN.HasValue || o.IS_LOCK_HEIN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listVHisTreatment4Expression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listVHisTreatment4Expression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.FEE_LOCK_TIME_FROM.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value >= this.FEE_LOCK_TIME_FROM.Value);
                }
                if (this.FEE_LOCK_TIME_TO.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value <= this.FEE_LOCK_TIME_TO.Value);
                }
                if (this.IN_TIME_FROM.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.IN_TIME >= this.IN_TIME_FROM.Value);
                }
                if (this.IN_TIME_TO.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.IN_TIME <= this.IN_TIME_TO.Value);
                }
                if (this.OUT_TIME_FROM.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME >= this.OUT_TIME_FROM.Value);
                }
                if (this.OUT_TIME_TO.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME <= this.OUT_TIME_TO.Value);
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.END_ROOM_IDs != null)
                {
                    search.listVHisTreatment4Expression.Add(o => o.END_ROOM_ID.HasValue && this.END_ROOM_IDs.Contains(o.END_ROOM_ID.Value));
                }
                if (this.IS_OUT.HasValue && this.IS_OUT.Value)
                {
                    listVHisTreatment4Expression.Add(o => o.OUT_TIME.HasValue);
                }
                if (this.IS_OUT.HasValue && !this.IS_OUT.Value)
                {
                    listVHisTreatment4Expression.Add(o => !o.OUT_TIME.HasValue);
                }
                if (this.TREATMENT_END_TYPE_ID != null)
                {
                    listVHisTreatment4Expression.Add(s => s.TREATMENT_END_TYPE_ID == this.TREATMENT_END_TYPE_ID);
                }
                if (this.WAS_BEEN_DEPARTMENT_ID.HasValue)
                {
                    //theo dung dinh dang do view cung cap: ,departmentId1,departmentId2,...
                    string departmentId = string.Format(",{0},", this.WAS_BEEN_DEPARTMENT_ID.Value);
                    listVHisTreatment4Expression.Add(s => (s.DEPARTMENT_IDS != null && ("," + s.DEPARTMENT_IDS + ",").Contains(departmentId)) || (s.CO_DEPARTMENT_IDS != null && ("," + s.CO_DEPARTMENT_IDS + ",").Contains(departmentId)));
                }

                if (this.CLINICAL_IN_TIME_FROM.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.CLINICAL_IN_TIME.HasValue && o.CLINICAL_IN_TIME.Value >= this.CLINICAL_IN_TIME_FROM.Value);
                }
                if (this.CLINICAL_IN_TIME_TO.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.CLINICAL_IN_TIME.HasValue && o.CLINICAL_IN_TIME.Value <= this.CLINICAL_IN_TIME_TO.Value);
                }
                if (this.IN_DEPARTMENT_ID.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.IN_DEPARTMENT_ID.HasValue && o.IN_DEPARTMENT_ID == this.IN_DEPARTMENT_ID.Value);
                }
                if (this.IN_DEPARTMENT_IDs != null)
                {
                    search.listVHisTreatment4Expression.Add(o => o.IN_DEPARTMENT_ID.HasValue && this.IN_DEPARTMENT_IDs.Contains(o.IN_DEPARTMENT_ID.Value));
                }
                if (this.IN_ROOM_ID.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.IN_ROOM_ID.HasValue && o.IN_ROOM_ID == this.IN_ROOM_ID.Value);
                }
                if (this.IN_ROOM_IDs != null)
                {
                    search.listVHisTreatment4Expression.Add(o => o.IN_ROOM_ID.HasValue && this.IN_ROOM_IDs.Contains(o.IN_ROOM_ID.Value));
                }
                if (this.IS_CHRONIC.HasValue && this.IS_CHRONIC.Value)
                {
                    listVHisTreatment4Expression.Add(o => o.IS_CHRONIC == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_CHRONIC.HasValue && !this.IS_CHRONIC.Value)
                {
                    listVHisTreatment4Expression.Add(o => !o.IS_CHRONIC.HasValue || o.IS_CHRONIC != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IN_DATE_FROM.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.IN_DATE >= this.IN_DATE_FROM.Value);
                }
                if (this.IN_DATE_TO.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.IN_DATE <= this.IN_DATE_TO.Value);
                }
                if (this.IN_ROOM_ID.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.IN_ROOM_ID.HasValue && o.IN_ROOM_ID == this.IN_ROOM_ID.Value);
                }
                if (this.TREATMENT_END_TYPE_IDs != null)
                {
                    search.listVHisTreatment4Expression.Add(o => o.TREATMENT_END_TYPE_ID.HasValue && this.TREATMENT_END_TYPE_IDs.Contains(o.TREATMENT_END_TYPE_ID.Value));
                }
                if (this.TREATMENT_RESULT_IDs != null)
                {
                    search.listVHisTreatment4Expression.Add(o => o.TREATMENT_RESULT_ID.HasValue && this.TREATMENT_RESULT_IDs.Contains(o.TREATMENT_RESULT_ID.Value));
                }
                if (this.TREATMENT_RESULT_ID.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.TREATMENT_RESULT_ID.HasValue && o.TREATMENT_RESULT_ID == this.TREATMENT_RESULT_ID.Value);
                }
                if (this.TDL_TREATMENT_TYPE_IDs != null)
                {
                    search.listVHisTreatment4Expression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && this.TDL_TREATMENT_TYPE_IDs.Contains(o.TDL_TREATMENT_TYPE_ID.Value));
                }
                if (this.TDL_TREATMENT_TYPE_ID.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && o.TDL_TREATMENT_TYPE_ID == this.TDL_TREATMENT_TYPE_ID.Value);
                }
                if (this.TDL_KSK_CONTRACT_ID.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.TDL_KSK_CONTRACT_ID.HasValue && o.TDL_KSK_CONTRACT_ID.Value == this.TDL_KSK_CONTRACT_ID.Value);
                }
                if (this.TDL_KSK_CONTRACT_IDs != null)
                {
                    listVHisTreatment4Expression.Add(o => o.TDL_KSK_CONTRACT_ID.HasValue && this.TDL_KSK_CONTRACT_IDs.Contains(o.TDL_KSK_CONTRACT_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.IN_CODE__EXACT))
                {
                    listVHisTreatment4Expression.Add(o => o.IN_CODE == this.IN_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.OUT_CODE__EXACT))
                {
                    listVHisTreatment4Expression.Add(o => o.OUT_CODE == this.OUT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.STORE_CODE__EXACT))
                {
                    listVHisTreatment4Expression.Add(o => o.STORE_CODE == this.STORE_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.END_CODE__EXACT))
                {
                    listVHisTreatment4Expression.Add(o => o.END_CODE == this.END_CODE__EXACT);
                }
                if (this.LAST_DEPARTMENT_ID.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.LAST_DEPARTMENT_ID.HasValue && o.LAST_DEPARTMENT_ID == this.LAST_DEPARTMENT_ID.Value);
                }
                if (this.LAST_DEPARTMENT_IDs != null)
                {
                    search.listVHisTreatment4Expression.Add(o => o.LAST_DEPARTMENT_ID.HasValue && this.LAST_DEPARTMENT_IDs.Contains(o.LAST_DEPARTMENT_ID.Value));
                }

                if (!String.IsNullOrEmpty(this.PATIENT_NAME))
                {
                    this.PATIENT_NAME = this.PATIENT_NAME.Trim().ToLower();
                    listVHisTreatment4Expression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.PATIENT_NAME)
                        );
                }
                if (this.DATA_STORE_ID_NULL__OR__INs != null)
                {
                    listVHisTreatment4Expression.Add(o => !o.DATA_STORE_ID.HasValue || this.DATA_STORE_ID_NULL__OR__INs.Contains(o.DATA_STORE_ID.Value));
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    listVHisTreatment4Expression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }
                if (this.BRANCH_ID.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }

                if (this.IS_APPROVE_FINISH.HasValue && this.IS_APPROVE_FINISH.Value)
                {
                    listVHisTreatment4Expression.Add(o => o.IS_APPROVE_FINISH.HasValue && o.IS_APPROVE_FINISH.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_APPROVE_FINISH.HasValue && !this.IS_APPROVE_FINISH.Value)
                {
                    listVHisTreatment4Expression.Add(o => !o.IS_APPROVE_FINISH.HasValue || o.IS_APPROVE_FINISH.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_KSK_APPROVE.HasValue && this.IS_KSK_APPROVE.Value)
                {
                    listVHisTreatment4Expression.Add(o => o.IS_KSK_APPROVE.HasValue && o.IS_KSK_APPROVE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_KSK_APPROVE.HasValue && !this.IS_KSK_APPROVE.Value)
                {
                    listVHisTreatment4Expression.Add(o => !o.IS_KSK_APPROVE.HasValue || o.IS_KSK_APPROVE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_REQUIRED_APPROVAL.HasValue && this.IS_REQUIRED_APPROVAL.Value)
                {
                    listVHisTreatment4Expression.Add(o => o.IS_REQUIRED_APPROVAL.HasValue && o.IS_REQUIRED_APPROVAL.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_REQUIRED_APPROVAL.HasValue && !this.IS_REQUIRED_APPROVAL.Value)
                {
                    listVHisTreatment4Expression.Add(o => !o.IS_REQUIRED_APPROVAL.HasValue || o.IS_REQUIRED_APPROVAL.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.APPROVAL_STORE_STT_ID.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.APPROVAL_STORE_STT_ID.HasValue && o.APPROVAL_STORE_STT_ID.Value == this.APPROVAL_STORE_STT_ID.Value);
                }
                if (this.APPROVAL_STORE_STT_IDs != null)
                {
                    listVHisTreatment4Expression.Add(o => o.APPROVAL_STORE_STT_ID.HasValue && this.APPROVAL_STORE_STT_IDs.Contains(o.APPROVAL_STORE_STT_ID.Value));
                }

                if (this.IN_DATE_EQUAL.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.IN_DATE == this.IN_DATE_EQUAL.Value);
                }
                if (this.IN_MONTH_EQUAL.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.VIR_IN_MONTH.HasValue && o.VIR_IN_MONTH == this.IN_MONTH_EQUAL.Value);
                }
                if (this.OUT_DATE_EQUAL.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.OUT_DATE == this.OUT_DATE_EQUAL.Value);
                }
                if (this.OUT_MONTH_EQUAL.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.VIR_OUT_MONTH.HasValue && o.VIR_OUT_MONTH == this.OUT_MONTH_EQUAL.Value);
                }
                if (this.OUT_YEAR_EQUAL.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.VIR_OUT_YEAR.HasValue && o.VIR_OUT_YEAR == this.OUT_YEAR_EQUAL.Value);
                }
                if (this.IN_YEAR_EQUAL.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.VIR_IN_YEAR.HasValue && o.VIR_IN_YEAR == this.IN_YEAR_EQUAL.Value);
                }
                if (this.OUT_DATE_FROM.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.OUT_DATE >= this.OUT_DATE_FROM.Value);
                }
                if (this.OUT_DATE_TO.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.OUT_DATE <= this.OUT_DATE_TO.Value);
                }
                if (this.HOSPITALIZE_DEPARTMENT_ID.HasValue)
                {
                    listVHisTreatment4Expression.Add(o => o.HOSPITALIZE_DEPARTMENT_ID.HasValue && o.HOSPITALIZE_DEPARTMENT_ID == this.HOSPITALIZE_DEPARTMENT_ID.Value);
                }
                if (this.HOSPITALIZE_DEPARTMENT_IDs != null)
                {
                    search.listVHisTreatment4Expression.Add(o => o.HOSPITALIZE_DEPARTMENT_ID.HasValue && this.HOSPITALIZE_DEPARTMENT_IDs.Contains(o.HOSPITALIZE_DEPARTMENT_ID.Value));
                }
                if (this.IS_RESTRICTED_KSK.HasValue && this.IS_RESTRICTED_KSK.Value)
                {
                    List<long> kskContractIds = TokenManager.GetAccessibleKskContract() ?? new List<long>();
                    listVHisTreatment4Expression.Add
                        (o => !o.TDL_KSK_CONTRACT_ID.HasValue
                              || 
                              (o.TDL_KSK_CONTRACT_ID.HasValue && 
                                (
                                    (!o.TDL_KSK_CONTRACT_IS_RESTRICTED.HasValue || o.TDL_KSK_CONTRACT_IS_RESTRICTED.Value != Constant.IS_TRUE) 
                                    || 
                                    (
                                        o.TDL_KSK_CONTRACT_IS_RESTRICTED.HasValue && o.TDL_KSK_CONTRACT_IS_RESTRICTED.Value == Constant.IS_TRUE && 
                                            kskContractIds.Contains(o.TDL_KSK_CONTRACT_ID.Value) && !o.DATA_STORE_ID.HasValue
                                    )
                                )
                              )
                        );
                }

                search.listVHisTreatment4Expression.AddRange(listVHisTreatment4Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatment4Expression.Clear();
                search.listVHisTreatment4Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
