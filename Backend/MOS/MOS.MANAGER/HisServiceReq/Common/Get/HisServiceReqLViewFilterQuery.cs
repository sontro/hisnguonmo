using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.Token;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq
{
    public class HisServiceReqLViewFilterQuery : HisServiceReqLViewFilter
    {
        public HisServiceReqLViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_SERVICE_REQ, bool>>> listLHisServiceReqExpression = new List<System.Linq.Expressions.Expression<Func<L_HIS_SERVICE_REQ, bool>>>();

        internal HisServiceReqSO Query()
        {
            HisServiceReqSO search = new HisServiceReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listLHisServiceReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.PARENT_ID.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.PARENT_ID == this.PARENT_ID);
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID);
                }
                if (this.SERVICE_REQ_TYPE_ID.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.SERVICE_REQ_TYPE_ID == this.SERVICE_REQ_TYPE_ID);
                }
                if (this.REQUEST_ROOM_ID__OR__EXECUTE_ROOM_ID.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID__OR__EXECUTE_ROOM_ID.Value || o.EXECUTE_ROOM_ID == this.REQUEST_ROOM_ID__OR__EXECUTE_ROOM_ID.Value);
                }
                if (this.SERVICE_REQ_STT_ID.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.SERVICE_REQ_STT_ID == this.SERVICE_REQ_STT_ID.Value);
                }
                if (this.SERVICE_REQ_STT_IDs != null)
                {
                    search.listLHisServiceReqExpression.Add(o => this.SERVICE_REQ_STT_IDs.Contains(o.SERVICE_REQ_STT_ID));
                }
                if (this.PARENT_IDs != null)
                {
                    search.listLHisServiceReqExpression.Add(o => o.PARENT_ID.HasValue && this.PARENT_IDs.Contains(o.PARENT_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.SERVICE_REQ_CODE__ENDS_WITH))
                {
                    search.listLHisServiceReqExpression.Add(o => o.SERVICE_REQ_CODE.EndsWith(this.SERVICE_REQ_CODE__ENDS_WITH));
                }
                if (this.INTRUCTION_TIME_FROM.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.INTRUCTION_TIME >= this.INTRUCTION_TIME_FROM.Value);
                }
                if (this.INTRUCTION_TIME_TO.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.INTRUCTION_TIME <= this.INTRUCTION_TIME_TO.Value);
                }
                if (this.REQUEST_DEPARTMENT_ID.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.REQUEST_DEPARTMENT_ID == this.REQUEST_DEPARTMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    search.listLHisServiceReqExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (!String.IsNullOrEmpty(this.SERVICE_REQ_CODE__EXACT))
                {
                    search.listLHisServiceReqExpression.Add(o => o.SERVICE_REQ_CODE == this.SERVICE_REQ_CODE__EXACT);
                }
                if (this.DHST_ID.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.DHST_ID.HasValue && o.DHST_ID.Value == this.DHST_ID.Value);
                }
                if (this.DHST_IDs != null)
                {
                    search.listLHisServiceReqExpression.Add(o => o.DHST_ID.HasValue && this.DHST_IDs.Contains(o.DHST_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listLHisServiceReqExpression.Add(o => o.TDL_TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.SERVICE_REQ_CODE__EXACT))
                {
                    listLHisServiceReqExpression.Add(o => o.SERVICE_REQ_CODE == this.SERVICE_REQ_CODE__EXACT);
                }
                if (this.SERVICE_REQ_TYPE_IDs != null)
                {
                    listLHisServiceReqExpression.Add(o => this.SERVICE_REQ_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID));
                }
                if (this.REQUEST_ROOM_IDs != null)
                {
                    listLHisServiceReqExpression.Add(o => this.REQUEST_ROOM_IDs.Contains(o.REQUEST_ROOM_ID));
                }
                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    listLHisServiceReqExpression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }
                if (this.TDL_PATIENT_TYPE_ID.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && o.TDL_PATIENT_TYPE_ID.Value == this.TDL_PATIENT_TYPE_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME__PATIENT_CODE))
                {
                    string keyword = this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME__PATIENT_CODE.ToLower().Trim();
                    listLHisServiceReqExpression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(keyword)
                        || o.SERVICE_REQ_CODE.ToLower().Contains(keyword)
                        || o.TDL_TREATMENT_CODE.ToLower().Contains(keyword)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(keyword)
                        || o.TDL_PATIENT_UNSIGNED_NAME.ToLower().Contains(keyword)
                        );
                }

                if (!String.IsNullOrEmpty(this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME))
                {
                    string keyword = this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME.ToLower().Trim();
                    listLHisServiceReqExpression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(keyword)
                        || o.SERVICE_REQ_CODE.ToLower().Contains(keyword)
                        || o.TDL_TREATMENT_CODE.ToLower().Contains(keyword)
                        );
                }

                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listLHisServiceReqExpression.Add(o => this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID));
                }
                if (this.NOT_IN_SERVICE_REQ_TYPE_IDs != null)
                {
                    search.listLHisServiceReqExpression.Add(o => !this.NOT_IN_SERVICE_REQ_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID));
                }
                if (this.NOT_IN_IDs != null && this.NOT_IN_IDs.Count > 0)
                {
                    search.listLHisServiceReqExpression.Add(o => !this.NOT_IN_IDs.Contains(o.ID));
                }
                if (this.IS_NOT_SURGERY__OR__IS__APPROVED__OR__IS_EMERGENCY.HasValue && this.IS_NOT_SURGERY__OR__IS__APPROVED__OR__IS_EMERGENCY.Value)
                {
                    listLHisServiceReqExpression.Add(o =>
                        o.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT ||
                        o.PTTT_APPROVAL_STT_ID == IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__APPROVED ||
                        o.IS_EMERGENCY == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_NOT_SURGERY__OR__IS__APPROVED__OR__IS_EMERGENCY.HasValue && !this.IS_NOT_SURGERY__OR__IS__APPROVED__OR__IS_EMERGENCY.Value)
                {
                    listLHisServiceReqExpression.Add(o =>
                        o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT &&
                        o.PTTT_APPROVAL_STT_ID != IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__APPROVED &&
                        o.IS_EMERGENCY != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_EXECUTE.HasValue && this.HAS_EXECUTE.Value)
                {
                    listLHisServiceReqExpression.Add(o => !o.IS_NO_EXECUTE.HasValue || o.IS_NO_EXECUTE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_EXECUTE.HasValue && !this.HAS_EXECUTE.Value)
                {
                    listLHisServiceReqExpression.Add(o => o.IS_NO_EXECUTE.HasValue && o.IS_NO_EXECUTE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.TDL_TREATMENT_TYPE_ID.HasValue)
                {
                    listLHisServiceReqExpression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && o.TDL_TREATMENT_TYPE_ID.Value == this.TDL_TREATMENT_TYPE_ID.Value);
                }
                if (this.TDL_TREATMENT_TYPE_IDs != null)
                {
                    listLHisServiceReqExpression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && this.TDL_TREATMENT_TYPE_IDs.Contains(o.TDL_TREATMENT_TYPE_ID.Value));
                }
                if (this.IS_NOT_KSK_REQURIED_APROVAL__OR__IS_KSK_APPROVE.HasValue)
                {
                    if (this.IS_NOT_KSK_REQURIED_APROVAL__OR__IS_KSK_APPROVE.Value)
                    {
                        listLHisServiceReqExpression.Add(o => !o.TDL_KSK_IS_REQUIRED_APPROVAL.HasValue || o.TDL_KSK_IS_REQUIRED_APPROVAL.Value != Constant.IS_TRUE || (o.TDL_IS_KSK_APPROVE.HasValue && o.TDL_IS_KSK_APPROVE.Value == Constant.IS_TRUE));
                    }
                    else
                    {
                        listLHisServiceReqExpression.Add(o => o.TDL_KSK_IS_REQUIRED_APPROVAL.HasValue && o.TDL_KSK_IS_REQUIRED_APPROVAL.Value == Constant.IS_TRUE && (!o.TDL_IS_KSK_APPROVE.HasValue || o.TDL_IS_KSK_APPROVE.Value != Constant.IS_TRUE));
                    }
                }
                if (this.IS_NOT_IN_DEBT.HasValue && this.IS_NOT_IN_DEBT.Value)
                {
                    listLHisServiceReqExpression.Add(o => o.IS_NOT_IN_DEBT == Constant.IS_TRUE);
                }
                if (this.IS_NOT_IN_DEBT.HasValue && !this.IS_NOT_IN_DEBT.Value)
                {
                    listLHisServiceReqExpression.Add(o => !o.IS_NOT_IN_DEBT.HasValue || o.IS_NOT_IN_DEBT != Constant.IS_TRUE);
                }
                if (this.IS_RESULTED.HasValue && this.IS_RESULTED.Value)
                {
                    listLHisServiceReqExpression.Add(o => o.IS_RESULTED == Constant.IS_TRUE);
                }
                if (this.IS_RESULTED.HasValue && !this.IS_RESULTED.Value)
                {
                    listLHisServiceReqExpression.Add(o => !o.IS_RESULTED.HasValue || o.IS_RESULTED != Constant.IS_TRUE);
                }
                if (this.INTRUCTION_DATE__EQUAL.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.INTRUCTION_DATE == this.INTRUCTION_DATE__EQUAL.Value);
                }
                if (this.VIR_INTRUCTION_MONTH__EQUAL.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.VIR_INTRUCTION_MONTH.HasValue && o.VIR_INTRUCTION_MONTH == this.VIR_INTRUCTION_MONTH__EQUAL.Value);
                }
                if (this.INTRUCTION_DATE_FROM.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.INTRUCTION_DATE >= this.INTRUCTION_DATE_FROM.Value);
                }
                if (this.INTRUCTION_DATE_TO.HasValue)
                {
                    search.listLHisServiceReqExpression.Add(o => o.INTRUCTION_DATE <= this.INTRUCTION_DATE_TO.Value);
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
                    search.listLHisServiceReqExpression.Add(o => o.VIR_INTRUCTION_MONTH.HasValue && INTRUCTION_MONTHs.Contains(o.VIR_INTRUCTION_MONTH.Value));
                }

                if (this.SERVICE_IDs != null)
                {
                    var searchPredicate = PredicateBuilder.False<L_HIS_SERVICE_REQ>();

                    foreach (long id in this.SERVICE_IDs)
                    {
                        var closureVariable = ";" + id.ToString() + ";";//can khai bao bien rieng de cho vao menh de ben duoi
                        searchPredicate = searchPredicate.Or(o => (o.TDL_SERVICE_IDS != null && (";" + o.TDL_SERVICE_IDS + ";").Contains(closureVariable)));
                    }
                    listLHisServiceReqExpression.Add(searchPredicate);
                }
                if (this.HAS_CALL_COUNT.HasValue && this.HAS_CALL_COUNT.Value)
                {
                    listLHisServiceReqExpression.Add(o => o.CALL_COUNT > 0);
                }
                if (this.HAS_CALL_COUNT.HasValue && !this.HAS_CALL_COUNT.Value)
                {
                    listLHisServiceReqExpression.Add(o => !o.CALL_COUNT.HasValue || o.CALL_COUNT <= 0);
                }
                if (this.IS_ENOUGH_SUBCLINICAL_PRES.HasValue && this.IS_ENOUGH_SUBCLINICAL_PRES.Value)
                {
                    listLHisServiceReqExpression.Add(o => o.IS_ENOUGH_SUBCLINICAL_PRES == Constant.IS_TRUE);
                }
                if (this.IS_ENOUGH_SUBCLINICAL_PRES.HasValue && !this.IS_ENOUGH_SUBCLINICAL_PRES.Value)
                {
                    listLHisServiceReqExpression.Add(o => o.IS_ENOUGH_SUBCLINICAL_PRES == null || o.IS_ENOUGH_SUBCLINICAL_PRES != Constant.IS_TRUE);
                }
                if (this.TDL_KSK_CONTRACT_ID.HasValue)
                {
                    listLHisServiceReqExpression.Add(o => o.TDL_KSK_CONTRACT_ID.HasValue && o.TDL_KSK_CONTRACT_ID == this.TDL_KSK_CONTRACT_ID.Value);
                }
                if (this.IS_RESTRICTED_KSK.HasValue && this.IS_RESTRICTED_KSK.Value)
                {
                    List<long> kskContractIds = TokenManager.GetAccessibleKskContract() ?? new List<long>();
                    listLHisServiceReqExpression.Add
                        (o => !o.TDL_KSK_CONTRACT_ID.HasValue
                              ||
                              (o.TDL_KSK_CONTRACT_ID.HasValue &&
                                (
                                    (!o.TDL_KSK_CONTRACT_IS_RESTRICTED.HasValue || o.TDL_KSK_CONTRACT_IS_RESTRICTED.Value != Constant.IS_TRUE)
                                    ||
                                    (
                                        o.TDL_KSK_CONTRACT_IS_RESTRICTED.HasValue && o.TDL_KSK_CONTRACT_IS_RESTRICTED.Value == Constant.IS_TRUE &&
                                            kskContractIds.Contains(o.TDL_KSK_CONTRACT_ID.Value)
                                    )
                                )
                              )
                        );
                }
                if (!string.IsNullOrWhiteSpace(this.BED_CODE__BED_NAME))
                {
                    HisBedLogView1FilterQuery filter = new HisBedLogView1FilterQuery();
                    filter.IS_FINISH = false;
                    filter.BED_CODE__OR__BED_NAME = this.BED_CODE__BED_NAME;
                    List<V_HIS_BED_LOG_1> vbedLogs = new HisBedLogGet().GetView1(filter);
                    if (vbedLogs != null && vbedLogs.Count > 0)
                    {
                        List<long> ids = vbedLogs.Select( o => o.TREATMENT_ID).Distinct().ToList();
                        if (ids != null && ids.Count > 0)
                        {
                            listLHisServiceReqExpression.Add(o => ids.Contains(o.TREATMENT_ID));
                        }
                    }
                }

                search.listLHisServiceReqExpression.AddRange(listLHisServiceReqExpression);
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
                search.listLHisServiceReqExpression.Clear();
                search.listLHisServiceReqExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
