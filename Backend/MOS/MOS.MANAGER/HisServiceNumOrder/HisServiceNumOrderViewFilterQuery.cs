using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceNumOrder
{
    public class HisServiceNumOrderViewFilterQuery : HisServiceNumOrderViewFilter
    {
        public HisServiceNumOrderViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_NUM_ORDER, bool>>> listVHisServiceNumOrderExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_NUM_ORDER, bool>>>();



        internal HisServiceNumOrderSO Query()
        {
            HisServiceNumOrderSO search = new HisServiceNumOrderSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisServiceNumOrderExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisServiceNumOrderExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisServiceNumOrderExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisServiceNumOrderExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisServiceNumOrderExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisServiceNumOrderExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisServiceNumOrderExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisServiceNumOrderExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisServiceNumOrderExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisServiceNumOrderExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion


                if (this.SERVICE_ID.HasValue)
                {
                    listVHisServiceNumOrderExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listVHisServiceNumOrderExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    listVHisServiceNumOrderExpression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (this.REQUEST_ROOM_IDs != null)
                {
                    listVHisServiceNumOrderExpression.Add(o => this.REQUEST_ROOM_IDs.Contains(o.REQUEST_ROOM_ID));
                }

                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listVHisServiceNumOrderExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisServiceNumOrderExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisServiceNumOrderExpression.AddRange(listVHisServiceNumOrderExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisServiceNumOrderExpression.Clear();
                search.listVHisServiceNumOrderExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
