using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomTime
{
    public class HisRoomTimeViewFilterQuery : HisRoomTimeViewFilter
    {
        public HisRoomTimeViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ROOM_TIME, bool>>> listVHisRoomTimeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ROOM_TIME, bool>>>();



        internal HisRoomTimeSO Query()
        {
            HisRoomTimeSO search = new HisRoomTimeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisRoomTimeExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisRoomTimeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisRoomTimeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisRoomTimeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisRoomTimeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisRoomTimeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisRoomTimeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisRoomTimeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisRoomTimeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisRoomTimeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisRoomTimeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ROOM_ID.HasValue)
                {
                    listVHisRoomTimeExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listVHisRoomTimeExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                if (this.ROOM_TYPE_ID.HasValue)
                {
                    listVHisRoomTimeExpression.Add(o => o.ROOM_TYPE_ID == this.ROOM_TYPE_ID.Value);
                }
                if (this.ROOM_TYPE_IDs != null)
                {
                    listVHisRoomTimeExpression.Add(o => this.ROOM_TYPE_IDs.Contains(o.ROOM_TYPE_ID));
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisRoomTimeExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.ROOM_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisRoomTimeExpression.AddRange(listVHisRoomTimeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisRoomTimeExpression.Clear();
                search.listVHisRoomTimeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
