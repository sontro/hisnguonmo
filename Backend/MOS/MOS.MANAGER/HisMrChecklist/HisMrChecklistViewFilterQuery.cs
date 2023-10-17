using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrChecklist
{
    public class HisMrChecklistViewFilterQuery : HisMrChecklistViewFilter
    {
        public HisMrChecklistViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MR_CHECKLIST, bool>>> listVHisMrChecklistExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MR_CHECKLIST, bool>>>();

        

        internal HisMrChecklistSO Query()
        {
            HisMrChecklistSO search = new HisMrChecklistSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMrChecklistExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisMrChecklistExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMrChecklistExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMrChecklistExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMrChecklistExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMrChecklistExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMrChecklistExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMrChecklistExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMrChecklistExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMrChecklistExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisMrChecklistExpression.AddRange(listVHisMrChecklistExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMrChecklistExpression.Clear();
                search.listVHisMrChecklistExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
