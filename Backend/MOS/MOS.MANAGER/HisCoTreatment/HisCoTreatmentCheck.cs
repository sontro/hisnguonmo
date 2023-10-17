using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCoTreatment
{
    partial class HisCoTreatmentCheck : BusinessBase
    {
        internal HisCoTreatmentCheck()
            : base()
        {

        }

        internal HisCoTreatmentCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_CO_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.DEPARTMENT_TRAN_ID <= 0) throw new ArgumentNullException("data.DEPARTMENT_TRAN_ID");
                if (data.DEPARTMENT_ID <= 0) throw new ArgumentNullException("data.DEPARTMENT_ID");
                if (data.TDL_TREATMENT_ID <= 0) throw new ArgumentNullException("data.TDL_TREATMENT_ID");
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
                if (new HisCoTreatmentGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_CO_TREATMENT data)
        {
            bool valid = true;
            try
            {
                data = new HisCoTreatmentGet().GetById(id);
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
                    HisCoTreatmentFilterQuery filter = new HisCoTreatmentFilterQuery();
                    filter.IDs = listId;
                    List<HIS_CO_TREATMENT> listData = new HisCoTreatmentGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_CO_TREATMENT> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisCoTreatmentFilterQuery filter = new HisCoTreatmentFilterQuery();
                    filter.IDs = listId;
                    List<HIS_CO_TREATMENT> listData = new HisCoTreatmentGet().Get(filter);
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
        internal bool IsUnLock(HIS_CO_TREATMENT data)
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
                if (!DAOWorker.HisCoTreatmentDAO.IsUnLock(id))
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
                    HisCoTreatmentFilterQuery filter = new HisCoTreatmentFilterQuery();
                    filter.IDs = listId;
                    List<HIS_CO_TREATMENT> listData = new HisCoTreatmentGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_CO_TREATMENT> listData)
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
                /*List<XXX> hisXXXs = new HisXXXGet().GetByHisCoTreatmentId(id);
                if (IsNotNullOrEmpty(hisXXXs))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisXXX_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu XXX, khong cho phep xoa" + LogUtil.TraceData("id", id));
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

        internal bool CheckValid(HisCoTreatmentSDO data, ref HIS_DEPARTMENT_TRAN previousDt)
        {
            bool result = true;
            try
            {
                List<HIS_DEPARTMENT_TRAN> listDt = new HisDepartmentTranGet().GetByTreatmentId(data.TreatmentId);
                if (!IsNotNullOrEmpty(listDt))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Khong lay duoc danh sach DepartmentTran theo treatmentId: " + data.TreatmentId);
                }
                HIS_DEPARTMENT_TRAN dtNotReceive = listDt.FirstOrDefault(o => !o.DEPARTMENT_IN_TIME.HasValue);
                if (dtNotReceive != null)
                {
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA != null ? HisDepartmentCFG.DATA.Where(o => o.ID == dtNotReceive.DEPARTMENT_ID).FirstOrDefault() : null;
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BenhNhanDangChoTiepNhanVaoKhoa, (department != null) ? department.DEPARTMENT_NAME : "");
                    return false;
                }

                previousDt = listDt.OrderByDescending(o => o.DEPARTMENT_IN_TIME).ThenByDescending(o => o.ID).FirstOrDefault();
                long lastDepartmentId = previousDt.DEPARTMENT_ID;
                HIS_DEPARTMENT previousDepart = HisDepartmentCFG.DATA != null ? HisDepartmentCFG.DATA.Where(o => o.ID == lastDepartmentId).FirstOrDefault() : null;
                string departmentName = previousDepart != null ? previousDepart.DEPARTMENT_NAME : "";

                WorkPlaceSDO sdo = TokenManager.GetWorkPlace(data.RequestRoomId);
                if (sdo == null || sdo.DepartmentId != previousDt.DEPARTMENT_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BenhNhanDangThuocKhoa, departmentName);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool CheckExists(HIS_CO_TREATMENT data)
        {
            bool result = true;
            try
            {
                HisCoTreatmentFilterQuery filter = new HisCoTreatmentFilterQuery();
                filter.DEPARTMENT_TRAN_ID = data.DEPARTMENT_TRAN_ID;
                filter.DEPARTMENT_ID = data.DEPARTMENT_ID;
                filter.HAS_FINISH_TIME = false;
                List<HIS_CO_TREATMENT> existsData = new HisCoTreatmentGet().Get(filter);
                if (IsNotNullOrEmpty(existsData))
                {
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == data.DEPARTMENT_ID).FirstOrDefault();
                    string departmentName = department != null ? department.DEPARTMENT_NAME : "";
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCoTreatment_BenhNhanDangDuocDieuTriKetHopTaiKhoa, departmentName);
                    result = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool CheckTime(HIS_CO_TREATMENT data)
        {
            bool result = true;
            try
            {
                HIS_DEPARTMENT_TRAN dt = null;
                if (data.START_TIME.HasValue)
                {
                    dt = new HisDepartmentTranGet().GetById(data.DEPARTMENT_TRAN_ID);
                    if (dt.DEPARTMENT_IN_TIME.HasValue && data.START_TIME.Value < dt.DEPARTMENT_IN_TIME.Value)
                    {
                        string inTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(dt.DEPARTMENT_IN_TIME.Value);
                        HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == dt.DEPARTMENT_ID).FirstOrDefault();
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCoTreatment_ThoiGianBatDauNhoHonThoiGianVaoKhoaChinh, department.DEPARTMENT_NAME, inTime);
                        return false;
                    }
                }

                if (data.FINISH_TIME.HasValue)
                {
                    if (dt == null) dt = new HisDepartmentTranGet().GetById(data.DEPARTMENT_TRAN_ID);
                    List<HIS_DEPARTMENT_TRAN> lstDt = new HisDepartmentTranGet().GetByPreviousId(data.DEPARTMENT_TRAN_ID);
                    HIS_DEPARTMENT_TRAN newDt = lstDt != null ? lstDt.FirstOrDefault() : null;
                    if (newDt != null && newDt.DEPARTMENT_IN_TIME.HasValue && data.FINISH_TIME.Value > newDt.DEPARTMENT_IN_TIME.Value)
                    {
                        string outTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(newDt.DEPARTMENT_IN_TIME.Value);
                        HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == dt.DEPARTMENT_ID).FirstOrDefault();
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCoTreatment_ThoiGianKetThucLonHonThoiGianRaKhoaChinh, department.DEPARTMENT_NAME, outTime);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
