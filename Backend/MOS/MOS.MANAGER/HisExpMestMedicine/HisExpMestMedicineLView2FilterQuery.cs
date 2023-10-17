using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMedicine
{
    public class HisExpMestMedicineLView2FilterQuery : HisExpMestMedicineLView2Filter
    {
        public HisExpMestMedicineLView2FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_MEDICINE_2, bool>>> listLHisExpMestMedicine2Expression = new List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_MEDICINE_2, bool>>>();

        internal HisExpMestMedicineSO Query()
        {
            HisExpMestMedicineSO search = new HisExpMestMedicineSO();
            try
            {
                if (this.HAS_CHMS_TYPE_ID.HasValue)
                {
                    if (this.HAS_CHMS_TYPE_ID.Value)
                    {
                        listLHisExpMestMedicine2Expression.Add(o => o.CHMS_TYPE_ID.HasValue);
                    }
                    else
                    {
                        listLHisExpMestMedicine2Expression.Add(o => !o.CHMS_TYPE_ID.HasValue);
                    }
                }
                if (this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID != null)
                {
                    listLHisExpMestMedicine2Expression.Add(o => o.IMP_MEDI_STOCK_ID == this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID || o.MEDI_STOCK_ID == this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID);
                }
                if (this.EXP_MEST_STT_ID != null)
                {
                    listLHisExpMestMedicine2Expression.Add(o => this.EXP_MEST_STT_ID.Value == o.EXP_MEST_STT_ID);
                }
                if (this.TDL_MEDICINE_TYPE_IDs != null)
                {
                    listLHisExpMestMedicine2Expression.Add(o => o.TDL_MEDICINE_TYPE_ID.HasValue && this.TDL_MEDICINE_TYPE_IDs.Contains(o.TDL_MEDICINE_TYPE_ID.Value));
                }

                search.listLHisExpMestMedicine2Expression.AddRange(listLHisExpMestMedicine2Expression);
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
