using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisMachine;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisServiceReq.Common.Update.Status;
using MOS.MANAGER.HisServiceReq.Pacs;
using MOS.MANAGER.HisServiceReq.Test.LisIntegrateThread;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor;
using MOS.MANAGER.HisServiceReq.Test.LisSenderV1;
using MOS.MANAGER.HisServiceReq.Test.SendResultToLabconn;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.TDO;
using MOS.UTILITY;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using MOS.MANAGER.HisTreatment.Util;
using MOS.MANAGER.HisBranch;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.NmsNotification;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisExecuteRoleUser;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisEkip;

namespace MOS.MANAGER.HisServiceReq.Update.Status
{
    partial class HisServiceReqUpdateStart : BusinessBase
    {
        private HIS_SERVICE_REQ before = null;
        private HisEkipUserTruncate hisEkipUserTruncate;
        private HisEkipTruncate hisEkipTruncate;
        private HisSereServUpdate hisSereServUpdate;
        private HisServiceReqUpdateStartProcessorEkip hisServiceReqUpdateStartProcessorEkip;

        class ThreadExportXmlData
        {
            public HIS_TREATMENT Treatment { get; set; }
            public HIS_BRANCH Branch { get; set; }
            public HIS_SERVICE_REQ ServiceReq { get; set; }
            public HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }
        }

        internal HisServiceReqUpdateStart()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqUpdateStart(CommonParam param)
            : base(param)
        {
            this.Init();
        }
        private void Init()
        {
            this.hisEkipTruncate = new HisEkipTruncate(param);
            this.hisEkipUserTruncate = new HisEkipUserTruncate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisServiceReqUpdateStartProcessorEkip = new HisServiceReqUpdateStartProcessorEkip(param);
        }

        /// <summary>
        /// Bat dau xu ly dich vu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Start(long id, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(id);
                result = this.Start(serviceReq, true, ref resultData, null, null);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Bat dau xu ly dich vu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool StartWithTime(HIS_SERVICE_REQ data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                HIS_SERVICE_REQ raw = new HisServiceReqGet().GetById(data.ID);
                result = this.Start(raw, true, ref resultData, data.EXECUTE_LOGINNAME, data.EXECUTE_USERNAME, data.START_TIME);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Bat dau xu ly dich vu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Start(string code, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                string serviceReqCode = String.Format("{0:000000000000}", int.Parse(code));
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.SERVICE_REQ_CODE__EXACT = serviceReqCode;
                List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);

                if (!IsNotNullOrEmpty(serviceReqs))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    return false;
                }
                result = this.Start(serviceReqs[0], true, ref resultData, null, null);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Bat dau xu ly dich vu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Start(HIS_SERVICE_REQ serviceReq, bool mustVerifyStart, ref HIS_SERVICE_REQ resultData, string loginname, string username, long? startTime = null)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                before = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                bool valid = true;
                if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL && serviceReq.START_TIME != startTime)
                {
                    valid = true;
                }
                else
                {
                    valid = !mustVerifyStart || checker.IsAllowedForStart(serviceReq);
                }
                if (valid)
                {
                    if (startTime.HasValue)
                    {
                        serviceReq.START_TIME = startTime.Value;
                    }
                    else
                    {
                        serviceReq.START_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now);
                    }

                    serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL;
                    
                    if (String.IsNullOrWhiteSpace(loginname) && !string.IsNullOrWhiteSpace(Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName()))
                    {
                        serviceReq.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        serviceReq.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        serviceReq.EXECUTE_USER_TITLE = HisEmployeeUtil.GetTitle(serviceReq.EXECUTE_LOGINNAME);
                        WorkPlaceSDO workPlaceSdo = TokenManager.GetWorkPlace(serviceReq.EXECUTE_ROOM_ID);
                        serviceReq.EXE_DESK_ID = workPlaceSdo != null ? workPlaceSdo.DeskId : null;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(username))
                        {
                            serviceReq.EXECUTE_USERNAME = username;
                        }
                        if (!string.IsNullOrWhiteSpace(loginname))
                        {
                            serviceReq.EXECUTE_LOGINNAME = loginname;
                            serviceReq.EXECUTE_USER_TITLE = HisEmployeeUtil.GetTitle(loginname);
                        }
                    }
                    serviceReq.EXE_WORKING_SHIFT_ID = TokenManager.GetWorkingShift();

                    if (this.ProcessStartUnstart(serviceReq, ref resultData))
                    {
                        result = true;
                        //Tao thread moi de gui du lieu tich hop
                        this.InitThreadSendAssign(resultData);

                        var executeRoomId = resultData.EXECUTE_ROOM_ID;
                        if (TheVietCFG.NUM_ORDER_BEFORE_NOTIFY_EXAM > 0 && HisExecuteRoomCFG.DATA.Exists(t => t.ROOM_ID == executeRoomId && t.IS_EXAM == Constant.IS_TRUE))
                        {
                            //Tao thread moi de gui thong bao NMS
                            this.InitThreadSendNotification(resultData);
                        }
                        if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                        {
                            //Tao thread moi de Tự động xuất XML checkin khi xử lý khám
                            this.IntegrateThreadInit(serviceReq);
                        }

                        if (!this.hisServiceReqUpdateStartProcessorEkip.Run(serviceReq))
                        {
                            LogSystem.Debug("Goi ham cap nhat hisServiceReqUpdateStartProcessorEkip that bai");
                        }

                        new EventLogGenerator(EventLog.Enum.HisServiceReq_BatDauXuLy)
                            .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                            .ServiceReqCode(serviceReq.SERVICE_REQ_CODE).Run();
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

        internal bool Unstart(long id)
        {
            HIS_SERVICE_REQ resultData = new HIS_SERVICE_REQ();
            return this.Unstart(id, ref resultData);
        }

        /// <summary>
        /// Huy Bat dau xu ly dich vu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Unstart(long id, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                HIS_SERVICE_REQ raw = null;
                HIS_TREATMENT treatment = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqStatusCheck startusChecker = new HisServiceReqStatusCheck(param);

                bool valid = checker.VerifyId(id, ref raw);
                valid = valid && checker.IsNotAprovedSurgeryRemuneration(raw);
                valid = valid && this.IsNotFinished(raw);
                valid = valid && treatmentChecker.VerifyId(raw.TREATMENT_ID, ref treatment);

                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    before = Mapper.Map<HIS_SERVICE_REQ>(raw);

                    raw.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;

                    valid = valid && startusChecker.IsValidSrTypeCodeAndExeServiceModuleId(raw);
                    if (valid)
                    {
                        List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(raw.ID);
                        if (IsNotNullOrEmpty(sereServs))
                        {
                            List<HIS_SERE_SERV> sereServUpdates = new List<HIS_SERE_SERV>();
                            List<HIS_SERE_SERV> SereServbefores = new List<HIS_SERE_SERV>();
                            List<HIS_EKIP> ekipDeletes = new List<HIS_EKIP>();
                            foreach (var ss in sereServs)
                            {
                                if (ss.EKIP_ID.HasValue)
                                {
                                    HIS_EKIP ekip = new HisEkipGet().GetById(ss.EKIP_ID.Value);
                                    if (ekip != null)
                                    {
                                        ekipDeletes.Add(ekip);
                                    }
                                    List<HIS_EKIP_USER> ekUserDelertes = new HisEkipUserGet().GetByEkipId(ss.EKIP_ID.Value);
                                    if (IsNotNullOrEmpty(ekUserDelertes) && !this.hisEkipUserTruncate.TruncateList(ekUserDelertes))
                                    {
                                        throw new Exception("Xoa du lieu List<HIS_EKIP_USER> ekUserDelertes that bai");
                                    }

                                    HIS_SERE_SERV ssbefore = Mapper.Map<HIS_SERE_SERV>(ss);
                                    SereServbefores.Add(ssbefore);
                                    ss.EKIP_ID = null;
                                    sereServUpdates.Add(ss);
                                }
                            }
                            if (IsNotNullOrEmpty(sereServUpdates) && IsNotNullOrEmpty(SereServbefores))
                            {
                                if (!this.hisSereServUpdate.UpdateList(sereServUpdates, SereServbefores, false))
                                {
                                    throw new Exception("Cap nhat sere_serv that bai");
                                }
                            }
                            if (IsNotNullOrEmpty(ekipDeletes))
                            {
                                if (!this.hisEkipTruncate.TruncateList(ekipDeletes))
                                {
                                    throw new Exception("Xoa du lieu List<HIS_EKIP> ekipDeletes that bai");
                                }
                            }
                        }
                    }
                    raw.START_TIME = null;
                    raw.EXECUTE_USERNAME = null;
                    raw.EXECUTE_LOGINNAME = null;
                    raw.EXE_WORKING_SHIFT_ID = null;
                    raw.EXE_DESK_ID = null;

                    if (this.ProcessStartUnstart(raw, ref resultData))
                    {
                        result = true;
                        //Tao thread moi de gui du lieu tich hop
                        this.InitThreadSendAssign(resultData);

                        new EventLogGenerator(EventLog.Enum.HisServiceReq_HuyBatDau)
                            .TreatmentCode(raw.TDL_TREATMENT_CODE)
                            .ServiceReqCode(raw.SERVICE_REQ_CODE).Run();
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

        private bool ProcessStartUnstart(HIS_SERVICE_REQ raw, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            ///Neu la phieu chi dinh la xet nghiem thi ko can kiem tra ho so dieu tri van cho bat dau
            ///(do thuc te, BN se lam xet nghiem, va cho ket thuc ho so dieu tri luon, nhung ngay hom sau moi co ket qua)
            bool valid = true;

            bool verifyTreatment = HisServiceReqStatusCheck.IsNeedToVerifyTreatment(raw);

            if (verifyTreatment)
            {
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HIS_TREATMENT hisTreatment = null;
                valid = valid && treatmentChecker.IsUnLock(raw.TREATMENT_ID, ref hisTreatment);
                valid = valid && treatmentChecker.IsUnpause(hisTreatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);
            }

            if (valid)
            {
                string sql = "UPDATE HIS_SERVICE_REQ SET START_TIME = :param1, SERVICE_REQ_STT_ID = :param2, EXECUTE_USERNAME = :param3, EXECUTE_LOGINNAME = :param4, EXE_WORKING_SHIFT_ID = :param5, EXE_DESK_ID = :param6, SAMPLE_TIME = :param7, EXECUTE_USER_TITLE = :param8 WHERE ID = :param9";
                if (!DAOWorker.SqlDAO.Execute(sql, raw.START_TIME, raw.SERVICE_REQ_STT_ID, raw.EXECUTE_USERNAME, raw.EXECUTE_LOGINNAME, raw.EXE_WORKING_SHIFT_ID, raw.EXE_DESK_ID, raw.SAMPLE_TIME, raw.EXECUTE_USER_TITLE, raw.ID))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                    throw new Exception("Cap nhat thong tin HisServiceReq that bai." + LogUtil.TraceData("raw", raw));
                }
                resultData = raw;
                result = true;
            }
            return result;
        }

        private bool IsNotFinished(HIS_SERVICE_REQ raw)
        {
            try
            {
                if (raw.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoHuyBatDauKhiChuaKetThuc);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private void InitThreadSendNotification(HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(this.SendNotification));
                thread.Priority = ThreadPriority.BelowNormal;
                thread.Start(serviceReq);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void SendNotification(object data)
        {
            try
            {
                HIS_SERVICE_REQ serviceReq = (HIS_SERVICE_REQ)data;
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                filter.EXECUTE_ROOM_ID = serviceReq.EXECUTE_ROOM_ID;
                filter.INTRUCTION_DATE__EQUAL = serviceReq.INTRUCTION_DATE;
                var nextServiceReqs = new HisServiceReqGet().Get(filter);
                if (!IsNotNullOrEmpty(nextServiceReqs)) return;
                HIS_SERVICE_REQ serviceReqToNotify = nextServiceReqs.Where(o => o.NUM_ORDER >= ((serviceReq.NUM_ORDER ?? 0) + TheVietCFG.NUM_ORDER_BEFORE_NOTIFY_EXAM.Value)).OrderBy(o => o.NUM_ORDER).FirstOrDefault();
                if (serviceReqToNotify != null && !String.IsNullOrWhiteSpace(serviceReqToNotify.TDL_PATIENT_PHONE))
                {
                    string content = String.Format(MOS.MANAGER.Base.MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisServiceReq_BacSyDaGoiDenSo_VuiLongCoMatTruocPhongKham_DeChuanBiVaoKham, param.LanguageCode), serviceReq.NUM_ORDER ?? 0, HisExecuteRoomCFG.DATA.First(t => t.ROOM_ID == serviceReq.EXECUTE_ROOM_ID && t.IS_EXAM == Constant.IS_TRUE).EXECUTE_ROOM_NAME);
                    string phoneNumber = serviceReqToNotify.TDL_PATIENT_PHONE;
                    var category = NmsNotificationSend.Category.VAO_KHAM;
                    bool result = new NmsNotificationSend(new CommonParam()).SendByIdentifierInfo(content, "", phoneNumber, category);
                    if (result)
                    {
                        Inventec.Common.Logging.LogSystem.Info(String.Format("Thanh cong gui thong bao den SDT:{0}.", phoneNumber));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void InitThreadSendAssign(HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(this.SendAssign));
                thread.Priority = ThreadPriority.BelowNormal;
                thread.Start(serviceReq);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void SendAssign(object data)
        {
            try
            {
                HIS_SERVICE_REQ serviceReq = (HIS_SERVICE_REQ)data;
                new HisServiceReqSendAssignProcesser().Run(serviceReq);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void IntegrateThreadInit(HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                Thread threadXml = new Thread(new ParameterizedThreadStart(this.ProcessCreateXml));
                threadXml.Priority = ThreadPriority.Lowest;
                threadXml.Start(serviceReq);
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh gui thong tin service_req sang HMS", ex);
            }
        }

        private void ProcessCreateXml(object data)
        {
            HIS_SERVICE_REQ serviceReq = (HIS_SERVICE_REQ)data;
            List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(serviceReq.ID);
            if (IsNotNullOrEmpty(sereServs))
            {
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(serviceReq.TREATMENT_ID);
                if (treatment != null)
                {
                    HIS_PATIENT_TYPE_ALTER pta = new HisPatientTypeAlterGet().GetLastByTreatmentId(treatment.ID);
                    HIS_BRANCH branch = new HisBranchGet().GetById(treatment.BRANCH_ID);

                    new HisTreatmentAutoExportXmlProcessor().Run(treatment, branch, pta, serviceReq);
                }
            }
        }

        public void RollbackData()
        {
            if (this.before != null)
            {
                if (!DAOWorker.HisServiceReqDAO.Update(this.before))
                {
                    LogSystem.Warn("Bat dau y lenh that bai. Rollback that bai");
                }
            }
            this.hisServiceReqUpdateStartProcessorEkip.RollbackData();
        }
    }
}
