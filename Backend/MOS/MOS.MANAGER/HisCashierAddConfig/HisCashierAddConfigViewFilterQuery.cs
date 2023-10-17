using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashierAddConfig
{
    public class HisCashierAddConfigViewFilterQuery : HisCashierAddConfigViewFilter
    {
        public HisCashierAddConfigViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_CASHIER_ADD_CONFIG, bool>>> listVHisCashierAddConfigExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_CASHIER_ADD_CONFIG, bool>>>();



        internal HisCashierAddConfigSO Query()
        {
            HisCashierAddConfigSO search = new HisCashierAddConfigSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisCashierAddConfigExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisCashierAddConfigExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisCashierAddConfigExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisCashierAddConfigExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisCashierAddConfigExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisCashierAddConfigExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisCashierAddConfigExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisCashierAddConfigExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisCashierAddConfigExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisCashierAddConfigExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion


                if (this.CASHIER_ROOM_ID.HasValue)
                {
                    listVHisCashierAddConfigExpression.Add(o => o.CASHIER_ROOM_ID == this.CASHIER_ROOM_ID.Value);
                }
                if (this.CASHIER_ROOM_IDs != null)
                {
                    listVHisCashierAddConfigExpression.Add(o => this.CASHIER_ROOM_IDs.Contains(o.CASHIER_ROOM_ID));
                }
                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    listVHisCashierAddConfigExpression.Add(o => o.EXECUTE_ROOM_ID.HasValue && o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listVHisCashierAddConfigExpression.Add(o => o.EXECUTE_ROOM_ID.HasValue && this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID.Value));
                }
                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    listVHisCashierAddConfigExpression.Add(o => o.REQUEST_ROOM_ID.HasValue && o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (this.REQUEST_ROOM_IDs != null)
                {
                    listVHisCashierAddConfigExpression.Add(o => o.REQUEST_ROOM_ID.HasValue && this.REQUEST_ROOM_IDs.Contains(o.REQUEST_ROOM_ID.Value));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisCashierAddConfigExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.CASHIER_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.CASHIER_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                       );
                }


                search.listVHisCashierAddConfigExpression.AddRange(listVHisCashierAddConfigExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisCashierAddConfigExpression.Clear();
                search.listVHisCashierAddConfigExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
