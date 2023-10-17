using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisRationTime;
using MOS.MANAGER.HisReceptionRoom;
using MOS.MANAGER.HisRefectory;
using MOS.MANAGER.HisRestRetrType;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRationSchedule
{
    partial class HisRationScheduleTruncate : BusinessBase
    {
        private static string FORMAT_FIELD = "{0}: {1}";
        internal HisRationScheduleTruncate()
            : base()
        {

        }

        internal HisRationScheduleTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationScheduleCheck checker = new HisRationScheduleCheck(param);
                HIS_RATION_SCHEDULE raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                valid = valid && this.IsValidRationSchedule(id);
                if (valid)
                {
                    result = DAOWorker.HisRationScheduleDAO.Truncate(raw);
                    
                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(raw.TREATMENT_ID.Value);
                    string eventLog = "";
                    ProcessEventLog(raw, ref eventLog);
                    new EventLogGenerator(EventLog.Enum.HisRationSchedule_XoaLichBaoAn, eventLog)
                        .TreatmentCode(treatment.TREATMENT_CODE)
                        .Run();
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

        internal bool TruncateList(List<HIS_RATION_SCHEDULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRationScheduleCheck checker = new HisRationScheduleCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisRationScheduleDAO.TruncateList(listData);
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


        internal bool IsValidRationSchedule(long RationScheduleId)
        {
            bool valid = true;
            try
            {
                HIS_RATION_SCHEDULE RationChedule = new HisRationScheduleGet().GetById(RationScheduleId);
                if (RationChedule == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    return false;
                }

                if (RationChedule != null && RationChedule.LAST_ASSIGN_DATE != null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRationSchedule_BaoAnDaDuocThucHienKhongChoPhepXoa);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private void ProcessEventLog(HIS_RATION_SCHEDULE data, ref string eventLog)
        {
            try
            {
                List<string> editFields = new List<string>();
                HIS_RATION_TIME rationTime = new HisRationTimeGet().GetById(data.RATION_TIME_ID ?? 0);
                HIS_PATIENT_TYPE patientType = new HisPatientTypeGet().GetById(data.PATIENT_TYPE_ID ?? 0);
                HIS_SERVICE servive = new HisServiceGet().GetById(data.SERVICE_ID ?? 0);
                HIS_REFECTORY refectory = new HisRefectoryGet().GetById(data.REFECTORY_ROOM_ID ?? 0);

                string fieldNameRationTime = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BuaAn);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameRationTime, rationTime != null ? rationTime.RATION_TIME_NAME : ""));

                string fieldNamePatientType = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DoiTuongThanhToan);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNamePatientType, patientType != null ? patientType.PATIENT_TYPE_NAME : ""));

                string fieldNameServive = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SuatAn);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameServive, servive != null ? servive.SERVICE_NAME : ""));

                string fieldNameSoluong = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuong1);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameSoluong, data.AMOUNT));

                string fieldNameNhaAn = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhaAn);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameNhaAn, refectory != null ? refectory.REFECTORY_NAME : ""));
                
                string fieldNameFromTime = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGianBatDau);
                string fromTime = data.FROM_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.FROM_TIME.Value) : "";
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameFromTime, fromTime));
                
                string fieldNameToTime = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Treatment_ThoiGianKetThuc);
                string toTime = data.TO_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.TO_TIME.Value) : "";
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameToTime, toTime));

                string choNguoiNhaValue = (data.IS_FOR_HOMIE == Constant.IS_TRUE) ? "Đúng" : "Sai";
                string fieldNameIsForHomie = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChoNguoiNha);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameIsForHomie, choNguoiNhaValue));

                string AnTuChieuValue = (data.IS_HALF_IN_FIRST_DAY == Constant.IS_TRUE) ? "Đúng" : "Sai";
                string fieldNameHalfInFirstDay = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.AnTuChieu);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameHalfInFirstDay, AnTuChieuValue));

                string fieldNameGhiChu = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GhiChu);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameGhiChu, data.NOTE != null ? data.NOTE : ""));

                eventLog = String.Join(". ", editFields);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
