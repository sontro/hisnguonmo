using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Blood
{
    class HisServiceReqMaker : BusinessBase
    {
        private HisServiceReqCreate hisServiceReqCreate;

        internal HisServiceReqMaker()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqMaker(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
        }

        internal bool Run(HIS_TREATMENT treatment, PatientBloodPresSDO data, V_HIS_MEDI_STOCK mediStock, List<HIS_PATIENT_TYPE_ALTER> ptas, string sessionCode, ref HIS_SERVICE_REQ resultData)
        {
            try
            {
                if (IsNotNullOrEmpty(data.ExpMestBltyReqs))
                {
                    HIS_SERVICE_REQ serviceReq = null;
                    this.MakeData(data, ptas, mediStock, sessionCode, ref serviceReq);
                    if (IsNotNull(serviceReq))
                    {
                        if (!this.hisServiceReqCreate.Create(serviceReq, treatment))
                        {
                            LogSystem.Info("HisServiceReqMaker => ServiceReqCreate => Create fail____" + LogUtil.TraceData("serviceReq", serviceReq) + LogUtil.TraceData("treatment", treatment));
                            return false;
                        }
                        resultData = serviceReq;
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        private void MakeData(PatientBloodPresSDO data, List<HIS_PATIENT_TYPE_ALTER> ptas, V_HIS_MEDI_STOCK mediStock, string sessionCode, ref HIS_SERVICE_REQ resultData)
        {
            HIS_PATIENT_TYPE_ALTER usingPta = new HisPatientTypeAlterGet().GetApplied(data.TreatmentId, data.InstructionTime, ptas);
            if (usingPta != null)
            {
                HIS_SERVICE_REQ serviceReq = new HIS_SERVICE_REQ();
                serviceReq.EXECUTE_DEPARTMENT_ID = mediStock.DEPARTMENT_ID;
                serviceReq.EXECUTE_ROOM_ID = mediStock.ROOM_ID;
                serviceReq.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONM;
                serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                serviceReq.PARENT_ID = data.ParentServiceReqId;
                serviceReq.INTRUCTION_TIME = data.InstructionTime;
                serviceReq.TREATMENT_ID = data.TreatmentId;
                serviceReq.ICD_TEXT = HisIcdUtil.RemoveDuplicateIcd(data.IcdText);
                serviceReq.ICD_NAME = data.IcdName;
                serviceReq.ICD_CODE = CommonUtil.ToUpper(data.IcdCode);
                serviceReq.ICD_CAUSE_NAME = data.IcdCauseName;
                serviceReq.ICD_CAUSE_CODE = CommonUtil.ToUpper(data.IcdCauseCode);
                serviceReq.ICD_SUB_CODE = CommonUtil.ToUpper(HisIcdUtil.RemoveDuplicateIcd(data.IcdSubCode));
                serviceReq.REQUEST_ROOM_ID = data.RequestRoomId;
                serviceReq.REQUEST_LOGINNAME = data.RequestLoginName;
                serviceReq.REQUEST_USERNAME = data.RequestUserName;
                serviceReq.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.RequestLoginName);
                serviceReq.DESCRIPTION = data.Description;
                serviceReq.TRACKING_ID = data.TrackingId;
                serviceReq.TREATMENT_TYPE_ID = usingPta.TREATMENT_TYPE_ID;
                serviceReq.SESSION_CODE = sessionCode;

                resultData = serviceReq;
            }
        }

        internal void Rollback()
        {
            this.hisServiceReqCreate.RollbackData();
        }
    }
}
