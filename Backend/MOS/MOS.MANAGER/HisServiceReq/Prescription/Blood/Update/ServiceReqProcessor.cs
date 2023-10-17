using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisIcd;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Blood.Update
{
    class ServiceReqProcessor : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal ServiceReqProcessor(CommonParam param)
            : base(param)
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(PatientBloodPresSDO data, V_HIS_MEDI_STOCK mediStock, HIS_SERVICE_REQ serviceReq)
        {
            bool result = false;
            try
            {
                if (serviceReq != null)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);
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
                    serviceReq.ICD_SUB_CODE = CommonUtil.ToUpper(HisIcdUtil.RemoveDuplicateIcd(data.IcdSubCode));
                    serviceReq.ICD_CAUSE_NAME = data.IcdCauseName;
                    serviceReq.ICD_CAUSE_CODE = CommonUtil.ToUpper(data.IcdCauseCode);
                    serviceReq.REQUEST_ROOM_ID = data.RequestRoomId;
                    serviceReq.REQUEST_LOGINNAME = data.RequestLoginName;
                    serviceReq.REQUEST_USERNAME = data.RequestUserName;
                    serviceReq.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.RequestLoginName);
                    serviceReq.DESCRIPTION = data.Description;
                    serviceReq.TRACKING_ID = data.TrackingId;

                    result = this.hisServiceReqUpdate.Update(serviceReq, before, false);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisServiceReqUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
