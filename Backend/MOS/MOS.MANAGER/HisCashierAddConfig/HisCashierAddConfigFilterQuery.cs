using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashierAddConfig
{
    public class HisCashierAddConfigFilterQuery : HisCashierAddConfigFilter
    {
        public HisCashierAddConfigFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_CASHIER_ADD_CONFIG, bool>>> listHisCashierAddConfigExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CASHIER_ADD_CONFIG, bool>>>();



        internal HisCashierAddConfigSO Query()
        {
            HisCashierAddConfigSO search = new HisCashierAddConfigSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisCashierAddConfigExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisCashierAddConfigExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisCashierAddConfigExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisCashierAddConfigExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisCashierAddConfigExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisCashierAddConfigExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisCashierAddConfigExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisCashierAddConfigExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisCashierAddConfigExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisCashierAddConfigExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.CASHIER_ROOM_ID.HasValue)
                {
                    listHisCashierAddConfigExpression.Add(o => o.CASHIER_ROOM_ID == this.CASHIER_ROOM_ID.Value);
                }
                if (this.CASHIER_ROOM_IDs != null)
                {
                    listHisCashierAddConfigExpression.Add(o => this.CASHIER_ROOM_IDs.Contains(o.CASHIER_ROOM_ID));
                }
                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    listHisCashierAddConfigExpression.Add(o => o.EXECUTE_ROOM_ID.HasValue && o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listHisCashierAddConfigExpression.Add(o => o.EXECUTE_ROOM_ID.HasValue && this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID.Value));
                }
                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    listHisCashierAddConfigExpression.Add(o => o.REQUEST_ROOM_ID.HasValue && o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (this.REQUEST_ROOM_IDs != null)
                {
                    listHisCashierAddConfigExpression.Add(o => o.REQUEST_ROOM_ID.HasValue && this.REQUEST_ROOM_IDs.Contains(o.REQUEST_ROOM_ID.Value));
                }

                search.listHisCashierAddConfigExpression.AddRange(listHisCashierAddConfigExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisCashierAddConfigExpression.Clear();
                search.listHisCashierAddConfigExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
