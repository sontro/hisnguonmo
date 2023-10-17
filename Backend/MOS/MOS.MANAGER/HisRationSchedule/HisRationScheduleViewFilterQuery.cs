using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSchedule
{
    public class HisRationScheduleViewFilterQuery : HisRationScheduleViewFilter
    {
        public HisRationScheduleViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_RATION_SCHEDULE, bool>>> listVHisRationScheduleExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_RATION_SCHEDULE, bool>>>();

        

        internal HisRationScheduleSO Query()
        {
            HisRationScheduleSO search = new HisRationScheduleSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisRationScheduleExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisRationScheduleExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisRationScheduleExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisRationScheduleExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisRationScheduleExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisRationScheduleExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisRationScheduleExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisRationScheduleExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisRationScheduleExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisRationScheduleExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.TREATMENT_ID__EXACT != null)
                {
                    listVHisRationScheduleExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID__EXACT);
                }
                if (this.LAST_DEPARTMENT_ID != null)
                {
                    listVHisRationScheduleExpression.Add(o => o.LAST_DEPARTMENT_ID == this.LAST_DEPARTMENT_ID);
                }
                if (this.FROM_DATE_FROM.HasValue)
                {
                    listVHisRationScheduleExpression.Add(o => o.VIR_FROM_DATE >= this.FROM_DATE_FROM.Value);
                }
                if (this.FROM_DATE_TO.HasValue)
                {
                    listVHisRationScheduleExpression.Add(o => o.VIR_FROM_DATE <= this.FROM_DATE_TO.Value);
                }
                if (this.TO_DATE_FROM.HasValue)
                {
                    listVHisRationScheduleExpression.Add(o => o.VIR_TO_DATE == null || o.VIR_TO_DATE >= this.TO_DATE_FROM.Value);
                }
                if (this.TO_DATE_TO.HasValue)
                {
                    listVHisRationScheduleExpression.Add(o => o.VIR_TO_DATE <= this.TO_DATE_TO.Value);
                }
                if (this.LAST_ASSIGN_DATE_FROM.HasValue)
                {
                    listVHisRationScheduleExpression.Add(o => o.LAST_ASSIGN_DATE >= this.LAST_ASSIGN_DATE_FROM.Value);
                }
                if (this.LAST_ASSIGN_DATE_TO.HasValue)
                {
                    listVHisRationScheduleExpression.Add(o => o.LAST_ASSIGN_DATE <= this.LAST_ASSIGN_DATE_TO.Value);
                }
                if (this.IS_PAUSE.HasValue)
                {
                    if (this.IS_PAUSE.Value)
                    {
                        listVHisRationScheduleExpression.Add(o => o.IS_PAUSE == 1);
                    }
                    else
                    {
                        listVHisRationScheduleExpression.Add(o =>o.IS_PAUSE == null || o.IS_PAUSE != 1);
                    }
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisRationScheduleExpression.Add(o =>
                        o.LAST_DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.LAST_DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.RATION_TIME_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.RATION_TIME_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REFECTORY_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.REFECTORY_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.NOTE.ToLower().Contains(this.KEY_WORD));
                }
                search.listVHisRationScheduleExpression.AddRange(listVHisRationScheduleExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisRationScheduleExpression.Clear();
                search.listVHisRationScheduleExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
