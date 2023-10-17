using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccAppointment
{
    public class HisVaccAppointmentFilterQuery : HisVaccAppointmentFilter
    {
        public HisVaccAppointmentFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_VACC_APPOINTMENT, bool>>> listHisVaccAppointmentExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACC_APPOINTMENT, bool>>>();



        internal HisVaccAppointmentSO Query()
        {
            HisVaccAppointmentSO search = new HisVaccAppointmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisVaccAppointmentExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisVaccAppointmentExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisVaccAppointmentExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisVaccAppointmentExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisVaccAppointmentExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisVaccAppointmentExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisVaccAppointmentExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisVaccAppointmentExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisVaccAppointmentExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisVaccAppointmentExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.VACCINE_TYPE_ID.HasValue)
                {
                    listHisVaccAppointmentExpression.Add(o => o.VACCINE_TYPE_ID == this.VACCINE_TYPE_ID.Value);
                }
                if (this.VACCINE_TYPE_IDs != null)
                {
                    listHisVaccAppointmentExpression.Add(o => this.VACCINE_TYPE_IDs.Contains(o.VACCINE_TYPE_ID));
                }

                if (this.VACCINATION_EXAM_ID.HasValue)
                {
                    listHisVaccAppointmentExpression.Add(o => o.VACCINATION_EXAM_ID == this.VACCINATION_EXAM_ID.Value);
                }
                if (this.VACCINATION_EXAM_IDs != null)
                {
                    listHisVaccAppointmentExpression.Add(o => this.VACCINATION_EXAM_IDs.Contains(o.VACCINATION_EXAM_ID));
                }

                search.listHisVaccAppointmentExpression.AddRange(listHisVaccAppointmentExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisVaccAppointmentExpression.Clear();
                search.listHisVaccAppointmentExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
