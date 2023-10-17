using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestStt;
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using Inventec.Token.ResourceSystem;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisAntibioticRequest;

namespace MOS.MANAGER.HisExpMest.Common
{
    class HisExpMestCheck : BusinessBase
    {
        class ExpMestCheckData
        {
            public string EXP_MEST_CODE { get; set; }
            public string TDL_SERVICE_REQ_CODE { get; set; }
        }

        #region Danh sach trang thai cho phep khi Delete
        private static List<long> allowedDeleteStatus = new List<long>(){
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DRAFT
        };
        #endregion

        #region Danh sach loai xuat la don (don mau, doi pk, don nt, don tt)
        private static List<long> expMestTypeIdPres = new List<long>(){
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__VACCIN
        };
        #endregion

        internal HisExpMestCheck()
            : base()
        {

        }

        internal HisExpMestCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        /// <summary>
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_EXP_MEST data)
        {
            bool valid = true;
            try
            {
                data = new HisExpMestGet().GetById(id);
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
        internal bool VerifyIds(List<long> listId, List<HIS_EXP_MEST> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                    filter.IDs = listId;
                    List<HIS_EXP_MEST> listData = new HisExpMestGet().Get(filter);
                    if (listData == null || listId.Count != listData.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        Logging("ListId invalid." + LogUtil.TraceData("listData", listData) + LogUtil.TraceData("listId", listId), LogType.Error);
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

        internal bool VerifyRequireField(HIS_EXP_MEST data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.EXP_MEST_STT_ID)) throw new ArgumentNullException("data.EXP_MEST_STT_ID");
                if (!IsGreaterThanZero(data.EXP_MEST_TYPE_ID)) throw new ArgumentNullException("data.EXP_MEST_TYPE_ID");
                if (!IsGreaterThanZero(data.MEDI_STOCK_ID)) throw new ArgumentNullException("data.MEDI_STOCK_ID");
                if (data.REQ_ROOM_ID <= 0) throw new ArgumentNullException("data.REQ_ROOM_ID");
                if (data.REQ_DEPARTMENT_ID <= 0) throw new ArgumentNullException("data.REQ_DEPARTMENT_ID");
                if (string.IsNullOrWhiteSpace(data.REQ_LOGINNAME)) throw new ArgumentNullException("data.REQ_LOGINNAME");
                if (string.IsNullOrWhiteSpace(data.REQ_USERNAME)) throw new ArgumentNullException("data.REQ_USERNAME");
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

        internal bool VerifyRequireField(HisExpMestSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId null");
                if (data.ExpMestId <= 0) throw new ArgumentNullException("data.ExpMestId null");
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

        internal bool IsUnlock(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (raw != null && raw.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
                    throw new Exception("Phieu xuat dang bi khoa Id" + raw.ID);
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

        internal bool IsUnlock(List<HIS_EXP_MEST> listData)
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

        internal bool HasNotInExpMestAggr(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (raw != null && raw.AGGR_EXP_MEST_ID.HasValue)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_DaThuocPhieuXuatTongHop, raw.TDL_AGGR_EXP_MEST_CODE);
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

        internal bool VerifyStatusForDelete(HIS_EXP_MEST data)
        {
            bool valid = true;
            try
            {
                if (!allowedDeleteStatus.Contains(data.EXP_MEST_STT_ID))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhongChoPhepHuyKhiDangOTrangThaiNay);
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

        internal bool VerifyStatusForDelete(long expMestId)
        {
            bool valid = true;
            try
            {
                HIS_EXP_MEST expMest = new HisExpMestGet().GetById(expMestId);
                valid = this.VerifyStatusForDelete(expMest);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool CheckPermission(HIS_EXP_MEST raw, bool allowDeletePres)
        {
            bool valid = true;
            try
            {
                if (!allowDeletePres && expMestTypeIdPres.Contains(raw.EXP_MEST_TYPE_ID))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanKhongCoQuyenXoaDonThuoc);
                    return false;
                }

                if (allowDeletePres && !expMestTypeIdPres.Contains(raw.EXP_MEST_TYPE_ID) && !raw.PRESCRIPTION_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanKhongCoQuyenXoaPhieuXuatKhongPhaiDonThuoc);
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

        internal bool HasNotBill(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (raw != null && raw.BILL_ID.HasValue)
                {
                    string transactionCode = "";
                    HisTransactionFilterQuery filter = new HisTransactionFilterQuery();
                    filter.ID = raw.BILL_ID.Value;
                    List<HIS_TRANSACTION> listData = new HisTransactionGet().Get(filter);
                    if (IsNotNullOrEmpty(listData))
                    {
                        transactionCode = string.Join(",", listData.Select(s => s.TRANSACTION_CODE));
                    }

                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatBanDaCoHoaDonThanhToan, raw.EXP_MEST_CODE, transactionCode);
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

        internal bool HasNoDebt(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (raw != null && raw.DEBT_ID.HasValue)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatBanDaDuocChotNo, raw.EXP_MEST_CODE);
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

        internal bool VerifyFieldNotUpdate(HIS_EXP_MEST newData, HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (newData.EXP_MEST_TYPE_ID != raw.EXP_MEST_TYPE_ID)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Khong cho phep sua loai xuat");
                }

                if (newData.MEDI_STOCK_ID != raw.MEDI_STOCK_ID)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Khong cho phep sua kho xuat");
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

        internal bool HasNoNationalCode(HIS_EXP_MEST expMest)
        {
            return HasNoNationalCode(new List<HIS_EXP_MEST>() { expMest });
        }

        internal bool HasNoNationalCode(List<HIS_EXP_MEST> expMest)
        {
            try
            {
                List<string> hasCodes = expMest != null ? expMest
                    .Where(o => !string.IsNullOrWhiteSpace(o.NATIONAL_EXP_MEST_CODE))
                    .Select(o => o.EXP_MEST_CODE)
                    .ToList() : null;

                if (IsNotNullOrEmpty(hasCodes))
                {
                    string str = string.Join(",", hasCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DaCoMaQuocGia, str);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool HasNationalCode(HIS_EXP_MEST expMest)
        {
            return HasNationalCode(new List<HIS_EXP_MEST>() { expMest });
        }

        internal bool HasNationalCode(List<HIS_EXP_MEST> expMest)
        {
            try
            {
                List<string> hasCodes = expMest != null ? expMest
                    .Where(o => string.IsNullOrWhiteSpace(o.NATIONAL_EXP_MEST_CODE))
                    .Select(o => o.EXP_MEST_CODE)
                    .ToList() : null;

                if (IsNotNullOrEmpty(hasCodes))
                {
                    string str = string.Join(",", hasCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_ChuaCoMaQuocGia, str);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsInRequest(HIS_EXP_MEST expMest)
        {
            try
            {
                if (expMest.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST
                    && expMest.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DRAFT)
                {
                    HIS_EXP_MEST_STT expMestStt = HisExpMestSttCFG.DATA.Where(o => o.ID == expMest.EXP_MEST_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuDaOTrangThaiKhongChoPhepChinhSua, expMestStt.EXP_MEST_STT_NAME);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

        internal bool IsInRequest(List<HIS_EXP_MEST> expMests)
        {
            bool valid = true;
            try
            {
                foreach (var item in expMests)
                {
                    valid = valid && this.IsInRequest(item);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra ton tai don co ho so dieu tri da duyet khoa vien phi thi khong cho huy thuc xuat
        /// </summary>
        /// <returns></returns>
        internal bool IsUnLockFeeTreatment(List<HIS_EXP_MEST> expMests, ref List<HIS_TREATMENT> treatments)
        {
            bool result = true;
            try
            {
                if (expMests != null && expMests.Count > 0)
                {
                    List<long> treatmentIds = expMests.Select(o => o.TDL_TREATMENT_ID ?? 0).Distinct().ToList();
                    treatments = new HisTreatmentGet().GetByIds(treatmentIds);
                    List<HIS_TREATMENT> lockFeeTreatments = IsNotNullOrEmpty(treatments) ? treatments.Where(o => o.IS_ACTIVE != Constant.IS_TRUE).ToList() : null;
                    if (IsNotNullOrEmpty(lockFeeTreatments))
                    {
                        List<string> codes = lockFeeTreatments.Select(o => o.TREATMENT_CODE).ToList();
                        string codeStr = string.Join(",", codes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacHoSoDaDuyetKhoaVienPhi, codeStr);
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

            return result;
        }

        internal bool IsUnfinishTreatmentInCaseOfInPatient(HIS_EXP_MEST expMest, ref HIS_TREATMENT treatment)
        {
            bool result = true;
            try
            {
                if (expMest.TDL_TREATMENT_ID.HasValue)
                {
                    treatment = new HisTreatmentGet().GetById(expMest.TDL_TREATMENT_ID.Value);
                    return this.IsUnfinishTreatmentInCaseOfInPatient(new List<HIS_TREATMENT>() { treatment });
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

            return result;
        }

        internal bool IsUnfinishTreatmentInCaseOfInPatient(List<HIS_TREATMENT> treatments)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(treatments) && HisExpMestCFG.DONOT_ALLOW_UNEXPORT_AFTER_TREATMENT_FINISHING_IN_CASE_OF_INPATIENT)
                {
                    List<string> finishedInpatientTreatments = treatments.Where(o => o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU && o.IS_PAUSE == Constant.IS_TRUE).Select(o => o.TREATMENT_CODE).ToList();
                    if (IsNotNullOrEmpty(finishedInpatientTreatments))
                    {
                        string codeStr = string.Join(",", finishedInpatientTreatments);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacHoSoDaKetThucDieuTri, codeStr);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

            return result;
        }

        internal bool IsFinished(HIS_EXP_MEST expMest)
        {
            return IsFinished(new List<HIS_EXP_MEST>() { expMest });
        }

        internal bool IsFinished(List<HIS_EXP_MEST> expMest)
        {
            try
            {
                List<string> hasCodes = expMest != null ? expMest
                    .Where(o => o.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                    .Select(o => o.EXP_MEST_CODE)
                    .ToList() : null;

                if (IsNotNullOrEmpty(hasCodes))
                {
                    string str = string.Join(",", hasCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_ChuaHoanThanh, str);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsUnNotTaken(HIS_EXP_MEST expMest)
        {
            return IsUnNotTaken(new List<HIS_EXP_MEST>() { expMest });
        }

        internal bool IsUnNotTaken(List<HIS_EXP_MEST> expMests)
        {
            bool valid = true;
            try
            {
                List<string> expMestCodes = expMests != null ? expMests.Where(o => o.IS_NOT_TAKEN == Constant.IS_TRUE).Select(o => o.EXP_MEST_CODE).ToList() : null;

                if (IsNotNullOrEmpty(expMestCodes))
                {
                    string codeStr = string.Join(",", expMestCodes);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDaDuocTichKhongLay, codeStr);
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

        /// <summary>
        /// Kiem tra xem phieu xuat co thuoc/vat tu nao bi danh dau "ko thuc hien" trong bang ke hay ko.
        /// Neu co thi ko cho xuat
        /// </summary>
        /// <param name="expMests"></param>
        /// <returns></returns>
        internal bool IsUnNoExecute(List<HIS_EXP_MEST> expMests)
        {
            try
            {
                //Chi kiem tra voi cac phieu xuat lien quan den BN
                List<long> ids = expMests != null ? expMests
                    .Where(o => o.SERVICE_REQ_ID.HasValue).Select(o => o.ID).ToList() : null;
                if (IsNotNullOrEmpty(ids))
                {

                    string sql = "SELECT EXP.EXP_MEST_CODE, EXP.TDL_SERVICE_REQ_CODE "
                              + "  FROM HIS_EXP_MEST EXP "
                              + " WHERE EXISTS (SELECT 1 FROM HIS_SERE_SERV S "
                              + "               WHERE S.SERVICE_REQ_ID = EXP.SERVICE_REQ_ID AND S.IS_NO_EXECUTE = 1 AND S.IS_DELETE = 0) "
                              + " AND  %IN_CLAUSE% ";
                    sql = DAOWorker.SqlDAO.AddInClause(ids, sql, "EXP.ID");
                    List<ExpMestCheckData> noExecutes = DAOWorker.SqlDAO.GetSql<ExpMestCheckData>(sql);
                    if (IsNotNullOrEmpty(noExecutes))
                    {
                        List<string> expMestCodes = noExecutes.Select(o => o.EXP_MEST_CODE).ToList();
                        List<string> serviceReqCodes = noExecutes.Select(o => o.TDL_SERVICE_REQ_CODE).ToList();
                        string expMestCodeStr = string.Join(",", expMestCodes);
                        string serviceReqCodeStr = string.Join(",", serviceReqCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuXuatCoDanhDauKhongThucHien, expMestCodeStr, serviceReqCodeStr);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return false;
        }

        internal bool IsUnNoExecute(HIS_EXP_MEST expMest)
        {
            return IsUnNoExecute(new List<HIS_EXP_MEST>() { expMest });
        }

        internal bool HasNotBill(List<HIS_EXP_MEST> listRaw)
        {
            bool valid = true;
            try
            {
                var hasBills = listRaw != null ? listRaw.Where(o => o.BILL_ID.HasValue).ToList() : null;
                if (IsNotNullOrEmpty(hasBills))
                {
                    string codes = String.Join(",", hasBills.Select(s => s.EXP_MEST_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDaCoHoaDonThanhToan, codes);
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

        /// <summary>
        /// Kiem tra co cau hinh tu dong duyet, xuat, tao phieu nhap voi xuat chuyen kho khong
        /// Neu co cau hinh thi check xem nguoi dung co quyen o ca kho xuat va kho nhap khong
        /// </summary>
        /// <param name="expMest"></param>
        /// <returns></returns>
        internal bool IsAutoStockTransfer(HIS_EXP_MEST expMest)
        {
            bool valid = false;
            try
            {
                if (!HisMediStockCFG.IS_AUTO_STOCK_TRANSFER) return false;
                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK
                    && expMest.IMP_MEDI_STOCK_ID.HasValue)
                {
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    if (!HisUserRoomCFG.DATA.Any(a => a.LOGINNAME == loginname && HisMediStockCFG.DATA.Exists(e => e.ID == expMest.MEDI_STOCK_ID && e.ROOM_ID == a.ROOM_ID)))
                    {
                        return false;
                    }

                    if (!HisUserRoomCFG.DATA.Any(a => a.LOGINNAME == loginname && HisMediStockCFG.DATA.Exists(e => e.ID == expMest.IMP_MEDI_STOCK_ID.Value && e.ROOM_ID == a.ROOM_ID)))
                    {
                        return false;
                    }

                    return true;
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
        /// Kiem tra xem da cho thong tin chot ky chua
        /// </summary>
        /// <param name="expMest"></param>
        /// <returns></returns>
        internal bool HasNoMediStockPeriod(List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_MATERIAL> expMaterials)
        {
            try
            {
                bool check = true;
                check = check && (!IsNotNullOrEmpty(expMedicines) || !expMedicines.Exists(t => t.MEDI_STOCK_PERIOD_ID.HasValue));
                check = check && (!IsNotNullOrEmpty(expMaterials) || !expMaterials.Exists(t => t.MEDI_STOCK_PERIOD_ID.HasValue));
                if (!check)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DaDuocChotKyKhongChoPhepCapNhatHoacXoa);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsChmsAdditionOrReduction(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (raw.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK
                    || !raw.CHMS_TYPE_ID.HasValue)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatKhongPhaiBoSungHoanCoSo);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }


        internal bool IsCompensationPres(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (raw.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS
                    && raw.BCS_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.BCS_TYPE__ID__PRES)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat khong phai bu theo don" + LogUtil.TraceData("ExpMest", raw));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsNotTaken(HIS_EXP_MEST expMest)
        {
            return IsNotTaken(new List<HIS_EXP_MEST>() { expMest });
        }

        internal bool IsNotTaken(List<HIS_EXP_MEST> expMests)
        {
            bool valid = true;
            try
            {
                List<string> expMestCodes = expMests != null ? expMests.Where(o => o.IS_NOT_TAKEN != Constant.IS_TRUE).Select(o => o.EXP_MEST_CODE).ToList() : null;

                if (IsNotNullOrEmpty(expMestCodes))
                {
                    string codeStr = string.Join(",", expMestCodes);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatChuaDuocTichKhongLay, codeStr);
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

        internal bool IsNotBeingApproved(HIS_EXP_MEST expMest)
        {
            return IsNotBeingApproved(new List<HIS_EXP_MEST>() { expMest });
        }

        internal bool IsNotBeingApproved(List<HIS_EXP_MEST> expMests)
        {
            bool valid = true;
            try
            {
                List<string> expMestCodes = expMests != null ? expMests.Where(o => o.IS_BEING_APPROVED == Constant.IS_TRUE).Select(o => o.EXP_MEST_CODE).ToList() : null;

                if (IsNotNullOrEmpty(expMestCodes))
                {
                    string codeStr = string.Join(",", expMestCodes);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDangThucHienDuyet, codeStr);
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



        internal bool HasToExpMestReason(long? expMestReasonId)
        {
            bool valid = true;
            try
            {
                if (!expMestReasonId.HasValue && HisExpMestCFG.IS_REASON_REQUIRED)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThieuThongTinLydoXuat);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool WorkingInMediStockOrIsCreator(HIS_EXP_MEST expMest, WorkPlaceSDO workPlace)
        {
            bool valid = true;
            try
            {
                string loginName = ResourceTokenManager.GetLoginName();
                if ((string.IsNullOrWhiteSpace(loginName) || !loginName.Equals(expMest.CREATOR))
                    && (!workPlace.MediStockId.HasValue || expMest.MEDI_STOCK_ID != workPlace.MediStockId.Value))
                {
                    V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == expMest.MEDI_STOCK_ID);
                    string name = mediStock != null ? mediStock.MEDI_STOCK_NAME : "";
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.BanDangKhongLamViecTaiKhoVaKhongPhaiLaNguoiTao, name);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsValidApproveAntibioticUse(List<HIS_EXP_MEST> expMests)
        {
            if (HisExpMestCFG.DISALLOW_TO_EXPORT_UNAPPROVED_USING_REQUEST && IsNotNullOrEmpty(expMests))
            {
                List<HIS_EXP_MEST> usingRequestApprovedRequireds = expMests.Where(o => o.IS_USING_APPROVAL_REQUIRED == Constant.IS_TRUE && o.TDL_SERVICE_REQ_CODE != null).ToList();
                List<HIS_EXP_MEST> unRequests = IsNotNullOrEmpty(usingRequestApprovedRequireds) ? usingRequestApprovedRequireds.Where(o => !o.ANTIBIOTIC_REQUEST_ID.HasValue).ToList() : null;

                List<string> AllserviceReqCodes = new List<string>();

                List<string> unRequestserviceReqCodes = null;
                if (IsNotNullOrEmpty(unRequests))
                {
                    unRequestserviceReqCodes = unRequests.Select(o => o.TDL_SERVICE_REQ_CODE).ToList();
                    AllserviceReqCodes.AddRange(unRequestserviceReqCodes);
                }

                List<long> antibioticRequestIds = IsNotNullOrEmpty(usingRequestApprovedRequireds) ? usingRequestApprovedRequireds.Where(o => o.ANTIBIOTIC_REQUEST_ID.HasValue).Select(o => o.ANTIBIOTIC_REQUEST_ID.Value).ToList() : null;

                List<HIS_EXP_MEST> unapprovals = null;
                List<string> unapprovalServiceReqCodes = null;
                if (IsNotNullOrEmpty(antibioticRequestIds))
                {
                    HisAntibioticRequestFilterQuery filter = new HisAntibioticRequestFilterQuery();
                    filter.IDs = antibioticRequestIds;
                    List<HIS_ANTIBIOTIC_REQUEST> antibioticRequests = new HisAntibioticRequestGet().Get(filter);

                    List<long> unapprovalIds = IsNotNullOrEmpty(antibioticRequests) ? antibioticRequests.Where(o => o.ANTIBIOTIC_REQUEST_STT != IMSys.DbConfig.HIS_RS.HIS_ANTIBIOTIC_REQUEST_STT.APPROVED).Select(o => o.ID).ToList() : null;

                    unapprovals = IsNotNullOrEmpty(unapprovalIds) ? usingRequestApprovedRequireds.Where(o => o.ANTIBIOTIC_REQUEST_ID.HasValue && unapprovalIds.Contains(o.ANTIBIOTIC_REQUEST_ID.Value)).ToList() : null;

                    if (IsNotNullOrEmpty(unapprovals))
                    {
                        unapprovalServiceReqCodes = unapprovals.Select(o => o.TDL_SERVICE_REQ_CODE).ToList();
                        AllserviceReqCodes.AddRange(unapprovalServiceReqCodes);
                    }
                }

                if (IsNotNullOrEmpty(AllserviceReqCodes))
                {
                    string serviceReqCodeStr = string.Join(",", AllserviceReqCodes);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DonChuaDuyetSuDungKhangSinh, serviceReqCodeStr);
                    return false;
                }
            }
            return true;
        }
    }
}
