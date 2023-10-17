using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCaroAccountBook
{
    public class HisCaroAccountBookFilterQuery : HisCaroAccountBookFilter
    {
        public HisCaroAccountBookFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_CARO_ACCOUNT_BOOK, bool>>> listHisCaroAccountBookExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CARO_ACCOUNT_BOOK, bool>>>();

        

        internal HisCaroAccountBookSO Query()
        {
            HisCaroAccountBookSO search = new HisCaroAccountBookSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisCaroAccountBookExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisCaroAccountBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisCaroAccountBookExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisCaroAccountBookExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisCaroAccountBookExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisCaroAccountBookExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisCaroAccountBookExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisCaroAccountBookExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisCaroAccountBookExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisCaroAccountBookExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisCaroAccountBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ACCOUNT_BOOK_ID.HasValue)
                {
                    listHisCaroAccountBookExpression.Add(o => o.ACCOUNT_BOOK_ID == this.ACCOUNT_BOOK_ID.Value);
                }
                if (this.ACCOUNT_BOOK_IDs != null)
                {
                    listHisCaroAccountBookExpression.Add(o => this.ACCOUNT_BOOK_IDs.Contains(o.ACCOUNT_BOOK_ID));
                }
                if (this.CASHIER_ROOM_ID.HasValue)
                {
                    listHisCaroAccountBookExpression.Add(o => o.CASHIER_ROOM_ID == this.CASHIER_ROOM_ID.Value);
                }
                if (this.CASHIER_ROOM_IDs != null)
                {
                    listHisCaroAccountBookExpression.Add(o => this.CASHIER_ROOM_IDs.Contains(o.CASHIER_ROOM_ID));
                }

                search.listHisCaroAccountBookExpression.AddRange(listHisCaroAccountBookExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisCaroAccountBookExpression.Clear();
                search.listHisCaroAccountBookExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
