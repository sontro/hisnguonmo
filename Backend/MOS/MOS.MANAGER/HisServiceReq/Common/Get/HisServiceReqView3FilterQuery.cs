using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public class HisServiceReqView3FilterQuery : HisServiceReqView3Filter
    {
        public HisServiceReqView3FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_REQ_3, bool>>> listVHisServiceReq3Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_REQ_3, bool>>>();

        internal HisServiceReqSO Query()
        {
            HisServiceReqSO search = new HisServiceReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisServiceReq3Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listVHisServiceReq3Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listVHisServiceReq3Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listVHisServiceReq3Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.SERVICE_REQ_CODE))
                {
                    listVHisServiceReq3Expression.Add(o => o.SERVICE_REQ_CODE == this.SERVICE_REQ_CODE);
                }
                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (this.SERVICE_REQ_STT_IDs != null)
                {
                    search.listVHisServiceReq3Expression.Add(o => this.SERVICE_REQ_STT_IDs.Contains(o.SERVICE_REQ_STT_ID));
                }
                if (this.INTRUCTION_TIME_FROM.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.INTRUCTION_TIME >= this.INTRUCTION_TIME_FROM.Value);
                }
                if (this.INTRUCTION_TIME_TO.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.INTRUCTION_TIME <= this.INTRUCTION_TIME_TO.Value);
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.EXECUTE_DEPARTMENT_ID.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.EXECUTE_DEPARTMENT_ID == this.EXECUTE_DEPARTMENT_ID.Value);
                }
                if (this.FINISH_TIME_FROM.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.FINISH_TIME >= this.FINISH_TIME_FROM);
                }
                if (this.FINISH_TIME_TO.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.FINISH_TIME <= this.FINISH_TIME_TO);
                }
                if (this.EXECUTE_DEPARTMENT_IDs != null)
                {
                    search.listVHisServiceReq3Expression.Add(s => EXECUTE_DEPARTMENT_IDs.Contains(s.EXECUTE_DEPARTMENT_ID));
                }
                if (this.START_TIME_FROM != null)
                {
                    search.listVHisServiceReq3Expression.Add(s => s.START_TIME.HasValue && s.START_TIME.Value >= this.START_TIME_FROM.Value);
                }
                if (this.START_TIME_TO != null)
                {
                    search.listVHisServiceReq3Expression.Add(s => s.START_TIME.HasValue && s.START_TIME.Value <= this.START_TIME_TO.Value);
                }
                if (this.REQUEST_DEPARTMENT_IDs != null)
                {
                    search.listVHisServiceReq3Expression.Add(s => this.REQUEST_DEPARTMENT_IDs.Contains(s.REQUEST_DEPARTMENT_ID));
                }
                if (this.REQUEST_DEPARTMENT_ID != null)
                {
                    search.listVHisServiceReq3Expression.Add(s => s.REQUEST_DEPARTMENT_ID == this.REQUEST_DEPARTMENT_ID);
                }
                if (!String.IsNullOrWhiteSpace(this.SESSION_CODE__EXACT))
                {
                    listVHisServiceReq3Expression.Add(o => o.SESSION_CODE == this.SESSION_CODE__EXACT);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisServiceReq3Expression.Add(o =>
                        o.TDL_TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.BARCODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_REQ_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (this.TREATMENT_IDs != null)
                {
                    search.listVHisServiceReq3Expression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.SERVICE_REQ_TYPE_ID.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.SERVICE_REQ_TYPE_ID == this.SERVICE_REQ_TYPE_ID.Value);
                }
                if (this.SERVICE_REQ_TYPE_IDs != null)
                {
                    search.listVHisServiceReq3Expression.Add(o => this.SERVICE_REQ_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID));
                }
                if (this.TRACKING_ID.HasValue)
                {
                    listVHisServiceReq3Expression.Add(o => o.TRACKING_ID.HasValue && o.TRACKING_ID.Value == this.TRACKING_ID.Value);
                }
                if (this.TRACKING_IDs != null)
                {
                    listVHisServiceReq3Expression.Add(o => o.TRACKING_ID.HasValue && this.TRACKING_IDs.Contains(o.TRACKING_ID.Value));
                }
                if (this.TDL_PATIENT_ID.HasValue)
                {
                    search.listVHisServiceReq3Expression.Add(o => o.TDL_PATIENT_ID == this.TDL_PATIENT_ID.Value);
                }
                if (this.IS_NO_EXECUTE.HasValue && this.IS_NO_EXECUTE.Value)
                {
                    listVHisServiceReq3Expression.Add(o => o.IS_NO_EXECUTE.HasValue && o.IS_NO_EXECUTE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_NO_EXECUTE.HasValue && !this.IS_NO_EXECUTE.Value)
                {
                    listVHisServiceReq3Expression.Add(o => !o.IS_NO_EXECUTE.HasValue || o.IS_NO_EXECUTE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    search.listVHisServiceReq3Expression.Add(o => o.TDL_TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (this.INTRUCTION_DATE_FROM.HasValue)
                {
                    listVHisServiceReq3Expression.Add(o => o.INTRUCTION_DATE >= this.INTRUCTION_DATE_FROM.Value);
                }
                if (this.INTRUCTION_DATE_TO.HasValue)
                {
                    listVHisServiceReq3Expression.Add(o => o.INTRUCTION_DATE <= this.INTRUCTION_DATE_TO.Value);
                }

                search.listVHisServiceReq3Expression.AddRange(listVHisServiceReq3Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
                search.ExtraOrderField1 = ORDER_FIELD1;
                search.ExtraOrderDirection1 = ORDER_DIRECTION1;
                search.ExtraOrderField3 = ORDER_FIELD2;
                search.ExtraOrderDirection3 = ORDER_DIRECTION2;
                search.ExtraOrderField3 = ORDER_FIELD3;
                search.ExtraOrderDirection3 = ORDER_DIRECTION3;
                search.ExtraOrderField4 = ORDER_FIELD4;
                search.ExtraOrderDirection4 = ORDER_DIRECTION4;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisServiceReq3Expression.Clear();
                search.listVHisServiceReq3Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
