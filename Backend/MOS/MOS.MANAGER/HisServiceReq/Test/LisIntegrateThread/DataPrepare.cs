using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MOS.MANAGER.Config.CFG;
using Inventec.Core;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegrateThread
{
    class DataPrepare
    {
        public static List<OrderData> Run()
        {
            try
            {
                List<HIS_SERE_SERV> sereServs = null;
                List<HIS_SERVICE_REQ> serviceReqs = null;
                List<long> executeRoomIds = DataPrepare.GetExecuteRoomId();
                DataPrepare.GetData(executeRoomIds, ref sereServs, ref serviceReqs);
                return DataPrepare.Prepare(sereServs, serviceReqs);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static List<long> GetExecuteRoomId()
        {
            try
            {
                List<long> executeRoomIds = null;
                if (Lis2CFG.LIS_INTEGRATION_TYPE == Lis2CFG.LisIntegrationType.INVENTEC)
                {
                    executeRoomIds = HisExecuteRoomCFG.DATA
                        .Where(o => LisInventecCFG.ADDRESSES != null
                            && LisInventecCFG.ADDRESSES.Exists(t => t.RoomCode == o.EXECUTE_ROOM_CODE))
                        .Select(o => o.ROOM_ID).ToList();
                }
                else if (Lis2CFG.LIS_INTEGRATION_TYPE == Lis2CFG.LisIntegrationType.ROCHE)
                {
                    if (LisRocheCFG.CONNECTION_TYPE == RocheConnectionType.TCP_IP)
                    {
                        executeRoomIds = HisExecuteRoomCFG.DATA
                        .Where(o => LisRocheCFG.TCP_IP_ADDRESSES != null
                            && LisRocheCFG.TCP_IP_ADDRESSES.Exists(t => t.RoomCode == o.EXECUTE_ROOM_CODE))
                        .Select(o => o.ROOM_ID).ToList();
                    }
                    else if (LisRocheCFG.CONNECTION_TYPE == RocheConnectionType.FILE)
                    {
                        List<long> id1s = HisExecuteRoomCFG.DATA
                         .Where(o => LisRocheCFG.FILE_ADDRESSES != null
                             && LisRocheCFG.FILE_ADDRESSES.Exists(t => t.RoomCode == o.EXECUTE_ROOM_CODE))
                         .Select(o => o.ROOM_ID).ToList();

                        List<long> id2s = HisExecuteRoomCFG.DATA
                        .Where(o => LisRocheCFG.FILE_HL7_ADDRESSES != null
                            && LisRocheCFG.FILE_HL7_ADDRESSES.Exists(t => t.RoomCode == o.EXECUTE_ROOM_CODE))
                        .Select(o => o.ROOM_ID).ToList();

                        executeRoomIds = new List<long>();
                        if (id1s != null && id1s.Count > 0)
                        {
                            executeRoomIds.AddRange(id1s);
                        }
                        if (id2s != null && id2s.Count > 0)
                        {
                            executeRoomIds.AddRange(id2s);
                        }
                    }
                }
                else if (Lis2CFG.LIS_INTEGRATION_TYPE == Lis2CFG.LisIntegrationType.LABCONN)
                {
                    executeRoomIds = HisExecuteRoomCFG.DATA
                        .Where(o => LisCFG.LIS_ADDRESSES != null && LisCFG.LIS_ADDRESSES.Exists(t => t.RoomCode == o.EXECUTE_ROOM_CODE))
                        .Select(o => o.ROOM_ID)
                        .ToList();
                }

                if (executeRoomIds == null || executeRoomIds.Count == 0)
                {
                    LogSystem.Warn("Chua cau hinh dia chi ket noi LIS tuong ung theo phong XN");
                }
                return executeRoomIds;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        private static void GetData(List<long> executeRoomIds, ref List<HIS_SERE_SERV> sereServs, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            if (executeRoomIds != null && executeRoomIds.Count > 0)
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.IS_NOT_SENT__OR__UPDATED = true; //lay cac y lenh chua gui sang LIS hoac co cap nhat
                filter.EXECUTE_ROOM_IDs = executeRoomIds;
                filter.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN;
                filter.INTRUCTION_DATE_FROM = Convert.ToInt64(DateTime.Now.AddDays(-HisServiceReqCFG.INTEGRATE_SYSTEM_DAY_NUM_SYNC).ToString("yyyyMMdd") + "000000");
                if (IntegratedSystemCFG.INTRUCTION_TIME_MUST_BE_LESS_THAN_SYSTEM_TIME_OPTION)
                {
                    long thoiGianHtai = Inventec.Common.DateTime.Get.Now() ?? 0;
                    filter.INTRUCTION_TIME_TO = thoiGianHtai;
                }
                serviceReqs = new HisServiceReqGet().Get(filter);

                serviceReqs = serviceReqs != null ? serviceReqs.Where(o => o.IS_NO_EXECUTE != Constant.IS_TRUE || (o.IS_NO_EXECUTE == Constant.IS_TRUE && o.IS_UPDATED_EXT == Constant.IS_TRUE)).ToList() : null;
                List<long> serviceReqIds = serviceReqs != null ? serviceReqs.Select(o => o.ID).ToList() : null;

                if (serviceReqIds != null && serviceReqIds.Count > 0)
                {
                    HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                    //ssFilter.HAS_EXECUTE = true;
                    ssFilter.SERVICE_REQ_IDs = serviceReqIds;
                    //can lay ca cac du lieu da xoa (is_delete = 1)
                    ssFilter.IS_INCLUDE_DELETED = true;
                    sereServs = new HisSereServGet().Get(ssFilter);
                }
            }
        }

        private static List<OrderData> Prepare(List<HIS_SERE_SERV> sereServs, List<HIS_SERVICE_REQ> serviceReqs)
        {
            List<OrderData> result = null;
            if (serviceReqs != null && serviceReqs.Count > 0 && sereServs != null && sereServs.Count > 0)
            {
                List<long> treatmentIds = sereServs != null ? sereServs
                    .Where(o => o.TDL_TREATMENT_ID.HasValue)
                    .Select(o => o.TDL_TREATMENT_ID.Value).Distinct().ToList() : null;
                List<V_HIS_TREATMENT_FEE_1> treatments = new HisTreatmentGet().GetFeeView1ByIds(treatmentIds);
                List<HIS_KSK_CONTRACT> kskContracts = null;
                List<long> kskContractIds = treatments != null ? treatments.Where(o => o.TDL_KSK_CONTRACT_ID.HasValue).Select(s => s.TDL_KSK_CONTRACT_ID.Value).Distinct().ToList() : null;
                if (kskContractIds != null && kskContractIds.Count > 0)
                {
                    kskContracts = new HisKskContractGet().GetByIds(kskContractIds);
                }

                List<long> sereServIds = sereServs.Select(o => o.ID).ToList();
                List<HIS_SERE_SERV_BILL> bills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                List<HIS_SERE_SERV_DEPOSIT> deposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServIds);
                List<HIS_SERE_SERV_DEBT> depts = new HisSereServDebtGet().GetNoCancelBySereServIds(sereServIds);
                List<long> sereServDepositIds = deposits != null ? deposits.Select(o => o.ID).ToList() : null;
                List<HIS_SESE_DEPO_REPAY> repays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(sereServDepositIds);

                result = new List<OrderData>();

                foreach (HIS_SERVICE_REQ sr in serviceReqs)
                {
                    V_HIS_TREATMENT_FEE_1 treat = treatments != null ? treatments.Where(o => o.ID == sr.TREATMENT_ID).FirstOrDefault() : null;

                    List<HIS_SERE_SERV> ss = sereServs != null ? sereServs.Where(o => o.SERVICE_REQ_ID == sr.ID).ToList() : null;

                    if (treat != null && ss != null && ss.Count > 0)
                    {
                        //Kiem tra xem co cho phep gui sang LIS ko
                        if (SendIntegratorCheck.IsAllowSend(treat, sr, ss, bills, deposits, repays, depts))
                        {
                            OrderData data = new OrderData();
                            data.ServiceReq = sr;

                            data.Availables = ss != null ? ss.Where(o => o.SERVICE_REQ_ID == sr.ID && o.IS_DELETE != Constant.IS_TRUE && o.IS_NO_EXECUTE != Constant.IS_TRUE).ToList() : null;
                            if (treat.TDL_KSK_CONTRACT_ID.HasValue)
                            {
                                data.KskContract = kskContracts != null ? kskContracts.FirstOrDefault(o => o.ID == treat.TDL_KSK_CONTRACT_ID.Value) : null;
                            }

                            if (sr.IS_UPDATED_EXT == Constant.IS_TRUE && sr.IS_SENT_EXT == Constant.IS_TRUE)
                            {
                                data.Inserts = data.Availables != null ? data.Availables.Where(o => o.IS_SENT_EXT != Constant.IS_TRUE).ToList() : null;
                                data.Deletes = ss != null ? ss.Where(o => o.SERVICE_REQ_ID == sr.ID && (o.IS_DELETE == Constant.IS_TRUE || o.IS_NO_EXECUTE == Constant.IS_TRUE)).ToList() : null;
                            }
                            else
                            {
                                data.Inserts = data.Availables;
                            }
                            List<OrderData> d = DataPrepare.SplitOrderDataForHl7(data);
                            if (d != null && d.Count > 0)
                            {
                                result.AddRange(d);
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static List<OrderData> SplitOrderDataForHl7(OrderData order)
        {
            if (order != null
                && ((order.Inserts != null && order.Inserts.Count > 0) || (order.Deletes != null && order.Deletes.Count > 0)))
            {
                List<OrderData> rs = new List<OrderData>();

                if (LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.ASTM_HL7
                    || LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.HL7)
                {
                    Mapper.CreateMap<OrderData, OrderData>();
                    if (order.Inserts != null && order.Inserts.Count > 0)
                    {
                        OrderData insertOrder = Mapper.Map<OrderData>(order);
                        insertOrder.Deletes = null;
                        rs.Add(insertOrder);
                    }
                    if (order.Deletes != null && order.Deletes.Count > 0)
                    {
                        OrderData deleteOrder = Mapper.Map<OrderData>(order);
                        deleteOrder.Inserts = null;
                        rs.Add(deleteOrder);
                    }
                }
                else
                {
                    rs.Add(order);
                }
                return rs;
            }
            return null;
        }
    }
}
