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
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRationSchedule
{
    partial class HisRationScheduleUpdate : BusinessBase
    {
		private List<HIS_RATION_SCHEDULE> beforeUpdateHisRationSchedules = new List<HIS_RATION_SCHEDULE>();
        private static string FORMAT_FIELD = "{0}: {1}";

        internal HisRationScheduleUpdate()
            : base()
        {

        }

        internal HisRationScheduleUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(RationScheduleSDO sdo, ref HIS_RATION_SCHEDULE resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationScheduleCheck checker = new HisRationScheduleCheck(param);
                valid = valid && checker.VerifyRequireField(sdo);
                HIS_RATION_SCHEDULE raw = null;
                valid = valid && this.IsValidRationSchedule(sdo, ref raw);
                valid = valid && checker.IsUnLock(raw);
                
                if (valid)
                {
                    raw.RATION_TIME_ID = sdo.RationTimeId;
                    raw.PATIENT_TYPE_ID = sdo.PatientTypeId;
                    raw.SERVICE_ID = sdo.ServiceId;
                    raw.AMOUNT = sdo.Amount;
                    raw.REFECTORY_ROOM_ID = sdo.RefectoryRoomId;
                    raw.FROM_TIME = sdo.FromTime;
                    raw.TO_TIME = sdo.ToTime;
                    raw.IS_FOR_HOMIE = sdo.IsForHomie ? (short?)Constant.IS_TRUE : null;
                    raw.IS_HALF_IN_FIRST_DAY = sdo.HalfInFirstDay ? (short?)Constant.IS_TRUE : null;
                    raw.NOTE = sdo.Note;

                    if (!DAOWorker.HisRationScheduleDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSchedule_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationSchedule that bai." + LogUtil.TraceData("raw", raw));
                    }
                    resultData = raw;

                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(sdo.TreatmentId);
                    string eventLog = "";
                    ProcessEventLog(sdo, ref eventLog);
                    new EventLogGenerator(EventLog.Enum.HisRationSchedule_SuaLichBaoAn, eventLog)
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

        internal bool UpdateList(List<HIS_RATION_SCHEDULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRationScheduleCheck checker = new HisRationScheduleCheck(param);
                List<HIS_RATION_SCHEDULE> listRaw = new List<HIS_RATION_SCHEDULE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisRationScheduleDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSchedule_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationSchedule that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisRationSchedules.AddRange(listRaw);
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

        internal bool IsValidRationSchedule(RationScheduleSDO sdo, ref HIS_RATION_SCHEDULE raw)
        {
            bool valid = true;
            try
            {
                if (sdo != null)
                {
                    HIS_RATION_SCHEDULE RationChedule = new HIS_RATION_SCHEDULE();
                    if (sdo.RationScheduleId.HasValue)
                    {
                        RationChedule = new HisRationScheduleGet().GetById(sdo.RationScheduleId.Value);
                        if (RationChedule == null)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                            return false;
                        }
                        raw = RationChedule;
                    }
                    else
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                        return false;
                    }

                    if (RationChedule != null && RationChedule.VIR_TO_DATE != null && RationChedule.VIR_TO_DATE <= RationChedule.LAST_ASSIGN_DATE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRationSchedule_BaoAnDaKetThucKhongChoPhepSua);
                        return false;
                    }

                    if (RationChedule != null && sdo.ToTime < RationChedule.LAST_ASSIGN_DATE)
                    {
                        string convertLastAssignDate = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(RationChedule.LAST_ASSIGN_DATE ?? 0);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRationSchedule_ThoiGianDenPhaiLonHonNgayChiDinhCuoiCung, convertLastAssignDate);
                        return false;
                    }

                    if (RationChedule != null && RationChedule.FROM_TIME != sdo.FromTime && RationChedule.LAST_ASSIGN_DATE != null)
                    {
                        LogSystem.Warn("Khong cho phep sua thoi gian tu(FROM_TIME) do da co thoi gian chi dinh lon nhat (LAST_ASSIGN_DATE khac null)" + LogUtil.TraceData("LAST_ASSIGN_DATE: ", RationChedule.LAST_ASSIGN_DATE));
                        return false;
                    }

                    HisRationScheduleFilterQuery filter = new HisRationScheduleFilterQuery();
                    filter.TREATMENT_ID__EXACT = sdo.TreatmentId;
                    List<HIS_RATION_SCHEDULE> lstRationSchedules = new HisRationScheduleGet().Get(filter);

                    if (!IsNotNullOrEmpty(lstRationSchedules))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                        return false;
                    }
                    if (IsNotNullOrEmpty(lstRationSchedules))
                    {
                        List<HIS_RATION_SCHEDULE> rationSchedules = new List<HIS_RATION_SCHEDULE>();
                        if (sdo.IsForHomie)
                        {
                            rationSchedules = lstRationSchedules.Where(o => o.IS_FOR_HOMIE == Constant.IS_TRUE).ToList();
                        }
                        else
                        {
                            rationSchedules = lstRationSchedules.Where(o => o.IS_FOR_HOMIE != Constant.IS_TRUE).ToList();
                        }

                        if (IsNotNullOrEmpty(rationSchedules))
                        {
                            long rationScheId = rationSchedules.OrderByDescending(o => o.TO_TIME ?? 99999999999999).FirstOrDefault().ID;
                            long? maxToTime = rationSchedules.Max(o => o.TO_TIME);
                            string convertToTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(maxToTime ?? 0);
                            if (sdo.FromTime < maxToTime && rationScheId != sdo.RationScheduleId)
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRationSchedule_ThoiGianTuPhaiLonHonHoacBangThoiGianDen, convertToTime);
                                return false;
                            }

                            List<HIS_RATION_SCHEDULE> rationScheduleTimes = rationSchedules.Where(o => o.ID != sdo.RationScheduleId && ((o.FROM_TIME >= sdo.FromTime && o.FROM_TIME <= sdo.ToTime) || (o.TO_TIME >= sdo.FromTime && o.TO_TIME <= sdo.ToTime) || o.TO_TIME == null)).ToList();
                            if (IsNotNullOrEmpty(rationScheduleTimes))
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRationSchedule_KhongChoPhepHoSoTonTaiHaiLichBaoAnCoThoiGianGiaoNhau);
                                return false;
                            }
                        }
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRationSchedules))
            {
                if (!DAOWorker.HisRationScheduleDAO.UpdateList(this.beforeUpdateHisRationSchedules))
                {
                    LogSystem.Warn("Rollback du lieu HisRationSchedule that bai, can kiem tra lai." + LogUtil.TraceData("HisRationSchedules", this.beforeUpdateHisRationSchedules));
                }
				this.beforeUpdateHisRationSchedules = null;
            }
        }
    }
}
