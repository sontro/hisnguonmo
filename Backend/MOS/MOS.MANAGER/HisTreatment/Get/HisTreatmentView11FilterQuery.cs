using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public class HisTreatmentView11FilterQuery : HisTreatmentView11Filter
    {
        public HisTreatmentView11FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_11, bool>>> listVHisTreatment11Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_11, bool>>>();



        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisTreatment11Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatment11Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatment11Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatment11Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    //Chi tim theo 1 so truong thuong su dung, de tranh van de ve hieu nang
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisTreatment11Expression.Add(o =>
                        o.STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.END_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_HEIN_CARD_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.TRANSFER_IN_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.OUT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_ADDRESS.Contains(this.KEY_WORD) ||
                        o.END_DEPARTMENT_NAME.Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_GENDER_NAME.Contains(this.KEY_WORD)
                        );
                }
				
				if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    listVHisTreatment11Expression.Add(o => o.TDL_PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTreatment11Expression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
				if (!String.IsNullOrEmpty(this.MR_STORE_CODE__EXACT))
                {
                    listVHisTreatment11Expression.Add(o => o.MR_STORE_CODE == this.MR_STORE_CODE__EXACT);
                }
				if (this.STORE_DATE_FROM.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.STORE_DATE.HasValue && o.STORE_DATE >= this.STORE_DATE_FROM.Value);
                }
                if (this.STORE_DATE_TO.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.STORE_DATE.HasValue && o.STORE_DATE <= this.STORE_DATE_TO.Value);
                }
				if (this.IN_DATE_FROM.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.IN_DATE >= this.IN_DATE_FROM.Value);
                }
                if (this.IN_DATE_TO.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.IN_DATE <= this.IN_DATE_TO.Value);
                }
				if (this.OUT_DATE_FROM.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.OUT_DATE.HasValue && o.OUT_DATE >= this.OUT_DATE_FROM.Value);
                }
                if (this.OUT_DATE_TO.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.OUT_DATE.HasValue && o.OUT_DATE <= this.OUT_DATE_TO.Value);
                }
				if (this.LAST_DEPARTMENT_ID.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.LAST_DEPARTMENT_ID.HasValue && o.LAST_DEPARTMENT_ID == this.LAST_DEPARTMENT_ID.Value);
                }
				if (this.END_DEPARTMENT_ID.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.END_DEPARTMENT_ID.HasValue && o.END_DEPARTMENT_ID == this.END_DEPARTMENT_ID.Value);
                }
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listVHisTreatment11Expression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listVHisTreatment11Expression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_MEDI_RECORD.HasValue && this.HAS_MEDI_RECORD.Value)
                {
                    listVHisTreatment11Expression.Add(o => o.MEDI_RECORD_ID.HasValue);
                }
                if (this.HAS_MEDI_RECORD.HasValue && !this.HAS_MEDI_RECORD.Value)
                {
                    listVHisTreatment11Expression.Add(o => !o.MEDI_RECORD_ID.HasValue);
                }
                if (this.HAS_RECORD_INSPECTION_STT_ID.HasValue && this.HAS_RECORD_INSPECTION_STT_ID.Value)
                {
                    listVHisTreatment11Expression.Add(o => o.RECORD_INSPECTION_STT_ID.HasValue);
                }
                if (this.HAS_RECORD_INSPECTION_STT_ID.HasValue && !this.HAS_RECORD_INSPECTION_STT_ID.Value)
                {
                    listVHisTreatment11Expression.Add(o => !o.RECORD_INSPECTION_STT_ID.HasValue);
                }
                if (this.RECORD_INSPECTION_STT_ID.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.RECORD_INSPECTION_STT_ID.HasValue && o.RECORD_INSPECTION_STT_ID == this.RECORD_INSPECTION_STT_ID.Value);
                }
                if (this.BRANCH_ID.HasValue)
                {
                    listVHisTreatment11Expression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }

                search.listVHisTreatment11Expression.AddRange(listVHisTreatment11Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatment11Expression.Clear();
                search.listVHisTreatment11Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
