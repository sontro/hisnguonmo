using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisService;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisCoTreatment;
using MOS.MANAGER.HisTreatment;

namespace MOS.MANAGER.HisDepartmentTran
{
    class HisDepartmentTranCheck : BusinessBase
    {
        internal HisDepartmentTranCheck()
            : base()
        {

        }

        internal HisDepartmentTranCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_DEPARTMENT_TRAN data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.DEPARTMENT_ID)) throw new ArgumentNullException("data.DEPARTMENT_ID");
                if (!IsGreaterThanZero(data.TREATMENT_ID)) throw new ArgumentNullException("data.TREATMENT_ID");
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

        internal bool ValidData(HIS_DEPARTMENT_TRAN data)
        {
            bool valid = true;
            try
            {

            }
            catch (ArgumentException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
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

        internal bool VerifyId(long id, ref HIS_DEPARTMENT_TRAN data)
        {
            bool valid = true;
            try
            {
                data = new HisDepartmentTranGet().GetById(id);
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
        /// Kiem tra su ton tai cua danh sach cac id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyIds(List<long> listId, List<HIS_DEPARTMENT_TRAN> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisDepartmentTranFilterQuery filter = new HisDepartmentTranFilterQuery();
                    filter.IDs = listId;
                    List<HIS_DEPARTMENT_TRAN> listData = new HisDepartmentTranGet().Get(filter);
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
        internal bool IsUnLock(HIS_DEPARTMENT_TRAN data)
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
                if (!DAOWorker.HisDepartmentTranDAO.IsUnLock(id))
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
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach data)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<HIS_DEPARTMENT_TRAN> listData)
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

        internal bool CheckInTime(HIS_DEPARTMENT_TRAN data)
        {
            bool result = false;
            try
            {
                if (data.PREVIOUS_ID.HasValue && data.DEPARTMENT_IN_TIME.HasValue)
                {
                    V_HIS_DEPARTMENT_TRAN previousDt = new HisDepartmentTranGet().GetViewById(data.PREVIOUS_ID.Value);
                    if (previousDt == null)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Du lieu dau vao khong hop le" + LogUtil.TraceData("PREVIOUS_ID", data.PREVIOUS_ID.Value));
                    }

                    if (previousDt.DEPARTMENT_IN_TIME.HasValue && previousDt.DEPARTMENT_IN_TIME.Value > data.DEPARTMENT_IN_TIME.Value)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_ThoiGianKhongDuocNhoThoiGianVaoKhoaTruoc, previousDt.DEPARTMENT_NAME, previousDt.DEPARTMENT_IN_TIME.Value + "");
                        return false;
                    }

                    List<HIS_CO_TREATMENT> hisCoTreatments = new HisCoTreatmentGet().GetByDepartmentTranId(previousDt.ID);
                    long? timeMax = hisCoTreatments != null ? hisCoTreatments.Max(s => s.FINISH_TIME) : null;
                    if (timeMax.HasValue && timeMax.Value > data.DEPARTMENT_IN_TIME.Value)
                    {
                        string finishTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(timeMax.Value);
                        HIS_DEPARTMENT previousDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == previousDt.DEPARTMENT_ID).FirstOrDefault();
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDepartmentTran_ThoiGianVaoKhoaNhoHonThoiGianKetThucDieuTriKeHopCuaKhoaTruoc, previousDepartment.DEPARTMENT_NAME, finishTime);
                        return false;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool CheckPatientTypeLogTime(HIS_DEPARTMENT_TRAN data)
        {
            bool result = false;
            try
            {
                if (!data.DEPARTMENT_IN_TIME.HasValue)
                {
                    return true;
                }
                List<HIS_PATIENT_TYPE_ALTER> patyAlters = new HisPatientTypeAlterGet().GetByDepartmentTranId(data.ID);
                if (IsNotNullOrEmpty(patyAlters))
                {
                    foreach (HIS_PATIENT_TYPE_ALTER item in patyAlters)
                    {
                        if (item.LOG_TIME < data.DEPARTMENT_IN_TIME.Value)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDepartmentTran_ThoiGianVaoKhoaLonHonThoiGianXacLapDoiTuongCuaKhoa
);
                            return false;
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool CheckCoTreatmentFinishTime(HIS_DEPARTMENT_TRAN data)
        {
            bool result = false;
            try
            {
                if (data.PREVIOUS_ID.HasValue && data.DEPARTMENT_IN_TIME.HasValue)
                {
                    List<HIS_CO_TREATMENT> hisCoTreatments = new HisCoTreatmentGet().GetByDepartmentTranId(data.PREVIOUS_ID.Value);
                    HIS_CO_TREATMENT coTreat = hisCoTreatments != null ? hisCoTreatments.OrderByDescending(o => o.FINISH_TIME).FirstOrDefault() : null;
                    if (coTreat != null && coTreat.FINISH_TIME.HasValue && coTreat.FINISH_TIME.Value > data.DEPARTMENT_IN_TIME.Value)
                    {
                        string finishTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(coTreat.FINISH_TIME.Value);
                        HIS_DEPARTMENT previousDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == coTreat.DEPARTMENT_ID).FirstOrDefault();
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDepartmentTran_ThoiGianVaoKhoaNhoHonThoiGianKetThucDieuTriKeHopCuaKhoaTruoc, previousDepartment.DEPARTMENT_NAME, finishTime);
                        return false;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool IsFinishCoTreatment(HIS_DEPARTMENT_TRAN data)
        {
            bool valid = true;
            try
            {
                if (data != null)
                {
                    List<HIS_CO_TREATMENT> hisCoTreatments = new HisCoTreatmentGet().GetByDepartmentTranId(data.ID);
                    hisCoTreatments = hisCoTreatments != null ? hisCoTreatments.Where(o => !o.FINISH_TIME.HasValue).ToList() : null;
                    if (IsNotNullOrEmpty(hisCoTreatments))
                    {
                        List<string> listDepart = HisDepartmentCFG.DATA.Where(o => hisCoTreatments.Exists(e => e.DEPARTMENT_ID == o.ID)).Select(s => s.DEPARTMENT_NAME).ToList();
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDepartmentTran_CacKhoaChuaKetThucDieuTriKetHop, String.Join(";", listDepart));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra xem co chi phi giuong tam tinh ko
        /// </summary>
        /// <param name="treatmentId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        internal bool HasNoTempBed(long treatmentId, long departmentId)
        {
            bool result = true;
            try
            {
                string sql = "SELECT S.TDL_SERVICE_NAME FROM HIS_SERE_SERV S WHERE S.AMOUNT_TEMP IS NOT NULL AND S.IS_DELETE = 0 AND S.SERVICE_REQ_ID IS NOT NULL AND S.TDL_TREATMENT_ID = :param1 AND S.TDL_REQUEST_DEPARTMENT_ID = :param2 AND S.TDL_SERVICE_TYPE_ID = :param3";
                List<string> bedNames = DAOWorker.SqlDAO.GetSql<string>(sql, treatmentId, departmentId, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G);
                if (IsNotNullOrEmpty(bedNames))
                {
                    string nameStr = string.Join(",", bedNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_TonTaiChiPhiGiuongTamTinh, nameStr);
                    return false;
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

        internal bool CheckConstraint(long id)
        {
            bool valid = true;
            try
            {
                List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByDepartmentTranId(id);
                if (IsNotNullOrEmpty(ptas))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_TonTaiDuLieu);
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

        internal bool IsValidWithReqDepartment(long departmentId, long treatmentId)
        {
            bool valid = true;
            try
            {
                List<V_HIS_SERVICE> blockServices = HisServiceCFG.DATA_VIEW.Where(o => o.IS_BLOCK_DEPARTMENT_TRAN == Constant.IS_TRUE).ToList();
                if (IsNotNullOrEmpty(blockServices))
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.REQUEST_DEPARTMENT_ID = departmentId;
                    filter.TREATMENT_ID = treatmentId;
                    filter.SERVICE_REQ_STT_IDs = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL };
                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);

                    if (IsNotNullOrEmpty(serviceReqs))
                    {
                        List<long> reqIds = serviceReqs.Select(o => o.ID).Distinct().ToList();
                        List<HIS_SERE_SERV> ss = new HisSereServGet().GetByServiceReqIds(reqIds);
                        if (IsNotNullOrEmpty(ss))
                        {
                            List<HIS_SERE_SERV> ssblock = ss.Where(o => blockServices.Exists(e => e.ID == o.SERVICE_ID)).ToList();
                            if (IsNotNullOrEmpty(ssblock))
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_DichVuChuaHoanThanhKhongChoPhepThucHienChuyenKhoa,
                                    string.Join(", ", ssblock.Select(o => o.TDL_SERVICE_NAME).Distinct().ToList()), string.Join(", ", ssblock.Select(o => o.TDL_SERVICE_REQ_CODE).Distinct().ToList()));
                                return false;
                            }
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
    }
}
