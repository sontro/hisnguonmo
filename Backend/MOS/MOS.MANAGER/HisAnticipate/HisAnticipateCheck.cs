using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipate
{
    /// <summary>
    /// Nguyen tac xay dung lop Checker: chu dong va doc lap
    /// 1. Chu dong: phai dung o goc do Checker la nguoi chu dong dua ra API check cho nguoi khac su dung tu khi ho con chua co nhu cau. Viec check cai gi, y nghia ra sao can duoc phan tich suy nghi khong le thuoc vao tinh huong cu the de tranh API bi han che ve mat tam nhin.
    /// 2. Doc lap: 1 ham check chi xu ly 1 va chi dung 1 viec. Tuyet doi tranh ham check nay lai phai lo ho nhiem vu cua 1 ham check khac. Nhu y tren, viec cua Checker la dua ra ham check & viec cua nguoi su dung la phai hieu ro ve cac ham check de su dung ket hop chung theo dung thu tu. Tu duy nay neu duoc trien khai tot se giup cac ham check khong bi phu thuoc vao nhau, co duoc su don gian, de tai su dung.
    /// </summary>
    partial class HisAnticipateCheck : BusinessBase
    {
        internal HisAnticipateCheck()
            : base()
        {

        }

        internal HisAnticipateCheck(CommonParam paramCheck)
            : base(paramCheck) 
        {

        }

        internal bool VerifyRequireField(HIS_ANTICIPATE data)
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
                if (new HisAnticipateGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_ANTICIPATE data)
        {
            bool valid = true;
            try
            {
                data = new HisAnticipateGet().GetById(id);
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
                    HisAnticipateFilterQuery filter = new HisAnticipateFilterQuery();
                    filter.IDs = listId;
                    List<HIS_ANTICIPATE> listData = new HisAnticipateGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_ANTICIPATE> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisAnticipateFilterQuery filter = new HisAnticipateFilterQuery();
                    filter.IDs = listId;
                    List<HIS_ANTICIPATE> listData = new HisAnticipateGet().Get(filter);
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
        internal bool IsUnLock(HIS_ANTICIPATE data)
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
                if (!DAOWorker.HisAnticipateDAO.IsUnLock(id))
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
                    HisAnticipateFilterQuery filter = new HisAnticipateFilterQuery();
                    filter.IDs = listId;
                    List<HIS_ANTICIPATE> listData = new HisAnticipateGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_ANTICIPATE> listData)
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

        internal bool VerifyValidData(HIS_ANTICIPATE data)
        {
            bool valid = true;
            try
            {
                List<HIS_ANTICIPATE_MATY> hisAnticipateMaties = data.HIS_ANTICIPATE_MATY != null ? data.HIS_ANTICIPATE_MATY.ToList() : null;
                List<HIS_ANTICIPATE_METY> hisAnticipateMeties = data.HIS_ANTICIPATE_METY != null ? data.HIS_ANTICIPATE_METY.ToList() : null;
                List<HIS_ANTICIPATE_BLTY> hisAnticipateBlties = data.HIS_ANTICIPATE_BLTY != null ? data.HIS_ANTICIPATE_BLTY.ToList() : null;
                if (!IsNotNullOrEmpty(hisAnticipateMaties) && !IsNotNullOrEmpty(hisAnticipateMeties) && !IsNotNullOrEmpty(hisAnticipateBlties))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("data.HIS_ANTICIPATE_MATY == null, data.HIS_ANTICIPATE_BLTY == null va data.HIS_ANTICIPATE_METY == null");
                }
                if (IsNotNullOrEmpty(hisAnticipateMaties))
                {
                    var maties = hisAnticipateMaties.Select(o => new
                    {
                        o.MATERIAL_TYPE_ID,
                        o.SUPPLIER_ID,
                        o.BID_ID
                    }).Distinct().ToList();

                    if (maties.Count != hisAnticipateMaties.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("du lieu trung MATERIAL_TYPE_ID");
                    }
                }
                if (IsNotNullOrEmpty(hisAnticipateMeties))
                {
                    var meties = hisAnticipateMeties.Select(o => new
                    {
                        o.MEDICINE_TYPE_ID,
                        o.SUPPLIER_ID,
                        o.BID_ID
                    }).Distinct().ToList();
                    if (meties.Count != hisAnticipateMeties.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("du lieu trung MEDICINE_TYPE_ID");
                    }
                }

                if (IsNotNullOrEmpty(hisAnticipateBlties))
                {
                    var bloodTypes = hisAnticipateBlties.Select(o => new
                    {
                        o.BLOOD_TYPE_ID,
                        o.SUPPLIER_ID
                    }).Distinct().ToList();
                    if (bloodTypes.Count != hisAnticipateBlties.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("du lieu trung BLOOD_TYPE_ID");
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
