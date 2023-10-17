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
    public class HisTreatmentViewFilterQuery : HisTreatmentViewFilter
    {
        public HisTreatmentViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT, bool>>> listVHisTreatmentExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT, bool>>>();



        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisTreatmentExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatmentExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatmentExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatmentExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisTreatmentExpression.Add(o =>
                        o.STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.END_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.IN_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_HEIN_CARD_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_FIRST_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_ADDRESS.ToLower().Contains(this.KEY_WORD) ||
                        o.OUT_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (this.MEDI_RECORD_ID.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.MEDI_RECORD_ID.HasValue && o.MEDI_RECORD_ID == this.MEDI_RECORD_ID.Value);
                }
                if (this.MEDI_RECORD_IDs != null)
                {
                    search.listVHisTreatmentExpression.Add(o => o.MEDI_RECORD_ID.HasValue && this.MEDI_RECORD_IDs.Contains(o.MEDI_RECORD_ID.Value));
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME))
                {
                    string keyWord = this.KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME.Trim().ToLower();
                    listVHisTreatmentExpression.Add(o => o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(keyWord) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(keyWord) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(keyWord) ||
                        o.TREATMENT_CODE.ToLower().Contains(keyWord));
                }
                if (this.IS_LOCK_HEIN.HasValue && this.IS_LOCK_HEIN.Value)
                {
                    listVHisTreatmentExpression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_LOCK_HEIN.HasValue && !this.IS_LOCK_HEIN.Value)
                {
                    listVHisTreatmentExpression.Add(o => !o.IS_LOCK_HEIN.HasValue || o.IS_LOCK_HEIN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listVHisTreatmentExpression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listVHisTreatmentExpression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.FEE_LOCK_TIME_FROM.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value >= this.FEE_LOCK_TIME_FROM.Value);
                }
                if (this.FEE_LOCK_TIME_TO.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value <= this.FEE_LOCK_TIME_TO.Value);
                }
                if (this.IN_TIME_FROM.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.IN_TIME >= this.IN_TIME_FROM.Value);
                }
                if (this.IN_TIME_TO.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.IN_TIME <= this.IN_TIME_TO.Value);
                }
                if (this.OUT_TIME_FROM.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME >= this.OUT_TIME_FROM.Value);
                }
                if (this.OUT_TIME_TO.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME <= this.OUT_TIME_TO.Value);
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.DOB_TO.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.TDL_PATIENT_DOB <= this.DOB_TO.Value);
                }
                if (this.DOB_FROM.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.TDL_PATIENT_DOB >= this.DOB_FROM.Value);
                }
                if (this.END_ROOM_IDs != null)
                {
                    search.listVHisTreatmentExpression.Add(o => o.END_ROOM_ID.HasValue && this.END_ROOM_IDs.Contains(o.END_ROOM_ID.Value));
                }

                if (this.IS_OUT.HasValue && this.IS_OUT.Value)
                {
                    listVHisTreatmentExpression.Add(o => o.OUT_TIME.HasValue);
                }
                if (this.IS_OUT.HasValue && !this.IS_OUT.Value)
                {
                    listVHisTreatmentExpression.Add(o => !o.OUT_TIME.HasValue);
                }
                if (this.TREATMENT_END_TYPE_ID != null)
                {
                    listVHisTreatmentExpression.Add(s => s.TREATMENT_END_TYPE_ID == this.TREATMENT_END_TYPE_ID);
                }
                if (this.HAS_DATA_STORE.HasValue && this.HAS_DATA_STORE.Value)
                {
                    listVHisTreatmentExpression.Add(o => o.DATA_STORE_ID.HasValue);
                }
                if (this.HAS_DATA_STORE.HasValue && !this.HAS_DATA_STORE.Value)
                {
                    listVHisTreatmentExpression.Add(o => !o.DATA_STORE_ID.HasValue);
                }
                if (this.DATA_STORE_IDs != null)
                {
                    search.listVHisTreatmentExpression.Add(o => o.DATA_STORE_ID.HasValue && this.DATA_STORE_IDs.Contains(o.DATA_STORE_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    listVHisTreatmentExpression.Add(o => o.TDL_PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTreatmentExpression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (this.STORE_TIME_FROM.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.STORE_TIME.HasValue && o.STORE_TIME >= this.STORE_TIME_FROM.Value);
                }
                if (this.STORE_TIME_TO.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.STORE_TIME.HasValue && o.STORE_TIME <= this.STORE_TIME_TO.Value);
                }
                if (this.END_DEPARTMENT_IDs != null)
                {
                    search.listVHisTreatmentExpression.Add(o => o.END_DEPARTMENT_ID.HasValue && this.END_DEPARTMENT_IDs.Contains(o.END_DEPARTMENT_ID.Value));
                }
                if (this.PATIENT_IDs != null)
                {
                    search.listVHisTreatmentExpression.Add(o => this.PATIENT_IDs.Contains(o.PATIENT_ID));
                }
                if (this.BRANCH_ID.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listVHisTreatmentExpression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }
                if (this.IS_CHRONIC.HasValue && this.IS_CHRONIC.Value)
                {
                    listVHisTreatmentExpression.Add(o => o.IS_CHRONIC.HasValue && o.IS_CHRONIC.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_CHRONIC.HasValue && !this.IS_CHRONIC.Value)
                {
                    listVHisTreatmentExpression.Add(o => !o.IS_CHRONIC.HasValue || o.IS_CHRONIC.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_HEIN_CARD_NUMBER__EXACT))
                {
                    listVHisTreatmentExpression.Add(o => !String.IsNullOrWhiteSpace(o.TDL_HEIN_CARD_NUMBER) && o.TDL_HEIN_CARD_NUMBER == this.TDL_HEIN_CARD_NUMBER__EXACT);
                }
                if (this.CLINICAL_IN_TIME_FROM.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.CLINICAL_IN_TIME >= this.CLINICAL_IN_TIME_FROM.Value);
                }
                if (this.CLINICAL_IN_TIME_TO.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.CLINICAL_IN_TIME <= this.CLINICAL_IN_TIME_TO.Value);
                }
                if (this.IN_DATE_FROM.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.IN_DATE >= this.IN_DATE_FROM.Value);
                }
                if (this.IN_DATE_TO.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.IN_DATE <= this.IN_DATE_TO.Value);
                }
                if (this.IS_YDT_UPLOAD.HasValue && this.IS_YDT_UPLOAD.Value)
                {
                    listVHisTreatmentExpression.Add(o => o.IS_YDT_UPLOAD.HasValue && o.IS_YDT_UPLOAD.Value == Constant.IS_TRUE);
                }
                if (this.IS_YDT_UPLOAD.HasValue && !this.IS_YDT_UPLOAD.Value)
                {
                    listVHisTreatmentExpression.Add(o => !o.IS_YDT_UPLOAD.HasValue || o.IS_YDT_UPLOAD.Value != Constant.IS_TRUE);
                }
                if (!String.IsNullOrWhiteSpace(this.ICD_CODE_OR_ICD_SUB_CODE))
                {
                    listVHisTreatmentExpression.Add(o => o.ICD_CODE == this.ICD_CODE_OR_ICD_SUB_CODE || o.ICD_SUB_CODE.Contains(this.ICD_CODE_OR_ICD_SUB_CODE));
                }

                if (this.ICD_CODE_OR_ICD_SUB_CODEs != null)
                {
                    var searchPredicate = PredicateBuilder.False<V_HIS_TREATMENT>();

                    foreach(string str in this.ICD_CODE_OR_ICD_SUB_CODEs)
                    {
                       var closureVariable = str;//can khai bao bien rieng de cho vao menh de ben duoi
                       searchPredicate = 
                         searchPredicate.Or(o => o.ICD_CODE == closureVariable || (o.ICD_SUB_CODE != null && (";" + o.ICD_SUB_CODE + ";").Contains(closureVariable)));
                    }
                    listVHisTreatmentExpression.Add(searchPredicate);
                }

                if (this.TDL_KSK_CONTRACT_ID.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.TDL_KSK_CONTRACT_ID.HasValue && o.TDL_KSK_CONTRACT_ID.Value == this.TDL_KSK_CONTRACT_ID.Value);
                }
                if (this.TDL_KSK_CONTRACT_IDs != null)
                {
                    listVHisTreatmentExpression.Add(o => o.TDL_KSK_CONTRACT_ID.HasValue && this.TDL_KSK_CONTRACT_IDs.Contains(o.TDL_KSK_CONTRACT_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.PATIENT_NAME))
                {
                    this.PATIENT_NAME = this.PATIENT_NAME.Trim().ToLower();
                    listVHisTreatmentExpression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.PATIENT_NAME)
                        );
                }
                if (!String.IsNullOrEmpty(this.HRM_KSK_CODE__EXACT))
                {
                    listVHisTreatmentExpression.Add(o => o.HRM_KSK_CODE == this.HRM_KSK_CODE__EXACT);
                }
                if (this.DATA_STORE_ID_NULL__OR__INs != null)
                {
                    listVHisTreatmentExpression.Add(o => !o.DATA_STORE_ID.HasValue || this.DATA_STORE_ID_NULL__OR__INs.Contains(o.DATA_STORE_ID.Value));
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    listVHisTreatmentExpression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }

                if (this.DEATH_SYNC_RESULT_TYPE.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.DEATH_SYNC_RESULT_TYPE == this.DEATH_SYNC_RESULT_TYPE.Value);
                }

                if (this.DEATH_TIME_FROM.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.DEATH_TIME != null && o.DEATH_TIME >= this.DEATH_TIME_FROM.Value);
                }
                if (this.DEATH_TIME_TO.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.DEATH_TIME != null && o.DEATH_TIME <= this.DEATH_TIME_TO.Value);
                }
                if (this.APPOINTMENT_DATE.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.APPOINTMENT_DATE != null && o.APPOINTMENT_DATE == this.APPOINTMENT_DATE.Value);
                }
                if (this.HAS_MOBILE.HasValue)
                {
                    if (this.HAS_MOBILE.Value)
                    {
                        listVHisTreatmentExpression.Add(o => !string.IsNullOrEmpty(o.TDL_PATIENT_MOBILE) || !string.IsNullOrEmpty(o.TDL_PATIENT_PHONE) || !string.IsNullOrEmpty(o.TDL_PATIENT_RELATIVE_MOBILE) || !string.IsNullOrEmpty(o.TDL_PATIENT_RELATIVE_PHONE));
                    }
                    else
                    {
                        listVHisTreatmentExpression.Add(o => string.IsNullOrEmpty(o.TDL_PATIENT_MOBILE) && string.IsNullOrEmpty(o.TDL_PATIENT_PHONE) && string.IsNullOrEmpty(o.TDL_PATIENT_RELATIVE_MOBILE) && string.IsNullOrEmpty(o.TDL_PATIENT_RELATIVE_PHONE));
                    }
                }

                if (this.DEATH_ISSUED_DATE_FROM.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.DEATH_ISSUED_DATE != null && o.DEATH_ISSUED_DATE >= this.DEATH_ISSUED_DATE_FROM.Value);
                }
                if (this.DEATH_ISSUED_DATE_TO.HasValue)
                {
                    listVHisTreatmentExpression.Add(o => o.DEATH_ISSUED_DATE != null && o.DEATH_ISSUED_DATE <= this.DEATH_ISSUED_DATE_TO.Value);
                }
                search.listVHisTreatmentExpression.AddRange(listVHisTreatmentExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatmentExpression.Clear();
                search.listVHisTreatmentExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
