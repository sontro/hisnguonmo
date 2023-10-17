using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamSchedule
{
    public class HisExamScheduleViewFilterQuery : HisExamScheduleViewFilter
    {
        public HisExamScheduleViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXAM_SCHEDULE, bool>>> listVHisExamScheduleExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXAM_SCHEDULE, bool>>>();

        

        internal HisExamScheduleSO Query()
        {
            HisExamScheduleSO search = new HisExamScheduleSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisExamScheduleExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisExamScheduleExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisExamScheduleExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisExamScheduleExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisExamScheduleExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisExamScheduleExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisExamScheduleExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisExamScheduleExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisExamScheduleExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisExamScheduleExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.ROOM_ID.HasValue)
                {
                    listVHisExamScheduleExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listVHisExamScheduleExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                if (this.DAY_OF_WEEK__EQUAL.HasValue)
                {
                    listVHisExamScheduleExpression.Add(o => o.DAY_OF_WEEK == this.DAY_OF_WEEK__EQUAL.Value);
                }
                if (!String.IsNullOrWhiteSpace(KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisExamScheduleExpression.Add(o => o.LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.USERNAME.ToLower().Contains(this.KEY_WORD));
                }

                search.listVHisExamScheduleExpression.AddRange(listVHisExamScheduleExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExamScheduleExpression.Clear();
                search.listVHisExamScheduleExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
