using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisServiceRoom;
using AutoMapper;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.EventLogUtil;
using MOS.UTILITY;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisNumOrderIssue;

namespace MOS.MANAGER.HisServiceReq.Exam.Change
{
    /// <summary>
    /// Xu ly nghiep vu sua chi dinh kham
    /// </summary>
    partial class HisServiceReqExamChange : BusinessBase
    {
        private HisServiceReqCreate hisServiceReqCreate;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServCreate hisSereServCreate;
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisSereServUpdate hisSereServUpdate;
        private HisDepartmentTranUpdate hisDepartmentTranUpdate;

        internal HisServiceReqExamChange()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqExamChange(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisDepartmentTranUpdate = new HisDepartmentTranUpdate(param);
        }

        internal bool Run(HisServiceReqExamChangeSDO data, ref HisServiceReqResultSDO resultData)
        {
            bool result = false;
            try
            {
                this.SetServerTime(data);
                HIS_SERVICE_REQ currentServiceReq = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqExamCheck examChecker = new HisServiceReqExamCheck(param);
                HisServiceReqExamChangeCheck changeChecker = new HisServiceReqExamChangeCheck(param);
                HIS_TREATMENT treatment = null;
                WorkPlaceSDO workPlace = null;
                bool valid = data != null;
                valid = valid && checker.VerifyId(data.CurrentServiceReqId, ref currentServiceReq);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && changeChecker.IsValidData(data, data.CurrentServiceReqId, workPlace, ref currentServiceReq);
                valid = valid && (HisServiceReqCFG.ALLOW_MODIFYING_OF_STARTED == HisServiceReqCFG.AllowModifyingStartedOption.ALL
                    || HisServiceReqCFG.ALLOW_MODIFYING_OF_STARTED == HisServiceReqCFG.AllowModifyingStartedOption.JUST_EXAM
                    || checker.IsNotStarted(currentServiceReq));
                valid = valid && checker.HasExecute(currentServiceReq);
                valid = valid && treatmentChecker.VerifyId(currentServiceReq.TREATMENT_ID, ref treatment);
                valid = valid && treatmentChecker.IsUnLock(treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && HisServiceRoomUtil.IsProcessable(data.RoomId, data.ServiceId, param);
                valid = valid && (!currentServiceReq.TREATMENT_TYPE_ID.HasValue || data.RoomId == currentServiceReq.EXECUTE_ROOM_ID || examChecker.IsNotExceedLimit(data.RoomId, currentServiceReq.TREATMENT_ID, currentServiceReq.TREATMENT_TYPE_ID.Value, currentServiceReq.INTRUCTION_TIME));

                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ beforeUpdateSr = Mapper.Map<HIS_SERVICE_REQ>(currentServiceReq);

                    HIS_SERVICE_REQ resultServiceReq = null;
                    List<HIS_SERE_SERV> allSereServs = new HisSereServGet().GetByTreatmentId(currentServiceReq.TREATMENT_ID);
                    HIS_SERE_SERV currentSereServ = allSereServs != null ? allSereServs.Where(o => o.SERVICE_REQ_ID == currentServiceReq.ID && o.IS_NO_EXECUTE != Constant.IS_TRUE).FirstOrDefault() : null;
                    List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters = new HisPatientTypeAlterGet().GetByTreatmentId(currentServiceReq.TREATMENT_ID);

                    HIS_SERVICE_REQ newSr = this.MakeNewServiceReq(treatment, data, currentServiceReq, workPlace);

                    //neu thong tin phong xu ly va dich vu khong thay doi thi ko tao dich vu moi
                    //chi cap nhat thong tin yeu cau hien tai
                    if (currentSereServ != null
                        && currentSereServ.SERVICE_ID == data.ServiceId
                        && currentServiceReq.EXECUTE_ROOM_ID == data.RoomId
                        && currentSereServ.PATIENT_TYPE_ID == data.PatientTypeId)
                    {
                        //Neu thay doi ngay thi xu ly de lay lai STT kham
                        long oldDay = Inventec.Common.DateTime.Get.StartDay(currentServiceReq.INTRUCTION_TIME).Value;
                        long newDay = Inventec.Common.DateTime.Get.StartDay(data.InstructionTime).Value;

                        currentServiceReq.INTRUCTION_TIME = data.InstructionTime;
                        currentServiceReq.PRIORITY = data.Priority ? new Nullable<long>(Constant.IS_TRUE) : null;
                        currentServiceReq.IS_NOT_REQUIRE_FEE = data.IsNotRequireFee ? new Nullable<short>(Constant.IS_TRUE) : null;

                        if (oldDay != newDay)
                        {
                            new HisNumOrderIssueUtil(param).SetNumOrder(currentServiceReq, null, null, null);
                        }

                        
                        if (!this.hisServiceReqUpdate.Update(currentServiceReq, beforeUpdateSr, false))
                        {
                            throw new Exception("Rollback du lieu");
                        }

                        Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                        HIS_SERE_SERV beforeSS = Mapper.Map<HIS_SERE_SERV>(currentSereServ);

                        currentSereServ.PRIMARY_PATIENT_TYPE_ID = data.PrimaryPatientTypeId;

                        if (!this.hisSereServUpdate.Update(currentSereServ, beforeSS))
                        {
                            throw new Exception("CAp nhat sere serv that bai . Rollback du lieu");
                        }
                        resultServiceReq = currentServiceReq;
                    }
                    else
                    {

                        HIS_SERE_SERV newSereServ = this.MakeNewSereServ(treatment, newSr, currentSereServ.ID, data, allSereServs, patientTypeAlters);

                        //Neu co cau hinh ko tao ra sere_serv, service_req moi trong truong hop co cung gia
                        //Thi thuc hien update lai sere_serv va service_req hien tai
                        //Nguoc lai thi thuc hien tao moi, dong thoi co check da tam ung, hoa don hay chua
                        if (HisServiceReqCFG.CHANGING_EXAM_OPTION == HisServiceReqCFG.ChangingExamOption.NO_CREATE_NEW_IF_PAID_SAME_PRICE_AND_NON_BHYT
                            && currentSereServ.PATIENT_TYPE_ID == data.PatientTypeId
                            && data.PatientTypeId != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                            && newSereServ.PRIMARY_PRICE == currentSereServ.PRIMARY_PRICE)
                        {
                            this.UpdateCurrentServiceReq(currentServiceReq, newSr);
                            this.UpdateCurrentSereServ(currentServiceReq, currentSereServ, data.ServiceId);

                            resultServiceReq = currentServiceReq;
                        }
                        else
                        {
                            bool check = true;

                            HisSereServCheck sereServChecker = new HisSereServCheck(param);
                            check = check && sereServChecker.HasNoBill(currentSereServ);
                            check = check && sereServChecker.HasNoInvoice(currentSereServ);
                            check = check && sereServChecker.HasNoDeposit(currentSereServ.ID, true);
                            if (check)
                            {
                                if (HisServiceReqCFG.CHANGING_EXAM_OPTION == HisServiceReqCFG.ChangingExamOption.NO_CREATE_NEW_IF_UNPAID)
                                {
                                    this.UpdateCurrentServiceReq(currentServiceReq, newSr);
                                    //Tao sere_serv moi gan voi service_req cu
                                    newSereServ = this.MakeNewSereServ(treatment, currentServiceReq, currentSereServ.ID, data, allSereServs, patientTypeAlters);
                                    this.UpdateNoExecuteCurrentSereServ(currentSereServ);
                                    this.CreateNewSereServ(treatment, currentServiceReq, newSereServ, allSereServs);
                                    resultServiceReq = currentServiceReq;
                                }
                                else
                                {
                                    this.UpdateNoExecuteCurrentServiceReq(currentServiceReq, beforeUpdateSr);
                                    this.UpdateNoExecuteCurrentSereServ(currentSereServ);
                                    this.CreateNewServiceReq(newSr, treatment, ref resultServiceReq);
                                    this.CreateNewSereServ(treatment, newSr, newSereServ, allSereServs);
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }

                        this.ProcessDepartmentTran(patientTypeAlters, beforeUpdateSr, resultServiceReq);
                    }
                    result = true;
                    this.PassResult(resultServiceReq, ref resultData);
                    HisServiceReqLog.Run(currentServiceReq, new List<HIS_SERE_SERV>() { currentSereServ }, resultData.ServiceReq, resultData.SereServs, LibraryEventLog.EventLog.Enum.HisServiceReq_SuaChiDinhKham);
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

        private void UpdateCurrentServiceReq(HIS_SERVICE_REQ currentServiceReq, HIS_SERVICE_REQ newSr)
        {
            Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
            HIS_SERVICE_REQ beforeUpdateSr = Mapper.Map<HIS_SERVICE_REQ>(currentServiceReq);

            //Neu thay doi ngay thi xu ly de lay lai STT kham
            long oldDay = Inventec.Common.DateTime.Get.StartDay(currentServiceReq.INTRUCTION_TIME).Value;
            long newDay = Inventec.Common.DateTime.Get.StartDay(newSr.INTRUCTION_TIME).Value;
            long oldRomId = currentServiceReq.EXECUTE_ROOM_ID;
            long newRomId = newSr.EXECUTE_ROOM_ID;

            currentServiceReq.INTRUCTION_TIME = newSr.INTRUCTION_TIME;
            currentServiceReq.PRIORITY = newSr.PRIORITY;
            currentServiceReq.IS_NOT_REQUIRE_FEE = newSr.IS_NOT_REQUIRE_FEE;
            currentServiceReq.REQUEST_ROOM_ID = newSr.REQUEST_ROOM_ID;
            currentServiceReq.REQUEST_DEPARTMENT_ID = newSr.REQUEST_DEPARTMENT_ID;
            currentServiceReq.EXECUTE_ROOM_ID = newSr.EXECUTE_ROOM_ID;
            currentServiceReq.EXECUTE_DEPARTMENT_ID = newSr.EXECUTE_DEPARTMENT_ID;
            currentServiceReq.TDL_SERVICE_IDS = newSr.TDL_SERVICE_IDS;

            if (oldDay != newDay || oldRomId != newRomId)
            {
                new HisNumOrderIssueUtil(param).SetNumOrder(currentServiceReq, null, null, null);
            }

            //Neu y lenh cu co chi dinh nguoi xu ly thi can cap nhat lai thong tin nguoi xu ly theo phong moi
            if (!string.IsNullOrWhiteSpace(currentServiceReq.ASSIGNED_EXECUTE_LOGINNAME))
            {
                currentServiceReq.ASSIGNED_EXECUTE_LOGINNAME = newSr.ASSIGNED_EXECUTE_LOGINNAME;
                currentServiceReq.ASSIGNED_EXECUTE_USERNAME = newSr.ASSIGNED_EXECUTE_USERNAME;
            }



            if (!this.hisServiceReqUpdate.Update(currentServiceReq, beforeUpdateSr, false))
            {
                throw new Exception("Rollback du lieu");
            }
        }

        private void UpdateCurrentSereServ(HIS_SERVICE_REQ currentServiceReq, HIS_SERE_SERV currentSereServ, long newServiceId)
        {
            Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
            HIS_SERE_SERV beforeUpdateSs = Mapper.Map<HIS_SERE_SERV>(currentSereServ);

            currentSereServ.SERVICE_ID = newServiceId;
            HisSereServUtil.SetTdl(currentSereServ, currentServiceReq);

            if (!this.hisSereServUpdate.Update(currentSereServ, beforeUpdateSs))
            {
                throw new Exception("Rollback du lieu");
            }
        }

        /// <summary>
        /// Xu ly cap nhat lai thong tin vao khoa neu dap ung cac dieu kien sau:
        /// - Khoa cua phieu chi dinh moi khac phieu chi dinh cu
        /// - Dich vu kham la dich vu dau tien cua BN
        /// - Doi tuong BN la kham
        /// </summary>
        /// <param name="currentServiceReq"></param>
        /// <param name="newServiceReq"></param>
        private void ProcessDepartmentTran(List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters, HIS_SERVICE_REQ beforeServiceReq, HIS_SERVICE_REQ newServiceReq)
        {
            //Neu khoa moi ko khac khoa cu thi bo qua
            if (beforeServiceReq == null || newServiceReq == null || beforeServiceReq.EXECUTE_DEPARTMENT_ID == newServiceReq.EXECUTE_DEPARTMENT_ID)
            {
                return;
            }

            //so luong != 2 (gom chi dinh cu va chi dinh vua tao moi)
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.HAS_EXECUTE = true;
            filter.TREATMENT_ID = beforeServiceReq.TREATMENT_ID;
            List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);
            if (!IsNotNullOrEmpty(serviceReqs) || serviceReqs.Count > 2)
            {
                return;
            }

            //Kiem tra xem BN da chuyen khoa lan nao chua, neu da tung chuyen khoa thi bo qua
            List<HIS_DEPARTMENT_TRAN> departmentTrans = new HisDepartmentTranGet().GetByTreatmentId(beforeServiceReq.TREATMENT_ID);
            if (!IsNotNullOrEmpty(departmentTrans) || departmentTrans.Count != 1)
            {
                return;
            }

            //Kiem tra xem neu BN ko phai la kham thi bo qua

            if (!IsNotNullOrEmpty(patientTypeAlters) || patientTypeAlters.Count != 1 || patientTypeAlters[0].TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
            {
                return;
            }

            //Neu dap ung tat ca cac y/c tren thi thuc hien cap nhat
            Mapper.CreateMap<V_HIS_DEPARTMENT_TRAN, HIS_DEPARTMENT_TRAN>();
            HIS_DEPARTMENT_TRAN currentDepartmentTran = Mapper.Map<HIS_DEPARTMENT_TRAN>(departmentTrans[0]);
            HIS_DEPARTMENT_TRAN beforeUpdate = Mapper.Map<HIS_DEPARTMENT_TRAN>(departmentTrans[0]);
            currentDepartmentTran.DEPARTMENT_ID = newServiceReq.EXECUTE_DEPARTMENT_ID;
            if (!this.hisDepartmentTranUpdate.Update(currentDepartmentTran, beforeUpdate))
            {
                LogSystem.Warn("Tu dong cap nhat thong tin khoa that bai. beforeUpdate: " + LogUtil.TraceData("beforeUpdate", beforeUpdate));
            }
        }

        private void RollbackData()
        {
            this.hisSereServUpdate.RollbackData();
            this.hisServiceReqUpdate.RollbackData();
            this.hisSereServCreate.RollbackData();
            this.hisServiceReqCreate.RollbackData();
            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
        }

        private void PassResult(HIS_SERVICE_REQ newServiceReq, ref HisServiceReqResultSDO resultData)
        {
            if (newServiceReq != null)
            {
                resultData = new HisServiceReqResultSDO();
                resultData.ServiceReq = new HisServiceReqGet().GetViewById(newServiceReq.ID);
                resultData.SereServs = new HisSereServGet().GetViewByServiceReqId(newServiceReq.ID);
            }
        }

        private void UpdateNoExecuteCurrentServiceReq(HIS_SERVICE_REQ currentServiceReq, HIS_SERVICE_REQ before)
        {
            if (currentServiceReq != null)
            {
                //cap nhat service_req cu thanh "ko thuc hien", bo "kham chinh"
                currentServiceReq.IS_NO_EXECUTE = MOS.UTILITY.Constant.IS_TRUE;
                currentServiceReq.IS_MAIN_EXAM = null;//bo "kham chinh" tranh truong hop nguoi dung bo check "ko thuc hien"
                if (!this.hisServiceReqUpdate.Update(currentServiceReq, before, false))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        private void UpdateNoExecuteCurrentSereServ(HIS_SERE_SERV currentSereServ)
        {
            if (currentSereServ != null)
            {
                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                //luu lai de phuc vu rollback
                HIS_SERE_SERV beforeUpdate = Mapper.Map<HIS_SERE_SERV>(currentSereServ);

                //cap nhat sere_serv cu thanh "ko thuc hien"
                currentSereServ.IS_NO_EXECUTE = MOS.UTILITY.Constant.IS_TRUE;
                currentSereServ.TDL_IS_MAIN_EXAM = null;

                if (!this.hisSereServUpdate.Update(currentSereServ, beforeUpdate))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        private void CreateNewServiceReq(HIS_SERVICE_REQ newServiceReq, HIS_TREATMENT treatment, ref HIS_SERVICE_REQ resultData)
        {
            //Cap STT kham
            new HisNumOrderIssueUtil(param).SetNumOrder(newServiceReq, null, null, null);

            if (!this.hisServiceReqCreate.Create(newServiceReq, treatment))
            {
                throw new Exception("Ket thuc nghiep vu");
            }

            resultData = newServiceReq;
        }

        private void CreateNewSereServ(HIS_TREATMENT treatment, HIS_SERVICE_REQ newServiceReq, HIS_SERE_SERV toInsertSereServ, List<HIS_SERE_SERV> existSereServs)
        {
            toInsertSereServ.SERVICE_REQ_ID = newServiceReq.ID;
            if (!this.hisSereServCreate.Create(toInsertSereServ, newServiceReq, false))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }

            this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, false);

            Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
            List<HIS_SERE_SERV> olds = Mapper.Map<List<HIS_SERE_SERV>>(existSereServs);

            List<HIS_SERE_SERV> allSereServs = new List<HIS_SERE_SERV>();
            allSereServs.Add(toInsertSereServ);
            if (existSereServs != null)
            {
                allSereServs.AddRange(existSereServs);
            }
            //Cap nhat ti le BHYT cho sere_serv
            if (!this.hisSereServUpdateHein.UpdateDb(allSereServs, allSereServs))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }
        }

        private HIS_SERVICE_REQ MakeNewServiceReq(HIS_TREATMENT treatment, HisServiceReqExamChangeSDO data, HIS_SERVICE_REQ currentServiceReq, WorkPlaceSDO workPlace)
        {
            V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == data.RoomId).FirstOrDefault();
            HIS_SERVICE_REQ newServiceReq = new HIS_SERVICE_REQ();
            newServiceReq.EXECUTE_DEPARTMENT_ID = room.DEPARTMENT_ID;
            newServiceReq.EXECUTE_ROOM_ID = data.RoomId;
            newServiceReq.INTRUCTION_TIME = data.InstructionTime;
            newServiceReq.PARENT_ID = currentServiceReq.PARENT_ID;
            newServiceReq.PRIORITY = data.Priority ? new Nullable<long>(Constant.IS_TRUE) : null;
            newServiceReq.IS_NOT_REQUIRE_FEE = data.IsNotRequireFee ? new Nullable<short>(Constant.IS_TRUE) : null;
            newServiceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
            newServiceReq.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH;
            newServiceReq.TREATMENT_ID = currentServiceReq.TREATMENT_ID;
            newServiceReq.IS_MAIN_EXAM = currentServiceReq.IS_MAIN_EXAM;
            newServiceReq.REQUEST_ROOM_ID = data.RequestRoomId;
            newServiceReq.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
            newServiceReq.PREVIOUS_SERVICE_REQ_ID = currentServiceReq.PREVIOUS_SERVICE_REQ_ID;
            newServiceReq.TREATMENT_TYPE_ID = currentServiceReq.TREATMENT_TYPE_ID;

            //Neu y lenh cu co chi dinh nguoi xu ly thi can cap nhat lai thong tin nguoi xu ly theo phong moi
            if (!string.IsNullOrWhiteSpace(currentServiceReq.ASSIGNED_EXECUTE_LOGINNAME))
            {
                newServiceReq.ASSIGNED_EXECUTE_LOGINNAME = room.RESPONSIBLE_LOGINNAME;
                newServiceReq.ASSIGNED_EXECUTE_USERNAME = room.RESPONSIBLE_USERNAME;
            }

            if (data.IsCopyOldInfo)
            {
                newServiceReq.HOSPITALIZATION_REASON = currentServiceReq.HOSPITALIZATION_REASON;
                newServiceReq.PATHOLOGICAL_HISTORY = currentServiceReq.PATHOLOGICAL_HISTORY;
                newServiceReq.PATHOLOGICAL_HISTORY_FAMILY = currentServiceReq.PATHOLOGICAL_HISTORY_FAMILY;
                newServiceReq.PATHOLOGICAL_PROCESS = currentServiceReq.PATHOLOGICAL_PROCESS;
                newServiceReq.PATIENT_CASE_ID = currentServiceReq.PATIENT_CASE_ID;
                newServiceReq.SICK_DAY = currentServiceReq.SICK_DAY;
                newServiceReq.PART_EXAM = currentServiceReq.PART_EXAM;
                newServiceReq.PART_EXAM_CIRCULATION = currentServiceReq.PART_EXAM_CIRCULATION;
                newServiceReq.PART_EXAM_DIGESTION = currentServiceReq.PART_EXAM_DIGESTION;
                newServiceReq.PART_EXAM_EAR = currentServiceReq.PART_EXAM_EAR;
                newServiceReq.PART_EXAM_ENT = currentServiceReq.PART_EXAM_ENT;
                newServiceReq.PART_EXAM_EYE = currentServiceReq.PART_EXAM_EYE;
                newServiceReq.PART_EXAM_EYESIGHT_GLASS_LEFT = currentServiceReq.PART_EXAM_EYESIGHT_GLASS_LEFT;
                newServiceReq.PART_EXAM_EYESIGHT_GLASS_RIGHT = currentServiceReq.PART_EXAM_EYESIGHT_GLASS_RIGHT;
                newServiceReq.PART_EXAM_EYESIGHT_LEFT = currentServiceReq.PART_EXAM_EYESIGHT_LEFT;
                newServiceReq.PART_EXAM_EYESIGHT_RIGHT = currentServiceReq.PART_EXAM_EYESIGHT_RIGHT;
                newServiceReq.PART_EXAM_EYE_TENSION_LEFT = currentServiceReq.PART_EXAM_EYE_TENSION_LEFT;
                newServiceReq.PART_EXAM_EYE_TENSION_RIGHT = currentServiceReq.PART_EXAM_EYE_TENSION_RIGHT;
                newServiceReq.DHST_ID = currentServiceReq.DHST_ID;
                newServiceReq.FULL_EXAM = currentServiceReq.FULL_EXAM;
                newServiceReq.HEALTH_EXAM_RANK_ID = currentServiceReq.HEALTH_EXAM_RANK_ID;
                newServiceReq.ICD_CAUSE_CODE = currentServiceReq.ICD_CAUSE_CODE;
                newServiceReq.ICD_CAUSE_NAME = currentServiceReq.ICD_CAUSE_NAME;
                newServiceReq.ICD_CODE = currentServiceReq.ICD_CODE;
                newServiceReq.ICD_NAME = currentServiceReq.ICD_NAME;
                newServiceReq.ICD_SUB_CODE = currentServiceReq.ICD_SUB_CODE;
                newServiceReq.ICD_TEXT = currentServiceReq.ICD_TEXT;
                newServiceReq.NEXT_TREATMENT_INSTRUCTION = currentServiceReq.NEXT_TREATMENT_INSTRUCTION;
                newServiceReq.NEXT_TREAT_INTR_CODE = currentServiceReq.NEXT_TREAT_INTR_CODE;
                newServiceReq.NOTE = currentServiceReq.NOTE;
                newServiceReq.PART_EXAM_KIDNEY_UROLOGY = currentServiceReq.PART_EXAM_KIDNEY_UROLOGY;
                newServiceReq.PART_EXAM_MENTAL = currentServiceReq.PART_EXAM_MENTAL;
                newServiceReq.PART_EXAM_MOTION = currentServiceReq.PART_EXAM_MOTION;
                newServiceReq.PART_EXAM_MUSCLE_BONE = currentServiceReq.PART_EXAM_MUSCLE_BONE;
                newServiceReq.PART_EXAM_NEUROLOGICAL = currentServiceReq.PART_EXAM_NEUROLOGICAL;
                newServiceReq.PART_EXAM_NOSE = currentServiceReq.PART_EXAM_NOSE;
                newServiceReq.PART_EXAM_NUTRITION = currentServiceReq.PART_EXAM_NUTRITION;
                newServiceReq.PART_EXAM_OBSTETRIC = currentServiceReq.PART_EXAM_OBSTETRIC;
                newServiceReq.PART_EXAM_OEND = currentServiceReq.PART_EXAM_OEND;
                newServiceReq.PART_EXAM_RESPIRATORY = currentServiceReq.PART_EXAM_RESPIRATORY;
                newServiceReq.PART_EXAM_STOMATOLOGY = currentServiceReq.PART_EXAM_STOMATOLOGY;
                newServiceReq.PART_EXAM_THROAT = currentServiceReq.PART_EXAM_THROAT;
                newServiceReq.PROVISIONAL_DIAGNOSIS = currentServiceReq.PROVISIONAL_DIAGNOSIS;
                newServiceReq.SUBCLINICAL = currentServiceReq.SUBCLINICAL;
                newServiceReq.TREATMENT_INSTRUCTION = currentServiceReq.TREATMENT_INSTRUCTION;
            }

            V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == data.ServiceId).FirstOrDefault();
            if (service != null)
            {
                newServiceReq.EXE_SERVICE_MODULE_ID = service.EXE_SERVICE_MODULE_ID.HasValue ? service.EXE_SERVICE_MODULE_ID : service.SETY_EXE_SERVICE_MODULE_ID;
            }
            HisServiceReqUtil.SetTdl(newServiceReq, treatment); //Luu du thua du lieu
            return newServiceReq;
        }

        private HIS_SERE_SERV MakeNewSereServ(HIS_TREATMENT treatment, HIS_SERVICE_REQ newServiceReq, long currentSereServId, HisServiceReqExamChangeSDO data, List<HIS_SERE_SERV> existSereServs, List<HIS_PATIENT_TYPE_ALTER> ptas)
        {
            HIS_SERE_SERV newSereServ = new HIS_SERE_SERV();
            newSereServ.SERVICE_ID = data.ServiceId;
            newSereServ.AMOUNT = 1;//fix so luong la 1 voi dich vu kham
            newSereServ.PATIENT_TYPE_ID = data.PatientTypeId;
            newSereServ.TDL_IS_MAIN_EXAM = newServiceReq.IS_MAIN_EXAM;
            newSereServ.OTHER_PAY_SOURCE_ID = treatment.OTHER_PAY_SOURCE_ID;
            newSereServ.PRIMARY_PATIENT_TYPE_ID = data.PrimaryPatientTypeId; // gan theo client gui

            HisSereServUtil.SetTdl(newSereServ, newServiceReq);

            newServiceReq.TDL_SERVICE_IDS = data.ServiceId.ToString();

            long executeBranchId = HisDepartmentCFG.DATA
                .Where(o => o.ID == newServiceReq.EXECUTE_DEPARTMENT_ID)
                .FirstOrDefault().BRANCH_ID;

            List<HIS_SERE_SERV> alls = new List<HIS_SERE_SERV>() { newSereServ };
            if (IsNotNullOrEmpty(existSereServs))
            {
                alls.AddRange(existSereServs);
            }

            HIS_PATIENT_TYPE_ALTER usingPta = ptas
                        .Where(o => o.LOG_TIME <= data.InstructionTime)
                        .OrderByDescending(o => o.LOG_TIME)
                        .ThenByDescending(o => o.ID)
                        .FirstOrDefault();

            //D/s de tinh gia thi phai bo dv hien tai ra ko d/s (vi sau nguoi dung doi tu dv kham nay sang dv kham khac)
            //Neu de nguyen d/s thi khi do, dv kham moi (dv kham thay the) se bi tinh theo chinh sach gia cua cong kham
            //thu 2 --> sai
            List<HIS_SERE_SERV> existSereServToCalcPrices = existSereServs != null ? existSereServs.Where(o => o.ID != currentSereServId).ToList() : null;
            List<long> toReloadPriceSereServIds = existSereServToCalcPrices != null ? existSereServToCalcPrices.Select(o => o.ID).ToList() : null;

            // Neu nguoi dung ko gui len doi tuongphu thu thi tu dong set lai doi tuong phu thu va doi tuong thanh toan
            if (!data.PrimaryPatientTypeId.HasValue)
            {
                SereServPriceUtil.SetPrimaryPatientTypeId(alls, newSereServ, usingPta, treatment);
            }

            SereServPriceUtil.ReloadPrice(param, treatment, existSereServToCalcPrices, newSereServ, toReloadPriceSereServIds);

            return newSereServ;
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(HisServiceReqExamChangeSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                data.InstructionTime = now;
            }
        }
    }
}
