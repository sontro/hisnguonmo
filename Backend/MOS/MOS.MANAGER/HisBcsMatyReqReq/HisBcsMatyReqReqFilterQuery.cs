using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMatyReqReq
{
    public class HisBcsMatyReqReqFilterQuery : HisBcsMatyReqReqFilter
    {
        public HisBcsMatyReqReqFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_BCS_MATY_REQ_REQ, bool>>> listHisBcsMatyReqReqExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BCS_MATY_REQ_REQ, bool>>>();



        internal HisBcsMatyReqReqSO Query()
        {
            HisBcsMatyReqReqSO search = new HisBcsMatyReqReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisBcsMatyReqReqExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisBcsMatyReqReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisBcsMatyReqReqExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisBcsMatyReqReqExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisBcsMatyReqReqExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisBcsMatyReqReqExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisBcsMatyReqReqExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisBcsMatyReqReqExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisBcsMatyReqReqExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisBcsMatyReqReqExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisBcsMatyReqReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.EXP_MEST_MATY_REQ_ID.HasValue)
                {
                    listHisBcsMatyReqReqExpression.Add(o => o.EXP_MEST_MATY_REQ_ID == this.EXP_MEST_MATY_REQ_ID.Value);
                }
                if (this.EXP_MEST_MATY_REQ_IDs != null)
                {
                    listHisBcsMatyReqReqExpression.Add(o => this.EXP_MEST_MATY_REQ_IDs.Contains(o.EXP_MEST_MATY_REQ_ID));
                }
                if (this.PRE_EXP_MEST_MATY_REQ_ID.HasValue)
                {
                    listHisBcsMatyReqReqExpression.Add(o => o.PRE_EXP_MEST_MATY_REQ_ID == this.PRE_EXP_MEST_MATY_REQ_ID.Value);
                }
                if (this.PRE_EXP_MEST_MATY_REQ_IDs != null)
                {
                    listHisBcsMatyReqReqExpression.Add(o => this.IDs.Contains(o.PRE_EXP_MEST_MATY_REQ_ID));
                }

                search.listHisBcsMatyReqReqExpression.AddRange(listHisBcsMatyReqReqExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisBcsMatyReqReqExpression.Clear();
                search.listHisBcsMatyReqReqExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
