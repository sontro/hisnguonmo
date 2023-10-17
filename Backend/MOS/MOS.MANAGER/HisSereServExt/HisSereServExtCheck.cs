using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisService;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.MANAGER.HisEkipUser;
using System.Text;
using MOS.MANAGER.HisServiceReq;

namespace MOS.MANAGER.HisSereServExt
{
    partial class HisSereServExtCheck : BusinessBase
    {
        internal HisSereServExtCheck()
            : base()
        {

        }

        internal HisSereServExtCheck(CommonParam paramCheck)
            : base(paramCheck) 
        {

        }

        internal bool VerifyRequireField(HIS_SERE_SERV_EXT data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.SERE_SERV_ID <= 0) throw new ArgumentNullException("data.SERE_SERV_ID");
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

        internal bool IsNotAprovedSurgeryRemuneration(HIS_SERE_SERV_EXT ext)
        {
            try
            {
                if (ext != null && (ext.IS_GATHER_DATA == Constant.IS_TRUE || ext.IS_FEE == Constant.IS_TRUE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_SoLieuCongThucHienPtttDaDuocChot, "");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return false;
        }

        internal bool IsValidLoginName(HisSereServExtSDO data, HIS_SERE_SERV sereServ)
        {
            bool valid = true;
            try
            {
                if (data != null && data.HisSereServExt.BEGIN_TIME.HasValue && data.HisSereServExt.END_TIME.HasValue)
                {
                    List<string> loginnames = new List<string>(); //danh sach tai khoan xu tri dich vu A
                    if (sereServ != null)
                    {
                        //List<HIS_EKIP_USER> exists = data.HisEkipUsers.Where(o => sereServ.EKIP_ID.HasValue && o.EKIP_ID == sereServ.EKIP_ID.Value).ToList();
                        if (IsNotNullOrEmpty(data.HisEkipUsers))
                        {
                            List<HIS_EXECUTE_ROLE> roles = HisExecuteRoleCFG.DATA.Where(o => o.ALLOW_SIMULTANEITY != Constant.IS_TRUE).ToList();
                            List<long> executeRoleIds = roles.Select(o => o.ID).ToList();
                            List<HIS_EKIP_USER> ekipUsers = data.HisEkipUsers.Where(o =>executeRoleIds.Contains(o.EXECUTE_ROLE_ID)).ToList();

                            loginnames = ekipUsers.Select(o => o.LOGINNAME).ToList();
                        }
                        else
                        {
                            string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                            if (!string.IsNullOrWhiteSpace(loginname))
                            {
                                loginnames.Add(loginname);
                            }
                        }
                        if (IsNotNullOrEmpty(loginnames))
                        {
                            List<HIS_EMPLOYEE> employees = HisEmployeeCFG.DATA.Where(o => loginnames.Contains(o.LOGINNAME) && o.DO_NOT_ALLOW_SIMULTANEITY == Constant.IS_TRUE).ToList();
                            if (IsNotNullOrEmpty(employees))
                            {
                                List<string> listLoginnames = employees.Select(o => o.LOGINNAME).ToList();
                                if (IsNotNullOrEmpty(listLoginnames))
                                {
                                    List<long> ServiceTypeIds = new List<long>(){
                                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__CDHA,
                                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TDCN,
                                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__NS,
                                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__SA,
                                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__GPBL
                                    };
                                    List<object> listParam = new List<object>();
                                    long? beginTime = data.HisSereServExt.BEGIN_TIME;
                                    long? endTime = data.HisSereServExt.END_TIME;

                                    List<string> sqlLoginnames = new List<string>();
                                    if (IsNotNullOrEmpty(listLoginnames))
                                    {
                                        sqlLoginnames.AddRange(listLoginnames);
                                    }
                                    for (int i = 0; i < sqlLoginnames.Count; i++)
                                    {
                                        sqlLoginnames[i] = "'" + sqlLoginnames[i] + "'";
                                    }
                                    StringBuilder sbSql = new StringBuilder("SELECT * FROM HIS_SERE_SERV_EXT EXT ");
                                    sbSql.Append("WHERE EXISTS (SELECT 1 FROM HIS_SERE_SERV SS WHERE SS.ID = EXT.SERE_SERV_ID ");
                                    sbSql.AppendFormat("AND SS.TDL_SERVICE_TYPE_ID IN ({0}) ", string.Join(", ", ServiceTypeIds));
                                    sbSql.Append("AND SS.IS_NO_EXECUTE IS NULL ");
                                    sbSql.Append("AND SS.ID <> :param1 ");
                                    listParam.Add(sereServ.ID);
                                    sbSql.Append("AND (EXISTS(SELECT 1 FROM HIS_SERVICE_REQ SR WHERE SR.ID = SS.SERVICE_REQ_ID");
                                    sbSql.AppendFormat(" AND SR.EXECUTE_LOGINNAME IN ({0}))", string.Join(", ", sqlLoginnames));
                                    sbSql.Append(" OR EXISTS(SELECT 1 FROM HIS_EKIP_USER EK WHERE EK.EKIP_ID = SS.EKIP_ID");
                                    sbSql.AppendFormat(" AND EK.LOGINNAME IN ({0})))", string.Join(", ", sqlLoginnames));
                                    sbSql.Append(") ");
                                    sbSql.Append("AND ((:param2 BETWEEN EXT.BEGIN_TIME AND EXT.END_TIME)");
                                    listParam.Add(beginTime ?? 0);
                                    sbSql.Append(" OR (:param3 BETWEEN EXT.BEGIN_TIME AND EXT.END_TIME))");
                                    listParam.Add(endTime ?? 0);
                                    LogSystem.Debug("IsValidLoginName: " + sbSql.ToString());
                                    List<HIS_SERE_SERV_EXT> ssExts = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV_EXT>(sbSql.ToString(), listParam.ToArray());
                                    if (IsNotNullOrEmpty(ssExts))
                                    {
                                        List<HIS_SERE_SERV> ss = new HisSereServGet().GetByIds(ssExts.Select(o => o.SERE_SERV_ID).ToList());
                                        List<HIS_SERVICE_REQ> reqs = new HisServiceReqGet().GetByIds(ss.Select(o => o.SERVICE_REQ_ID.Value).ToList());

                                        List<string> reqCodes = IsNotNullOrEmpty(reqs) ? reqs.Select(o => o.SERVICE_REQ_CODE).Distinct().ToList() : null;
                                        List<string> serviceNames = IsNotNullOrEmpty(ss) ? ss.Select(o => o.TDL_SERVICE_NAME).Distinct().ToList() : null;

                                        string reqInfo = string.Format("{0} ({1}: {2})", string.Join(",", serviceNames), MessageUtil.GetMessage(LibraryMessage.Message.Enum.Common_MaYLenh, param.LanguageCode), string.Join(",", reqCodes));

                                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServExt_TaiKhoanCoThucHienDvTrongKhoangTGGiaoVoiKhoangTGThucHienDvKhac,String.Join(", ", listLoginnames), reqInfo, sereServ.TDL_SERVICE_NAME);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
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
                if (new HisSereServExtGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_SERE_SERV_EXT data)
        {
            bool valid = true;
            try
            {
                data = new HisSereServExtGet().GetById(id);
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
                    HisSereServExtFilterQuery filter = new HisSereServExtFilterQuery();
                    filter.IDs = listId;
                    List<HIS_SERE_SERV_EXT> listData = new HisSereServExtGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_SERE_SERV_EXT> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisSereServExtFilterQuery filter = new HisSereServExtFilterQuery();
                    filter.IDs = listId;
                    List<HIS_SERE_SERV_EXT> listData = new HisSereServExtGet().Get(filter);
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
        internal bool IsUnLock(HIS_SERE_SERV_EXT data)
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
                if (!DAOWorker.HisSereServExtDAO.IsUnLock(id))
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
                    HisSereServExtFilterQuery filter = new HisSereServExtFilterQuery();
                    filter.IDs = listId;
                    List<HIS_SERE_SERV_EXT> listData = new HisSereServExtGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_SERE_SERV_EXT> listData)
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
                /*List<XXX> hisXXXs = new HisXXXGet().GetByHisSereServExtId(id);
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

        internal bool IsValidMinAndMaxProcessTime(HIS_SERE_SERV_EXT sereServExt)
        {
            bool valid = true;
            try
            {
                if (sereServExt.BEGIN_TIME.HasValue && sereServExt.END_TIME.HasValue)
                {
                    DateTime? beginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sereServExt.BEGIN_TIME.Value);
                    DateTime? endTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sereServExt.END_TIME.Value);
                    if (beginTime.HasValue && endTime.HasValue)
                    {
                        double minutes = (endTime - beginTime).Value.TotalMinutes;
                        var sereServ = new HisSereServGet().GetById(sereServExt.SERE_SERV_ID);
                        var service = IsNotNull(sereServ) ? HisServiceCFG.DATA_VIEW.Where(o => o.ID == sereServ.SERVICE_ID).FirstOrDefault() : null;
                        var minProcessTime = IsNotNull(service) && service.MIN_PROCESS_TIME.HasValue ? service.MIN_PROCESS_TIME.Value : 0;
                        var maxProcessTime = IsNotNull(service) && service.MAX_PROCESS_TIME.HasValue ? service.MAX_PROCESS_TIME.Value : 0;
                        // Truong hop nguoi dung nhap thoi gian bat dau va ket thuc nho hon min thoi gian trong service thi chan va thong bao

                        string patientType = sereServ.PATIENT_TYPE_ID.ToString();

                        List<string> listPatientTypeNotCheckMin = IsNotNull(service) && !string.IsNullOrWhiteSpace(service.MIN_PROC_TIME_EXCEPT_PATY_IDS)
                             ? service.MIN_PROC_TIME_EXCEPT_PATY_IDS.Split(',').ToList() : null;
                        bool isNotCheckMinTime = IsNotNullOrEmpty(listPatientTypeNotCheckMin) ? listPatientTypeNotCheckMin.Contains(patientType) : false;

                        List<string> listPatientTypeNotCheckMax = IsNotNull(service) && !string.IsNullOrWhiteSpace(service.MAX_PROC_TIME_EXCEPT_PATY_IDS)
                             ? service.MAX_PROC_TIME_EXCEPT_PATY_IDS.Split(',').ToList() : null;
                        bool isNotCheckMaxTime = IsNotNullOrEmpty(listPatientTypeNotCheckMax) ? listPatientTypeNotCheckMax.Contains(patientType) : false;

                        if (!isNotCheckMinTime && minProcessTime > 0 && minutes < minProcessTime)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServExt_BenhNhanCoThoiGianCLSItHonSoPhut, minProcessTime.ToString());
                            return false;
                        }

                        // Truong hop nguoi dung nhap thoi gian bat dau va ket thuc nho hon max thoi gian trong service thi chan va thong bao
                        if (!isNotCheckMaxTime && maxProcessTime > 0 && minutes > maxProcessTime)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServExt_BenhNhanCoThoiGianCLSLonHonSoPhut, maxProcessTime.ToString());
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

        internal bool IsValidMinProcessTime(HIS_SERE_SERV_EXT sereServExt)
        {
            bool valid = true;
            try
            {
                if (sereServExt.BEGIN_TIME.HasValue && sereServExt.END_TIME.HasValue)
                {
                    DateTime? beginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sereServExt.BEGIN_TIME.Value);
                    DateTime? endTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sereServExt.END_TIME.Value);
                    if (beginTime.HasValue && endTime.HasValue)
                    {
                        double minutes = (endTime - beginTime).Value.TotalMinutes;
                        var sereServs = new HisSereServGet().GetById(sereServExt.SERE_SERV_ID);
                        var service = IsNotNull(sereServs) ? new HisServiceGet().GetById(sereServs.SERVICE_ID) : null;
                        var minProcessTime = IsNotNull(service) && service.MIN_PROCESS_TIME.HasValue ? service.MIN_PROCESS_TIME.Value : 0;
                        // Truong hop nguoi dung nhap thoi gian bat dau va ket thuc nho hon thoi gian trong service thi chan va thong bao
                        if (minProcessTime > 0 && minutes < minProcessTime)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServExt_BenhNhanCoThoiGianCLSItHonSoPhut, minProcessTime.ToString());
                            valid = false;
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
