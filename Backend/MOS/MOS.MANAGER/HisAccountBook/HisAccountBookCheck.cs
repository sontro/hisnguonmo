using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTransaction;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccountBook
{
    class HisAccountBookCheck : BusinessBase
    {
        internal HisAccountBookCheck()
            : base()
        {

        }

        internal HisAccountBookCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        /// <summary>
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_ACCOUNT_BOOK data)
        {
            bool valid = true;
            try
            {
                data = new HisAccountBookGet().GetById(id);
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

        internal bool VerifyRequireField(HIS_ACCOUNT_BOOK data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.ACCOUNT_BOOK_NAME)) throw new ArgumentNullException("data.ACCOUNT_BOOK_NAME");
                if (!IsNotNullOrEmpty(data.ACCOUNT_BOOK_CODE)) throw new ArgumentNullException("data.ACCOUNT_BOOK_CODE");
                if (!IsGreaterThanZero(data.TOTAL)) throw new ArgumentNullException("data.TOTAL");
                if (!IsGreaterThanZero(data.FROM_NUM_ORDER)) throw new ArgumentNullException("data.FROM_NUM_ORDER");
                if (!IsGreaterThanZero(data.TOTAL)) throw new ArgumentNullException("data.TOTAL");
                if (data.IS_FOR_DEPOSIT != MOS.UTILITY.Constant.IS_TRUE && data.IS_FOR_BILL != MOS.UTILITY.Constant.IS_TRUE && data.IS_FOR_REPAY != MOS.UTILITY.Constant.IS_TRUE && data.IS_FOR_DEBT != MOS.UTILITY.Constant.IS_TRUE && data.IS_FOR_OTHER_SALE != MOS.UTILITY.Constant.IS_TRUE)
                {
                    throw new ArgumentNullException("data.IS_FOR_BILL, data.FOR_REPAY, data.FOR_DEPOSIT, data.IS_FOR_DEBT, data.IS_FOR_OTHER_SALE");
                }
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
                if (DAOWorker.HisAccountBookDAO.ExistsCode(code, id))
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

        internal bool IsUnLock(HIS_ACCOUNT_BOOK data)
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
                if (!DAOWorker.HisAccountBookDAO.IsUnLock(id))
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

        internal bool VerifyTransactionExistForUpdate(HIS_ACCOUNT_BOOK data, HIS_ACCOUNT_BOOK existed)
        {
            bool valid = true;
            try
            {
                List<HIS_TRANSACTION> existedTrans = new HisTransactionGet().GetByAccountBookId(data.ID);
                if (IsNotNullOrEmpty(existedTrans))
                {
                    //Kiem tra xem co chinh sua truong "Tu so" ko
                    if (existed.FROM_NUM_ORDER != data.FROM_NUM_ORDER)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_KhongDuocCapNhatTuSoViDaCoGiaoDich);
                        valid = false;
                    }

                    //Kiem tra xem co chinh sua truong "total" nho hon tong so da su dung ko
                    if (data.TOTAL < existedTrans.Count)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_TongSoPhieuPhaiLonHonSoDaSuDung);
                        valid = false;
                    }

                    if (((data.SYMBOL_CODE ?? "") != (existed.SYMBOL_CODE ?? ""))
                        || ((data.TEMPLATE_CODE ?? "") != (existed.TEMPLATE_CODE ?? "")))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisAccountBook_KhongChoPhepCapNhatMauSoKyHieuViDaCoGiaoDich);
                        return false;
                    }

                    List<HIS_TRANSACTION> listBill = existedTrans.Where(o => o.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT).ToList();
                    List<HIS_TRANSACTION> listDeposit = existedTrans.Where(o => o.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU).ToList();
                    List<HIS_TRANSACTION> listRepay = existedTrans.Where(o => o.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU).ToList();

                    //Kiem tra xem co bo bot loai su dung la "bill" ma da ton tai giao dich thuoc loai do ko
                    if ((data.IS_FOR_BILL == null || data.IS_FOR_BILL != MOS.UTILITY.Constant.IS_TRUE) && listBill != null && listBill.Count > 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_KhongTheBoBotLoaiSuDungDaCoGiaoDich);
                        valid = false;
                    }

                    //Kiem tra xem co bo bot loai su dung la "deposit" ma da ton tai giao dich thuoc loai do ko
                    if ((data.IS_FOR_DEPOSIT == null || data.IS_FOR_DEPOSIT != MOS.UTILITY.Constant.IS_TRUE) && listDeposit != null && listDeposit.Count > 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_KhongTheBoBotLoaiSuDungDaCoGiaoDich);
                        valid = false;
                    }

                    //Kiem tra xem co bo bot loai su dung la "repay" ma da ton tai giao dich thuoc loai do ko
                    if ((data.IS_FOR_REPAY == null || data.IS_FOR_REPAY != MOS.UTILITY.Constant.IS_TRUE) && listBill != null && listRepay.Count > 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_KhongTheBoBotLoaiSuDungDaCoGiaoDich);
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

        internal bool VerifyTransactionExistForDelete(HIS_ACCOUNT_BOOK data)
        {
            bool valid = true;
            try
            {
                List<HIS_TRANSACTION> existedTrans = new HisTransactionGet().GetByAccountBookId(data.ID);
                if (IsNotNullOrEmpty(existedTrans))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_KhongTheXoaViDaCoGiaoDich);
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

        internal bool VerifyBookType(HIS_ACCOUNT_BOOK data)
        {
            bool valid = true;
            try
            {
                //if (data.IS_FOR_BILL == MOS.UTILITY.Constant.IS_TRUE
                //    && (data.IS_FOR_DEPOSIT == MOS.UTILITY.Constant.IS_TRUE
                //    || data.IS_FOR_REPAY == MOS.UTILITY.Constant.IS_TRUE))
                //{
                //    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                //    throw new Exception("Neu chon so thanh toan thi khong cho phep hoan ung hoac tam ung." + LogUtil.TraceData("data", data));
                //}
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
