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
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using AutoMapper;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisTreatment.Util;
using Inventec.Common.ObjectChecker;
using MOS.MANAGER.EventLogUtil;
using MOS.UTILITY;

namespace MOS.MANAGER.HisDepartmentTran.Create
{
    class HisDepartmentTranCreate : BusinessBase
    {
        private HIS_DEPARTMENT_TRAN recentHisDepartmentTran;

        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisTreatmentBedRoomRemove hisTreatmentBedRoomRemove;
        private HisTreatmentBedRoomCreate hisTreatmentBedRoomCreate;
        private HisPatientTypeAlterCreate hisPatientTypeAlterCreate;
        private HisSereServUpdateHein hisSereServUpdateHein;

        internal HisDepartmentTranCreate()
            : base()
        {
            this.Init();
        }

        internal HisDepartmentTranCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTreatmentBedRoomRemove = new HisTreatmentBedRoomRemove(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisTreatmentBedRoomCreate = new HisTreatmentBedRoomCreate(param);
            this.hisPatientTypeAlterCreate = new HisPatientTypeAlterCreate(param);
        }

        internal bool Create(HisDepartmentTranSDO data, bool firstCreating, ref HIS_DEPARTMENT_TRAN resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_TREATMENT treatment = null;
                HIS_DEPARTMENT department = null;
                HIS_DEPARTMENT_TRAN lastDt = null;
                List<HIS_DEPARTMENT_TRAN> allDts = null;
                HisTreatmentCheckPrescription checkPres = new HisTreatmentCheckPrescription(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                HisDepartmentTranCheck checker = new HisDepartmentTranCheck(param);
                HisDepartmentTranCreateCheck createChecker = new HisDepartmentTranCreateCheck(param);

                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && treatmentChecker.VerifyId(data.TreatmentId, ref treatment);
                valid = valid && checker.IsValidWithReqDepartment(workPlace.DepartmentId, data.TreatmentId);
                valid = valid && (firstCreating || checkPres.HasNoPostponePrescriptionByDepartmentTran(workPlace.DepartmentId, data.TreatmentId));
                valid = valid && (firstCreating || createChecker.IsValidDepartment(data, workPlace, ref lastDt, ref allDts));
                valid = valid && (firstCreating || checker.IsFinishCoTreatment(lastDt));
                valid = valid && (firstCreating || treatment.IS_OLD_TEMP_BED != Constant.IS_TRUE || checker.HasNoTempBed(data.TreatmentId, lastDt.DEPARTMENT_ID));
                if (valid)
                {
                    long? newBedRoomId = null;
                    this.ProcessDepartmentTran(data, lastDt, firstCreating, ref newBedRoomId, ref department);

                    new TemporaryBedProcessor(param).Run(data, treatment, workPlace);

                    this.ProcessTreatmentBedRoom(data, firstCreating, lastDt, newBedRoomId);
                    this.ProcessTreatment(firstCreating, allDts, ref treatment);
                    this.ProcessRecalcSereServ(treatment, this.recentHisDepartmentTran);
                    this.PassResult(ref resultData);
                    result = true;
                    if (!firstCreating)
                    {
                        HisDepartmentTranLog.LogCreate(treatment, department, this.recentHisDepartmentTran, workPlace);
                    }
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

        private void ProcessDepartmentTran(HisDepartmentTranSDO data, HIS_DEPARTMENT_TRAN lastDt, bool firstCreating, ref long? bedRoomId, ref HIS_DEPARTMENT department)
        {
            //ko phai tao lan dau tien (luc tiep don) thi moi xu ly tiep
            HIS_DEPARTMENT_TRAN toInsert = new HIS_DEPARTMENT_TRAN();
            toInsert.DEPARTMENT_ID = data.DepartmentId;
            toInsert.ICD_CODE = data.IcdCode;
            toInsert.ICD_NAME = data.IcdName;
            toInsert.ICD_SUB_CODE = data.IcdSubCode;
            toInsert.ICD_TEXT = data.IcdText;
            toInsert.TREATMENT_ID = data.TreatmentId;
            toInsert.PREVIOUS_ID = lastDt != null ? (long?)lastDt.ID : null;
            toInsert.REQUEST_TIME = data.Time > 0 ? data.Time : Inventec.Common.DateTime.Get.Now();

            bool isAuto = false;

            if (!firstCreating)
            {
                ///Neu la chuyen khoa thi kiem tra xem khoa tiep nhan co duoc cau hinh tu dong nhan benh nhan hay khong
                ///Neu co cau hinh tu dong nhan thi thuc hien tao du lieu tiep nhan chuyen khoa
                HIS_DEPARTMENT inDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == data.DepartmentId).FirstOrDefault();
                department = inDepartment;
                if (inDepartment.IS_AUTO_RECEIVE_PATIENT == MOS.UTILITY.Constant.IS_TRUE)
                {
                    HIS_PATIENT_TYPE_ALTER patientTypeAlter = new HisPatientTypeAlterGet().GetLastByTreatmentId(data.TreatmentId);
                    isAuto = true;

                    //Neu la benh nhan noi tru thi kiem tra xem khoa co bao nhieu buong,
                    //neu chi co 1 buong thi tu dong tiep nhan vao buong do, neu co nhieu buong thi bo qua nghiep vu "tu dong"
                    if (patientTypeAlter != null && patientTypeAlter.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                    {
                        List<V_HIS_BED_ROOM> bedRooms = HisBedRoomCFG.DATA.Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && o.DEPARTMENT_ID == inDepartment.ID).ToList();
                        if (bedRooms != null && bedRooms.Count == 1)
                        {
                            bedRoomId = bedRooms[0].ID;
                        }
                        else
                        {
                            isAuto = false;
                        }
                    }
                }
            }

            //set thoi gian vao khoa bang thoi gian ra khoa hien tai
            toInsert.DEPARTMENT_IN_TIME = data.IsReceive || isAuto ? (long?)data.Time : null;
            toInsert.IS_HOSPITALIZED = data.IsHospitalized ? (short?)Constant.IS_TRUE : null;

            if (!DAOWorker.HisDepartmentTranDAO.Create(toInsert))
            {
                throw new Exception("Tao thong tin departmentTran that bai. Nghiep vu tiep theo se khong thuc hien duoc");
            }
            this.recentHisDepartmentTran = toInsert;
        }

        private void ProcessTreatmentBedRoom(HisDepartmentTranSDO data, bool firstCreating, HIS_DEPARTMENT_TRAN lastDt, long? newBedRoomId)
        {
            //neu la ban ghi chuyen khoa dau tien ==> duoc tao luc dang ky tiep don ==> ko can check
            if (!firstCreating)
            {
                //Neu nguoi dung chon "tu dong roi phong"
                if (data.AutoLeaveRoom)
                {
                    List<HIS_TREATMENT_BED_ROOM> treatmentBedRooms = new HisTreatmentBedRoomGet().GetCurrentInByTreatmentId(data.TreatmentId);

                    if (IsNotNullOrEmpty(treatmentBedRooms))
                    {
                        //lay ra danh sach HIS_TREATMENT_BED_ROOM ma co bed_room_id thuoc khoa chuyen di
                        List<V_HIS_BED_ROOM> vBedRooms = HisBedRoomCFG.DATA.Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && o.DEPARTMENT_ID == lastDt.DEPARTMENT_ID).ToList();
                        List<HIS_TREATMENT_BED_ROOM> toUpdate = IsNotNullOrEmpty(vBedRooms) ? treatmentBedRooms.Where(o => vBedRooms.Where(t => t.ID == o.BED_ROOM_ID).Any()).ToList() : null;
                        if (IsNotNullOrEmpty(toUpdate))
                        {
                            List<HIS_TREATMENT_BED_ROOM> tmp = null;
                            if (!this.hisTreatmentBedRoomRemove.Remove(toUpdate, data.Time, false, ref tmp))
                            {
                                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                            }
                        }
                    }
                }

                //Neu co tu dong tiep nhan vao buong cua khoa moi
                if (newBedRoomId.HasValue)
                {
                    HIS_TREATMENT_BED_ROOM treatmentBedRoom = new HIS_TREATMENT_BED_ROOM();
                    treatmentBedRoom.BED_ROOM_ID = newBedRoomId.Value;
                    treatmentBedRoom.TREATMENT_ID = data.TreatmentId;
                    treatmentBedRoom.ADD_TIME = data.Time;
                    if (!hisTreatmentBedRoomCreate.Create(treatmentBedRoom))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                }
            }
        }

        private void ProcessTreatment(bool firstCreating, List<HIS_DEPARTMENT_TRAN> allDts, ref HIS_TREATMENT treatment)
        {
            //Bat buoc lay treatment moi nhat ve tranh truong hop treatment duoc update o buoc xu ly dong doi tuong
            treatment = new HisTreatmentGet().GetById(this.recentHisDepartmentTran.TREATMENT_ID);
            if (this.recentHisDepartmentTran.DEPARTMENT_IN_TIME.HasValue && !firstCreating)
            {
                allDts.Add(this.recentHisDepartmentTran);

                if (treatment != null)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);

                    HisTreatmentUtil.SetDepartmentInfo(treatment, allDts);

                    if (ValueChecker.IsPrimitiveDiff<HIS_TREATMENT>(beforeUpdate, treatment)
                        && !this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                    {
                        throw new Exception("Cap nhat thong tin DEPARTMENT_IDS cho bang Treatment that bai. Rollback du lieu");
                    }
                }
            }
        }

        private void PassResult(ref HIS_DEPARTMENT_TRAN resultData)
        {
            resultData = this.recentHisDepartmentTran;
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

        internal void RollbackData()
        {
            this.hisTreatmentUpdate.RollbackData();
            //Rollback his_department_Tran
            if (this.recentHisDepartmentTran != null)
            {
                if (!DAOWorker.HisDepartmentTranDAO.Truncate(this.recentHisDepartmentTran))
                {
                    LogSystem.Warn("Xoa thong tin his_department_Tran that bai. Can kiem tra lai log.");
                }
                this.recentHisDepartmentTran = null;
            }
        }
    }
}
