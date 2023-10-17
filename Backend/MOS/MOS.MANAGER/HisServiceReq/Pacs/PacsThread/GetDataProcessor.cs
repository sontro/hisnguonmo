using AutoMapper;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs.PacsThread
{
    class GetDataProcessor
    {
        public static List<PacsOrderData> Run()
        {
            try
            {
                List<HIS_SERE_SERV> sereServs = null;
                List<HIS_SERVICE_REQ> serviceReqs = null;
                GetDataProcessor.GetData(ref sereServs, ref serviceReqs);
                return GetDataProcessor.Prepare(sereServs, serviceReqs);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        private static void GetData(ref List<HIS_SERE_SERV> sereServs, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            List<long> executeRoomIds = null;

            if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.FILE)
            {
                executeRoomIds = HisRoomCFG.DATA.Where(o => PacsCFG.PACS_FILE_ADDRESSES.Any(a => a.RoomCode == o.ROOM_CODE && !String.IsNullOrWhiteSpace(a.SaveFolder))).Select(s => s.ID).ToList();
            }
            else
            {
                executeRoomIds = HisRoomCFG.DATA.Where(o => PacsCFG.PACS_ADDRESS.Any(a => a.RoomCode == o.ROOM_CODE && !String.IsNullOrWhiteSpace(a.Address))).Select(s => s.ID).ToList();
            }

            if (executeRoomIds != null && executeRoomIds.Count > 0)
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.IS_NOT_SENT__OR__UPDATED = true; //lay cac y lenh chua gui sang PACS hoac co cap nhat
                filter.HAS_EXECUTE = true;
                filter.EXECUTE_ROOM_IDs = executeRoomIds;
                filter.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                filter.ALLOW_SEND_PACS = true;
                filter.NOT_IN_SERVICE_REQ_TYPE_IDs = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN };
                filter.CREATE_TIME_FROM = Convert.ToInt64(DateTime.Now.AddDays(-HisServiceReqCFG.INTEGRATE_SYSTEM_DAY_NUM_SYNC).ToString("yyyyMMdd") + "000000");
                if (IntegratedSystemCFG.INTRUCTION_TIME_MUST_BE_LESS_THAN_SYSTEM_TIME_OPTION)
                {
                    long thoiGianHtai = Inventec.Common.DateTime.Get.Now() ?? 0;
                    filter.INTRUCTION_TIME_TO = thoiGianHtai;
                }
                serviceReqs = new HisServiceReqGet().Get(filter);
                
                List<long> serviceReqIds = serviceReqs != null ? serviceReqs.Select(o => o.ID).ToList() : null;

                if (serviceReqIds != null && serviceReqIds.Count > 0)
                {
                    HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                    ssFilter.HAS_EXECUTE = true;
                    ssFilter.SERVICE_REQ_IDs = serviceReqIds;
                    //voi sancy thi can lay ca cac du lieu da xoa (is_delete = 1)
                    ssFilter.IS_INCLUDE_DELETED = true;
                    sereServs = new HisSereServGet().Get(ssFilter);
                }
            }
        }

        internal static List<PacsOrderData> Prepare(List<HIS_SERE_SERV> sereServs, List<HIS_SERVICE_REQ> serviceReqs)
        {
            List<PacsOrderData> result = null;
            if (serviceReqs != null && serviceReqs.Count > 0 && sereServs != null && sereServs.Count > 0)
            {
                List<long> treatmentIds = sereServs != null ? sereServs
                    .Where(o => o.TDL_TREATMENT_ID.HasValue)
                    .Select(o => o.TDL_TREATMENT_ID.Value).Distinct().ToList() : null;
                List<V_HIS_TREATMENT_FEE_1> treatments = new HisTreatmentGet().GetFeeView1ByIds(treatmentIds);

                List<long> sereServIds = sereServs.Select(o => o.ID).ToList();
                List<HIS_SERE_SERV_BILL> bills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                List<HIS_SERE_SERV_DEPOSIT> deposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServIds);
                List<long> sereServDepositIds = deposits != null ? deposits.Select(o => o.ID).ToList() : null;
                List<HIS_SESE_DEPO_REPAY> repays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(sereServDepositIds);
                List<HIS_SERE_SERV_DEBT> depts = new HisSereServDebtGet().GetNoCancelBySereServIds(sereServIds);

                List<V_HIS_KSK_CONTRACT> kskContract = null;
                List<long> kskContractIds = serviceReqs.Where(o => o.TDL_KSK_CONTRACT_ID.HasValue).Select(s => s.TDL_KSK_CONTRACT_ID.Value).Distinct().ToList();
                if (kskContractIds != null && kskContractIds.Count > 0)
                {
                    kskContract = new HisKskContractGet().GetViewByIds(kskContractIds);
                }

                result = new List<PacsOrderData>();

                Mapper.CreateMap<V_HIS_TREATMENT_FEE_1, HIS_TREATMENT>();

                foreach (HIS_SERVICE_REQ sr in serviceReqs)
                {
                    V_HIS_TREATMENT_FEE_1 treat = treatments != null ? treatments.Where(o => o.ID == sr.TREATMENT_ID).FirstOrDefault() : null;
                    List<HIS_SERE_SERV> ss = sereServs != null ? sereServs.Where(o => o.SERVICE_REQ_ID == sr.ID).ToList() : null;

                    //Kiem tra xem co cho phep gui sang PACS ko
                    if (ss != null && treat != null && ss.Count > 0 && SendIntegratorCheck.IsAllowSend(treat, sr, ss, bills, deposits, repays, depts))
                    {
                        PacsOrderData data = new PacsOrderData();
                        data.Treatment = Mapper.Map<HIS_TREATMENT>(treat);
                        data.ServiceReq = sr;
                        data.Availables = ss != null ? ss.Where(o => o.SERVICE_REQ_ID == sr.ID && o.IS_DELETE != Constant.IS_TRUE).ToList() : null;

                        data.Inserts = data.Availables != null ? data.Availables.Where(o => !o.IS_SENT_EXT.HasValue).ToList() : null;

                        if (sr.IS_UPDATED_EXT == Constant.IS_TRUE && sr.IS_SENT_EXT == Constant.IS_TRUE)
                        {
                            data.Deletes = ss != null ? ss.Where(o => o.SERVICE_REQ_ID == sr.ID && o.IS_DELETE == Constant.IS_TRUE && !o.IS_SENT_EXT.HasValue).ToList() : null;
                        }

                        if (kskContract != null && kskContract.Count > 0)
                        {
                            data.KskContract = kskContract.FirstOrDefault(o => o.ID == sr.TDL_KSK_CONTRACT_ID);
                        }

                        result.Add(data);
                    }
                }
            }
            return result;
        }
    }
}
