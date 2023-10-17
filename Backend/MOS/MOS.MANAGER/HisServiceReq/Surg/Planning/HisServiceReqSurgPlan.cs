using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEkip;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisPtttCalendar;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Surg.Planning
{
    /// <summary>
    /// Lap ke hoach cho phau thuat
    /// </summary>
    class HisServiceReqSurgPlan : BusinessBase
    {
        private PlanEkipProcessor planEkipProcessor;
        private ServiceReqProcessor serviceReqProcessor;
        private SereServPtttProcessor sereServPtttProcessor;

        internal HisServiceReqSurgPlan()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqSurgPlan(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.planEkipProcessor = new PlanEkipProcessor(param);
            this.serviceReqProcessor = new ServiceReqProcessor(param);
            this.sereServPtttProcessor = new SereServPtttProcessor(param);
        }

        internal bool Run(HisServiceReqPlanSDO data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqCheck serviceReqCheck = new HisServiceReqCheck(param);
                HisServiceReqSurgPlanCheck planChecker = new HisServiceReqSurgPlanCheck(param);
                HisPtttCalendarCheck calendarChecker = new HisPtttCalendarCheck(param);
                HisServiceReqCheck checker = new HisServiceReqCheck(param);

                HIS_SERVICE_REQ serviceReq = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && serviceReqCheck.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && this.IsWorkingAtDepartment(serviceReq.EXECUTE_DEPARTMENT_ID, workPlace.DepartmentId);
                valid = valid && planChecker.IsValidTime(data);
                valid = valid && planChecker.IsValidType(serviceReq);
                valid = valid && planChecker.IsNotApproved(serviceReq);
                valid = valid && planChecker.IsNotApprovedCalendar(serviceReq);
                valid = valid && planChecker.IsNotDuplicateEkipUser(data.PlanEkip);
                valid = valid && (data.ExecuteRoomId == serviceReq.EXECUTE_ROOM_ID || checker.IsProcessable(data.ExecuteRoomId, new HisSereServGet().GetByServiceReqId(data.ServiceReqId)));
                if (valid)
                {
                    long ekipPlanId = 0;
                    if (!this.planEkipProcessor.Run(serviceReq.EKIP_PLAN_ID, data.PlanEkip, ref ekipPlanId))
                    {
                        throw new Exception("planEkipProcessor. Rollback du lieu");
                    }

                    if (!this.serviceReqProcessor.Run(serviceReq, ekipPlanId, data))
                    {
                        throw new Exception("serviceReqProcessor. Rollback du lieu");
                    }

                    if (!this.sereServPtttProcessor.Run(serviceReq, data))
                    {
                        throw new Exception("sereServPtttProcessor. Rollback du lieu");
                    }
                    result = true;
                    resultData = serviceReq;
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

        private void RollbackData()
        {
            this.sereServPtttProcessor.Rollback();
            this.serviceReqProcessor.Rollback();
            this.planEkipProcessor.Rollback();
        }
    }
}
