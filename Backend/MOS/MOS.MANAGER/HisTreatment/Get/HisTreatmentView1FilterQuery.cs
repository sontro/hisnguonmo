using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatment
{
    public class HisTreatmentView1FilterQuery : HisTreatmentView1Filter
    {
        public HisTreatmentView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_1, bool>>> listVHisTreatment1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_1, bool>>>();

        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisTreatment1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatment1Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatment1Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatment1Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTreatment1Expression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisTreatment1Expression.Add(o =>
                        o.STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_HEIN_CARD_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.APPOINTMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.XML130_CHECK_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME))
                {
                    string keyWord = this.KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME.Trim().ToLower();
                    listVHisTreatment1Expression.Add(o => o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(keyWord) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(keyWord) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(keyWord) ||
                        o.TREATMENT_CODE.ToLower().Contains(keyWord));
                }
                if (this.IS_LOCK_HEIN.HasValue && this.IS_LOCK_HEIN.Value)
                {
                    listVHisTreatment1Expression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_LOCK_HEIN.HasValue && !this.IS_LOCK_HEIN.Value)
                {
                    listVHisTreatment1Expression.Add(o => !o.IS_LOCK_HEIN.HasValue || o.IS_LOCK_HEIN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listVHisTreatment1Expression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listVHisTreatment1Expression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.FEE_LOCK_TIME_FROM.HasValue)
                {
                    search.listVHisTreatment1Expression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value >= this.FEE_LOCK_TIME_FROM.Value);
                }
                if (this.FEE_LOCK_TIME_TO.HasValue)
                {
                    search.listVHisTreatment1Expression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value <= this.FEE_LOCK_TIME_TO.Value);
                }
                if (this.IN_TIME_FROM.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.IN_TIME >= this.IN_TIME_FROM.Value);
                }
                if (this.IN_TIME_TO.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.IN_TIME <= this.IN_TIME_TO.Value);
                }
                if (this.OUT_TIME_FROM.HasValue)
                {
                    search.listVHisTreatment1Expression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME >= this.OUT_TIME_FROM.Value);
                }
                if (this.OUT_TIME_TO.HasValue)
                {
                    search.listVHisTreatment1Expression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME <= this.OUT_TIME_TO.Value);
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.DOB_TO.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.TDL_PATIENT_DOB <= this.DOB_TO.Value);
                }
                if (this.DOB_FROM.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.TDL_PATIENT_DOB >= this.DOB_FROM.Value);
                }
                if (this.END_ROOM_IDs != null)
                {
                    search.listVHisTreatment1Expression.Add(o => o.END_ROOM_ID.HasValue && this.END_ROOM_IDs.Contains(o.END_ROOM_ID.Value));
                }

                if (this.IS_OUT.HasValue && this.IS_OUT.Value)
                {
                    listVHisTreatment1Expression.Add(o => o.OUT_TIME.HasValue);
                }
                if (this.IS_OUT.HasValue && !this.IS_OUT.Value)
                {
                    listVHisTreatment1Expression.Add(o => !o.OUT_TIME.HasValue);
                }
                if (this.TREATMENT_END_TYPE_ID != null)
                {
                    listVHisTreatment1Expression.Add(s => s.TREATMENT_END_TYPE_ID == this.TREATMENT_END_TYPE_ID);
                }
                if (this.HAS_PATY_ALTER_BHYT.HasValue && this.HAS_PATY_ALTER_BHYT.Value)
                {
                    listVHisTreatment1Expression.Add(o => o.COUNT_PATY_ALTER_BHYT.HasValue && o.COUNT_PATY_ALTER_BHYT.Value > 0);
                }
                if (this.HAS_PATY_ALTER_BHYT.HasValue && !this.HAS_PATY_ALTER_BHYT.Value)
                {
                    listVHisTreatment1Expression.Add(o => !o.COUNT_PATY_ALTER_BHYT.HasValue || o.COUNT_PATY_ALTER_BHYT.Value <= 0);
                }
                if (this.HAS_HEIN_APPROVAL.HasValue && this.HAS_HEIN_APPROVAL.Value)
                {
                    listVHisTreatment1Expression.Add(o => o.COUNT_HEIN_APPROVAL.HasValue && o.COUNT_HEIN_APPROVAL.Value > 0);
                }
                if (this.HAS_HEIN_APPROVAL.HasValue && !this.HAS_HEIN_APPROVAL.Value)
                {
                    listVHisTreatment1Expression.Add(o => !o.COUNT_HEIN_APPROVAL.HasValue || o.COUNT_HEIN_APPROVAL.Value <= 0);
                }
                if (this.HAS_HEIN_APPROVAL_OR_IS_PAUSE.HasValue && this.HAS_HEIN_APPROVAL_OR_IS_PAUSE.Value)
                {
                    listVHisTreatment1Expression.Add(o => (o.COUNT_HEIN_APPROVAL.HasValue && o.COUNT_HEIN_APPROVAL.Value > 0) || (o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE));
                }
                if (this.HAS_HEIN_APPROVAL_OR_IS_PAUSE.HasValue && !this.HAS_HEIN_APPROVAL_OR_IS_PAUSE.Value)
                {
                    listVHisTreatment1Expression.Add(o => (!o.COUNT_HEIN_APPROVAL.HasValue || o.COUNT_HEIN_APPROVAL.Value <= 0) && (!o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE));
                }
                if (this.HAS_NO_XML_URL_HEIN_APPROVAL.HasValue && this.HAS_NO_XML_URL_HEIN_APPROVAL.Value)
                {
                    listVHisTreatment1Expression.Add(o => o.COUNT_XML_URL_NULL.HasValue && o.COUNT_XML_URL_NULL.Value > 0);
                }
                else if (this.HAS_NO_XML_URL_HEIN_APPROVAL.HasValue && !this.HAS_NO_XML_URL_HEIN_APPROVAL.Value)
                {
                    listVHisTreatment1Expression.Add(o => !o.COUNT_XML_URL_NULL.HasValue || o.COUNT_XML_URL_NULL.Value == 0);
                }
                if (this.BRANCH_ID != null)
                {
                    listVHisTreatment1Expression.Add(s => s.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    search.listVHisTreatment1Expression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }
                if (!String.IsNullOrEmpty(this.TDL_HEIN_CARD_NUMBER__EXACT))
                {
                    listVHisTreatment1Expression.Add(o => o.TDL_HEIN_CARD_NUMBER == this.TDL_HEIN_CARD_NUMBER__EXACT);
                }
                if (this.TDL_HEIN_CARD_NUMBER_PREFIXs != null)
                {
                    listVHisTreatment1Expression.Add(o => this.TDL_HEIN_CARD_NUMBER_PREFIXs.Where(t => o.TDL_HEIN_CARD_NUMBER.StartsWith(t)).Any());
                }
                if (this.TDL_HEIN_CARD_NUMBER_PREFIX__NOT_INs != null)
                {
                    listVHisTreatment1Expression.Add(o => !this.TDL_HEIN_CARD_NUMBER_PREFIX__NOT_INs.Where(t => o.TDL_HEIN_CARD_NUMBER.StartsWith(t)).Any());
                }
                if (this.IN_DATE_FROM.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.IN_DATE >= this.IN_DATE_FROM.Value);
                }
                if (this.IN_DATE_TO.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.IN_DATE <= this.IN_DATE_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.TDL_PATIENT_CODE__EXACT))
                {
                    listVHisTreatment1Expression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_PATIENT_NAME))
                {
                    listVHisTreatment1Expression.Add(o => o.TDL_PATIENT_NAME.ToLower().Contains(this.TDL_PATIENT_NAME.Trim().ToLower()));
                }
                if (this.DATA_STORE_ID_NULL__OR__INs != null)
                {
                    listVHisTreatment1Expression.Add(o => !o.DATA_STORE_ID.HasValue || this.DATA_STORE_ID_NULL__OR__INs.Contains(o.DATA_STORE_ID.Value));
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    search.listVHisTreatment1Expression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }
                if (this.TDL_TREATMENT_TYPE_ID != null)
                {
                    listVHisTreatment1Expression.Add(s => s.TDL_TREATMENT_TYPE_ID.HasValue && s.TDL_TREATMENT_TYPE_ID.Value == this.TDL_TREATMENT_TYPE_ID.Value);
                }
                if (this.TDL_TREATMENT_TYPE_IDs != null)
                {
                    search.listVHisTreatment1Expression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && this.TDL_TREATMENT_TYPE_IDs.Contains(o.TDL_TREATMENT_TYPE_ID.Value));
                }
                if (this.XML4210_RESULT.HasValue)
                {
                    listVHisTreatment1Expression.Add(s => s.XML4210_RESULT.HasValue && s.XML4210_RESULT.Value == this.XML4210_RESULT.Value);
                }
                if (this.XML4210_RESULTs != null)
                {
                    search.listVHisTreatment1Expression.Add(o => o.XML4210_RESULT.HasValue && this.XML4210_RESULTs.Contains(o.XML4210_RESULT.Value));
                }
                if (this.COLLINEAR_XML4210_RESULT.HasValue)
                {
                    listVHisTreatment1Expression.Add(s => s.COLLINEAR_XML4210_RESULT.HasValue && s.COLLINEAR_XML4210_RESULT.Value == this.COLLINEAR_XML4210_RESULT.Value);
                }
                if (this.COLLINEAR_XML4210_RESULTs != null)
                {
                    search.listVHisTreatment1Expression.Add(o => o.COLLINEAR_XML4210_RESULT.HasValue && this.COLLINEAR_XML4210_RESULTs.Contains(o.COLLINEAR_XML4210_RESULT.Value));
                }
               
                if (this.IS_APPROVE_FINISH.HasValue && this.IS_APPROVE_FINISH.Value)
                {
                    listVHisTreatment1Expression.Add(o => o.IS_APPROVE_FINISH.HasValue && o.IS_APPROVE_FINISH.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_APPROVE_FINISH.HasValue && !this.IS_APPROVE_FINISH.Value)
                {
                    listVHisTreatment1Expression.Add(o => !o.IS_APPROVE_FINISH.HasValue || o.IS_APPROVE_FINISH.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.APPROVAL_STORE_STT_ID != null)
                {
                    listVHisTreatment1Expression.Add(s => s.APPROVAL_STORE_STT_ID.HasValue && s.APPROVAL_STORE_STT_ID.Value == this.APPROVAL_STORE_STT_ID.Value);
                }
                if (this.APPROVAL_STORE_STT_IDs != null)
                {
                    search.listVHisTreatment1Expression.Add(o => o.APPROVAL_STORE_STT_ID.HasValue && this.APPROVAL_STORE_STT_IDs.Contains(o.APPROVAL_STORE_STT_ID.Value));
                }
                if (this.HAS_IN_CODE.HasValue && this.HAS_IN_CODE.Value)
                {
                    listVHisTreatment1Expression.Add(o => o.IN_CODE != null);
                }
                if (this.HAS_IN_CODE.HasValue && !this.HAS_IN_CODE.Value)
                {
                    listVHisTreatment1Expression.Add(o => o.IN_CODE == null);
                }
                if (this.REQUEST_HOSPITALIZE_TIME_FROM.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.REQUEST_HOSPITALIZE_TIME.HasValue && o.REQUEST_HOSPITALIZE_TIME.Value >= this.REQUEST_HOSPITALIZE_TIME_FROM.Value);
                }
                if (this.REQUEST_HOSPITALIZE_TIME_TO.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.REQUEST_HOSPITALIZE_TIME.HasValue && o.REQUEST_HOSPITALIZE_TIME.Value <= this.REQUEST_HOSPITALIZE_TIME_TO.Value);
                }

                if (this.HEIN_LOCK_TIME_FROM.HasValue)
                {
                    search.listVHisTreatment1Expression.Add(o => o.HEIN_LOCK_TIME.HasValue && o.HEIN_LOCK_TIME >= this.HEIN_LOCK_TIME_FROM.Value);
                }
                if (this.HEIN_LOCK_TIME_TO.HasValue)
                {
                    search.listVHisTreatment1Expression.Add(o => o.HEIN_LOCK_TIME.HasValue && o.HEIN_LOCK_TIME <= this.HEIN_LOCK_TIME_TO.Value);
                }

                if (this.ICD_CODEs != null && this.ICD_CODEs.Count > 0)
                {
                    var searchPredicate = PredicateBuilder.False<V_HIS_TREATMENT_1>();

                    foreach (string str in this.ICD_CODEs)
                    {
                        var closureVariable = str;//can khai bao bien rieng de cho vao menh de ben duoi
                        searchPredicate =
                          searchPredicate.Or(o => o.ICD_CODE == closureVariable || (o.ICD_SUB_CODE != null && (";" + o.ICD_SUB_CODE + ";").Contains(closureVariable)));
                    }
                    listVHisTreatment1Expression.Add(searchPredicate);
                }

                if (this.XML130_RESULT.HasValue)
                {
                    listVHisTreatment1Expression.Add(o => o.XML130_RESULT.HasValue && o.XML130_RESULT.Value == this.XML130_RESULT.Value);
                }
                if (this.HAS_XML130_RESULT.HasValue)
                {
                    if (this.HAS_XML130_RESULT.Value)
                    {
                        listVHisTreatment1Expression.Add(o => o.XML130_RESULT.HasValue);
                    }
                    else
                    {
                        listVHisTreatment1Expression.Add(o => !o.XML130_RESULT.HasValue);
                    }
                }

                search.listVHisTreatment1Expression.AddRange(listVHisTreatment1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatment1Expression.Clear();
                search.listVHisTreatment1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
