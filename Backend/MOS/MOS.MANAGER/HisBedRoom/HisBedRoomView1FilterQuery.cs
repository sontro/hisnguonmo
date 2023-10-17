using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedRoom
{
    public class HisBedRoomView1FilterQuery : HisBedRoomView1Filter
    {
        public HisBedRoomView1FilterQuery()
            : base()
        {

        }
        
        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BED_ROOM_1, bool>>> listVHisBedRoom1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BED_ROOM_1, bool>>>();

        internal HisBedRoomSO Query()
        {
            HisBedRoomSO search = new HisBedRoomSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listVHisBedRoom1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisBedRoom1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listVHisBedRoom1Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listVHisBedRoom1Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listVHisBedRoom1Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listVHisBedRoom1Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listVHisBedRoom1Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listVHisBedRoom1Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listVHisBedRoom1Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listVHisBedRoom1Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.DEPARTMENT_ID.HasValue)
                {
                    search.listVHisBedRoom1Expression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listVHisBedRoom1Expression.Add(o => this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID));
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisBedRoom1Expression.Add(o =>
                        o.BED_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.BED_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisBedRoom1Expression.AddRange(listVHisBedRoom1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBedRoom1Expression.Clear();
                search.listVHisBedRoom1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
