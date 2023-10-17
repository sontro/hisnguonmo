using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public class HisTreatmentLView1FilterQuery : HisTreatmentLView1Filter
    {
        public HisTreatmentLView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_1, bool>>> listLHisTreatment1Expression = new List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_1, bool>>>();



        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listLHisTreatment1Expression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listLHisTreatment1Expression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listLHisTreatment1Expression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }

                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listLHisTreatment1Expression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (this.LAST_DEPARTMENT_ID.HasValue)
                {
                    listLHisTreatment1Expression.Add(o => o.LAST_DEPARTMENT_ID == this.LAST_DEPARTMENT_ID.Value);
                }
                if (this.TDL_TREATMENT_TYPE_ID.HasValue)
                {
                    listLHisTreatment1Expression.Add(o => o.TDL_TREATMENT_TYPE_ID == this.TDL_TREATMENT_TYPE_ID.Value);
                }
                if (this.TOTAL_HEIN_PRICE_FROM.HasValue)
                {
                    listLHisTreatment1Expression.Add(o => o.TOTAL_HEIN_PRICE >= this.TOTAL_HEIN_PRICE_FROM.Value);
                }
                if (this.TOTAL_HEIN_PRICE_TO.HasValue)
                {
                    listLHisTreatment1Expression.Add(o => o.TOTAL_HEIN_PRICE <= this.TOTAL_HEIN_PRICE_TO.Value);
                }
                if (this.IN_DATE_FROM.HasValue)
                {
                    listLHisTreatment1Expression.Add(o => o.IN_DATE >= this.IN_DATE_FROM.Value);
                }
                if (this.IN_DATE_TO.HasValue)
                {
                    listLHisTreatment1Expression.Add(o => o.IN_DATE <= this.IN_DATE_TO.Value);
                }

                search.listLHisTreatment1Expression.AddRange(listLHisTreatment1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listLHisTreatment1Expression.Clear();
                search.listLHisTreatment1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
