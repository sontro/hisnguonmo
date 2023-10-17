using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDocHoldType
{
	public class HisDocHoldTypeFilterQuery : HisDocHoldTypeFilter
	{
		public HisDocHoldTypeFilterQuery()
			: base()
		{

		}

		internal List<System.Linq.Expressions.Expression<Func<HIS_DOC_HOLD_TYPE, bool>>> listHisDocHoldTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DOC_HOLD_TYPE, bool>>>();

		

		internal HisDocHoldTypeSO Query()
		{
			HisDocHoldTypeSO search = new HisDocHoldTypeSO();
			try
			{
				#region Abstract Base
				if (this.ID.HasValue)
				{
					listHisDocHoldTypeExpression.Add(o => o.ID == this.ID.Value);
				}
				if (this.IDs != null)
				{
					listHisDocHoldTypeExpression.Add(o => this.IDs.Contains(o.ID));
				}
				if (this.IS_ACTIVE.HasValue)
				{
					listHisDocHoldTypeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
				}
				if (this.CREATE_TIME_FROM.HasValue)
				{
					listHisDocHoldTypeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
				}
				if (this.CREATE_TIME_TO.HasValue)
				{
					listHisDocHoldTypeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
				}
				if (this.MODIFY_TIME_FROM.HasValue)
				{
					listHisDocHoldTypeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
				}
				if (this.MODIFY_TIME_TO.HasValue)
				{
					listHisDocHoldTypeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
				}
				if (!String.IsNullOrEmpty(this.CREATOR))
				{
					listHisDocHoldTypeExpression.Add(o => o.CREATOR == this.CREATOR);
				}
				if (!String.IsNullOrEmpty(this.MODIFIER))
				{
					listHisDocHoldTypeExpression.Add(o => o.MODIFIER == this.MODIFIER);
				}
				if (!String.IsNullOrEmpty(this.GROUP_CODE))
				{
					listHisDocHoldTypeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
				}
				if (this.IDs != null)
				{
					listHisDocHoldTypeExpression.Add(o => this.IDs.Contains(o.ID));
				}
				#endregion

				if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
				{
					this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
					listHisDocHoldTypeExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
						o.DOC_HOLD_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
						o.DOC_HOLD_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
						o.MODIFIER.ToLower().Contains(this.KEY_WORD)
						);
				}
                if (this.IS_HEIN_CARD.HasValue)
				{
                    listHisDocHoldTypeExpression.Add(o => o.IS_HEIN_CARD == this.IS_HEIN_CARD.Value);
				}

				search.listHisDocHoldTypeExpression.AddRange(listHisDocHoldTypeExpression);
				search.OrderField = ORDER_FIELD;
				search.OrderDirection = ORDER_DIRECTION;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				search.listHisDocHoldTypeExpression.Clear();
				search.listHisDocHoldTypeExpression.Add(o => o.ID == NEGATIVE_ID);
			}
			return search;
		}
	}
}
