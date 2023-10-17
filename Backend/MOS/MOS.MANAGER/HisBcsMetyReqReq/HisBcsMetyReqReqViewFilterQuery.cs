using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMetyReqReq
{
    public class HisBcsMetyReqReqViewFilterQuery : HisBcsMetyReqReqViewFilter
    {
        public HisBcsMetyReqReqViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BCS_METY_REQ_REQ, bool>>> listVHisBcsMetyReqReqExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BCS_METY_REQ_REQ, bool>>>();

        

        internal HisBcsMetyReqReqSO Query()
        {
            HisBcsMetyReqReqSO search = new HisBcsMetyReqReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBcsMetyReqReqExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisBcsMetyReqReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBcsMetyReqReqExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBcsMetyReqReqExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBcsMetyReqReqExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBcsMetyReqReqExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBcsMetyReqReqExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBcsMetyReqReqExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBcsMetyReqReqExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBcsMetyReqReqExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisBcsMetyReqReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisBcsMetyReqReqExpression.AddRange(listVHisBcsMetyReqReqExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBcsMetyReqReqExpression.Clear();
                search.listVHisBcsMetyReqReqExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
