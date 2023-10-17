using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Bed.CreateByBedLog
{
    internal class HisServiceReqProcessor : BusinessBase
    {
        private HisServiceReqCreate hisServiceReqCreate;

        internal HisServiceReqProcessor()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
        }

        internal void Run(HisBedServiceReqSDO sdo, HIS_TREATMENT treatment, string sessionCode, WorkPlaceSDO workPlace, List<HIS_PATIENT_TYPE_ALTER> ptas, ref Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>> dicSereServ, ref Dictionary<long, List<HIS_SERVICE_REQ>> dicBedLog)
        {
            Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>> ssDic = new Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>>();
            Dictionary<long, List<HIS_SERVICE_REQ>> bedLogDic = new Dictionary<long, List<HIS_SERVICE_REQ>>();

            List<HIS_SERVICE_REQ> toInserts = new List<HIS_SERVICE_REQ>();

            if (sdo != null && IsNotNullOrEmpty(sdo.BedServices))
            {
                foreach (HisBedServiceSDO s in sdo.BedServices)
                {
                    HIS_SERVICE_REQ serviceReq = this.MakeServiceReq(s, treatment, ptas, sessionCode, workPlace);
                    List<HIS_SERE_SERV> sereServs = this.MakeSereServ(s.ServiceReqDetails);
                    toInserts.Add(serviceReq);
                    ssDic.Add(serviceReq, sereServs);
                    if (!bedLogDic.ContainsKey(s.BedLogId))
                    {
                        bedLogDic[s.BedLogId] = new List<HIS_SERVICE_REQ>();
                    }

                    bedLogDic[s.BedLogId].Add(serviceReq);
                }
            }

            if (!this.hisServiceReqCreate.CreateList(toInserts, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            dicSereServ = ssDic;
            dicBedLog = bedLogDic;
        }

        private HIS_SERVICE_REQ MakeServiceReq(HisBedServiceSDO sdo, HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas, string sessionCode, WorkPlaceSDO workPlace)
        {
            HIS_PATIENT_TYPE_ALTER pta = new HisPatientTypeAlterGet().GetApplied(treatment.ID, sdo.InstructionTime, ptas);

            HIS_SERVICE_REQ serviceReq = new HIS_SERVICE_REQ();
            serviceReq.REQUEST_ROOM_ID = workPlace.RoomId;
            serviceReq.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
            serviceReq.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;
            serviceReq.EXECUTE_ROOM_ID = workPlace.RoomId;
            serviceReq.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__G;
            serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
            serviceReq.PRIORITY = sdo.Priority;
            serviceReq.PARENT_ID = sdo.ParentId;
            serviceReq.INTRUCTION_TIME = sdo.InstructionTime;
            serviceReq.TREATMENT_ID = treatment.ID;
            serviceReq.ICD_TEXT = treatment.ICD_TEXT;//icd lay mac dinh trong treatment
            serviceReq.ICD_NAME = treatment.ICD_NAME;//icd lay mac dinh trong treatment
            serviceReq.ICD_CODE = treatment.ICD_CODE;//icd lay mac dinh trong treatment
            serviceReq.ICD_CAUSE_CODE = treatment.ICD_CAUSE_CODE;
            serviceReq.ICD_CAUSE_NAME = treatment.ICD_CAUSE_NAME;
            serviceReq.IS_NOT_REQUIRE_FEE = sdo.IsNotRequireFee;
            serviceReq.TDL_PATIENT_ID = treatment.PATIENT_ID;
            serviceReq.DESCRIPTION = sdo.Description;
            serviceReq.TREATMENT_TYPE_ID = pta.TREATMENT_TYPE_ID;
            serviceReq.SESSION_CODE = sessionCode;

            return serviceReq;
        }

        private List<HIS_SERE_SERV> MakeSereServ(List<ServiceReqDetailSDO> serviceReqDetails)
        {
            List<HIS_SERE_SERV> result = null;
            if (IsNotNullOrEmpty(serviceReqDetails))
            {
                result = new List<HIS_SERE_SERV>();

                //Tao danh sach sere_serv tuong ung voi service_req
                foreach (ServiceReqDetailSDO req in serviceReqDetails)
                {
                    HIS_SERE_SERV sereServ = new HIS_SERE_SERV();
                    sereServ.SERVICE_ID = req.ServiceId;
                    sereServ.AMOUNT = req.Amount;
                    sereServ.PARENT_ID = req.ParentId;
                    sereServ.PATIENT_TYPE_ID = req.PatientTypeId;
                    if (HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.AUTO
                        || HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.YES)
                    {
                        sereServ.PRIMARY_PATIENT_TYPE_ID = req.PrimaryPatientTypeId;
                    }
                    sereServ.IS_EXPEND = req.IsExpend;
                    sereServ.IS_OUT_PARENT_FEE = req.IsOutParentFee;
                    sereServ.EKIP_ID = req.EkipId;
                    sereServ.SHARE_COUNT = req.ShareCount;
                    sereServ.OTHER_PAY_SOURCE_ID = req.OtherPaySourceId;
                    result.Add(sereServ);
                }
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisServiceReqCreate.RollbackData();
        }
    }
}
