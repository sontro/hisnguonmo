using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MOS.MANAGER.HisEmployeeSchedule
{
    partial class HisEmployeeScheduleCheck : BusinessBase
    {
        internal HisEmployeeScheduleCheck()
            : base()
        {

        }

        internal HisEmployeeScheduleCheck(CommonParam paramCheck)
            : base(paramCheck) 
        {

        }

        internal bool VerifyRequireField(HIS_EMPLOYEE_SCHEDULE data)
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
                if (new HisEmployeeScheduleGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_EMPLOYEE_SCHEDULE data)
        {
            bool valid = true;
            try
            {
                data = new HisEmployeeScheduleGet().GetById(id);
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
                    HisEmployeeScheduleFilterQuery filter = new HisEmployeeScheduleFilterQuery();
                    filter.IDs = listId;
                    List<HIS_EMPLOYEE_SCHEDULE> listData = new HisEmployeeScheduleGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_EMPLOYEE_SCHEDULE> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisEmployeeScheduleFilterQuery filter = new HisEmployeeScheduleFilterQuery();
                    filter.IDs = listId;
                    List<HIS_EMPLOYEE_SCHEDULE> listData = new HisEmployeeScheduleGet().Get(filter);
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
        internal bool IsUnLock(HIS_EMPLOYEE_SCHEDULE data)
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
                if (!DAOWorker.HisEmployeeScheduleDAO.IsUnLock(id))
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
                    HisEmployeeScheduleFilterQuery filter = new HisEmployeeScheduleFilterQuery();
                    filter.IDs = listId;
                    List<HIS_EMPLOYEE_SCHEDULE> listData = new HisEmployeeScheduleGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_EMPLOYEE_SCHEDULE> listData)
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
                /*List<XXX> hisXXXs = new HisXXXGet().GetByHisEmployeeScheduleId(id);
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

        /// <summary>
        /// Kiem tra su ton tai cua du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyValid(List<HIS_EMPLOYEE_SCHEDULE> before, HIS_EMPLOYEE_SCHEDULE data)
        {
            bool valid = true;
            try
            {
                if (data == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("Id invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), LogType.Error);
                    valid = false;
                }
                if (data.LOGINNAME == null)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BatBuocChonTaiKhoan);
                    return false;
                }
                if (data.SCHEDULE_DATE == null)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BatBuocChonNgay);
                    return false;
                }
                if (IsNotNullOrEmpty(before))
                {
                    decimal? A = decimal.Parse(data.SCHEDULE_DATE.ToString() + data.TIME_FROM + "00");
                    decimal? B = decimal.Parse(data.SCHEDULE_DATE.ToString() + data.TIME_TO + "59");

                    List<HIS_EMPLOYEE_SCHEDULE> be = before.Where(o => o.LOGINNAME == data.LOGINNAME && o.ID != data.ID).ToList();
                    if (IsNotNullOrEmpty(be))
                    {
                        be = be.Where(o => ((o.VIR_SCHEDULE_TIME_FROM <= A) && (A <= o.VIR_SCHEDULE_TIME_TO))
                            || ((o.VIR_SCHEDULE_TIME_FROM <= B) && (B <= o.VIR_SCHEDULE_TIME_TO))
                            || ((A <= o.VIR_SCHEDULE_TIME_FROM) && (o.VIR_SCHEDULE_TIME_FROM <= B))
                            || ((A <= o.VIR_SCHEDULE_TIME_TO) && (o.VIR_SCHEDULE_TIME_TO <= B))).ToList();
                        if (IsNotNullOrEmpty(be))
                        {
                            string login = string.Format("{0} - {1}", data.LOGINNAME, data.USERNAME);
                            string timeFrom = data.TIME_FROM != null ? string.Format("{0}h{1}p", data.TIME_FROM.Substring(0, 2), data.TIME_FROM.Substring(2, 2)) : "";
                            string timeTo = data.TIME_TO != null ? string.Format("{0}h{1}p", data.TIME_TO.Substring(0, 2), data.TIME_TO.Substring(2, 2)) : "";
                            string scheduleDate = Inventec.Common.DateTime.Convert.TimeNumberToDateString((long)data.SCHEDULE_DATE);
                            MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisEmployeeSchedule_ThoiGianKhongDuocNamTrongKhoangThoiGianDaTao, login, scheduleDate, timeFrom, timeTo);
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

        internal bool CheckSchedule()
        {
            bool result = false;
            try
            {
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                HIS_EMPLOYEE employee = HisEmployeeCFG.DATA.FirstOrDefault(o => o.LOGINNAME == loginname);
                if (employee != null)
                {
                    if (employee.IS_LIMIT_SCHEDULE != Constant.IS_TRUE)
                    {
                        return true;
                    }
                    if (employee.IS_LIMIT_SCHEDULE == Constant.IS_TRUE)
                    {
                        HisEmployeeScheduleFilterQuery filter = new HisEmployeeScheduleFilterQuery();
                        filter.LOGINNAME__EXACT = loginname;
                        filter.SCHEDULE_DATE = int.Parse(System.DateTime.Now.ToString("yyyyMMdd"));

                        List<HIS_EMPLOYEE_SCHEDULE> employeeSchedules = new HisEmployeeScheduleGet().Get(filter);

                        if (IsNotNullOrEmpty(employeeSchedules))
                        {
                            decimal? thoiGianHtai = (decimal)(Inventec.Common.DateTime.Get.Now());
                            HIS_EMPLOYEE_SCHEDULE employeeSche = employeeSchedules.FirstOrDefault(o => (o.VIR_SCHEDULE_TIME_FROM <= thoiGianHtai) && (thoiGianHtai <= o.VIR_SCHEDULE_TIME_TO));

                            if (employeeSche == null)
                            {
                                MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisEmployeeSchedule_TaiKhoanChuaDuocThietLapLichLamViec);
                                return false;
                            }
                            if (employeeSche != null)
                            {
                                result = true;
                                Inventec.Token.Core.TokenData token = Inventec.Token.ResourceSystem.ResourceTokenManager.GetTokenData();
                                long virTimeTo = (long)(employeeSche.VIR_SCHEDULE_TIME_TO ?? 0);
                                long expireTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(token.ExpireTime) ?? 0;

                                if (virTimeTo < expireTime)
                                {
                                    Inventec.Core.ApiResultObject<bool> apiResult = ApiConsumerStore.AcsConsumer.Post<Inventec.Core.ApiResultObject<bool>>("api/AcsToken/UpdateExpireTime", param, virTimeTo);
                                    if (apiResult != null && apiResult.Data)
                                    {
                                        result = true;
                                        Inventec.Token.ResourceSystem.ResourceTokenManager.UpdateExpireTime(virTimeTo);
                                    }

                                }

                            }
                        }
                        else
                        {
                            MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisEmployeeSchedule_TaiKhoanChuaDuocThietLapLichLamViec);
                            return false;
                        }
                    }
                }
                else
                {
                    return true;
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
