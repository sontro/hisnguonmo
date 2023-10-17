using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMetyReqReq
{
    public class HisBcsMetyReqReqFilterQuery : HisBcsMetyReqReqFilter
    {
        public HisBcsMetyReqReqFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_BCS_METY_REQ_REQ, bool>>> listHisBcsMetyReqReqExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BCS_METY_REQ_REQ, bool>>>();



        internal HisBcsMetyReqReqSO Query()
        {
            HisBcsMetyReqReqSO search = new HisBcsMetyReqReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisBcsMetyReqReqExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisBcsMetyReqReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisBcsMetyReqReqExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisBcsMetyReqReqExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisBcsMetyReqReqExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisBcsMetyReqReqExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisBcsMetyReqReqExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisBcsMetyReqReqExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisBcsMetyReqReqExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisBcsMetyReqReqExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisBcsMetyReqReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.EXP_MEST_METY_REQ_ID.HasValue)
                {
                    listHisBcsMetyReqReqExpression.Add(o => o.EXP_MEST_METY_REQ_ID == this.EXP_MEST_METY_REQ_ID.Value);
                }
                if (this.EXP_MEST_METY_REQ_IDs != null)
                {
                    listHisBcsMetyReqReqExpression.Add(o => this.EXP_MEST_METY_REQ_IDs.Contains(o.EXP_MEST_METY_REQ_ID));
                }
                if (this.PRE_EXP_MEST_METY_REQ_ID.HasValue)
                {
                    listHisBcsMetyReqReqExpression.Add(o => o.PRE_EXP_MEST_METY_REQ_ID == this.PRE_EXP_MEST_METY_REQ_ID.Value);
                }
                if (this.PRE_EXP_MEST_METY_REQ_IDs != null)
                {
                    listHisBcsMetyReqReqExpression.Add(o => this.PRE_EXP_MEST_METY_REQ_IDs.Contains(o.PRE_EXP_MEST_METY_REQ_ID));
                }

                search.listHisBcsMetyReqReqExpression.AddRange(listHisBcsMetyReqReqExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisBcsMetyReqReqExpression.Clear();
                search.listHisBcsMetyReqReqExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
