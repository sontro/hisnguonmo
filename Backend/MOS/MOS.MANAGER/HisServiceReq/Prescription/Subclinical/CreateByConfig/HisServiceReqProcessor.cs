using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.CreateByConfig
{
    class HisServiceReqProcessor : BusinessBase
    {
        private HisServiceReqCreate hisServiceReqCreate;

        internal HisServiceReqProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
        }

        internal bool Run(V_HIS_MEDI_STOCK mediStock, HIS_TREATMENT treatment, long parentServiceReqId, long requestRoomId, long instructionTime, ref HIS_SERVICE_REQ resultData)
        {
            try
            {
                HIS_SERVICE_REQ serviceReq = this.MakeData(mediStock, treatment, parentServiceReqId, requestRoomId, instructionTime);
                if (!this.hisServiceReqCreate.Create(serviceReq, treatment))
                {
                    return false;
                }
                resultData = serviceReq;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return true;
        }

        //Tao service_req dua vao thong tin chi dinh thuoc trong kho
        private HIS_SERVICE_REQ MakeData(V_HIS_MEDI_STOCK mediStock, HIS_TREATMENT treatment, long parentServiceReqId, long requestRoomId, long instructionTime)
        {
            HIS_SERVICE_REQ serviceReq = new HIS_SERVICE_REQ();
            serviceReq.EXECUTE_DEPARTMENT_ID = mediStock.DEPARTMENT_ID;
            serviceReq.EXECUTE_ROOM_ID = mediStock.ROOM_ID;
            serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
            serviceReq.PARENT_ID = parentServiceReqId;
            serviceReq.INTRUCTION_TIME = instructionTime;
            serviceReq.TREATMENT_ID = treatment.ID;
            serviceReq.ICD_TEXT = treatment.ICD_TEXT;
            serviceReq.ICD_NAME = treatment.ICD_NAME;
            serviceReq.ICD_CODE = treatment.ICD_CODE;
            serviceReq.ICD_CAUSE_NAME = treatment.ICD_CAUSE_NAME;
            serviceReq.ICD_CAUSE_CODE = treatment.ICD_CAUSE_CODE;
            serviceReq.ICD_SUB_CODE = treatment.ICD_SUB_CODE;
            serviceReq.REQUEST_ROOM_ID = requestRoomId;
            serviceReq.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            serviceReq.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            serviceReq.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(serviceReq.REQUEST_LOGINNAME);
            serviceReq.TREATMENT_TYPE_ID = treatment.TDL_TREATMENT_TYPE_ID;
            serviceReq.PRESCRIPTION_TYPE_ID = (short)PrescriptionType.SUBCLINICAL;
            serviceReq.SESSION_CODE = Guid.NewGuid().ToString();

            if (mediStock.IS_CABINET == MOS.UTILITY.Constant.IS_TRUE)
            {
                serviceReq.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT;
            }
            else
            {
                if (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU ||
                    treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_BenhNhanDaVaoDieuTri);
                    throw new Exception("Benh nhan da va dieu tri khong cho phep tao don phong kham");
                }
                serviceReq.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK;
            }
            return serviceReq;
        }

        internal void Rollback()
        {
            this.hisServiceReqCreate.RollbackData();
        }
    }
}
