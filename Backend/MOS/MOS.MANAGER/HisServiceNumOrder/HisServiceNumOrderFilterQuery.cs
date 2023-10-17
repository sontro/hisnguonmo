using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceNumOrder
{
    public class HisServiceNumOrderFilterQuery : HisServiceNumOrderFilter
    {
        public HisServiceNumOrderFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_NUM_ORDER, bool>>> listHisServiceNumOrderExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_NUM_ORDER, bool>>>();

        

        internal HisServiceNumOrderSO Query()
        {
            HisServiceNumOrderSO search = new HisServiceNumOrderSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisServiceNumOrderExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisServiceNumOrderExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisServiceNumOrderExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisServiceNumOrderExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisServiceNumOrderExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisServiceNumOrderExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisServiceNumOrderExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisServiceNumOrderExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisServiceNumOrderExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisServiceNumOrderExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.SERVICE_ID.HasValue)
                {
                    listHisServiceNumOrderExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listHisServiceNumOrderExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    listHisServiceNumOrderExpression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (this.REQUEST_ROOM_IDs != null)
                {
                    listHisServiceNumOrderExpression.Add(o => this.REQUEST_ROOM_IDs.Contains(o.REQUEST_ROOM_ID));
                }

                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listHisServiceNumOrderExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }

                search.listHisServiceNumOrderExpression.AddRange(listHisServiceNumOrderExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisServiceNumOrderExpression.Clear();
                search.listHisServiceNumOrderExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
