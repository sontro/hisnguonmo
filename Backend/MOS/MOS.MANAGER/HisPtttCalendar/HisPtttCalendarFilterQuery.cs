using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttCalendar
{
	public class HisPtttCalendarFilterQuery : HisPtttCalendarFilter
	{
		public HisPtttCalendarFilterQuery()
			: base()
		{

		}

		internal List<System.Linq.Expressions.Expression<Func<HIS_PTTT_CALENDAR, bool>>> listHisPtttCalendarExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PTTT_CALENDAR, bool>>>();

		

		internal HisPtttCalendarSO Query()
		{
			HisPtttCalendarSO search = new HisPtttCalendarSO();
			try
			{
				#region Abstract Base
				if (this.ID.HasValue)
				{
					listHisPtttCalendarExpression.Add(o => o.ID == this.ID.Value);
				}
				if (this.IDs != null)
				{
					listHisPtttCalendarExpression.Add(o => this.IDs.Contains(o.ID));
				}
				if (this.IS_ACTIVE.HasValue)
				{
					listHisPtttCalendarExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
				}
				if (this.CREATE_TIME_FROM.HasValue)
				{
					listHisPtttCalendarExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
				}
				if (this.CREATE_TIME_TO.HasValue)
				{
					listHisPtttCalendarExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
				}
				if (this.MODIFY_TIME_FROM.HasValue)
				{
					listHisPtttCalendarExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
				}
				if (this.MODIFY_TIME_TO.HasValue)
				{
					listHisPtttCalendarExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
				}
				if (!String.IsNullOrEmpty(this.CREATOR))
				{
					listHisPtttCalendarExpression.Add(o => o.CREATOR == this.CREATOR);
				}
				if (!String.IsNullOrEmpty(this.MODIFIER))
				{
					listHisPtttCalendarExpression.Add(o => o.MODIFIER == this.MODIFIER);
				}
				if (!String.IsNullOrEmpty(this.GROUP_CODE))
				{
					listHisPtttCalendarExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
				}
				if (this.IDs != null)
				{
					listHisPtttCalendarExpression.Add(o => this.IDs.Contains(o.ID));
				}
				#endregion
				
				if (this.DEPARTMENT_ID.HasValue)
				{
					listHisPtttCalendarExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
				}
				if (this.DEPARTMENT_IDs != null)
				{
					listHisPtttCalendarExpression.Add(o => this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID));
				}
				if (this.TIME_FROM_FROM.HasValue)
				{
					listHisPtttCalendarExpression.Add(o => o.TIME_FROM >= this.TIME_FROM_FROM.Value);
				}
				if (this.TIME_FROM_TO.HasValue)
				{
					listHisPtttCalendarExpression.Add(o => o.TIME_FROM <= this.TIME_FROM_TO.Value);
				}
				if (this.TIME_TO_FROM.HasValue)
				{
					listHisPtttCalendarExpression.Add(o => o.TIME_TO >= this.TIME_TO_FROM.Value);
				}
				if (this.TIME_TO_TO.HasValue)
				{
					listHisPtttCalendarExpression.Add(o => o.TIME_TO <= this.TIME_TO_TO.Value);
				}
                if (this.DATE_FROM__FROM.HasValue)
                {
                    listHisPtttCalendarExpression.Add(o => o.VIR_DATE_FROM >= this.DATE_FROM__FROM.Value);
                }
                if (this.DATE_TO__TO.HasValue)
                {
                    listHisPtttCalendarExpression.Add(o => o.VIR_DATE_TO <= this.DATE_TO__TO.Value);
                }

				search.listHisPtttCalendarExpression.AddRange(listHisPtttCalendarExpression);
				search.OrderField = ORDER_FIELD;
				search.OrderDirection = ORDER_DIRECTION;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				search.listHisPtttCalendarExpression.Clear();
				search.listHisPtttCalendarExpression.Add(o => o.ID == NEGATIVE_ID);
			}
			return search;
		}
	}
}
