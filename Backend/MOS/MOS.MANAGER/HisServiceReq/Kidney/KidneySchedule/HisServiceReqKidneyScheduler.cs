using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Kidney.KidneySchedule
{
    public class HisServiceReqKidneyScheduler: BusinessBase
    {
        private HisServiceReqAssignServiceCreate assignServiceCreate;

        internal HisServiceReqKidneyScheduler()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqKidneyScheduler(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.assignServiceCreate = new HisServiceReqAssignServiceCreate(param);
        }

        public bool Run(HisServiceReqKidneyScheduleSDO sdo, ref HisServiceReqListResultSDO resultData)
        {
            try
            {
                bool valid = true;
                HisServiceReqKidneySchedulerCheck checker = new HisServiceReqKidneySchedulerCheck(param);
                valid = valid && checker.IsValidData(sdo);
                valid = valid && checker.IsNotExist(sdo);
                if (valid)
                {
                    AssignServiceSDO data = new AssignServiceSDO();
                    data.Description = sdo.Note;
                    data.InstructionTimes = new List<long>() {sdo.ExecuteTime};
                    data.RequestLoginName = sdo.RequestLoginname;
                    data.RequestUserName = sdo.RequestUsername;
                    data.KidneyShift = sdo.KidneyShift;
                    data.MachineId = sdo.MachineId;
                    data.ExpMestTemplateId = sdo.ExpMestTemplateId;
                    data.IsKidney = true;

                    ServiceReqDetailSDO detail = new ServiceReqDetailSDO();
                    detail.Amount = 1;
                    detail.PatientTypeId = sdo.PatientTypeId;
                    detail.RoomId = sdo.RoomId;
                    detail.ServiceId = sdo.ServiceId;

                    data.ServiceReqDetails = new List<ServiceReqDetailSDO>() { detail };
                    data.RequestRoomId = sdo.WorkingRoomId;
                    data.TreatmentId = sdo.TreatmentId;

                    return this.assignServiceCreate.Create(data, false, true, ref resultData);
                }

                return false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
