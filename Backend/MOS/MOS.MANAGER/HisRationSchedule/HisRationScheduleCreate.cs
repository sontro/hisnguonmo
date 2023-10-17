using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisRationTime;
using MOS.MANAGER.HisReceptionRoom;
using MOS.MANAGER.HisRefectory;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSchedule
{
    partial class HisRationScheduleCreate : BusinessBase
    {
		private List<HIS_RATION_SCHEDULE> recentHisRationSchedules = new List<HIS_RATION_SCHEDULE>();
        private static string FORMAT_FIELD = "{0}: {1}";
        internal HisRationScheduleCreate()
            : base()
        {

        }

        internal HisRationScheduleCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(RationScheduleSDO sdo, ref HIS_RATION_SCHEDULE resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HisRationScheduleCheck checker = new HisRationScheduleCheck(param);
                valid = valid && checker.VerifyRequireField(sdo);
                valid = valid && checker.IsValidRationSchedule(sdo);
                valid = valid && this.HasWorkPlaceInfo(sdo.ReqRoomId, ref workPlace);
                if (valid)
                {
                    HIS_RATION_SCHEDULE data = new HIS_RATION_SCHEDULE();
                    data.TREATMENT_ID = sdo.TreatmentId;
                    data.RATION_TIME_ID = sdo.RationTimeId;
                    data.PATIENT_TYPE_ID = sdo.PatientTypeId;
                    data.SERVICE_ID = sdo.ServiceId;
                    data.AMOUNT = sdo.Amount;
                    data.REFECTORY_ROOM_ID = sdo.RefectoryRoomId;
                    data.FROM_TIME = sdo.FromTime;
                    data.TO_TIME = sdo.ToTime;
                    data.IS_FOR_HOMIE = sdo.IsForHomie ? (short?)Constant.IS_TRUE : null;
                    data.IS_HALF_IN_FIRST_DAY = sdo.HalfInFirstDay ? (short?)Constant.IS_TRUE : null;
                    data.NOTE = sdo.Note;
                    data.DEPARTMENT_ID = workPlace.DepartmentId;

					if (!DAOWorker.HisRationScheduleDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSchedule_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRationSchedule that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRationSchedules.Add(data);
                    resultData = data;

                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(sdo.TreatmentId);
                    string eventLog = "";
                    ProcessEventLog(sdo, ref eventLog);
                    new EventLogGenerator(EventLog.Enum.HisRationSchedule_TaoLichBaoAn, eventLog)
                        .TreatmentCode(treatment.TREATMENT_CODE)
                        .Run();

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
		
		internal bool CreateList(List<HIS_RATION_SCHEDULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRationScheduleCheck checker = new HisRationScheduleCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisRationScheduleDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSchedule_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRationSchedule that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisRationSchedules.AddRange(listData);
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

        private void ProcessEventLog(RationScheduleSDO sdo, ref string eventLog)
        {
            try
            {
                List<string> editFields = new List<string>();
                HIS_RATION_TIME rationTime = new HisRationTimeGet().GetById(sdo.RationTimeId);
                HIS_PATIENT_TYPE patientType = new HisPatientTypeGet().GetById(sdo.PatientTypeId);
                HIS_SERVICE servive = new HisServiceGet().GetById(sdo.ServiceId);
                HIS_REFECTORY refectory = new HisRefectoryGet().GetById(sdo.RefectoryRoomId);

                string fieldNameRationTime = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BuaAn);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameRationTime, rationTime != null ? rationTime.RATION_TIME_NAME : ""));

                string fieldNamePatientType = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DoiTuongThanhToan);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNamePatientType, patientType != null ? patientType.PATIENT_TYPE_NAME : ""));

                string fieldNameServive = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SuatAn);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameServive, servive != null ? servive.SERVICE_NAME : ""));

                string fieldNameSoluong = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuong1);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameSoluong, sdo.Amount));

                string fieldNameNhaAn = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhaAn);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameNhaAn, refectory != null ? refectory.REFECTORY_NAME : ""));

                string fieldNameFromTime = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGianBatDau);
                string fromTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(sdo.FromTime);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameFromTime, fromTime));

                string fieldNameToTime = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Treatment_ThoiGianKetThuc);
                string toTime = sdo.ToTime.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(sdo.ToTime.Value) : "";
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameToTime, toTime));

                string choNguoiNhaValue = sdo.IsForHomie ? "Đúng" : "Sai";
                string fieldNameIsForHomie = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChoNguoiNha);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameIsForHomie, choNguoiNhaValue));

                string AnTuChieuValue = sdo.HalfInFirstDay ? "Đúng" : "Sai";
                string fieldNameHalfInFirstDay = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.AnTuChieu);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameHalfInFirstDay, AnTuChieuValue));

                string fieldNameGhiChu = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GhiChu);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameGhiChu, sdo.Note != null ? sdo.Note : ""));

                eventLog = String.Join(". ", editFields);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisRationSchedules))
            {
                if (!DAOWorker.HisRationScheduleDAO.TruncateList(this.recentHisRationSchedules))
                {
                    LogSystem.Warn("Rollback du lieu HisRationSchedule that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRationSchedules", this.recentHisRationSchedules));
                }
				this.recentHisRationSchedules = null;
            }
        }
    }
}
