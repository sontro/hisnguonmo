using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public class HisTreatmentView5FilterQuery : HisTreatmentView5Filter
    {
        public HisTreatmentView5FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_5, bool>>> listVHisTreatment5Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_5, bool>>>();


        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisTreatment5Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatment5Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatment5Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatment5Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    //chi tim kiem theo 1 so truong thuong dung de tranh hieu nang
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisTreatment5Expression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_HEIN_CARD_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.IN_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.ICD_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.ICD_SUB_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.ICD_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.ICD_TEXT.ToLower().Contains(this.KEY_WORD) ||
                        o.END_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTreatment5Expression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    listVHisTreatment5Expression.Add(o => o.TDL_PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                if (this.IS_LOCK_HEIN.HasValue && this.IS_LOCK_HEIN.Value)
                {
                    listVHisTreatment5Expression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_LOCK_HEIN.HasValue && !this.IS_LOCK_HEIN.Value)
                {
                    listVHisTreatment5Expression.Add(o => !o.IS_LOCK_HEIN.HasValue || o.IS_LOCK_HEIN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listVHisTreatment5Expression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listVHisTreatment5Expression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.FEE_LOCK_TIME_FROM.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value >= this.FEE_LOCK_TIME_FROM.Value);
                }
                if (this.FEE_LOCK_TIME_TO.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value <= this.FEE_LOCK_TIME_TO.Value);
                }
                if (this.IN_TIME_FROM.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.IN_TIME >= this.IN_TIME_FROM.Value);
                }
                if (this.IN_TIME_TO.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.IN_TIME <= this.IN_TIME_TO.Value);
                }
                if (this.OUT_TIME_FROM.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME >= this.OUT_TIME_FROM.Value);
                }
                if (this.OUT_TIME_TO.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME <= this.OUT_TIME_TO.Value);
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisTreatment5Expression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.IS_OUT.HasValue && this.IS_OUT.Value)
                {
                    listVHisTreatment5Expression.Add(o => o.OUT_TIME.HasValue);
                }
                if (this.IS_OUT.HasValue && !this.IS_OUT.Value)
                {
                    listVHisTreatment5Expression.Add(o => !o.OUT_TIME.HasValue);
                }
                if (this.TREATMENT_END_TYPE_ID != null)
                {
                    listVHisTreatment5Expression.Add(s => s.TREATMENT_END_TYPE_ID == this.TREATMENT_END_TYPE_ID);
                }
                if (this.DATA_STORE_ID_NULL__OR__INs != null)
                {
                    listVHisTreatment5Expression.Add(o => !o.DATA_STORE_ID.HasValue || this.DATA_STORE_ID_NULL__OR__INs.Contains(o.DATA_STORE_ID.Value));
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    listVHisTreatment5Expression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }
                
                search.listVHisTreatment5Expression.AddRange(listVHisTreatment5Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatment5Expression.Clear();
                search.listVHisTreatment5Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
