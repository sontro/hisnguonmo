using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockMaty
{
    public class HisMediStockMatyView1FilterQuery: HisMediStockMatyView1Filter
    {
        public HisMediStockMatyView1FilterQuery()
            : base()
        {

        }
        
        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_STOCK_MATY_1, bool>>> listVHisMediStockMaty1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_STOCK_MATY_1, bool>>>();

        

        internal HisMediStockMatySO Query()
        {
            HisMediStockMatySO search = new HisMediStockMatySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listVHisMediStockMaty1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisMediStockMaty1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listVHisMediStockMaty1Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listVHisMediStockMaty1Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listVHisMediStockMaty1Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listVHisMediStockMaty1Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listVHisMediStockMaty1Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listVHisMediStockMaty1Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listVHisMediStockMaty1Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listVHisMediStockMaty1Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.MEDI_STOCK_ID.HasValue)
                {
                    listVHisMediStockMaty1Expression.Add(o => o.MEDI_STOCK_ID == this.MEDI_STOCK_ID.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    search.listVHisMediStockMaty1Expression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }

                search.listVHisMediStockMaty1Expression.AddRange(listVHisMediStockMaty1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMediStockMaty1Expression.Clear();
                search.listVHisMediStockMaty1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
