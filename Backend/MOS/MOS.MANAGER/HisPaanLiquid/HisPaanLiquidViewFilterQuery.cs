using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPaanLiquid
{
    public class HisPaanLiquidViewFilterQuery : HisPaanLiquidViewFilter
    {
        public HisPaanLiquidViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_PAAN_LIQUID, bool>>> listVHisPaanLiquidExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PAAN_LIQUID, bool>>>();

        

        internal HisPaanLiquidSO Query()
        {
            HisPaanLiquidSO search = new HisPaanLiquidSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisPaanLiquidExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisPaanLiquidExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisPaanLiquidExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisPaanLiquidExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisPaanLiquidExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisPaanLiquidExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisPaanLiquidExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisPaanLiquidExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisPaanLiquidExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisPaanLiquidExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisPaanLiquidExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisPaanLiquidExpression.AddRange(listVHisPaanLiquidExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisPaanLiquidExpression.Clear();
                search.listVHisPaanLiquidExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
