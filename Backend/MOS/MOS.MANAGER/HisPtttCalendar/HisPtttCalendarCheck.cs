using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisServiceReq;

namespace MOS.MANAGER.HisPtttCalendar
{
    partial class HisPtttCalendarCheck : BusinessBase
    {
        internal HisPtttCalendarCheck()
            : base()
        {

        }

        internal HisPtttCalendarCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_PTTT_CALENDAR data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.DEPARTMENT_ID <= 0) throw new ArgumentNullException("data.DEPARTMENT_ID");
                if (data.TIME_FROM <= 0) throw new ArgumentNullException("data.TIME_FROM");
                if (data.TIME_TO <= 0) throw new ArgumentNullException("data.TIME_TO");
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

        internal bool VerifyRequireField(HisPtttCalendarSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.WorkingRoomId <= 0) throw new ArgumentNullException("data.WorkingRoomId");
                if (data.TimeFrom <= 0) throw new ArgumentNullException("data.TimeFrom");
                if (data.TimeTo <= 0) throw new ArgumentNullException("data.TimeTo");
                if (data.TimeTo < data.TimeFrom) throw new ArgumentNullException("data.TimeTo < data.TimeFrom");
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
        /// Kiem tra xem thoi gian co giao voi cac lich mo khac hay khong
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="timeFrom"></param>
        /// <param name="timeTo"></param>
        /// <returns></returns>
        internal bool IsNotIntersectTime(long departmentId, long timeFrom, long timeTo, long id)
        {
            bool result = true;
            try
            {
                string sql = "SELECT * FROM HIS_PTTT_CALENDAR P WHERE P.ID <> :param1 AND P.DEPARTMENT_ID = :param2 AND ((P.TIME_FROM <= :param3 AND P.TIME_TO >= :param4) OR (P.TIME_FROM <= :param5 AND P.TIME_TO >= :param6))";
                List<HIS_PTTT_CALENDAR> exists = DAOWorker.SqlDAO.GetSql<HIS_PTTT_CALENDAR>(sql, id, departmentId, timeFrom, timeFrom, timeTo, timeTo);
                if (IsNotNullOrEmpty(exists))
                {
                    List<string> ptttCalendarCodes = exists.Select(o => o.PTTT_CALENDAR_CODE).ToList();
                    string ptttCalendarCodeStr = string.Join(",", ptttCalendarCodes);
                    string timeFromStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(timeFrom);
                    string timeToStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(timeTo);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPtttCalendar_TonTaiLichPhauThuatCoKhoangThoiGianGiaoVoiKhoangThoiGianDaChon, ptttCalendarCodeStr, timeFromStr, timeToStr);
                    result = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// Kiem tra xem thoi gian co giao voi cac lich mo khac hay khong
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="timeFrom"></param>
        /// <param name="timeTo"></param>
        /// <returns></returns>
        internal bool HasNoServiceReqOutOfTime(long id, long timeFrom, long timeTo)
        {
            bool result = true;
            try
            {
                string sql = "SELECT * FROM HIS_SERVICE_REQ S WHERE S.PTTT_CALENDAR_ID = :param1 AND (S.PLAN_TIME_FROM > :parm2 OR s.PLAN_TIME_TO < :param3)";
                List<HIS_SERVICE_REQ> exists = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ>(sql, id, timeTo, timeFrom);
                if (IsNotNullOrEmpty(exists))
                {
                    List<string> serviceReqCodes = exists.Select(o => o.SERVICE_REQ_CODE).ToList();
                    string serviceReqCodeStr = string.Join(",", serviceReqCodes);
                    string timeFromStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(timeFrom);
                    string timeToStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(timeTo);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPtttCalendar_TonTaiPhauThuatCoThoiGianKeHoachNamNgoaiKhoangThoiGianDaChon, serviceReqCodeStr, timeFromStr, timeToStr);
                    result = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// Kiem tra xem thoi gian co giao voi cac lich mo khac hay khong
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="timeFrom"></param>
        /// <param name="timeTo"></param>
        /// <returns></returns>
        internal bool IsNotApproved(HIS_PTTT_CALENDAR calendar)
        {
            bool result = true;
            try
            {
                if (calendar.APPROVAL_TIME.HasValue || !string.IsNullOrWhiteSpace(calendar.APPROVAL_LOGINNAME))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPtttCalendar_LichDaDuocDuyet);
                    result = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
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
                if (new HisPtttCalendarGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_PTTT_CALENDAR data)
        {
            bool valid = true;
            try
            {
                data = new HisPtttCalendarGet().GetById(id);
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
                    HisPtttCalendarFilterQuery filter = new HisPtttCalendarFilterQuery();
                    filter.IDs = listId;
                    List<HIS_PTTT_CALENDAR> listData = new HisPtttCalendarGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_PTTT_CALENDAR> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisPtttCalendarFilterQuery filter = new HisPtttCalendarFilterQuery();
                    filter.IDs = listId;
                    List<HIS_PTTT_CALENDAR> listData = new HisPtttCalendarGet().Get(filter);
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
        internal bool IsUnLock(HIS_PTTT_CALENDAR data)
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
                if (!DAOWorker.HisPtttCalendarDAO.IsUnLock(id))
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
                    HisPtttCalendarFilterQuery filter = new HisPtttCalendarFilterQuery();
                    filter.IDs = listId;
                    List<HIS_PTTT_CALENDAR> listData = new HisPtttCalendarGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_PTTT_CALENDAR> listData)
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
                List<HIS_SERVICE_REQ> exists = new HisServiceReqGet().GetByPtttCalendarId(id);
                if (IsNotNullOrEmpty(exists))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_SERVICE_REQ, khong cho phep xoa" + LogUtil.TraceData("id", id));
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
