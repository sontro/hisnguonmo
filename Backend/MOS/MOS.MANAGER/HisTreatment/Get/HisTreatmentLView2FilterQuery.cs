using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public class HisTreatmentLView2FilterQuery : HisTreatmentLView2Filter
    {
        public HisTreatmentLView2FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_2, bool>>> listLHisTreatment2Expression = new List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_2, bool>>>();



        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listLHisTreatment2Expression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listLHisTreatment2Expression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listLHisTreatment2Expression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.ID.HasValue)
                {
                    listLHisTreatment2Expression.Add(o => o.ID == this.ID);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listLHisTreatment2Expression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listLHisTreatment2Expression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listLHisTreatment2Expression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }

                search.listLHisTreatment2Expression.AddRange(listLHisTreatment2Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listLHisTreatment2Expression.Clear();
                search.listLHisTreatment2Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
