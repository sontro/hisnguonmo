using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPatient;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHoldReturn
{
    partial class HisHoldReturnCheck : BusinessBase
    {
        internal HisHoldReturnCheck()
            : base()
        {

        }

        internal HisHoldReturnCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_HOLD_RETURN data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.PATIENT_ID <= 0) throw new ArgumentNullException("data.PATIENT_ID");
                if (data.HOLD_ROOM_ID <= 0) throw new ArgumentNullException("data.HOLD_ROOM_ID");
                if (data.RESPONSIBLE_ROOM_ID <= 0) throw new ArgumentNullException("data.RESPONSIBLE_ROOM_ID");
                if (data.HOLD_TIME <= 0) throw new ArgumentNullException("data.HOLD_TIME");
                if (String.IsNullOrWhiteSpace(data.HOLD_LOGINNAME)) throw new ArgumentNullException("data.HOLD_LOGINNAME");
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
                if (new HisHoldReturnGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_HOLD_RETURN data)
        {
            bool valid = true;
            try
            {
                data = new HisHoldReturnGet().GetById(id);
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
                    HisHoldReturnFilterQuery filter = new HisHoldReturnFilterQuery();
                    filter.IDs = listId;
                    List<HIS_HOLD_RETURN> listData = new HisHoldReturnGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_HOLD_RETURN> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisHoldReturnFilterQuery filter = new HisHoldReturnFilterQuery();
                    filter.IDs = listId;
                    List<HIS_HOLD_RETURN> listData = new HisHoldReturnGet().Get(filter);
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
        internal bool IsUnLock(HIS_HOLD_RETURN data)
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
                if (!DAOWorker.HisHoldReturnDAO.IsUnLock(id))
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
                    HisHoldReturnFilterQuery filter = new HisHoldReturnFilterQuery();
                    filter.IDs = listId;
                    List<HIS_HOLD_RETURN> listData = new HisHoldReturnGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_HOLD_RETURN> listData)
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
                /*List<XXX> hisXXXs = new HisXXXGet().GetByHisHoldReturnId(id);
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

        //internal bool IsHandovering(HIS_HOLD_RETURN raw)
        //{
        //    bool valid = true;
        //    try
        //    {
        //        valid = this.IsHandover(new List<HIS_HOLD_RETURN>() { raw });
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        valid = false;
        //    }
        //    return valid;
        //}

        //internal bool IsHandovering(List<HIS_HOLD_RETURN> listRaw)
        //{
        //    bool valid = true;
        //    try
        //    {
        //        List<HIS_HOLD_RETURN> handovers = listRaw != null ? listRaw.Where(o => o.IS_HANDOVERING.HasValue && o.IS_HANDOVERING.Value == Constant.IS_TRUE).ToList() : null;
        //        if (IsNotNullOrEmpty(handovers))
        //        {
        //            List<long> patientIds = handovers.Select(s => s.PATIENT_ID).Distinct().ToList();
        //            List<HIS_PATIENT> patients = new HisPatientGet().GetByIds(patientIds);
        //            string codes = String.Join(",", patients.Select(s => s.PATIENT_CODE).ToList());
        //            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisHoldReturn_PhieuGiuTraChuaDuocTiepNhan, codes);
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        valid = false;
        //    }
        //    return valid;
        //}

        internal bool IsNotHandovering(HIS_HOLD_RETURN raw)
        {
            bool valid = true;
            try
            {
                valid = this.IsNotHandovering(new List<HIS_HOLD_RETURN>() { raw });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotHandovering(List<HIS_HOLD_RETURN> listRaw)
        {
            bool valid = true;
            try
            {
                List<HIS_HOLD_RETURN> notHandovers = listRaw != null ? listRaw.Where(o => o.IS_HANDOVERING.HasValue && o.IS_HANDOVERING.Value == Constant.IS_TRUE).ToList() : null;
                if (IsNotNullOrEmpty(notHandovers))
                {
                    List<long> patientIds = notHandovers.Select(s => s.PATIENT_ID).Distinct().ToList();
                    List<HIS_PATIENT> patients = new HisPatientGet().GetByIds(patientIds);
                    string codes = String.Join(",", patients.Select(s => s.PATIENT_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisHoldReturn_PhieuGiuTraChuaDuocTiepNhan, codes);
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

        internal bool IsReturn(HIS_HOLD_RETURN raw)
        {
            bool valid = true;
            try
            {
                valid = this.IsReturn(new List<HIS_HOLD_RETURN>() { raw });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsReturn(List<HIS_HOLD_RETURN> listRaw)
        {
            bool valid = true;
            try
            {
                List<HIS_HOLD_RETURN> notReturns = listRaw != null ? listRaw.Where(o => !o.RETURN_TIME.HasValue).ToList() : null;
                if (IsNotNullOrEmpty(notReturns))
                {
                    List<long> patientIds = notReturns.Select(s => s.PATIENT_ID).Distinct().ToList();
                    List<HIS_PATIENT> patients = new HisPatientGet().GetByIds(patientIds);
                    string codes = String.Join(",", patients.Select(s => s.PATIENT_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisHoldReturn_PhieuGiuTraChuaTra, codes);
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

        internal bool IsNotReturn(HIS_HOLD_RETURN raw)
        {
            bool valid = true;
            try
            {
                valid = IsNotReturn(new List<HIS_HOLD_RETURN>() { raw });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotReturn(List<HIS_HOLD_RETURN> listRaw)
        {
            bool valid = true;
            try
            {
                List<HIS_HOLD_RETURN> returneds = listRaw != null ? listRaw.Where(o => o.RETURN_TIME.HasValue).ToList() : null;
                if (IsNotNullOrEmpty(returneds))
                {
                    List<long> patientIds = returneds.Select(s => s.PATIENT_ID).Distinct().ToList();
                    List<HIS_PATIENT> patients = new HisPatientGet().GetByIds(patientIds);
                    string codes = String.Join(",", patients.Select(s => s.PATIENT_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisHoldReturn_PhieuGiuTraDaDuocTra, codes);
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

        internal bool VerifyResponsibleRoom(HIS_HOLD_RETURN raw, long roomId)
        {
            bool valid = true;
            try
            {
                valid = this.VerifyResponsibleRoom(new List<HIS_HOLD_RETURN>() { raw }, roomId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool VerifyResponsibleRoom(List<HIS_HOLD_RETURN> listRaw, long roomId)
        {
            bool valid = true;
            try
            {
                List<HIS_HOLD_RETURN> notResponsibles = listRaw != null ? listRaw.Where(o => o.RESPONSIBLE_ROOM_ID != roomId).ToList() : null;
                if (IsNotNullOrEmpty(notResponsibles))
                {
                    List<long> patientIds = notResponsibles.Select(s => s.PATIENT_ID).Distinct().ToList();
                    List<HIS_PATIENT> patients = new HisPatientGet().GetByIds(patientIds);
                    string codes = String.Join(",", patients.Select(s => s.PATIENT_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisHoldReturn_PhongLamViecKhongPhaiPhongGiuPhieu, codes);
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

        internal bool IsLocked(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE == data.IS_ACTIVE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisHoldReturn_BNChuaKhoaVienPhiKhongDuocTraGiayTo);
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
