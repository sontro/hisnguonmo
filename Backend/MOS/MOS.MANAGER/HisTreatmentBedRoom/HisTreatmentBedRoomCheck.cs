using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt.HeinTreatmentType;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisCoTreatment;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentBedRoom
{
    partial class HisTreatmentBedRoomCheck : BusinessBase
    {
        internal HisTreatmentBedRoomCheck()
            : base()
        {

        }

        internal HisTreatmentBedRoomCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_TREATMENT_BED_ROOM data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.BED_ROOM_ID)) throw new ArgumentNullException("data.BED_ROOM_ID");
                if (!IsGreaterThanZero(data.TREATMENT_ID)) throw new ArgumentNullException("data.TREATMENT_ID");
                if (!IsGreaterThanZero(data.ADD_TIME)) throw new ArgumentNullException("data.ADD_TIME");
                if (!IsNotNullOrEmpty(data.ADD_USERNAME)) throw new ArgumentNullException("data.ADD_USERNAME");
                if (!IsNotNullOrEmpty(data.ADD_LOGINNAME)) throw new ArgumentNullException("data.ADD_LOGINNAME");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra su ton tai cua id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id)
        {
            bool valid = true;
            try
            {
                if (new HisTreatmentBedRoomGet().GetById(id) == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("Id invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id), LogType.Error);
                    valid = false;
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

        /// <summary>
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_TREATMENT_BED_ROOM data)
        {
            bool valid = true;
            try
            {
                data = new HisTreatmentBedRoomGet().GetById(id);
                if (data == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("Id invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id), LogType.Error);
                    valid = false;
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

        /// <summary>
        /// Kiem tra su ton tai cua danh sach cac id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyIds(List<long> listId)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisTreatmentBedRoomFilterQuery filter = new HisTreatmentBedRoomFilterQuery();
                    filter.IDs = listId;
                    List<HIS_TREATMENT_BED_ROOM> listData = new HisTreatmentBedRoomGet().Get(filter);
                    if (listData == null || listId.Count != listData.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        Logging("ListId invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listId), listId), LogType.Error);
                        valid = false;
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

        /// <summary>
        /// Kiem tra su ton tai cua danh sach cac id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyIds(List<long> listId, List<HIS_TREATMENT_BED_ROOM> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisTreatmentBedRoomFilterQuery filter = new HisTreatmentBedRoomFilterQuery();
                    filter.IDs = listId;
                    List<HIS_TREATMENT_BED_ROOM> listData = new HisTreatmentBedRoomGet().Get(filter);
                    if (listData == null || listId.Count != listData.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        Logging("ListId invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listId), listId), LogType.Error);
                        valid = false;
                    }
                    else
                    {
                        listObject.AddRange(listData);
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

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung doi tuong)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(HIS_TREATMENT_BED_ROOM data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
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

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung id)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisTreatmentBedRoomDAO.IsUnLock(id))
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
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

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach id)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<long> listId)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisTreatmentBedRoomFilterQuery filter = new HisTreatmentBedRoomFilterQuery();
                    filter.IDs = listId;
                    List<HIS_TREATMENT_BED_ROOM> listData = new HisTreatmentBedRoomGet().Get(filter);
                    if (listData != null && listData.Count > 0)
                    {
                        foreach (var data in listData)
                        {
                            if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                            {
                                valid = false;
                                break;
                            }
                        }
                        if (!valid)
                        {
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
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

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach data)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<HIS_TREATMENT_BED_ROOM> listData)
        {
            bool valid = true;
            try
            {
                if (listData != null && listData.Count > 0)
                {
                    foreach (var data in listData)
                    {
                        if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE) //khong duoc goi ham IsUnLock(data) vi vi pham nguyen tac doc lap
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (!valid)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
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

        internal bool ValidTreatmentTypeCode(HIS_TREATMENT_BED_ROOM data)
        {
            bool valid = true;
            try
            {
                HIS_PATIENT_TYPE_ALTER lastPatientTypeAlter = new HisPatientTypeAlterGet().GetLastByTreatmentId(data.TREATMENT_ID);
                if (lastPatientTypeAlter == null
                    || lastPatientTypeAlter.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatmentBedRoom_BenhNhanDoiTuongLaDieuTriMoiChoPhepVaoBuongBenh);
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

        internal bool IsNotInRoom(HIS_TREATMENT_BED_ROOM data)
        {
            bool valid = true;
            try
            {
                List<HIS_TREATMENT_BED_ROOM> treatmentBedRooms = new HisTreatmentBedRoomGet().GetCurrentIn(data.BED_ROOM_ID, data.TREATMENT_ID);
                if (IsNotNullOrEmpty(treatmentBedRooms))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatmentBedRoom_BenhNhanDangTrongBuongBenhKhongChoPhepTao);
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

        internal bool IsValidTime(HIS_TREATMENT_BED_ROOM data, HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (treatment.IN_TIME > data.ADD_TIME)
                {
                    string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(treatment.IN_TIME);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatmentBedRoom_ThoiGianVaoBuongKhongDuocNhoHonThoiGianDenVien, time);
                    return false;
                }

                if (treatment.OUT_TIME.HasValue && treatment.OUT_TIME < data.ADD_TIME)
                {
                    string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(treatment.OUT_TIME.Value);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatmentBedRoom_ThoiGianVaoBuongKhongDuocLonHonThoiGianRaVien, time);
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

        internal bool IsValidDepartmentTranTime(HIS_TREATMENT_BED_ROOM data)
        {
            bool valid = true;
            try
            {
                if (data.CO_TREATMENT_ID.HasValue)
                {
                    HIS_CO_TREATMENT coTreat = new HisCoTreatmentGet().GetById(data.CO_TREATMENT_ID.Value);
                    if (coTreat.START_TIME.HasValue && data.ADD_TIME < coTreat.START_TIME.Value)
                    {
                        string add = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.ADD_TIME);
                        string start = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(coTreat.START_TIME ?? 0);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatmentBedRoom_ThoiGianVaoBuongBeHonVaoKhoaKetHop, add, start);
                        return false;
                    }
                    if (coTreat.FINISH_TIME.HasValue && data.REMOVE_TIME.HasValue && data.REMOVE_TIME.Value < coTreat.FINISH_TIME.Value)
                    {
                        string remove = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.REMOVE_TIME ?? 0);
                        string finish = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(coTreat.FINISH_TIME ?? 0);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatmentBedRoom_ThoiGianRaBuongLonHonRaKhoaKetHop, remove, finish);
                        return false;
                    }
                }
                else
                {
                    List<HIS_DEPARTMENT_TRAN> departmentTrans = new HisDepartmentTranGet().GetByTreatmentId(data.TREATMENT_ID);
                    V_HIS_BED_ROOM bedRoom = HisBedRoomCFG.DATA.FirstOrDefault(o => o.ID == data.BED_ROOM_ID);
                    if (departmentTrans != null && departmentTrans.Count > 0)
                    {
                        departmentTrans = departmentTrans.OrderBy(o => o.DEPARTMENT_IN_TIME).ToList();
                        bool isIn = false;
                        bool exists = false;
                        foreach (var item in departmentTrans)
                        {
                            if (item.DEPARTMENT_ID != bedRoom.DEPARTMENT_ID)
                                continue;
                            exists = true;
                            if (item.DEPARTMENT_IN_TIME > data.ADD_TIME)
                            {
                                continue;
                            }
                            if (data.REMOVE_TIME.HasValue)
                            {
                                var next = departmentTrans.FirstOrDefault(o => o.PREVIOUS_ID == item.ID);
                                if (next != null)
                                {
                                    if (next.DEPARTMENT_IN_TIME < data.REMOVE_TIME.Value)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        isIn = true;
                                        continue;
                                    }
                                }
                            }
                            isIn = true;
                        }

                        if (!isIn)
                        {
                            if (exists)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatmentBedRoom_ThoiGianVaoBuongNgoaiKhoangThoiGianVoaKhoa);
                            }
                            else
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatmentBedRoom_KhongCoDuLieuVaoKhoa);
                            }
                            return false;
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

        internal bool IsExistsDepartmentTran(HIS_TREATMENT_BED_ROOM data)
        {
            bool valid = true;
            try
            {
                if (!data.CO_TREATMENT_ID.HasValue)
                {
                    List<HIS_DEPARTMENT_TRAN> departmentTrans = new HisDepartmentTranGet().GetByTreatmentId(data.TREATMENT_ID);
                    V_HIS_BED_ROOM bedRoom = HisBedRoomCFG.DATA.FirstOrDefault(o => o.ID == data.BED_ROOM_ID);
                    if (departmentTrans == null || !departmentTrans.Any(a => a.DEPARTMENT_ID == bedRoom.DEPARTMENT_ID && a.DEPARTMENT_IN_TIME.HasValue))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatmentBedRoom_KhongCoDuLieuVaoKhoa);
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

        internal bool CheckConstraint(long id)
        {
            bool valid = true;
            try
            {
                List<HIS_BED_LOG> exists = new HisBedLogGet().GetByTreatmentBedRoomId(id);
                if (IsNotNullOrEmpty(exists))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBedLog_TonTaiDuLieu);
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

        public bool IsValidRemoveTime(List<HIS_TREATMENT_BED_ROOM> treatmentBedRooms, long removeTime, ref List<HIS_BED_LOG> hisBedLogs)
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
