using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSeseTransReq
{
    public class HisSeseTransReqFilterQuery : HisSeseTransReqFilter
    {
        public HisSeseTransReqFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SESE_TRANS_REQ, bool>>> listHisSeseTransReqExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SESE_TRANS_REQ, bool>>>();

        

        internal HisSeseTransReqSO Query()
        {
            HisSeseTransReqSO search = new HisSeseTransReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisSeseTransReqExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisSeseTransReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisSeseTransReqExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisSeseTransReqExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisSeseTransReqExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisSeseTransReqExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisSeseTransReqExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisSeseTransReqExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisSeseTransReqExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisSeseTransReqExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.TRANS_REQ_ID.HasValue)
                {
                    listHisSeseTransReqExpression.Add(o => o.TRANS_REQ_ID == this.TRANS_REQ_ID.Value);
                }
                if (this.TRANS_REQ_IDs != null)
                {
                    listHisSeseTransReqExpression.Add(o => this.TRANS_REQ_IDs.Contains(o.TRANS_REQ_ID));
                }

                if (this.SERE_SERV_IDs != null)
                {
                    listHisSeseTransReqExpression.Add(o => this.SERE_SERV_IDs.Contains(o.SERE_SERV_ID));
                }

                search.listHisSeseTransReqExpression.AddRange(listHisSeseTransReqExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisSeseTransReqExpression.Clear();
                search.listHisSeseTransReqExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
