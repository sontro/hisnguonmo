using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAppointmentPeriod
{
    public class HisAppointmentPeriodFilterQuery : HisAppointmentPeriodFilter
    {
        public HisAppointmentPeriodFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_APPOINTMENT_PERIOD, bool>>> listHisAppointmentPeriodExpression = new List<System.Linq.Expressions.Expression<Func<HIS_APPOINTMENT_PERIOD, bool>>>();

        

        internal HisAppointmentPeriodSO Query()
        {
            HisAppointmentPeriodSO search = new HisAppointmentPeriodSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisAppointmentPeriodExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisAppointmentPeriodExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisAppointmentPeriodExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisAppointmentPeriodExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisAppointmentPeriodExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisAppointmentPeriodExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisAppointmentPeriodExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisAppointmentPeriodExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisAppointmentPeriodExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisAppointmentPeriodExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.BRANCH_ID.HasValue)
                {
                    listHisAppointmentPeriodExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listHisAppointmentPeriodExpression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }

                search.listHisAppointmentPeriodExpression.AddRange(listHisAppointmentPeriodExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisAppointmentPeriodExpression.Clear();
                search.listHisAppointmentPeriodExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
