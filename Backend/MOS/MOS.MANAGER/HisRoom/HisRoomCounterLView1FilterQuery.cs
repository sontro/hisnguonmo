using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoom
{
    public class HisRoomCounterLView1FilterQuery : HisRoomCounterLViewFilter
    {
        public HisRoomCounterLView1FilterQuery()
            : base()
        {

        }


        internal List<System.Linq.Expressions.Expression<Func<L_HIS_ROOM_COUNTER_1, bool>>> listLHisRoomCounter1Expression = new List<System.Linq.Expressions.Expression<Func<L_HIS_ROOM_COUNTER_1, bool>>>();

        internal HisRoomSO Query()
        {
            HisRoomSO search = new HisRoomSO();
            try
            {
                if (this.ID.HasValue)
                {
                    search.listLHisRoomCounter1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listLHisRoomCounter1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listLHisRoomCounter1Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.BRANCH_ID.HasValue)
                {
                    search.listLHisRoomCounter1Expression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.IS_EXAM.HasValue && this.IS_EXAM.Value)
                {
                    search.listLHisRoomCounter1Expression.Add(o => o.IS_EXAM.HasValue && o.IS_EXAM == Constant.IS_TRUE);
                }
                if (this.IS_EXAM.HasValue && !this.IS_EXAM.Value)
                {
                    search.listLHisRoomCounter1Expression.Add(o => !o.IS_EXAM.HasValue && o.IS_EXAM != Constant.IS_TRUE);
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    search.listLHisRoomCounter1Expression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    search.listLHisRoomCounter1Expression.Add(o => this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID));
                }

                search.listLHisRoomCounter1Expression.AddRange(listLHisRoomCounter1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listLHisRoomCounter1Expression.Clear();
                search.listLHisRoomCounter1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
