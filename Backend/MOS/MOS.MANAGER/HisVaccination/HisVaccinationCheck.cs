using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccination
{
    partial class HisVaccinationCheck : BusinessBase
    {
        internal HisVaccinationCheck()
            : base()
        {

        }

        internal HisVaccinationCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_VACCINATION data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.BRANCH_ID <= 0) throw new ArgumentNullException("data.BRANCH_ID");
                if (data.EXECUTE_DEPARTMENT_ID <= 0) throw new ArgumentNullException("data.EXECUTE_DEPARTMENT_ID");
                if (data.EXECUTE_ROOM_ID <= 0) throw new ArgumentNullException("data.EXECUTE_ROOM_ID");
                if (data.PATIENT_ID <= 0) throw new ArgumentNullException("data.PATIENT_ID");
                if (data.PATIENT_TYPE_ID <= 0) throw new ArgumentNullException("data.PATIENT_TYPE_ID");
                if (data.REQUEST_DEPARTMENT_ID <= 0) throw new ArgumentNullException("data.REQUEST_DEPARTMENT_ID");
                if (string.IsNullOrWhiteSpace(data.REQUEST_LOGINNAME)) throw new ArgumentNullException("data.REQUEST_LOGINNAME");
                if (data.REQUEST_ROOM_ID <= 0) throw new ArgumentNullException("data.REQUEST_ROOM_ID");
                if (data.REQUEST_TIME <= 0) throw new ArgumentNullException("data.REQUEST_TIME");
                if (string.IsNullOrWhiteSpace(data.REQUEST_USERNAME)) throw new ArgumentNullException("data.REQUEST_USERNAME");
                if (string.IsNullOrWhiteSpace(data.TDL_PATIENT_CODE)) throw new ArgumentNullException("data.TDL_PATIENT_CODE");
                if (data.TDL_PATIENT_DOB <= 0) throw new ArgumentNullException("data.TDL_PATIENT_DOB");
                if (string.IsNullOrWhiteSpace(data.TDL_PATIENT_FIRST_NAME)) throw new ArgumentNullException("data.TDL_PATIENT_FIRST_NAME");
                if (data.TDL_PATIENT_GENDER_ID <= 0) throw new ArgumentNullException("data.TDL_PATIENT_GENDER_ID");
                if (string.IsNullOrWhiteSpace(data.TDL_PATIENT_GENDER_NAME)) throw new ArgumentNullException("data.TDL_PATIENT_GENDER_NAME");
                if (string.IsNullOrWhiteSpace(data.TDL_PATIENT_NAME)) throw new ArgumentNullException("data.TDL_PATIENT_NAME");
                if (data.VACCINATION_EXAM_ID <= 0) throw new ArgumentNullException("data.VACCINATION_EXAM_ID");
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
                if (new HisVaccinationGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_VACCINATION data)
        {
            bool valid = true;
            try
            {
                data = new HisVaccinationGet().GetById(id);
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
                    HisVaccinationFilterQuery filter = new HisVaccinationFilterQuery();
                    filter.IDs = listId;
                    List<HIS_VACCINATION> listData = new HisVaccinationGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_VACCINATION> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisVaccinationFilterQuery filter = new HisVaccinationFilterQuery();
                    filter.IDs = listId;
                    List<HIS_VACCINATION> listData = new HisVaccinationGet().Get(filter);
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
        internal bool IsUnLock(HIS_VACCINATION data)
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
                if (!DAOWorker.HisVaccinationDAO.IsUnLock(id))
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
                    HisVaccinationFilterQuery filter = new HisVaccinationFilterQuery();
                    filter.IDs = listId;
                    List<HIS_VACCINATION> listData = new HisVaccinationGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_VACCINATION> listData)
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

        internal bool IsNotFinish(HIS_VACCINATION vaccination)
        {
            bool valid = true;
            try
            {
                if (vaccination.VACCINATION_STT_ID == IMSys.DbConfig.HIS_RS.HIS_VACCINATION_STT.ID__FINISH)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisVaccination_YeuCauTiemDaHoanThanh);
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

        internal bool IsNotFinishExpMest(HIS_VACCINATION vaccination, ref HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                var exp = new HisExpMestGet().GetByVaccinationId(vaccination.ID);
                if (exp.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST
                    && exp.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DRAFT)
                {
                    string sttname = Config.HisExpMestSttCFG.DATA.FirstOrDefault(o => o.ID == exp.EXP_MEST_STT_ID).EXP_MEST_STT_NAME;

                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDangOTrangThai, sttname);
                    valid = false;
                }
                expMest = exp;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsStatusNew(HIS_VACCINATION vaccination)
        {
            bool valid = true;
            try
            {
                if (vaccination.VACCINATION_STT_ID == IMSys.DbConfig.HIS_RS.HIS_VACCINATION_STT.ID__PROCESSING)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisVaccination_YeuCauTiemDangXuLy);
                    return false;
                }
                else if (vaccination.VACCINATION_STT_ID == IMSys.DbConfig.HIS_RS.HIS_VACCINATION_STT.ID__FINISH)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisVaccination_YeuCauTiemDaHoanThanh);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidExpMestForDelete(HIS_VACCINATION data, ref HIS_EXP_MEST expMest)
        {
            try
            {
                expMest = new HisExpMestGet().GetByVaccinationId(data.ID);
                if (expMest != null)
                {
                    if (expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_PhieuXuatVaccineDaThucXuatKhongChoPhepXoa);
                        return false;
                    }

                    if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT && expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_PhieuXuatVaccineDaDuyetKhongChoPhepXoa);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool CheckConstraint(long id)
        {
            bool valid = true;
            try
            {
                /*List<XXX> hisXXXs = new HisXXXGet().GetByHisVaccinationId(id);
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

        internal bool HasNotBill(HIS_VACCINATION data)
        {
            bool valid = true;
            try
            {
                return this.HasNotBill(new List<HIS_VACCINATION>() { data });
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasNotBill(List<HIS_VACCINATION> datas)
        {
            bool valid = true;
            try
            {
                var hasBills = datas != null ? datas.Where(o => o.BILL_ID.HasValue).ToList() : null;
                if (IsNotNullOrEmpty(hasBills))
                {
                    string codes = String.Join(",", hasBills.Select(s => s.VACCINATION_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisVaccination_CacMaYeuCauTiemSauDaThanhToan, codes);
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
