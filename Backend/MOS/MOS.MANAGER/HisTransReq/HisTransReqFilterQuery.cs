using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransReq
{
    public class HisTransReqFilterQuery : HisTransReqFilter
    {
        public HisTransReqFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TRANS_REQ, bool>>> listHisTransReqExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRANS_REQ, bool>>>();



        internal HisTransReqSO Query()
        {
            HisTransReqSO search = new HisTransReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTransReqExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisTransReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTransReqExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTransReqExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTransReqExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTransReqExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTransReqExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTransReqExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTransReqExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTransReqExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.TRANS_REQ_CODE__EXACT))
                {
                    listHisTransReqExpression.Add(o => o.TRANS_REQ_CODE == this.TRANS_REQ_CODE__EXACT);
                }
                //if (this.IS_CANCEL.HasValue && this.IS_CANCEL.Value)
                //{
                //    listHisTransReqExpression.Add(o => o.IS_CANCEL.HasValue && o.IS_CANCEL.Value == MOS.UTILITY.Constant.IS_TRUE);
                //}
                //if (this.IS_CANCEL.HasValue && !this.IS_CANCEL.Value)
                //{
                //    listHisTransReqExpression.Add(o => !o.IS_CANCEL.HasValue || o.IS_CANCEL.Value != MOS.UTILITY.Constant.IS_TRUE);
                //}
                if (this.TREATMENT_ID.HasValue)
                {
                    listHisTransReqExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                //if (!String.IsNullOrWhiteSpace(this.TIG_TRANSACTION_CODE__EXACT))
                //{
                //    listHisTransReqExpression.Add(o => o.TIG_TRANSACTION_CODE == this.TIG_TRANSACTION_CODE__EXACT);
                //}

                search.listHisTransReqExpression.AddRange(listHisTransReqExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTransReqExpression.Clear();
                search.listHisTransReqExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
