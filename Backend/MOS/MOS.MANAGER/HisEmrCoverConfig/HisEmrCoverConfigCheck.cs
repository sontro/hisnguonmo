using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmrCoverType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmrCoverConfig
{
    partial class HisEmrCoverConfigCheck : BusinessBase
    {
        internal HisEmrCoverConfigCheck()
            : base()
        {

        }

        internal HisEmrCoverConfigCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_EMR_COVER_CONFIG data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!data.DEPARTMENT_ID.HasValue && !data.ROOM_ID.HasValue) throw new ArgumentNullException("data.DEPARTMENT_ID && data.ROOM_ID");
                if (data.TREATMENT_TYPE_ID <= 0) throw new ArgumentNullException("data.TREATMENT_TYPE_ID");
                if (data.EMR_COVER_TYPE_ID <= 0) throw new ArgumentNullException("data.EMR_COVER_TYPE_ID");
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
                if (new HisEmrCoverConfigGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_EMR_COVER_CONFIG data)
        {
            bool valid = true;
            try
            {
                data = new HisEmrCoverConfigGet().GetById(id);
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
                    HisEmrCoverConfigFilterQuery filter = new HisEmrCoverConfigFilterQuery();
                    filter.IDs = listId;
                    List<HIS_EMR_COVER_CONFIG> listData = new HisEmrCoverConfigGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_EMR_COVER_CONFIG> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisEmrCoverConfigFilterQuery filter = new HisEmrCoverConfigFilterQuery();
                    filter.IDs = listId;
                    List<HIS_EMR_COVER_CONFIG> listData = new HisEmrCoverConfigGet().Get(filter);
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
        internal bool IsUnLock(HIS_EMR_COVER_CONFIG data)
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
                if (!DAOWorker.HisEmrCoverConfigDAO.IsUnLock(id))
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
                    HisEmrCoverConfigFilterQuery filter = new HisEmrCoverConfigFilterQuery();
                    filter.IDs = listId;
                    List<HIS_EMR_COVER_CONFIG> listData = new HisEmrCoverConfigGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_EMR_COVER_CONFIG> listData)
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
                /*List<XXX> hisXXXs = new HisXXXGet().GetByHisEmrCoverConfigId(id);
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

        internal bool IsNotExists(HIS_EMR_COVER_CONFIG data)
        {
            bool valid = true;
            try
            {
                if (data.DEPARTMENT_ID.HasValue)
                {
                    List<HIS_EMR_COVER_CONFIG> exists = DAOWorker.SqlDAO.GetSql<HIS_EMR_COVER_CONFIG>("SELECT * FROM HIS_EMR_COVER_CONFIG WHERE DEPARTMENT_ID = :param1 AND TREATMENT_TYPE_ID = :param2 AND EMR_COVER_TYPE_ID = :param2 AND ID <> :param4", data.DEPARTMENT_ID.Value, data.TREATMENT_TYPE_ID, data.EMR_COVER_TYPE_ID, data.ID);
                    if (IsNotNullOrEmpty(exists))
                    {
                        HIS_DEPARTMENT dp = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == data.DEPARTMENT_ID.Value);
                        HIS_TREATMENT_TYPE tt = HisTreatmentTypeCFG.DATA.FirstOrDefault(o => o.ID == data.TREATMENT_TYPE_ID);
                        HIS_EMR_COVER_TYPE cover = new HisEmrCoverTypeGet().GetById(data.EMR_COVER_TYPE_ID);
                        string depaname = dp != null ? dp.DEPARTMENT_NAME : "";
                        string tt_name = tt != null ? tt.TREATMENT_TYPE_NAME : "";
                        string cover_name = cover != null ? cover.EMR_COVER_TYPE_NAME : "";
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisEmrCoverConfig_TonTaiDuLieuLoaiVBAVoiKhoaVaDienDieuTri, depaname, tt_name, cover_name);
                        return false;
                    }
                }

                if (data.ROOM_ID.HasValue)
                {
                    List<HIS_EMR_COVER_CONFIG> exists = DAOWorker.SqlDAO.GetSql<HIS_EMR_COVER_CONFIG>("SELECT * FROM HIS_EMR_COVER_CONFIG WHERE ROOM_ID = :param1 AND TREATMENT_TYPE_ID = :param2 AND EMR_COVER_TYPE_ID = :param3 AND ID <> :param4", data.ROOM_ID.Value, data.TREATMENT_TYPE_ID, data.EMR_COVER_TYPE_ID, data.ID);
                    if (IsNotNullOrEmpty(exists))
                    {
                        V_HIS_ROOM ro = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.ROOM_ID.Value);
                        HIS_TREATMENT_TYPE tt = HisTreatmentTypeCFG.DATA.FirstOrDefault(o => o.ID == data.TREATMENT_TYPE_ID);
                        HIS_EMR_COVER_TYPE cover = new HisEmrCoverTypeGet().GetById(data.EMR_COVER_TYPE_ID);
                        string room_name = ro != null ? ro.ROOM_NAME : "";
                        string tt_name = tt != null ? tt.TREATMENT_TYPE_NAME : "";
                        string cover_name = cover != null ? cover.EMR_COVER_TYPE_NAME : "";
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisEmrCoverConfig_TonTaiDuLieuLoaiVBAVoiPhongVaDienDieuTri, room_name, tt_name, cover_name);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
