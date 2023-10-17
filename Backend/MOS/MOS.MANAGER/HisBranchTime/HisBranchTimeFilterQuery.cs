using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBranchTime
{
    public class HisBranchTimeFilterQuery : HisBranchTimeFilter
    {
        public HisBranchTimeFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_BRANCH_TIME, bool>>> listHisBranchTimeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BRANCH_TIME, bool>>>();

        

        internal HisBranchTimeSO Query()
        {
            HisBranchTimeSO search = new HisBranchTimeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisBranchTimeExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisBranchTimeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisBranchTimeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisBranchTimeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisBranchTimeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisBranchTimeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisBranchTimeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisBranchTimeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisBranchTimeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisBranchTimeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisBranchTimeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.BRANCH_ID.HasValue)
                {
                    listHisBranchTimeExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listHisBranchTimeExpression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }

                search.listHisBranchTimeExpression.AddRange(listHisBranchTimeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisBranchTimeExpression.Clear();
                search.listHisBranchTimeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
