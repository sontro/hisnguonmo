using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisTreatment;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisFinancePeriod;
using MOS.UTILITY;
using MOS.SDO;
using MOS.MANAGER.HisUserRoom;
using MOS.MANAGER.HisCaroAccountBook;
using MOS.MANAGER.HisUserAccountBook;
using System.Text;

namespace MOS.MANAGER.HisTransaction
{
    class HisTransactionCheck : BusinessBase
    {
        internal HisTransactionCheck()
            : base()
        {

        }

        internal HisTransactionCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.ACCOUNT_BOOK_ID)) throw new ArgumentNullException("data.ACCOUNT_BOOK_ID");
                if (!IsGreaterThanZero(data.PAY_FORM_ID)) throw new ArgumentNullException("data.PAY_FORM_ID");
                if (!IsGreaterThanZero(data.TRANSACTION_TYPE_ID)) throw new ArgumentNullException("data.TRANSACTION_TYPE_ID");
                if (data.AMOUNT < 0) throw new ArgumentNullException("data.AMOUNT");
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
        internal bool VerifyRequireFieldLockSDO(TransactionLockSDO sdo)
        {
            bool valid = true;
            try
            {
                if (sdo == null) throw new ArgumentNullException("sdo");
                if (!IsGreaterThanZero(sdo.TransactionId)) throw new ArgumentNullException("sdo.TransactionId");
                if (!IsGreaterThanZero(sdo.RequestRoomId)) throw new ArgumentNullException("sdo.RequestRoomId");
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
                if (DAOWorker.HisTransactionDAO.ExistsCode(code, id))
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

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung doi tuong)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(HIS_TRANSACTION data)
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
        /// Kiem tra du lieu da bi huy hay chua
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnCancel(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                if (data.IS_CANCEL == MOS.UTILITY.Constant.IS_TRUE)
                {
                    valid = false;
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichDaBiHuy);
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
        /// Kiem tra du lieu da bi huy hay chua
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnCancel(List<HIS_TRANSACTION> listData)
        {
            bool valid = true;
            try
            {
                foreach (HIS_TRANSACTION data in listData)
                {
                    valid = valid && IsUnCancel(data);
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
                if (!DAOWorker.HisTransactionDAO.IsUnLock(id))
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
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                data = new HisTransactionGet().GetById(id);
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
        internal bool VerifyIds(List<long> listId, List<HIS_TRANSACTION> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisTransactionFilterQuery filter = new HisTransactionFilterQuery();
                    filter.IDs = listId;
                    List<HIS_TRANSACTION> listData = new HisTransactionGet().Get(filter);
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
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyViewId(long id, ref V_HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                data = new HisTransactionGet().GetViewById(id);
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
        internal bool VerifyViewIds(List<long> listId, List<V_HIS_TRANSACTION> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisTransactionViewFilterQuery filter = new HisTransactionViewFilterQuery();
                    filter.IDs = listId;
                    List<V_HIS_TRANSACTION> listData = new HisTransactionGet().GetView(filter);
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
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach data)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<HIS_TRANSACTION> listData)
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

        internal bool IsUnlockAccountBook(long accountBookId, ref V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                accountBook = new HisAccountBookGet().GetViewById(accountBookId);

                if (accountBook == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("So thu chi khong ton tai tren he thong. accountBookId: " + accountBookId);
                }

                if (accountBook.IS_ACTIVE == null || accountBook.IS_ACTIVE.Value != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_SoDangBiKhoa, accountBook.ACCOUNT_BOOK_NAME);
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

        internal bool IsValidNumOrder(HIS_TRANSACTION data, V_HIS_ACCOUNT_BOOK accountBook, bool isCheckIsFor = true)
        {
            bool valid = true;
            try
            {
                if (data == null)
                {
                    return true;
                }

                if (!this.IsValidNumOrder(data.NUM_ORDER, accountBook))
                {
                    return false;
                }

                if (isCheckIsFor && (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT && accountBook.IS_FOR_BILL != MOS.UTILITY.Constant.IS_TRUE)
                    || (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU && accountBook.IS_FOR_DEPOSIT != MOS.UTILITY.Constant.IS_TRUE)
                    || (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU && accountBook.IS_FOR_REPAY != MOS.UTILITY.Constant.IS_TRUE)
                    || (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO && accountBook.IS_FOR_DEBT != MOS.UTILITY.Constant.IS_TRUE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_KhongChoPhepThucHienLoaiGiaoDich, accountBook.ACCOUNT_BOOK_NAME);
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

        internal bool IsValidNumOrder(long? numOrder, V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                if (accountBook.IS_NOT_GEN_TRANSACTION_ORDER == MOS.UTILITY.Constant.IS_TRUE)
                {
                    if (numOrder < 0 || !numOrder.HasValue)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_ChuaCoThongTinSoChungTu);
                        LogSystem.Warn("So thu chi khong tu dong sinh NumOrder. Nguoi dung phai truyen len NumOrder");
                        return false;
                    }

                    if (!this.IsInRangeNumOrder(accountBook, numOrder.Value))
                    {
                        return false;
                    }

                    HisTransactionFilterQuery tranFilterQuery = new HisTransactionFilterQuery();
                    tranFilterQuery.ACCOUNT_BOOK_ID = accountBook.ID;
                    tranFilterQuery.NUM_ORDER__EQUAL = numOrder.Value;
                    var listTran = new HisTransactionGet().Get(tranFilterQuery);
                    if (IsNotNullOrEmpty(listTran))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoChungTuCuaSoThuChiDaTonTai, numOrder.Value.ToString(), accountBook.ACCOUNT_BOOK_NAME);
                        return false;
                    }
                }
                else
                {
                    if (accountBook.CURRENT_NUM_ORDER >= (accountBook.FROM_NUM_ORDER + accountBook.TOTAL - 1))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_HetSo, accountBook.ACCOUNT_BOOK_NAME);
                        return false;
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

        internal bool IsValidAccountBookType(long transactionTypeId, V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                if ((transactionTypeId == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT && accountBook.IS_FOR_BILL != MOS.UTILITY.Constant.IS_TRUE)
                    || (transactionTypeId == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU && accountBook.IS_FOR_DEPOSIT != MOS.UTILITY.Constant.IS_TRUE)
                    || (transactionTypeId == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU && accountBook.IS_FOR_REPAY != MOS.UTILITY.Constant.IS_TRUE)
                    || (transactionTypeId == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO && accountBook.IS_FOR_DEBT != MOS.UTILITY.Constant.IS_TRUE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_KhongChoPhepThucHienLoaiGiaoDich, accountBook.ACCOUNT_BOOK_NAME);
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

        internal bool IsNotCollect(List<HIS_TRANSACTION> listRaw)
        {
            bool valid = true;
            try
            {
                List<string> collecteds = listRaw != null ? listRaw
                    .Where(o => o.CASHOUT_ID.HasValue)
                    .Select(o => o.TRANSACTION_CODE)
                    .ToList() : null;
                if (IsNotNullOrEmpty(collecteds))
                {
                    string codes = string.Join(",", collecteds);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_CacGiaoDichDaDuocNhapQuy, codes);
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

        internal bool IsCollect(List<HIS_TRANSACTION> listRaw)
        {
            bool valid = true;
            try
            {
                List<string> unCollecteds = listRaw != null ? listRaw
                    .Where(o => o.CASHOUT_ID.HasValue)
                    .Select(o => o.TRANSACTION_CODE)
                    .ToList() : null;
                if (IsNotNullOrEmpty(unCollecteds))
                {
                    string codes = string.Join(",", unCollecteds);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_CacGiaoDichChuaDuocNhapQuy, codes);
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

        internal bool IsNotCollect(HIS_TRANSACTION raw)
        {
            bool valid = true;
            try
            {
                if (raw.CASHOUT_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichDaDuocNhapQuy);
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

        internal bool IsAlowUpdateFile(HIS_TRANSACTION raw)
        {
            bool valid = true;
            try
            {
                if (raw.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichKhongPhaiLaThanhToan);
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

        internal bool IsNotCreatingTransaction(HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (treatment != null && treatment.IS_CREATING_TRANSACTION == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoDangDuocTaoGiaoDich, treatment.TREATMENT_CODE);
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
        /// Kiem tra xem so tien thuc hien giao dich co hop le ko.
        /// - So tien giao dich phai > 0
        /// - Neu la giao dich hoan ung thi phai dam bao sau khi hoan ung thi so tien con phai tra phai <= 0
        /// </summary>
        /// <param name="hisTransactionDTO"></param>
        /// <returns></returns>
        internal bool IsValidAmountRepay(HIS_TRANSACTION data, bool repayInDetail)
        {
            bool valid = true;
            try
            {
                if (data.AMOUNT < 0)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("So tien giao dich khong hop le. Amount: " + data.AMOUNT);
                }

                //Neu ko phai hoan ung theo tung dich vu thi kiem tra xem co so tien du de hoan ung khong
                if (!repayInDetail)
                {
                    //neu giao dich gan voi ho so dieu tri thi check ho so dieu tri
                    if (data.TREATMENT_ID.HasValue)
                    {
                        //Lay so tien con phai tra hien tai
                        decimal? unpaidAmount = new HisTreatmentGet(param).GetUnpaid(data.TREATMENT_ID.Value);
                        if (unpaidAmount == null)
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Ko xac dinh duoc unpaid_amount");
                        }

                        //So tien con phai tra sau khi thuc hien giao dich
                        unpaidAmount = unpaidAmount + data.AMOUNT;
                        if (unpaidAmount > 0)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_SoTienHoanUngKhongChoPhep);
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

        internal bool IsAlowCancelDeposit(HIS_TRANSACTION data, ref List<HIS_SERE_SERV_DEPOSIT> hisSereServDeposits)
        {
            bool valid = true;
            try
            {
                if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU)
                {
                    valid = valid && this.HasNoRepay(data.ID, ref hisSereServDeposits);
                    valid = valid && this.IsValidAvailableAmount(data);
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

        internal bool IsDeposit(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_GiaoDichKhongPhaiLaHoanUngKhongChoPhepKhoa);
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

        internal bool IsAlowLock(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV_DEPOSIT> hisSereServDeposits = null;
                valid = valid && this.HasNoRepay(data.ID, ref hisSereServDeposits);
                valid = valid && this.IsValidAvailableAmount(data);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasNoRepay(long depositId, ref List<HIS_SERE_SERV_DEPOSIT> sereServDeposits)
        {
            bool valid = true;
            try
            {
                HisSereServDepositFilterQuery filter = new HisSereServDepositFilterQuery();
                filter.DEPOSIT_ID = depositId;
                sereServDeposits = new HisSereServDepositGet().Get(filter);

                if (IsNotNullOrEmpty(sereServDeposits))
                {
                    List<HIS_SESE_DEPO_REPAY> seseDepoRepays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(sereServDeposits.Select(s => s.ID).ToList());
                    if (IsNotNullOrEmpty(seseDepoRepays))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDeposit_DaTonTaiGiaoDichHoanUng);
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

        internal bool IsValidAvailableAmount(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                if (data.TREATMENT_ID.HasValue)
                {
                    decimal? availableAmount = new HisTreatmentGet().GetAvailableAmount(data.TREATMENT_ID.Value);

                    //neu so tien hien du sau khi thuc hien giao dich < 0 ==> so tien tam ung da duoc
                    //ket chuyen hoac hoan ung 1 phan hoac toan bo
                    if (availableAmount == null || availableAmount - data.AMOUNT < 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DaHoanUngHoacKetChuyen);
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
        /// Kiem tra du lieu co o trang thai lock (su dung doi tuong)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsLock(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE == data.IS_ACTIVE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDaMoKhoa);
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

        internal bool HasNotFinancePeriod(long cashierRoomId, long transactionTime)
        {
            bool valid = false;
            try
            {
                HisFinancePeriodCheck financePeriodChecker = new HisFinancePeriodCheck(param);

                V_HIS_CASHIER_ROOM room = HisCashierRoomCFG.DATA.Where(o => o.ID == cashierRoomId).FirstOrDefault();
                if (room == null)
                {
                    LogSystem.Warn("cashierRoomId truyen len ko hop le, khong tim thay V_HIS_CASHIER_ROOM voi ID tuong ung");
                    return false;
                }
                return financePeriodChecker.HasNotFinancePeriod(room.BRANCH_ID, transactionTime);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasNotFinancePeriod(HIS_TRANSACTION data)
        {
            bool valid = false;
            try
            {
                valid = data != null && this.HasNotFinancePeriod(data.CASHIER_ROOM_ID, data.TRANSACTION_TIME);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasNoNationalCode(HIS_TRANSACTION transaction)
        {
            return HasNoNationalCode(new List<HIS_TRANSACTION>() { transaction });
        }

        internal bool HasNoNationalCode(List<HIS_TRANSACTION> transactions)
        {
            try
            {
                List<string> hasCodes = transactions != null ? transactions
                    .Where(o => !string.IsNullOrWhiteSpace(o.NATIONAL_TRANSACTION_CODE))
                    .Select(o => o.TRANSACTION_CODE)
                    .ToList() : null;

                if (IsNotNullOrEmpty(hasCodes))
                {
                    string str = string.Join(",", hasCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DaCoMaQuocGia, str);
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

        internal bool HasNationalCode(HIS_TRANSACTION transaction)
        {
            return HasNationalCode(new List<HIS_TRANSACTION>() { transaction });
        }

        internal bool HasNationalCode(List<HIS_TRANSACTION> transactions)
        {
            try
            {
                List<string> hasCodes = transactions != null ? transactions
                    .Where(o => string.IsNullOrWhiteSpace(o.NATIONAL_TRANSACTION_CODE))
                    .Select(o => o.TRANSACTION_CODE)
                    .ToList() : null;

                if (IsNotNullOrEmpty(hasCodes))
                {
                    string str = string.Join(",", hasCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DaCoMaQuocGia, str);
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

        internal bool IsNotGenTransactionNumOrder(V_HIS_ACCOUNT_BOOK data)
        {
            bool valid = true;
            try
            {
                if (!data.IS_NOT_GEN_TRANSACTION_ORDER.HasValue || data.IS_NOT_GEN_TRANSACTION_ORDER.Value != Constant.IS_TRUE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisAccountBook_SoBienLaiTuDongTangSo, data.ACCOUNT_BOOK_CODE);
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

        internal bool IsInRangeNumOrder(V_HIS_ACCOUNT_BOOK data, long numOrder)
        {
            bool valid = true;
            try
            {
                long max = (data.FROM_NUM_ORDER + data.TOTAL) - 1;
                if (data.IS_NOT_GEN_TRANSACTION_ORDER == Constant.IS_TRUE && (numOrder < data.FROM_NUM_ORDER || numOrder > max))
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoBienLaiNamNgoaiKhoangChoPhep, numOrder.ToString(), data.ACCOUNT_BOOK_NAME, data.FROM_NUM_ORDER.ToString(), max.ToString());
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

        public bool IsCashierRoom(WorkPlaceSDO workPlace)
        {
            try
            {
                if (workPlace == null || !workPlace.CashierRoomId.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
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

        internal bool CheckMustFinishTreatmentForBill(List<HIS_SERE_SERV> sereServs, HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (treatment.IS_PAUSE == Constant.IS_TRUE)
                {
                    return true;
                }
                if (!(HisTransactionCFG.MUST_FINISH_TREATMENT_FOR_BILL == (int)HisTransactionCFG.MUST_FINISH_TREATMENT.BHYT_ONLY
                    || HisTransactionCFG.MUST_FINISH_TREATMENT_FOR_BILL == (int)HisTransactionCFG.MUST_FINISH_TREATMENT.ALL))
                {
                    return true;
                }
                if (IsNotNullOrEmpty(sereServs))
                {
                    if (HisTransactionCFG.MUST_FINISH_TREATMENT_FOR_BILL == (int)HisTransactionCFG.MUST_FINISH_TREATMENT.BHYT_ONLY
                        && sereServs.Any(a => a.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_BenhNhanChuaKetThucDieuTriKhongDuocThanhToanBHYT);
                        return false;
                    }

                    if (HisTransactionCFG.MUST_FINISH_TREATMENT_FOR_BILL == (int)HisTransactionCFG.MUST_FINISH_TREATMENT.ALL)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_BenhNhanChuaKetThucDieuTri);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }

        internal bool HasPermission(long requestRoomId, ref V_HIS_CASHIER_ROOM cashierRoom)
        {
            cashierRoom = HisCashierRoomCFG.DATA.Where(o => o.ROOM_ID == requestRoomId).FirstOrDefault();
            if (cashierRoom == null)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                return false;
            }

            HisUserRoomFilterQuery filter = new HisUserRoomFilterQuery();
            filter.LOGINNAME__EXACT = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            filter.ROOM_ID = requestRoomId;
            filter.IS_ACTIVE = Constant.IS_TRUE;

            List<HIS_USER_ROOM> userRooms = new HisUserRoomGet().Get(filter);
            if (!IsNotNullOrEmpty(userRooms))
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_BanKhongCoQuyenTruyCapVaoPhongThuNgan, cashierRoom.CASHIER_ROOM_NAME);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Kiem tra du lieu da bi huy hay chua
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool HasNoDeptId(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                return this.HasNoDeptId(new List<HIS_TRANSACTION>() { data });
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
        /// Kiem tra du lieu da bi huy hay chua
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool HasNoDeptId(List<HIS_TRANSACTION> listData)
        {
            bool valid = true;
            try
            {
                List<HIS_TRANSACTION> hasDeptId = listData != null ? listData.Where(o => o.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO && o.DEBT_BILL_ID.HasValue).ToList() : null;
                if (IsNotNullOrEmpty(hasDeptId))
                {
                    string code = String.Join(",", hasDeptId.Select(s => s.TRANSACTION_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_GiaoDichDaDuocThuNo, code);
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

        internal bool HasPermissionAccountBook(V_HIS_ACCOUNT_BOOK data, long cashierRoomId)
        {
            bool valid = true;
            try
            {
                HisCaroAccountBookFilterQuery caroFilter = new HisCaroAccountBookFilterQuery();
                caroFilter.ACCOUNT_BOOK_ID = data.ID;
                caroFilter.CASHIER_ROOM_ID = cashierRoomId;
                var caroAccountBook = new HisCaroAccountBookGet().Get(caroFilter);

                HisUserAccountBookFilterQuery userFilter = new HisUserAccountBookFilterQuery();
                userFilter.ACCOUNT_BOOK_ID = data.ID;
                userFilter.LOGINNAME__EXACT = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                var userAccountBook = new HisUserAccountBookGet().Get(userFilter);

                if (!IsNotNullOrEmpty(caroAccountBook) && !IsNotNullOrEmpty(userAccountBook))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_BanKhongCoQuyenSuDungSo, data.ACCOUNT_BOOK_NAME);
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

        internal bool IsValidNumOrderUpdate(HIS_TRANSACTION data, V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                if (accountBook.IS_NOT_GEN_TRANSACTION_ORDER == MOS.UTILITY.Constant.IS_TRUE)
                {
                    if ((data.NUM_ORDER < 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("So thu chi khong tu dong sinh NumOrder. Nguoi dung truyen len NumOrder lon hon gioi han cua so thu chi: " + data.NUM_ORDER + "; AccountBookId: " + accountBook.ID);
                    }

                    long max = (accountBook.FROM_NUM_ORDER + accountBook.TOTAL) - 1;
                    if (data.NUM_ORDER < accountBook.FROM_NUM_ORDER || data.NUM_ORDER > max)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoBienLaiNamNgoaiKhoangChoPhep, accountBook.ACCOUNT_BOOK_NAME, accountBook.FROM_NUM_ORDER.ToString(), max.ToString());
                        return false;
                    }

                    HisTransactionFilterQuery tranFilterQuery = new HisTransactionFilterQuery();
                    tranFilterQuery.ACCOUNT_BOOK_ID = accountBook.ID;
                    tranFilterQuery.NUM_ORDER__EQUAL = data.NUM_ORDER;
                    var listTran = new HisTransactionGet().Get(tranFilterQuery);
                    if (IsNotNullOrEmpty(listTran))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoChungTuCuaSoThuChiDaTonTai, data.NUM_ORDER.ToString(), accountBook.ACCOUNT_BOOK_NAME);
                        return false;
                    }
                }
                else
                {
                    if (accountBook.CURRENT_NUM_ORDER >= (accountBook.FROM_NUM_ORDER + accountBook.TOTAL - 1))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_HetSo, accountBook.ACCOUNT_BOOK_NAME);
                        return false;
                    }

                    HisTransactionFilterQuery tranFilterQuery = new HisTransactionFilterQuery();
                    tranFilterQuery.ACCOUNT_BOOK_ID = accountBook.ID;
                    tranFilterQuery.NUM_ORDER__EQUAL = data.NUM_ORDER;
                    var listTran = new HisTransactionGet().Get(tranFilterQuery);
                    if (IsNotNullOrEmpty(listTran) && listTran.Exists(o => o.ID != data.ID))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoChungTuCuaSoThuChiDaTonTai, data.NUM_ORDER.ToString(), accountBook.ACCOUNT_BOOK_NAME);
                        return false;
                    }
                }

                if ((data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT && accountBook.IS_FOR_BILL != MOS.UTILITY.Constant.IS_TRUE)
                    || (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU && accountBook.IS_FOR_DEPOSIT != MOS.UTILITY.Constant.IS_TRUE)
                    || (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU && accountBook.IS_FOR_REPAY != MOS.UTILITY.Constant.IS_TRUE)
                    || (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO && accountBook.IS_FOR_DEBT != MOS.UTILITY.Constant.IS_TRUE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_KhongChoPhepThucHienLoaiGiaoDich, accountBook.ACCOUNT_BOOK_NAME);
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

        internal bool IsNotHasTigCode(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                if (!String.IsNullOrWhiteSpace(data.TIG_TRANSACTION_CODE) || !String.IsNullOrWhiteSpace(data.TIG_VOID_CODE))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("TigTransactionCode, TigVoidCode Is Not Null");
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

        internal bool IsHasDocumentInfo(List<long> materialIds)
        {
            bool valid = true;
            try
            {
                if (HisTransactionCFG.VERIFY_INVOICE_MATERIAL_BEFORE_BILLING && IsNotNullOrEmpty(materialIds))
                {
                    string tempSql = new StringBuilder().Append("SELECT IMP.IMP_MEST_CODE, IMP.MEDI_STOCK_ID")
                        .Append(", IMMA.MATERIAL_ID")
                        .Append(", MATE.MATERIAL_TYPE_ID")
                        .Append(", MATY.MATERIAL_TYPE_CODE, MATY.MATERIAL_TYPE_NAME")
                        .Append(" FROM HIS_IMP_MEST_MATERIAL IMMA")
                        .Append(" JOIN HIS_IMP_MEST IMP ON IMMA.IMP_MEST_ID = IMP.ID")
                        .Append(" JOIN HIS_MATERIAL MATE ON IMMA.MATERIAL_ID = MATE.ID")
                        .Append(" JOIN HIS_MATERIAL_TYPE MATY ON MATE.MATERIAL_TYPE_ID = MATY.ID")
                        .Append(" WHERE IMP.IMP_MEST_TYPE_ID = :param1")
                        .Append(" AND (IMP.DOCUMENT_NUMBER IS NULL OR IMP.DOCUMENT_DATE IS NULL)")
                        .Append(" AND IMMA.IS_DELETE = 0 AND (%IN_CLAUSE%)").ToString();

                    string sql = DAOWorker.SqlDAO.AddInClause(materialIds, tempSql, "IMMA.MATERIAL_ID");
                    LogSystem.Info("Sql check document info: " + sql);
                    List<VerifyDocumentData> exists = DAOWorker.SqlDAO.GetSql<VerifyDocumentData>(sql, IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC);
                    if (IsNotNullOrEmpty(exists))
                    {
                        string names = String.Join(", ", exists.Select(s => s.MATERIAL_TYPE_NAME).Distinct().ToList());
                        string codes = String.Join(", ", exists.Select(s => s.IMP_MEST_CODE).Distinct().ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_VatTuThieuThongTinHoaDon, names, codes);
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

        internal bool IsHasDocumentInfo(List<HIS_SERE_SERV> sereServs)
        {
            List<long> materialIds = IsNotNullOrEmpty(sereServs) ? sereServs.Where(o => o.MATERIAL_ID.HasValue).Select(s => s.MATERIAL_ID.Value).ToList() : null;
            return this.IsHasDocumentInfo(materialIds);
        }

        internal bool IsBill(List<HIS_TRANSACTION> data)
        {
            bool valid = true;
            try
            {
                List<string> notBills = data != null ? data
                    .Where(o => o.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
                    .Select(o => o.TRANSACTION_CODE).ToList() : null;

                if (IsNotNullOrEmpty(notBills))
                {
                    string codeStr = string.Join(",", notBills);

                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_GiaoDichKhongPhaiLaThanhToan, codeStr);
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

        internal bool IsCancel(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                if (data != null && data.IS_CANCEL != Constant.IS_TRUE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_GiaoDichKhongPhaiLaTrangThaiHuy, data.TRANSACTION_CODE);
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

        internal bool HasReplaceReason(string replaceReason)
        {
            bool valid = true;
            try
            {
                if (string.IsNullOrWhiteSpace(replaceReason))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_KhongCoThongTinLyDoThayThe);
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

        internal bool HasNoReplaceTransaction(long transactionId, ref List<HIS_TRANSACTION> replaceTransactions)
        {
            bool valid = true;
            try
            {
                replaceTransactions = new HisTransactionGet().GetByOriginalTransactionId(transactionId);
                replaceTransactions = replaceTransactions.Where(o => o.IS_CANCEL != Constant.IS_TRUE).ToList();
                if (IsNotNullOrEmpty(replaceTransactions))
                {
                    List<string> codes = replaceTransactions.Select(o => o.TRANSACTION_CODE).ToList();
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_GiaoDichDaDuocThayTheBoiGiaoDich, string.Join(", ", codes));
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

        internal bool HasNoReplaceTransaction(long originalTransactionId)
        {
            bool valid = true;
            try
            {
                var replaces = new HisTransactionGet().GetByOriginalTransactionId(originalTransactionId);
                if (IsNotNullOrEmpty(replaces))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_GiaoDichDaDuocThayThe);
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

    class VerifyDocumentData
    {
        public string IMP_MEST_CODE { get; set; }
        public long MEDI_STOCK_ID { get; set; }
        public long MATERIAL_ID { get; set; }
        public long MATERIAL_TYPE_ID { get; set; }
        public string MATERIAL_TYPE_CODE { get; set; }
        public string MATERIAL_TYPE_NAME { get; set; }
    }
}