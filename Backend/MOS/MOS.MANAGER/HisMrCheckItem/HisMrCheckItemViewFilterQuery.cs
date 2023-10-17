using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckItem
{
    public class HisMrCheckItemViewFilterQuery : HisMrCheckItemViewFilter
    {
        public HisMrCheckItemViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MR_CHECK_ITEM, bool>>> listVHisMrCheckItemExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MR_CHECK_ITEM, bool>>>();

        

        internal HisMrCheckItemSO Query()
        {
            HisMrCheckItemSO search = new HisMrCheckItemSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMrCheckItemExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisMrCheckItemExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMrCheckItemExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMrCheckItemExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMrCheckItemExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMrCheckItemExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMrCheckItemExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMrCheckItemExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMrCheckItemExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMrCheckItemExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisMrCheckItemExpression.AddRange(listVHisMrCheckItemExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMrCheckItemExpression.Clear();
                search.listVHisMrCheckItemExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
