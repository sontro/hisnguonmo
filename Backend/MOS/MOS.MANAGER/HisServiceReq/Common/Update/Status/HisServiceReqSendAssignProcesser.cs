using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using MOS.MANAGER.HisServiceReq.Pacs;
using Inventec.Common.Logging;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using Inventec.Core;

namespace MOS.MANAGER.HisServiceReq.Common.Update.Status
{
    class HisServiceReqSendAssignProcesser : BusinessBase
    {
        public void Run(HIS_SERVICE_REQ serviceReq)
        {
            bool result = false;
            try
            {
                if (PacsCFG.SEND_WHEN_CHANGE_STATUS
                        && (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL || serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                        && (HisServiceReqTypeCFG.PACS_TYPE_IDs.Contains(serviceReq.SERVICE_REQ_TYPE_ID) || serviceReq.ALLOW_SEND_PACS == Constant.IS_TRUE))
                {
                    List<HIS_SERE_SERV> ss = new HisSereServGet().GetByServiceReqId(serviceReq.ID);
                    List<string> sqls = new List<string>();

                    PacsOrderData data = new PacsOrderData();
                    data.ServiceReq = serviceReq;
                    data.Availables = ss;
                    data.Treatment = serviceReq != null ? new HisTreatmentGet().GetById(serviceReq.TREATMENT_ID) : null;

                    if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL)
                    {
                        data.Inserts = ss;//coi nhu la gui lan dau
                    }
                    else if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                    {
                        data.Deletes = ss;
                    }

                    IPacsProcessor processor = PacsFactory.GetProcessor(param);
                    result = processor.SendOrder(data, ref sqls);
                    if (result)
                    {
                        processor.UpdateStatus(new List<PacsOrderData>() { data }, sqls);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
        }
    }
}
