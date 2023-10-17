using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.SDO;
using MOS.Filter;
using MOS.MANAGER.Config;

namespace MOS.MANAGER.HisExecuteRoom
{
    partial class HisExecuteRoomGetSql : GetBase
    {
        internal HisExecuteRoomGetSql()
            : base()
        {

        }

        internal HisExecuteRoomGetSql(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HisExecuteRoomAppointedSDO> GetCountAppointed(HisExecuteRoomAppointedFilter filter)
        {
            List<HisExecuteRoomAppointedSDO> result = null;
            try
            {
                if (!filter.INTR_OR_APPOINT_DATE.HasValue)
                {
                    LogSystem.Warn("Khong co INTR_OR_APPOINT_DATE");
                    return result;
                }
                List<V_HIS_EXECUTE_ROOM> executeRooms = new List<V_HIS_EXECUTE_ROOM>();

                if (filter.EXECUTE_ROOM_IDs != null)
                {
                    executeRooms = HisExecuteRoomCFG.DATA.Where(o => filter.EXECUTE_ROOM_IDs.Contains(o.ID) && (o.MAX_APPOINTMENT_BY_DAY ?? 0) > 0).ToList();
                }
                else
                {
                    executeRooms = HisExecuteRoomCFG.DATA.Where(o => (o.MAX_APPOINTMENT_BY_DAY ?? 0) > 0).ToList();
                }

                result = new List<HisExecuteRoomAppointedSDO>();

                if (IsNotNullOrEmpty(executeRooms))
                {
                    string sql1 = "SELECT TREA.ID, TREA.APPOINTMENT_EXAM_ROOM_IDS FROM HIS_TREATMENT TREA WHERE TREA.IS_PAUSE = 1 AND TREA.APPOINTMENT_DATE IS NOT NULL AND TREA.APPOINTMENT_DATE = :param1 AND TREA.APPOINTMENT_EXAM_ROOM_IDS IS NOT NULL";
                    List<Treatment> treatments = DAOWorker.SqlDAO.GetSql<Treatment>(sql1, filter.INTR_OR_APPOINT_DATE.Value);
                    treatments = treatments != null ? treatments.Where(o => executeRooms.Any(a => this.VerifyExist(a.ROOM_ID, o.APPOINTMENT_EXAM_ROOM_IDS))).ToList() : null;
                    string temp = DAOWorker.SqlDAO.AddInClause(executeRooms.Select(s => s.ROOM_ID).ToList(), "SELECT ID, EXECUTE_ROOM_ID FROM HIS_SERVICE_REQ WHERE (IS_DELETE IS NULL OR IS_DELETE <> 1) AND (IS_NO_EXECUTE IS NULL OR IS_NO_EXECUTE <> 1) AND INTRUCTION_DATE =: param1 AND %IN_CLAUSE% ", "EXECUTE_ROOM_ID");
                    string sql2 = "";
                    if (IsNotNullOrEmpty(treatments))
                    {
                        sql2 = DAOWorker.SqlDAO.AddNotInClause(treatments.Select(s => s.ID).ToList(), String.Format("{0} AND %IN_CLAUSE% ", temp), "TREATMENT_ID");
                    }
                    else
                    {
                        sql2 = temp;
                    }
                    LogSystem.Info("Sql2: " + sql2);
                    List<ServiceReq> serviceReqs = DAOWorker.SqlDAO.GetSql<ServiceReq>(sql2, filter.INTR_OR_APPOINT_DATE.Value);

                    foreach (V_HIS_EXECUTE_ROOM room in executeRooms)
                    {
                        HisExecuteRoomAppointedSDO sdo = new HisExecuteRoomAppointedSDO();
                        sdo.ExecuteRoomId = room.ID;
                        sdo.MaxAmount = room.MAX_APPOINTMENT_BY_DAY;
                        sdo.ExecuteRoomCode = room.EXECUTE_ROOM_CODE;
                        sdo.ExecuteRoomName = room.EXECUTE_ROOM_NAME;

                        int count1 = treatments != null ? treatments.Count(c => this.VerifyExist(room.ROOM_ID, c.APPOINTMENT_EXAM_ROOM_IDS)) : 0;
                        int count2 = serviceReqs != null ? serviceReqs.Count(c => room.ROOM_ID == c.EXECUTE_ROOM_ID) : 0;
                        sdo.CurrentAmount = (count1 + count2);
                        result.Add(sdo);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        bool VerifyExist(long roomId, string roomIds)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(roomIds) && roomId > 0)
                {
                    return String.Format(",{0},", roomIds).Contains(String.Format(",{0},", roomId));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return false;
        }
    }

    class Treatment
    {
        public long ID { get; set; }
        public string APPOINTMENT_EXAM_ROOM_IDS { get; set; }
    }
    class ServiceReq
    {
        public long ID { get; set; }
        public long EXECUTE_ROOM_ID { get; set; }
    }
}
