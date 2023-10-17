using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisServiceReq.Test.LisIntegrateThread;
using MOS.ApiConsumerManager;
using MOS.UTILITY;
using MOS.TDO;
using MOS.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.SendResultToLabconn
{
    class LisLabconnProcessor : BusinessBase
    {
        private List<HIS_SERE_SERV> sereServs;

        internal LisLabconnProcessor()
            : base()
        {
        }

        internal LisLabconnProcessor(CommonParam param)
            : base(param)
        {
        }

        public bool ProcessDataToLabconn(List<HIS_SERVICE_REQ> serviceReqs)
        {
            bool result = false;
            if (IsNotNullOrEmpty(serviceReqs))
            {
                List<long> serviceReqIds = serviceReqs.Select(o => o.ID).ToList();

                //các y lệnh có trạng thái xóa sẽ lấy tất cả y lệnh cùng lượt để cập nhật số lượng dịch vụ của lượt đó
                List<HIS_SERVICE_REQ> deleteReq = serviceReqs.Where(o => o.IS_DELETE == Constant.IS_TRUE).ToList();
                if (IsNotNullOrEmpty(deleteReq))
                {
                    List<string> turnCodes = deleteReq.Select(s => s.ASSIGN_TURN_CODE).Distinct().ToList();

                    List<string> listCodeSql = new List<string>();
                    int skip = 0;
                    while (turnCodes.Count - skip > 0)
                    {
                        List<string> ids = turnCodes.Skip(skip).Take(100).ToList();
                        skip += 100;

                        listCodeSql.Add(string.Format("('{0}')", string.Join("','", ids)));
                    }

                    string sampleTurnSql = string.Format("SELECT * FROM HIS_SERVICE_REQ WHERE 1 = 1 AND ASSIGN_TURN_CODE IN  {0}", string.Join(" OR ASSIGN_TURN_CODE IN ", listCodeSql));
                    LogSystem.Info(LogUtil.TraceData("sampleTurnSql:", serviceReqIds));
                    List<HIS_SERVICE_REQ> sampleTurnReq = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ>(sampleTurnSql);
                    if (IsNotNullOrEmpty(sampleTurnReq))
                    {
                        //lọc trùng với y lệnh đang xử lý.
                        sampleTurnReq = sampleTurnReq.Where(o => !serviceReqIds.Exists(e => e == o.ID)).ToList();
                        if (IsNotNullOrEmpty(sampleTurnReq))
                        {
                            serviceReqs.AddRange(sampleTurnReq);
                        }
                    }
                }

                serviceReqIds = serviceReqs.Select(o => o.ID).ToList();
                LogSystem.Info("filter INTRUCTION_DATE_FROM: " + Convert.ToInt64(DateTime.Now.AddDays(-HisServiceReqCFG.INTEGRATE_SYSTEM_DAY_NUM_SYNC).ToString("yyyyMMdd") + "000000").ToString());
                LogSystem.Info("so ngay de lay dich vu truoc do: " + HisServiceReqCFG.INTEGRATE_SYSTEM_DAY_NUM_SYNC.ToString());
                LogSystem.Info(LogUtil.TraceData("Data serviceReqIds query by exe_room_code send to Labconn:", serviceReqIds));
                if (serviceReqIds != null && serviceReqIds.Count > 0)
                {
                    HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                    ssFilter.HAS_EXECUTE = true;
                    ssFilter.SERVICE_REQ_IDs = serviceReqIds;
                    ssFilter.IS_INCLUDE_DELETED = true; // Include deleted data
                    this.sereServs = new HisSereServGet().Get(ssFilter);
                    LogSystem.Info(LogUtil.TraceData("Data sereServs query by serviceReqIds send to Labconn:", sereServs));
                }
                // Get data to check if can sent to Lis
                List<long> treatmentIds = sereServs != null ? sereServs.Where(o => o.TDL_TREATMENT_ID.HasValue).Select(o => o.TDL_TREATMENT_ID.Value).Distinct().ToList() : null;
                List<V_HIS_TREATMENT_FEE_1> treatments = new HisTreatmentGet().GetFeeView1ByIds(treatmentIds);

                List<long> sereServIds = sereServs != null ? sereServs.Select(o => o.ID).ToList() : null;
                List<HIS_SERE_SERV_BILL> bills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                List<HIS_SERE_SERV_DEPOSIT> deposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServIds);
                List<HIS_SERE_SERV_DEBT> depts = new HisSereServDebtGet().GetNoCancelBySereServIds(sereServIds);
                List<long> sereServDepositIds = deposits != null ? deposits.Select(o => o.ID).ToList() : null;
                List<HIS_SESE_DEPO_REPAY> repays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(sereServDepositIds);
                List<HIS_SERVICE_REQ> serviceReqsToLabconn = new List<HIS_SERVICE_REQ>();
                foreach (HIS_SERVICE_REQ sr in serviceReqs)
                {
                    V_HIS_TREATMENT_FEE_1 treat = treatments != null ? treatments.Where(o => o.ID == sr.TREATMENT_ID).FirstOrDefault() : null;
                    List<HIS_SERE_SERV> ss = sereServs != null ? sereServs.Where(o => o.SERVICE_REQ_ID == sr.ID).ToList() : null;
                    if (treat != null && ss != null && ss.Count > 0)
                    {
                        //Kiem tra xem co cho phep gui sang LIS ko
                        if (SendIntegratorCheck.IsAllowSend(treat, sr, ss, bills, deposits, repays, depts))
                        {
                            serviceReqsToLabconn.Add(sr);
                        }
                    }
                    else if (sr.IS_DELETE == Constant.IS_TRUE && !IsNotNullOrEmpty(ss))
                    {
                        LogSystem.Info("Truong hop serviceReq da xoa va khong ton tai dich vu");
                        serviceReqsToLabconn.Add(sr);
                    }
                }
                LogSystem.Info(LogUtil.TraceData("Data serviceReqsToLabconn after check is allow send to Labconn:", serviceReqsToLabconn));

                // Send data to API Lis Labconn
                var data = serviceReqsToLabconn != null && serviceReqsToLabconn.Count > 0 ? serviceReqsToLabconn.GroupBy(o => new { o.TDL_PATIENT_CODE, o.ASSIGN_TURN_CODE }) : null;
                LogSystem.Info(LogUtil.TraceData("data after group by TDL_PATIENT_CODE ASSIGN_TURN_CODE:", data));
                if (data != null)
                {
                    List<long> reqIds = new List<long>();
                    List<long> sereservIds = new List<long>();
                    foreach (var item in data)
                    {
                        LisLabconnSenderTDO tdo = new LisLabconnSenderTDO();
                        tdo.MaHS = item.First().ASSIGN_TURN_CODE;
                        tdo.MaBN = item.First().TDL_PATIENT_CODE;
                        tdo.HoTen = item.First().TDL_PATIENT_NAME;
                        tdo.NamSinh = long.Parse(item.First().TDL_PATIENT_DOB.ToString().Substring(0, 4));
                        tdo.NgayChiDinh = (item.First().INTRUCTION_DATE / 1000000).ToString();
                        if (item.First().TDL_PATIENT_GENDER_ID.HasValue && item.First().TDL_PATIENT_GENDER_ID.Value == 1)
                        {
                            tdo.GioiTinh = "F";
                        }
                        else if (item.First().TDL_PATIENT_GENDER_ID.HasValue && item.First().TDL_PATIENT_GENDER_ID.Value == 2)
                        {
                            tdo.GioiTinh = "M";
                        }
                        else
                        {
                            tdo.GioiTinh = "O";
                        }

                        var services = sereServs != null ? sereServs.Where(o => item.Select(s => s.ID).ToList().Contains(o.SERVICE_REQ_ID ?? 0) && o.IS_DELETE != Constant.IS_TRUE).ToList() : null;
                        if (services != null && services.Count > 0)
                        {
                            tdo.SLDichVu = services.Count;
                        }

                        LogSystem.Info(LogUtil.TraceData("Data tdo send to Labconn:", tdo));
                        V_HIS_EXECUTE_ROOM executeRooom = HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ROOM_ID == item.First().EXECUTE_ROOM_ID);
                        var urlAddress = executeRooom != null ? LisCFG.LIS_ADDRESSES.FirstOrDefault(o => o.RoomCode == executeRooom.EXECUTE_ROOM_CODE) : null;
                        LogSystem.Info(LogUtil.TraceData("Data url server send to Labconn:", urlAddress));
                        if (urlAddress != null && !string.IsNullOrWhiteSpace(urlAddress.Url))
                        {
                            if (new SendDataToLabconnProcessor().SendToLabconn(tdo, urlAddress.Url))
                            {
                                LogSystem.Info("Gui sang labconn thanh cong");
                                reqIds.AddRange(item.Select(o => o.ID).ToList());
                                sereservIds.AddRange(services.Select(o => o.ID).ToList());
                            }
                        }
                        else
                        {
                            LogSystem.Error("Gui yeu cau sang LisLabconn khong lay duoc thong tin dia chi server");
                        }
                    }

                    // Update status for serviceReq and sereServ
                    if (new UpdateStatusProcessor().Run(reqIds, sereservIds))
                    {
                        LogSystem.Info("Gui yeu cau sang LibLabconn thanh cong va cap nhat trang thai serviceReqs and sereServ thanh cong");
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
