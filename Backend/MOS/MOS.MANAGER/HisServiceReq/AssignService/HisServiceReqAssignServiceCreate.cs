using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MOS.UTILITY;
using MOS.MANAGER.HisServiceReq.Exam;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisSereServ.Update;
using MOS.OldSystem.HMS;
using MOS.MANAGER.OldSystemIntegrate;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.MANAGER.HisServiceReq.Test.GenerateBarcode;
using MOS.MANAGER.HisTreatment.Util;
using MOS.MANAGER.HisServiceReq.AssignService.TruncateReq;
using MOS.MANAGER.HisCard;
//using MOS.MANAGER.HisServiceReq.AssignService.PaylaterDeposit;
using MOS.MANAGER.HisServiceReq.AssignService.Deposit;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisNumOrderIssue;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.CodeGenerator.HisServiceReq;
using MOS.MANAGER.HisEkip;
using MOS.MANAGER.HisTransReq.CreateByService;
using MOS.MANAGER.HisBranch;
using System.IO;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;


namespace MOS.MANAGER.HisServiceReq.AssignService
{
    class ThreatDeleteTestData
    {
        public Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>> TestServiceReqs { get; set; }
    }

    class IntegrateServiceReqThreadData
    {
        public List<HIS_SERVICE_REQ> ServiceReqs { get; set; }
        public List<HIS_SERE_SERV> SereServs { get; set; }
    }

    class ThreadExportXmlData
    {
        public HIS_TREATMENT Treatment { get; set; }
        public HIS_BRANCH Branch { get; set; }
        public HIS_SERVICE_REQ ServiceReq { get; set; }
        public HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }
    }
    partial class HisServiceReqAssignServiceCreate : BusinessBase
    {
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisSereServCreate hisSereServCreate;
        private HisBedLogCreate hisBedLogCreate;
        private HisSereServExtCreate hisSereServExtCreate;
        private HisSereServUpdate hisSereServUpdate;
        private HisServiceReqTestCreate hisServiceReqTestCreate;
        private HisServiceReqCreate hisServiceReqCreate;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisDepartmentTranUpdate hisDepartmentTranUpdate;
        private ServiceReqTruncate serviceReqTruncate;
        private HisBedLogTruncate hisBedLogTruncate;
        private HisEkipCreate hisEkipCreate;

        //Xu ly cac nghiep vu ve goi
        private HisSereServPackage37 processPackage37;
        private HisSereServPackageBirth processPackageBirth;
        private HisSereServPackagePttm processPackagePttm;

        private List<HIS_SERE_SERV> beforeUpdateSereServs = new List<HIS_SERE_SERV>();
        private Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>> recentMapTestHisServiceReqs;

        //Anh xa giua service_req va d/s sere_serv tuong ung
        private Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>> SR_SS_MAPPING = new Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>>();
        //Anh xa giua sere_serv va sere_serv_ext tuong ung
        private Dictionary<HIS_SERE_SERV, HIS_SERE_SERV_EXT> SS_SS_EXT_MAPPING = new Dictionary<HIS_SERE_SERV, HIS_SERE_SERV_EXT>();

        //Anh xa giua sere_serv va d/s ekip tuong ung
        private Dictionary<HIS_SERE_SERV, HIS_EKIP> SS_EKIP_MAPPING = new Dictionary<HIS_SERE_SERV, HIS_EKIP>();

        internal HisServiceReqAssignServiceCreate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqAssignServiceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            //Nghiep vu xu ly o day luon thuc hien sau nghiep vu tao service_req
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisServiceReqTestCreate = new HisServiceReqTestCreate(param);
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
            this.hisDepartmentTranUpdate = new HisDepartmentTranUpdate(param);
            this.serviceReqTruncate = new ServiceReqTruncate(param);
            this.hisBedLogCreate = new HisBedLogCreate(param);
            this.hisBedLogTruncate = new HisBedLogTruncate(param);
            this.hisEkipCreate = new HisEkipCreate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Create(AssignServiceSDO data, bool setWorkPlaceInfo, ref HisServiceReqListResultSDO resultData)
        {
            return this.Create(data, setWorkPlaceInfo, false, ref resultData);
        }

        internal bool Create(AssignServiceSDO data, bool setWorkPlaceInfo, bool isSetIcdFromTreatment, ref HisServiceReqListResultSDO resultData)
        {
            bool result = false;
            try
            {
                this.SetServerTime(data);
                List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(data.TreatmentId);
                HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HIS_TREATMENT treatment = null;
                List<V_HIS_SERVICE> services = null;

                Dictionary<ServiceReqDetailSDO, HIS_TREATMENT_BED_ROOM> treatmentBedRoomDic = null;

                string sessionCode = Guid.NewGuid().ToString();
                bool valid = treatmentChecker.IsUnLock(data.TreatmentId, ref treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && serviceReqChecker.IsValidData(data.ServiceReqDetails, data.InstructionTimes, ref services);
                valid = valid && serviceReqChecker.IsValidBedInfo(treatment.ID, data.ServiceReqDetails, data.InstructionTimes, services, ref treatmentBedRoomDic);
                valid = valid && serviceReqChecker.IsAllowAssignServiceWithOxy(services);
                valid = valid && serviceReqChecker.IsValidBHYTServices(data.ServiceReqDetails);
                valid = valid && serviceReqChecker.IsValidDoNotUseBHYT(data.ServiceReqDetails);
                if (valid)
                {
                    if (isSetIcdFromTreatment && string.IsNullOrWhiteSpace(data.IcdCode))
                    {
                        data.IcdCode = treatment.ICD_CODE;
                        data.IcdName = treatment.ICD_NAME;
                    }

                    this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, ptas, false);

                    List<HIS_SERE_SERV> existsSereServs = new HisSereServGet().GetByTreatmentId(data.TreatmentId);
                    List<HIS_SERVICE_REQ> listReqTruncate = null;
                    long? updateDepartmentId = null;

                    if (this.Create(data, treatment, services, treatmentBedRoomDic, ptas, existsSereServs, sessionCode, setWorkPlaceInfo, ref updateDepartmentId, ref resultData, ref listReqTruncate))
                    {
                        if (IsNotNullOrEmpty(listReqTruncate))
                        {
                            HisServiceReqLog.RunUpdate(treatment.TREATMENT_CODE, listReqTruncate, resultData.ServiceReqs, resultData.SereServs, LibraryEventLog.EventLog.Enum.HisServiceReq_SuaChiDinhDichVu);
                        }
                        else
                        {
                            HisServiceReqLog.Run(resultData.ServiceReqs, resultData.SereServs, LibraryEventLog.EventLog.Enum.HisServiceReq_ChiDinhDichVu);
                        }

                        //tao tien trinh moi de update thong tin treatment
                        this.UpdateTreatmentThreadInit(treatment, data, updateDepartmentId);

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Rollback du lieu yeu cau dich vu:
        /// </summary>
        /// <param name="hisServiceReq"></param>
        /// <returns></returns>
        internal void RollbackData()
        {
            this.hisDepartmentTranUpdate.RollbackData();

            //Rollback du lieu sere_Serv
            //Luu y: can rollback tham so BHYT truoc khi thuc hien xoa sere_serv
            if (IsNotNullOrEmpty(this.beforeUpdateSereServs) && !this.hisSereServUpdate.UpdateRaw(this.beforeUpdateSereServs))
            {
                LogSystem.Error("Rollback sere_serv that bai");
            }
            this.hisEkipCreate.RollbackData();
            this.hisServiceReqCreate.RollbackData();
            this.serviceReqTruncate.Rollback();
            this.TestServiceReqRollbackThreadInit();
            this.hisServiceReqUpdate.RollbackData();
        }

        /// <summary>
        /// Xu ly de tao yeu cau dich vu dua vao danh sach ServiceReqDetailSDO
        /// </summary>
        /// <param name="serviceReqSdo">Thong tin chi dinh dich vu</param>
        /// <param name="serviceReqTypeId">Loai phieu yeu cau</param>
        /// <param name="returnHisServiceReqs">Danh sach ket qua phieu y/c tra ve</param>
        /// <returns></returns>
        private bool Create(AssignServiceSDO data, HIS_TREATMENT treatment, List<V_HIS_SERVICE> services, Dictionary<ServiceReqDetailSDO, HIS_TREATMENT_BED_ROOM> treatmentBedRoomDic, List<HIS_PATIENT_TYPE_ALTER> ptas, List<HIS_SERE_SERV> existedSereServs, string sessionCode, bool setWorkPlaceInfo, ref long? upDepartId, ref HisServiceReqListResultSDO resultData, ref List<HIS_SERVICE_REQ> reqTruncates)
        {
            bool result = false;
            try
            {
                HIS_PATIENT_TYPE_ALTER usingPta = null;
                List<HIS_SERVICE_REQ> lstReqDelete = null;
                List<HIS_SERE_SERV> lstSereServDelete = null;
                List<HIS_BED_LOG> lstBedLogDelete = null;
                HIS_SERVICE_REQ parent = null;

                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                ServiceReqTruncateCheck truncateChecker = new ServiceReqTruncateCheck(param);
                HisServiceReqAssignServiceCreateCheck assignChecker = new HisServiceReqAssignServiceCreateCheck(param);

                bool valid = checker.VerifyRequireField(data);

                //trong truong hop chi dinh nhieu ngay thi chi check voi ngay dau tien
                valid = valid && checker.IsValidPatientTypeAlter(data.InstructionTimes[0], ptas, ref usingPta);
                valid = valid && checker.VerifySessionCode(data.SessionCode, treatment.ID, existedSereServs, ref lstReqDelete, ref lstSereServDelete, ref lstBedLogDelete);
                valid = valid && (String.IsNullOrWhiteSpace(data.SessionCode) || truncateChecker.Verify(lstReqDelete, lstSereServDelete, treatment));
                valid = valid && assignChecker.AllowUseBhytByParent(data, ref parent);
                valid = valid && assignChecker.IsValidEkipInfoForService(data);
                if (valid)
                {
                    if (IsNotNullOrEmpty(lstReqDelete))
                    {
                        existedSereServs = existedSereServs != null ? existedSereServs.Where(o => !lstReqDelete.Any(a => a.ID == o.SERVICE_REQ_ID)).ToList() : null;
                    }

                    long reqRoomId = 0;
                    long reqDepartmentId = 0;
                    long reqBranchId = 0;

                    if (setWorkPlaceInfo && !data.ManualRequestRoomId)
                    {
                        WorkPlaceSDO workPlace = null;
                        if (!this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace))
                        {
                            return false;
                        }
                        reqRoomId = workPlace.RoomId;
                        reqDepartmentId = workPlace.DepartmentId;
                        reqBranchId = workPlace.BranchId;
                    }
                    else
                    {
                        V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == data.RequestRoomId).FirstOrDefault();
                        if (room == null)
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            LogSystem.Warn("request_room_id ko hop le.");
                            return false;
                        }
                        reqRoomId = room.ID;
                        reqDepartmentId = room.DEPARTMENT_ID;
                        reqBranchId = room.BRANCH_ID;
                    }

                    //Danh sach phan bo phong xu ly tuong ung voi dich vu duoc yeu cau
                    HisServiceReqRoomAssign roomAssigner = new HisServiceReqRoomAssign(param, services, data.ServiceReqDetails, reqRoomId, reqDepartmentId, reqBranchId, data.InstructionTimes, existedSereServs);
                    List<RoomAssignData> assignedRooms = roomAssigner.RoomAssign();

                    if (!IsNotNullOrEmpty(assignedRooms)
                        || !new HisServiceReqExamCheck(param).IsNotExceedLimit(assignedRooms, data.TreatmentId, usingPta.TREATMENT_TYPE_ID, data.InstructionTimes[0]))//vi ko cho chi dinh dich vu kham ==> voi chi dinh dich vu kham thi chi luon co 1 InstructionTime
                    {
                        throw new Exception("Khong the chi dinh phong cho dich vu. Ket thuc nghiep vu");
                    }

                    //Thuc hien xoa y lenh sau khi thuc hien verify phong xu ly, chinh sach gia ok
                    if (IsNotNullOrEmpty(lstReqDelete))
                    {
                        if (!this.serviceReqTruncate.Run(lstReqDelete, lstSereServDelete, existedSereServs, treatment, true))
                        {
                            throw new Exception("serviceReqTruncate. Ket thuc nghiep vu");
                        }
                    }

                    //Thuc hien xoa bed_log
                    if (IsNotNullOrEmpty(lstBedLogDelete))
                    {
                        if (!this.hisBedLogTruncate.TruncateList(lstBedLogDelete))
                        {
                            throw new Exception("serviceReqTruncate. Ket thuc nghiep vu");
                        }
                    }

                    //Tao du lieu service_reqs
                    List<HIS_SERE_SERV> newSereServs = null;
                    long maxExistedSereServId = IsNotNullOrEmpty(existedSereServs) ? existedSereServs.Max(o => o.ID) : 0;

                    //Kiem tra xem ho so da co chi dinh kham chua
                    bool isExistsExam = IsNotNullOrEmpty(existedSereServs) && existedSereServs.Exists(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH && o.IS_NO_EXECUTE != Constant.IS_TRUE);

                    //Danh dau ton tai y lenh cha la y lenh kham va chua co dich vu hay khong
                    bool setSereServToParent = false;

                    //Neu:
                    //+ Y lenh cha la "kham"
                    //+ Y lenh cha ko chua dich vu nao
                    //+ Y lenh cha co phong xu ly duoc cau hinh cho phep ko chon dich vu
                    //+ Trong d/s chi dinh co ton tai dv kham 
                    //Thi sere_serv moi duoc tao ra se gan vao y lenh cha
                    if (parent != null
                        && parent.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH
                        && (existedSereServs == null || !existedSereServs.Exists(t => t.SERVICE_REQ_ID == parent.ID))
                        && assignedRooms != null && assignedRooms.Exists(t => t.ServiceReqTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH))
                    {
                        V_HIS_EXECUTE_ROOM room = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == parent.EXECUTE_ROOM_ID).FirstOrDefault();
                        if (room != null && room.ALLOW_NOT_CHOOSE_SERVICE == Constant.IS_TRUE)
                        {
                            setSereServToParent = true;
                        }
                    }

                    //Tao du lieu service_req theo du lieu phan bo dich vu theo phong
                    List<HIS_SERVICE_REQ> serviceReqs = this.MakeServiceReq(data, usingPta, isExistsExam, maxExistedSereServId, treatment, assignedRooms, reqRoomId, reqDepartmentId, sessionCode, parent, existedSereServs, setSereServToParent);

                    //Lay ra cac service-req ko phai parent (da ton tai) de thuc hien xu y insert
                    List<HIS_SERVICE_REQ> toCreateServiceReqs = serviceReqs != null ? serviceReqs.Where(o => parent == null || parent.ID != o.ID).ToList() : null;

                    //Goi sang he thong lis sinh barcode trong truong hop cau hinh sinh barcode tu he thong LIS
                    this.GenerateBarcodeTest(toCreateServiceReqs, treatment);

                    //Viec verify treatment da duoc thuc hien o phia tren, nen ko thuc hien verify lai
                    setWorkPlaceInfo = setWorkPlaceInfo && !data.ManualRequestRoomId;
                    if (IsNotNullOrEmpty(toCreateServiceReqs))
                    {
                        List<HIS_SERVICE_REQ> exams = toCreateServiceReqs.Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH).ToList();
                        if (IsNotNullOrEmpty(exams))
                        {
                            foreach (HIS_SERVICE_REQ r in exams)
                            {
                                //Cap STT kham
                                new HisNumOrderIssueUtil(param).SetNumOrder(r, null, null, null);
                            }
                        }

                        if (!this.hisServiceReqCreate.CreateList(toCreateServiceReqs, treatment, setWorkPlaceInfo))
                        {
                            //tao loi can xoa barcode
                            this.FinishUpdateDB(toCreateServiceReqs);
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                        }
                    }

                    if (setSereServToParent)
                    {
                        HIS_SERVICE_REQ toUpdateServiceReq = serviceReqs != null ? serviceReqs.Where(o => parent.ID == o.ID).FirstOrDefault() : null;
                        if (IsNotNull(toUpdateServiceReq))
                        {
                            List<HIS_SERE_SERV> list = SR_SS_MAPPING[toUpdateServiceReq];
                            if (IsNotNullOrEmpty(list) && !this.hisServiceReqUpdate.Update(toUpdateServiceReq, parent, false))
                            {
                                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                            }
                        }
                    }

                    this.FinishUpdateDB(toCreateServiceReqs);
                    this.ProcessNewEkip();
                    //Xu ly thong tin sere_serv: insert du lieu moi, update du lieu cu (cac du lieu da co trong DB)
                    this.ProcessSereServ(reqRoomId, reqDepartmentId, maxExistedSereServId, treatment, existedSereServs, ref newSereServs, serviceReqs, usingPta);

                    List<HIS_SERE_SERV_EXT> exts = null;
                    this.ProcessSereServExt(ref exts);
                    this.CreateTestServiceReq(toCreateServiceReqs);
                    this.ProcessDepartmentTran(toCreateServiceReqs, ptas, ref upDepartId);
                    this.ProcessBedLog(treatmentBedRoomDic, newSereServs, data.ServiceReqDetails);

                    List<long> serviceReqIds = serviceReqs != null ? serviceReqs.Select(o => o.ID).ToList() : null;
                    List<long> sereServIds = newSereServs != null ? newSereServs.Select(o => o.ID).ToList() : null;

                    #region TranReq
                    if (newSereServs.Exists(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && o.VIR_TOTAL_PATIENT_PRICE > 0))
                    {
                        WorkPlaceSDO workPlace = new WorkPlaceSDO() { RoomId = reqRoomId, DepartmentId = reqDepartmentId, BranchId = reqBranchId };
                        //tao HisTransReq truoc de sau khi getview se co du lieu
                        if (!new HisTransReqCreateByService(param).Run(treatment, serviceReqs, workPlace))
                        {
                            Inventec.Common.Logging.LogSystem.Error("Tao HisTransReq that bai");
                            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                        }
                    }
                    #endregion

                    List<V_HIS_SERE_SERV> vSereServs = new HisSereServGet().GetViewByIds(sereServIds);
                    List<V_HIS_SERVICE_REQ> vServiceReqs = new HisServiceReqGet().GetViewByIds(serviceReqIds);
                    HIS_TRANSACTION transaction = null;
                    List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = null;
                    List<V_HIS_SERE_SERV> depositedSereServs = null;

                    //Xu ly thanh toan va tao giao dich tam thu
                    new DepositProcessor(param).Run(reqRoomId, treatment, parent, vSereServs, vServiceReqs, existedSereServs, ref transaction, ref sereServDeposits, ref depositedSereServs);

                    this.PassResult(vServiceReqs, vSereServs, exts, sessionCode, transaction, sereServDeposits, depositedSereServs, ref resultData);
                    result = true;
                    reqTruncates = lstReqDelete;

                    //Tao thread moi de gui du lieu tich hop sang HIS cu (PMS)
                    this.IntegrateThreadInit(toCreateServiceReqs, newSereServs, treatment, ptas);
                    //Kiem tra co vuot qua 6 thang luong co ban khong
                    this.ProcessCheckBaseSalary(treatment, ptas, existedSereServs, newSereServs);
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                result = false;
            }
            return result;
        }

        private void ProcessNewEkip()
        {
            if (IsNotNullOrEmpty(this.SS_EKIP_MAPPING))
            {
                if (!this.hisEkipCreate.CreateList(this.SS_EKIP_MAPPING.Values.ToList()))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        private void ProcessBedLog(Dictionary<ServiceReqDetailSDO, HIS_TREATMENT_BED_ROOM> treatmentBedRoomDic, List<HIS_SERE_SERV> newSereServs, List<ServiceReqDetailSDO> serviceReqDetails)
        {
            List<ServiceReqDetailSDO> bedReqs = IsNotNullOrEmpty(serviceReqDetails) ? serviceReqDetails.Where(o => o.BedId.HasValue).ToList() : null;

            if (IsNotNullOrEmpty(treatmentBedRoomDic) && IsNotNullOrEmpty(newSereServs) && IsNotNullOrEmpty(bedReqs))
            {
                List<HIS_BED_LOG> bedLogs = new List<HIS_BED_LOG>();
                foreach (ServiceReqDetailSDO sdo in bedReqs)
                {
                    HIS_TREATMENT_BED_ROOM treatmentBedRoom = treatmentBedRoomDic[sdo];
                    V_HIS_BED_ROOM bedRoom = treatmentBedRoom != null ? HisBedRoomCFG.DATA.Where(o => o.ID == treatmentBedRoom.BED_ROOM_ID).FirstOrDefault() : null;
                    HIS_SERE_SERV ss = bedRoom != null ? newSereServs.Where(o => o.SERVICE_ID == sdo.ServiceId && o.TDL_EXECUTE_ROOM_ID == bedRoom.ROOM_ID).FirstOrDefault() : null;

                    if (ss != null && treatmentBedRoom != null)
                    {
                        HIS_BED_LOG bedLog = new HIS_BED_LOG();
                        bedLog.BED_ID = sdo.BedId.Value;
                        bedLog.BED_SERVICE_TYPE_ID = sdo.ServiceId;
                        bedLog.PATIENT_TYPE_ID = sdo.PatientTypeId;
                        bedLog.PRIMARY_PATIENT_TYPE_ID = sdo.PrimaryPatientTypeId;
                        bedLog.SHARE_COUNT = sdo.ShareCount;
                        bedLog.START_TIME = sdo.BedStartTime.Value;
                        bedLog.FINISH_TIME = sdo.BedFinishTime.Value;
                        bedLog.TREATMENT_BED_ROOM_ID = treatmentBedRoom.ID;
                        bedLog.SERVICE_REQ_ID = ss.SERVICE_REQ_ID;
                        bedLog.IS_SERVICE_REQ_ASSIGNED = Constant.IS_TRUE;
                        bedLogs.Add(bedLog);
                    }
                }

                if (IsNotNullOrEmpty(bedLogs) && !this.hisBedLogCreate.CreateWithoutChecking(bedLogs))
                {
                    throw new Exception("Them thong tin bedlog that bai.");
                }
            }
        }

        private void ProcessSereServ(long requestRoomId, long requestDepartmentId, long maxExistedSereServId, HIS_TREATMENT treatment, List<HIS_SERE_SERV> existedSereServs, ref List<HIS_SERE_SERV> newSereServs, List<HIS_SERVICE_REQ> serviceReqs, HIS_PATIENT_TYPE_ALTER pta)
        {
            List<HIS_SERE_SERV> beforeUpdates = null;
            if (existedSereServs != null)
            {
                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                beforeUpdates = Mapper.Map<List<HIS_SERE_SERV>>(existedSereServs);
            }

            if (IsNotNullOrEmpty(serviceReqs))
            {
                newSereServs = new List<HIS_SERE_SERV>();
                foreach (HIS_SERVICE_REQ sr in serviceReqs)
                {
                    HIS_DEPARTMENT executeDepartment = HisDepartmentCFG.DATA
                        .Where(o => o.ID == sr.EXECUTE_DEPARTMENT_ID).FirstOrDefault();

                    List<HIS_SERE_SERV> list = SR_SS_MAPPING[sr];
                    foreach (HIS_SERE_SERV ss in list)
                    {
                        HIS_EKIP ekip = IsNotNullOrEmpty(SS_EKIP_MAPPING) && SS_EKIP_MAPPING.ContainsKey(ss) ? SS_EKIP_MAPPING[ss] : null;
                        ss.SERVICE_REQ_ID = sr.ID;
                        if (ekip != null)
                            ss.EKIP_ID = ekip.ID;
                        //can set thua du lieu o day luon do phan xu ly ti le thanh toan 
                        //co su dung cac truong thua du lieu
                        HisSereServUtil.SetTdl(ss, sr);
                    }
                    newSereServs.AddRange(list);
                }
                List<long> allSereServIds = new List<long>();
                if (newSereServs != null)
                {
                    allSereServIds.AddRange(newSereServs.Select(o => o.ID).ToList());
                }
                if (existedSereServs != null)
                {
                    allSereServIds.AddRange(existedSereServs.Select(o => o.ID).ToList());
                }
                SereServPriceUtil.ReloadPrice(param, treatment, existedSereServs, newSereServs, allSereServIds);
            }

            //Cap nhat du lieu sere_serv
            List<HIS_SERE_SERV> changeSereServs = null;
            List<HIS_SERE_SERV> oldOfChangeSereServs = null;
            this.SetAdditionalInfo(requestRoomId, requestDepartmentId, treatment, beforeUpdates, existedSereServs, serviceReqs, newSereServs, ref changeSereServs, ref oldOfChangeSereServs);

            List<HIS_SERE_SERV> toUpdates = IsNotNullOrEmpty(changeSereServs) ?
                changeSereServs.Where(o => o.ID <= maxExistedSereServId).ToList() : null;

            if (!this.hisSereServCreate.CreateList(newSereServs, serviceReqs, false))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }

            if (IsNotNullOrEmpty(toUpdates))
            {
                List<HIS_SERE_SERV> olds = oldOfChangeSereServs.Where(o => o.ID <= maxExistedSereServId).ToList();
                this.beforeUpdateSereServs.AddRange(olds);//luu lai phuc vu rollback

                //tao thread moi de update sere_serv cu~
                Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessUpdateSereServ));
                thread.Priority = ThreadPriority.AboveNormal;
                UpdateSereServThreadData threadData = new UpdateSereServThreadData();
                threadData.SereServs = toUpdates;
                thread.Start(threadData);
            }
        }

        //Luu y: Chi chay sau khi da xu ly insert sere-serv (do co nghiep vu su dung sere_serv_id)
        private void ProcessSereServExt(ref List<HIS_SERE_SERV_EXT> resultData)
        {
            if (SS_SS_EXT_MAPPING != null && SS_SS_EXT_MAPPING.Count > 0)
            {
                List<HIS_SERE_SERV_EXT> toCreates = new List<HIS_SERE_SERV_EXT>();
                foreach (HIS_SERE_SERV ss in SS_SS_EXT_MAPPING.Keys)
                {
                    HIS_SERE_SERV_EXT ext = SS_SS_EXT_MAPPING[ss];
                    ext.SERE_SERV_ID = ss.ID;
                    HisSereServExtUtil.SetTdl(ext, ss);
                    toCreates.Add(ext);
                }
                if (!this.hisSereServExtCreate.CreateList(toCreates))
                {
                    throw new Exception("Rollback du lieu");
                }
                resultData = toCreates;
            }
        }

        //Cap nhat de bo sung thong tin vao sere_serv
        private void SetAdditionalInfo(long requestRoomId, long requestDepartmentId, HIS_TREATMENT treatment, List<HIS_SERE_SERV> beforeUpdates, List<HIS_SERE_SERV> existedSereServs, List<HIS_SERVICE_REQ> newServiceReqs, List<HIS_SERE_SERV> newSereServs, ref List<HIS_SERE_SERV> changeRecords, ref List<HIS_SERE_SERV> oldOfChangeRecords)
        {
            List<HIS_SERE_SERV> toUpdateData = new List<HIS_SERE_SERV>();
            if (IsNotNullOrEmpty(newSereServs))
            {
                this.processPackage37 = new HisSereServPackage37(param, treatment.ID, requestRoomId, requestDepartmentId, existedSereServs);
                this.processPackageBirth = new HisSereServPackageBirth(param, requestDepartmentId, existedSereServs);
                this.processPackagePttm = new HisSereServPackagePttm(param, requestDepartmentId, existedSereServs);

                var groups = newSereServs.GroupBy(o => o.TDL_INTRUCTION_TIME).ToList();

                foreach (var g in groups)
                {
                    //Xu ly de ap dung goi 3 ngay 7 ngay
                    this.processPackage37.Apply3Day7Day(g.ToList(), g.Key);
                    //Xu ly de ap dung goi de
                    this.processPackageBirth.Run(g.ToList(), g.ToList()[0].PARENT_ID);
                    //Xu ly de ap dung goi phau thuat tham my
                    this.processPackagePttm.Run(g.ToList(), g.ToList()[0].PARENT_ID, g.Key);
                }

                toUpdateData.AddRange(newSereServs);
            }

            if (IsNotNullOrEmpty(existedSereServs))
            {
                toUpdateData.AddRange(existedSereServs);
            }

            //Xu ly de set thong tin ti le chi tra, doi tuong,...
            if (!this.hisSereServUpdateHein.Update(beforeUpdates, toUpdateData, newSereServs, ref changeRecords, ref oldOfChangeRecords))
            {
                throw new Exception("Rollback du lieu");
            }
        }

        private void PassResult(List<V_HIS_SERVICE_REQ> serviceReqs, List<V_HIS_SERE_SERV> sereServs, List<HIS_SERE_SERV_EXT> exts, string sessionCode, HIS_TRANSACTION transaction, List<HIS_SERE_SERV_DEPOSIT> sereServDeposits, List<V_HIS_SERE_SERV> depositedSereServs, ref HisServiceReqListResultSDO resultData)
        {
            resultData = new HisServiceReqListResultSDO();
            resultData.SessionCode = sessionCode;
            resultData.ServiceReqs = serviceReqs;
            resultData.SereServs = sereServs;
            resultData.SereServExts = exts;
            resultData.SereServDeposits = sereServDeposits;
            resultData.DepositedSereServs = depositedSereServs;

            if (transaction != null)
            {
                V_HIS_TRANSACTION tran = new HisTransactionGet().GetViewById(transaction.ID);
                resultData.Transactions = new List<V_HIS_TRANSACTION>() { tran };
            }
        }

        /// <summary>
        /// Tao du lieu service_req phan bo theo phong
        /// </summary>
        /// <param name="data"></param>
        /// <param name="usingPta"></param>
        /// <param name="isExistsExam"></param>
        /// <param name="maxExistedSereServId"></param>
        /// <param name="treatment"></param>
        /// <param name="assignedRooms"></param>
        /// <param name="reqRoomId"></param>
        /// <param name="reqDepartmentId"></param>
        /// <param name="sessionCode"></param>
        /// <returns></returns>
        private List<HIS_SERVICE_REQ> MakeServiceReq(AssignServiceSDO data, HIS_PATIENT_TYPE_ALTER usingPta, bool isExistsExam, long maxExistedSereServId, HIS_TREATMENT treatment, List<RoomAssignData> assignedRooms, long reqRoomId, long reqDepartmentId, string sessionCode, HIS_SERVICE_REQ parent, List<HIS_SERE_SERV> existedSereServs, bool setSereServToParent)
        {
            List<HIS_SERVICE_REQ> result = new List<HIS_SERVICE_REQ>();

            bool isOnlyType = (assignedRooms.GroupBy(g => g.ServiceReqTypeId).Count() == 1);

            foreach (long instructionTime in data.InstructionTimes)
            {
                foreach (RoomAssignData roomAssignData in assignedRooms)
                {
                    if (!IsNotNullOrEmpty(roomAssignData.ServiceReqDetails))
                    {
                        continue;
                    }

                    HIS_SERVICE_REQ serviceReq = null;
                    //Trong truong hop co nghiep vu gan sere_serv vao service_req hien tai thi ko tao service_req moi
                    if (!setSereServToParent || roomAssignData.ServiceReqTypeId != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                    {
                        serviceReq = new HIS_SERVICE_REQ();
                        serviceReq.EXECUTE_DEPARTMENT_ID = roomAssignData.DepartmentId;
                        serviceReq.EXECUTE_ROOM_ID = roomAssignData.RoomId;
                        serviceReq.SERVICE_REQ_TYPE_ID = roomAssignData.ServiceReqTypeId;
                        serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                        serviceReq.PRIORITY = data.Priority;
                        serviceReq.PRIORITY_TYPE_ID = data.PriorityTypeId;
                        serviceReq.PARENT_ID = data.ParentServiceReqId;
                        serviceReq.INTRUCTION_TIME = instructionTime;
                        serviceReq.TREATMENT_ID = data.TreatmentId;
                        serviceReq.ICD_TEXT = HisIcdUtil.RemoveDuplicateIcd(data.IcdText);
                        serviceReq.ICD_NAME = data.IcdName;
                        serviceReq.ICD_CODE = CommonUtil.ToUpper(data.IcdCode);
                        serviceReq.ICD_CAUSE_NAME = data.IcdCauseName;
                        serviceReq.ICD_CAUSE_CODE = CommonUtil.ToUpper(data.IcdCauseCode);
                        serviceReq.ICD_SUB_CODE = HisIcdUtil.RemoveDuplicateIcd(CommonUtil.ToUpper(data.IcdSubCode));
                        serviceReq.IS_NOT_REQUIRE_FEE = data.IsNotRequireFee;
                        serviceReq.EXECUTE_GROUP_ID = data.ExecuteGroupId;
                        serviceReq.DESCRIPTION = data.Description;
                        serviceReq.REQUEST_ROOM_ID = reqRoomId;
                        serviceReq.REQUEST_DEPARTMENT_ID = reqDepartmentId;
                        serviceReq.REQUEST_LOGINNAME = data.RequestLoginName;
                        serviceReq.REQUEST_USERNAME = data.RequestUserName;
                        serviceReq.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.RequestLoginName);
                        if (IsNotNullOrEmpty(data.TrackingInfos))
                        {
                            var trackingInfo = data.TrackingInfos.FirstOrDefault(o => o.IntructionTime == serviceReq.INTRUCTION_TIME);
                            if (IsNotNull(trackingInfo))
                            {
                                serviceReq.TRACKING_ID = trackingInfo.TrackingId;
                            }
                        }
                        else
                        {
                            serviceReq.TRACKING_ID = data.TrackingId;
                        }
                        serviceReq.TREATMENT_TYPE_ID = usingPta.TREATMENT_TYPE_ID;
                        serviceReq.SESSION_CODE = sessionCode;
                        serviceReq.IS_EMERGENCY = data.IsEmergency ? (short?)Constant.IS_TRUE : null;
                        serviceReq.IS_INFORM_RESULT_BY_SMS = data.IsInformResultBySms ? (short?)Constant.IS_TRUE : null;
                        serviceReq.EXE_SERVICE_MODULE_ID = roomAssignData.ExeServiceModuleId;
                        serviceReq.KIDNEY_SHIFT = data.KidneyShift;
                        serviceReq.MACHINE_ID = data.MachineId;
                        serviceReq.EXP_MEST_TEMPLATE_ID = data.ExpMestTemplateId;
                        serviceReq.IS_KIDNEY = data.IsKidney ? (short?)Constant.IS_TRUE : null;
                        serviceReq.IS_ANTIBIOTIC_RESISTANCE = roomAssignData.IsAntibioticResistance ? (short?)Constant.IS_TRUE : null;
                        serviceReq.PROVISIONAL_DIAGNOSIS = data.ProvisionalDiagnosis;
                        serviceReq.CONSULTANT_LOGINNAME = data.ConsultantLoginName;
                        serviceReq.CONSULTANT_USERNAME = data.ConsultantUserName;
                        serviceReq.BARCODE_LENGTH = HisServiceReqCFG.LIS_SID_LENGTH;//su dung truong barcode cho SID ben LIS (labconn, roche, ...)
                        serviceReq.ALLOW_SEND_PACS = roomAssignData.ServiceReqDetails.Exists(o => o.AllowSendPacs) ? (short?)Constant.IS_TRUE : null;

                        if (!Lis2CFG.IS_CALL_GENERATE_BARCODE)//khong goi sang lis de sinh barcode thi gui sang barcode
                        {
                            serviceReq.IS_SEND_BARCODE_TO_LIS = Constant.IS_TRUE;
                        }

                        //Neu chi dinh cha "ko huong BHYT" thi cac chi dinh phat sinh cung "ko huong BHYT"
                        if (IsNotNull(parent))
                        {
                            serviceReq.IS_NOT_USE_BHYT = parent.IS_NOT_USE_BHYT;
                        }

                        if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                        {
                            serviceReq.RESERVED_NUM_ORDER = HisServiceReqCFG.RESERVED_NUM_ORDER;
                        }

                        //Neu la chi dinh phau thuat thi mac dinh la chua duoc duyet
                        if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT)
                        {
                            serviceReq.PTTT_APPROVAL_STT_ID = IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__NEW;
                        }

                        //Neu ho so chua co chi dinh kham nao thi set chi dinh kham nay la "kham chinh"
                        if (!isExistsExam && roomAssignData.ServiceReqTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                        {
                            serviceReq.IS_MAIN_EXAM = Constant.IS_TRUE;
                            isExistsExam = true;
                        }
                    }
                    else
                    {
                        serviceReq = Mapper.Map<HIS_SERVICE_REQ>(parent);
                        serviceReq.REQUEST_ROOM_ID = reqRoomId;
                        serviceReq.REQUEST_DEPARTMENT_ID = reqDepartmentId;
                        serviceReq.REQUEST_LOGINNAME = data.RequestLoginName;
                        serviceReq.REQUEST_USERNAME = data.RequestUserName;
                        serviceReq.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.RequestLoginName);
                        serviceReq.REQ_WORKING_SHIFT_ID = TokenManager.GetWorkingShift();
                        if (!isExistsExam && parent.IS_MAIN_EXAM == Constant.IS_TRUE)
                        {
                            isExistsExam = true;
                        }
                        setSereServToParent = false;
                    }
                    if (!String.IsNullOrWhiteSpace(roomAssignData.SampleTypeCode))
                    {
                        HIS_TEST_SAMPLE_TYPE testSampleType = new HisTestSampleType.HisTestSampleTypeGet().GetByCode(roomAssignData.SampleTypeCode);
                        if (testSampleType != null)
                            serviceReq.TEST_SAMPLE_TYPE_ID = testSampleType.ID;
                    }

                    bool isNotRequiredComplete = false;
                    List<HIS_SERE_SERV> ss = new List<HIS_SERE_SERV>();

                    List<string> attachAssignPrintTypeCodes = new List<string>();
                    //Tao danh sach sere_serv tuong ung voi service_req
                    foreach (ServiceReqDetail req in roomAssignData.ServiceReqDetails)
                    {
                        HIS_SERE_SERV sereServ = new HIS_SERE_SERV();
                        sereServ.SERVICE_ID = req.ServiceId;
                        sereServ.AMOUNT = req.Amount;
                        sereServ.PARENT_ID = req.ParentId;
                        sereServ.PATIENT_TYPE_ID = req.PatientTypeId;
                        sereServ.IS_EXPEND = req.IsExpend;
                        sereServ.IS_OUT_PARENT_FEE = req.IsOutParentFee;
                        sereServ.IS_NO_HEIN_DIFFERENCE = req.IsNoHeinDifference ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        sereServ.EKIP_ID = req.EkipId;
                        sereServ.SHARE_COUNT = req.ShareCount;
                        sereServ.TDL_IS_MAIN_EXAM = serviceReq.IS_MAIN_EXAM;//luu du thua du lieu
                        sereServ.ID = ++maxExistedSereServId; //tao fake id (de dinh danh sere_serv khi chua insert vao DB)
                        sereServ.OTHER_PAY_SOURCE_ID = req.OtherPaySourceId;
                        sereServ.PACKAGE_ID = req.PackageId;
                        sereServ.SERVICE_CONDITION_ID = req.ServiceConditionId;
                        ServiceReqDetailSDO detailSDO = data.ServiceReqDetails != null ? data.ServiceReqDetails.FirstOrDefault(o => o.ServiceId == sereServ.SERVICE_ID) : null;
                        sereServ.IS_NOT_USE_BHYT = detailSDO != null && detailSDO.IsNotUseBhyt ? (short?)Constant.IS_TRUE : null;

                        //Chi gan doi tuong phu thu khi bat cau hinh
                        if (HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.AUTO
                            || HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.YES)
                        {
                            sereServ.PRIMARY_PATIENT_TYPE_ID = req.PrimaryPatientTypeId;
                        }
                        //xử lý khi tạo y lệnh nếu dịch vụ truyền lên có IS_NOT_REQUIRED_COMPLETE(HIS_SERVICE) = 1 thì Lưu thông tin IS_NOT_REQUIRED_COMPLETE = 1(HIS_SERVICE_REQ)
                        if (!isNotRequiredComplete && IsNotNullOrEmpty(HisServiceCFG.IS_NOT_REQUIRED_COMPLETE_DATA_VIEW))
                        {
                            V_HIS_SERVICE service = HisServiceCFG.IS_NOT_REQUIRED_COMPLETE_DATA_VIEW.FirstOrDefault(o => o.ID == req.ServiceId);

                            if (IsNotNull(service) && service.IS_NOT_REQUIRED_COMPLETE == Constant.IS_TRUE)
                            {
                                isNotRequiredComplete = true;
                            }
                        }
                        //Xu ly de tu dong gan chinh sach gia cho dich vu
                        //Luu y: can xu ly truoc doan set gia o duoi (do o duoi co check truong package_id)
                        HisSereServPackageUtil.AutoAssign(sereServ);

                        //Neu cau hinh cho phep nguoi dung nhap gia luc chi dinh DVKT, 
                        //va doi tuong thanh toan, doi tuong phu thu ko phai la BHYT
                        if ((req.IsEnableAssignPrice || sereServ.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__KSK)
                            && sereServ.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                            && sereServ.PRIMARY_PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                        {
                            if (sereServ.PACKAGE_ID.HasValue && req.UserPackagePrice.HasValue)
                            {
                                sereServ.PACKAGE_PRICE = req.UserPackagePrice;
                                sereServ.IS_USER_PACKAGE_PRICE = Constant.IS_TRUE;
                            }
                            else if (req.UserPrice.HasValue)
                            {
                                sereServ.USER_PRICE = req.UserPrice;
                            }
                        }

                        ss.Add(sereServ);

                        //du lieu duoc luu trong sere_serv_ext
                        if (!string.IsNullOrWhiteSpace(req.InstructionNote))
                        {
                            HIS_SERE_SERV_EXT ext = new HIS_SERE_SERV_EXT();
                            ext.INSTRUCTION_NOTE = req.InstructionNote;
                            SS_SS_EXT_MAPPING.Add(sereServ, ext);

                            //neu chua co thong tin TDL_INSTRUCTION_NOTE trong service_req thi cap nhat vao
                            if (String.IsNullOrWhiteSpace(serviceReq.TDL_INSTRUCTION_NOTE))
                            {
                                serviceReq.TDL_INSTRUCTION_NOTE = req.InstructionNote;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(req.AttachAssignPrintTypeCode))
                        {
                            attachAssignPrintTypeCodes.Add(req.AttachAssignPrintTypeCode);
                        }

                        // Tao ekip theo thon tin ekip theo tung dich vu
                        if (IsNotNullOrEmpty(req.EkipInfos))
                        {
                            HIS_EKIP ekip = new HIS_EKIP();
                            List<HIS_EKIP_USER> ekUsers = new List<HIS_EKIP_USER>();
                            foreach (var ekipInfo in req.EkipInfos)
                            {
                                HIS_EKIP_USER ekUser = new HIS_EKIP_USER();
                                ekUser.EXECUTE_ROLE_ID = ekipInfo.ExecuteRoleId;
                                ekUser.LOGINNAME = ekipInfo.LoginName;
                                ekUser.USERNAME = ekipInfo.UserName;
                                ekUsers.Add(ekUser);
                            }

                            ekip.HIS_EKIP_USER = ekUsers;
                            SS_EKIP_MAPPING.Add(sereServ, ekip);
                        }
                    }

                    //Neu service_req tao moi (ko phai lay service_req hien tai thi moi xu ly nghiep vu gan thong tin huong dan benh nhan)
                    if (parent == null || serviceReq.ID != parent.ID)
                    {
                        HisServiceReqUtil.SetReturnAddress(serviceReq);
                        HisServiceReqUtil.SetCashierRoom(serviceReq, isOnlyType);
                        HisServiceReqUtil.SetReturnInDiffDay(serviceReq, ss);
                    }

                    serviceReq.TDL_SERVICE_IDS = String.Join(";", ss.Select(s => s.SERVICE_ID).ToList());
                    serviceReq.ATTACH_ASSIGN_PRINT_TYPE_CODE = IsNotNullOrEmpty(attachAssignPrintTypeCodes) ? string.Join(",", attachAssignPrintTypeCodes.Distinct().ToList()) : null;
                    serviceReq.IS_NOT_REQUIRED_COMPLETE = isNotRequiredComplete ? (short?)Constant.IS_TRUE : null;
                    //serviceReq.IS_NOT_REQUIRED_COMPLETE = isNotRequiredComplete ? (short?)Constant.IS_FALSE : null;
                    SR_SS_MAPPING.Add(serviceReq, ss);
                    result.Add(serviceReq);
                }

            }

            return result;
        }

        private void CreateTestServiceReq(List<HIS_SERVICE_REQ> serviceReqs)
        {
            //Kiem tra xem co y/c xet nghiem ko, neu co thi bo sung insert
            List<HIS_SERVICE_REQ> testHisServiceReqs = serviceReqs != null ? serviceReqs
                .Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
                .ToList() : null;

            if (IsNotNullOrEmpty(testHisServiceReqs))
            {
                this.recentMapTestHisServiceReqs = new Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>>();

                #region Tao cac doi tuong du lieu chi tiet cho tung service_req, rieng test_service_req thi thuc hien insert vao CSDL luon
                foreach (HIS_SERVICE_REQ hisServiceReq in testHisServiceReqs)
                {
                    List<HIS_SERE_SERV> sereSers = SR_SS_MAPPING[hisServiceReq];
                    if (!this.hisServiceReqTestCreate.Create(hisServiceReq, sereSers))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    this.recentMapTestHisServiceReqs.Add(hisServiceReq, sereSers);
                }
                #endregion
            }
        }

        /// <summary>
        /// Xu ly de phuc vu nghiep vu nguoi dung tao thong tin BN xong sau do moi thuc hien chi dinh kham
        /// Xu ly cap nhat lai thong tin vao khoa neu dap ung cac dieu kien sau:
        /// - Khoa cua y lenh moi khac phieu chi dinh cu
        /// - Dich vu kham la dich vu dau tien cua BN
        /// - Doi tuong BN la kham
        /// </summary>
        /// <param name="currentServiceReq"></param>
        /// <param name="newServiceReq"></param>
        private void ProcessDepartmentTran(List<HIS_SERVICE_REQ> reqs, List<HIS_PATIENT_TYPE_ALTER> ptas, ref long? upDepartId)
        {
            HIS_SERVICE_REQ exam = IsNotNullOrEmpty(reqs) ? reqs
                .Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                .FirstOrDefault() : null;

            if (exam != null)
            {
                //Kiem tra xem BN da chuyen khoa lan nao chua, neu da tung chuyen khoa thi bo qua
                List<HIS_DEPARTMENT_TRAN> departmentTrans = new HisDepartmentTranGet().GetByTreatmentId(exam.TREATMENT_ID);
                if (!IsNotNullOrEmpty(departmentTrans) || departmentTrans.Count != 1)
                {
                    return;
                }

                //Kiem tra, neu khoa hien tai giong voi khoa xu ly cua y lenh kham thi bo qua
                if (exam.EXECUTE_DEPARTMENT_ID == departmentTrans[0].DEPARTMENT_ID)
                {
                    return;
                }

                //Kiem tra xem neu BN ko phai la kham thi bo qua
                if (!IsNotNullOrEmpty(ptas) || ptas.Count != 1 || ptas[0].TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    return;
                }

                //Kiem tra xem ngoai cac y lenh vua tao thi ho so co y lenh nao khac ko. Neu co thi bo qua
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.HAS_EXECUTE = true;
                filter.TREATMENT_ID = exam.TREATMENT_ID;
                filter.NOT_IN_IDs = reqs.Select(o => o.ID).ToList();
                List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);
                if (IsNotNullOrEmpty(serviceReqs))
                {
                    return;
                }

                //Neu dap ung tat ca cac y/c tren thi thuc hien cap nhat
                Mapper.CreateMap<HIS_DEPARTMENT_TRAN, HIS_DEPARTMENT_TRAN>();
                HIS_DEPARTMENT_TRAN beforeUpdate = Mapper.Map<HIS_DEPARTMENT_TRAN>(departmentTrans[0]);
                departmentTrans[0].DEPARTMENT_ID = exam.EXECUTE_DEPARTMENT_ID;

                if (!this.hisDepartmentTranUpdate.Update(departmentTrans[0], beforeUpdate))
                {
                    LogSystem.Warn("Tu dong cap nhat thong tin khoa that bai. beforeUpdate: " + LogUtil.TraceData("beforeUpdate", beforeUpdate));
                }
                upDepartId = exam.EXECUTE_DEPARTMENT_ID;
            }
        }

        private void UpdateTreatmentThreadInit(HIS_TREATMENT treatment, HisServiceReqSDO data, long? departmentId)
        {
            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessUpdateTreatment));
                thread.Priority = ThreadPriority.Highest;
                UpdateIcdTreatmentThreadData threadData = new UpdateIcdTreatmentThreadData();
                threadData.Treatment = treatment;
                threadData.IcdCode = data.IcdCode;
                threadData.IcdName = data.IcdName;
                threadData.IcdSubCode = data.IcdSubCode;
                threadData.IcdText = data.IcdText;
                threadData.UpDepartmentId = departmentId;
                thread.Start(threadData);
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh cap nhat treatment", ex);
            }
        }

        private void TestServiceReqRollbackThreadInit()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentMapTestHisServiceReqs))
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessDeleteTest));
                    thread.Priority = ThreadPriority.Normal;
                    ThreatDeleteTestData threadData = new ThreatDeleteTestData();
                    threadData.TestServiceReqs = this.recentMapTestHisServiceReqs;
                    thread.Start(threadData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh cap nhat treatment", ex);
            }
        }

        //Tien trinh cap nhat lai thong tin sere_serv
        private void ThreadProcessUpdateSereServ(object threadData)
        {
            try
            {
                UpdateSereServThreadData td = (UpdateSereServThreadData)threadData;
                List<HIS_SERE_SERV> sereServs = td.SereServs;

                if (!this.hisSereServUpdate.UpdateRaw(sereServs))
                {
                    LogSystem.Error("Cap nhat lai ti le BHYT cua sere_serv that bai");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        //Tien trinh xu ly de cap nhat thong tin ho so dieu tri (ICD)
        private void ThreadProcessUpdateTreatment(object threadData)
        {
            try
            {
                UpdateIcdTreatmentThreadData td = (UpdateIcdTreatmentThreadData)threadData;
                HIS_TREATMENT treatment = td.Treatment;

                bool hasUpdate = false;
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);//clone phuc vu rollback

                //Neu treatment chua co thong tin benh chinh thi cap nhat thong tin benh chinh cho treatment
                if (string.IsNullOrWhiteSpace(treatment.ICD_CODE) && string.IsNullOrWhiteSpace(treatment.ICD_NAME))
                {
                    if (!string.IsNullOrWhiteSpace(td.IcdCode) && !string.IsNullOrWhiteSpace(td.IcdName))
                    {
                        treatment.ICD_CODE = td.IcdCode;
                        treatment.ICD_NAME = td.IcdName;
                        hasUpdate = true;
                    }
                }

                if (HisTreatmentUpdate.AddIcd(treatment, td.IcdSubCode, td.IcdText))
                {
                    hasUpdate = true;
                }

                if (td.UpDepartmentId.HasValue)
                {
                    treatment.DEPARTMENT_IDS = td.UpDepartmentId.Value.ToString();
                    treatment.LAST_DEPARTMENT_ID = td.UpDepartmentId.Value;
                    hasUpdate = true;
                }

                //Neu co su thay doi thong tin ICD thi moi thuc hien update treatment
                if (hasUpdate && !this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                {
                    LogSystem.Warn("Cap nhat thong tin ICD cho treatment dua vao thong tin ICD cua dich vu kham that bai.");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void ThreadProcessDeleteTest(object threadData)
        {
            try
            {
                ThreatDeleteTestData td = (ThreatDeleteTestData)threadData;
                if (IsNotNullOrEmpty(td.TestServiceReqs))
                {
                    foreach (HIS_SERVICE_REQ serviceReq in this.recentMapTestHisServiceReqs.Keys.ToList())
                    {
                        List<HIS_SERE_SERV> sereServs = this.recentMapTestHisServiceReqs[serviceReq];
                        if (!new HisServiceReqTestTruncate(param).Truncate(serviceReq, sereServs))
                        {
                            LogSystem.Warn("Rollback du lieu XN that bai");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

        }

        private void IntegrateThreadInit(List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> sereServs, HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas)
        {
            try
            {
                if (OldSystemCFG.INTEGRATION_TYPE != OldSystemCFG.IntegrationType.NONE)
                {
                    IntegrateServiceReqThreadData threadData = new IntegrateServiceReqThreadData();
                    threadData.ServiceReqs = serviceReqs;
                    threadData.SereServs = sereServs;
                    Thread thread = new Thread(new ParameterizedThreadStart(this.ProcessSendingIntegration));
                    thread.Priority = ThreadPriority.Normal;
                    thread.Start(threadData);
                }

                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                Mapper.CreateMap<HIS_PATIENT_TYPE_ALTER, HIS_PATIENT_TYPE_ALTER>();
                HIS_BRANCH branch = new HisBranchGet().GetById(treatment.BRANCH_ID);

                ThreadExportXmlData threadXmlData = new ThreadExportXmlData();
                threadXmlData.Branch = branch;
                threadXmlData.PatientTypeAlter = Mapper.Map<HIS_PATIENT_TYPE_ALTER>(ptas[0]);
                threadXmlData.Treatment = treatment;
                threadXmlData.ServiceReq = Mapper.Map<HIS_SERVICE_REQ>(serviceReqs[0]);

                Thread threadXml = new Thread(new ParameterizedThreadStart(this.ProcessCreateXml));
                threadXml.Priority = ThreadPriority.Lowest;
                threadXml.Start(threadXmlData);
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh gui thong tin service_req sang HMS", ex);
            }
        }

        private void ProcessSendingIntegration(object threadData)
        {
            IntegrateServiceReqThreadData td = (IntegrateServiceReqThreadData)threadData;
            OldSystemIntegrateProcessor.CreateServiceReq(td.ServiceReqs, td.SereServs);
        }

        private void ProcessCreateXml(object threadData)
        {
            ThreadExportXmlData td = (ThreadExportXmlData)threadData;
            if (td.Treatment != null)
            {
                new HisTreatmentAutoExportXmlProcessor().Run(td.Treatment, td.Branch, td.PatientTypeAlter, td.ServiceReq);
            }
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(AssignServiceSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            //Chi ap dung trong truong hop chi dinh 1 ngay
            if (SystemCFG.IS_USING_SERVER_TIME && data != null && data.InstructionTimes != null && data.InstructionTimes.Count == 1)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                data.InstructionTimes[0] = now;
            }
        }

        private void GenerateBarcodeTest(List<HIS_SERVICE_REQ> serviceReqs, HIS_TREATMENT treatment)
        {
            GenerateBarcodeProcessor generator = new GenerateBarcodeProcessor(param);
            if (!generator.Run(serviceReqs, treatment))
            {
                throw new Exception("GenerateBarcodeProcessor. Ket thuc nghiep vu");
            }
        }

        private void FinishUpdateDB(List<HIS_SERVICE_REQ> serviceReqs)
        {
            if (!Lis2CFG.IS_CALL_GENERATE_BARCODE)
            {
                if (!BarcodeGenerator.FinishUpdateDB(serviceReqs.Select(s => s.BARCODE).ToList()))
                {
                    LogSystem.Warn("BarcodeGenerator.FinishUpdateDB that bai");
                }
            }
        }

        private void ProcessCheckBaseSalary(HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> patyAlters, List<HIS_SERE_SERV> existedSereServs, List<HIS_SERE_SERV> newSereServs)
        {
            try
            {
                List<HIS_SERE_SERV> sereServs = new List<HIS_SERE_SERV>();
                if (IsNotNullOrEmpty(existedSereServs))
                {
                    sereServs.AddRange(existedSereServs);
                }
                if (IsNotNullOrEmpty(newSereServs))
                {
                    sereServs.AddRange(newSereServs);
                }
                new HisTreatmentCheckOverSixMonthSalary(param).Run(treatment, patyAlters, sereServs);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
