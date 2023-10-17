using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public class HisTreatmentView3FilterQuery : HisTreatmentView3Filter
    {
        public HisTreatmentView3FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_3, bool>>> listVHisTreatment3Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_3, bool>>>();



        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisTreatment3Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatment3Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatment3Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatment3Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    if (!String.IsNullOrEmpty(this.KEY_WORD))
                    {
                        this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                        listVHisTreatment3Expression.Add(o =>
                            o.STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                            o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                            o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                            o.END_CODE.ToLower().Contains(this.KEY_WORD) ||
                            o.IN_CODE.ToLower().Contains(this.KEY_WORD) ||
                            o.TDL_HEIN_CARD_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                            o.TRANSFER_IN_CODE.ToLower().Contains(this.KEY_WORD) ||
                            o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                            o.TDL_PATIENT_ADDRESS.ToLower().Contains(this.KEY_WORD) ||
                            o.OUT_CODE.ToLower().Contains(this.KEY_WORD) ||
                            o.APPOINTMENT_CODE.ToLower().Contains(this.KEY_WORD)
                            );
                    }
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTreatment3Expression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (this.IS_LOCK_HEIN.HasValue && this.IS_LOCK_HEIN.Value)
                {
                    listVHisTreatment3Expression.Add(o => o.IS_LOCK_HEIN.HasValue && o.IS_LOCK_HEIN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_LOCK_HEIN.HasValue && !this.IS_LOCK_HEIN.Value)
                {
                    listVHisTreatment3Expression.Add(o => !o.IS_LOCK_HEIN.HasValue || o.IS_LOCK_HEIN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listVHisTreatment3Expression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listVHisTreatment3Expression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.FEE_LOCK_TIME_FROM.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value >= this.FEE_LOCK_TIME_FROM.Value);
                }
                if (this.FEE_LOCK_TIME_TO.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.FEE_LOCK_TIME.HasValue && o.FEE_LOCK_TIME.Value <= this.FEE_LOCK_TIME_TO.Value);
                }
                if (this.IN_TIME_FROM.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.IN_TIME >= this.IN_TIME_FROM.Value);
                }
                if (this.IN_TIME_TO.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.IN_TIME <= this.IN_TIME_TO.Value);
                }
                if (this.OUT_TIME_FROM.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME >= this.OUT_TIME_FROM.Value);
                }
                if (this.OUT_TIME_TO.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.OUT_TIME.HasValue && o.OUT_TIME <= this.OUT_TIME_TO.Value);
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.END_ROOM_IDs != null)
                {
                    search.listVHisTreatment3Expression.Add(o => o.END_ROOM_ID.HasValue && this.END_ROOM_IDs.Contains(o.END_ROOM_ID.Value));
                }

                if (this.IS_OUT.HasValue && this.IS_OUT.Value)
                {
                    listVHisTreatment3Expression.Add(o => o.OUT_TIME.HasValue);
                }
                if (this.IS_OUT.HasValue && !this.IS_OUT.Value)
                {
                    listVHisTreatment3Expression.Add(o => !o.OUT_TIME.HasValue);
                }
                if (this.TREATMENT_END_TYPE_ID != null)
                {
                    listVHisTreatment3Expression.Add(s => s.TREATMENT_END_TYPE_ID == this.TREATMENT_END_TYPE_ID);
                }
                if (this.IN_DATE_FROM.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.IN_DATE >= this.IN_DATE_FROM.Value);
                }
                if (this.IN_DATE_TO.HasValue)
                {
                    listVHisTreatment3Expression.Add(o => o.IN_DATE <= this.IN_DATE_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.PATIENT_NAME))
                {
                    this.PATIENT_NAME = this.PATIENT_NAME.Trim().ToLower();
                    listVHisTreatment3Expression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.PATIENT_NAME)
                        );
                }
                if (this.DATA_STORE_ID_NULL__OR__INs != null)
                {
                    listVHisTreatment3Expression.Add(o => !o.DATA_STORE_ID.HasValue || this.DATA_STORE_ID_NULL__OR__INs.Contains(o.DATA_STORE_ID.Value));
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    listVHisTreatment3Expression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }
                if (this.BRANCH_ID != null)
                {
                    listVHisTreatment3Expression.Add(s => s.BRANCH_ID == this.BRANCH_ID);
                }

                search.listVHisTreatment3Expression.AddRange(listVHisTreatment3Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatment3Expression.Clear();
                search.listVHisTreatment3Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
