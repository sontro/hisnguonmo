using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisInvoiceBook;
using MOS.MANAGER.HisUserInvoiceBook;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;

namespace MOS.MANAGER.HisInvoice
{
    partial class HisInvoiceCheck : BusinessBase
    {
        internal HisInvoiceCheck()
            : base()
        {

        }

        internal HisInvoiceCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_INVOICE data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.INVOICE_BOOK_ID)) throw new ArgumentNullException("data.INVOICE_BOOK_ID");
                if (!IsGreaterThanZero(data.INVOICE_TIME)) throw new ArgumentNullException("data.INVOICE_TIME");
                if (!IsGreaterThanZero(data.PAY_FORM_ID)) throw new ArgumentNullException("data.PAY_FORM_ID");
                if (!IsNotNullOrEmpty(data.SELLER_NAME)) throw new ArgumentNullException("data.SELLER_NAME");
                if (!IsNotNullOrEmpty(data.BUYER_NAME)) throw new ArgumentNullException("data.BUYER_NAME");
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
                if (new HisInvoiceGet().GetById(id) == null)
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
        internal bool VerifyId(long id, ref HIS_INVOICE data)
        {
            bool valid = true;
            try
            {
                data = new HisInvoiceGet().GetById(id);
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
                    HisInvoiceFilterQuery filter = new HisInvoiceFilterQuery();
                    filter.IDs = listId;
                    List<HIS_INVOICE> listData = new HisInvoiceGet().Get(filter);
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
        internal bool VerifyIds(List<long> listId, List<HIS_INVOICE> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisInvoiceFilterQuery filter = new HisInvoiceFilterQuery();
                    filter.IDs = listId;
                    List<HIS_INVOICE> listData = new HisInvoiceGet().Get(filter);
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
        internal bool IsUnLock(HIS_INVOICE data)
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
                if (!DAOWorker.HisInvoiceDAO.IsUnLock(id))
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
                    HisInvoiceFilterQuery filter = new HisInvoiceFilterQuery();
                    filter.IDs = listId;
                    List<HIS_INVOICE> listData = new HisInvoiceGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_INVOICE> listData)
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
        /// Kiem tra xem nguoi dung da co quyen chon so hoa don do khong
        /// </summary>
        /// <param name="invoiceBookId"></param>
        /// <returns></returns>
        internal bool IsValidPermission(long invoiceBookId)
        {
            bool valid = true;
            try
            {
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                List<HIS_USER_INVOICE_BOOK> hisUserInvoiceBooks = new HisUserInvoiceBookGet().GetByInvoiceBookIdAndLoginName(invoiceBookId, loginName);
                if (!IsNotNullOrEmpty(hisUserInvoiceBooks))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisInvoice_KhongCoQuyenSuDungSoHoaDonDaChon);
                    throw new Exception("Khong co quyen su dung so hoa don. Invoice_book_id: " + invoiceBookId);
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

        internal bool IsValidCount(long invoiceBookId)
        {
            bool valid = true;
            try
            {
                HisInvoiceBookGet hisInvoiceGet = new HisInvoiceBookGet();

                V_HIS_INVOICE_BOOK invoiceBook = hisInvoiceGet.GetViewById(invoiceBookId);
                if (invoiceBook.FROM_NUM_ORDER + invoiceBook.TOTAL - 1 <= invoiceBook.CURRENT_NUM_ORDER)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisInvoiceBook_SoDaHetSo);
                    LogSystem.Info("So da het so." + LogUtil.TraceData("invoiceBook", invoiceBook));
                    return false;
                }

                //Kiem tra xem so truoc do da het so hay chua
                if (invoiceBook.LINK_ID.HasValue)
                {
                    V_HIS_INVOICE_BOOK previous = hisInvoiceGet.GetViewById(invoiceBook.LINK_ID.Value);
                    if (previous != null)
                    {
                        long lastNumberOfPrevious = previous.FROM_NUM_ORDER + previous.TOTAL - 1;
                        if (previous.CURRENT_NUM_ORDER < lastNumberOfPrevious)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisInvoiceBook_SoCuChuaHetSo, previous.INVOICE_BOOK_NAME);
                            LogSystem.Info("So chua het so, khong cho tao so moi " + LogUtil.TraceData("previous", previous));
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

        internal bool IsNotCancel(HIS_INVOICE raw)
        {
            return this.IsNotCancel(new List<HIS_INVOICE>() { raw });
        }

        /// <summary>
        /// Kiem tra du lieu da bi huy hay chua
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsNotCancel(List<HIS_INVOICE> listData)
        {
            bool valid = true;
            try
            {
                List<string> canceledCodes = listData != null ? listData.Where(o => o.IS_CANCEL == Constant.IS_TRUE).Select(o => o.NUM_ORDER.ToString()).ToList() : null;
                if (IsNotNullOrEmpty(canceledCodes))
                {
                    string codeStr = string.Join(",", canceledCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisInvoice_HoaDonDaBiHuy, codeStr);
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
