using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMatyReqDt
{
    public class HisBcsMatyReqDtViewFilterQuery : HisBcsMatyReqDtViewFilter
    {
        public HisBcsMatyReqDtViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BCS_MATY_REQ_DT, bool>>> listVHisBcsMatyReqDtExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BCS_MATY_REQ_DT, bool>>>();

        

        internal HisBcsMatyReqDtSO Query()
        {
            HisBcsMatyReqDtSO search = new HisBcsMatyReqDtSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBcsMatyReqDtExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisBcsMatyReqDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBcsMatyReqDtExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBcsMatyReqDtExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBcsMatyReqDtExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBcsMatyReqDtExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBcsMatyReqDtExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBcsMatyReqDtExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBcsMatyReqDtExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBcsMatyReqDtExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisBcsMatyReqDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisBcsMatyReqDtExpression.AddRange(listVHisBcsMatyReqDtExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBcsMatyReqDtExpression.Clear();
                search.listVHisBcsMatyReqDtExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
