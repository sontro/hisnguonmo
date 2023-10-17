using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBed;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisTreatmentBedRoom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBedLog
{
    partial class HisBedLogDelete : BusinessBase
    {
        internal HisBedLogDelete()
            : base()
        {

        }

        internal HisBedLogDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BED_LOG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_BED_LOG raw = null;
                HIS_TREATMENT_BED_ROOM treatmentBedRoom = null;

                HisBedLogCheck checker = new HisBedLogCheck(param);
                HisTreatmentBedRoomCheck treatmentBedRoomChecker = new HisTreatmentBedRoomCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && treatmentBedRoomChecker.VerifyId(raw.TREATMENT_BED_ROOM_ID, ref treatmentBedRoom);
                if (valid)
                {
                    result = this.ProcessServiceReq(raw, treatmentBedRoom)
                        && DAOWorker.HisBedLogDAO.Truncate(raw)
                        && this.ProcessTreatmentBedRoom(raw, treatmentBedRoom);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool ProcessTreatmentBedRoom(HIS_BED_LOG data, HIS_TREATMENT_BED_ROOM treatmentBedRoom)
        {
            if (treatmentBedRoom != null)
            {
                if (treatmentBedRoom.BED_ID == data.BED_ID)
                {
                    //Cap nhat lai bed
                    HisBedLogFilterQuery filter = new HisBedLogFilterQuery();
                    filter.TREATMENT_BED_ROOM_ID = data.TREATMENT_BED_ROOM_ID;
                    filter.ORDER_FIELD = "START_TIME";
                    filter.ORDER_DIRECTION = "DESC";
                    filter.ORDER_FIELD1 = "CREATE_TIME";
                    filter.ORDER_DIRECTION = "DESC";
                    List<HIS_BED_LOG> bedLogs = new HisBedLogGet().Get(filter);
                    if (bedLogs != null && bedLogs.Count > 0)
                    {
                        treatmentBedRoom.BED_ID = bedLogs[0].BED_ID;
                    }
                    else
                    {
                        treatmentBedRoom.BED_ID = null;
                    }

                    if (!new HisTreatmentBedRoomUpdate().Update(treatmentBedRoom))
                    {
                        LogSystem.Warn("Rollback du lieu");
                        return false;
                    }
                }
            }
            return true;
        }

        private bool ProcessServiceReq(HIS_BED_LOG bedLog, HIS_TREATMENT_BED_ROOM treatmentBedRoom)
        {
            string ssSql = "SELECT * FROM HIS_SERE_SERV S WHERE S.AMOUNT_TEMP IS NOT NULL AND S.IS_DELETE = 0 AND S.SERVICE_REQ_ID IS NOT NULL AND S.TDL_TREATMENT_ID = :param1 AND EXISTS (SELECT 1 FROM HIS_SERE_SERV_EXT EXT WHERE EXT.SERE_SERV_ID = S.ID AND EXT.BED_LOG_ID = :param2)";
            HIS_SERE_SERV ss = DAOWorker.SqlDAO.GetSqlSingle<HIS_SERE_SERV>(ssSql, treatmentBedRoom.TREATMENT_ID, bedLog.ID);

            if (ss != null)
            {
                string sqlExt = string.Format("UPDATE HIS_SERE_SERV_EXT EXT SET EXT.IS_DELETE = 1, EXT.TDL_SERVICE_REQ_ID = NULL, EXT.TDL_TREATMENT_ID = NULL, BED_LOG_ID = NULL WHERE SERE_SERV_ID = {0}", ss.ID);
                string sqlSs = string.Format("UPDATE HIS_SERE_SERV SS SET SS.IS_DELETE = 1, SS.SERVICE_REQ_ID = NULL, SS.TDL_TREATMENT_ID = NULL WHERE ID = {0}", ss.ID);
                string sqlSr = string.Format("UPDATE HIS_SERVICE_REQ SR SET SR.IS_DELETE = 1 WHERE ID = {0}", ss.SERVICE_REQ_ID);
                List<string> sqls = new List<string>(){sqlExt, sqlSs, sqlSr};

                if (!DAOWorker.SqlDAO.Execute(sqls))
                {
                    LogSystem.Warn("Xoa HIS_SERE_SERV_EXT, HIS_SERE_SERV, HIS_SERVICE_REQ that bai");
                    return false;
                }
            }
            return true;
        }

        internal bool DeleteList(List<HIS_BED_LOG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBedLogCheck checker = new HisBedLogCheck(param);
                List<HIS_BED_LOG> listRaw = new List<HIS_BED_LOG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBedLogDAO.DeleteList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
