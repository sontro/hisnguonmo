using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMaterial
{
    public class HisExpMestMaterialLViewFilterQuery : HisExpMestMaterialLViewFilter
    {
        public HisExpMestMaterialLViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_MATERIAL, bool>>> listLHisExpMestMaterialExpression = new List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_MATERIAL, bool>>>();

        internal HisExpMestMaterialSO Query()
        {
            HisExpMestMaterialSO search = new HisExpMestMaterialSO();
            try
            {
                if (this.ID.HasValue)
                {
                    listLHisExpMestMaterialExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listLHisExpMestMaterialExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.EXP_MEST_TYPE_ID.HasValue)
                {
                    listLHisExpMestMaterialExpression.Add(o => o.EXP_MEST_TYPE_ID == this.EXP_MEST_TYPE_ID.Value);
                }
                if (this.EXP_MEST_TYPE_IDs != null)
                {
                    listLHisExpMestMaterialExpression.Add(o => this.EXP_MEST_TYPE_IDs.Contains(o.EXP_MEST_TYPE_ID));
                }
                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listLHisExpMestMaterialExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listLHisExpMestMaterialExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }
                if (this.REPLACE_MATERIAL_TYPE_ID.HasValue)
                {
                    listLHisExpMestMaterialExpression.Add(o => o.REPLACE_MATERIAL_TYPE_ID == this.REPLACE_MATERIAL_TYPE_ID.Value);
                }
                if (this.REPLACE_MATERIAL_TYPE_IDs != null)
                {
                    listLHisExpMestMaterialExpression.Add(o => o.REPLACE_MATERIAL_TYPE_ID.HasValue && this.REPLACE_MATERIAL_TYPE_IDs.Contains(o.REPLACE_MATERIAL_TYPE_ID.Value));
                }
                if (this.TDL_MEDI_STOCK_ID.HasValue)
                {
                    listLHisExpMestMaterialExpression.Add(o => o.TDL_MEDI_STOCK_ID == this.TDL_MEDI_STOCK_ID.Value);
                }
                if (this.TDL_MEDI_STOCK_IDs != null)
                {
                    listLHisExpMestMaterialExpression.Add(o => o.TDL_MEDI_STOCK_ID.HasValue && this.TDL_MEDI_STOCK_IDs.Contains(o.TDL_MEDI_STOCK_ID.Value));
                }
                if (this.IS_REPLACE.HasValue)
                {
                    if (this.IS_REPLACE.Value)
                    {
                        listLHisExpMestMaterialExpression.Add(o => o.REPLACE_MATERIAL_TYPE_ID.HasValue && o.MATERIAL_TYPE_ID != o.REPLACE_MATERIAL_TYPE_ID.Value);
                    }
                    else
                    {
                        listLHisExpMestMaterialExpression.Add(o => o.REPLACE_MATERIAL_TYPE_ID.HasValue && o.MATERIAL_TYPE_ID == o.REPLACE_MATERIAL_TYPE_ID.Value);
                    }
                }

                search.listLHisExpMestMaterialExpression.AddRange(listLHisExpMestMaterialExpression);
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
