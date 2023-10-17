using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAppointmentServ
{
    public class HisAppointmentServViewFilterQuery : HisAppointmentServViewFilter
    {
        public HisAppointmentServViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_APPOINTMENT_SERV, bool>>> listVHisAppointmentServExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_APPOINTMENT_SERV, bool>>>();

        

        internal HisAppointmentServSO Query()
        {
            HisAppointmentServSO search = new HisAppointmentServSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAppointmentServExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAppointmentServExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAppointmentServExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAppointmentServExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAppointmentServExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAppointmentServExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAppointmentServExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAppointmentServExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAppointmentServExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAppointmentServExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisAppointmentServExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisAppointmentServExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisAppointmentServExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.SERVICE_ID.HasValue)
                {
                    listVHisAppointmentServExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listVHisAppointmentServExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.SERVICE_TYPE_ID.HasValue)
                {
                    listVHisAppointmentServExpression.Add(o => o.SERVICE_TYPE_ID == this.SERVICE_TYPE_ID.Value);
                }

                search.listVHisAppointmentServExpression.AddRange(listVHisAppointmentServExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAppointmentServExpression.Clear();
                search.listVHisAppointmentServExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
