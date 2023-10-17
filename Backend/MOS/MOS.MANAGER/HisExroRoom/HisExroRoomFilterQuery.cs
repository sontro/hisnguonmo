using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExroRoom
{
    public class HisExroRoomFilterQuery : HisExroRoomFilter
    {
        public HisExroRoomFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EXRO_ROOM, bool>>> listHisExroRoomExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EXRO_ROOM, bool>>>();



        internal HisExroRoomSO Query()
        {
            HisExroRoomSO search = new HisExroRoomSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisExroRoomExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisExroRoomExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisExroRoomExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisExroRoomExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisExroRoomExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisExroRoomExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisExroRoomExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisExroRoomExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisExroRoomExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisExroRoomExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisExroRoomExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    listHisExroRoomExpression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listHisExroRoomExpression.Add(o => this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID));
                }
                if (this.ROOM_ID.HasValue)
                {
                    listHisExroRoomExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listHisExroRoomExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                if (this.IS_ALLOW_REQUEST.HasValue)
                {
                    if (this.IS_ALLOW_REQUEST.Value)
                    {
                        listHisExroRoomExpression.Add(o => o.IS_ALLOW_REQUEST.HasValue && o.IS_ALLOW_REQUEST.Value == UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisExroRoomExpression.Add(o => !o.IS_ALLOW_REQUEST.HasValue || o.IS_ALLOW_REQUEST.Value != UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.IS_HOLD_ORDER.HasValue)
                {
                    if (this.IS_HOLD_ORDER.Value)
                    {
                        listHisExroRoomExpression.Add(o => o.IS_HOLD_ORDER.HasValue && o.IS_HOLD_ORDER.Value == UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisExroRoomExpression.Add(o => !o.IS_HOLD_ORDER.HasValue || o.IS_HOLD_ORDER.Value != UTILITY.Constant.IS_TRUE);
                    }
                }

                search.listHisExroRoomExpression.AddRange(listHisExroRoomExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisExroRoomExpression.Clear();
                search.listHisExroRoomExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
