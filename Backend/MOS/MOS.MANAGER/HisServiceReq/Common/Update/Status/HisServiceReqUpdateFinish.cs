using Inventec.Common.Logging;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReq.Common.Update.Status;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisServiceReqType;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.NmsNotification;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MOS.MANAGER.HisServiceReq.Update.Finish
{
    partial class HisServiceReqUpdateFinish : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisServiceReqUpdateFinish()
            : base()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal HisServiceReqUpdateFinish(CommonParam param)
            : base(param)
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool FinishWithTime(HIS_SERVICE_REQ data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE_REQ raw = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsValidAccount();
                if (valid)
                {
                    if (data.START_TIME.HasValue)
                        raw.START_TIME = data.START_TIME;
                    raw.FINISH_TIME = data.FINISH_TIME;
                    if (this.CheckFinishTime(raw))
                    {
                        bool verifyTreatment = HisServiceReqStatusCheck.IsNeedToVerifyTreatment(raw);
                        result = this.Finish(raw, verifyTreatment, ref resultData, data.EXECUTE_LOGINNAME, data.EXECUTE_USERNAME);
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

        internal bool Finish(long id, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE_REQ raw = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsValidAccount();
                if (valid)
                {
                    bool verifyTreatment = HisServiceReqStatusCheck.IsNeedToVerifyTreatment(raw);
                    return this.Finish(raw, verifyTreatment, ref resultData, null, null);
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

        internal bool Finish(HIS_SERVICE_REQ raw, bool verifyTreatment, ref HIS_SERVICE_REQ resultData, string exeLoginname, string exeUsername)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                valid = valid && checker.IsNotAprovedSurgeryRemuneration(raw);
                valid = valid && this.IsHasIcdExam(raw);

                if (valid)
                {
                    bool checkSttHT = false;
                    if (raw.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                    {
                        checkSttHT = true;
                    }
                    HisServiceReqUpdateFinish.SetFinishInfo(raw, exeLoginname, exeUsername);
                    if (this.hisServiceReqUpdate.Update(raw, verifyTreatment))
                    {
                        resultData = raw;
                        result = true;

                        //this.InitThreadSendIntegration(raw);
                        if (TheVietCFG.SUBCLINICAL_RESULT && checkSttHT)
                        {
                            //Tao thread moi de gui thong bao NMS
                            this.InitThreadSendNotification(raw);
                        }
                        new EventLogGenerator(EventLog.Enum.HisServiceReq_KetThucXuLyYLenh)
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

        //Bo sung thong tin ket thuc service_Req
        internal static void SetFinishInfo(HIS_SERVICE_REQ raw, string loginname, string username)
        {
            if (raw != null)
            {
                //Chuyen trang thai sang trang thai "hoan thanh" va luu thoi gian ket thuc
                raw.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                raw.FINISH_TIME = !raw.FINISH_TIME.HasValue ? Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now) : raw.FINISH_TIME;
                raw.IS_AUTO_FINISHED = Constant.IS_TRUE;
                if (String.IsNullOrWhiteSpace(loginname))
                {
                    raw.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    raw.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                }
                else
                {
                    raw.EXECUTE_LOGINNAME = loginname;
                    raw.EXECUTE_USERNAME = username;
                }
                raw.EXE_WORKING_SHIFT_ID = TokenManager.GetWorkingShift();
                raw.BIIN_TEST_RESULT = null;//set null de gui lai ket qua xet nghiem
            }
        }

        //Bo sung thong tin ket thuc service_Req voi ke don
        internal static void SetFinishInfo(HIS_SERVICE_REQ raw, string loginname, string username, long treatmentFinishTime)
        {
            if (raw != null)
            {
                //Chuyen trang thai sang trang thai "hoan thanh" va luu thoi gian ket thuc
                raw.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                raw.FINISH_TIME = treatmentFinishTime;
                raw.IS_AUTO_FINISHED = Constant.IS_TRUE;
                if (String.IsNullOrWhiteSpace(loginname))
                {
                    raw.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    raw.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                }
                else
                {
                    raw.EXECUTE_LOGINNAME = loginname;
                    raw.EXECUTE_USERNAME = username;
                }
                raw.EXE_WORKING_SHIFT_ID = TokenManager.GetWorkingShift();
                raw.BIIN_TEST_RESULT = null;//set null de gui lai ket qua xet nghiem
            }
        }

        //Bo sung thong tin ket thuc service_Req theo thong tin luc chi dinh
        internal static void SetFinishInfoUsingRequestInfo(HIS_SERVICE_REQ raw)
        {
            if (raw != null)
            {
                if ((!raw.START_TIME.HasValue) || (raw.START_TIME.HasValue && raw.START_TIME.Value > raw.INTRUCTION_TIME))
                {
                    raw.START_TIME = raw.INTRUCTION_TIME;
                }
                raw.FINISH_TIME = raw.INTRUCTION_TIME;
                raw.EXECUTE_LOGINNAME = raw.REQUEST_LOGINNAME;
                raw.EXECUTE_USERNAME = raw.REQUEST_USERNAME;
                raw.EXECUTE_USER_TITLE = HisEmployeeUtil.GetTitle(raw.REQUEST_LOGINNAME);
                raw.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                raw.EXE_WORKING_SHIFT_ID = raw.REQ_WORKING_SHIFT_ID;
            }
        }

        private bool CheckFinishTime(HIS_SERVICE_REQ data)
        {
            if (!data.FINISH_TIME.HasValue)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.LoiDuLieu);
                return false;
            }
            if (data.START_TIME.HasValue && data.FINISH_TIME.Value < data.START_TIME.Value)
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_ThoiGianKetThucKhongDuocBeHonThoiGianBatDau, Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.START_TIME.Value));
                return false;
            }
            //if (data.FINISH_TIME.Value > Inventec.Common.DateTime.Get.Now().Value)
            //{
            //    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_ThoiGianKetThucKhongDuocLonHonThoiGianHienTai);
            //    return false;
            //}
            if (data.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT
                || data.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT)
            {
                List<HIS_SERE_SERV_EXT> hisSereServExts = new HisSereServExtGet().GetByServiceReqId(data.ID);
                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceReqId(data.ID);
                hisSereServs = hisSereServs != null ? hisSereServs.Where(o => !o.IS_NO_EXECUTE.HasValue || o.IS_NO_EXECUTE.Value != Constant.IS_TRUE).ToList() : null;

                if (IsNotNullOrEmpty(hisSereServExts))
                {
                    List<HIS_SERE_SERV_EXT> ssExtCurrents = new List<HIS_SERE_SERV_EXT>();
                    hisSereServExts = hisSereServExts.OrderByDescending(o => o.ID).ToList();
                    foreach (HIS_SERE_SERV_EXT ssExt in hisSereServExts)
                    {
                        if (hisSereServs == null || !hisSereServs.Exists(e => e.ID == ssExt.SERE_SERV_ID))
                            continue;
                        if (ssExtCurrents.Exists(e => e.SERE_SERV_ID == ssExt.SERE_SERV_ID))
                        {
                            continue;
                        }
                        ssExtCurrents.Add(ssExt);
                    }
                    List<HIS_SERE_SERV_EXT> startSSExts = ssExtCurrents.Where(o => o.BEGIN_TIME.HasValue).OrderBy(o => o.BEGIN_TIME).ToList();
                    List<HIS_SERE_SERV_EXT> finishSSExts = ssExtCurrents.Where(o => o.END_TIME.HasValue).OrderByDescending(o => o.END_TIME).ToList();
                    long? startTime = IsNotNullOrEmpty(startSSExts) ? startSSExts.FirstOrDefault().BEGIN_TIME : null;
                    long? finishTime = IsNotNullOrEmpty(finishSSExts) ? finishSSExts.FirstOrDefault().END_TIME : null;
                    if (startTime.HasValue && data.START_TIME.HasValue && data.START_TIME.Value > startTime.Value)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_ThoiGianBatDauLonHonThoiGianBatDauDichVu, Inventec.Common.DateTime.Convert.TimeNumberToTimeString(startTime.Value));
                    }
                    if (finishTime.HasValue && data.FINISH_TIME.Value < finishTime.Value)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_ThoiGianKetThucBeHonThoiGianKetThucDichVu, Inventec.Common.DateTime.Convert.TimeNumberToTimeString(finishTime.Value));
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Neu la y lenh kham, doi tuong la BHYT thi bat buoc phai co thong tin ICD
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool IsHasIcdExam(HIS_SERVICE_REQ data)
        {
            try
            {
                if (data.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH && String.IsNullOrWhiteSpace(data.ICD_CODE))
                {
                    List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceReqId(data.ID);
                    hisSereServs = hisSereServs != null ? hisSereServs.Where(o => !o.IS_NO_EXECUTE.HasValue && o.PATIENT_TYPE_ID == Config.HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList() : null;
                    if (IsNotNullOrEmpty(hisSereServs))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_ThieuThongTinICD);
                        LogSystem.Warn("Yeu cau kham thieu thong tin ICD");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        private void InitThreadSendIntegration(HIS_SERVICE_REQ data)
        {
            try
            {
                if (data.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.ProcessSendIntegration));
                    thread.Priority = ThreadPriority.Lowest;
                    thread.Start(data.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessSendIntegration(object threadData)
        {
            try
            {
                if (threadData != null && ApiConsumerStore.EhrLisConsumer != null)
                {
                    long id = (long)threadData;
                    HIS_SERVICE_REQ data = new HisServiceReqGet().GetById(id);
                    List<HIS_SERE_SERV> sereServs = null;
                    List<V_HIS_SERE_SERV_TEIN> ssTeins = null;
                    HIS_PATIENT patient = null;

                    if (data != null
                        && data.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN
                        && data.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                    {
                        HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                        ssFilter.HAS_EXECUTE = true;
                        ssFilter.SERVICE_REQ_ID = data.ID;
                        ssFilter.TDL_SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN;
                        sereServs = new HisSereServGet().Get(ssFilter);
                    }
                    if (IsNotNullOrEmpty(sereServs))
                    {
                        ssTeins = new HisSereServTeinGet().GetViewBySereServIds(sereServs.Select(s => s.ID).ToList());
                        patient = new HisPatientGet().GetById(data.TDL_PATIENT_ID);
                    }
                    string rs = "";
                    new HisServiceReqTestEhrResulting().Run(patient, data, sereServs, ssTeins, ref rs);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
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

                if (serviceReq != null 
                    && HisServiceReqTypeCFG.SUBCLINICAL_TYPE_IDs.Contains(serviceReq.SERVICE_REQ_TYPE_ID) 
                    && !String.IsNullOrWhiteSpace(serviceReq.TDL_PATIENT_PHONE))
                {
                    HIS_SERVICE_REQ_TYPE srType = new HisServiceReqTypeGet().GetById(serviceReq.SERVICE_REQ_TYPE_ID);
                    string content = String.Format(MOS.MANAGER.Base.MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisServiceReq_DaCoKetQua, param.LanguageCode), srType != null ? srType.SERVICE_REQ_TYPE_NAME : "", serviceReq.SERVICE_REQ_CODE);
                    string phoneNumber = serviceReq.TDL_PATIENT_PHONE;
                    var category = NmsNotificationSend.Category.KQ_CLS;
                    bool result = new NmsNotificationSend(new CommonParam()).SendByIdentifierInfo(content, "", phoneNumber, category);
                    if (result)
                    {
                        Inventec.Common.Logging.LogSystem.Info(String.Format("Thanh cong gui thong bao den SDT:{0}", phoneNumber));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        internal void Rollback()
        {
            this.hisServiceReqUpdate.RollbackData();
        }
    }
}
