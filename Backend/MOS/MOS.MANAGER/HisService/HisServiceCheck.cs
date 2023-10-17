using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisService
{
    class HisServiceCheck : BusinessBase
    {
        internal HisServiceCheck()
            : base()
        {

        }

        internal HisServiceCheck(Inventec.Core.CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_SERVICE data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (String.IsNullOrEmpty(data.SERVICE_CODE)) throw new ArgumentNullException("Data.SERVICE_CODE");
                if (String.IsNullOrEmpty(data.SERVICE_NAME)) throw new ArgumentNullException("Data.SERVICE_NAME");
                if (!IsGreaterThanZero(data.SERVICE_TYPE_ID)) throw new ArgumentNullException("data.SERVICE_TYPE_ID");
                if (!IsGreaterThanZero(data.SERVICE_UNIT_ID)) throw new ArgumentNullException("data.SERVICE_UNIT_ID");
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

        /// <summary>
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_SERVICE data)
        {
            bool valid = true;
            try
            {
                data = new HisServiceGet().GetById(id);
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

        internal bool IsUnLock(HIS_SERVICE data)
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

        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisServiceDAO.IsUnLock(id))
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
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach data)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<HIS_SERVICE> listData)
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
        /// Kiem tra su ton tai cua danh sach cac id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyIds(List<long> listId, List<HIS_SERVICE> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisServiceFilterQuery filter = new HisServiceFilterQuery();
                    filter.IDs = listId;
                    List<HIS_SERVICE> listData = new HisServiceGet().Get(filter);
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

        internal bool CheckConstraint(long serviceId)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceId(serviceId);
                if (IsNotNullOrEmpty(hisSereServs))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_SERE_SERV, khong cho phep xoa" + LogUtil.TraceData("serviceId", serviceId));
                }
                List<HIS_SERVICE> hisServices = new HisServiceGet().GetByParentId(serviceId);
                if (IsNotNullOrEmpty(hisServices))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_SERVICE con, khong cho phep xoa" + LogUtil.TraceData("serviceId", serviceId));
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

        internal bool ExistsCode(string code, long? id)
        {
            bool valid = true;
            try
            {
                if (DAOWorker.HisServiceDAO.ExistsCode(code, id))
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

        internal bool IsValidData(HIS_SERVICE data)
        {
            try
            {
                List<long> typeIdNotPtttInfos = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__AN,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU
                };

                if ((data.HEIN_LIMIT_PRICE.HasValue || data.HEIN_LIMIT_PRICE_OLD.HasValue) && (data.HEIN_LIMIT_RATIO.HasValue || data.HEIN_LIMIT_RATIO_OLD.HasValue))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    return false;
                }
                if ((data.HEIN_LIMIT_PRICE_OLD.HasValue || data.HEIN_LIMIT_RATIO_OLD.HasValue) && !data.HEIN_LIMIT_PRICE_IN_TIME.HasValue && !data.HEIN_LIMIT_PRICE_INTR_TIME.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisService_ThieuThongTinThoiGianApDung);
                    return false;
                }
                if (data.HEIN_LIMIT_PRICE_IN_TIME.HasValue && data.HEIN_LIMIT_PRICE_INTR_TIME.HasValue)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    return false;
                }
                if (typeIdNotPtttInfos.Contains(data.SERVICE_TYPE_ID))
                {
                    if (data.PTTT_GROUP_ID.HasValue)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisService_LoaiDichVuKhongDuocPhepNhapThongTinNhomPTTT);
                        return false;
                    }
                    if (data.PTTT_METHOD_ID.HasValue)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisService_LoaiDichVuKhongDuocPhepNhapThongTinPhuongThucPTTT);
                        return false;
                    }
                    if (data.ICD_CM_ID.HasValue)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisService_LoaiDichVuKhongDuocPhepNhapThongTinICD);
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

        internal bool IsValidMinMaxServiceTime(List<HIS_SERE_SERV_EXT> ssExts, List<HIS_SERE_SERV> sereServs)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(ssExts))
                {
                    if (!IsNotNullOrEmpty(sereServs))
                        sereServs = new HisSereServGet().GetByIds(ssExts.Select(s => s.SERE_SERV_ID).ToList());
                    List<V_HIS_SERVICE> invalidMin = new List<V_HIS_SERVICE>();
                    List<V_HIS_SERVICE> invalidMax = new List<V_HIS_SERVICE>();

                    foreach (var ext in ssExts)
                    {
                        HIS_SERE_SERV s = sereServs.FirstOrDefault(o => o.ID == ext.SERE_SERV_ID);
                        if (!IsNotNull(s))
                        {
                            //bo qua khong xu ly HIS_SERE_SERV_EXT khi HIS_SERE_SERV bi xoa
                            continue;
                        }
                        V_HIS_SERVICE sv = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == s.SERVICE_ID);
                        if (sv != null && ext != null && ext.BEGIN_TIME.HasValue && ext.END_TIME.HasValue)
                        {
                            var arrMinProcessTimeExceptPatyIds = (sv.MIN_PROC_TIME_EXCEPT_PATY_IDS ?? "").Split(',');
                            List<long> minProcessTimeExceptPatyIds = arrMinProcessTimeExceptPatyIds != null ? arrMinProcessTimeExceptPatyIds.Select(o => Inventec.Common.TypeConvert.Parse.ToInt64(o)).ToList() : new List<long>();
                            var arrMaxProcessTimeExceptPatyIds = (sv.MAX_PROC_TIME_EXCEPT_PATY_IDS ?? "").Split(',');
                            List<long> maxProcessTimeExceptPatyIds = arrMaxProcessTimeExceptPatyIds != null ? arrMaxProcessTimeExceptPatyIds.Select(o => Inventec.Common.TypeConvert.Parse.ToInt64(o)).ToList() : new List<long>();

                            DateTime dtBegin = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ext.BEGIN_TIME.Value).Value;
                            DateTime dtEnd = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ext.END_TIME.Value).Value;
                            double minuteDistance = (dtEnd - dtBegin).TotalMinutes;

                            if (!minProcessTimeExceptPatyIds.Contains(s.PATIENT_TYPE_ID) && sv.MIN_PROCESS_TIME.HasValue && sv.MIN_PROCESS_TIME.Value > 0)
                            {
                                if (minuteDistance < sv.MIN_PROCESS_TIME.Value)
                                {
                                    invalidMin.Add(sv);
                                }
                            }
                            if (!maxProcessTimeExceptPatyIds.Contains(s.PATIENT_TYPE_ID) && sv.MAX_PROCESS_TIME.HasValue && sv.MAX_PROCESS_TIME.Value > 0)
                            {
                                if (minuteDistance > sv.MAX_PROCESS_TIME.Value)
                                {
                                    invalidMax.Add(sv);
                                }
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(invalidMin))
                    {
                        var group = invalidMin.Distinct().GroupBy(o => o.MIN_PROCESS_TIME);
                        foreach (var g in group)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisService_BenhNhanCoThoiGianThucHienDichVuNhoHonSoPhut, string.Join(", ", g.Select(o => o.SERVICE_NAME).ToList()), g.FirstOrDefault().MIN_PROCESS_TIME.Value.ToString());
                        }

                        valid = false;
                    }

                    if (IsNotNullOrEmpty(invalidMax))
                    {
                        var group = invalidMax.Distinct().GroupBy(o => o.MAX_PROCESS_TIME);
                        foreach (var g in group)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisService_BenhNhanCoThoiGianThucHienDichVuLonHonSoPhut, string.Join(", ", g.Select(o => o.SERVICE_NAME).ToList()), g.FirstOrDefault().MAX_PROCESS_TIME.Value.ToString());
                        }

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
    }
}
