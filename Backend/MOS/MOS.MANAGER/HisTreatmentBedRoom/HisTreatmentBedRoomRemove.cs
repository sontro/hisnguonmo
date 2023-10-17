using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisBedLog.Update;
using MOS.MANAGER.HisCoTreatment;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentBedRoom
{
    partial class HisTreatmentBedRoomRemove : BusinessBase
    {
        private List<HIS_TREATMENT_BED_ROOM> beforeUpdateHisTreatmentBedRooms = new List<HIS_TREATMENT_BED_ROOM>();
        private HisBedLogUpdate hisBedLogUpdate;

        internal HisTreatmentBedRoomRemove()
            : base()
        {
            this.hisBedLogUpdate = new HisBedLogUpdate(param);
        }

        internal HisTreatmentBedRoomRemove(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisBedLogUpdate = new HisBedLogUpdate(param);
        }

        internal bool Remove(HIS_TREATMENT_BED_ROOM data, ref HIS_TREATMENT_BED_ROOM resultData)
        {
            HIS_TREATMENT_BED_ROOM raw = new HisTreatmentBedRoomGet().GetById(data.ID);

            bool valid = true;
            valid = valid && data.REMOVE_TIME.HasValue;
            valid = valid && raw != null;

            if (valid)
            {
                List<HIS_TREATMENT_BED_ROOM> tmp = null;
                if (this.Remove(new List<HIS_TREATMENT_BED_ROOM>() { raw }, data.REMOVE_TIME.Value, true, ref tmp))
                {
                    resultData = tmp[0];
                    return true;
                }
            }
            return false;
        }

        internal bool Remove(List<HIS_TREATMENT_BED_ROOM> listRaw, long removeTime, bool checkCoTreatment, ref List<HIS_TREATMENT_BED_ROOM> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_BED_LOG> bedLogs = null;
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);

                valid = valid && this.IsValidRemoveTime(listRaw, removeTime, ref bedLogs);
                valid = valid && this.CheckCoTreatment(listRaw, checkCoTreatment);
                valid = valid && checker.VerifyId(listRaw[0].TREATMENT_ID, ref treatment);
                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT_BED_ROOM, HIS_TREATMENT_BED_ROOM>();
                    List<HIS_TREATMENT_BED_ROOM> beforeUpdates = Mapper.Map<List<HIS_TREATMENT_BED_ROOM>>(listRaw);

                    listRaw.ForEach(o =>
                    {
                        o.REMOVE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        o.REMOVE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        o.REMOVE_TIME = removeTime;
                    });

                    if (DAOWorker.HisTreatmentBedRoomDAO.UpdateList(listRaw))
                    {
                        this.beforeUpdateHisTreatmentBedRooms.AddRange(beforeUpdates);

                        this.ProcessBedLog(bedLogs, removeTime, beforeUpdates[0].TREATMENT_ID);

                        resultData = listRaw;
                        result = true;
                    }

                    if (result)
                    {
                        List<string> bedRoomNames = HisBedRoomCFG.DATA != null ? HisBedRoomCFG.DATA.Where(o => listRaw != null && listRaw.Exists(t => t.BED_ROOM_ID == o.ID)).Select(o => o.BED_ROOM_NAME).ToList() : null;
                        string bedRoomNameStr = string.Join(",", bedRoomNames);

                        new EventLogGenerator(EventLog.Enum.HisTreatment_RoiBuong, bedRoomNames).TreatmentCode(treatment.TREATMENT_CODE).Run();
                    }
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

        private void ProcessBedLog(List<HIS_BED_LOG> bedLogs, long removeTime, long treatmentId)
        {
            HIS_TREATMENT treatment = new HisTreatmentGet().GetById(treatmentId);

            List<HIS_BED_LOG> toUpdates = bedLogs != null ? bedLogs.Where(o => !o.FINISH_TIME.HasValue).ToList() : null;
            if (IsNotNullOrEmpty(toUpdates))
            {
                if (!this.hisBedLogUpdate.Finish(toUpdates, treatment, removeTime))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        private bool CheckCoTreatment(List<HIS_TREATMENT_BED_ROOM> listRaw, bool checkCoTreatment)
        {
            bool valid = true;
            try
            {
                if (checkCoTreatment)
                {
                    List<long> bedRoomIds = listRaw.Where(o => o.CO_TREATMENT_ID.HasValue).Select(s => s.BED_ROOM_ID).ToList();
                    if (IsNotNullOrEmpty(bedRoomIds))
                    {
                        List<V_HIS_BED_ROOM> hisBedRooms = HisBedRoomCFG.DATA.Where(o => bedRoomIds.Contains(o.ID)).ToList();
                        List<string> departs = hisBedRooms != null ? hisBedRooms.Select(s => s.DEPARTMENT_NAME).Distinct().ToList() : new List<string>();
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatmentBedRoom_BenhNhanDieuTriKetHop,string.Join(";",departs));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisTreatmentBedRooms))
            {
                if (!DAOWorker.HisTreatmentBedRoomDAO.UpdateList(this.beforeUpdateHisTreatmentBedRooms))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentBedRoom that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisTreatmentBedRooms", this.beforeUpdateHisTreatmentBedRooms));
                }
                this.beforeUpdateHisTreatmentBedRooms = null;
            }
        }

        private bool IsValidRemoveTime(List<HIS_TREATMENT_BED_ROOM> treatmentBedRooms, long removeTime, ref List<HIS_BED_LOG> hisBedLogs)
        {
            bool valid = true;
            try
            {
                List<long> removedList = treatmentBedRooms
                    .Where(o => o.REMOVE_TIME.HasValue)
                    .Select(o => o.BED_ROOM_ID).ToList();
                if (IsNotNullOrEmpty(removedList))
                {
                    List<string> bedRoomNames = HisBedRoomCFG.DATA
                        .Where(o => removedList.Contains(o.ID))
                        .Select(o => o.BED_ROOM_NAME).Distinct().ToList();
                    string tmp = string.Join(", ", bedRoomNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatmentBedRoom_BenhNhanDaRoiKhoiBuong, tmp);
                    return false;
                }

                List<HIS_TREATMENT_BED_ROOM> invalids = treatmentBedRooms.Where(o => o.ADD_TIME > removeTime).ToList();

                if (IsNotNullOrEmpty(invalids))
                {
                    string addTimeStr = "";
                    foreach (HIS_TREATMENT_BED_ROOM t in invalids)
                    {
                        string tmp = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(t.ADD_TIME);
                        addTimeStr += tmp + ";";
                    }

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatmentBedRoom_KhongChoPhepNhapThoiGianRaKhoiBuongLonHonThoiGianVaoBuong, addTimeStr);
                    return false;
                }

                hisBedLogs = new HisBedLogGet().GetByTreatmentBedRoomIds(treatmentBedRooms.Select(o => o.ID).ToList());
                if (hisBedLogs != null)
                {
                    List<HIS_BED_LOG> invalidStartTimes = hisBedLogs
                        .Where(o => o.START_TIME > removeTime)
                        .Distinct().ToList();
                    string startTimeStr = "";
                    foreach (HIS_BED_LOG t in invalidStartTimes)
                    {
                        string tmp = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(t.START_TIME);
                        startTimeStr += tmp + ";";
                    }
                    if (IsNotNullOrEmpty(invalidStartTimes))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatmentBedRoom_ThoiGianRaBuongNhoHonThoiGianBatDauSuDungGiuong, startTimeStr);
                        return false;
                    }

                    List<HIS_BED_LOG> invalidFinishTimes = hisBedLogs
                        .Where(o => o.FINISH_TIME.HasValue && o.FINISH_TIME.Value > removeTime)
                        .Distinct().ToList();
                    string finishTimeStr = "";
                    foreach (HIS_BED_LOG t in invalidStartTimes)
                    {
                        string tmp = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(t.FINISH_TIME.Value);
                        finishTimeStr += tmp + ";";
                    }

                    if (IsNotNullOrEmpty(invalidFinishTimes))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatmentBedRoom_ThoiGianRaBuongNhoHonThoiGianKetThucSuDungGiuong, finishTimeStr);
                        return false;
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

    }
}
