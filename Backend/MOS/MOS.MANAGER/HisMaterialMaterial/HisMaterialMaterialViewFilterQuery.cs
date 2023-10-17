using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialMaterial
{
    public class HisMaterialMaterialViewFilterQuery : HisMaterialMaterialViewFilter
    {
        public HisMaterialMaterialViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MATERIAL_MATERIAL, bool>>> listVHisMaterialMaterialExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MATERIAL_MATERIAL, bool>>>();

        

        internal HisMaterialMaterialSO Query()
        {
            HisMaterialMaterialSO search = new HisMaterialMaterialSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMaterialMaterialExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisMaterialMaterialExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMaterialMaterialExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMaterialMaterialExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMaterialMaterialExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMaterialMaterialExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMaterialMaterialExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMaterialMaterialExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMaterialMaterialExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMaterialMaterialExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisMaterialMaterialExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisMaterialMaterialExpression.AddRange(listVHisMaterialMaterialExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMaterialMaterialExpression.Clear();
                search.listVHisMaterialMaterialExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
