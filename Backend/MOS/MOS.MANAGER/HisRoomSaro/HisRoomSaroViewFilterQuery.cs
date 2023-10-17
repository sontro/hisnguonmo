using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomSaro
{
    public class HisRoomSaroViewFilterQuery : HisRoomSaroViewFilter
    {
        public HisRoomSaroViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ROOM_SARO, bool>>> listVHisRoomSaroExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ROOM_SARO, bool>>>();

        

        internal HisRoomSaroSO Query()
        {
            HisRoomSaroSO search = new HisRoomSaroSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisRoomSaroExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisRoomSaroExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisRoomSaroExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisRoomSaroExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisRoomSaroExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisRoomSaroExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisRoomSaroExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisRoomSaroExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisRoomSaroExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisRoomSaroExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisRoomSaroExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.SAMPLE_ROOM_ID.HasValue)
                {
                    listVHisRoomSaroExpression.Add(o => o.SAMPLE_ROOM_ID == this.SAMPLE_ROOM_ID.Value);
                }
                if (this.SAMPLE_ROOM_IDs != null)
                {
                    listVHisRoomSaroExpression.Add(o => this.SAMPLE_ROOM_IDs.Contains(o.SAMPLE_ROOM_ID));
                }
                if (this.ROOM_ID.HasValue)
                {
                    listVHisRoomSaroExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listVHisRoomSaroExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisRoomSaroExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.ROOM_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.SAMPLE_ROOM_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.SAMPLE_ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisRoomSaroExpression.AddRange(listVHisRoomSaroExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisRoomSaroExpression.Clear();
                search.listVHisRoomSaroExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
