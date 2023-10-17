using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceChangeReq
{
    public class HisServiceChangeReqViewFilterQuery : HisServiceChangeReqViewFilter
    {
        public HisServiceChangeReqViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_CHANGE_REQ, bool>>> listVHisServiceChangeReqExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_CHANGE_REQ, bool>>>();

        

        internal HisServiceChangeReqSO Query()
        {
            HisServiceChangeReqSO search = new HisServiceChangeReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisServiceChangeReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisServiceChangeReqExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisServiceChangeReqExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisServiceChangeReqExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.TDL_SERVICE_REQ_ID.HasValue)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.TDL_SERVICE_REQ_ID == this.TDL_SERVICE_REQ_ID.Value);
                }
                if (this.ALTER_SERVICE_ID.HasValue)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.ALTER_SERVICE_ID == this.ALTER_SERVICE_ID.Value);
                }
                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (!string.IsNullOrWhiteSpace(this.SERVICE_REQ_CODE__EXACT))
                {
                    listVHisServiceChangeReqExpression.Add(o => o.SERVICE_REQ_CODE == this.SERVICE_REQ_CODE__EXACT);
                }
                if (!string.IsNullOrWhiteSpace(this.TDL_TREATMENT_CODE__EXACT))
                {
                    listVHisServiceChangeReqExpression.Add(o => o.TDL_TREATMENT_CODE == this.TDL_TREATMENT_CODE__EXACT);
                }
                if (this.HAS_APPROVAL_LOGINNAME.HasValue && this.HAS_APPROVAL_LOGINNAME.Value)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.APPROVAL_LOGINNAME != null);
                }
                if (this.HAS_APPROVAL_CASHIER_LOGINNAME.HasValue && this.HAS_APPROVAL_CASHIER_LOGINNAME.Value)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.APPROVAL_CASHIER_LOGINNAME != null);
                }
                if (this.HAS_APPROVAL_LOGINNAME.HasValue && !this.HAS_APPROVAL_LOGINNAME.Value)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.APPROVAL_LOGINNAME == null );
                }
                if (this.HAS_APPROVAL_CASHIER_LOGINNAME.HasValue && !this.HAS_APPROVAL_CASHIER_LOGINNAME.Value)
                {
                    listVHisServiceChangeReqExpression.Add(o => o.APPROVAL_CASHIER_LOGINNAME == null);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisServiceChangeReqExpression.Add(o =>
                        o.SERVICE_REQ_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.APPROVAL_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.APPROVAL_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.APPROVAL_CASHIER_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.APPROVAL_CASHIER_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisServiceChangeReqExpression.AddRange(listVHisServiceChangeReqExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisServiceChangeReqExpression.Clear();
                search.listVHisServiceChangeReqExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
