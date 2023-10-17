using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisCoTreatment;
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisDepartmentTran.Receive;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Bed.CreateTempByBedLog;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Util;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDepartmentTran.Receive
{
    class HisDepartmentTranReceive : BusinessBase
    {
        //Du lieu phuc vu xu ly
        private HIS_TREATMENT_BED_ROOM recentHisTreatmentBedRoom;

        private HisDepartmentTranUpdate hisDepartmentTranUpdate;
        private HisTreatmentBedRoomCreate hisTreatmentBedRoomCreate;
        private HisPatientTypeAlterCreate hisPatientTypeAlterCreate;
        private HisBedLogCreate hisBedLogCreate;
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisPatientUpdate hisPatientUpdate;
        private string CheckTreatmentBedRoomKey;

        private static List<string> DataTreatmentReceive = new List<string>();

        internal HisDepartmentTranReceive()
            : base()
        {
            this.Init();
        }

        internal HisDepartmentTranReceive(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisDepartmentTranUpdate = new HisDepartmentTranUpdate(param);
            this.hisTreatmentBedRoomCreate = new HisTreatmentBedRoomCreate(param);
            this.hisPatientTypeAlterCreate = new HisPatientTypeAlterCreate(param);
            this.hisBedLogCreate = new HisBedLogCreate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisPatientUpdate = new HisPatientUpdate(param);
        }

        internal bool Run(HisDepartmentTranReceiveSDO data, ref HIS_DEPARTMENT_TRAN resultData)
        {
            bool result = false;
            try
            {
                this.SetServerTime(data);
                HisDepartmentTranReceiveCheck checker = new HisDepartmentTranReceiveCheck(param);
                HisPatientCheck patientChecker = new HisPatientCheck(param);
                HIS_TREATMENT treatment = null;
                HIS_PATIENT patient = null;
                HIS_DEPARTMENT_TRAN departmentTran = null;

                HisTreatmentCheck treatmentCheck = new HisTreatmentCheck(param);
                bool valid = true;
                valid = valid && checker.IsAllow(data, ref departmentTran);
                valid = valid && treatmentCheck.VerifyId(departmentTran.TREATMENT_ID, ref treatment);
                valid = valid && patientChecker.VerifyId(treatment.PATIENT_ID, ref patient);
                valid = valid && checker.IsValidData(data, departmentTran, treatment);
                valid = valid && checker.IsAllowIcdTreatment(data, treatment);
                valid = valid && treatmentCheck.IsUnpause(treatment);
                valid = valid && checker.IsValidInTime(data.InTime, false);
                valid = valid && CheckTreatmentReceive(data, treatment);
                if (valid)
                {
                    HIS_BED_LOG bedLog = null;

                    this.ProcessDepartmentTran(data, departmentTran);
                    this.ProcessPatientTypeAlter(data, treatment.ID, departmentTran);
                    this.ProcessTreatmentBedRoom(data, treatment.ID);
                    this.ProcessTreatment(data, ref treatment, departmentTran);
                    this.ProcessBedLog(data, treatment, ref bedLog);
                    this.ProcessPatient(data.PatientClassifyId, patient);
                    this.ProcessRecalcSereServ(treatment, departmentTran);
                    this.ProcessServiceReq(data.PatientClassifyId, treatment.ID);

                    resultData = departmentTran;
                    result = true;
                    if (resultData != null)
                    {
                        new HisTreatmentUploadEmr().Run(resultData.TREATMENT_ID);
                    }

                    HisDepartmentTranLog.LogReceive(data, treatment, departmentTran);
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
            finally
            {
                if (!String.IsNullOrWhiteSpace(CheckTreatmentBedRoomKey))
                {
                    lock (DataTreatmentReceive)
                    {
                        DataTreatmentReceive.Remove(CheckTreatmentBedRoomKey);
                    }
                }
            }
            return result;
        }

        private bool CheckTreatmentReceive(HisDepartmentTranReceiveSDO data, HIS_TREATMENT treatment)
        {
            bool result = true;
            try
            {
                if (IsNotNull(data) && data.BedRoomId.HasValue)
                {
                    CheckTreatmentBedRoomKey = String.Format("{0}_{1}_{2}", treatment.ID, data.BedRoomId.Value, DateTime.Now.ToString("yyyyMMddHHmm"));
                    lock (DataTreatmentReceive)
                    {
                        if (DataTreatmentReceive.Contains(CheckTreatmentBedRoomKey))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatmentBedRoom_BenhNhanDangTrongBuongBenhKhongChoPhepTao);
                            return false;
                        }
                        else
                        {
                            DataTreatmentReceive.Add(CheckTreatmentBedRoomKey);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private void ProcessServiceReq(long? patientClassifyid, long treatmentId)
        {
            // Update PATIENT_CLASSIFY_ID for HIS_SERVICE_REQ table
            string sql = "UPDATE HIS_SERVICE_REQ SET TDL_PATIENT_CLASSIFY_ID = :param1 WHERE TREATMENT_ID = :param2";
            if (!DAOWorker.SqlDAO.Execute(sql, patientClassifyid, treatmentId))
            {
                throw new Exception("Cap nhat thong tin PATIENT_CLASSIFY_ID cho bang HIS_SERVICE_REQ that bai. Rollback du lieu");
            }
        }

        private void ProcessPatient(long? patientClassifyId, HIS_PATIENT patient)
        {
            if (patient != null)
            {
                Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
                HIS_PATIENT beforeUpdate = Mapper.Map<HIS_PATIENT>(patient);
                patient.PATIENT_CLASSIFY_ID = patientClassifyId;

                if (ValueChecker.IsPrimitiveDiff<HIS_PATIENT>(beforeUpdate, patient)
                    && !this.hisPatientUpdate.Update(patient, beforeUpdate))
                {
                    throw new Exception("Cap nhat thong tin PATIENT_CLASSIFY_ID cho bang HIS_PATIENT that bai. Rollback du lieu");
                }
            }
        }

        private void ProcessRecalcSereServ(HIS_TREATMENT treatment, HIS_DEPARTMENT_TRAN departmentTran)
        {
            if (treatment != null && departmentTran.PREVIOUS_ID.HasValue && HisHeinBhytCFG.EMERGENCY_EXAM_POLICY_OPTION == HisHeinBhytCFG.EmergencyExamPolicyOption.BY_EXECUTE_DEPARTMENT && departmentTran.DEPARTMENT_IN_TIME.HasValue)
            {
                //Kiem tra xem khoa truoc do co phai a khoa cap cuu ko
                HIS_DEPARTMENT_TRAN previous = new HisDepartmentTranGet().GetById(departmentTran.PREVIOUS_ID.Value);
                HIS_DEPARTMENT previousDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == previous.DEPARTMENT_ID).FirstOrDefault();
                if (previousDepartment != null && previousDepartment.IS_EMERGENCY == Constant.IS_TRUE)
                {
                    List<HIS_SERE_SERV> existsSereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);
                    List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);

                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> olds = Mapper.Map<List<HIS_SERE_SERV>>(existsSereServs);

                    //Luu y can set thoi gian vao truoc khi goi ham update hisSereServUpdateHein.UpdateDb
                    //vi o nghiep vu tinh toan lai sere_serv co su dung out_time
                    this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, ptas, false, previous.DEPARTMENT_ID, previous.DEPARTMENT_IN_TIME, departmentTran.DEPARTMENT_IN_TIME);
                    this.hisSereServUpdateHein.UpdateDb(olds, existsSereServs);
                }
            }
        }

        private void ProcessPatientTypeAlter(HisDepartmentTranReceiveSDO data, long treatmentId, HIS_DEPARTMENT_TRAN departmentTran)
        {
            //Neu co su thay doi dien doi tuong thi moi thuc hien them ban ghi
            if (data.TreatmentTypeId.HasValue)
            {
                HIS_PATIENT_TYPE_ALTER lastPta = new HisPatientTypeAlterGet().GetLastByTreatmentId(treatmentId);
                if (lastPta.TREATMENT_TYPE_ID != data.TreatmentTypeId)
                {
                    Mapper.CreateMap<HIS_PATIENT_TYPE_ALTER, HIS_PATIENT_TYPE_ALTER>();
                    HIS_PATIENT_TYPE_ALTER pta = Mapper.Map<HIS_PATIENT_TYPE_ALTER>(lastPta);

                    HIS_PATIENT_TYPE_ALTER result = new HIS_PATIENT_TYPE_ALTER();

                    pta.LOG_TIME = data.InTime;
                    pta.EXECUTE_ROOM_ID = data.RequestRoomId;
                    pta.TREATMENT_TYPE_ID = data.TreatmentTypeId.Value;
                    pta.DEPARTMENT_TRAN_ID = departmentTran.ID;
                    this.hisPatientTypeAlterCreate.Create(pta, ref result);
                }
            }
        }

        private void ProcessTreatmentBedRoom(HisDepartmentTranReceiveSDO data, long treatmentId)
        {
            if (data.BedRoomId.HasValue)
            {
                HIS_TREATMENT_BED_ROOM treatmentBedRoom = new HIS_TREATMENT_BED_ROOM();
                treatmentBedRoom.BED_ROOM_ID = data.BedRoomId.Value;
                treatmentBedRoom.TREATMENT_ID = treatmentId;
                treatmentBedRoom.ADD_TIME = data.InTime;
                //treatmentBedRoom.BED_ID = data.BedId; Khong xet vao o day neu khong se bi tao double Bed_Log
                if (!hisTreatmentBedRoomCreate.Create(treatmentBedRoom, data.BedServiceId, data.ShareCount, data.PatientTypeId, data.PrimaryPatientTypeId))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
                this.recentHisTreatmentBedRoom = treatmentBedRoom;
            }
        }

        private void ProcessBedLog(HisDepartmentTranReceiveSDO data, HIS_TREATMENT treatment, ref HIS_BED_LOG bedLog)
        {
            if (data.BedId.HasValue && this.recentHisTreatmentBedRoom != null)
            {
                bedLog = new HIS_BED_LOG();
                bedLog.BED_ID = data.BedId.Value;
                bedLog.BED_SERVICE_TYPE_ID = data.BedServiceId;
                bedLog.TREATMENT_BED_ROOM_ID = this.recentHisTreatmentBedRoom.ID;
                bedLog.START_TIME = data.InTime;
                bedLog.SHARE_COUNT = data.ShareCount;
                bedLog.PATIENT_TYPE_ID = data.PatientTypeId;
                bedLog.PRIMARY_PATIENT_TYPE_ID = data.PrimaryPatientTypeId;
                bedLog.IS_SERVICE_REQ_ASSIGNED = Constant.IS_TRUE;

                if (!this.hisBedLogCreate.Create(bedLog, treatment, data.RequestRoomId))
                {
                    throw new Exception("Tao thong tin giuong that bai. Ket thuc nghiep vu Rollback du lieu");
                }
            }
        }

        private void ProcessDepartmentTran(HisDepartmentTranReceiveSDO data, HIS_DEPARTMENT_TRAN departmentTran)
        {
            departmentTran.DEPARTMENT_IN_TIME = data.InTime;
            departmentTran.ICD_NAME = data.IcdName;
            departmentTran.ICD_SUB_CODE = data.IcdSubCode;
            departmentTran.ICD_TEXT = data.IcdText;
            departmentTran.ICD_CODE = data.IcdCode;
            departmentTran.TRADITIONAL_ICD_CODE = data.TraditionalIcdCode;
            departmentTran.TRADITIONAL_ICD_NAME = data.TraditionalIcdName;
            departmentTran.TRADITIONAL_ICD_SUB_CODE = data.TraditionalIcdSubCode;
            departmentTran.TRADITIONAL_ICD_TEXT = data.TraditionalIcdText;

            if (!this.hisDepartmentTranUpdate.Update(departmentTran))
            {
                throw new Exception("Cap nhat thong tin hisDepartmentTran that bai. Nghiep vu tiep theo se khong thuc hien duoc");
            }
        }

        private void ProcessTreatment(HisDepartmentTranReceiveSDO data, ref HIS_TREATMENT treatment, HIS_DEPARTMENT_TRAN departmentTran)
        {
            //Phai lay treatment tu db tranh truong hop mat dien dieu tri, thoi gian nhap vien duoc update trong ham ProcessPatientTypeAlter
            treatment = new HisTreatmentGet().GetById(treatment.ID);
            if (treatment != null)
            {
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);

                //Trong truong hop cau hinh "nhap thu cong so vao vien" thi xu ly de cap nhat so vao vien
                if (!string.IsNullOrWhiteSpace(data.InCode) && HisTreatmentCFG.IS_MANUAL_IN_CODE && departmentTran.IS_HOSPITALIZED == Constant.IS_TRUE)
                {
                    treatment.IN_CODE = data.InCode;
                }

                //Neu la tiep nhan vao vien thi cap nhat lai thong tin "khoa nhap vien" trong HIS_TREATMENT
                if (departmentTran.IS_HOSPITALIZED == Constant.IS_TRUE)
                {
                    treatment.HOSPITALIZE_DEPARTMENT_ID = departmentTran.DEPARTMENT_ID;
                }

                List<HIS_DEPARTMENT_TRAN> lstDt = new HisDepartmentTranGet().GetByTreatmentId(treatment.ID);

                HisTreatmentUtil.SetDepartmentInfo(treatment, lstDt);

                if (!String.IsNullOrWhiteSpace(data.IcdCode)) treatment.ICD_CODE = data.IcdCode;
                if (!String.IsNullOrWhiteSpace(data.IcdName)) treatment.ICD_NAME = data.IcdName;
                if (!String.IsNullOrWhiteSpace(data.IcdSubCode)) treatment.ICD_SUB_CODE = data.IcdSubCode;
                if (!String.IsNullOrWhiteSpace(data.IcdText)) treatment.ICD_TEXT = data.IcdText;
                if (!String.IsNullOrWhiteSpace(data.TraditionalIcdCode)) treatment.TRADITIONAL_ICD_CODE = data.TraditionalIcdCode;
                if (!String.IsNullOrWhiteSpace(data.TraditionalIcdName)) treatment.TRADITIONAL_ICD_NAME = data.TraditionalIcdName;
                if (!String.IsNullOrWhiteSpace(data.TraditionalIcdSubCode)) treatment.TRADITIONAL_ICD_SUB_CODE = data.TraditionalIcdSubCode;
                if (!String.IsNullOrWhiteSpace(data.TraditionalIcdText)) treatment.TRADITIONAL_ICD_TEXT = data.TraditionalIcdText;
                treatment.TDL_PATIENT_CLASSIFY_ID = data.PatientClassifyId;

                if (ValueChecker.IsPrimitiveDiff<HIS_TREATMENT>(beforeUpdate, treatment)
                    && !this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                {
                    throw new Exception("Cap nhat thong tin DEPARTMENT_IDS cho bang Treatment that bai. Rollback du lieu");
                }
            }
        }

        private void SetServerTime(HisDepartmentTranReceiveSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                data.InTime = now;
            }
        }

        internal void RollbackData()
        {
            this.hisTreatmentUpdate.RollbackData();
            this.hisBedLogCreate.RollbackData();
            this.hisTreatmentBedRoomCreate.RollbackData();
            this.hisPatientTypeAlterCreate.RollbackData();
            this.hisDepartmentTranUpdate.RollbackData();
        }
    }
}
