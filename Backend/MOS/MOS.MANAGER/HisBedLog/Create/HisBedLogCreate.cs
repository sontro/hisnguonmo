using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBed;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Bed.CreateTempByBedLog;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedLog
{
	partial class HisBedLogCreate : BusinessBase
	{
		private List<HIS_BED_LOG> recentHisBedLogs = new List<HIS_BED_LOG>();
		private HisServiceReqCreateTempByBedLogProcessor hisServiceReqCreateTempByBedLogProcessor;

		internal HisBedLogCreate()
			: base()
		{
			this.hisServiceReqCreateTempByBedLogProcessor = new HisServiceReqCreateTempByBedLogProcessor(param);
		}

		internal HisBedLogCreate(CommonParam paramCreate)
			: base(paramCreate)
		{
			this.hisServiceReqCreateTempByBedLogProcessor = new HisServiceReqCreateTempByBedLogProcessor(param);
		}

		internal bool Create(HisBedLogSDO sdo, ref HIS_BED_LOG resultData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisTreatmentBedRoomCheck treatmentBedRoomChecker = new HisTreatmentBedRoomCheck(param);
				HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

				HIS_TREATMENT_BED_ROOM treatmentBedRoom = null;
				HIS_TREATMENT treatment = null;
				valid = valid && treatmentBedRoomChecker.VerifyId(sdo.TreatmentBedRoomId, ref treatmentBedRoom);
				valid = valid && treatmentChecker.VerifyId(treatmentBedRoom.TREATMENT_ID, ref treatment);
				if (valid)
				{
					HIS_BED_LOG data = new HIS_BED_LOG();
					data.TREATMENT_BED_ROOM_ID = sdo.TreatmentBedRoomId;
					data.PATIENT_TYPE_ID = sdo.PatientTypeId;
					data.PRIMARY_PATIENT_TYPE_ID = sdo.PrimaryPatientTypeId;
					data.SHARE_COUNT = sdo.ShareCount;
					data.START_TIME = sdo.StartTime;
					data.FINISH_TIME = sdo.FinishTime;
					data.BED_SERVICE_TYPE_ID = sdo.BedServiceTypeId;
					data.BED_ID = sdo.BedId;
					if (this.Create(data, treatment, sdo.WorkingRoomId))
					{
						resultData = data;
					}
					return true;
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

		internal bool Create(HIS_BED_LOG data, HIS_TREATMENT treatment)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisBedLogCheck checker = new HisBedLogCheck(param);
				HisTreatmentBedRoomCheck treatmentBedRoomChecker = new HisTreatmentBedRoomCheck(param);
				HIS_TREATMENT_BED_ROOM treatmentBedRoom = null;
				valid = valid && checker.VerifyRequireField(data);
				valid = valid && treatmentBedRoomChecker.VerifyId(data.TREATMENT_BED_ROOM_ID, ref treatmentBedRoom);
				valid = valid && checker.IsValidTime(data, treatmentBedRoom, treatment);
				if (valid)
				{
					if (!DAOWorker.HisBedLogDAO.Create(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedLog_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisBedLog that bai." + LogUtil.TraceData("data", data));
					}
					this.recentHisBedLogs.Add(data);

					this.ProcessTreatmentBedRoom(data, treatmentBedRoom);
					result = true;
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

        internal bool CreateWithoutChecking(List<HIS_BED_LOG> data)
        {
            bool result = false;
            try
            {
                if (!DAOWorker.HisBedLogDAO.CreateList(data))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedLog_ThemMoiThatBai);
                    throw new Exception("Them moi thong tin HisBedLog that bai." + LogUtil.TraceData("data", data));
                }
                this.recentHisBedLogs.AddRange(data);

                result = true;
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

		internal bool Create(HIS_BED_LOG data, HIS_TREATMENT treatment, long workingRoomId)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisBedLogCheck checker = new HisBedLogCheck(param);
				HisTreatmentBedRoomCheck treatmentBedRoomChecker = new HisTreatmentBedRoomCheck(param);
				HIS_TREATMENT_BED_ROOM treatmentBedRoom = null;
				valid = valid && checker.VerifyRequireField(data);
				valid = valid && treatmentBedRoomChecker.VerifyId(data.TREATMENT_BED_ROOM_ID, ref treatmentBedRoom);
				valid = valid && checker.IsValidTime(data, treatmentBedRoom, treatment);
				if (valid)
				{
					if (!DAOWorker.HisBedLogDAO.Create(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedLog_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisBedLog that bai." + LogUtil.TraceData("data", data));
					}
					this.recentHisBedLogs.Add(data);

					this.ProcessTreatmentBedRoom(data, treatmentBedRoom);
					this.ProcessServiceReq(data, treatment, workingRoomId);

					result = true;
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

		private void ProcessTreatmentBedRoom(HIS_BED_LOG data, HIS_TREATMENT_BED_ROOM treatmentBedRoom)
		{
			if (treatmentBedRoom != null)
			{
				if (treatmentBedRoom.BED_ID != data.BED_ID)
				{
					treatmentBedRoom.BED_ID = data.BED_ID;
					if (!new HisTreatmentBedRoomUpdate().Update(treatmentBedRoom))
					{
						throw new Exception("Rollback du lieu");
					}
				}
			}
		}

		private void ProcessServiceReq(HIS_BED_LOG bedLog, HIS_TREATMENT treatment, long requestRoomId)
		{
			if (HisSereServCFG.IS_USING_BED_TEMP && !bedLog.FINISH_TIME.HasValue && bedLog.PATIENT_TYPE_ID.HasValue && bedLog.BED_SERVICE_TYPE_ID.HasValue)
			{
				List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
				List<HIS_SERE_SERV> existSereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);

				this.hisServiceReqCreateTempByBedLogProcessor.Run(bedLog, treatment, requestRoomId, ptas, existSereServs);
			}
		}
		
		internal void RollbackData()
		{
			try
			{
				this.hisServiceReqCreateTempByBedLogProcessor.Rollback();
				if (IsNotNullOrEmpty(this.recentHisBedLogs) && !DAOWorker.HisBedLogDAO.TruncateList(this.recentHisBedLogs))
				{
					LogSystem.Warn("Rollback du lieu HisBedLog that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBedLogs", this.recentHisBedLogs));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}
	}
}
