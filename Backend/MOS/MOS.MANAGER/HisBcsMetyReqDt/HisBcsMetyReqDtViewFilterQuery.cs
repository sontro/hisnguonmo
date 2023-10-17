using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMetyReqDt
{
    public class HisBcsMetyReqDtViewFilterQuery : HisBcsMetyReqDtViewFilter
    {
        public HisBcsMetyReqDtViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BCS_METY_REQ_DT, bool>>> listVHisBcsMetyReqDtExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BCS_METY_REQ_DT, bool>>>();

        

        internal HisBcsMetyReqDtSO Query()
        {
            HisBcsMetyReqDtSO search = new HisBcsMetyReqDtSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBcsMetyReqDtExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisBcsMetyReqDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBcsMetyReqDtExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBcsMetyReqDtExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBcsMetyReqDtExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBcsMetyReqDtExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBcsMetyReqDtExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBcsMetyReqDtExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBcsMetyReqDtExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBcsMetyReqDtExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisBcsMetyReqDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisBcsMetyReqDtExpression.AddRange(listVHisBcsMetyReqDtExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBcsMetyReqDtExpression.Clear();
                search.listVHisBcsMetyReqDtExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
