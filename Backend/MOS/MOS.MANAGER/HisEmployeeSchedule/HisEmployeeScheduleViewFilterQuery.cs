using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmployeeSchedule
{
    public class HisEmployeeScheduleViewFilterQuery : HisEmployeeScheduleViewFilter
    {
        public HisEmployeeScheduleViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EMPLOYEE_SCHEDULE, bool>>> listVHisEmployeeScheduleExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EMPLOYEE_SCHEDULE, bool>>>();

        

        internal HisEmployeeScheduleSO Query()
        {
            HisEmployeeScheduleSO search = new HisEmployeeScheduleSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisEmployeeScheduleExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisEmployeeScheduleExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisEmployeeScheduleExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisEmployeeScheduleExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisEmployeeScheduleExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisEmployeeScheduleExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisEmployeeScheduleExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisEmployeeScheduleExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisEmployeeScheduleExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisEmployeeScheduleExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisEmployeeScheduleExpression.AddRange(listVHisEmployeeScheduleExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisEmployeeScheduleExpression.Clear();
                search.listVHisEmployeeScheduleExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
