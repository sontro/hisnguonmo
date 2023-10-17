using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMedicine
{
    public class HisExpMestMedicineLViewFilterQuery : HisExpMestMedicineLViewFilter
    {
        public HisExpMestMedicineLViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_MEDICINE, bool>>> listLHisExpMestMedicineExpression = new List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_MEDICINE, bool>>>();

        internal HisExpMestMedicineSO Query()
        {
            HisExpMestMedicineSO search = new HisExpMestMedicineSO();
            try
            {
                if (this.ID.HasValue)
                {
                    listLHisExpMestMedicineExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listLHisExpMestMedicineExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.EXP_MEST_TYPE_ID.HasValue)
                {
                    listLHisExpMestMedicineExpression.Add(o => o.EXP_MEST_TYPE_ID == this.EXP_MEST_TYPE_ID.Value);
                }
                if (this.EXP_MEST_TYPE_IDs != null)
                {
                    listLHisExpMestMedicineExpression.Add(o => this.EXP_MEST_TYPE_IDs.Contains(o.EXP_MEST_TYPE_ID));
                }
                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listLHisExpMestMedicineExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listLHisExpMestMedicineExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }
                if (this.REPLACE_MEDICINE_TYPE_ID.HasValue)
                {
                    listLHisExpMestMedicineExpression.Add(o => o.REPLACE_MEDICINE_TYPE_ID == this.REPLACE_MEDICINE_TYPE_ID.Value);
                }
                if (this.REPLACE_MEDICINE_TYPE_IDs != null)
                {
                    listLHisExpMestMedicineExpression.Add(o => o.REPLACE_MEDICINE_TYPE_ID.HasValue && this.REPLACE_MEDICINE_TYPE_IDs.Contains(o.REPLACE_MEDICINE_TYPE_ID.Value));
                } if (this.TDL_MEDI_STOCK_ID.HasValue)
                {
                    listLHisExpMestMedicineExpression.Add(o => o.TDL_MEDI_STOCK_ID == this.TDL_MEDI_STOCK_ID.Value);
                }
                if (this.TDL_MEDI_STOCK_IDs != null)
                {
                    listLHisExpMestMedicineExpression.Add(o => o.TDL_MEDI_STOCK_ID.HasValue && this.TDL_MEDI_STOCK_IDs.Contains(o.TDL_MEDI_STOCK_ID.Value));
                }
                if (this.IS_REPLACE.HasValue)
                {
                    if (this.IS_REPLACE.Value)
                    {
                        listLHisExpMestMedicineExpression.Add(o => o.REPLACE_MEDICINE_TYPE_ID.HasValue && o.MEDICINE_TYPE_ID != o.REPLACE_MEDICINE_TYPE_ID.Value);
                    }
                    else
                    {
                        listLHisExpMestMedicineExpression.Add(o => o.REPLACE_MEDICINE_TYPE_ID.HasValue && o.MEDICINE_TYPE_ID == o.REPLACE_MEDICINE_TYPE_ID.Value);
                    }
                }

                search.listLHisExpMestMedicineExpression.AddRange(listLHisExpMestMedicineExpression);
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
