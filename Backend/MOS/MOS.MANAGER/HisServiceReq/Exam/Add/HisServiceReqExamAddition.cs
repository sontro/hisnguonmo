using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisServiceReq;
using Inventec.Token.ResourceSystem;
using MOS.MANAGER.HisServiceRoom;
using AutoMapper;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisDhst;
using MOS.UTILITY;
using System.Threading;
using MOS.MANAGER.OldSystemIntegrate;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisNumOrderIssue;
using MOS.MANAGER.HisDepartmentTran.Create;
using MOS.MANAGER.HisTransReq.CreateByService;

namespace MOS.MANAGER.HisServiceReq.Exam.Add
{
    class IntegrateAddExamThreadData
    {
        public HIS_TREATMENT Treatment { get; set; }
        public HIS_SERVICE_REQ ServiceReq { get; set; }
    }

    /// <summary>
    /// Xu ly nghiep vu khi bac sy chi dinh kham them o phong kham khac
    /// </summary>
    partial class HisServiceReqExamAddition : BusinessBase
    {
        private HisServiceReqCreate hisServiceReqCreate;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServCreate hisSereServCreate;
        private HisSereServExtCreate hisSereServExtCreate;
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisDepartmentTranCreate hisDepartmentTranCreate;
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisServiceReqExamAddition()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqExamAddition(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
            this.hisDepartmentTranCreate = new HisDepartmentTranCreate(param);
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Run(HisServiceReqExamAdditionSDO data, ref V_HIS_SERVICE_REQ resultData)
        {
            HIS_SERE_SERV currentSereServ = new HisSereServGet().GetById(data.CurrentSereServId);
            HIS_TREATMENT treatment = currentSereServ != null ? new HisTreatmentGet().GetById(currentSereServ.TDL_TREATMENT_ID.Value) : null;
            HIS_SERVICE_REQ currentServiceReq = currentSereServ != null && currentSereServ.SERVICE_REQ_ID.HasValue ? new HisServiceReqGet().GetById(currentSereServ.SERVICE_REQ_ID.Value) : null;
            HIS_SERE_SERV newSereServ = null;
            return this.Run(treatment, currentServiceReq, data, ref newSereServ, ref resultData);
        }

        internal bool Run(HIS_TREATMENT treatment, HIS_SERVICE_REQ currentServiceReq, HisServiceReqExamAdditionSDO data, ref HIS_SERE_SERV newSereServ, ref V_HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;
                this.SetServerTime(data);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqExamCheck examChecker = new HisServiceReqExamCheck(param);
                HisServiceReqExamAdditionCheck addChecker = new HisServiceReqExamAdditionCheck(param);

                bool valid = currentServiceReq != null;
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && addChecker.IsValidData(data, workPlace, currentServiceReq);
                valid = valid && treatmentChecker.IsUnLock(treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && ((!data.AdditionServiceId.HasValue && HisServiceRoomUtil.VerifyExecuteRoom(data.AdditionRoomId, param)) || HisServiceRoomUtil.IsProcessable(data.AdditionRoomId, data.AdditionServiceId.Value, param));
                valid = valid && (!currentServiceReq.TREATMENT_TYPE_ID.HasValue || examChecker.IsNotExceedLimit(data.AdditionRoomId, currentServiceReq.TREATMENT_ID, currentServiceReq.TREATMENT_TYPE_ID.Value, currentServiceReq.INTRUCTION_TIME));
                valid = valid && addChecker.IsFinishAllExamForAddExam(currentServiceReq, treatment.ID);
                if (valid)
                {
                    HIS_SERVICE_REQ newServiceReq = null;
                    this.ProcessServiceReq(treatment, data, currentServiceReq, ref newServiceReq);
                    this.ProcessCurrentServiceReq(data, currentServiceReq);
                    //Can xu ly sau khi update service_req, de trong truong hop cap nhat kham chinh thi sere_serv da duoc tu dong cap nhat truoc (theo trigger trong his_service_req)
                    this.ProcessSereServ(treatment, newServiceReq, data, ref newSereServ);
                    // Xu ly update treatment truoc tranh truong hop trong ham department_tran co update departmentIds cho treatment
                    //Se dan den truong departmentIds bi update ve nhu cu
                    this.ProcessTreatment(data, treatment, currentServiceReq);
                    this.ProcessDepartmentTran(data, newServiceReq);

                    List<HIS_SERVICE_REQ> serviceReqs = new List<HIS_SERVICE_REQ>();
                    if (newServiceReq != null)
                    {
                        serviceReqs.Add(newServiceReq);
                    }

                    if (!new HisTransReqCreateByService(param).Run(treatment, serviceReqs, workPlace))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Tao HisTransReq that bai");
                        Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                    }

                    this.PassResult(newServiceReq, ref resultData);
                    result = true;

                    //Tao thread moi de gui du lieu tich hop sang HIS cu (PMS)
                    this.IntegrateThreadInit(treatment, newServiceReq);
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

        private void ProcessTreatment(HisServiceReqExamAdditionSDO data, HIS_TREATMENT treatment, HIS_SERVICE_REQ currentServiceReq)
        {
            if (currentServiceReq != null && data.IsPrimary)
            {
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT before = Mapper.Map<HIS_TREATMENT>(treatment);

                treatment.ICD_CODE = null;
                treatment.ICD_NAME = null;
                string icdCode = currentServiceReq.ICD_CODE;
                string icdText = !string.IsNullOrWhiteSpace(currentServiceReq.ICD_TEXT) ? currentServiceReq.ICD_NAME + ";" + currentServiceReq.ICD_TEXT : currentServiceReq.ICD_NAME;
                HisTreatmentUpdate.AddIcd(treatment, icdCode, icdText);

                if (!this.hisTreatmentUpdate.Update(treatment, before))
                {
                    throw new Exception();
                }
            }
        }

        //Cap nhat lai thong tin "kham chinh"
        private void ProcessCurrentServiceReq(HisServiceReqExamAdditionSDO data, HIS_SERVICE_REQ currentServiceReq)
        {
            if (currentServiceReq != null && (data.IsPrimary || data.IsFinishCurrent))
            {
                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                HIS_SERVICE_REQ beforeUpdate = Mapper.Map<HIS_SERVICE_REQ>(currentServiceReq);

                currentServiceReq.IS_MAIN_EXAM = data.IsPrimary ? null : currentServiceReq.IS_MAIN_EXAM;

                if (data.IsFinishCurrent)
                {
                    HisServiceReqUpdateFinish.SetFinishInfo(currentServiceReq, null, null);
                }

                if (!this.hisServiceReqUpdate.Update(currentServiceReq, beforeUpdate, false))
                {
                    throw new Exception();
                }
            }
        }

        private void PassResult(HIS_SERVICE_REQ additionServiceReq, ref V_HIS_SERVICE_REQ resultData)
        {
            if (additionServiceReq != null)
            {
                resultData = new HisServiceReqGet().GetViewById(additionServiceReq.ID);
            }
        }

        //Thuc hien nghiep vu tu dong chuyen khoa cho BN
        private void ProcessDepartmentTran(HisServiceReqExamAdditionSDO data, HIS_SERVICE_REQ additionServiceReq)
        {
            //Neu dv kham them la dich vu chinh thi thuc hien nghiep vu chuyen khoa
            if (data.IsPrimary || data.IsChangeDepartment)
            {
                HIS_DEPARTMENT_TRAN currentDepartmentTran = new HisDepartmentTranGet().GetLastByTreatmentId(additionServiceReq.TREATMENT_ID);

                //neu khoa thuc hien cung voi khoa hien tai cua BN thi bo qua
                if (currentDepartmentTran.DEPARTMENT_ID == additionServiceReq.EXECUTE_DEPARTMENT_ID)
                {
                    return;
                }

                //Neu ko phai doi tuong la "kham" thi bo qua
                HIS_PATIENT_TYPE_ALTER patientTypeAlter = new HisPatientTypeAlterGet().GetLastByTreatmentId(additionServiceReq.TREATMENT_ID);
                if (patientTypeAlter.TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    return;
                }

                HisDepartmentTranSDO nextDepartmentTran = new HisDepartmentTranSDO();
                nextDepartmentTran.Time = additionServiceReq.INTRUCTION_TIME;
                nextDepartmentTran.TreatmentId = additionServiceReq.TREATMENT_ID;
                nextDepartmentTran.DepartmentId = additionServiceReq.EXECUTE_DEPARTMENT_ID;
                nextDepartmentTran.IsReceive = true;
                nextDepartmentTran.RequestRoomId = data.RequestRoomId;

                HIS_DEPARTMENT_TRAN resultData = null;
                if (!this.hisDepartmentTranCreate.Create(nextDepartmentTran, false, ref resultData))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        private void ProcessSereServ(HIS_TREATMENT treatment, HIS_SERVICE_REQ newServiceReq, HisServiceReqExamAdditionSDO data, ref HIS_SERE_SERV newSereServ)
        {
            if (data.AdditionServiceId.HasValue)
            {
                HIS_SERE_SERV toInsertSereServ = new HIS_SERE_SERV();
                toInsertSereServ.SERVICE_REQ_ID = newServiceReq.ID;
                toInsertSereServ.SERVICE_ID = data.AdditionServiceId.Value;
                toInsertSereServ.AMOUNT = 1;//fix so luong la 1 voi dich vu kham
                toInsertSereServ.TDL_IS_MAIN_EXAM = newServiceReq.IS_MAIN_EXAM;
                toInsertSereServ.PATIENT_TYPE_ID = data.PatientTypeId.Value;
                toInsertSereServ.PRIMARY_PATIENT_TYPE_ID = data.PrimaryPatientTypeId;
                toInsertSereServ.OTHER_PAY_SOURCE_ID = treatment.OTHER_PAY_SOURCE_ID;

                //Tu dong hao phi neu phong xu ly kham duoc cau hinh "mac dinh hao phi voi cong kham them"
                V_HIS_EXECUTE_ROOM executeRoom = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == newServiceReq.REQUEST_ROOM_ID).FirstOrDefault();

                toInsertSereServ.IS_EXPEND = executeRoom != null && executeRoom.IS_AUTO_EXPEND_ADD_EXAM == Constant.IS_TRUE ? (short?)Constant.IS_TRUE : null;

                long executeBranchId = HisDepartmentCFG.DATA
                    .Where(o => o.ID == newServiceReq.EXECUTE_DEPARTMENT_ID)
                    .FirstOrDefault().BRANCH_ID;

                //Cap nhat thong tin gia theo dich vu moi
                HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);
                if (!priceAdder.AddPrice(toInsertSereServ, newServiceReq.INTRUCTION_TIME, executeBranchId, newServiceReq.REQUEST_ROOM_ID, newServiceReq.REQUEST_DEPARTMENT_ID, newServiceReq.EXECUTE_ROOM_ID))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }

                if (!this.hisSereServCreate.Create(toInsertSereServ, newServiceReq, false))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }

                this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, false);

                //Cap nhat ti le BHYT cho sere_serv
                if (!this.hisSereServUpdateHein.UpdateDb())
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
                newSereServ = toInsertSereServ;
            }
        }

        /// <summary>
        /// Xu ly du lieu service_req
        /// </summary>
        /// <param name="currentSereServ"></param>
        /// <param name="data"></param>
        /// <param name="currentServiceReq"></param>
        /// <param name="additionServiceReq"></param>
        private void ProcessServiceReq(HIS_TREATMENT treatment, HisServiceReqExamAdditionSDO data, HIS_SERVICE_REQ currentServiceReq, ref HIS_SERVICE_REQ newServiceReq)
        {
            HIS_SERVICE_REQ toInsert = new HIS_SERVICE_REQ();
            toInsert.EXECUTE_DEPARTMENT_ID = HisRoomCFG.DATA.Where(o => o.ID == data.AdditionRoomId).FirstOrDefault().DEPARTMENT_ID;
            toInsert.EXECUTE_ROOM_ID = data.AdditionRoomId;
            //Luu y: ko gan chan doan cua phieu chi dinh dv cu vao phieu chi dinh dv moi 
            //==> de sang xu ly dich vu kham moi se load tu his_treatment len
            toInsert.INTRUCTION_TIME = data.InstructionTime;
            toInsert.PARENT_ID = currentServiceReq.ID;
            toInsert.PRIORITY = currentServiceReq.PRIORITY;
            toInsert.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
            toInsert.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH;
            toInsert.TREATMENT_ID = currentServiceReq.TREATMENT_ID;
            toInsert.REQUEST_ROOM_ID = data.RequestRoomId;
            toInsert.IS_MAIN_EXAM = data.IsPrimary ? (short?)Constant.IS_TRUE : null;
            toInsert.IS_NOT_REQUIRE_FEE = data.IsNotRequireFee ? (short?)Constant.IS_TRUE : null;
            toInsert.FULL_EXAM = currentServiceReq.FULL_EXAM;
            toInsert.HOSPITALIZATION_REASON = currentServiceReq.HOSPITALIZATION_REASON;
            toInsert.NEXT_TREATMENT_INSTRUCTION = currentServiceReq.NEXT_TREATMENT_INSTRUCTION;
            toInsert.PART_EXAM = currentServiceReq.PART_EXAM;
            toInsert.PART_EXAM_CIRCULATION = currentServiceReq.PART_EXAM_CIRCULATION;
            toInsert.PART_EXAM_DIGESTION = currentServiceReq.PART_EXAM_DIGESTION;
            toInsert.PART_EXAM_ENT = currentServiceReq.PART_EXAM_ENT;
            toInsert.PART_EXAM_EYE = currentServiceReq.PART_EXAM_EYE;
            toInsert.PART_EXAM_KIDNEY_UROLOGY = currentServiceReq.PART_EXAM_KIDNEY_UROLOGY;
            toInsert.PART_EXAM_MENTAL = currentServiceReq.PART_EXAM_MENTAL;
            toInsert.PART_EXAM_MOTION = currentServiceReq.PART_EXAM_MOTION;
            toInsert.PART_EXAM_MUSCLE_BONE = currentServiceReq.PART_EXAM_MUSCLE_BONE;
            toInsert.PART_EXAM_NEUROLOGICAL = currentServiceReq.PART_EXAM_NEUROLOGICAL;
            toInsert.PART_EXAM_NUTRITION = currentServiceReq.PART_EXAM_NUTRITION;
            toInsert.PART_EXAM_OBSTETRIC = currentServiceReq.PART_EXAM_OBSTETRIC;
            toInsert.PART_EXAM_OEND = currentServiceReq.PART_EXAM_OEND;
            toInsert.PART_EXAM_RESPIRATORY = currentServiceReq.PART_EXAM_RESPIRATORY;
            toInsert.PART_EXAM_STOMATOLOGY = currentServiceReq.PART_EXAM_STOMATOLOGY;
            toInsert.PATHOLOGICAL_HISTORY = currentServiceReq.PATHOLOGICAL_HISTORY;
            toInsert.PATHOLOGICAL_HISTORY_FAMILY = currentServiceReq.PATHOLOGICAL_HISTORY_FAMILY;
            toInsert.PATHOLOGICAL_PROCESS = currentServiceReq.PATHOLOGICAL_PROCESS;
            toInsert.NOTE = currentServiceReq.NOTE;
            toInsert.SICK_DAY = currentServiceReq.SICK_DAY;
            toInsert.PREVIOUS_SERVICE_REQ_ID = currentServiceReq.ID;
            toInsert.TREATMENT_TYPE_ID = currentServiceReq.TREATMENT_TYPE_ID;
            toInsert.IS_NOT_USE_BHYT = data.IsNotUseBhyt ? (short?)Constant.IS_TRUE : null;
            toInsert.TDL_SERVICE_IDS = data.AdditionServiceId.ToString();

            if (data.AdditionServiceId.HasValue)
            {
                V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == data.AdditionServiceId.Value).FirstOrDefault();
                if (service != null)
                {
                    toInsert.EXE_SERVICE_MODULE_ID = service.EXE_SERVICE_MODULE_ID.HasValue ? service.EXE_SERVICE_MODULE_ID : service.SETY_EXE_SERVICE_MODULE_ID;
                    toInsert.IS_NOT_REQUIRED_COMPLETE = service.IS_NOT_REQUIRED_COMPLETE == Constant.IS_TRUE ? (short?)Constant.IS_TRUE : null;
                }
            }
            else
            {
                HIS_SERVICE_TYPE examServiceType = HisServiceTypeCFG.DATA.Where(o => o.ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH).FirstOrDefault();
                toInsert.EXE_SERVICE_MODULE_ID = examServiceType.EXE_SERVICE_MODULE_ID;
            }

            //Cap STT kham
            new HisNumOrderIssueUtil(param).SetNumOrder(toInsert, null, null, null);

            if (!this.hisServiceReqCreate.Create(toInsert, treatment))
            {
                throw new Exception("Ket thuc nghiep vu");
            }

            newServiceReq = toInsert;
        }

        private void IntegrateThreadInit(HIS_TREATMENT treatment, HIS_SERVICE_REQ newServiceReq)
        {
            try
            {
                if (OldSystemCFG.INTEGRATION_TYPE != OldSystemCFG.IntegrationType.NONE)
                {
                    IntegrateAddExamThreadData threadData = new IntegrateAddExamThreadData();
                    threadData.Treatment = treatment;
                    threadData.ServiceReq = newServiceReq;
                    Thread thread = new Thread(new ParameterizedThreadStart(this.ProcessSendingIntegration));
                    thread.Priority = ThreadPriority.Normal;
                    thread.Start(threadData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh gui thong tin kham them sang HMS", ex);
            }
        }

        private void ProcessSendingIntegration(object threadData)
        {
            IntegrateAddExamThreadData td = (IntegrateAddExamThreadData)threadData;
            OldSystemIntegrateProcessor.AddExam(td.Treatment, td.ServiceReq);
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(HisServiceReqExamAdditionSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                if (data != null)
                {
                    data.InstructionTime = Inventec.Common.DateTime.Get.Now().Value;
                }
            }
        }

        //Rollback du lieu
        internal void RollbackData()
        {
            this.hisSereServCreate.RollbackData();

            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
            this.hisServiceReqCreate.RollbackData();
            this.hisServiceReqUpdate.RollbackData();
            this.hisTreatmentUpdate.RollbackData();
        }
    }
}
