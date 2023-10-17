using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccAppointment
{
    public class HisVaccAppointmentViewFilterQuery : HisVaccAppointmentViewFilter
    {
        public HisVaccAppointmentViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_VACC_APPOINTMENT, bool>>> listVHisVaccAppointmentExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_VACC_APPOINTMENT, bool>>>();

        

        internal HisVaccAppointmentSO Query()
        {
            HisVaccAppointmentSO search = new HisVaccAppointmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisVaccAppointmentExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisVaccAppointmentExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisVaccAppointmentExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisVaccAppointmentExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisVaccAppointmentExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisVaccAppointmentExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisVaccAppointmentExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisVaccAppointmentExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisVaccAppointmentExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisVaccAppointmentExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisVaccAppointmentExpression.Add(o =>
                        o.VACCINE_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.VACCINE_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (this.VACCINATION_EXAM_ID.HasValue)
                {
                    listVHisVaccAppointmentExpression.Add(o => o.VACCINATION_EXAM_ID == this.VACCINATION_EXAM_ID.Value);
                }
                if (this.VACCINE_TYPE_ID.HasValue)
                {
                    listVHisVaccAppointmentExpression.Add(o => o.VACCINE_TYPE_ID == this.VACCINE_TYPE_ID.Value);
                }

                search.listVHisVaccAppointmentExpression.AddRange(listVHisVaccAppointmentExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisVaccAppointmentExpression.Clear();
                search.listVHisVaccAppointmentExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
