using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Exam
{
    class HisServiceReqExamCheck : BusinessBase
    {
        internal HisServiceReqExamCheck()
            : base()
        {

        }

        internal HisServiceReqExamCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsNotExceedLimit(List<RoomAssignData> data, long treatmentId, long treatmentTypeId, long instructionTime)
        {
            List<long> examRoomIds = IsNotNullOrEmpty(data) ? data
                .Where(o => o.ServiceReqTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                .Select(o => o.RoomId).ToList() : null;
            return this.IsNotExceedLimit(examRoomIds, treatmentId, treatmentTypeId, instructionTime);
        }

        internal bool IsNotExceedLimit(List<HIS_SERVICE_REQ> data, long treatmentId, long treatmentTypeId, long instructionTime)
        {
            List<long> examRoomIds = IsNotNullOrEmpty(data) ? data
                .Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                .Select(o => o.EXECUTE_ROOM_ID).ToList() : null;
            return this.IsNotExceedLimit(examRoomIds, treatmentId, treatmentTypeId, instructionTime);
        }

        internal bool IsNotExceedLimit(long roomId, long treatmentId, long treatmentTypeId, long instructionTime)
        {
            return this.IsNotExceedLimit(new List<long>() { roomId }, treatmentId, treatmentTypeId, instructionTime);
        }

        internal bool IsNotExceedLimit(List<long> examRoomIds, long treatmentId, long treatmentTypeId, long instructionTime)
        {
            try
            {
                //Chi kiem tra neu co cau hinh kiem tra gioi han va doi tuong BN la kham
                if (HisServiceReqCFG.CHECK_EXAM_ROOM_LIMIT
                    && treatmentTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM
                    && IsNotNullOrEmpty(examRoomIds))
                {
                    foreach (long roomId in examRoomIds)
                    {
                        V_HIS_EXECUTE_ROOM room = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == roomId).FirstOrDefault();

                        if (room != null && room.MAX_REQUEST_BY_DAY.HasValue)
                        {
                            string sql = "SELECT COUNT(1) FROM "
                                + "(SELECT DISTINCT TREATMENT_ID "
                                + " FROM HIS_SERVICE_REQ REQ "
                                + " WHERE EXECUTE_ROOM_ID = :param1 "
                                + " AND INTRUCTION_DATE = :param2 "
                                + " AND TREATMENT_TYPE_ID = :param4 "
                                + " AND TREATMENT_ID <> :param5)";
                            long? startTime = Inventec.Common.DateTime.Get.StartDay(instructionTime);
                            long number = DAOWorker.SqlDAO.GetSqlSingle<long>(sql, roomId, startTime.Value, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM, treatmentId);
                        
                            if (room.MAX_REQUEST_BY_DAY.Value <= number)
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhongKhamVuotQuaSoLuongKhamTrongNgay, room.EXECUTE_ROOM_NAME, room.MAX_REQUEST_BY_DAY.Value.ToString());
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsAllowedUser()
        {
            bool valid = true;
            try
            {
                if (HisServiceReqCFG.EXAM_USER_MUST_HAS_DIPLOMA)
                {
                    HIS_EMPLOYEE employee = HisEmployeeUtil.GetEmployee();
                    if (employee == null || string.IsNullOrWhiteSpace(employee.DIPLOMA))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TaiKhoanKhongCoThongTinChungChiHanhNghe);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
