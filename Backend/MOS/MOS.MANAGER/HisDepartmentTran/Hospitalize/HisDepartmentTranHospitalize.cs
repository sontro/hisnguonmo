using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBedRoom;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisCareer;
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisDepartmentTran.Receive;
using AutoMapper;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.Token;
using MOS.UTILITY;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment.Update;
using System.Threading;
using MOS.MANAGER.OldSystemIntegrate;
using MOS.MANAGER.HisTreatment.Util;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.HisDepartmentTran.Create;

namespace MOS.MANAGER.HisDepartmentTran.Hospitalize
{
    class IntegrateHospitalizeThreadData
    {
        public HIS_TREATMENT Treatment { get; set; }
        public long Time { get; set; }
        public long DepartmentId { get; set; }
    }

    class HisDepartmentTranHospitalize : BusinessBase
    {
        private HIS_DEPARTMENT_TRAN recentDepartmentTran;
        private HIS_TREATMENT_BED_ROOM recentTreatmentBedRoom;
        private HIS_PATIENT_TYPE_ALTER recentPatientTypeAlter;
        private List<HIS_SERVICE_REQ> recentServiceReqs;

        private HisTreatmentBedRoomCreate hisTreatmentBedRoomCreate;
        private HisTreatmentBedRoomRemove hisTreatmentBedRoomRemove;
        private HisDepartmentTranCreate hisDepartmentTranCreate;
        private HisDepartmentTranUpdate hisDepartmentTranUpdate;
        private HisPatientTypeAlterCreate hisPatientTypeAlterCreate;
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisPatientUpdate hisPatientUpdate;

        internal HisDepartmentTranHospitalize()
            : base()
        {
            this.Init();
        }

        internal HisDepartmentTranHospitalize(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisDepartmentTranCreate = new HisDepartmentTranCreate(param);
            this.hisTreatmentBedRoomCreate = new HisTreatmentBedRoomCreate(param);
            this.hisTreatmentBedRoomRemove = new HisTreatmentBedRoomRemove(param);
            this.hisPatientTypeAlterCreate = new HisPatientTypeAlterCreate(param);
            this.hisDepartmentTranUpdate = new HisDepartmentTranUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisPatientUpdate = new HisPatientUpdate(param);
            this.recentServiceReqs = new List<HIS_SERVICE_REQ>();
        }

        internal bool Create(HisDepartmentTranHospitalizeSDO data, ref HisDepartmentTranHospitalizeResultSDO resultData)
        {
            return this.Create(data, null, ref resultData);
        }

        internal bool Create(HisDepartmentTranHospitalizeSDO data, long? currentServiceReqId, ref HisDepartmentTranHospitalizeResultSDO resultData)
        {
            bool result = false;
            try
            {
                this.SetServerTime(data);

                HIS_DEPARTMENT_TRAN lastDepartmentTran = null;
                List<HIS_DEPARTMENT_TRAN> allDepartmentTrans = null;
                HIS_PATIENT_TYPE_ALTER lastPta = null;
                HIS_TREATMENT treatment = null;
                HIS_CAREER career = null;
                WorkPlaceSDO workPlace = null;
                bool valid = true;

                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisDepartmentTranHospitalizeCheck checker = new HisDepartmentTranHospitalizeCheck(param);
                HisPatientTypeAlterCheck ptChecker = new HisPatientTypeAlterCheck(param);
                HisDepartmentTranCheck dpChecker = new HisDepartmentTranCheck(param);

                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.IsAllow(data, ref allDepartmentTrans, ref lastDepartmentTran, ref lastPta, workPlace);
                valid = valid && ptChecker.HasNoOutPrescription(data.TreatmentId, data.TreatmentTypeId);
                valid = valid && treatmentChecker.VerifyId(data.TreatmentId, ref treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && checker.IsFinishAllExamForHospitalize(treatment.ID, currentServiceReqId);
                valid = valid && checker.IsAllowIcdTreatment(data);
                valid = valid && ptChecker.IsValidOpenNotBhytTreatmentPolicy2(treatment.PATIENT_ID, treatment, lastPta, data);
                valid = valid && dpChecker.IsValidWithReqDepartment(workPlace.DepartmentId, data.TreatmentId);
                if (valid)
                {
                    if (data.CareerId.HasValue)
                    {
                        career = new HisCareerGet().GetById(data.CareerId.Value);
                    }
                    this.ProcessDepartmentTran(lastDepartmentTran, lastPta, data, allDepartmentTrans);
                    this.ProcessTreatmentBedRoom(data, workPlace);
                    this.ProcessTreatment(data, treatment, lastDepartmentTran, allDepartmentTrans, career);
                    this.ProcessServiceReq(treatment.ID, career);
                    this.ProcessPatient(data, treatment, career);
                    this.PassResult(data.TreatmentId, ref resultData);
                    result = true;

                    //Tao thread moi de gui du lieu tich hop sang HIS cu (PMS)
                    this.IntegrateThreadInit(treatment, data.Time, data.DepartmentId);

                    HisDepartmentTranLog.LogHospitalize(data, treatment, workPlace);
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessServiceReq(long treatmentId, HIS_CAREER career)
        {
            List<HIS_SERVICE_REQ> reqs = new HisServiceReqGet().GetByTreatmentId(treatmentId);

            if (IsNotNullOrEmpty(reqs))
            {
                // Phuc vu rollback
                this.recentServiceReqs.AddRange(reqs);

                reqs.ForEach(o => o.TDL_PATIENT_CAREER_NAME = IsNotNull(career) ? career.CAREER_NAME : null);

                if (!DAOWorker.HisServiceReqDAO.UpdateList(reqs))
                {
                    throw new Exception("Cap nhat thong tin y lenh that bai. Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        private void ProcessPatient(HisDepartmentTranHospitalizeSDO data, HIS_TREATMENT treatment, HIS_CAREER career)
        {
            HIS_PATIENT patient = new HisPatientGet().GetById(treatment.PATIENT_ID);
            Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
            HIS_PATIENT before = Mapper.Map<HIS_PATIENT>(patient);

            patient.RELATIVE_ADDRESS = data.RelativeAddress;
            patient.RELATIVE_PHONE = data.RelativePhone;
            patient.RELATIVE_NAME = data.RelativeName;
            if (IsNotNull(career))
            {
                patient.CAREER_ID = career.ID;
                patient.CAREER_CODE = career.CAREER_CODE;
                patient.CAREER_NAME = career.CAREER_NAME;
            }
            else
            {
                patient.CAREER_ID = null;
                patient.CAREER_CODE = null;
                patient.CAREER_NAME = null;
            }

            if (!this.hisPatientUpdate.UpdateWithoutChecking(patient, before))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }
        }

        private void PassResult(long treatmentId, ref HisDepartmentTranHospitalizeResultSDO resultData)
        {
            resultData = new HisDepartmentTranHospitalizeResultSDO();
            resultData.Treatment = new HisTreatmentGet().GetById(treatmentId);
            resultData.Patient = resultData.Treatment != null ? new HisPatientGet().GetViewById(resultData.Treatment.PATIENT_ID) : null;

            if (this.recentDepartmentTran != null)
            {
                resultData.DepartmentTran = new HisDepartmentTranGet().GetViewById(this.recentDepartmentTran.ID);
            }
            if (this.recentPatientTypeAlter != null)
            {
                resultData.PatientTypeAlter = new HisPatientTypeAlterGet().GetViewById(this.recentPatientTypeAlter.ID);
            }
        }

        private void ProcessTreatmentBedRoom(HisDepartmentTranHospitalizeSDO data, WorkPlaceSDO workPlace)
        {
            //Roi khoi cac buong thuoc khoa chuyen di
            List<HIS_TREATMENT_BED_ROOM> treatmentBedRooms = new HisTreatmentBedRoomGet().GetCurrentInByTreatmentId(data.TreatmentId);

            if (IsNotNullOrEmpty(treatmentBedRooms))
            {
                //lay ra danh sach HIS_TREATMENT_BED_ROOM ma co bed_room_id thuoc khoa chuyen di
                List<V_HIS_BED_ROOM> vBedRooms = HisBedRoomCFG.DATA.Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && o.DEPARTMENT_ID == workPlace.DepartmentId).ToList();
                List<HIS_TREATMENT_BED_ROOM> toUpdate = IsNotNullOrEmpty(vBedRooms) ? treatmentBedRooms.Where(o => vBedRooms.Where(t => t.ID == o.BED_ROOM_ID).Any()).ToList() : null;
                if (IsNotNullOrEmpty(toUpdate))
                {
                    List<HIS_TREATMENT_BED_ROOM> result = null;
                    if (!this.hisTreatmentBedRoomRemove.Remove(toUpdate, data.Time, false, ref result))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                }
            }

            if (data.BedRoomId.HasValue && (data.TreatmentTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU || data.TreatmentTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU))
            {
                WorkPlaceSDO workPlaceSDO = TokenManager.GetWorkPlace(data.RequestRoomId);

                //Kiem tra xem bed_room_id co thuoc khoa khong
                V_HIS_BED_ROOM bedRoom = HisBedRoomCFG.DATA
                    .Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && o.DEPARTMENT_ID == data.DepartmentId && o.ID == data.BedRoomId.Value)
                    .FirstOrDefault();

                HIS_TREATMENT_BED_ROOM treatmentBedRoom = new HIS_TREATMENT_BED_ROOM();
                treatmentBedRoom.ADD_TIME = data.Time;
                treatmentBedRoom.BED_ROOM_ID = data.BedRoomId.Value;
                treatmentBedRoom.TREATMENT_ID = data.TreatmentId;

                if (!this.hisTreatmentBedRoomCreate.Create(treatmentBedRoom))
                {
                    throw new Exception("Xu ly that bai, ket thuc nghiep vu" + LogUtil.TraceData("treatmentBedRoom", treatmentBedRoom));
                }
                this.recentTreatmentBedRoom = treatmentBedRoom;
            }
        }

        //Cap nhat thong tin treatment neu co yeu cau
        private void ProcessTreatment(HisDepartmentTranHospitalizeSDO data, HIS_TREATMENT treatment, HIS_DEPARTMENT_TRAN lastDepartmentTran, List<HIS_DEPARTMENT_TRAN> allDepartmentTrans, HIS_CAREER career)
        {
            //clone phuc vu rollback
            Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
            HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);

            //Cap nhat thong tin benh chinh cho treatment
            if (!string.IsNullOrWhiteSpace(data.IcdName)
                || !string.IsNullOrWhiteSpace(data.IcdText)
                || !string.IsNullOrWhiteSpace(data.IcdSubCode)
                || !string.IsNullOrWhiteSpace(data.IcdCode))
            {
                treatment.ICD_CODE = data.IcdCode;
                treatment.ICD_NAME = data.IcdName;
                treatment.TRADITIONAL_ICD_NAME = data.TraditionalIcdName;
                treatment.TRADITIONAL_ICD_CODE = data.TraditionalIcdCode;
                treatment.TRADITIONAL_ICD_TEXT = data.TraditionalIcdText;
                treatment.TRADITIONAL_ICD_SUB_CODE = data.TraditionalIcdSubCode;
                treatment.ICD_TEXT = HisIcdUtil.RemoveDuplicateIcd(data.IcdText);
                treatment.ICD_SUB_CODE = HisIcdUtil.RemoveDuplicateIcd(data.IcdSubCode);

                treatment.IN_ICD_CODE = data.IcdCode;
                treatment.TRADITIONAL_IN_ICD_CODE = data.TraditionalIcdCode;
                treatment.TRADITIONAL_IN_ICD_NAME = data.TraditionalIcdName;
                treatment.TRADITIONAL_IN_ICD_TEXT = data.TraditionalIcdText;
                treatment.TRADITIONAL_IN_ICD_SUB_CODE = data.TraditionalIcdSubCode;
                treatment.IN_ICD_SUB_CODE = data.IcdSubCode;
                treatment.IN_ICD_NAME = data.IcdName;
                treatment.IN_ICD_TEXT = data.IcdText;
            }

            //Luu thong tin nhap vien do bs nhap khi y/c nhap vien
            V_HIS_ROOM room = HisRoomCFG.DATA != null ? HisRoomCFG.DATA.Where(o => o.ID == data.RequestRoomId).FirstOrDefault() : null;
            treatment.IN_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            treatment.IN_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            treatment.IN_TREATMENT_TYPE_ID = data.TreatmentTypeId;
            treatment.IN_ROOM_ID = data.RequestRoomId;
            treatment.IN_DEPARTMENT_ID = room != null ? room.DEPARTMENT_ID : 0;
            treatment.IS_EMERGENCY = data.IsEmergency ? (short?)Constant.IS_TRUE : null;
            treatment.TDL_PATIENT_RELATIVE_NAME = data.RelativeName;
            
            //Set DepartmentIds cho ho so dieu tri
            HisTreatmentUtil.SetDepartmentInfo(treatment, allDepartmentTrans);

            //Neu chuyen nhap vien vao khoa hien tai thi ko co nghiep vu tiep nhan nen 
            //lay luon thoi gian y/c nhap vien lam thoi gian vao noi tru
            //va tu dong sinh so vao vien cho benh nhan
            if (lastDepartmentTran.DEPARTMENT_ID == data.DepartmentId)
            {
                HIS_TREATMENT treatmetAfterCreatePatientTypeAlter = new HisTreatmentGet().GetById(treatment.ID);
                treatment.IN_CODE = treatmetAfterCreatePatientTypeAlter.IN_CODE;
                treatment.IN_CODE_SEED_CODE = treatmetAfterCreatePatientTypeAlter.IN_CODE_SEED_CODE;
                treatment.CLINICAL_IN_TIME = treatmetAfterCreatePatientTypeAlter.CLINICAL_IN_TIME;
                treatment.TDL_TREATMENT_TYPE_ID = treatmetAfterCreatePatientTypeAlter.TDL_TREATMENT_TYPE_ID;
                treatment.HOSPITALIZE_DEPARTMENT_ID = data.DepartmentId; //Neu nhap vien vao khoa hien tai thi cap nhat luon thong tin khoa nhap vien
            }
            else if (HisTreatmentCFG.IN_CODE_GENERATE_OPTION == (int)HisTreatmentCFG.InCodeGenerateOption.BY_REQ)
            {
                HisTreatmentInCode.SetInCode(treatment, data.Time, data.DepartmentId, treatment.IN_TREATMENT_TYPE_ID.Value);
            }

            if (!treatment.EMR_COVER_TYPE_ID.HasValue && IsNotNullOrEmpty(HisEmrCoverConfigCFG.DATA))
            {
                var emrConver = HisEmrCoverConfigCFG.DATA.Where(o => o.DEPARTMENT_ID == data.DepartmentId && o.TREATMENT_TYPE_ID == data.TreatmentTypeId).ToList();
                if (emrConver != null && emrConver.Count == 1)
                {
                    treatment.EMR_COVER_TYPE_ID = emrConver.FirstOrDefault().EMR_COVER_TYPE_ID;
                }
            }
            treatment.TDL_PATIENT_CAREER_NAME = IsNotNull(career) ? career.CAREER_NAME : null;

            if (!this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
            {
                LogSystem.Warn("Cap nhat thong tin ICD, IN_CODE cho treatment dua vao thong tin ICD cua dich vu kham that bai.");
            }
            HisTreatmentInCode.FinishDB(treatment);//xac nhan da xu ly DB (phuc vu nghiep vu sinh so vao vien)
        }

        private void ProcessDepartmentTran(HIS_DEPARTMENT_TRAN lastDepartmentTran, HIS_PATIENT_TYPE_ALTER lastPta, HisDepartmentTranHospitalizeSDO data, List<HIS_DEPARTMENT_TRAN> allDepartmentTrans)
        {
            //Neu khoa nhap vao khac khoa hien tai cua BN thi tao ban ghi chuyen sang khoa khac
            //Hoac cung khoa nhung co cau hinh "nhap thu cong so vao vien" thi cung tao ban ghi "nhap vien" de bo phan tiep nhan BN co the nhap so vao vien
            if (lastDepartmentTran.DEPARTMENT_ID != data.DepartmentId || HisTreatmentCFG.IS_MANUAL_IN_CODE)
            {
                HIS_DEPARTMENT_TRAN toInsert = new HIS_DEPARTMENT_TRAN();
                toInsert.DEPARTMENT_ID = data.DepartmentId;
                toInsert.ICD_CODE = data.IcdCode;
                toInsert.ICD_NAME = data.IcdName;
                toInsert.TRADITIONAL_ICD_NAME = data.TraditionalIcdName;
                toInsert.TRADITIONAL_ICD_CODE = data.TraditionalIcdCode;
                toInsert.TRADITIONAL_ICD_TEXT = data.TraditionalIcdText;
                toInsert.TRADITIONAL_ICD_SUB_CODE = data.TraditionalIcdSubCode;
                toInsert.ICD_SUB_CODE = data.IcdSubCode;
                toInsert.ICD_TEXT = data.IcdText;
                toInsert.TREATMENT_ID = data.TreatmentId;
                toInsert.PREVIOUS_ID = lastDepartmentTran.ID;
                toInsert.REQUEST_TIME = data.Time;
                toInsert.IS_HOSPITALIZED = Constant.IS_TRUE;

                if (!DAOWorker.HisDepartmentTranDAO.Create(toInsert))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDepartmentTran_ThemMoiThatBai);
                    throw new Exception("Them moi thong tin HisDepartmentTran that bai." + LogUtil.TraceData("data", data));
                }
                this.recentDepartmentTran = toInsert;
                allDepartmentTrans.Add(toInsert);
            }
            else if (lastDepartmentTran.DEPARTMENT_ID == data.DepartmentId)
            {
                lastDepartmentTran.ICD_CODE = data.IcdCode;
                lastDepartmentTran.ICD_NAME = data.IcdName;
                lastDepartmentTran.TRADITIONAL_ICD_NAME = data.TraditionalIcdName;
                lastDepartmentTran.TRADITIONAL_ICD_CODE = data.TraditionalIcdCode;
                lastDepartmentTran.TRADITIONAL_ICD_TEXT = data.TraditionalIcdText;
                lastDepartmentTran.TRADITIONAL_ICD_SUB_CODE = data.TraditionalIcdSubCode;
                lastDepartmentTran.ICD_SUB_CODE = data.IcdSubCode;
                lastDepartmentTran.ICD_TEXT = data.IcdText;
                if (!this.hisDepartmentTranUpdate.Update(lastDepartmentTran))
                {
                    throw new Exception("Sua thong tin departmentTran that bai. Nghiep vu tiep theo se khong thuc hien duoc");
                }
                this.recentDepartmentTran = lastDepartmentTran;

                //Neu nhap vien trong cung khoa thi tao thong tin dien doi tuong luon
                HIS_PATIENT_TYPE_ALTER resultData = null;
                if (!this.hisPatientTypeAlterCreate.CreateByChangeTreatmentType(lastPta, lastDepartmentTran, data.TreatmentTypeId, data.Time, data.RequestRoomId, ref resultData))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
                this.recentPatientTypeAlter = resultData;
            }
        }

        private void IntegrateThreadInit(HIS_TREATMENT treatment, long time, long departmentId)
        {
            try
            {
                if (OldSystemCFG.INTEGRATION_TYPE != OldSystemCFG.IntegrationType.NONE)
                {
                    IntegrateHospitalizeThreadData threadData = new IntegrateHospitalizeThreadData();
                    threadData.Treatment = treatment;
                    threadData.Time = time;
                    threadData.DepartmentId = departmentId;
                    Thread thread = new Thread(new ParameterizedThreadStart(this.ProcessSendingIntegration));
                    thread.Priority = ThreadPriority.Normal;
                    thread.Start(threadData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh gui thong tin nhap vien sang HMS", ex);
            }
        }

        private void ProcessSendingIntegration(object threadData)
        {
            IntegrateHospitalizeThreadData td = (IntegrateHospitalizeThreadData)threadData;
            OldSystemIntegrateProcessor.Hospitalize(td.Treatment, td.Time, td.DepartmentId);
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(HisDepartmentTranHospitalizeSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                data.Time = now;
            }
        }

        internal void RollbackData()
        {
            this.hisPatientUpdate.RollbackData();
            if (IsNotNullOrEmpty(this.recentServiceReqs))
            {
                if (!DAOWorker.HisServiceReqDAO.UpdateList(this.recentServiceReqs))
                {
                    throw new Exception("Rollback cap nhat thong tin y lenh that bai. Ket thuc nghiep vu. Rollback du lieu");
                }
            }
            this.hisDepartmentTranCreate.RollbackData();
            this.hisPatientTypeAlterCreate.RollbackData();
            this.hisTreatmentBedRoomCreate.RollbackData();
        }
    }
}
