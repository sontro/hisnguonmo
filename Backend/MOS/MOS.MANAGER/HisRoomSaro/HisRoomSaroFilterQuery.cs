using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomSaro
{
    public class HisRoomSaroFilterQuery : HisRoomSaroFilter
    {
        public HisRoomSaroFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_ROOM_SARO, bool>>> listHisRoomSaroExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ROOM_SARO, bool>>>();

        

        internal HisRoomSaroSO Query()
        {
            HisRoomSaroSO search = new HisRoomSaroSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisRoomSaroExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisRoomSaroExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisRoomSaroExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisRoomSaroExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisRoomSaroExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisRoomSaroExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisRoomSaroExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisRoomSaroExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisRoomSaroExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisRoomSaroExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisRoomSaroExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.SAMPLE_ROOM_ID.HasValue)
                {
                    listHisRoomSaroExpression.Add(o => o.SAMPLE_ROOM_ID == this.SAMPLE_ROOM_ID.Value);
                }
                if (this.SAMPLE_ROOM_IDs != null)
                {
                    listHisRoomSaroExpression.Add(o => this.SAMPLE_ROOM_IDs.Contains(o.SAMPLE_ROOM_ID));
                }
                if (this.ROOM_ID.HasValue)
                {
                    listHisRoomSaroExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listHisRoomSaroExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }

                search.listHisRoomSaroExpression.AddRange(listHisRoomSaroExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisRoomSaroExpression.Clear();
                search.listHisRoomSaroExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
