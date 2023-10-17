using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public class HisTreatmentLViewFilterQuery : HisTreatmentLViewFilter
    {
        public HisTreatmentLViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT, bool>>> listLHisTreatmentExpression = new List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT, bool>>>();



        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listLHisTreatmentExpression.Add(o => 
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME))
                {
                    string keyWord = this.KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME.Trim().ToLower();
                    listLHisTreatmentExpression.Add(o => o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(keyWord) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(keyWord) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(keyWord) ||
                        o.TREATMENT_CODE.ToLower().Contains(keyWord));
                }
                if (this.IS_LOCK_HEIN.HasValue && this.IS_LOCK_HEIN.Value)
                {
                    listLHisTreatmentExpression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_LOCK_HEIN.HasValue && !this.IS_LOCK_HEIN.Value)
                {
                    listLHisTreatmentExpression.Add(o => !o.IS_LOCK_HEIN.HasValue || o.IS_LOCK_HEIN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listLHisTreatmentExpression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listLHisTreatmentExpression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IN_TIME_FROM.HasValue)
                {
                    listLHisTreatmentExpression.Add(o => o.IN_TIME >= this.IN_TIME_FROM.Value);
                }
                if (this.IN_TIME_TO.HasValue)
                {
                    listLHisTreatmentExpression.Add(o => o.IN_TIME <= this.IN_TIME_TO.Value);
                }
                if (this.OUT_TIME_FROM.HasValue)
                {
                    listLHisTreatmentExpression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME >= this.OUT_TIME_FROM.Value);
                }
                if (this.OUT_TIME_TO.HasValue)
                {
                    listLHisTreatmentExpression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME <= this.OUT_TIME_TO.Value);
                }
                if (this.DOB_TO.HasValue)
                {
                    listLHisTreatmentExpression.Add(o => o.TDL_PATIENT_DOB <= this.DOB_TO.Value);
                }
                if (this.DOB_FROM.HasValue)
                {
                    listLHisTreatmentExpression.Add(o => o.TDL_PATIENT_DOB >= this.DOB_FROM.Value);
                }

                if (this.IS_OUT.HasValue && this.IS_OUT.Value)
                {
                    listLHisTreatmentExpression.Add(o => o.OUT_TIME.HasValue);
                }
                if (this.IS_OUT.HasValue && !this.IS_OUT.Value)
                {
                    listLHisTreatmentExpression.Add(o => !o.OUT_TIME.HasValue);
                }
                if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    listLHisTreatmentExpression.Add(o => o.TDL_PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listLHisTreatmentExpression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (this.IN_DATE_FROM.HasValue)
                {
                    listLHisTreatmentExpression.Add(o => o.IN_DATE >= this.IN_DATE_FROM.Value);
                }
                if (this.IN_DATE_TO.HasValue)
                {
                    listLHisTreatmentExpression.Add(o => o.IN_DATE <= this.IN_DATE_TO.Value);
                }

                if (!String.IsNullOrEmpty(this.PATIENT_NAME))
                {
                    this.PATIENT_NAME = this.PATIENT_NAME.Trim().ToLower();
                    listLHisTreatmentExpression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.PATIENT_NAME)
                        );
                }
                if (!String.IsNullOrWhiteSpace(this.ICD_CODE_OR_ICD_SUB_CODE))
                {
                    listLHisTreatmentExpression.Add(o => o.ICD_CODE == this.ICD_CODE_OR_ICD_SUB_CODE || o.ICD_SUB_CODE.Contains(this.ICD_CODE_OR_ICD_SUB_CODE));
                }
                if (this.ICD_CODE_OR_ICD_SUB_CODEs != null)
                {
                    var searchPredicate = PredicateBuilder.False<L_HIS_TREATMENT>();

                    foreach (string str in this.ICD_CODE_OR_ICD_SUB_CODEs)
                    {
                        var closureVariable = str;//can khai bao bien rieng de cho vao menh de ben duoi
                        searchPredicate =
                          searchPredicate.Or(o => o.ICD_CODE == closureVariable || (o.ICD_SUB_CODE != null && (";" + o.ICD_SUB_CODE + ";").Contains(closureVariable)));
                    }
                    listLHisTreatmentExpression.Add(searchPredicate);
                }
                if (this.IS_RESTRICTED_KSK.HasValue && this.IS_RESTRICTED_KSK.Value)
                {
                    List<long> kskContractIds = TokenManager.GetAccessibleKskContract() ?? new List<long>();
                    listLHisTreatmentExpression.Add
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
                
                search.listLHisTreatmentExpression.AddRange(listLHisTreatmentExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listLHisTreatmentExpression.Clear();
                search.listLHisTreatmentExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
