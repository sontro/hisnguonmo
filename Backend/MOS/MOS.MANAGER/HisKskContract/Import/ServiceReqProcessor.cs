using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.CodeGenerator.HisServiceReq;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisKskService;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.MANAGER.HisServiceReq.Exam;
using MOS.MANAGER.HisServiceReq.Test.GenerateBarcode;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskContract.Import
{
    class ServiceReqProcessor : BusinessBase
    {
        private HisServiceReqCreate hisServiceReqCreate;
        private HisSereServCreate hisSereServCreate;

        private Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>> SR_SS_MAPPING = new Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>>();

        internal ServiceReqProcessor()
            : base()
        {
            this.Init();
        }

        internal ServiceReqProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
        }

        internal bool Run(PrepareData prepareData, HIS_PATIENT_TYPE_ALTER ptAlter, WorkPlaceSDO workPlace, string loginname, string username, ref string desc)
        {
            bool result = false;
            try
            {
                List<ServiceReqDetailSDO> ServiceReqDetails = new List<ServiceReqDetailSDO>();
                List<V_HIS_SERVICE> services = new List<V_HIS_SERVICE>();

                foreach (HIS_KSK_SERVICE service in prepareData.KskServices)
                {
                    ServiceReqDetailSDO sdo = new ServiceReqDetailSDO();
                    sdo.Amount = service.AMOUNT;
                    sdo.PatientTypeId = ptAlter.PATIENT_TYPE_ID;
                    sdo.RoomId = service.ROOM_ID;
                    sdo.ServiceId = service.SERVICE_ID;
                    if (service.PRICE.HasValue)
                    {
                        decimal vat = service.VAT_RATIO.HasValue ? service.VAT_RATIO.Value : 0;
                        sdo.UserPrice = service.PRICE * (1 + vat);
                    }

                    ServiceReqDetails.Add(sdo);
                    V_HIS_SERVICE s = HisServiceCFG.DATA_VIEW.Where(o => o.ID == service.SERVICE_ID).FirstOrDefault();
                    if (s != null)
                    {
                        services.Add(s);
                    }
                }

                // Lay ra cac chi dinh dich vu cu de sap xep uu tien chi dinh phong
                List<HIS_SERE_SERV> existSerServs = new HisSereServGet().GetByTreatmentId(ptAlter.TREATMENT_ID);
                HisServiceReqRoomAssign roomAssigner = new HisServiceReqRoomAssign(param, services, ServiceReqDetails, workPlace.RoomId, workPlace.DepartmentId, workPlace.BranchId, new List<long>() { prepareData.Treatment.IN_TIME }, existSerServs);
                List<RoomAssignData> assignedRooms = roomAssigner.RoomAssign();

                //Tao du lieu service_req theo du lieu phan bo dich vu theo phong
                List<HIS_SERVICE_REQ> serviceReqs = this.MakeServiceReq(prepareData.KskContract, ptAlter, prepareData.Treatment, assignedRooms, workPlace, prepareData.HisKskPatientSDO.Barcode, loginname, username, prepareData.HisKskPatientSDO);

                //Goi sang he thong lis sinh barcode trong truong hop cau hinh sinh barcode tu he thong LIS
                this.GenerateBarcodeTest(serviceReqs, prepareData.Treatment);

                //Viec verify treatment da duoc thuc hien o phia tren, nen ko thuc hien verify lai
                if (!this.hisServiceReqCreate.CreateList(serviceReqs, prepareData.Treatment, false))
                {
                    throw new Exception("hisServiceReqCreate Rollback du lieu. Ket thuc nghiep vu");
                }

                this.FinishUpdateDB(serviceReqs);

                //Xu ly thong tin sere_serv: insert du lieu moi, update du lieu cu (cac du lieu da co trong DB)
                List<HIS_SERE_SERV> newSereServs = null;
                this.ProcessSereServ(workPlace, prepareData.Treatment, ref newSereServs, serviceReqs);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                if (string.IsNullOrWhiteSpace(desc))
                {
                    desc = !String.IsNullOrWhiteSpace(param.GetMessage()) ? param.GetMessage() : param.GetBugCode();
                }
            }
            return result;
        }

        //Tao du lieu service_req phan bo theo phong
        private List<HIS_SERVICE_REQ> MakeServiceReq(HIS_KSK_CONTRACT kskContract, HIS_PATIENT_TYPE_ALTER usingPta, HIS_TREATMENT treatment, List<RoomAssignData> assignedRooms, WorkPlaceSDO workPlace, string barcode, string loginname, string username, HisKskPatientSDO kskPatientSdo)
        {
            List<HIS_SERVICE_REQ> result = new List<HIS_SERVICE_REQ>();

            long maxId = 0;

            bool isExistsExam = false;
            string sessionCode = Guid.NewGuid().ToString();

            foreach (RoomAssignData roomAssignData in assignedRooms)
            {
                if (!IsNotNullOrEmpty(roomAssignData.ServiceReqDetails))
                {
                    continue;
                }

                HIS_SERVICE_REQ serviceReq = new HIS_SERVICE_REQ();
                serviceReq.EXECUTE_DEPARTMENT_ID = roomAssignData.DepartmentId;
                serviceReq.EXECUTE_ROOM_ID = roomAssignData.RoomId;
                serviceReq.SERVICE_REQ_TYPE_ID = roomAssignData.ServiceReqTypeId;
                serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                serviceReq.INTRUCTION_TIME = treatment.IN_TIME;
                serviceReq.TREATMENT_ID = treatment.ID;
                serviceReq.REQUEST_ROOM_ID = workPlace.RoomId;
                serviceReq.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
                serviceReq.REQUEST_LOGINNAME = loginname;
                serviceReq.REQUEST_USERNAME = username;
                serviceReq.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(loginname);
                serviceReq.TREATMENT_TYPE_ID = usingPta.TREATMENT_TYPE_ID;
                serviceReq.EXE_SERVICE_MODULE_ID = roomAssignData.ExeServiceModuleId;
                serviceReq.TDL_KSK_IS_REQUIRED_APPROVAL = kskContract.IS_REQUIRED_APPROVAL;
                serviceReq.SESSION_CODE = sessionCode;
                serviceReq.BARCODE_LENGTH = HisServiceReqCFG.LIS_SID_LENGTH;//su dung truong barcode cho SID ben LIS (labconn, roche, ...)
                serviceReq.TDL_KSK_CONTRACT_ID = kskContract.ID;
                serviceReq.TDL_KSK_CONTRACT_IS_RESTRICTED = kskContract.IS_RESTRICTED;
                serviceReq.ALLOW_SEND_PACS = roomAssignData.ServiceReqDetails.Exists(o => o.AllowSendPacs) ? (short?)Constant.IS_TRUE : null;

                if (!string.IsNullOrWhiteSpace(kskPatientSdo.IcdCode) && !string.IsNullOrWhiteSpace(kskPatientSdo.IcdName))
                {
                    serviceReq.ICD_CODE = kskPatientSdo.IcdCode;
                    serviceReq.ICD_NAME = kskPatientSdo.IcdName;
                }
                HIS_ICD hisIcdCode = new HisIcdGet().GetByCode(kskPatientSdo.IcdCode);
                List<HIS_ICD> hisIcdSubCodes = new List<HIS_ICD>();
                if (!string.IsNullOrWhiteSpace(kskPatientSdo.IcdSubCode))
                {
                    List<string> subCodes = new List<string>();
                    subCodes = kskPatientSdo.IcdSubCode.Split(';').ToList();
                    hisIcdSubCodes = new HisIcdGet().GetByCodes(subCodes);
                }

                if (!string.IsNullOrWhiteSpace(kskPatientSdo.IcdCode) && string.IsNullOrWhiteSpace(kskPatientSdo.IcdName))
                {
                    serviceReq.ICD_CODE = kskPatientSdo.IcdCode;
                    if (hisIcdCode != null && hisIcdCode.ICD_NAME != null)
                    {
                        serviceReq.ICD_NAME = hisIcdCode.ICD_NAME;
                    }
                    else
                    {
                        serviceReq.ICD_NAME = "";
                    }
                }
                if (!string.IsNullOrWhiteSpace(kskPatientSdo.IcdSubCode) && !string.IsNullOrWhiteSpace(kskPatientSdo.IcdText))
                {
                    serviceReq.ICD_SUB_CODE = kskPatientSdo.IcdSubCode;
                    serviceReq.ICD_TEXT = kskPatientSdo.IcdText;
                }
                if (!string.IsNullOrWhiteSpace(kskPatientSdo.IcdSubCode) && string.IsNullOrWhiteSpace(kskPatientSdo.IcdText))
                {
                    if (hisIcdCode != null)
                    {
                        List<string> icdSubCodes = hisIcdSubCodes.Select(o => o.ICD_CODE).ToList();
                        List<string> icdNames = hisIcdSubCodes.Select(o => o.ICD_NAME).ToList();

                        serviceReq.ICD_SUB_CODE = string.Join(";", icdSubCodes);
                        serviceReq.ICD_TEXT = string.Join(";", icdNames);
                    }
                    else
                    {
                        serviceReq.ICD_SUB_CODE = "";
                        serviceReq.ICD_TEXT = "";
                    }
                }
                if (string.IsNullOrWhiteSpace(kskPatientSdo.IcdSubCode) && string.IsNullOrWhiteSpace(kskPatientSdo.IcdText))
                {
                    serviceReq.ICD_SUB_CODE = "";
                    serviceReq.ICD_TEXT = "";
                }
                if (string.IsNullOrWhiteSpace(kskPatientSdo.IcdCode) && string.IsNullOrWhiteSpace(kskPatientSdo.IcdName))
                {
                    serviceReq.ICD_CODE = "";
                    serviceReq.ICD_NAME = "";
                }
                //Neu ho so chua co chi dinh kham nao thi set chi dinh kham nay la "kham chinh"
                if (!isExistsExam && roomAssignData.ServiceReqTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                {
                    serviceReq.IS_MAIN_EXAM = Constant.IS_TRUE;
                    isExistsExam = true;
                }

                //Neu la chi dinh phau thuat thi mac dinh la chua duoc duyet
                if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT)
                {
                    serviceReq.PTTT_APPROVAL_STT_ID = IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__NEW;
                }

                if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN
                    && !String.IsNullOrWhiteSpace(barcode))
                {
                    serviceReq.IS_SEND_BARCODE_TO_LIS = Constant.IS_TRUE;
                    serviceReq.BARCODE_TEMP = barcode;
                }

                List<HIS_SERE_SERV> ss = new List<HIS_SERE_SERV>();
                //Tao danh sach sere_serv tuong ung voi service_req
                foreach (ServiceReqDetail req in roomAssignData.ServiceReqDetails)
                {
                    HIS_SERE_SERV sereServ = new HIS_SERE_SERV();
                    sereServ.SERVICE_ID = req.ServiceId;
                    sereServ.AMOUNT = req.Amount;
                    sereServ.PARENT_ID = req.ParentId;
                    sereServ.PATIENT_TYPE_ID = req.PatientTypeId;
                    sereServ.HEIN_RATIO = kskContract.PAYMENT_RATIO;
                    sereServ.USER_PRICE = req.UserPrice;
                    sereServ.TDL_IS_MAIN_EXAM = serviceReq.IS_MAIN_EXAM;//luu du thua du lieu
                    sereServ.ID = ++maxId; //tao fake id (de dinh danh sere_serv khi chua insert vao DB)
                    ss.Add(sereServ);
                }

                SR_SS_MAPPING.Add(serviceReq, ss);
                result.Add(serviceReq);
            }
            return result;
        }

        private void ProcessSereServ(WorkPlaceSDO workPlace, HIS_TREATMENT treatment, ref List<HIS_SERE_SERV> newSereServs, List<HIS_SERVICE_REQ> serviceReqs)
        {
            if (IsNotNullOrEmpty(serviceReqs))
            {
                //Neu priceAdder ko duoc truyen vao thi khoi tao priceAdder
                HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);

                newSereServs = new List<HIS_SERE_SERV>();
                foreach (HIS_SERVICE_REQ sr in serviceReqs)
                {
                    HIS_DEPARTMENT executeDepartment = HisDepartmentCFG.DATA
                        .Where(o => o.ID == sr.EXECUTE_DEPARTMENT_ID).FirstOrDefault();

                    List<HIS_SERE_SERV> list = SR_SS_MAPPING[sr];
                    foreach (HIS_SERE_SERV ss in list)
                    {
                        ss.SERVICE_REQ_ID = sr.ID;
                        //can set thua du lieu o day luon do phan xu ly ti le thanh toan co su dung cac truong thua du lieu
                        HisSereServUtil.SetTdl(ss, sr);
                        if (!priceAdder.AddPrice(ss, treatment.IN_TIME, executeDepartment.BRANCH_ID, workPlace.RoomId, sr.REQUEST_DEPARTMENT_ID, sr.EXECUTE_ROOM_ID))
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                        }
                    }
                    newSereServs.AddRange(list);
                }
            }

            if (!this.hisSereServCreate.CreateList(newSereServs, serviceReqs, false))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        private void GenerateBarcodeTest(List<HIS_SERVICE_REQ> serviceReqs, HIS_TREATMENT treatment)
        {
            List<HIS_SERVICE_REQ> needs = serviceReqs.Where(o => String.IsNullOrWhiteSpace(o.BARCODE_TEMP)).ToList();
            if (IsNotNullOrEmpty(needs))
            {
                GenerateBarcodeProcessor generator = new GenerateBarcodeProcessor(param);
                if (!generator.Run(needs, treatment))
                {
                    throw new Exception("GenerateBarcodeProcessor. Ket thuc nghiep vu");
                }
            }
        }

        private void FinishUpdateDB(List<HIS_SERVICE_REQ> serviceReqs)
        {
            if (!Lis2CFG.IS_CALL_GENERATE_BARCODE && HisServiceReqCFG.GENERATE_BARCODE_OPTION == HisServiceReqCFG.GenrateBarcodeOption.DAY_WITH_NUMBER)
            {
                if (!BarcodeGenerator.FinishUpdateDB(serviceReqs.Select(s => s.BARCODE).ToList()))
                {
                    LogSystem.Warn("BarcodeGenerator.FinishUpdateDB that bai");
                }
            }
        }

        internal void Rollback()
        {
            this.hisSereServCreate.RollbackData();
            this.hisServiceReqCreate.RollbackData();
        }
    }
}
