using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMaterial
{
    public class HisExpMestMaterialLView2FilterQuery : HisExpMestMaterialLView2Filter
    {
        public HisExpMestMaterialLView2FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_MATERIAL_2, bool>>> listLHisExpMestMaterial2Expression = new List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_MATERIAL_2, bool>>>();

        internal HisExpMestMaterialSO Query()
        {
            HisExpMestMaterialSO search = new HisExpMestMaterialSO();
            try
            {
                if (this.HAS_CHMS_TYPE_ID.HasValue)
                {
                    if (this.HAS_CHMS_TYPE_ID.Value)
                    {
                        listLHisExpMestMaterial2Expression.Add(o => o.CHMS_TYPE_ID.HasValue);
                    }
                    else
                    {
                        listLHisExpMestMaterial2Expression.Add(o => !o.CHMS_TYPE_ID.HasValue);
                    }
                }
                if (this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID != null)
                {
                    listLHisExpMestMaterial2Expression.Add(o => o.IMP_MEDI_STOCK_ID == this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID || o.MEDI_STOCK_ID == this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID);
                }
                if (this.EXP_MEST_STT_ID != null)
                {
                    listLHisExpMestMaterial2Expression.Add(o => this.EXP_MEST_STT_ID.Value == o.EXP_MEST_STT_ID);
                }
                if (this.TDL_MATERIAL_TYPE_IDs != null)
                {
                    listLHisExpMestMaterial2Expression.Add(o => o.TDL_MATERIAL_TYPE_ID.HasValue && this.TDL_MATERIAL_TYPE_IDs.Contains(o.TDL_MATERIAL_TYPE_ID.Value));
                }

                search.listLHisExpMestMaterial2Expression.AddRange(listLHisExpMestMaterial2Expression);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listLHisExpMestMaterialExpression.Clear();
                search.listLHisExpMestMaterialExpression.Add(o => o.ID == -1);
            }
            return search;
        }
    }
}
