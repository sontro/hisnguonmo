using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisNumOrderBlock;
using MOS.MANAGER.HisNumOrderIssue;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.MANAGER.HisTransReq.CreateByService;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq.Exam.Register
{
    class ServiceReqServiceDetailMapping
    {
        public HIS_SERVICE_REQ ServiceReq { get; set; }
        public List<long> ServiceDetailDummyIds { get; set; }
    }

    class AttachmentServiceReqMapping
    {
        public HIS_SERVICE_REQ Parent { get; set; }
        public HIS_SERVICE_REQ Child { get; set; }
    }

    /// <summary>
    /// Dang ky tiep don
    /// </summary>
    partial class HisServiceReqCreateOnRegister : BusinessBase
    {
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisSereServCreate hisSereServCreate;
        private HisServiceReqCreate hisServiceReqCreate;

        //Anh xa giua service_req va d/s sere_serv tuong ung
        private Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>> SR_SS_MAPPING = new Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>>();
        
        

        //Anh xa giua dich vu y/c va y lenh duoc tao ra
        private List<ServiceReqServiceDetailMapping> SR_SERVICEDETAIL = new List<ServiceReqServiceDetailMapping>();

        private List<AttachmentServiceReqMapping> ATTACHMENT_MAPPING = new List<AttachmentServiceReqMapping>();

        internal HisServiceReqCreateOnRegister()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqCreateOnRegister(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            //Nghiep vu xu ly o day luon thuc hien sau nghiep vu tao service_req
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
        }

        internal bool Create(AssignServiceSDO data, HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER pta, ref HisServiceReqListResultSDO resultData)
        {
            bool result = false;
            try
            {
                data.InstructionTimes = new List<long>() { data.InstructionTime };

                //Viec tao service_req luon duoc verify treatment 
                //vi vay ko can verify lai treatment khi update sere_serv
                this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, new List<HIS_PATIENT_TYPE_ALTER>() { pta }, false);
                
                WorkPlaceSDO workPlace = null;
                string sessionCode = Guid.NewGuid().ToString();

                //Neu phong lam viec khong hop le thi ket thuc xu ly
                if (!this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace))
                {
                    return false;
                }
                
                //Xu ly de tu dong bo sung cac dich vu dinh kem theo cau hinh
                AttachmentProcessor.AddAttachmentService(data, workPlace.BranchId, workPlace.DepartmentId);

                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                List<V_HIS_SERVICE> services = null;
                bool valid = checker.VerifyRequireField(data);
                valid = valid && checker.IsValidData(data.ServiceReqDetails, data.InstructionTimes, ref services);
                
                if (valid)
                {
                    //Tao du lieu service_reqs
                    List<HIS_SERE_SERV> newSereServs = null;

                    //Tao du lieu service_req theo du lieu phan bo dich vu theo phong
                    List<HIS_SERVICE_REQ> serviceReqs = this.MakeServiceReq(data, services, treatment, pta, sessionCode, workPlace);

                    //Viec verify treatment da duoc thuc hien o phia tren, nen ko thuc hien verify lai
                    if (!this.hisServiceReqCreate.CreateList(serviceReqs, treatment))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Cap nhat du lieu y lenh dinh kem
                    AttachmentProcessor.UpdateAttachedId(ATTACHMENT_MAPPING);

                    //Xu ly thong tin sere_serv: insert du lieu moi
                    this.ProcessSereServ(data.RequestRoomId, data.InstructionTime, treatment, serviceReqs, ref newSereServs);

                    if (newSereServs.Exists(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && o.VIR_TOTAL_PATIENT_PRICE > 0))
                    {
                        if (!new HisTransReqCreateByService(param).Run(treatment, serviceReqs, workPlace))
                        {
                            Inventec.Common.Logging.LogSystem.Error("Tao HisTransReq that bai");
                            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("tranReqParam", param));
                        }
                    }

                    this.PassResult(newSereServs, serviceReqs, ref resultData);
                    result = true;
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

        private void ProcessSereServ(long requestRoomId, long instructionTime, HIS_TREATMENT treatment, List<HIS_SERVICE_REQ> serviceReqs, ref List<HIS_SERE_SERV> newSereServs)
        {
            //cap nhat lai thong tin service_req_id va cac thong tin du thua du lieu
            newSereServs = new List<HIS_SERE_SERV>();
            foreach (HIS_SERVICE_REQ sr in serviceReqs)
            {
                List<HIS_SERE_SERV> list = SR_SS_MAPPING.ContainsKey(sr) ? SR_SS_MAPPING[sr] : null;
                if (IsNotNullOrEmpty(list))
                {
                    foreach (HIS_SERE_SERV ss in list)
                    {
                        ss.SERVICE_REQ_ID = sr.ID;
                        //can set thua du lieu o day luon do phan xu ly ti le thanh toan 
                        //co su dung cac truong thua du lieu
                        HisSereServUtil.SetTdl(ss, sr);
                        newSereServs.Add(ss);
                    }
                }
            }

            if (IsNotNullOrEmpty(newSereServs))
            {
                //Cap nhat du lieu sere_serv
                List<HIS_SERE_SERV> changeSereServs = null;
                List<HIS_SERE_SERV> oldOfChangeSereServs = null;

                //Xu ly de set thong tin ti le chi tra, doi tuong,...
                if (!this.hisSereServUpdateHein.Update(null, newSereServs, newSereServs, ref changeSereServs, ref oldOfChangeSereServs))
                {
                    throw new Exception("Rollback du lieu");
                }

                if (!this.hisSereServCreate.CreateList(newSereServs, serviceReqs, false))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void PassResult(List<HIS_SERE_SERV> sereServs, List<HIS_SERVICE_REQ> serviceReqs, ref HisServiceReqListResultSDO resultData)
        {
            resultData = new HisServiceReqListResultSDO();
            List<long> serviceReqIds = serviceReqs.Select(o => o.ID).ToList();
            List<long> sereServIds = sereServs.Select(o => o.ID).ToList();
            resultData.ServiceReqs = new HisServiceReqGet().GetViewByIds(serviceReqIds);
            resultData.SereServs = new HisSereServGet().GetViewByIds(sereServIds);
        }

        //Tao du lieu service_req phan bo theo phong
        private List<HIS_SERVICE_REQ> MakeServiceReq(AssignServiceSDO data, List<V_HIS_SERVICE> services, HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER pta, string sessionCode, WorkPlaceSDO workPlace)
        {
            //Chi lay ra cac chi dinh co dich vu de xu ly phan bo vao cac phong
            List<ServiceReqDetailSDO> serviceReqDetails = data != null && data.ServiceReqDetails != null ? data.ServiceReqDetails.Where(o => o.ServiceId > 0).ToList() : null;

            //Lay ra cac chi dinh ko co dich vu de tao service_req
            List<ServiceReqDetailSDO> nonServiceReqDetails = data != null && data.ServiceReqDetails != null ? data.ServiceReqDetails.Where(o => o.ServiceId == 0 && o.RoomId.HasValue).ToList() : null;

            //Danh sach phan bo phong xu ly tuong ung voi dich vu duoc yeu cau
            List<RoomAssignData> assignedRooms = null;
            if (IsNotNullOrEmpty(serviceReqDetails))
            {
                // Lay ra cac chi dinh dich vu cu de sap xep uu tien chi dinh phong
                List<HIS_SERE_SERV> existSerServs = new HisSereServGet().GetByTreatmentId(treatment.ID);
                HisServiceReqRoomAssign roomAssigner = new HisServiceReqRoomAssign(param, services, serviceReqDetails, workPlace.RoomId, workPlace.DepartmentId, workPlace.BranchId, data.InstructionTimes, existSerServs);
                assignedRooms = roomAssigner.RoomAssign();
            }


            List<HIS_SERVICE_REQ> result = new List<HIS_SERVICE_REQ>();
            List<HIS_SERE_SERV> allSereServs = new List<HIS_SERE_SERV>();

            long fakeId = 0;
            bool isExistsExam = false;

            //Tao y lenh cho cac chi dinh ma ko gan dich vu
            if (IsNotNullOrEmpty(nonServiceReqDetails))
            {
                foreach (ServiceReqDetailSDO nonService in nonServiceReqDetails)
                {
                    V_HIS_EXECUTE_ROOM executeRoom = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == nonService.RoomId.Value).FirstOrDefault();
                    HIS_SERVICE_TYPE examServiceType = HisServiceTypeCFG.DATA.Where(o => o.ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH).FirstOrDefault();

                    //mac dinh cac chi dinh ko co dich vu se dien la "Kham"
                    HIS_SERVICE_REQ serviceReq = this.MakeServiceReq(data, workPlace, sessionCode, pta, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH);

                    serviceReq.EXECUTE_ROOM_ID = nonService.RoomId.Value;
                    serviceReq.EXECUTE_DEPARTMENT_ID = executeRoom.DEPARTMENT_ID;
                    serviceReq.EXE_SERVICE_MODULE_ID = examServiceType.EXE_SERVICE_MODULE_ID;
                    serviceReq.ASSIGNED_EXECUTE_LOGINNAME = nonService.AssignedExecuteLoginName;
                    serviceReq.ASSIGNED_EXECUTE_USERNAME = nonService.AssignedExecuteUserName;

                    //Xu ly de lay STT kham
                    new HisNumOrderIssueUtil(param).SetNumOrder(serviceReq, nonService.NumOrderBlockId, nonService.NumOrderIssueId, nonService.NumOrder);

                    if (!isExistsExam)
                    {
                        serviceReq.IS_MAIN_EXAM = Constant.IS_TRUE;
                        isExistsExam = true;
                    }

                    result.Add(serviceReq);
                }
            }

            if (IsNotNullOrEmpty(assignedRooms))
            {
                //Tao y lenh cho cac chi dinh co gan dich vu
                foreach (RoomAssignData roomAssignData in assignedRooms)
                {
                    if (!IsNotNullOrEmpty(roomAssignData.ServiceReqDetails))
                    {
                        continue;
                    }

                    HIS_SERVICE_REQ serviceReq = this.MakeServiceReq(data, workPlace, sessionCode, pta, roomAssignData.ServiceReqTypeId);
                    serviceReq.EXECUTE_DEPARTMENT_ID = roomAssignData.DepartmentId;
                    serviceReq.EXECUTE_ROOM_ID = roomAssignData.RoomId;
                    serviceReq.EXE_SERVICE_MODULE_ID = roomAssignData.ExeServiceModuleId;
                    serviceReq.IS_ANTIBIOTIC_RESISTANCE = roomAssignData.IsAntibioticResistance ? (short?)Constant.IS_TRUE : null;
                    serviceReq.ASSIGNED_EXECUTE_LOGINNAME = roomAssignData.AssignedExecuteLoginName;
                    serviceReq.ASSIGNED_EXECUTE_USERNAME = roomAssignData.AssignedExecuteUserName;
                    serviceReq.IS_NOT_REQUIRED_COMPLETE = roomAssignData.ServiceReqDetails.Exists(o => o.IsNotRequiredComplete) ? (short?)Constant.IS_TRUE : null;

                    List<long> serviceIds = roomAssignData.ServiceReqDetails.Select(o => o.ServiceId).Distinct().ToList();
                    var servicesCurrent = services != null ? services.Where(o => serviceIds.Contains(o.ID)) : null;
                    if (servicesCurrent != null && servicesCurrent.Any(o => o.ALLOW_SEND_PACS == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE))
                    {
                        serviceReq.ALLOW_SEND_PACS = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                    }

                    //Xu ly de lay STT kham
                    new HisNumOrderIssueUtil(param).SetNumOrder(serviceReq, roomAssignData.NumOrderBlockId, roomAssignData.NumOrderIssueId, roomAssignData.NumOrder);

                    //Neu ho so chua co chi dinh kham nao thi set chi dinh kham nay la "kham chinh"
                    if (!isExistsExam && roomAssignData.ServiceReqTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                    {
                        serviceReq.IS_MAIN_EXAM = Constant.IS_TRUE;
                        isExistsExam = true;
                    }

                    List<HIS_SERE_SERV> ss = new List<HIS_SERE_SERV>();

                    List<long> serviceReqDetailDummyIds = new List<long>();
                    //Tao danh sach sere_serv tuong ung voi service_req
                    foreach (ServiceReqDetail req in roomAssignData.ServiceReqDetails)
                    {
                        if (req.DummyId.HasValue)
                        {
                            serviceReqDetailDummyIds.Add(req.DummyId.Value);
                        }
                        
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
                        sereServ.OTHER_PAY_SOURCE_ID = req.OtherPaySourceId;

                        sereServ.TDL_IS_MAIN_EXAM = serviceReq.IS_MAIN_EXAM;//luu du thua du lieu
                        sereServ.ID = ++fakeId; //tao fake id (de dinh danh sere_serv khi chua insert vao DB)
                        //Chi gan doi tuong phu thu khi bat cau hinh
                        if (HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.AUTO
                            || HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.YES)
                        {
                            sereServ.PRIMARY_PATIENT_TYPE_ID = req.PrimaryPatientTypeId;
                        }
                        HisSereServUtil.SetTdl(sereServ, serviceReq);
                        ss.Add(sereServ);
                        allSereServs.Add(sereServ);

                        
                    }
                    serviceReq.TDL_SERVICE_IDS = String.Join(";", ss.Select(s => s.SERVICE_ID).ToList());

                    //Gan vao d/s anh xa giua y lenh (his_service_req) va chi tiet y lenh (his_sere_serv) tao ra de phuc vu sau khi them moi
                    //thi cap nhat duoc service_req_id trong his_sere_serv
                    SR_SS_MAPPING.Add(serviceReq, ss);

                    //Gan vao d/s anh xa giua y lenh (his_service_req) va chi tiet y lenh (his_sere_serv) tao ra de phuc vu sau khi them moi
                    //thi cap nhat duoc service_req_id trong his_sere_serv
                    ServiceReqServiceDetailMapping mapping = new ServiceReqServiceDetailMapping();
                    mapping.ServiceDetailDummyIds = serviceReqDetailDummyIds;
                    mapping.ServiceReq = serviceReq;
                    SR_SERVICEDETAIL.Add(mapping);

                    result.Add(serviceReq);
                }

                //Cap nhat anh xa giua y lenh chinh va y lenh dinh kem de phuc vu cap nhat truong ATTACHED_ID trong HIS_SERVICE_REQ
                ATTACHMENT_MAPPING = AttachmentProcessor.GetAttachmentMapping(SR_SERVICEDETAIL, data.ServiceReqDetails);

                List<long> allSereServIds = allSereServs.Select(o => o.ID).ToList();

                SereServPriceUtil.ReloadPrice(param, treatment, allSereServs, allSereServIds);
            }

            return result;
        }



        private HIS_SERVICE_REQ MakeServiceReq(AssignServiceSDO data, WorkPlaceSDO workPlace, string sessionCode, HIS_PATIENT_TYPE_ALTER pta, long serviceReqTypeId)
        {
            HIS_SERVICE_REQ serviceReq = new HIS_SERVICE_REQ();

            serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
            serviceReq.PRIORITY = data.Priority;
            serviceReq.PRIORITY_TYPE_ID = data.PriorityTypeId;
            serviceReq.PARENT_ID = data.ParentServiceReqId;
            serviceReq.INTRUCTION_TIME = data.InstructionTimes[0];//voi tiep don thi chi luon chi co 1 thoi gian chi dinh
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
            serviceReq.REQUEST_ROOM_ID = workPlace.RoomId;
            serviceReq.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
            serviceReq.REQUEST_LOGINNAME = data.RequestLoginName;
            serviceReq.REQUEST_USERNAME = data.RequestUserName;
            serviceReq.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.RequestLoginName);
            serviceReq.TRACKING_ID = data.TrackingId;
            serviceReq.TREATMENT_TYPE_ID = pta.TREATMENT_TYPE_ID;
            serviceReq.SESSION_CODE = sessionCode;
            serviceReq.IS_EMERGENCY = data.IsEmergency ? (short?)Constant.IS_TRUE : null;
            serviceReq.IS_INFORM_RESULT_BY_SMS = data.IsInformResultBySms ? (short?)Constant.IS_TRUE : null;
            serviceReq.KIDNEY_SHIFT = data.KidneyShift;
            serviceReq.MACHINE_ID = data.MachineId;
            serviceReq.EXP_MEST_TEMPLATE_ID = data.ExpMestTemplateId;
            serviceReq.IS_KIDNEY = data.IsKidney ? (short?)Constant.IS_TRUE : null;
            serviceReq.PROVISIONAL_DIAGNOSIS = data.ProvisionalDiagnosis;
            serviceReq.SERVICE_REQ_TYPE_ID = serviceReqTypeId;
            serviceReq.TDL_KSK_CONTRACT_ID = pta.KSK_CONTRACT_ID;

            if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
            {
                serviceReq.RESERVED_NUM_ORDER = HisServiceReqCFG.RESERVED_NUM_ORDER;
                serviceReq.NOTE = data.Note;
            }

            //Neu la chi dinh phau thuat thi mac dinh la chua duoc duyet
            if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT)
            {
                serviceReq.PTTT_APPROVAL_STT_ID = IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__NEW;
            }
            
            return serviceReq;
        }

        /// <summary>
        /// Rollback du lieu yeu cau dich vu:
        /// </summary>
        /// <param name="hisServiceReq"></param>
        /// <returns></returns>
        internal void RollbackData()
        {
            this.hisServiceReqCreate.RollbackData();
        }
    }
}
