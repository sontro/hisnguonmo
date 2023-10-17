using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebtGoods
{
    public class HisDebtGoodsFilterQuery : HisDebtGoodsFilter
    {
        public HisDebtGoodsFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_DEBT_GOODS, bool>>> listHisDebtGoodsExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEBT_GOODS, bool>>>();

        

        internal HisDebtGoodsSO Query()
        {
            HisDebtGoodsSO search = new HisDebtGoodsSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisDebtGoodsExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisDebtGoodsExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisDebtGoodsExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisDebtGoodsExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisDebtGoodsExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisDebtGoodsExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisDebtGoodsExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisDebtGoodsExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisDebtGoodsExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisDebtGoodsExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.DEBT_ID.HasValue)
                {
                    listHisDebtGoodsExpression.Add(o => o.DEBT_ID == this.DEBT_ID.Value);
                }
                if (this.DEBT_IDs != null)
                {
                    listHisDebtGoodsExpression.Add(o => this.DEBT_IDs.Contains(o.DEBT_ID));
                }

                search.listHisDebtGoodsExpression.AddRange(listHisDebtGoodsExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisDebtGoodsExpression.Clear();
                search.listHisDebtGoodsExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
