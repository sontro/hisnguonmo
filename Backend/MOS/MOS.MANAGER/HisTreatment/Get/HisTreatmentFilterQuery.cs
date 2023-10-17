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
    public class HisTreatmentFilterQuery : HisTreatmentFilter
    {
        public HisTreatmentFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT, bool>>> listHisTreatmentExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT, bool>>>();


        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listHisTreatmentExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTreatmentExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTreatmentExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTreatmentExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.PATIENT_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }
                if (this.TREATMENT_END_TYPE_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.TREATMENT_END_TYPE_ID.HasValue && o.TREATMENT_END_TYPE_ID.Value == this.TREATMENT_END_TYPE_ID.Value);
                }
                if (this.TREATMENT_RESULT_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.TREATMENT_RESULT_ID.HasValue && o.TREATMENT_RESULT_ID.Value == this.TREATMENT_RESULT_ID.Value);
                }
                if (this.IN_TIME_FROM.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.IN_TIME >= this.IN_TIME_FROM.Value);
                }
                if (this.IN_TIME_TO.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.IN_TIME <= this.IN_TIME_TO.Value);
                }
                if (this.IS_OUT.HasValue && this.IS_OUT.Value)
                {
                    listHisTreatmentExpression.Add(o => o.OUT_TIME.HasValue);
                }
                if (this.IS_OUT.HasValue && !this.IS_OUT.Value)
                {
                    listHisTreatmentExpression.Add(o => !o.OUT_TIME.HasValue);
                }
                if (this.EMERGENCY_WTIME_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.EMERGENCY_WTIME_ID.HasValue && o.EMERGENCY_WTIME_ID.Value == this.EMERGENCY_WTIME_ID.Value);
                }
                if (this.EMERGENCY_WTIME_IDs != null)
                {
                    listHisTreatmentExpression.Add(o => o.EMERGENCY_WTIME_ID.HasValue && this.EMERGENCY_WTIME_IDs.Contains(o.EMERGENCY_WTIME_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.STORE_CODE__END_WITH))
                {
                    listHisTreatmentExpression.Add(o => o.STORE_CODE != null && o.STORE_CODE.EndsWith(this.STORE_CODE__END_WITH));
                }
                if (!String.IsNullOrEmpty(this.STORE_CODE__START_WITH))
                {
                    listHisTreatmentExpression.Add(o => o.STORE_CODE != null && o.STORE_CODE.StartsWith(this.STORE_CODE__START_WITH));
                }
                if (!String.IsNullOrEmpty(this.EXTRA_END_CODE__END_WITH))
                {
                    listHisTreatmentExpression.Add(o => o.EXTRA_END_CODE != null && o.EXTRA_END_CODE.EndsWith(this.EXTRA_END_CODE__END_WITH));
                }
                if (!String.IsNullOrEmpty(this.EXTRA_END_CODE__START_WITH))
                {
                    listHisTreatmentExpression.Add(o => o.EXTRA_END_CODE != null && o.EXTRA_END_CODE.StartsWith(this.EXTRA_END_CODE__START_WITH));
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listHisTreatmentExpression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.IN_CODE__EXACT))
                {
                    listHisTreatmentExpression.Add(o => o.IN_CODE == this.IN_CODE__EXACT);
                }
                if (this.OWE_TYPE_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.OWE_TYPE_ID.HasValue && o.OWE_TYPE_ID.Value == this.OWE_TYPE_ID.Value);
                }
                if (this.OWE_TYPE_IDs != null)
                {
                    search.listHisTreatmentExpression.Add(o => o.OWE_TYPE_ID.HasValue && this.OWE_TYPE_IDs.Contains(o.OWE_TYPE_ID.Value));
                }
                if (this.DEATH_WITHIN_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.DEATH_WITHIN_ID.HasValue && o.DEATH_WITHIN_ID.Value == this.DEATH_WITHIN_ID.Value);
                }
                if (this.DEATH_WITHIN_IDs != null)
                {
                    search.listHisTreatmentExpression.Add(o => o.DEATH_WITHIN_ID.HasValue && this.DEATH_WITHIN_IDs.Contains(o.DEATH_WITHIN_ID.Value));
                }
                if (this.DEATH_CAUSE_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.DEATH_CAUSE_ID.HasValue && o.DEATH_CAUSE_ID.Value == this.DEATH_CAUSE_ID.Value);
                }
                if (this.DEATH_CAUSE_IDs != null)
                {
                    search.listHisTreatmentExpression.Add(o => o.DEATH_CAUSE_ID.HasValue && this.DEATH_CAUSE_IDs.Contains(o.DEATH_CAUSE_ID.Value));
                }
                if (this.TRAN_PATI_FORM_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.TRAN_PATI_FORM_ID.HasValue && o.TRAN_PATI_FORM_ID.Value == this.TRAN_PATI_FORM_ID.Value);
                }
                if (this.TRAN_PATI_FORM_IDs != null)
                {
                    search.listHisTreatmentExpression.Add(o => o.TRAN_PATI_FORM_ID.HasValue && this.TRAN_PATI_FORM_IDs.Contains(o.TRAN_PATI_FORM_ID.Value));
                }
                if (this.TRAN_PATI_REASON_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.TRAN_PATI_REASON_ID.HasValue && o.TRAN_PATI_REASON_ID.Value == this.TRAN_PATI_REASON_ID.Value);
                }
                if (this.TRAN_PATI_REASON_IDs != null)
                {
                    listHisTreatmentExpression.Add(o => o.TRAN_PATI_REASON_ID.HasValue && this.TRAN_PATI_REASON_IDs.Contains(o.TRAN_PATI_REASON_ID.Value));
                }
                if (this.BRANCH_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listHisTreatmentExpression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }
                if (!String.IsNullOrEmpty(this.APPOINTMENT_CODE__EXACT))
                {
                    listHisTreatmentExpression.Add(o => o.APPOINTMENT_CODE == this.APPOINTMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_PATIENT_CODE__EXACT))
                {
                    listHisTreatmentExpression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (this.IN_DATE_FROM.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.IN_DATE >= this.IN_DATE_FROM.Value);
                }
                if (this.IN_DATE_TO.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.IN_DATE <= this.IN_DATE_TO.Value);
                }
                if (this.IS_YDT_UPLOAD.HasValue && this.IS_YDT_UPLOAD.Value)
                {
                    listHisTreatmentExpression.Add(o => o.IS_YDT_UPLOAD.HasValue && o.IS_YDT_UPLOAD.Value == Constant.IS_TRUE);
                }
                if (this.IS_YDT_UPLOAD.HasValue && !this.IS_YDT_UPLOAD.Value)
                {
                    listHisTreatmentExpression.Add(o => !o.IS_YDT_UPLOAD.HasValue || o.IS_YDT_UPLOAD.Value != Constant.IS_TRUE);
                }
                if (this.IS_LOCK_HEIN.HasValue && this.IS_LOCK_HEIN.Value)
                {
                    listHisTreatmentExpression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_LOCK_HEIN.HasValue && !this.IS_LOCK_HEIN.Value)
                {
                    listHisTreatmentExpression.Add(o => !o.IS_LOCK_HEIN.HasValue || o.IS_LOCK_HEIN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listHisTreatmentExpression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listHisTreatmentExpression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (!String.IsNullOrWhiteSpace(this.ICD_CODE_OR_ICD_SUB_CODE))
                {
                    listHisTreatmentExpression.Add(o => o.ICD_CODE == this.ICD_CODE_OR_ICD_SUB_CODE || o.ICD_SUB_CODE.Contains(this.ICD_CODE_OR_ICD_SUB_CODE));
                }
                if (this.TDL_KSK_CONTRACT_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.TDL_KSK_CONTRACT_ID.HasValue && o.TDL_KSK_CONTRACT_ID.Value == this.TDL_KSK_CONTRACT_ID.Value);
                }
                if (this.TDL_KSK_CONTRACT_IDs != null)
                {
                    listHisTreatmentExpression.Add(o => o.TDL_KSK_CONTRACT_ID.HasValue && this.TDL_KSK_CONTRACT_IDs.Contains(o.TDL_KSK_CONTRACT_ID.Value));
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisTreatmentExpression.Add(o =>
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (this.IS_LOCK_FEE.HasValue && this.IS_LOCK_FEE.Value)
                {
                    listHisTreatmentExpression.Add(o => o.IS_ACTIVE == null || o.IS_ACTIVE == MOS.UTILITY.Constant.IS_FALSE);
                }
                if (this.IS_LOCK_FEE.HasValue && !this.IS_LOCK_FEE.Value)
                {
                    listHisTreatmentExpression.Add(o => o.IS_ACTIVE.HasValue || o.IS_ACTIVE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (!String.IsNullOrEmpty(this.IN_CODE__END_WITH))
                {
                    listHisTreatmentExpression.Add(o => o.IN_CODE != null && o.IN_CODE.EndsWith(this.IN_CODE__END_WITH));
                }
                if (!String.IsNullOrEmpty(this.IN_CODE__START_WITH))
                {
                    listHisTreatmentExpression.Add(o => o.IN_CODE != null && o.IN_CODE.StartsWith(this.IN_CODE__START_WITH));
                }
                if (!String.IsNullOrEmpty(this.HRM_KSK_CODE__EXACT))
                {
                    listHisTreatmentExpression.Add(o => o.HRM_KSK_CODE == this.HRM_KSK_CODE__EXACT);
                }
                if (this.DATA_STORE_ID_NULL__OR__INs != null)
                {
                    listHisTreatmentExpression.Add(o => !o.DATA_STORE_ID.HasValue || this.DATA_STORE_ID_NULL__OR__INs.Contains(o.DATA_STORE_ID.Value));
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    listHisTreatmentExpression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }
                if (this.MEDI_RECORD_TYPE_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.MEDI_RECORD_TYPE_ID.HasValue && o.MEDI_RECORD_TYPE_ID.Value == this.MEDI_RECORD_TYPE_ID.Value);
                }
                if (this.MEDI_RECORD_TYPE_IDs != null)
                {
                    search.listHisTreatmentExpression.Add(o => o.MEDI_RECORD_TYPE_ID.HasValue && this.MEDI_RECORD_TYPE_IDs.Contains(o.MEDI_RECORD_TYPE_ID.Value));
                }
                if (this.MEDI_RECORD_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.MEDI_RECORD_ID.HasValue && o.MEDI_RECORD_ID.Value == this.MEDI_RECORD_ID.Value);
                }
                if (this.MEDI_RECORD_IDs != null)
                {
                    search.listHisTreatmentExpression.Add(o => o.MEDI_RECORD_ID.HasValue && this.MEDI_RECORD_IDs.Contains(o.MEDI_RECORD_ID.Value));
                }

                if (this.XML4210_RESULT.HasValue)
                {
                    listHisTreatmentExpression.Add(s => s.XML4210_RESULT.HasValue && s.XML4210_RESULT.Value == this.XML4210_RESULT.Value);
                }
                if (this.XML4210_RESULTs != null)
                {
                    listHisTreatmentExpression.Add(o => o.XML4210_RESULT.HasValue && this.XML4210_RESULTs.Contains(o.XML4210_RESULT.Value));
                }
                if (this.COLLINEAR_XML4210_RESULT.HasValue)
                {
                    listHisTreatmentExpression.Add(s => s.COLLINEAR_XML4210_RESULT.HasValue && s.COLLINEAR_XML4210_RESULT.Value == this.COLLINEAR_XML4210_RESULT.Value);
                }
                if (this.COLLINEAR_XML4210_RESULTs != null)
                {
                    listHisTreatmentExpression.Add(o => o.COLLINEAR_XML4210_RESULT.HasValue && this.COLLINEAR_XML4210_RESULTs.Contains(o.COLLINEAR_XML4210_RESULT.Value));
                }

                if (this.XML4210_RESULT__OR__COLLINEAR_XML4210_RESULT.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.COLLINEAR_XML4210_RESULT == this.XML4210_RESULT__OR__COLLINEAR_XML4210_RESULT || o.XML4210_RESULT == this.XML4210_RESULT__OR__COLLINEAR_XML4210_RESULT);
                }

                if (this.FEE_LOCK_TIME__FROM.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value >= this.FEE_LOCK_TIME__FROM.Value);
                }
                if (this.FEE_LOCK_TIME__TO.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value <= this.FEE_LOCK_TIME__TO.Value);
                }

                if (this.IS_APPROVE_FINISH.HasValue && this.IS_APPROVE_FINISH.Value)
                {
                    listHisTreatmentExpression.Add(o => o.IS_APPROVE_FINISH.HasValue && o.IS_APPROVE_FINISH.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_APPROVE_FINISH.HasValue && !this.IS_APPROVE_FINISH.Value)
                {
                    listHisTreatmentExpression.Add(o => !o.IS_APPROVE_FINISH.HasValue || o.IS_APPROVE_FINISH.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_KSK_APPROVE.HasValue && this.IS_KSK_APPROVE.Value)
                {
                    listHisTreatmentExpression.Add(o => o.IS_KSK_APPROVE.HasValue && o.IS_KSK_APPROVE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_KSK_APPROVE.HasValue && !this.IS_KSK_APPROVE.Value)
                {
                    listHisTreatmentExpression.Add(o => !o.IS_KSK_APPROVE.HasValue || o.IS_KSK_APPROVE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.APPOINTMENT_PERIOD_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.APPOINTMENT_PERIOD_ID == this.APPOINTMENT_PERIOD_ID.Value);
                }
                if (this.DOCUMENT_BOOK_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.DOCUMENT_BOOK_ID == this.DOCUMENT_BOOK_ID.Value);
                }
                if (this.APPROVAL_STORE_STT_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.APPROVAL_STORE_STT_ID.HasValue && o.APPROVAL_STORE_STT_ID.Value == this.APPROVAL_STORE_STT_ID.Value);
                }
                if (this.APPROVAL_STORE_STT_IDs != null)
                {
                    search.listHisTreatmentExpression.Add(o => o.APPROVAL_STORE_STT_ID.HasValue && this.APPROVAL_STORE_STT_IDs.Contains(o.APPROVAL_STORE_STT_ID.Value));
                }
                if (this.PATIENT_CLASSIFY_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.TDL_PATIENT_CLASSIFY_ID.HasValue && o.TDL_PATIENT_CLASSIFY_ID.Value == this.PATIENT_CLASSIFY_ID.Value);
                }
                if (this.PATIENT_CLASSIFY_IDs != null)
                {
                    listHisTreatmentExpression.Add(o => o.TDL_PATIENT_CLASSIFY_ID.HasValue && this.PATIENT_CLASSIFY_IDs.Contains(o.TDL_PATIENT_CLASSIFY_ID.Value));
                }

                if (this.OUT_DATE.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.OUT_DATE.HasValue && o.OUT_DATE == this.OUT_DATE.Value);
                }
                if (this.IN_DATE.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.IN_DATE == this.IN_DATE.Value);
                }

                if (this.TDL_TREATMENT_TYPE_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.TDL_TREATMENT_TYPE_ID == this.TDL_TREATMENT_TYPE_ID.Value);
                }

                if (!String.IsNullOrEmpty(this.DOCTOR_LOGINNAME__EXACT))
                {
                    listHisTreatmentExpression.Add(o => o.DOCTOR_LOGINNAME == this.DOCTOR_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.STORE_CODE__EXACT))
                {
                    listHisTreatmentExpression.Add(o => o.STORE_CODE == this.STORE_CODE__EXACT);
                }
                if (this.TREATMENT_END_TYPE_EXT_ID.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.TREATMENT_END_TYPE_EXT_ID.HasValue && o.TREATMENT_END_TYPE_EXT_ID.Value == this.TREATMENT_END_TYPE_EXT_ID.Value);
                }
                if (this.OUT_DATE_FROM.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.OUT_DATE >= this.OUT_DATE_FROM.Value);
                }
                if (this.OUT_DATE_TO.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.OUT_DATE <= this.OUT_DATE_TO.Value);
                }
                if (this.HAS_TRAN_PATI_BOOK_NUMBER.HasValue)
                {
                    if (this.HAS_TRAN_PATI_BOOK_NUMBER.Value)
                    {
                        listHisTreatmentExpression.Add(o => o.TRAN_PATI_BOOK_NUMBER.HasValue);
                    }
                    else
                    {
                        listHisTreatmentExpression.Add(o => !o.TRAN_PATI_BOOK_NUMBER.HasValue);
                    }
                }
                if (this.OUT_TIME_FROM.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.OUT_TIME >= this.OUT_TIME_FROM.Value);
                }
                if (this.OUT_TIME_TO.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.OUT_TIME <= this.OUT_TIME_TO.Value);
                }
                if (this.HAS_XML4210_URL.HasValue)
                {
                    if (this.HAS_XML4210_URL.Value)
                    {
                        listHisTreatmentExpression.Add(o => !string.IsNullOrEmpty(o.XML4210_URL));
                    }
                    else
                    {
                        listHisTreatmentExpression.Add(o => string.IsNullOrEmpty(o.XML4210_URL));
                    }
                }
                if (this.IS_BHYT_PATIENT_TYPE.HasValue)
                {
                    if (this.IS_BHYT_PATIENT_TYPE.Value)
                    {
                        listHisTreatmentExpression.Add(o => o.TDL_PATIENT_TYPE_ID == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisTreatmentExpression.Add(o => o.TDL_PATIENT_TYPE_ID != Constant.IS_TRUE);
                    }
                }
                if (this.HEIN_LOCK_TIME_FROM.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.HEIN_LOCK_TIME >= this.HEIN_LOCK_TIME_FROM.Value);
                }
                if (this.HEIN_LOCK_TIME_TO.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.HEIN_LOCK_TIME <= this.HEIN_LOCK_TIME_TO.Value);
                }
                if (this.IS_NOI_TRU_TREATMENT_TYPE.HasValue)
                {
                    if (this.IS_NOI_TRU_TREATMENT_TYPE.Value)
                    {
                        listHisTreatmentExpression.Add(o => o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU);
                    }
                    else
                    {
                        listHisTreatmentExpression.Add(o => o.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU);
                    }
                }
                if (this.HAS_AUTO_CREATE_RATION.HasValue)
                {
                    if (this.HAS_AUTO_CREATE_RATION.Value)
                    {
                        listHisTreatmentExpression.Add(o => o.HAS_AUTO_CREATE_RATION == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisTreatmentExpression.Add(o => o.HAS_AUTO_CREATE_RATION != Constant.IS_TRUE);
                    }
                }
                if (this.HAS_MEDI_RECORD.HasValue)
                {
                    if (this.HAS_MEDI_RECORD.Value)
                    {
                        listHisTreatmentExpression.Add(o => o.MEDI_RECORD_ID.HasValue);
                    }
                    else
                    {
                        listHisTreatmentExpression.Add(o => !o.MEDI_RECORD_ID.HasValue);
                    }
                }
                if (this.HAS_STORE_CODE.HasValue)
                {
                    if (this.HAS_STORE_CODE.Value)
                    {
                        listHisTreatmentExpression.Add(o => !string.IsNullOrEmpty(o.STORE_CODE));
                    }
                    else
                    {
                        listHisTreatmentExpression.Add(o => string.IsNullOrEmpty(o.STORE_CODE));
                    }
                }

                if (this.TDL_TREATMENT_TYPE_IDs != null)
                {
                    listHisTreatmentExpression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && this.TDL_TREATMENT_TYPE_IDs.Contains(o.TDL_TREATMENT_TYPE_ID.Value));
                }

                if (this.DEATH_SYNC_RESULT_TYPE.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.DEATH_SYNC_RESULT_TYPE == this.DEATH_SYNC_RESULT_TYPE.Value);
                }

                if (this.DEATH_TIME_FROM.HasValue)
                {
                    listHisTreatmentExpression.Add(o =>o.DEATH_TIME != null && o.DEATH_TIME >= this.DEATH_TIME_FROM.Value);
                }
                if (this.DEATH_TIME_TO.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.DEATH_TIME != null && o.DEATH_TIME <= this.DEATH_TIME_TO.Value);
                }
                if (this.APPOINTMENT_DATE.HasValue)
                {
                    listHisTreatmentExpression.Add(o => o.APPOINTMENT_DATE != null && o.APPOINTMENT_DATE == this.APPOINTMENT_DATE.Value);
                }
                if (this.HAS_MOBILE.HasValue)
                {
                    if (this.HAS_MOBILE.Value)
                    {
                        listHisTreatmentExpression.Add(o => !string.IsNullOrEmpty(o.TDL_PATIENT_MOBILE) || !string.IsNullOrEmpty(o.TDL_PATIENT_PHONE) || !string.IsNullOrEmpty(o.TDL_PATIENT_RELATIVE_MOBILE) || !string.IsNullOrEmpty(o.TDL_PATIENT_RELATIVE_PHONE));
                    }
                    else
                    {
                        listHisTreatmentExpression.Add(o => string.IsNullOrEmpty(o.TDL_PATIENT_MOBILE) && string.IsNullOrEmpty(o.TDL_PATIENT_PHONE) && string.IsNullOrEmpty(o.TDL_PATIENT_RELATIVE_MOBILE) && string.IsNullOrEmpty(o.TDL_PATIENT_RELATIVE_PHONE));
                    }
                }
                search.listHisTreatmentExpression.AddRange(listHisTreatmentExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
                search.DynamicColumns = this.ColumnParams;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTreatmentExpression.Clear();
                search.listHisTreatmentExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
