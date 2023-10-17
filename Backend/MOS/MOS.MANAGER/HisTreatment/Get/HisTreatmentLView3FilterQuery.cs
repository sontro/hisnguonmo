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
    public class HisTreatmentLView3FilterQuery : HisTreatmentLView3Filter
    {
        public HisTreatmentLView3FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_3, bool>>> listLHisTreatment3Expression = new List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_3, bool>>>();



        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listLHisTreatment3Expression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (this.ID.HasValue)
                {
                    listLHisTreatment3Expression.Add(o => o.ID == this.ID);
                }
                if (this.END_DEPARTMENT_ID.HasValue)
                {
                    listLHisTreatment3Expression.Add(o => o.END_DEPARTMENT_ID.HasValue && o.END_DEPARTMENT_ID.Value == this.END_DEPARTMENT_ID);
                }
                if (this.TDL_PATIENT_TYPE_ID.HasValue)
                {
                    listLHisTreatment3Expression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && o.TDL_PATIENT_TYPE_ID.Value == this.TDL_PATIENT_TYPE_ID);
                }
                if (this.TDL_TREATMENT_TYPE_ID.HasValue)
                {
                    listLHisTreatment3Expression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && o.TDL_TREATMENT_TYPE_ID.Value == this.TDL_TREATMENT_TYPE_ID);
                }
                if (this.MEDI_RECORD_ID.HasValue)
                {
                    listLHisTreatment3Expression.Add(o => o.MEDI_RECORD_ID.HasValue && o.MEDI_RECORD_ID.Value == this.MEDI_RECORD_ID);
                }

                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listLHisTreatment3Expression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }

                if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    listLHisTreatment3Expression.Add(o => o.TDL_PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }

                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listLHisTreatment3Expression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listLHisTreatment3Expression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_LOCK_HEIN.HasValue && this.IS_LOCK_HEIN.Value)
                {
                    listLHisTreatment3Expression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_LOCK_HEIN.HasValue && !this.IS_LOCK_HEIN.Value)
                {
                    listLHisTreatment3Expression.Add(o => !o.IS_LOCK_HEIN.HasValue || o.IS_LOCK_HEIN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_STORED.HasValue && this.IS_STORED.Value)
                {
                    listLHisTreatment3Expression.Add(o => o.MEDI_RECORD_ID.HasValue);
                }
                if (this.IS_STORED.HasValue && !this.IS_STORED.Value)
                {
                    listLHisTreatment3Expression.Add(o => !o.MEDI_RECORD_ID.HasValue);
                }

                if (this.IN_DATE_FROM.HasValue)
                {
                    listLHisTreatment3Expression.Add(o => o.IN_DATE >= this.IN_DATE_FROM.Value);
                }
                if (this.IN_DATE_TO.HasValue)
                {
                    listLHisTreatment3Expression.Add(o => o.IN_DATE <= this.IN_DATE_TO.Value);
                }
                if (this.OUT_DATE_FROM.HasValue)
                {
                    listLHisTreatment3Expression.Add(o => o.OUT_DATE.HasValue && o.OUT_DATE >= this.OUT_DATE_FROM.Value);
                }
                if (this.OUT_DATE_TO.HasValue)
                {
                    listLHisTreatment3Expression.Add(o => o.OUT_DATE.HasValue && o.OUT_DATE <= this.OUT_DATE_TO.Value);
                }

                if (this.END_DEPARTMENT_IDs != null)
                {
                    listLHisTreatment3Expression.Add(o => o.END_DEPARTMENT_ID.HasValue && this.END_DEPARTMENT_IDs.Contains(o.END_DEPARTMENT_ID.Value));
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    listLHisTreatment3Expression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }
                if (this.TDL_TREATMENT_TYPE_IDs != null)
                {
                    listLHisTreatment3Expression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && this.TDL_TREATMENT_TYPE_IDs.Contains(o.TDL_TREATMENT_TYPE_ID.Value));
                }
                if (this.APPROVAL_STORE_STT_ID.HasValue)
                {
                    listLHisTreatment3Expression.Add(o => o.APPROVAL_STORE_STT_ID.HasValue && o.APPROVAL_STORE_STT_ID.Value == this.APPROVAL_STORE_STT_ID.Value);
                }
                if (this.APPROVAL_STORE_STT_ID__NULL_OR_EQUAL.HasValue)
                {
                    listLHisTreatment3Expression.Add(o => !o.APPROVAL_STORE_STT_ID.HasValue || o.APPROVAL_STORE_STT_ID.Value == this.APPROVAL_STORE_STT_ID__NULL_OR_EQUAL.Value);
                }
                if (this.HAS_APPROVAL_STORE_STT_ID.HasValue)
                {
                    if (this.HAS_APPROVAL_STORE_STT_ID.Value)
                    {
                        listLHisTreatment3Expression.Add(o => o.APPROVAL_STORE_STT_ID.HasValue);
                    }
                    else
                    {
                        listLHisTreatment3Expression.Add(o => !o.APPROVAL_STORE_STT_ID.HasValue);
                    }
                }
                if (this.LAST_DEPARTMENT_ID.HasValue)
                {
                    listLHisTreatment3Expression.Add(o => o.LAST_DEPARTMENT_ID.HasValue && o.LAST_DEPARTMENT_ID.Value == this.LAST_DEPARTMENT_ID);
                }
                if (this.LAST_DEPARTMENT_IDs != null)
                {
                    listLHisTreatment3Expression.Add(o => o.LAST_DEPARTMENT_ID.HasValue && this.LAST_DEPARTMENT_IDs.Contains(o.LAST_DEPARTMENT_ID.Value));
                }
                if (this.IS_RESTRICTED_KSK.HasValue && this.IS_RESTRICTED_KSK.Value)
                {
                    List<long> kskContractIds = TokenManager.GetAccessibleKskContract() ?? new List<long>();
                    listLHisTreatment3Expression.Add
                        (o => !o.TDL_KSK_CONTRACT_ID.HasValue
                              ||
                              (o.TDL_KSK_CONTRACT_ID.HasValue &&
                                (
                                    (!o.TDL_KSK_CONTRACT_IS_RESTRICTED.HasValue || o.TDL_KSK_CONTRACT_IS_RESTRICTED.Value != Constant.IS_TRUE)
                                    ||
                                    (
                                        o.TDL_KSK_CONTRACT_IS_RESTRICTED.HasValue && o.TDL_KSK_CONTRACT_IS_RESTRICTED.Value == Constant.IS_TRUE &&
                                            kskContractIds.Contains(o.TDL_KSK_CONTRACT_ID.Value)
                                    )
                                )
                              )
                        );
                }

                search.listLHisTreatment3Expression.AddRange(listLHisTreatment3Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listLHisTreatment3Expression.Clear();
                search.listLHisTreatment3Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
