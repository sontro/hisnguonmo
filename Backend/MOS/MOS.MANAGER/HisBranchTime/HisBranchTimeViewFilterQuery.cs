using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBranchTime
{
    public class HisBranchTimeViewFilterQuery : HisBranchTimeViewFilter
    {
        public HisBranchTimeViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BRANCH_TIME, bool>>> listVHisBranchTimeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BRANCH_TIME, bool>>>();



        internal HisBranchTimeSO Query()
        {
            HisBranchTimeSO search = new HisBranchTimeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBranchTimeExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisBranchTimeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBranchTimeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBranchTimeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBranchTimeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBranchTimeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBranchTimeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBranchTimeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBranchTimeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBranchTimeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisBranchTimeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.BRANCH_ID.HasValue)
                {
                    listVHisBranchTimeExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listVHisBranchTimeExpression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisBranchTimeExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.BRANCH_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.BRANCH_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.FROM_TIME.ToLower().Contains(this.KEY_WORD) ||
                        o.GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.TO_TIME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisBranchTimeExpression.AddRange(listVHisBranchTimeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBranchTimeExpression.Clear();
                search.listVHisBranchTimeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
