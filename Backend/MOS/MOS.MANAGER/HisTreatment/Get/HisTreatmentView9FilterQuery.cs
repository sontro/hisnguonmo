using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public class HisTreatmentView9FilterQuery : HisTreatmentView9Filter
    {
        public HisTreatmentView9FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_9, bool>>> listVHisTreatment9Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_9, bool>>>();



        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisTreatment9Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatment9Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatment9Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatment9Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                 if (!String.IsNullOrEmpty(this.PATIENT_PROGRAM_CODE))
                {
                    this.PATIENT_PROGRAM_CODE = this.PATIENT_PROGRAM_CODE.Trim().ToLower();
                    listVHisTreatment9Expression.Add(o => o.PATIENT_PROGRAM_CODE != null && o.PATIENT_PROGRAM_CODE.ToLower().Contains(this.PATIENT_PROGRAM_CODE));
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    //Chi tim theo 1 so truong thuong su dung, de tranh van de ve hieu nang
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisTreatment9Expression.Add(o => 
                        o.STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.END_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_HEIN_CARD_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.TRANSFER_IN_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.OUT_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME))
                {
                    string keyWord = this.KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME.Trim().ToLower();
                    listVHisTreatment9Expression.Add(o => o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(keyWord) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(keyWord) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(keyWord) ||
                        o.TREATMENT_CODE.ToLower().Contains(keyWord));
                }
                if (this.IS_LOCK_HEIN.HasValue && this.IS_LOCK_HEIN.Value)
                {
                    listVHisTreatment9Expression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_LOCK_HEIN.HasValue && !this.IS_LOCK_HEIN.Value)
                {
                    listVHisTreatment9Expression.Add(o => !o.IS_LOCK_HEIN.HasValue || o.IS_LOCK_HEIN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listVHisTreatment9Expression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listVHisTreatment9Expression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.FEE_LOCK_TIME_FROM.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value >= this.FEE_LOCK_TIME_FROM.Value);
                }
                if (this.FEE_LOCK_TIME_TO.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value <= this.FEE_LOCK_TIME_TO.Value);
                }
                if (this.IN_TIME_FROM.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.IN_TIME >= this.IN_TIME_FROM.Value);
                }
                if (this.IN_TIME_TO.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.IN_TIME <= this.IN_TIME_TO.Value);
                }
                if (this.OUT_TIME_FROM.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME >= this.OUT_TIME_FROM.Value);
                }
                if (this.OUT_TIME_TO.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME <= this.OUT_TIME_TO.Value);
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.DOB_TO.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.TDL_PATIENT_DOB <= this.DOB_TO.Value);
                }
                if (this.DOB_FROM.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.TDL_PATIENT_DOB >= this.DOB_FROM.Value);
                }
                if (this.END_ROOM_IDs != null)
                {
                    search.listVHisTreatment9Expression.Add(o => o.END_ROOM_ID.HasValue && this.END_ROOM_IDs.Contains(o.END_ROOM_ID.Value));
                }

                if (this.IS_OUT.HasValue && this.IS_OUT.Value)
                {
                    listVHisTreatment9Expression.Add(o => o.OUT_TIME.HasValue);
                }
                if (this.IS_OUT.HasValue && !this.IS_OUT.Value)
                {
                    listVHisTreatment9Expression.Add(o => !o.OUT_TIME.HasValue);
                }
                if (this.TREATMENT_END_TYPE_ID != null)
                {
                    listVHisTreatment9Expression.Add(s => s.TREATMENT_END_TYPE_ID == this.TREATMENT_END_TYPE_ID);
                }
                if (this.HAS_DATA_STORE.HasValue && this.HAS_DATA_STORE.Value)
                {
                    listVHisTreatment9Expression.Add(o => o.DATA_STORE_ID.HasValue);
                }
                if (this.HAS_DATA_STORE.HasValue && !this.HAS_DATA_STORE.Value)
                {
                    listVHisTreatment9Expression.Add(o => !o.DATA_STORE_ID.HasValue);
                }
                if (this.DATA_STORE_IDs != null)
                {
                    search.listVHisTreatment9Expression.Add(o => o.DATA_STORE_ID.HasValue && this.DATA_STORE_IDs.Contains(o.DATA_STORE_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    listVHisTreatment9Expression.Add(o => o.TDL_PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTreatment9Expression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (this.STORE_TIME_FROM.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.STORE_TIME.HasValue && o.STORE_TIME >= this.STORE_TIME_FROM.Value);
                }
                if (this.STORE_TIME_TO.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.STORE_TIME.HasValue && o.STORE_TIME <= this.STORE_TIME_TO.Value);
                }
                if (this.END_DEPARTMENT_IDs != null)
                {
                    search.listVHisTreatment9Expression.Add(o => o.END_DEPARTMENT_ID.HasValue && this.END_DEPARTMENT_IDs.Contains(o.END_DEPARTMENT_ID.Value));
                }
                if (this.PATIENT_IDs != null)
                {
                    search.listVHisTreatment9Expression.Add(o => this.PATIENT_IDs.Contains(o.PATIENT_ID));
                }
                if (this.IS_CHRONIC.HasValue && this.IS_CHRONIC.Value)
                {
                    listVHisTreatment9Expression.Add(o => o.IS_CHRONIC.HasValue && o.IS_CHRONIC.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_CHRONIC.HasValue && !this.IS_CHRONIC.Value)
                {
                    listVHisTreatment9Expression.Add(o => !o.IS_CHRONIC.HasValue || o.IS_CHRONIC.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_HEIN_CARD_NUMBER__EXACT))
                {
                    listVHisTreatment9Expression.Add(o => !String.IsNullOrWhiteSpace(o.TDL_HEIN_CARD_NUMBER) && o.TDL_HEIN_CARD_NUMBER == this.TDL_HEIN_CARD_NUMBER__EXACT);
                }
                if (this.CLINICAL_IN_TIME_FROM.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.CLINICAL_IN_TIME >= this.CLINICAL_IN_TIME_FROM.Value);
                }
                if (this.CLINICAL_IN_TIME_TO.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.CLINICAL_IN_TIME <= this.CLINICAL_IN_TIME_TO.Value);
                }
                if (this.IN_DATE_FROM.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.IN_DATE >= this.IN_DATE_FROM.Value);
                }
                if (this.IN_DATE_TO.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.IN_DATE <= this.IN_DATE_TO.Value);
                }
                if (this.TREATMENT_TYPE_IDs != null)
                {
                    search.listVHisTreatment9Expression.Add(o => this.TREATMENT_TYPE_IDs.Contains(o.TREATMENT_TYPE_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.STORE_CODE__EXACT))
                {
                    listVHisTreatment9Expression.Add(o => o.STORE_CODE == this.STORE_CODE__EXACT && o.STORE_CODE != null);
                }

                if (this.HAS_BORROW_APPOINTMENT_TIME.HasValue)
                {
                    if (this.HAS_BORROW_APPOINTMENT_TIME.Value)
                    {
                        listVHisTreatment9Expression.Add(o => o.BORROW_APPOINTMENT_TIME.HasValue);
                    }
                    else
                    {
                        listVHisTreatment9Expression.Add(o => !o.BORROW_APPOINTMENT_TIME.HasValue);
                    }
                }

                if (!String.IsNullOrEmpty(this.PATIENT_NAME))
                {
                    this.PATIENT_NAME = this.PATIENT_NAME.Trim().ToLower();
                    listVHisTreatment9Expression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.PATIENT_NAME)
                        );
                }
                if (!String.IsNullOrWhiteSpace(this.ICD_CODE_OR_ICD_SUB_CODE))
                {
                    listVHisTreatment9Expression.Add(o => o.ICD_CODE == this.ICD_CODE_OR_ICD_SUB_CODE || o.ICD_SUB_CODE.Contains(this.ICD_CODE_OR_ICD_SUB_CODE));
                }
                if (this.ICD_CODE_OR_ICD_SUB_CODEs != null)
                {
                    var searchPredicate = PredicateBuilder.False<V_HIS_TREATMENT_9>();

                    foreach (string str in this.ICD_CODE_OR_ICD_SUB_CODEs)
                    {
                        var closureVariable = str;//can khai bao bien rieng de cho vao menh de ben duoi
                        searchPredicate =
                          searchPredicate.Or(o => o.ICD_CODE == closureVariable || (o.ICD_SUB_CODE != null && (";" + o.ICD_SUB_CODE + ";").Contains(closureVariable)));
                    }
                    listVHisTreatment9Expression.Add(searchPredicate);
                }
                if (this.BRANCH_ID.HasValue)
                {
                    listVHisTreatment9Expression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listVHisTreatment9Expression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }
                if (this.DATA_STORE_ID_NULL__OR__INs != null)
                {
                    listVHisTreatment9Expression.Add(o => !o.DATA_STORE_ID.HasValue || this.DATA_STORE_ID_NULL__OR__INs.Contains(o.DATA_STORE_ID.Value));
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    search.listVHisTreatment9Expression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }

                search.listVHisTreatment9Expression.AddRange(listVHisTreatment9Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatment9Expression.Clear();
                search.listVHisTreatment9Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
