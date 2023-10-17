using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepartment
{
    class HisDepartmentCheck : BusinessBase
    {
        internal HisDepartmentCheck()
            : base()
        {

        }

        internal HisDepartmentCheck(CommonParam paramCheck)
            : base(paramCheck) 
        {

        }

        internal bool VerifyRequireField(HIS_DEPARTMENT data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.DEPARTMENT_CODE)) throw new ArgumentNullException("data.DEPARTMENT_CODE");
                if (!IsNotNullOrEmpty(data.DEPARTMENT_NAME)) throw new ArgumentNullException("data.DEPARTMENT_NAME");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool ExistsCode(string code, long? id)
        {
            bool valid = true;
            try
            {
                if (DAOWorker.HisDepartmentDAO.ExistsCode(code, id))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.MaDaTonTaiTrenHeThong, code);
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

        internal bool IsUnLock(HIS_DEPARTMENT data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                {
                    valid = false;
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
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
        
        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisDepartmentDAO.IsUnLock(id))
                {
                    valid = false;
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
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
                if (new HisDepartmentGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_DEPARTMENT data)
        {
            bool valid = true;
            try
            {
                data = new HisDepartmentGet().GetById(id);
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
                    HisDepartmentFilterQuery filter = new HisDepartmentFilterQuery();
                    filter.IDs = listId;
                    List<HIS_DEPARTMENT> listData = new HisDepartmentGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_DEPARTMENT> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisDepartmentFilterQuery filter = new HisDepartmentFilterQuery();
                    filter.IDs = listId;
                    List<HIS_DEPARTMENT> listData = new HisDepartmentGet().Get(filter);
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

        internal bool CheckConstraint(HIS_DEPARTMENT data)
        {
            bool valid = true;
            try
            {
                List<HIS_ROOM> rooms = new HisRoomGet().GetByDepartmentId(data.ID);
                if (IsNotNullOrEmpty(rooms))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRoom_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_ROOM, khong cho phep xoa" + LogUtil.TraceData("data", data));
                }

                List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().GetByDepartmentId(data.ID);
                if (IsNotNullOrEmpty(serviceReqs))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_SERVICE_REQ, khong cho phep xoa" + LogUtil.TraceData("data", data));
                }

                List<HIS_EXP_MEST> expMests = new HisExpMestGet().GetByReqDepartmentId(data.ID);
                if (IsNotNullOrEmpty(expMests))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_EXP_MEST, khong cho phep xoa" + LogUtil.TraceData("data", data));
                }

                List<HIS_DEPARTMENT_TRAN> departmentTrans = new HisDepartmentTranGet().GetByDepartmentId(data.ID);
                if (IsNotNullOrEmpty(departmentTrans))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_DEPARTMENT_TRAN, khong cho phep xoa" + LogUtil.TraceData("data", data));
                }

                List<HIS_IMP_MEST> impMests = new HisImpMestGet().GetByReqDepartmentId(data.ID);
                if (IsNotNullOrEmpty(impMests))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_IMP_MEST, khong cho phep xoa" + LogUtil.TraceData("data", data));
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

        internal bool IsNotUpdatedBranch(HIS_DEPARTMENT data, HIS_DEPARTMENT raw)
        {
            bool valid = true;
            try
            {
                if (data.BRANCH_ID != raw.BRANCH_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartment_KhongChoPhepThayDoiChiNhanh);
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
    }
}
