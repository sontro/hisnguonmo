using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExamSchedule
{
    partial class HisExamScheduleCheck : BusinessBase
    {
        internal HisExamScheduleCheck()
            : base()
        {

        }

        internal HisExamScheduleCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_EXAM_SCHEDULE data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
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
                if (new HisExamScheduleGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_EXAM_SCHEDULE data)
        {
            bool valid = true;
            try
            {
                data = new HisExamScheduleGet().GetById(id);
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
                    HisExamScheduleFilterQuery filter = new HisExamScheduleFilterQuery();
                    filter.IDs = listId;
                    List<HIS_EXAM_SCHEDULE> listData = new HisExamScheduleGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_EXAM_SCHEDULE> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisExamScheduleFilterQuery filter = new HisExamScheduleFilterQuery();
                    filter.IDs = listId;
                    List<HIS_EXAM_SCHEDULE> listData = new HisExamScheduleGet().Get(filter);
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
        internal bool IsUnLock(HIS_EXAM_SCHEDULE data)
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
                if (!DAOWorker.HisExamScheduleDAO.IsUnLock(id))
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
                    HisExamScheduleFilterQuery filter = new HisExamScheduleFilterQuery();
                    filter.IDs = listId;
                    List<HIS_EXAM_SCHEDULE> listData = new HisExamScheduleGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_EXAM_SCHEDULE> listData)
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

        internal bool CheckConstraint(long id)
        {
            bool valid = true;
            try
            {
                /*List<XXX> hisXXXs = new HisXXXGet().GetByHisExamScheduleId(id);
                if (IsNotNullOrEmpty(hisXXXs))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisXXX_TonTaiDuLieu);
                    return false;
                }*/
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool CheckExists(HIS_EXAM_SCHEDULE data, long id)
        {
            bool valid = true;
            try
            {
                if (data != null)
                {
                    HisExamScheduleFilterQuery filter = new HisExamScheduleFilterQuery();
                    filter.ROOM_ID = data.ROOM_ID;
                    filter.DAY_OF_WEEK__EQUAL = data.DAY_OF_WEEK;

                    List<HIS_EXAM_SCHEDULE> exists = new HisExamScheduleGet().Get(filter);
                    List<HIS_EXAM_SCHEDULE> duplicates = exists != null ? exists.Where(o => o.ID != id && !this.notDulicate(o, data)).ToList() : null;
                    if (IsNotNullOrEmpty(duplicates))
                    {
                        string mes = String.Join(", ", duplicates.Select(s => String.Format("{0}({1} - {2})", s.LOGINNAME, this.HHmmToTimeString(s.TIME_FROM), this.HHmmToTimeString(s.TIME_TO))).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExamSchedule_TrungKhoangThoiGianLamViec, mes);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        private bool notDulicate(HIS_EXAM_SCHEDULE oldData, HIS_EXAM_SCHEDULE newData)
        {
            long oldFromTime = Convert.ToInt64(oldData.TIME_FROM);
            long? oldToTime = null;
            if (!String.IsNullOrWhiteSpace(oldData.TIME_TO))
                oldToTime = Convert.ToInt64(oldData.TIME_TO);
            long newFromTime = Convert.ToInt64(newData.TIME_FROM);
            long? newToTime = null;
            if (!String.IsNullOrWhiteSpace(newData.TIME_TO))
                newToTime = Convert.ToInt64(newData.TIME_TO);

            if (oldFromTime <= newFromTime && (!oldToTime.HasValue || oldToTime.Value >= newFromTime))
            {
                return false;
            }

            if (oldFromTime >= newFromTime && (!newToTime.HasValue || newToTime.Value >= oldFromTime))
            {
                return false;
            }



            return true;
        }

        private string HHmmToTimeString(string time)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(time) && time.Length == 4)
                {
                    return String.Format("{0}:{1}", time.Substring(0, 2), time.Substring(2, 2));
                }
                return null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        internal bool VerifyTime(HIS_EXAM_SCHEDULE data)
        {
            bool valid = true;
            try
            {
                long fromTime = Convert.ToInt64(data.TIME_FROM);
                long? toTime = null;
                if (!String.IsNullOrWhiteSpace(data.TIME_TO))
                    toTime = Convert.ToInt64(data.TIME_TO);

                if (toTime.HasValue && fromTime >= toTime)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExamSchedule_ThoiGianTuPhaiBeHonThoiGianDen);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }
    }
}
