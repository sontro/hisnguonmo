using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMaterial
{
    public class HisExpMestMaterialView2FilterQuery : HisExpMestMaterialView2Filter
    {
        public HisExpMestMaterialView2FilterQuery()
            : base()
        {

        }
        
        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_MATERIAL_2, bool>>> listVHisExpMestMaterial2Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_MATERIAL_2, bool>>>();

        

        internal HisExpMestMaterialSO Query()
        {
            HisExpMestMaterialSO search = new HisExpMestMaterialSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisExpMestMaterial2Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.EXP_MEST_ID.HasValue)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.EXP_MEST_ID == this.EXP_MEST_ID.Value);
                }
                if (this.EXP_MEST_IDs != null)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.EXP_MEST_ID.HasValue && this.EXP_MEST_IDs.Contains(o.EXP_MEST_ID.Value));
                }
                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.TDL_MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.MATERIAL_ID.HasValue)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.MATERIAL_ID == this.MATERIAL_ID.Value);
                }
                if (this.MATERIAL_IDs != null)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.MATERIAL_ID.HasValue && this.MATERIAL_IDs.Contains(o.MATERIAL_ID.Value));
                }
                if (this.SERE_SERV_PARENT_ID.HasValue)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.SERE_SERV_PARENT_ID.HasValue && o.SERE_SERV_PARENT_ID == this.SERE_SERV_PARENT_ID.Value);
                }
                if (this.SERE_SERV_PARENT_IDs != null)
                {
                    listVHisExpMestMaterial2Expression.Add(o => o.SERE_SERV_PARENT_ID.HasValue && this.SERE_SERV_PARENT_IDs.Contains(o.SERE_SERV_PARENT_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower();
                    listVHisExpMestMaterial2Expression.Add(o => o.APP_CREATOR.Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.Contains(this.KEY_WORD) ||
                        o.CREATOR.Contains(this.KEY_WORD) ||
                        o.DESCRIPTION.Contains(this.KEY_WORD) ||
                        o.MATERIAL_TYPE_CODE.Contains(this.KEY_WORD) ||
                        o.MATERIAL_TYPE_NAME.Contains(this.KEY_WORD) ||
                        o.NATIONAL_NAME.Contains(this.KEY_WORD)
                        );
                }

                search.listVHisExpMestMaterial2Expression.AddRange(listVHisExpMestMaterial2Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExpMestMaterial2Expression.Clear();
                search.listVHisExpMestMaterial2Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
