using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialTypeMap
{
    public class HisMaterialTypeMapViewFilterQuery : HisMaterialTypeMapViewFilter
    {
        public HisMaterialTypeMapViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MATERIAL_TYPE_MAP, bool>>> listVHisMaterialTypeMapExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MATERIAL_TYPE_MAP, bool>>>();

        

        internal HisMaterialTypeMapSO Query()
        {
            HisMaterialTypeMapSO search = new HisMaterialTypeMapSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMaterialTypeMapExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisMaterialTypeMapExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMaterialTypeMapExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMaterialTypeMapExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMaterialTypeMapExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMaterialTypeMapExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMaterialTypeMapExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMaterialTypeMapExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMaterialTypeMapExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMaterialTypeMapExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisMaterialTypeMapExpression.AddRange(listVHisMaterialTypeMapExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMaterialTypeMapExpression.Clear();
                search.listVHisMaterialTypeMapExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
