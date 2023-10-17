using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamSchedule
{
    public class HisExamScheduleFilterQuery : HisExamScheduleFilter
    {
        public HisExamScheduleFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EXAM_SCHEDULE, bool>>> listHisExamScheduleExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EXAM_SCHEDULE, bool>>>();

        

        internal HisExamScheduleSO Query()
        {
            HisExamScheduleSO search = new HisExamScheduleSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisExamScheduleExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisExamScheduleExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisExamScheduleExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisExamScheduleExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisExamScheduleExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisExamScheduleExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisExamScheduleExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisExamScheduleExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisExamScheduleExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisExamScheduleExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.ROOM_ID.HasValue)
                {
                    listHisExamScheduleExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listHisExamScheduleExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                if (this.DAY_OF_WEEK__EQUAL.HasValue)
                {
                    listHisExamScheduleExpression.Add(o => o.DAY_OF_WEEK == this.DAY_OF_WEEK__EQUAL.Value);
                }

                if (!String.IsNullOrWhiteSpace(KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisExamScheduleExpression.Add(o => o.LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.USERNAME.ToLower().Contains(this.KEY_WORD));
                }

                search.listHisExamScheduleExpression.AddRange(listHisExamScheduleExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisExamScheduleExpression.Clear();
                search.listHisExamScheduleExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
