using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientObservation
{
    public class HisPatientObservationFilterQuery : HisPatientObservationFilter
    {
        public HisPatientObservationFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_PATIENT_OBSERVATION, bool>>> listHisPatientObservationExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PATIENT_OBSERVATION, bool>>>();

        

        internal HisPatientObservationSO Query()
        {
            HisPatientObservationSO search = new HisPatientObservationSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPatientObservationExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisPatientObservationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPatientObservationExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPatientObservationExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPatientObservationExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPatientObservationExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPatientObservationExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPatientObservationExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPatientObservationExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPatientObservationExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.TREATMENT_BED_ROOM_ID.HasValue)
                {
                    listHisPatientObservationExpression.Add(o => o.TREATMENT_BED_ROOM_ID == this.TREATMENT_BED_ROOM_ID.Value);
                }
                if (this.OBSERVED_TIME_FROM.HasValue)
                {
                    listHisPatientObservationExpression.Add(o => o.OBSERVED_TIME_FROM == this.OBSERVED_TIME_FROM.Value);
                }
                if (this.OBSERVED_TIME_TO.HasValue)
                {
                    listHisPatientObservationExpression.Add(o => o.OBSERVED_TIME_TO == this.OBSERVED_TIME_TO.Value);
                }
                
                search.listHisPatientObservationExpression.AddRange(listHisPatientObservationExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPatientObservationExpression.Clear();
                search.listHisPatientObservationExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
