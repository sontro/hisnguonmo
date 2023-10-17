using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment.Update.Finish;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.FinishCheck
{
    class HisTreatmentFinishCheckProcessor : BusinessBase
    {
        internal HisTreatmentFinishCheckProcessor()
            : base()
        {
        }

        internal HisTreatmentFinishCheckProcessor(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(HisTreatmentFinishSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HIS_PROGRAM program = null;
                WorkPlaceSDO workPlace = null;
                HIS_DEPARTMENT_TRAN lastDt = null;
                V_HIS_DEATH_CERT_BOOK deathCertBook = null;
                List<HIS_SERVICE_REQ> serviceReqs = null;
                List<HIS_BED_LOG> bedLogs = null;

                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                HisTreatmentFinishCheck fChecker = new HisTreatmentFinishCheck(param);
                HisDepartmentTranCheck dtChecker = new HisDepartmentTranCheck(param);
                HisTreatmentCheckPrescription presChecker = new HisTreatmentCheckPrescription(param);
                HisTreatmentBedRoomCheck bedroomChecker = new HisTreatmentBedRoomCheck(param);

                valid = valid && checker.VerifyId(data.TreatmentId, ref raw);

                List<HIS_SERE_SERV> existsSereServs = new HisSereServGet().GetByTreatmentId(raw.ID);
                List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(raw.ID);
                HIS_PATIENT_TYPE_ALTER lastPta = ptas != null ? ptas.OrderByDescending(o => o.LOG_TIME).FirstOrDefault() : null;

                valid = checker.IsUnLock(raw) && valid;  // Kiem tra ho so chua khoa vien phi
                valid = checker.IsUnTemporaryLock(raw) && valid;  // Kiem tra ho so chua tam khoa vien phi
                valid = checker.IsUnpause(raw) && valid;  // Kiem tra ho so chua ket thuc dieu tri
                valid = this.HasWorkPlaceInfo(data.EndRoomId, ref workPlace) && valid;  // Kiem tra Phong lam viec hien tai co dung khoa ma benh nhan dang dieu tri
                valid = checker.IsValidDepartment(data.TreatmentId, workPlace, ref lastDt) && valid;
                valid = fChecker.VerifyRequireField2(data, lastPta, ref program) && valid;
                valid = fChecker.IsValidBhytClinicalEmergencyBedPolicy(data, raw, existsSereServs) && valid;  // Kiem tra thong tin ho so cap cuu da chi dinh giuong co thoi gian dieu tri nho hon 4 gio
                valid = fChecker.IsValidOutTime(raw, data, existsSereServs, ptas, lastPta, lastDt) && valid;  // Kiem tra thong tin ket qua, loai ra vien (neu co)
                valid = fChecker.FinishTimeIsNotGreatherThenCurrentTime(data) && valid;  // Kiem tra thoi gian ra vien theo cau hinh he thong
                valid = fChecker.VerifyFinishServiceReq(data, raw, workPlace, null, existsSereServs, ref serviceReqs) && valid;  // Kiem tra thoi gian ra vien so voi thoi gian vao vien, thoi gian ra khoa, thoi gian ket thuc y lenh, thoi gian ket thuc dich vu theo cac cau hinh he thong
                valid = fChecker.IsValidMinimumTimesForExam(raw, serviceReqs, data.TreatmentFinishTime) && valid;  // Kiem tra thoi gian xu ly dich vu theo cau hinh
                valid = fChecker.IsIntructionTimeNotGreaterThanFinishTime(serviceReqs) && valid;  // Kiem tra thoi gian y lenh va thoi gian ket thuc y lenh theo cau hinh
                valid = fChecker.VerifyApproveRation(serviceReqs, lastPta) && valid;  // Kiem tra thong tin duyet suat an theo cau hinh
                valid = presChecker.HasNoPostponePrescriptionByFinish(serviceReqs, lastPta, lastDt) && valid;  // Kiem tra thong tin linh thuoc theo cau hinh
                valid = dtChecker.IsFinishCoTreatment(lastDt) && valid;  // Kiem tra thong tin đieu tri ket hop
                valid = dtChecker.HasNoTempBed(raw.ID, lastDt.DEPARTMENT_ID) && valid;  // Kiem tra thong tin giuong tam tinh (da bo)
                valid = presChecker.IsMustApproveMobaPress(raw) && valid;  // Kiem tra thong tin tra thuoc theo cau hinh
                valid = fChecker.VerifyTimeSereServExt(data, raw, existsSereServs) && valid;  // Kiem tra thoi gian xu ly dich vu BEGIN_TIME nho hơn IN_TIME va END_TIME lon hon OUT_TIME
                valid = checker.VerifyTreatmentFinishCheckTracking(data) && valid;  // Kiem tra thoi gian to dieu tri khong duoc lon hon thoi gian ra vien
                valid = fChecker.VerifyBlockAppointment(data, lastPta) && valid;  // Kiem tra thong tin thoi gian hen kham(neu cc) theo cau hinh
                valid = fChecker.IsValidDeathCertBook(data, workPlace, ref deathCertBook) && valid;  // Kiem tra thong tin so tu vong (neu co)
                valid = fChecker.IsFinishMandatoryService(raw, existsSereServs) && valid;
                valid = fChecker.IsValidIcdCode(raw,data) && valid;
                valid = (!data.IsExpXml4210Collinear || fChecker.HasXml4210CollinearFolderPath()) && valid;

                List<HIS_TREATMENT_BED_ROOM> treatmentBedRooms = new HisTreatmentBedRoomGet().GetCurrentInByTreatmentId(raw.ID);
                if (IsNotNullOrEmpty(treatmentBedRooms))
                {
                    valid = bedroomChecker.IsValidRemoveTime(treatmentBedRooms, data.TreatmentFinishTime, ref bedLogs) && valid;
                }
                

                result = valid;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
