using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBillGoods
{
    public class HisBillGoodsViewFilterQuery : HisBillGoodsViewFilter
    {
        public HisBillGoodsViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BILL_GOODS, bool>>> listVHisBillGoodsExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BILL_GOODS, bool>>>();

        

        internal HisBillGoodsSO Query()
        {
            HisBillGoodsSO search = new HisBillGoodsSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBillGoodsExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisBillGoodsExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBillGoodsExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBillGoodsExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBillGoodsExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBillGoodsExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBillGoodsExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBillGoodsExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBillGoodsExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBillGoodsExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisBillGoodsExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisBillGoodsExpression.AddRange(listVHisBillGoodsExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBillGoodsExpression.Clear();
                search.listVHisBillGoodsExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
