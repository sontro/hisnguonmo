using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisMachine;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisServiceReq.Pacs;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor;
using MOS.MANAGER.HisServiceReq.Test.LisSenderV1;
using MOS.MANAGER.HisServiceReq.Test.SendResultToLabconn;
using MOS.MANAGER.HisServiceReq.Test.LisIntegrateThread;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.TDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using MOS.MANAGER.Config.CFG;

namespace MOS.MANAGER.HisServiceReq
{
    /// <summary>
    /// Xu ly nghiep vu nguoi dung chu dong gui chi dinh sang LIS/PACS
    /// </summary>
    partial class HisServiceReqRequestOrder : BusinessBase
    {
        internal HisServiceReqRequestOrder()
            : base()
        {

        }

        internal HisServiceReqRequestOrder(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        /// <summary>
        /// Gui lai y/c XN/PACS sang he thong LIS tich hop
        /// </summary>
        /// <param name="serviceReqId"></param>
        /// <returns></returns>
        internal bool Run(long serviceReqId)
        {
            bool result = false;
            try
            {
                HIS_SERVICE_REQ sr = null;
                List<HIS_SERE_SERV> ss = null;

                if (this.IsAllowToSend(serviceReqId, ref sr, ref ss))
                {
                    List<string> sqls = new List<string>();

                    //Neu la xet nghiem
                    if (sr.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
                    {
                        if (Lis2CFG.LIS_INTEGRATION_VERSION == Lis2CFG.LisIntegrationVersion.V2)
                        {
                            OrderData data = new OrderData();
                            data.ServiceReq = sr;
                            data.Availables = ss;
                            data.Inserts = ss;//coi nhu la gui lan dau
                            ILisProcessor sender = LisFactory.GetProcessor(param);
                            List<string> messages = null;

                            if (Lis2CFG.LIS_INTEGRATION_TYPE == Lis2CFG.LisIntegrationType.LABCONN) // Neu sender bang null thi loai tien trinh gui sang lis la Labconn
                            {
                                LogSystem.Info("Batdau goi api send to Labconn");
                                List<long> executeRoomIds = DataPrepare.GetExecuteRoomId();
                                LogSystem.Info(LogUtil.TraceData("thong tin phong xu ly executeRoomIds:", executeRoomIds));
                                if (IsNotNullOrEmpty(executeRoomIds) && executeRoomIds.Contains(data.ServiceReq.EXECUTE_ROOM_ID))
                                {
                                    if (new LisLabconnProcessor().ProcessDataToLabconn(new List<HIS_SERVICE_REQ>() { data.ServiceReq }))
                                    {
                                        result = true;
                                    }
                                    else
                                    {
                                        LogSystem.Warn("ProcessDataToLabconn that bai");
                                    }
                                }
                                else
                                {
                                    LogSystem.Warn("Y lenh hien tai khong thuoc phong xu ly trong cau hinh dia chi Lis");
                                }
                                LogSystem.Info("Ketthuc goi api send to Labconn");
                            }
                            else if (sender != null)  // Khi sender khong null thi tuc la loai tien trinh gui sang lis là Inventec hoac Roche
                            {
                                result = sender.RequestOrder(data, ref messages);
                                if (result)
                                {
                                    if (!new UpdateOrderStatus(param).UpdateSentOrder(new List<OrderData>() { data }))
                                    {
                                        LogSystem.Warn("Cap nhat trang thai 'Da gui sang LIS' that bai");
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS)
                            {
                                result = (new LisUtil(param)).SendOrderToLis(serviceReqId, ss, ref sqls);
                                if (result && IsNotNullOrEmpty(sqls))
                                {
                                    if (!DAOWorker.SqlDAO.Execute(sqls))
                                    {
                                        LogSystem.Warn("Cap nhat trang thai 'Da gui sang LIS/PACS' that bai");
                                    }
                                }
                            }
                        }
                    }
                    //Neu ko phai la XN thi la PACS
                    else
                    {
                        PacsOrderData data = new PacsOrderData();
                        data.ServiceReq = sr;
                        data.Availables = ss;
                        data.Inserts = ss;//coi nhu la gui lan dau
                        data.Treatment = sr != null ? new HisTreatmentGet().GetById(sr.TREATMENT_ID) : null;

                        IPacsProcessor processor = PacsFactory.GetProcessor(param);
                        result = processor.SendOrder(data, ref sqls);
                        if (result)
                        {
                            processor.UpdateStatus(new List<PacsOrderData>() { data }, sqls);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool IsAllowToSend(long serviceReqId, ref HIS_SERVICE_REQ sr, ref List<HIS_SERE_SERV> ss)
        {
            try
            {
                //Lay du lieu de check
                sr = new HisServiceReqGet().GetById(serviceReqId);
                V_HIS_TREATMENT_FEE_1 treatment = sr != null ? new HisTreatmentGet().GetFeeView1ById(sr.TREATMENT_ID) : null;
                ss = sr != null ? new HisSereServGet().GetByServiceReqId(sr.ID) : null;
                List<long> sereServIds = sr != null ? ss.Select(o => o.ID).ToList() : null;

                List<HIS_SERE_SERV_BILL> bills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                List<HIS_SERE_SERV_DEPOSIT> deposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServIds);
                List<long> sereServDepositIds = deposits != null ? deposits.Select(o => o.ID).ToList() : null;
                List<HIS_SESE_DEPO_REPAY> repays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(sereServDepositIds);
                List<HIS_SERE_SERV_DEBT> debt = new HisSereServDebtGet().GetNoCancelBySereServIds(sereServIds);

                List<long> integrateTypeIds = new List<long>(){
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__NS,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__SA,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__CDHA,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TDCN,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PHCN,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KHAC
                };

                if (sr == null || !integrateTypeIds.Contains(sr.SERVICE_REQ_TYPE_ID))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("du lieu ko ton tai hoac ko phai loai la XN");
                }
                if (IsNotNullOrEmpty(ss) && treatment != null && !SendIntegratorCheck.IsAllowSend(treatment, sr, ss, bills, deposits, repays, debt))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepGuiSangHeThongTichHop);
                    return false;
                }

                if (IntegratedSystemCFG.INTRUCTION_TIME_MUST_BE_LESS_THAN_SYSTEM_TIME_OPTION)
                {
                    long thoiGianHtai = Inventec.Common.DateTime.Get.Now() ?? 0;
                    if (sr.INTRUCTION_TIME > thoiGianHtai)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYLenhLonHonThoiGianHienTai);
                        return false;
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
