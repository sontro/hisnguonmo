using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public class HisServiceReqLView1FilterQuery : HisServiceReqLView1Filter
    {
        public HisServiceReqLView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_SERVICE_REQ_1, bool>>> listLHisServiceReq1Expression = new List<System.Linq.Expressions.Expression<Func<L_HIS_SERVICE_REQ_1, bool>>>();

        internal HisServiceReqSO Query()
        {
            HisServiceReqSO search = new HisServiceReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listLHisServiceReq1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listLHisServiceReq1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    search.listLHisServiceReq1Expression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID);
                }
                if (this.SERVICE_REQ_STT_ID.HasValue)
                {
                    search.listLHisServiceReq1Expression.Add(o => o.SERVICE_REQ_STT_ID == this.SERVICE_REQ_STT_ID.Value);
                }
                if (this.SERVICE_REQ_STT_IDs != null)
                {
                    search.listLHisServiceReq1Expression.Add(o => this.SERVICE_REQ_STT_IDs.Contains(o.SERVICE_REQ_STT_ID));
                }
                if (this.INTRUCTION_TIME_FROM.HasValue)
                {
                    search.listLHisServiceReq1Expression.Add(o => o.INTRUCTION_TIME >= this.INTRUCTION_TIME_FROM.Value);
                }
                if (this.INTRUCTION_TIME_TO.HasValue)
                {
                    search.listLHisServiceReq1Expression.Add(o => o.INTRUCTION_TIME <= this.INTRUCTION_TIME_TO.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    search.listLHisServiceReq1Expression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (!String.IsNullOrEmpty(this.SERVICE_REQ_CODE__EXACT))
                {
                    search.listLHisServiceReq1Expression.Add(o => o.SERVICE_REQ_CODE == this.SERVICE_REQ_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listLHisServiceReq1Expression.Add(o => o.TDL_TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.SERVICE_REQ_CODE__EXACT))
                {
                    listLHisServiceReq1Expression.Add(o => o.SERVICE_REQ_CODE == this.SERVICE_REQ_CODE__EXACT);
                }
                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    search.listLHisServiceReq1Expression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    listLHisServiceReq1Expression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }
                if (this.TDL_PATIENT_TYPE_ID.HasValue)
                {
                    search.listLHisServiceReq1Expression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && o.TDL_PATIENT_TYPE_ID.Value == this.TDL_PATIENT_TYPE_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME__PATIENT_CODE))
                {
                    string keyword = this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME__PATIENT_CODE.ToLower().Trim();
                    listLHisServiceReq1Expression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(keyword)
                        || o.SERVICE_REQ_CODE.ToLower().Contains(keyword)
                        || o.TDL_TREATMENT_CODE.ToLower().Contains(keyword)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(keyword)
                        );
                }

                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listLHisServiceReq1Expression.Add(o => this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID));
                }
                if (this.NOT_IN_IDs != null && this.NOT_IN_IDs.Count > 0)
                {
                    search.listLHisServiceReq1Expression.Add(o => !this.NOT_IN_IDs.Contains(o.ID));
                }
                if (this.HAS_EXECUTE.HasValue && this.HAS_EXECUTE.Value)
                {
                    listLHisServiceReq1Expression.Add(o => !o.IS_NO_EXECUTE.HasValue || o.IS_NO_EXECUTE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_EXECUTE.HasValue && !this.HAS_EXECUTE.Value)
                {
                    listLHisServiceReq1Expression.Add(o => o.IS_NO_EXECUTE.HasValue && o.IS_NO_EXECUTE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }                
                if (this.IS_NOT_IN_DEBT.HasValue && this.IS_NOT_IN_DEBT.Value)
                {
                    listLHisServiceReq1Expression.Add(o => o.IS_NOT_IN_DEBT == Constant.IS_TRUE);
                }
                if (this.IS_NOT_IN_DEBT.HasValue && !this.IS_NOT_IN_DEBT.Value)
                {
                    listLHisServiceReq1Expression.Add(o => !o.IS_NOT_IN_DEBT.HasValue || o.IS_NOT_IN_DEBT != Constant.IS_TRUE);
                }
                if (this.INTRUCTION_DATE__EQUAL.HasValue)
                {
                    search.listLHisServiceReq1Expression.Add(o => o.INTRUCTION_DATE == this.INTRUCTION_DATE__EQUAL.Value);
                }
                if (this.VIR_INTRUCTION_MONTH__EQUAL.HasValue)
                {
                    search.listLHisServiceReq1Expression.Add(o => o.VIR_INTRUCTION_MONTH.HasValue && o.VIR_INTRUCTION_MONTH == this.VIR_INTRUCTION_MONTH__EQUAL.Value);
                }
                if (this.INTRUCTION_DATE_FROM.HasValue)
                {
                    search.listLHisServiceReq1Expression.Add(o => o.INTRUCTION_DATE >= this.INTRUCTION_DATE_FROM.Value);
                }
                if (this.INTRUCTION_DATE_TO.HasValue)
                {
                    search.listLHisServiceReq1Expression.Add(o => o.INTRUCTION_DATE <= this.INTRUCTION_DATE_TO.Value);
                }

                if (this.INTRUCTION_DATE_FROM.HasValue && this.INTRUCTION_DATE_TO.HasValue && this.INTRUCTION_MONTHs == null)
                {
                    DateTime dateFrom = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(this.INTRUCTION_DATE_FROM.Value) ?? DateTime.MinValue;
                    DateTime dateTo = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(this.INTRUCTION_DATE_TO.Value) ?? DateTime.MinValue;
                    if (dateTo != DateTime.MinValue && dateFrom != DateTime.MinValue)
                    {
                        this.INTRUCTION_MONTHs = new List<decimal>();
                        this.INTRUCTION_MONTHs.Add(long.Parse(dateTo.ToString("yyyyMM") + "00000000"));
                        while (dateTo.Month > dateFrom.Month)
                        {
                            this.INTRUCTION_MONTHs.Add(long.Parse(dateTo.ToString("yyyyMM") + "00000000"));
                            dateTo.AddMonths(-1);
                        }
                    }
                }

                if (this.INTRUCTION_MONTHs != null)
                {
                    search.listLHisServiceReq1Expression.Add(o => o.VIR_INTRUCTION_MONTH.HasValue && INTRUCTION_MONTHs.Contains(o.VIR_INTRUCTION_MONTH.Value));
                }

                if (this.SERVICE_IDs != null)
                {
                    var searchPredicate = PredicateBuilder.False<L_HIS_SERVICE_REQ_1>();

                    foreach (long id in this.SERVICE_IDs)
                    {
                        var closureVariable = ";" + id.ToString() + ";";//can khai bao bien rieng de cho vao menh de ben duoi
                        searchPredicate = searchPredicate.Or(o => (o.TDL_SERVICE_IDS != null && (";" + o.TDL_SERVICE_IDS + ";").Contains(closureVariable)));
                    }
                    listLHisServiceReq1Expression.Add(searchPredicate);
                }

                search.listLHisServiceReq1Expression.AddRange(listLHisServiceReq1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
                search.ExtraOrderField1 = ORDER_FIELD1;
                search.ExtraOrderDirection1 = ORDER_DIRECTION1;
                search.ExtraOrderField2 = ORDER_FIELD2;
                search.ExtraOrderDirection2 = ORDER_DIRECTION2;
                search.ExtraOrderField3 = ORDER_FIELD3;
                search.ExtraOrderDirection3 = ORDER_DIRECTION3;
                search.ExtraOrderField4 = ORDER_FIELD4;
                search.ExtraOrderDirection4 = ORDER_DIRECTION4;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listLHisServiceReq1Expression.Clear();
                search.listLHisServiceReq1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
