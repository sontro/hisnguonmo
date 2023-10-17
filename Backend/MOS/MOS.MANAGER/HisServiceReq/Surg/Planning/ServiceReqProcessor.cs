using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Surg.Planning
{
    class ServiceReqProcessor : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;
        
        internal ServiceReqProcessor()
            : base()
        {
            this.Init();
        }

        internal ServiceReqProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(HIS_SERVICE_REQ serviceReq, long? ekipPlanId, HisServiceReqPlanSDO data)
        {
            bool result = false;
            try
            {
                V_HIS_ROOM newRoom = HisRoomCFG.DATA.Where(o => o.ID == data.ExecuteRoomId).FirstOrDefault();

                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);
                serviceReq.PLAN_TIME_FROM = data.PlanTimeFrom;
                serviceReq.PLAN_TIME_TO = data.PlanTimeTo;
                serviceReq.EKIP_PLAN_ID = ekipPlanId;
                serviceReq.EXECUTE_ROOM_ID = data.ExecuteRoomId;
                serviceReq.EXECUTE_DEPARTMENT_ID = newRoom.DEPARTMENT_ID;
                serviceReq.PLANNING_REQUEST = data.PlanningRequest;
                serviceReq.SURGERY_NOTE = data.SurgeryNote;

                if (!Inventec.Common.ObjectChecker.ValueChecker.IsPrimitiveDiff<HIS_SERVICE_REQ>(before, serviceReq)
                    || this.hisServiceReqUpdate.Update(serviceReq, before, false))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisServiceReqUpdate.RollbackData();
        }
    }
}
