using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentEndType
{
    /// <summary>
    /// Nguyen tac xay dung lop Checker: chu dong va doc lap
    /// 1. Chu dong: phai dung o goc do Checker la nguoi chu dong dua ra API check cho nguoi khac su dung tu khi ho con chua co nhu cau. Viec check cai gi, y nghia ra sao can duoc phan tich suy nghi khong le thuoc vao tinh huong cu the de tranh API bi han che ve mat tam nhin.
    /// 2. Doc lap: 1 ham check chi xu ly 1 va chi dung 1 viec. Tuyet doi tranh ham check nay lai phai lo ho nhiem vu cua 1 ham check khac. Nhu y tren, viec cua Checker la dua ra ham check & viec cua nguoi su dung la phai hieu ro ve cac ham check de su dung ket hop chung theo dung thu tu. Tu duy nay neu duoc trien khai tot se giup cac ham check khong bi phu thuoc vao nhau, co duoc su don gian, de tai su dung.
    /// </summary>
    partial class HisTreatmentEndTypeCheck : BusinessBase
    {
        internal HisTreatmentEndTypeCheck()
            : base()
        {

        }

        internal HisTreatmentEndTypeCheck(CommonParam paramCheck)
            : base(paramCheck) 
        {

        }

        internal bool VerifyRequireField(HIS_TREATMENT_END_TYPE data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (String.IsNullOrWhiteSpace(data.TREATMENT_END_TYPE_CODE)) throw new ArgumentNullException("data.TREATMENT_END_TYPE_CODE");
                if (String.IsNullOrWhiteSpace(data.TREATMENT_END_TYPE_NAME)) throw new ArgumentNullException("data.TREATMENT_END_TYPE_NAME");
                if (!data.IS_FOR_IN_PATIENT.HasValue && !data.IS_FOR_OUT_PATIENT.HasValue) throw new ArgumentNullException("data.IS_FOR_IN_PATIENT && data.IS_FOR_OUT_PATIENT.HasValue");
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
                if (new HisTreatmentEndTypeGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_TREATMENT_END_TYPE data)
        {
            bool valid = true;
            try
            {
                data = new HisTreatmentEndTypeGet().GetById(id);
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
                    HisTreatmentEndTypeFilterQuery filter = new HisTreatmentEndTypeFilterQuery();
                    filter.IDs = listId;
                    List<HIS_TREATMENT_END_TYPE> listData = new HisTreatmentEndTypeGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_TREATMENT_END_TYPE> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisTreatmentEndTypeFilterQuery filter = new HisTreatmentEndTypeFilterQuery();
                    filter.IDs = listId;
                    List<HIS_TREATMENT_END_TYPE> listData = new HisTreatmentEndTypeGet().Get(filter);
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
        internal bool IsUnLock(HIS_TREATMENT_END_TYPE data)
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
                if (!DAOWorker.HisTreatmentEndTypeDAO.IsUnLock(id))
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
                    HisTreatmentEndTypeFilterQuery filter = new HisTreatmentEndTypeFilterQuery();
                    filter.IDs = listId;
                    List<HIS_TREATMENT_END_TYPE> listData = new HisTreatmentEndTypeGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_TREATMENT_END_TYPE> listData)
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
        
        /// <summary>
        /// Kiem tra ma da ton tai hay chua, id duoc su dung trong truong hop muon bo qua chinh ma cua minh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool ExistsCode(string code, long? id)
        {
            bool valid = true;
            try
            {
                if (DAOWorker.HisTreatmentEndTypeDAO.ExistsCode(code, id))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.MaDaTonTaiTrenHeThong, code);
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

        internal bool CheckConstraint(long id)
        {
            bool valid = true;
            try
            {
                List<HIS_TREATMENT> hisTreatments = new HisTreatmentGet().GetByTreatmentEndTypeId(id);
                if (IsNotNullOrEmpty(hisTreatments))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_TREATMENT, khong cho phep xoa" + LogUtil.TraceData("id", id));
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

        internal bool CheckConstraint(List<long> ids)
        {
            bool valid = true;
            try
            {
                foreach (long id in ids)
                {
                    valid = valid && CheckConstraint(id);
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

        internal bool CheckAllowUpdateOrDelete(HIS_TREATMENT_END_TYPE data)
        {
            bool valid = true;
            try
            {
                if (data.ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHET
                    || data.ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN
                    || data.ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CTCV
                    || data.ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN
                    || data.ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__KHAC
                    || data.ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__RAVIEN
                    || data.ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__TRON
                    || data.ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__XINRAVIEN)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Khong cho phep sua nhung loai ra vien mac dinh");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsAllowUpdate(HIS_TREATMENT_END_TYPE newData, HIS_TREATMENT_END_TYPE oldData)
        {
            bool valid = true;
            try
            {
                List<long> ids = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHET,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CTCV,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__KHAC,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__RAVIEN,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__TRON,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__XINRAVIEN
                };

                if (ids.Contains(newData.ID) &&
                    (newData.TREATMENT_END_TYPE_CODE != oldData.TREATMENT_END_TYPE_CODE || newData.TREATMENT_END_TYPE_NAME != oldData.TREATMENT_END_TYPE_NAME)
                   )
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatmentEndType_KhongChoPhepSuaMaTenLoaiRaVien);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckAllowUpdateOrDelete(List<HIS_TREATMENT_END_TYPE> listData)
        {
            bool valid = true;
            try
            {
                foreach (HIS_TREATMENT_END_TYPE data in listData)
                {
                    valid = valid && CheckAllowUpdateOrDelete(data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
