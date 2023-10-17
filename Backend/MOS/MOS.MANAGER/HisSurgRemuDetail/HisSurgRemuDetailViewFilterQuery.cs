using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuDetail
{
	public class HisSurgRemuDetailViewFilterQuery : HisSurgRemuDetailViewFilter
	{
		public HisSurgRemuDetailViewFilterQuery()
			: base()
		{

		}

		internal List<System.Linq.Expressions.Expression<Func<V_HIS_SURG_REMU_DETAIL, bool>>> listVHisSurgRemuDetailExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SURG_REMU_DETAIL, bool>>>();

		

		internal HisSurgRemuDetailSO Query()
		{
			HisSurgRemuDetailSO search = new HisSurgRemuDetailSO();
			try
			{
				#region Abstract Base
				if (this.ID.HasValue)
				{
					listVHisSurgRemuDetailExpression.Add(o => o.ID == this.ID.Value);
				}
				if (this.IDs != null)
				{
					listVHisSurgRemuDetailExpression.Add(o => this.IDs.Contains(o.ID));
				}
				if (this.IS_ACTIVE.HasValue)
				{
					listVHisSurgRemuDetailExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
				}
				if (this.CREATE_TIME_FROM.HasValue)
				{
					listVHisSurgRemuDetailExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
				}
				if (this.CREATE_TIME_TO.HasValue)
				{
					listVHisSurgRemuDetailExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
				}
				if (this.MODIFY_TIME_FROM.HasValue)
				{
					listVHisSurgRemuDetailExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
				}
				if (this.MODIFY_TIME_TO.HasValue)
				{
					listVHisSurgRemuDetailExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
				}
				if (!String.IsNullOrEmpty(this.CREATOR))
				{
					listVHisSurgRemuDetailExpression.Add(o => o.CREATOR == this.CREATOR);
				}
				if (!String.IsNullOrEmpty(this.MODIFIER))
				{
					listVHisSurgRemuDetailExpression.Add(o => o.MODIFIER == this.MODIFIER);
				}
				if (!String.IsNullOrEmpty(this.GROUP_CODE))
				{
					listVHisSurgRemuDetailExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
				}
				if (this.IDs != null)
				{
					listVHisSurgRemuDetailExpression.Add(o => this.IDs.Contains(o.ID));
				}
				#endregion

				if (this.ID_NOT_EQUAL.HasValue)
				{
					listVHisSurgRemuDetailExpression.Add(o => o.ID != this.ID_NOT_EQUAL.Value);
				}
				if (this.EXECUTE_ROLE_ID.HasValue)
				{
					listVHisSurgRemuDetailExpression.Add(o => o.EXECUTE_ROLE_ID == this.EXECUTE_ROLE_ID.Value);
				}
				if (this.EXECUTE_ROLE_IDs != null)
				{
					listVHisSurgRemuDetailExpression.Add(o => this.EXECUTE_ROLE_IDs.Contains(o.EXECUTE_ROLE_ID));
				}
				if (this.SURG_REMUNERATION_ID.HasValue)
				{
					listVHisSurgRemuDetailExpression.Add(o => o.SURG_REMUNERATION_ID == this.SURG_REMUNERATION_ID.Value);
				}
				if (this.SURG_REMUNERATION_IDs != null)
				{
					listVHisSurgRemuDetailExpression.Add(o => this.SURG_REMUNERATION_IDs.Contains(o.SURG_REMUNERATION_ID));
				}

				if (!String.IsNullOrEmpty(this.KEY_WORD))
				{
					this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
					listVHisSurgRemuDetailExpression.Add(o =>
						o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
						o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
						o.SURG_REMUNERATION_CODE.ToLower().Contains(this.KEY_WORD) ||
						o.SURG_REMUNERATION_NAME.ToLower().Contains(this.KEY_WORD)
						);
				}

				search.listVHisSurgRemuDetailExpression.AddRange(listVHisSurgRemuDetailExpression);
				search.OrderField = ORDER_FIELD;
				search.OrderDirection = ORDER_DIRECTION;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				search.listVHisSurgRemuDetailExpression.Clear();
				search.listVHisSurgRemuDetailExpression.Add(o => o.ID == NEGATIVE_ID);
			}
			return search;
		}
	}
}
