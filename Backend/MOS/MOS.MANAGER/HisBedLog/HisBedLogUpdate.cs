using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatmentBedRoom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBedLog
{
	partial class HisBedLogUpdate : BusinessBase
	{
		private List<HIS_BED_LOG> beforeUpdateHisBedLogs = new List<HIS_BED_LOG>();
		
		internal HisBedLogUpdate()
			: base()
		{

		}

		internal HisBedLogUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_BED_LOG data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisBedLogCheck checker = new HisBedLogCheck(param);
				HisTreatmentBedRoomCheck treatmentBedRoomChecker = new HisTreatmentBedRoomCheck(param);
				HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

				valid = valid && checker.VerifyRequireField(data);
				HIS_BED_LOG raw = null;
				HIS_TREATMENT_BED_ROOM treatmentBedRoom = null;
				HIS_TREATMENT treatment = null;

				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				valid = valid && treatmentBedRoomChecker.VerifyId(data.TREATMENT_BED_ROOM_ID, ref treatmentBedRoom);
				valid = valid && treatmentChecker.VerifyId(treatmentBedRoom.TREATMENT_ID, ref treatment);
				valid = valid && checker.IsValidTime(data, treatmentBedRoom, treatment);
				if (valid)
				{
					if (!DAOWorker.HisBedLogDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedLog_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisBedLog that bai." + LogUtil.TraceData("data", data));
					}

					this.beforeUpdateHisBedLogs.Add(raw);

					this.ProcessTreatmentBedRoom(data, treatmentBedRoom);

					this.ProcessServiceReq(data, raw, treatment);

					result = true;
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

		private void ProcessTreatmentBedRoom(HIS_BED_LOG data, HIS_TREATMENT_BED_ROOM treatmentBedRoom)
		{
			if (treatmentBedRoom != null)
			{
				HisBedLogFilterQuery filter = new HisBedLogFilterQuery();
				filter.TREATMENT_BED_ROOM_ID = data.TREATMENT_BED_ROOM_ID;
				filter.ORDER_FIELD = "START_TIME";
				filter.ORDER_DIRECTION = "DESC";
				filter.ORDER_FIELD1 = "CREATE_TIME";
				filter.ORDER_DIRECTION = "DESC";
				List<HIS_BED_LOG> bedLogs = new HisBedLogGet().Get(filter);
				if (bedLogs != null && bedLogs.Count > 0)
				{
					if (treatmentBedRoom.BED_ID != bedLogs[0].BED_ID)
					{
						treatmentBedRoom.BED_ID = bedLogs[0].BED_ID;
						if (!new HisTreatmentBedRoomUpdate().Update(treatmentBedRoom))
						{
							throw new Exception("Rollback du lieu");
						}
					}
				}
			}
		}

        private bool ProcessServiceReq(HIS_BED_LOG bedLog, HIS_BED_LOG beforeUpdate, HIS_TREATMENT treatment)
		{
			//Neu thuc hien bo sung thoi gian ket thuc cho thong tin giuong thi thuc hien xoa thong tin
			//chi phi giuong tam tinh
            if (bedLog != null && beforeUpdate != null && bedLog.FINISH_TIME.HasValue && !beforeUpdate.FINISH_TIME.HasValue)
            {
                string ssSql = "SELECT * FROM HIS_SERE_SERV S WHERE S.AMOUNT_TEMP IS NOT NULL AND S.IS_DELETE = 0 AND S.SERVICE_REQ_ID IS NOT NULL AND S.TDL_TREATMENT_ID = :param1 AND EXISTS (SELECT 1 FROM HIS_SERE_SERV_EXT EXT WHERE EXT.SERE_SERV_ID = S.ID AND EXT.BED_LOG_ID = :param2)";
                HIS_SERE_SERV ss = DAOWorker.SqlDAO.GetSqlSingle<HIS_SERE_SERV>(ssSql, bedLog.ID, treatment.ID);

                if (ss != null)
                {
                    string sqlExt = string.Format("UPDATE HIS_SERE_SERV_EXT EXT SET EXT.IS_DELETE = 1, EXT.TDL_SERVICE_REQ_ID = NULL, EXT.TDL_TREATMENT_ID = NULL, BED_LOG_ID = NULL WHERE SERE_SERV_ID = {0}", ss.ID);
                    string sqlSs = string.Format("UPDATE HIS_SERE_SERV SS SET SS.IS_DELETE = 1, SS.SERVICE_REQ_ID = NULL, SS.TDL_TREATMENT_ID = NULL WHERE ID = {0}", ss.ID);
                    string sqlSr = string.Format("UPDATE HIS_SERVICE_REQ SR SET SR.IS_DELETE = 1 WHERE ID = {0}", ss.SERVICE_REQ_ID);
                    List<string> sqls = new List<string>() { sqlExt, sqlSs, sqlSr };

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        LogSystem.Warn("Xoa HIS_SERE_SERV_EXT, HIS_SERE_SERV, HIS_SERVICE_REQ that bai");
                        return false;
                    }
                }
            }
            //Neu cap nhat bo thoi gian ket thuc thi tu dong tao "chi phi tam tinh"
            //(se bo sung sau)
            else if (bedLog != null && beforeUpdate != null && !bedLog.FINISH_TIME.HasValue && beforeUpdate.FINISH_TIME.HasValue)
            {
                if (HisSereServCFG.IS_USING_BED_TEMP && !bedLog.FINISH_TIME.HasValue && bedLog.PATIENT_TYPE_ID.HasValue && bedLog.BED_SERVICE_TYPE_ID.HasValue)
                {
                    //List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
                    //List<HIS_SERE_SERV> existSereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);

                    //this.hisServiceReqCreateTempByBedLogProcessor.Run(bedLog, treatment, requestRoomId, ptas, existSereServs);
                }
            }
			
			return true;
		}

		internal bool UpdateList(List<HIS_BED_LOG> listData, List<HIS_BED_LOG> beforeUpdates)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisBedLogCheck checker = new HisBedLogCheck(param);
				valid = valid && checker.IsUnLock(beforeUpdates);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
				}
				if (valid)
				{
					if (!DAOWorker.HisBedLogDAO.UpdateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedLog_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisBedLog that bai." + LogUtil.TraceData("listData", listData));
					}
					this.beforeUpdateHisBedLogs.AddRange(beforeUpdates);
					result = true;
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

		internal bool ResetServiceReqId(long serviceReqId)
		{
			bool result = false;
			try
			{
				List<HIS_BED_LOG> hisBedLogs = new HisBedLogGet().GetByServiceReqId(serviceReqId);
				if (IsNotNullOrEmpty(hisBedLogs))
				{
					if (new HisBedLogCheck().IsUnLock(hisBedLogs))
					{
						Mapper.CreateMap<HIS_BED_LOG, HIS_BED_LOG>();
						List<HIS_BED_LOG> beforeUpdates = Mapper.Map<List<HIS_BED_LOG>>(hisBedLogs);
						this.beforeUpdateHisBedLogs.AddRange(beforeUpdates);

						hisBedLogs.ForEach(o => o.SERVICE_REQ_ID = null);

						if (!DAOWorker.HisBedLogDAO.UpdateList(hisBedLogs))
						{
							MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedLog_CapNhatThatBai);
							throw new Exception("Cap nhat thong tin HisBedLog that bai." + LogUtil.TraceData("hisBedLogs", hisBedLogs));
						}
						result = true;
					}
				}
				else
				{
					result = true;
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

		internal void RollbackData()
		{
			if (IsNotNullOrEmpty(this.beforeUpdateHisBedLogs))
			{
				if (!DAOWorker.HisBedLogDAO.UpdateList(this.beforeUpdateHisBedLogs))
				{
					LogSystem.Warn("Rollback du lieu HisBedLog that bai, can kiem tra lai." + LogUtil.TraceData("HisBedLogs", this.beforeUpdateHisBedLogs));
				}
				this.beforeUpdateHisBedLogs = null; //tranh goi rollback 2 lan
			}
		}
	}
}
