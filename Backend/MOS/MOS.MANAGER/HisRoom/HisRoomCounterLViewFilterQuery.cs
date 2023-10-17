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
    public class HisRoomCounterLViewFilterQuery : HisRoomCounterLViewFilter
    {
        public HisRoomCounterLViewFilterQuery()
            : base()
        {

        }


        internal List<System.Linq.Expressions.Expression<Func<L_HIS_ROOM_COUNTER, bool>>> listLHisRoomCounterExpression = new List<System.Linq.Expressions.Expression<Func<L_HIS_ROOM_COUNTER, bool>>>();

        internal HisRoomSO Query()
        {
            HisRoomSO search = new HisRoomSO();
            try
            {
                if (this.ID.HasValue)
                {
                    search.listLHisRoomCounterExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listLHisRoomCounterExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listLHisRoomCounterExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.BRANCH_ID.HasValue)
                {
                    search.listLHisRoomCounterExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.IS_EXAM.HasValue && this.IS_EXAM.Value)
                {
                    search.listLHisRoomCounterExpression.Add(o => o.IS_EXAM.HasValue && o.IS_EXAM == Constant.IS_TRUE);
                }
                if (this.IS_EXAM.HasValue && !this.IS_EXAM.Value)
                {
                    search.listLHisRoomCounterExpression.Add(o => !o.IS_EXAM.HasValue && o.IS_EXAM != Constant.IS_TRUE);
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    search.listLHisRoomCounterExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    search.listLHisRoomCounterExpression.Add(o => this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID));
                }
                if (this.AREA_ID.HasValue)
                {
                    //Ko khai bao khu vuc thi duoc hieu la dung chung cho tat ca cac khu vuc
                    search.listLHisRoomCounterExpression.Add(o => !o.AREA_ID.HasValue || o.AREA_ID == this.AREA_ID.Value);
                }
                if (this.IS_PAUSE_ENCLITIC.HasValue && this.IS_PAUSE_ENCLITIC.Value)
                {
                    search.listLHisRoomCounterExpression.Add(o => o.IS_PAUSE_ENCLITIC == Constant.IS_TRUE);
                }
                if (this.IS_PAUSE_ENCLITIC.HasValue && !this.IS_PAUSE_ENCLITIC.Value)
                {
                    search.listLHisRoomCounterExpression.Add(o => !o.IS_PAUSE_ENCLITIC.HasValue || o.IS_PAUSE_ENCLITIC != Constant.IS_TRUE);
                }
                if (this.ID__NOT_EQUAL.HasValue)
                {
                    search.listLHisRoomCounterExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    search.listLHisRoomCounterExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                

                search.listLHisRoomCounterExpression.AddRange(listLHisRoomCounterExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisRoomExpression.Clear();
                search.listVHisRoomExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
