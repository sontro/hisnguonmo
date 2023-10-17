using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockMety
{
    public class HisMediStockMetyView1FilterQuery: HisMediStockMetyViewFilter
    {
        public HisMediStockMetyView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_STOCK_METY_1, bool>>> listVHisMediStockMety1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_STOCK_METY_1, bool>>>();

        internal HisMediStockMetySO Query()
        {
            HisMediStockMetySO search = new HisMediStockMetySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listVHisMediStockMety1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisMediStockMety1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listVHisMediStockMety1Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listVHisMediStockMety1Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listVHisMediStockMety1Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listVHisMediStockMety1Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listVHisMediStockMety1Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listVHisMediStockMety1Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listVHisMediStockMety1Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listVHisMediStockMety1Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.MEDI_STOCK_ID.HasValue)
                {
                    search.listVHisMediStockMety1Expression.Add(o => o.MEDI_STOCK_ID == this.MEDI_STOCK_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    search.listVHisMediStockMety1Expression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }

                search.listVHisMediStockMety1Expression.AddRange(listVHisMediStockMety1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMediStockMety1Expression.Clear();
                search.listVHisMediStockMety1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
