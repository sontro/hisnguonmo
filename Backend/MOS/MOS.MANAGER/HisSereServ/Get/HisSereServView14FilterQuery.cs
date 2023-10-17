using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
    public class HisSereServView14FilterQuery : HisSereServView14Filter
    {
        public HisSereServView14FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_14, bool>>> listVHisSereServ14Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_14, bool>>>();



        internal HisSereServSO Query()
        {
            HisSereServSO search = new HisSereServSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisSereServ14Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisSereServ14Expression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.IS_EXPEND.HasValue && this.IS_EXPEND.Value)
                {
                    listVHisSereServ14Expression.Add(o => o.IS_EXPEND.HasValue && o.IS_EXPEND.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_EXPEND.HasValue && !this.IS_EXPEND.Value)
                {
                    listVHisSereServ14Expression.Add(o => !o.IS_EXPEND.HasValue || o.IS_EXPEND.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_EXECUTE.HasValue && !this.HAS_EXECUTE.Value)
                {
                    listVHisSereServ14Expression.Add(o => o.IS_NO_EXECUTE.HasValue && o.IS_NO_EXECUTE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_EXECUTE.HasValue && this.HAS_EXECUTE.Value)
                {
                    listVHisSereServ14Expression.Add(o => !o.IS_NO_EXECUTE.HasValue || o.IS_NO_EXECUTE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                
                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisSereServ14Expression.Add(o => o.TDL_TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisSereServ14Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID.Value));
                }
                if (this.HAS_MUST_PAY_PRICE.HasValue && !this.HAS_MUST_PAY_PRICE.Value)
                {
                    listVHisSereServ14Expression.Add(o => !o.VIR_TOTAL_PATIENT_PRICE.HasValue || o.VIR_TOTAL_PATIENT_PRICE <= o.TOTAL_DEBT_PRICE);
                }
                if (this.HAS_MUST_PAY_PRICE.HasValue && this.HAS_MUST_PAY_PRICE.Value)
                {
                    listVHisSereServ14Expression.Add(o => o.VIR_TOTAL_PATIENT_PRICE.HasValue && o.VIR_TOTAL_PATIENT_PRICE > o.TOTAL_DEBT_PRICE);
                }

                if (!String.IsNullOrWhiteSpace(KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisSereServ14Expression.Add(o => o.TDL_SERVICE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisSereServ14Expression.AddRange(listVHisSereServ14Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisSereServ14Expression.Clear();
                search.listVHisSereServ14Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
