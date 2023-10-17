using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaexVaer
{
	public class HisVaexVaerFilterQuery : HisVaexVaerFilter
	{
		public HisVaexVaerFilterQuery()
			: base()
		{

		}

		internal List<System.Linq.Expressions.Expression<Func<HIS_VAEX_VAER, bool>>> listHisVaexVaerExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VAEX_VAER, bool>>>();

		

		internal HisVaexVaerSO Query()
		{
			HisVaexVaerSO search = new HisVaexVaerSO();
			try
			{
				#region Abstract Base
				if (this.ID.HasValue)
				{
					listHisVaexVaerExpression.Add(o => o.ID == this.ID.Value);
				}
				if (this.IDs != null)
				{
					listHisVaexVaerExpression.Add(o => this.IDs.Contains(o.ID));
				}
				if (this.IS_ACTIVE.HasValue)
				{
					listHisVaexVaerExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
				}
				if (this.CREATE_TIME_FROM.HasValue)
				{
					listHisVaexVaerExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
				}
				if (this.CREATE_TIME_TO.HasValue)
				{
					listHisVaexVaerExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
				}
				if (this.MODIFY_TIME_FROM.HasValue)
				{
					listHisVaexVaerExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
				}
				if (this.MODIFY_TIME_TO.HasValue)
				{
					listHisVaexVaerExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
				}
				if (!String.IsNullOrEmpty(this.CREATOR))
				{
					listHisVaexVaerExpression.Add(o => o.CREATOR == this.CREATOR);
				}
				if (!String.IsNullOrEmpty(this.MODIFIER))
				{
					listHisVaexVaerExpression.Add(o => o.MODIFIER == this.MODIFIER);
				}
				if (!String.IsNullOrEmpty(this.GROUP_CODE))
				{
					listHisVaexVaerExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
				}
				if (this.IDs != null)
				{
					listHisVaexVaerExpression.Add(o => this.IDs.Contains(o.ID));
				}
				#endregion

				if (this.VACCINATION_EXAM_ID.HasValue)
				{
					listHisVaexVaerExpression.Add(o => o.VACCINATION_EXAM_ID == this.VACCINATION_EXAM_ID.Value);
				}
				if (this.VACCINATION_EXAM_IDs != null)
				{
					listHisVaexVaerExpression.Add(o => this.VACCINATION_EXAM_IDs.Contains(o.VACCINATION_EXAM_ID));
				}
				if (this.VACC_EXAM_RESULT_ID.HasValue)
				{
					listHisVaexVaerExpression.Add(o => o.VACC_EXAM_RESULT_ID == this.VACC_EXAM_RESULT_ID.Value);
				}
				if (this.VACC_EXAM_RESULT_IDs != null)
				{
					listHisVaexVaerExpression.Add(o => this.VACC_EXAM_RESULT_IDs.Contains(o.VACC_EXAM_RESULT_ID));
				}

				search.listHisVaexVaerExpression.AddRange(listHisVaexVaerExpression);
				search.OrderField = ORDER_FIELD;
				search.OrderDirection = ORDER_DIRECTION;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				search.listHisVaexVaerExpression.Clear();
				search.listHisVaexVaerExpression.Add(o => o.ID == NEGATIVE_ID);
			}
			return search;
		}
	}
}
