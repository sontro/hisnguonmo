using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHandover
{
    public class HisHoreHandoverViewFilterQuery : HisHoreHandoverViewFilter
    {
        public HisHoreHandoverViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_HORE_HANDOVER, bool>>> listVHisHoreHandoverExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_HORE_HANDOVER, bool>>>();

        

        internal HisHoreHandoverSO Query()
        {
            HisHoreHandoverSO search = new HisHoreHandoverSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisHoreHandoverExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisHoreHandoverExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisHoreHandoverExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisHoreHandoverExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisHoreHandoverExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisHoreHandoverExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisHoreHandoverExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisHoreHandoverExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisHoreHandoverExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisHoreHandoverExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisHoreHandoverExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.SEND_ROOM_ID.HasValue)
                {
                    listVHisHoreHandoverExpression.Add(o => o.SEND_ROOM_ID == this.SEND_ROOM_ID.Value);
                }
                if (this.SEND_ROOM_IDs != null)
                {
                    listVHisHoreHandoverExpression.Add(o => this.SEND_ROOM_IDs.Contains(o.SEND_ROOM_ID));
                }
                if (this.HORE_HANDOVER_STT_ID.HasValue)
                {
                    listVHisHoreHandoverExpression.Add(o => o.HORE_HANDOVER_STT_ID == this.HORE_HANDOVER_STT_ID.Value);
                }
                if (this.HORE_HANDOVER_STT_IDs != null)
                {
                    listVHisHoreHandoverExpression.Add(o => this.HORE_HANDOVER_STT_IDs.Contains(o.HORE_HANDOVER_STT_ID));
                }
                if (this.RECEIVE_ROOM_ID.HasValue)
                {
                    listVHisHoreHandoverExpression.Add(o => o.RECEIVE_ROOM_ID == this.RECEIVE_ROOM_ID.Value);
                }
                if (this.RECEIVE_ROOM_IDs != null)
                {
                    listVHisHoreHandoverExpression.Add(o => this.RECEIVE_ROOM_IDs.Contains(o.RECEIVE_ROOM_ID));
                }
                if (!String.IsNullOrEmpty(this.HORE_HANDOVER_CODE__EXACT))
                {
                    listVHisHoreHandoverExpression.Add(o => o.HORE_HANDOVER_CODE == this.HORE_HANDOVER_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.SEND_LOGINNAME__EXACT))
                {
                    listVHisHoreHandoverExpression.Add(o => o.SEND_LOGINNAME != null && o.SEND_LOGINNAME == this.SEND_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrEmpty(this.RECEIVE_LOGINNAME__EXACT))
                {
                    listVHisHoreHandoverExpression.Add(o => o.RECEIVE_LOGINNAME != null && o.RECEIVE_LOGINNAME == this.RECEIVE_LOGINNAME__EXACT);
                }
                if (this.SEND_OR_RECEIVE_ROOM_ID.HasValue)
                {
                    listVHisHoreHandoverExpression.Add(o => o.SEND_ROOM_ID == this.SEND_OR_RECEIVE_ROOM_ID.Value || o.RECEIVE_ROOM_ID == this.SEND_OR_RECEIVE_ROOM_ID.Value);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisHoreHandoverExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.HORE_HANDOVER_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.RECEIVE_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.RECEIVE_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SEND_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SEND_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.HORE_HANDOVER_STT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.RECEIVE_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.RECEIVE_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SEND_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SEND_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisHoreHandoverExpression.AddRange(listVHisHoreHandoverExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisHoreHandoverExpression.Clear();
                search.listVHisHoreHandoverExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
