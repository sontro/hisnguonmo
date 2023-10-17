using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAppointmentPeriod
{
    public class HisAppointmentPeriodViewFilterQuery : HisAppointmentPeriodViewFilter
    {
        public HisAppointmentPeriodViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_APPOINTMENT_PERIOD, bool>>> listVHisAppointmentPeriodExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_APPOINTMENT_PERIOD, bool>>>();

        

        internal HisAppointmentPeriodSO Query()
        {
            HisAppointmentPeriodSO search = new HisAppointmentPeriodSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAppointmentPeriodExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAppointmentPeriodExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAppointmentPeriodExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAppointmentPeriodExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAppointmentPeriodExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAppointmentPeriodExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAppointmentPeriodExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAppointmentPeriodExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAppointmentPeriodExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAppointmentPeriodExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.BRANCH_ID.HasValue)
                {
                    listVHisAppointmentPeriodExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listVHisAppointmentPeriodExpression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisAppointmentPeriodExpression.Add(o => 
                        o.BRANCH_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.BRANCH_CODE.ToLower().Contains(this.KEY_WORD));
                }

                search.listVHisAppointmentPeriodExpression.AddRange(listVHisAppointmentPeriodExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAppointmentPeriodExpression.Clear();
                search.listVHisAppointmentPeriodExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
