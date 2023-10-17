using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestTemplate;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Kidney.KidneySchedule
{
    public class HisServiceReqKidneySchedulerCheck: BusinessBase
    {
        internal HisServiceReqKidneySchedulerCheck()
            : base()
        {
        }

        internal HisServiceReqKidneySchedulerCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsValidData(HisServiceReqKidneyScheduleSDO sdo)
        {
            try
            {
                HIS_MACHINE machine = HisMachineCFG.DATA.Where(o => o.ID == sdo.MachineId).FirstOrDefault();
                if (machine == null || machine.IS_KIDNEY != Constant.IS_TRUE)
                {
                    string tmp = machine != null ? machine.MACHINE_NAME : "";
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongPhaiLaMayChayThan, tmp);
                    return false;
                }

                V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == sdo.ServiceId).FirstOrDefault();
                if (service == null || service.IS_KIDNEY != Constant.IS_TRUE)
                {
                    string tmp = service != null ? service.SERVICE_NAME : "";
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongPhaiLaDichVuChayThan, tmp);
                    return false;
                }

                V_HIS_EXECUTE_ROOM executeRoom = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == sdo.RoomId).FirstOrDefault();
                if (executeRoom == null || executeRoom.IS_KIDNEY != Constant.IS_TRUE)
                {
                    string tmp = executeRoom != null ? executeRoom.EXECUTE_ROOM_NAME : "";
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongPhaiPhongChayThan, tmp);
                    return false;
                }

                if (!executeRoom.KIDNEY_SHIFT_COUNT.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhongChuaKhaiBaoSoCaChayThan, executeRoom.EXECUTE_ROOM_NAME);
                    return false;
                }

                if (executeRoom.KIDNEY_SHIFT_COUNT.Value < sdo.KidneyShift)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_VuotQuaSoCaToiDa, executeRoom.KIDNEY_SHIFT_COUNT.Value.ToString());
                    return false;
                }

                HIS_EXP_MEST_TEMPLATE template = new HisExpMestTemplateGet().GetById(sdo.ExpMestTemplateId);
                if (template == null || template.IS_KIDNEY != Constant.IS_TRUE)
                {
                    string tmp = template != null ? template.EXP_MEST_TEMPLATE_NAME : "";
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongPhaiGoiChayThan, tmp);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsNotExist(HisServiceReqKidneyScheduleSDO sdo)
        {
            try
            {
                string executeDate = sdo.ExecuteTime.ToString().Substring(0, 8);
                long instructionDate = long.Parse(string.Format("{0}000000", executeDate));

                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.INTRUCTION_DATE__EQUAL = instructionDate;
                filter.EXECUTE_ROOM_ID = sdo.RoomId;
                filter.KIDNEY_SHIFT = sdo.KidneyShift;
                filter.MACHINE_ID = sdo.MachineId;
                //filter.HAS_EXECUTE = true; (bo sung sau, khi co bo sung viec check o chuc nang chuyen trang thai 'is_no_execute' bang ke
                List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);
                if (IsNotNullOrEmpty(serviceReqs))
                {
                    V_HIS_EXECUTE_ROOM room = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == sdo.RoomId).FirstOrDefault();
                    HIS_MACHINE machine = HisMachineCFG.DATA.Where(o => o.ID == sdo.MachineId).FirstOrDefault();
                    string date = Inventec.Common.DateTime.Convert.TimeNumberToDateString(instructionDate);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DaTonTaiBenhNhanChayThan, room.EXECUTE_ROOM_NAME, date, sdo.KidneyShift.ToString(), machine.MACHINE_NAME);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
