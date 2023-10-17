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
    public class HisTreatmentFeeView1FilterQuery : HisTreatmentFeeView1Filter
    {
        public HisTreatmentFeeView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE_1, bool>>> listVHisTreatmentFee1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE_1, bool>>>();

        

        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatmentFee1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisTreatmentFee1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatmentFee1Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatmentFee1Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatmentFee1Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatmentFee1Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatmentFee1Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatmentFee1Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatmentFee1Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatmentFee1Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.PATIENT_ID.HasValue)
                {
                    listVHisTreatmentFee1Expression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.VIR_PATIENT_NAME))
                {
                    listVHisTreatmentFee1Expression.Add(o => o.TDL_PATIENT_NAME.ToLower().Contains(this.VIR_PATIENT_NAME.ToLower().Trim()));
                }
                if (!String.IsNullOrEmpty(this.PATIENT_CODE))
                {
                    listVHisTreatmentFee1Expression.Add(o => o.TDL_PATIENT_CODE.ToLower().Contains(this.PATIENT_CODE.ToLower().Trim()));
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE))
                {
                    listVHisTreatmentFee1Expression.Add(o => o.TREATMENT_CODE.ToLower().Contains(this.TREATMENT_CODE.ToLower().Trim()));
                }
                if (this.IS_LOCK_HEIN.HasValue)
                {
                    listVHisTreatmentFee1Expression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == this.IS_LOCK_HEIN.Value);
                }
                if (this.PATIENT_IDs != null)
                {
                    listVHisTreatmentFee1Expression.Add(o => this.PATIENT_IDs.Contains(o.PATIENT_ID));
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisTreatmentFee1Expression.Add(o =>
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
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listVHisTreatmentFee1Expression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE == Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listVHisTreatmentFee1Expression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE != Constant.IS_TRUE);
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    listVHisTreatmentFee1Expression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }

                search.listVHisTreatmentFee1Expression.AddRange(listVHisTreatmentFee1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatmentFee1Expression.Clear();
                search.listVHisTreatmentFee1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
