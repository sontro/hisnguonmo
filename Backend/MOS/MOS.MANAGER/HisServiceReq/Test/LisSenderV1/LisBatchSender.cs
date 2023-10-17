using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Test.LisSenderV1;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisSenderV1
{
    //Phuc vu gui du lieu sang he thong LIS theo tien trinh
    public class LisBatchSender : BusinessBase
    {
        private static bool isSending = false;

        public static void Send()
        {
            try
            {
                if (isSending)
                {
                    LogSystem.Info("Tien trinh Gui du lieu sang LIS truoc chua ket thuc. Khong start tien trinh moi");
                    return;
                }

                isSending = true;

                List<long> executeRoomIds = null;
                if (!CheckIsSend(ref executeRoomIds))
                {
                    isSending = false;
                    return;
                }

                long intructionDateFrom = Convert.ToInt64(DateTime.Now.AddDays(-HisServiceReqCFG.INTEGRATE_SYSTEM_DAY_NUM_SYNC).ToString("yyyyMMdd") + "000000");
                long thoiGianHtai = Inventec.Common.DateTime.Get.Now() ?? 0;
                List<HIS_SERVICE_REQ> tests = null;
                if (LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS)
                {
                    StringBuilder sb = new StringBuilder().Append("SELECT * FROM HIS_SERVICE_REQ WHERE")
                    .Append(" SERVICE_REQ_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
                    .Append(" AND SERVICE_REQ_STT_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                    .Append(" AND ((IS_NO_EXECUTE IS NULL AND LIS_STT_ID IS NULL)")
                    .Append(" OR (LIS_STT_ID = ").Append(LisUtil.LIS_STT_ID__UPDATE).Append("))")
                    .Append(" AND EXECUTE_ROOM_ID IN (").Append(String.Join(",", executeRoomIds)).Append(")")
                    .Append(" AND INTRUCTION_DATE >= ").Append(intructionDateFrom);
                    if (IntegratedSystemCFG.INTRUCTION_TIME_MUST_BE_LESS_THAN_SYSTEM_TIME_OPTION)
                    {
                        sb.Append(" AND INTRUCTION_TIME <= ").Append(thoiGianHtai);
                    }
                    string sqlQuery = sb.ToString();
                    tests = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ>(sqlQuery);
                }
                else
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.IS_SEND_LIS = false;
                    filter.HAS_EXECUTE = true;
                    filter.EXECUTE_ROOM_IDs = executeRoomIds;
                    filter.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                    filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN;
                    filter.INTRUCTION_DATE_FROM = intructionDateFrom;
                    if (IntegratedSystemCFG.INTRUCTION_TIME_MUST_BE_LESS_THAN_SYSTEM_TIME_OPTION)
                    {
                        filter.INTRUCTION_TIME_TO = thoiGianHtai;
                    }
                    tests = new HisServiceReqGet().Get(filter);
                }

                if (tests == null || tests.Count <= 0)
                {
                    isSending = false;
                    return;
                }
                List<V_HIS_TREATMENT_FEE_1> treatmentFees = null;
                List<HIS_KSK_CONTRACT> kskContracts = null;
                List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters = null;

                List<long> treatmentIds = tests.Select(o => o.TREATMENT_ID).Distinct().ToList();

                treatmentFees = new HisTreatmentGet().GetFeeView1ByIds(treatmentIds);

                List<long> kskContractIds = treatmentFees != null ? treatmentFees.Where(o => o.TDL_KSK_CONTRACT_ID.HasValue).Select(s => s.TDL_KSK_CONTRACT_ID.Value).Distinct().ToList() : null;
                if (kskContractIds != null && kskContractIds.Count > 0)
                {
                    kskContracts = new HisKskContractGet().GetByIds(kskContractIds);
                }

                //Neu he thong co cau hinh khong cho phep lay cac XN chua du vien phi
                if (LisCFG.LIS_FORBID_NOT_ENOUGH_FEE)
                {
                    patientTypeAlters = new HisPatientTypeAlterGet().GetByTreatmentIds(treatmentIds);
                }

                HisPatientTypeAlterGet patientTypeAlterGet = new HisPatientTypeAlterGet();
                LisBatchSenderCheck checker = new LisBatchSenderCheck();

                List<HIS_SERVICE_REQ> listToSends = new List<HIS_SERVICE_REQ>();
                foreach (HIS_SERVICE_REQ sr in tests)
                {
                    if (sr.IS_NO_EXECUTE.HasValue && sr.IS_NO_EXECUTE.Value == Constant.IS_TRUE)
                    {
                        listToSends.Add(sr);
                    }
                    else if (!LisCFG.LIS_FORBID_NOT_ENOUGH_FEE || checker.IsAllowedForStart(sr, patientTypeAlters, treatmentFees))
                    {
                        listToSends.Add(sr);
                    }
                }

                if (listToSends.Count == 0)
                {
                    isSending = false;
                    return;
                }

                List<long> patientIds = listToSends.Select(o => o.TDL_PATIENT_ID).Distinct().ToList();
                List<long> srIds = listToSends.Select(o => o.ID).ToList();

                HisSereServFilterQuery sereServFilter = new HisSereServFilterQuery();
                sereServFilter.SERVICE_REQ_IDs = srIds;
                sereServFilter.HAS_EXECUTE = true;
                List<HIS_SERE_SERV> sereServs = new HisSereServGet().Get(sereServFilter);

                if (LisCFG.LIS_INTEGRATE_OPTION != (int)LisCFG.LisIntegrateOption.LIS && (sereServs == null || sereServs.Count == 0))
                {
                    isSending = false;
                    return;
                }

                List<HIS_SERVICE_REQ> successList = new List<HIS_SERVICE_REQ>();
                List<string> sqls = new List<string>();
                foreach (HIS_SERVICE_REQ sr in listToSends)
                {
                    if (sr.IS_NO_EXECUTE.HasValue && sr.IS_NO_EXECUTE.Value == Constant.IS_TRUE)
                    {
                        if (LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS)
                        {
                            new LisUtil().SendDeleteOrderToLis(sr, ref sqls);
                        }
                    }
                    else
                    {
                        List<HIS_SERE_SERV> ss = sereServs.Where(o => o.SERVICE_REQ_ID == sr.ID).ToList();
                        if (ss != null && ss.Count > 0)
                        {
                            bool success = false;
                            if (LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS)
                            {
                                V_HIS_TREATMENT_FEE_1 fee = treatmentFees.FirstOrDefault(o => o.ID == sr.TREATMENT_ID);
                                HIS_KSK_CONTRACT contract = null;
                                if (fee.TDL_KSK_CONTRACT_ID.HasValue)
                                {
                                    contract = kskContracts != null ? kskContracts.FirstOrDefault(o => o.ID == fee.TDL_KSK_CONTRACT_ID.Value) : null;
                                }
                                new LisUtil().SendOrderToLis(sr, ss, contract, ref sqls);
                            }
                            if (success)
                            {

                                successList.Add(sr);
                            }
                        }
                    }
                }

                if (successList.Count > 0)
                {
                    successList.ForEach(o =>
                    {
                        if (o.LIS_STT_ID != 0)
                        {
                            o.LIS_STT_ID = 1;
                            o.IS_SENT_EXT = Constant.IS_TRUE;
                        }
                    });
                    if (!DAOWorker.HisServiceReqDAO.UpdateList(successList))
                    {
                        LogSystem.Warn("Cap nhat trang thai da gui sang LIS that bai");
                    }
                }
                else if (sqls.Count > 0)
                {
                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        LogSystem.Warn("Cap nhat trang thai da gui sang LIS that bai");
                    }
                }

                isSending = false;
            }
            catch (Exception ex)
            {
                isSending = false;
                LogSystem.Error(ex);
            }
        }

        private static bool CheckIsSend(ref List<long> executeRoomIds)
        {
            bool valid = true;

            try
            {
                if (LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS
                    || LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.ROCHE_TCP_IP)
                {
                    if (LisCFG.LIS_ADDRESSES == null || LisCFG.LIS_ADDRESSES.Count <= 0)
                    {
                        LogSystem.Warn("Chua cau hinh dia chi server LIS (KeyConfig: MOS.LIS.ADDRESS)");
                        valid = false;
                    }
                    if (valid)
                    {
                        List<string> roomCodes = LisCFG.LIS_ADDRESSES.Select(o => o.RoomCode).ToList();
                        List<long> roomIds = HisExecuteRoomCFG.DATA
                        .Where(o => roomCodes.Contains(o.EXECUTE_ROOM_CODE)).Select(s => s.ROOM_ID).ToList();
                        executeRoomIds = roomIds;
                    }
                }
                else
                {
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }

            return valid;
        }
    }
}
