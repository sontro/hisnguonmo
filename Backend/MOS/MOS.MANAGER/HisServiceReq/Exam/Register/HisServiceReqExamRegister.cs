using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.Token;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.UTILITY;
using System.Threading;
using MOS.MANAGER.OldSystemIntegrate;
using AutoMapper;
using MOS.MANAGER.HisServiceReq.Exam.Register;
using MOS.MANAGER.HisPatient.Register;
using MOS.MANAGER.HisServiceReq.Exam.Register.Billing;
using MOS.MANAGER.HisServiceReq.Exam.Register.Deposit;
using MOS.MANAGER.HisTreatment.Util;
using MOS.MANAGER.HisServiceReq.Exam.Register.Epayment;
using MOS.MANAGER.HisPatientTypeAlter;

namespace MOS.MANAGER.HisServiceReq.Exam.Register
{
    class IntegrateRegisterThreadData
    {
        public bool IsOldPatient { get; set; }
        public HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }
        public HIS_PATIENT Patient { get; set; }
        public HIS_TREATMENT Treatment { get; set; }
        public HIS_SERE_SERV SereServ { get; set; }
    }

    class ThreadExportXmlData
    {
        public HIS_TREATMENT Treatment { get; set; }
        public HIS_BRANCH Branch { get; set; }
        public HIS_SERVICE_REQ ServiceReq { get; set; }
        public HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }
    }

    /// <summary>
    /// Vua tao y/c kham + tao thong tin benh nhan, ho so benh an, ho so vien phi, thong tin the,
    /// </summary>
    partial class HisServiceReqExamRegister : BusinessBase
    {
        private HisPatientRegister hisPatientRegister;
        private HisServiceReqCreateOnRegister hisServiceReqCreateOnRegister;

        internal HisServiceReqExamRegister()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqExamRegister(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisPatientRegister = new HisPatientRegister(param);
            this.hisServiceReqCreateOnRegister = new HisServiceReqCreateOnRegister(param);
        }

        internal bool Create(HisServiceReqExamRegisterSDO data, bool forceUseClientTime, ref HisServiceReqExamRegisterResultSDO resultData, ref HIS_TREATMENT treatment, ref WorkPlaceSDO workPlace)
        {
            bool result = false;
            try
            {
                this.SetServerTime(data, forceUseClientTime);

                HIS_CARD hisCard = null;

                HisPatientRegisterCheck patientRegisterCheck = new HisPatientRegisterCheck(param);

                bool valid = data != null;
                
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && this.IsNotExceedRoomLimit(data);
                valid = valid && patientRegisterCheck.IsValidCardInfo(data.HisPatientProfile, ref hisCard);
                valid = valid && patientRegisterCheck.IsValidInCode(data.HisPatientProfile);
                if (valid)
                {
                    bool isOldPatient = data.HisPatientProfile.HisPatient.ID > 0;
                    ServiceReqDetailSDO sdo = data.ServiceReqDetails.Where(o => o.RoomId.HasValue).FirstOrDefault();
                    long roomId = sdo != null ? sdo.RoomId.Value : 0;
                    long serviceId = sdo != null ? sdo.ServiceId : 0;

                    //Cap nhat du lieu dich vu kham gan nhat
                    data.HisPatientProfile.HisPatient.RECENT_ROOM_ID = roomId;

                    //Cho phep ko chon dich vu nen chi update neu co thong tin dich vu, tranh bi update ve 0
                    if (serviceId != 0)
                    {
                        data.HisPatientProfile.HisPatient.RECENT_SERVICE_ID = serviceId;
                    }

                    //Neu co cau hinh tu dong lay khoa tiep nhan chinh la khoa xu ly dich vu kham dau tien
                    //thi thuc hien gan lai department_id
                    if (HisTreatmentCFG.IS_AUTO_DETECT_RECEIVING_DEPARTMENT_BY_FIRST_EXAM_EXECUTE_ROOM
                        && IsNotNullOrEmpty(data.ServiceReqDetails)
                        && data.ServiceReqDetails[0].RoomId.HasValue)
                    {
                        V_HIS_EXECUTE_ROOM executeRoom = HisExecuteRoomCFG.DATA
                            .Where(o => o.ROOM_ID == roomId).FirstOrDefault();
                        if (executeRoom != null && data.HisPatientProfile != null)
                        {
                            data.HisPatientProfile.DepartmentId = executeRoom.DEPARTMENT_ID;
                        }
                    }

                    treatment = data.HisPatientProfile.HisTreatment;
                    treatment.TDL_FIRST_EXAM_ROOM_ID = roomId; //luu du thua du lieu phong kham dau tien
                    data.HisPatientProfile.RequestRoomId = data.RequestRoomId;

                    //Thuc hien tao ho so dieu tri, thong tin benh nhan, ...
                    if (!this.hisPatientRegister.RegisterProfile(data.HisPatientProfile, hisCard, true))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }

                    resultData = new HisServiceReqExamRegisterResultSDO();
                    resultData.HisPatientProfile = data.HisPatientProfile;

                    //neu co yeu cau dich vu thi moi tao dich vu
                    if (IsNotNullOrEmpty(data.ServiceReqDetails))
                    {
                        //Tao thong tin yeu cau dich vu
                        data.TreatmentId = treatment.ID;
                        //Neu cap cuu thi khong thu vien phi
                        if ((data.IsNotRequireFee.HasValue && data.IsNotRequireFee.Value == Constant.IS_TRUE)
                            || data.HisPatientProfile.HisTreatment.IS_EMERGENCY == Constant.IS_TRUE)
                        {
                            data.IsNotRequireFee = MOS.UTILITY.Constant.IS_TRUE;
                        }
                        else
                        {
                            data.IsNotRequireFee = null;
                        }

                        HisServiceReqListResultSDO serviceReqListResult = new HisServiceReqListResultSDO();

                        if (!this.hisServiceReqCreateOnRegister.Create(data, treatment, data.HisPatientProfile.HisPatientTypeAlter, ref serviceReqListResult))
                        {
                            throw new Exception("Du lieu se bi rollback. Ket thuc nghiep vu");
                        }

                        resultData.SereServs = serviceReqListResult.SereServs;
                        resultData.ServiceReqs = serviceReqListResult.ServiceReqs;
                    }

                    HisTreatmentLog.Run(data.HisPatientProfile.HisPatient, data.HisPatientProfile.HisTreatment, data.HisPatientProfile.HisPatientTypeAlter, resultData.ServiceReqs, resultData.SereServs, EventLog.Enum.HisTreatment_DangKyTiepDon);

                    //Tao thread moi de gui du lieu tich hop
                    this.IntegrateThreadInit(isOldPatient, resultData);
                    this.InitThreadCreateMS(data.HisPatientProfile.HisPatient);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                resultData = null;
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void InitThreadCreateMS(HIS_PATIENT patient)
        {
            try
            {
                if (CosCFG.IS_CREATE_REGISTER_CODE && IsNotNull(patient) && string.IsNullOrEmpty(patient.REGISTER_CODE))
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.CreateMS));
                    thread.Priority = ThreadPriority.BelowNormal;
                    thread.Start(patient.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void CreateMS(object data)
        {
            try
            {
                long patientId = (long)data;
                new HisPatientCreateRegisterCode().Run(patientId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void IntegrateThreadInit(bool isOldPatient, HisServiceReqExamRegisterResultSDO resultData)
        {
            try
            {
                HIS_BRANCH branch = new TokenManager().GetBranch();

                if (IsNotNullOrEmpty(resultData.SereServs))
                {
                    if (OldSystemCFG.INTEGRATION_TYPE != OldSystemCFG.IntegrationType.NONE)
                    {
                        Mapper.CreateMap<V_HIS_SERE_SERV, HIS_SERE_SERV>();
                        IntegrateRegisterThreadData threadData = new IntegrateRegisterThreadData();
                        threadData.IsOldPatient = isOldPatient;
                        threadData.Patient = resultData.HisPatientProfile.HisPatient;
                        threadData.PatientTypeAlter = resultData.HisPatientProfile.HisPatientTypeAlter;
                        threadData.Treatment = resultData.HisPatientProfile.HisTreatment;
                        threadData.SereServ = Mapper.Map<HIS_SERE_SERV>(resultData.SereServs[0]);
                        Thread thread = new Thread(new ParameterizedThreadStart(this.ProcessSendingIntegration));
                        thread.Priority = ThreadPriority.Normal;
                        thread.Start(threadData);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh cap nhat treatment", ex);
            }
        }

        private void ProcessSendingIntegration(object threadData)
        {
            IntegrateRegisterThreadData td = (IntegrateRegisterThreadData)threadData;
            OldSystemIntegrateProcessor.CreateTreatment(td.IsOldPatient, td.PatientTypeAlter, td.Patient, td.Treatment, td.SereServ);
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server.
        /// Neu dang ky qua he thong DKK thi lay theo thong tin thoi gian tu DKK
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(HisServiceReqExamRegisterSDO data, bool forceUseClientTime)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (!forceUseClientTime && SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                data.InstructionTimes = new List<long>() {now};
                data.InstructionTime = now;
                if (data.HisPatientProfile != null)
                {
                    data.HisPatientProfile.TreatmentTime = now;
                    if (data.HisPatientProfile.HisPatientTypeAlter != null)
                    {
                        data.HisPatientProfile.HisPatientTypeAlter.LOG_TIME = now;
                    }
                }
            }
        }

        /// <summary>
        /// Kiem tra xem da vuot so luong kham cau hinh theo phong chua
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool IsNotExceedRoomLimit(HisServiceReqExamRegisterSDO data)
        {
            try
            {
                HisServiceReqExamCheck examChecker = new HisServiceReqExamCheck(param);
                List<long> examRoomIds = data.ServiceReqDetails != null ? data.ServiceReqDetails.Where(o =>
                    o.RoomId.HasValue).Select(o => o.RoomId.Value).ToList() : null;

                return examChecker.IsNotExceedLimit(examRoomIds, data.TreatmentId, data.HisPatientProfile.HisPatientTypeAlter.TREATMENT_TYPE_ID, data.InstructionTime);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Rollback du lieu ExamServiceReq
        /// </summary>
        /// <returns></returns>
        internal void RollbackData()
        {
            this.hisServiceReqCreateOnRegister.RollbackData();
            //rollback du lieu HisServiceReqRegister
            this.hisPatientRegister.RollbackData();
        }
    }
}
