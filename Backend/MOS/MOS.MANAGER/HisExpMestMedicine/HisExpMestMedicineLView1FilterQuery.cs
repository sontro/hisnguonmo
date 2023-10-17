using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMedicine
{
    public class HisExpMestMedicineLView1FilterQuery : HisExpMestMedicineLView1Filter
    {
        public HisExpMestMedicineLView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_MEDICINE_1, bool>>> listLHisExpMestMedicine1Expression = new List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_MEDICINE_1, bool>>>();

        internal HisExpMestMedicineSO Query()
        {
            HisExpMestMedicineSO search = new HisExpMestMedicineSO();
            try
            {
                if (this.TDL_PATIENT_ID.HasValue)
                {
                    listLHisExpMestMedicine1Expression.Add(o => o.TDL_PATIENT_ID == this.TDL_PATIENT_ID.Value);
                }
                if (this.INTRUCTION_DATE.HasValue)
                {
                    listLHisExpMestMedicine1Expression.Add(o => o.INTRUCTION_DATE == this.INTRUCTION_DATE.Value);
                }
                if (this.TDL_SERVICE_REQ_ID__NOT_EQUAL.HasValue)
                {
                    listLHisExpMestMedicine1Expression.Add(o => o.TDL_SERVICE_REQ_ID != this.TDL_SERVICE_REQ_ID__NOT_EQUAL.Value);
                }

                search.listLHisExpMestMedicine1Expression.AddRange(listLHisExpMestMedicine1Expression);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExpMestMedicineExpression.Clear();
                search.listVHisExpMestMedicineExpression.Add(o => o.ID == -1);
            }
            return search;
        }
    }
}
