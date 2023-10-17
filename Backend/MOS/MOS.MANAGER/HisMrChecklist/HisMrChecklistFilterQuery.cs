using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrChecklist
{
    public class HisMrChecklistFilterQuery : HisMrChecklistFilter
    {
        public HisMrChecklistFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MR_CHECKLIST, bool>>> listHisMrChecklistExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MR_CHECKLIST, bool>>>();

        

        internal HisMrChecklistSO Query()
        {
            HisMrChecklistSO search = new HisMrChecklistSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMrChecklistExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisMrChecklistExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMrChecklistExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMrChecklistExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMrChecklistExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMrChecklistExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMrChecklistExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMrChecklistExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMrChecklistExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMrChecklistExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.MR_CHECK_ITEM_ID.HasValue)
                {
                    listHisMrChecklistExpression.Add(o => o.MR_CHECK_ITEM_ID == this.MR_CHECK_ITEM_ID.Value);
                }
                if (this.MR_CHECK_ITEM_IDs != null)
                {
                    listHisMrChecklistExpression.Add(o => this.MR_CHECK_ITEM_IDs.Contains(o.MR_CHECK_ITEM_ID));
                }
                if (this.MR_CHECK_SUMMARY_ID.HasValue)
                {
                    listHisMrChecklistExpression.Add(o => o.MR_CHECK_SUMMARY_ID == this.MR_CHECK_SUMMARY_ID.Value);
                }
                if (this.MR_CHECK_SUMMARY_IDs != null)
                {
                    listHisMrChecklistExpression.Add(o => this.MR_CHECK_SUMMARY_IDs.Contains(o.MR_CHECK_SUMMARY_ID));
                }
                
                search.listHisMrChecklistExpression.AddRange(listHisMrChecklistExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMrChecklistExpression.Clear();
                search.listHisMrChecklistExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
