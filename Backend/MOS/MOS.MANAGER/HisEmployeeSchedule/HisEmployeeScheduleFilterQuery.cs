using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using Inventec.Core;
namespace MOS.MANAGER.HisEmployeeSchedule
{
    public class HisEmployeeScheduleFilterQuery : HisEmployeeScheduleFilter
    {
        public HisEmployeeScheduleFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EMPLOYEE_SCHEDULE, bool>>> listHisEmployeeScheduleExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EMPLOYEE_SCHEDULE, bool>>>();

        

        internal HisEmployeeScheduleSO Query()
        {
            HisEmployeeScheduleSO search = new HisEmployeeScheduleSO();
            try
            {

                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisEmployeeScheduleExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisEmployeeScheduleExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisEmployeeScheduleExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisEmployeeScheduleExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisEmployeeScheduleExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisEmployeeScheduleExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisEmployeeScheduleExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisEmployeeScheduleExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisEmployeeScheduleExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisEmployeeScheduleExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.LOGINNAME__EXACT))
                {
                    listHisEmployeeScheduleExpression.Add(o => o.LOGINNAME == this.LOGINNAME__EXACT);
                }
                if (this.SCHEDULE_DATES != null && this.SCHEDULE_DATES.Count > 0)
                {
                    listHisEmployeeScheduleExpression.Add(o => o.SCHEDULE_DATE != null && this.SCHEDULE_DATES.Contains(o.SCHEDULE_DATE));
                }
                if (this.SCHEDULE_DATE.HasValue)
                {
                    listHisEmployeeScheduleExpression.Add(o => o.SCHEDULE_DATE == this.SCHEDULE_DATE.Value);
                }
                
                search.listHisEmployeeScheduleExpression.AddRange(listHisEmployeeScheduleExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisEmployeeScheduleExpression.Clear();
                search.listHisEmployeeScheduleExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
