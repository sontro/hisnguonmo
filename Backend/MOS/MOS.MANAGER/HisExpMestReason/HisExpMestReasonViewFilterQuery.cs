using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestReason
{
    public class HisExpMestReasonViewFilterQuery : HisExpMestReasonViewFilter
    {
        public HisExpMestReasonViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_REASON, bool>>> listVHisExpMestReasonExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_REASON, bool>>>();

        

        internal HisExpMestReasonSO Query()
        {
            HisExpMestReasonSO search = new HisExpMestReasonSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisExpMestReasonExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisExpMestReasonExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisExpMestReasonExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisExpMestReasonExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisExpMestReasonExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisExpMestReasonExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisExpMestReasonExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisExpMestReasonExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisExpMestReasonExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisExpMestReasonExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisExpMestReasonExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisExpMestReasonExpression.AddRange(listVHisExpMestReasonExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExpMestReasonExpression.Clear();
                search.listVHisExpMestReasonExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
