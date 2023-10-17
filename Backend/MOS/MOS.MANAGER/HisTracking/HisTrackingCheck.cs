using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using MOS.SDO;
using Inventec.Token.ResourceSystem;

namespace MOS.MANAGER.HisTracking
{
    partial class HisTrackingCheck : BusinessBase
    {
        internal HisTrackingCheck()
            : base()
        {

        }

        internal HisTrackingCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool ValiTrackingTime(HIS_TRACKING data)
        {
            bool valid = true;
            try
            {
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(data.TREATMENT_ID);
                if (treatment == null)
                {
                    throw new Exception("Khong tim thay ho so dieu tri . TreatmentID " + data.TREATMENT_ID);
                }

                if (treatment.OUT_TIME.HasValue && data.TRACKING_TIME > treatment.OUT_TIME.Value)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTracking_ThoiGianDieuTriThoiGianKetThucDieuTri);
                    return false;
                }

                if (data.TRACKING_TIME < treatment.IN_TIME)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTracking_ThoiGianDieuTriThoiGianVaoVien);
                    return false;
                }
            }
            catch (Exception ex)
            {
                valid = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return valid;
        }

        internal bool VerifyRequireField(HIS_TRACKING data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.TREATMENT_ID <= 0) throw new ArgumentNullException("data.TREATMENT_ID");
                if (data.TRACKING_TIME <= 0) throw new ArgumentNullException("data.TRACKING_TIME");
                if (!data.DEPARTMENT_ID.HasValue) throw new ArgumentNullException("data.DEPARTMENT_ID");
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
                if (new HisTrackingGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_TRACKING data)
        {
            bool valid = true;
            try
            {
                data = new HisTrackingGet().GetById(id);
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
                    HisTrackingFilterQuery filter = new HisTrackingFilterQuery();
                    filter.IDs = listId;
                    List<HIS_TRACKING> listData = new HisTrackingGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_TRACKING> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisTrackingFilterQuery filter = new HisTrackingFilterQuery();
                    filter.IDs = listId;
                    List<HIS_TRACKING> listData = new HisTrackingGet().Get(filter);
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
        internal bool IsUnLock(HIS_TRACKING data)
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
                if (!DAOWorker.HisTrackingDAO.IsUnLock(id))
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
                    HisTrackingFilterQuery filter = new HisTrackingFilterQuery();
                    filter.IDs = listId;
                    List<HIS_TRACKING> listData = new HisTrackingGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_TRACKING> listData)
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

        internal bool IsNotExisted(HIS_TRACKING raw)
        {
            bool valid = true;
            try
            {
                long? beginDay = Inventec.Common.DateTime.Get.StartDay(raw.TRACKING_TIME);
                long? endDay = Inventec.Common.DateTime.Get.EndDay(raw.TRACKING_TIME);
                HisTrackingFilterQuery filter = new HisTrackingFilterQuery();
                filter.TRACKING_TIME_FROM = beginDay;
                filter.TRACKING_TIME_TO = endDay;
                filter.TREATMENT_ID = raw.TREATMENT_ID;
                List<HIS_TRACKING> existTrackings = new HisTrackingGet().Get(filter);
                List<HIS_TRACKING> existTrackingOthers = IsNotNullOrEmpty(existTrackings) ? existTrackings.Where(o => o.ID != raw.ID).ToList() : null;

                if (IsNotNullOrEmpty(existTrackingOthers))
                {
                    string day = Inventec.Common.DateTime.Convert.TimeNumberToDateString(beginDay.Value);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTracking_DaTonTaiToDieuTriCuaNgay, day);
                    throw new Exception("Da ton tai to dieu tri tuong ung voi ngay " + day);
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
        /// Kiem tra cho phep tao, sua, xoa to dieu tri voi ho so da kie thuc hay khong
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        internal bool IsAllowCreateOrEdit(HIS_TRACKING data)
        {
            bool valid = true;
            try
            {
                if (data != null && HisTreatmentCFG.DO_NOT_ALLOW_CREATE_OR_EDIT_TRACKING)
                {
                    HIS_TREATMENT treatment = null;
                    HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                    valid = valid && treatmentChecker.VerifyId(data.TREATMENT_ID, ref treatment);
                    valid = valid && treatmentChecker.IsUnpause(treatment);
                    valid = valid && treatmentChecker.IsUnLock(treatment);
                    valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                    valid = valid && treatmentChecker.IsUnLockHein(treatment);
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
        /// Kiem tra cho phep sua to dieu tri hay khong 
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        internal bool IsAllowEdit(HIS_TRACKING data)
        {
            bool valid = true;
            try
            {
                string loginName = ResourceTokenManager.GetLoginName();
                var coltrolRole = new AcsAuthorize.AcsAuthorizeGet().GetControlInRoles();
                if ((string.IsNullOrWhiteSpace(loginName) ||
                    (!loginName.Equals(data.CREATOR) &&
                    !MOS.MANAGER.HisEmployee.HisEmployeeUtil.IsAdmin(loginName) &&
                    (coltrolRole == null || !coltrolRole.Exists(o => o.CONTROL_CODE == "HIS000033")) &&
                    !MOS.MANAGER.HisEmployee.HisEmployeeUtil.IsSampleDepartment(data.CREATOR, loginName))))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDoNguoiKhacTaoKhongChoPhepSua);
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
    }
}
