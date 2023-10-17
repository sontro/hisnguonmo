using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceChangeReq
{
    public class HisServiceChangeReqFilterQuery : HisServiceChangeReqFilter
    {
        public HisServiceChangeReqFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_CHANGE_REQ, bool>>> listHisServiceChangeReqExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_CHANGE_REQ, bool>>>();

        

        internal HisServiceChangeReqSO Query()
        {
            HisServiceChangeReqSO search = new HisServiceChangeReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisServiceChangeReqExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisServiceChangeReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisServiceChangeReqExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisServiceChangeReqExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisServiceChangeReqExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisServiceChangeReqExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisServiceChangeReqExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisServiceChangeReqExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisServiceChangeReqExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisServiceChangeReqExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.TDL_SERVICE_REQ_ID.HasValue)
                {
                    listHisServiceChangeReqExpression.Add(o => o.TDL_SERVICE_REQ_ID == this.TDL_SERVICE_REQ_ID.Value);
                }
                if (this.SERE_SERV_ID.HasValue)
                {
                    listHisServiceChangeReqExpression.Add(o => o.SERE_SERV_ID == this.SERE_SERV_ID.Value);
                }
                if (this.ALTER_SERVICE_ID.HasValue)
                {
                    listHisServiceChangeReqExpression.Add(o => o.ALTER_SERVICE_ID == this.ALTER_SERVICE_ID.Value);
                }
                if (this.HAS_APPROVAL_LOGINNAME.HasValue && this.HAS_APPROVAL_LOGINNAME.Value)
                {
                    listHisServiceChangeReqExpression.Add(o => o.APPROVAL_LOGINNAME != null && !string.IsNullOrWhiteSpace(o.APPROVAL_LOGINNAME));
                }
                if (this.HAS_APPROVAL_CASHIER_LOGINNAME.HasValue && this.HAS_APPROVAL_CASHIER_LOGINNAME.Value)
                {
                    listHisServiceChangeReqExpression.Add(o => o.APPROVAL_CASHIER_LOGINNAME != null && !string.IsNullOrWhiteSpace(o.APPROVAL_CASHIER_LOGINNAME));
                }
                if (this.HAS_APPROVAL_LOGINNAME.HasValue && !this.HAS_APPROVAL_LOGINNAME.Value)
                {
                    listHisServiceChangeReqExpression.Add(o => o.APPROVAL_LOGINNAME == null || string.IsNullOrWhiteSpace(o.APPROVAL_LOGINNAME));
                }
                if (this.HAS_APPROVAL_CASHIER_LOGINNAME.HasValue && !this.HAS_APPROVAL_CASHIER_LOGINNAME.Value)
                {
                    listHisServiceChangeReqExpression.Add(o => o.APPROVAL_CASHIER_LOGINNAME == null || string.IsNullOrWhiteSpace(o.APPROVAL_CASHIER_LOGINNAME));
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisServiceChangeReqExpression.Add(o =>
                        o.APPROVAL_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.APPROVAL_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.APPROVAL_CASHIER_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.APPROVAL_CASHIER_USERNAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisServiceChangeReqExpression.AddRange(listHisServiceChangeReqExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisServiceChangeReqExpression.Clear();
                search.listHisServiceChangeReqExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
