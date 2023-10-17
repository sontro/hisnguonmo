using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMatyReqReq
{
    public class HisBcsMatyReqReqViewFilterQuery : HisBcsMatyReqReqViewFilter
    {
        public HisBcsMatyReqReqViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BCS_MATY_REQ_REQ, bool>>> listVHisBcsMatyReqReqExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BCS_MATY_REQ_REQ, bool>>>();

        

        internal HisBcsMatyReqReqSO Query()
        {
            HisBcsMatyReqReqSO search = new HisBcsMatyReqReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBcsMatyReqReqExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisBcsMatyReqReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBcsMatyReqReqExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBcsMatyReqReqExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBcsMatyReqReqExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBcsMatyReqReqExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBcsMatyReqReqExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBcsMatyReqReqExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBcsMatyReqReqExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBcsMatyReqReqExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisBcsMatyReqReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisBcsMatyReqReqExpression.AddRange(listVHisBcsMatyReqReqExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBcsMatyReqReqExpression.Clear();
                search.listVHisBcsMatyReqReqExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
