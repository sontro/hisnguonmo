using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt.HeinTreatmentType;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRetyCat
{
    /// <summary>
    /// Nguyen tac xay dung lop Checker: chu dong va doc lap
    /// 1. Chu dong: phai dung o goc do Checker la nguoi chu dong dua ra API check cho nguoi khac su dung tu khi ho con chua co nhu cau. Viec check cai gi, y nghia ra sao can duoc phan tich suy nghi khong le thuoc vao tinh huong cu the de tranh API bi han che ve mat tam nhin.
    /// 2. Doc lap: 1 ham check chi xu ly 1 va chi dung 1 viec. Tuyet doi tranh ham check nay lai phai lo ho nhiem vu cua 1 ham check khac. Nhu y tren, viec cua Checker la dua ra ham check & viec cua nguoi su dung la phai hieu ro ve cac ham check de su dung ket hop chung theo dung thu tu. Tu duy nay neu duoc trien khai tot se giup cac ham check khong bi phu thuoc vao nhau, co duoc su don gian, de tai su dung.
    /// </summary>
    partial class HisServiceRetyCatCheck : BusinessBase
    {
        internal HisServiceRetyCatCheck()
            : base()
        {

        }

        internal HisServiceRetyCatCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_SERVICE_RETY_CAT data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.REPORT_TYPE_CAT_ID)) throw new ArgumentNullException("data.REPORT_TYPE_CAT_ID");
                if (data.SERVICE_ID < 0) throw new ArgumentNullException("data.SERVICE_ID");
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
                if (new HisServiceRetyCatGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_SERVICE_RETY_CAT data)
        {
            bool valid = true;
            try
            {
                data = new HisServiceRetyCatGet().GetById(id);
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
                    HisServiceRetyCatFilterQuery filter = new HisServiceRetyCatFilterQuery();
                    filter.IDs = listId;
                    List<HIS_SERVICE_RETY_CAT> listData = new HisServiceRetyCatGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_SERVICE_RETY_CAT> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisServiceRetyCatFilterQuery filter = new HisServiceRetyCatFilterQuery();
                    filter.IDs = listId;
                    List<HIS_SERVICE_RETY_CAT> listData = new HisServiceRetyCatGet().Get(filter);
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
        internal bool IsUnLock(HIS_SERVICE_RETY_CAT data)
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
                if (!DAOWorker.HisServiceRetyCatDAO.IsUnLock(id))
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
                    HisServiceRetyCatFilterQuery filter = new HisServiceRetyCatFilterQuery();
                    filter.IDs = listId;
                    List<HIS_SERVICE_RETY_CAT> listData = new HisServiceRetyCatGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_SERVICE_RETY_CAT> listData)
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

        internal bool VerifyExisted(HIS_SERVICE_RETY_CAT data)
        {
            bool valid = true;
            try
            {
                HisServiceRetyCatFilterQuery filter = new HisServiceRetyCatFilterQuery();
                filter.SERVICE_ID = data.SERVICE_ID;
                filter.REPORT_TYPE_CAT_ID = data.REPORT_TYPE_CAT_ID;
                List<HIS_SERVICE_RETY_CAT> serviceRetyCats = new HisServiceRetyCatGet().Get(filter);
                if (IsNotNullOrEmpty(serviceRetyCats))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceRetyCat_DichVuDaTonTaiTrongNhomLoaiBaoCao);
                    throw new Exception("Da ton tai dich vu trong nhom loai bao cao nay." + LogUtil.TraceData("serviceRetyCats", serviceRetyCats));
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
