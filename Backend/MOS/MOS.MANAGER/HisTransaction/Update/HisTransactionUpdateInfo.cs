using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisPayForm;
using MOS.MANAGER.HisRepayReason;
using MOS.MANAGER.HisWorkingShift;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Update
{
    /// <summary>
    /// Xu ly cap nhat cac truong thong tin lien quan den hoa don ma cho phep sua (vd: thong tin nguoi mua, ...)
    /// </summary>
    class HisTransactionUpdateInfo : BusinessBase
    {
        internal HisTransactionUpdateInfo()
            : base()
        {

        }

        internal HisTransactionUpdateInfo(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisTransactionUpdateInfoSDO sdo, ref HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TRANSACTION raw = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);

                valid = valid && checker.VerifyId(sdo.TransactionId, ref raw);
                valid = valid && checker.IsUnCancel(raw);
                valid = valid && CheckConfig(sdo, raw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
                    HIS_TRANSACTION before = Mapper.Map<HIS_TRANSACTION>(raw);

                    raw.BUYER_ACCOUNT_NUMBER = sdo.BuyerAccountNumber;
                    raw.BUYER_ADDRESS = sdo.BuyerAddress;
                    raw.BUYER_NAME = sdo.BuyerName;
                    raw.BUYER_ORGANIZATION = sdo.BuyerOrganization;
                    raw.BUYER_TAX_CODE = sdo.BuyerTaxCode;
                    raw.PAY_FORM_ID = sdo.PayFormId;
                    raw.TRANSFER_AMOUNT = sdo.TransferAmount;
                    raw.DESCRIPTION = sdo.Description;
                    raw.WORKING_SHIFT_ID = sdo.WorkingShiftId;
                    if (raw.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU)
                    {
                        raw.REPAY_REASON_ID = sdo.RepayReasonId;
                    }

                    if ((sdo.NumOrder.HasValue && sdo.NumOrder != before.NUM_ORDER) || (sdo.AccountBookId.HasValue && sdo.AccountBookId != before.ACCOUNT_BOOK_ID))
                    {
                        if (sdo.NumOrder.HasValue && sdo.NumOrder != raw.NUM_ORDER)
                        {
                            raw.NUM_ORDER = sdo.NumOrder.Value;
                        }

                        V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                        valid = valid && checker.IsUnlockAccountBook(sdo.AccountBookId.HasValue ? sdo.AccountBookId.Value : raw.ACCOUNT_BOOK_ID, ref hisAccountBook);

                        if (sdo.AccountBookId.HasValue && sdo.AccountBookId != raw.ACCOUNT_BOOK_ID)
                        {
                            V_HIS_CASHIER_ROOM cashierRoom = null;
                            valid = valid && checker.HasPermission(sdo.RequestRoomId, ref cashierRoom);
                            valid = valid && checker.HasPermissionAccountBook(hisAccountBook, cashierRoom.ID);
                            raw.ACCOUNT_BOOK_ID = sdo.AccountBookId.Value;
                            if (hisAccountBook.IS_NOT_GEN_TRANSACTION_ORDER != MOS.UTILITY.Constant.IS_TRUE)
                            {
                                raw.NUM_ORDER = 0;
                            }
                        }

                        //sua so chung tu khong duoc trung voi so da co
                        valid = valid && checker.IsValidNumOrderUpdate(raw, hisAccountBook);
                    }

                    if (!valid)
                    {
                        return false;
                    }

                    if (!DAOWorker.HisTransactionDAO.Update(raw))
                    {
                        return false;
                    }
                    result = true;
                    resultData = raw;

                    new EventLogGenerator(EventLog.Enum.HisTransaction_CapNhatThongTin, this.LogInfo(before), this.LogInfo(resultData)).TransactionCode(raw.TRANSACTION_CODE).Run();
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

        private bool CheckConfig(HisTransactionUpdateInfoSDO sdo, HIS_TRANSACTION raw)
        {
            bool result = true;
            try
            {
                //Chỉ cho phép sửa sổ thu chi (account_book_id) và số chứng từ (num_order) trong trường hợp key cấu hình "MOS.HIS_TRANSACTION.ALLOW_UPDATE_ACCOUNT_BOOK" được bật
                if (!Config.HisTransactionCFG.ALLOW_UPDATE_ACCOUNT_BOOK)
                {
                    result = result && (!sdo.AccountBookId.HasValue || (sdo.AccountBookId.HasValue && sdo.AccountBookId == raw.ACCOUNT_BOOK_ID));
                    result = result && (!sdo.NumOrder.HasValue || (sdo.NumOrder.HasValue && sdo.NumOrder == raw.NUM_ORDER));
                }

                if (!result)
                {
                    if (sdo.NumOrder.HasValue && !sdo.AccountBookId.HasValue)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_KhongChoPhepSuSoChungTu);
                    }
                    else if (!sdo.NumOrder.HasValue && sdo.AccountBookId.HasValue)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_KhongChoPhepSuaSoThuChi);
                    }
                    else
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_KhongChoPhepSuaSoThuChiVaSoChungTu);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private string LogInfo(HIS_TRANSACTION input)
        {
            string rs = "";
            try
            {
                string name = LogCommonUtil.GetEventLogContent(EventLog.Enum.TenNguoiMua);
                string address = LogCommonUtil.GetEventLogContent(EventLog.Enum.DiaChiNguoiMua);
                string accountNumber = LogCommonUtil.GetEventLogContent(EventLog.Enum.SoTaiKhoanNguoiMua);
                string taxcode = LogCommonUtil.GetEventLogContent(EventLog.Enum.MaSoThueNguoiMua);
                string organization = LogCommonUtil.GetEventLogContent(EventLog.Enum.CongTyNguoiMua);
                string payForm = LogCommonUtil.GetEventLogContent(EventLog.Enum.HinhThucGiaoDich);
                string transferAmount = LogCommonUtil.GetEventLogContent(EventLog.Enum.SoTienChuyenKhoan);
                string description = LogCommonUtil.GetEventLogContent(EventLog.Enum.MoTa);
                string WorkingShift = LogCommonUtil.GetEventLogContent(EventLog.Enum.CaLamViec);
                string AccountBook = LogCommonUtil.GetEventLogContent(EventLog.Enum.SoThuChi);
                string NumOrder = LogCommonUtil.GetEventLogContent(EventLog.Enum.SoChungTu);
                string RepayReason = LogCommonUtil.GetEventLogContent(EventLog.Enum.LyDoHoanUng);
                string payFormName = "";
                HIS_PAY_FORM pF = new HisPayFormGet().GetById(input.PAY_FORM_ID);
                payFormName = pF != null ? pF.PAY_FORM_NAME : "";

                string workingShiftName = "";
                HIS_WORKING_SHIFT wf = new HisWorkingShiftGet().GetById(input.WORKING_SHIFT_ID ?? 0);
                workingShiftName = wf != null ? wf.WORKING_SHIFT_NAME : "";
                string accountBookName = "";
                HIS_ACCOUNT_BOOK accb = new HisAccountBookGet().GetById(input.ACCOUNT_BOOK_ID);
                accountBookName = accb != null ? accb.ACCOUNT_BOOK_NAME : "";
                string repayReasonName = "";
                HIS_REPAY_REASON rp = new HisRepayReasonGet().GetById(input.REPAY_REASON_ID ?? 0);
                repayReasonName = rp != null ? rp.REPAY_REASON_NAME : "";

                rs = string.Format("{0}: {1}; {2}: {3}; {4}: {5}; {6}: {7}; {8}: {9}; {10}: {11}; {12}: {13}; {14}: {15}; {16}: {17}; {18}: {19}; {20}: {21}; {22}:{23}",
                    name, CommonUtil.NVL(input.BUYER_NAME),
                    address, CommonUtil.NVL(input.BUYER_ADDRESS),
                    accountNumber, CommonUtil.NVL(input.BUYER_ACCOUNT_NUMBER),
                    taxcode, CommonUtil.NVL(input.BUYER_TAX_CODE),
                    organization, CommonUtil.NVL(input.BUYER_ORGANIZATION),
                    payForm, CommonUtil.NVL(payFormName),
                    transferAmount, CommonUtil.NVL(input.TRANSFER_AMOUNT),
                    description, CommonUtil.NVL(input.DESCRIPTION),
                    WorkingShift, CommonUtil.NVL(workingShiftName),
                    AccountBook, CommonUtil.NVL(accountBookName),
                    NumOrder, CommonUtil.NVL(input.NUM_ORDER),
                    RepayReason, CommonUtil.NVL(repayReasonName)
                    );
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return rs;
        }
    }
}
