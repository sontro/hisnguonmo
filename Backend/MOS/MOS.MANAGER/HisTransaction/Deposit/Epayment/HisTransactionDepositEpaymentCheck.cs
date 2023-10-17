using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisPatient;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Deposit.Epayment
{
    class HisTransactionDepositEpaymentCheck : BusinessBase
    {
        /// <summary>
        /// Quy dinh do dai toi thieu cua cac ky tu ngan hang khi verify
        /// </summary>
        private const int MIN_LENGTH_OF_LAST_DIGITS_OF_BANK_CARD_CODE = 4;

        internal HisTransactionDepositEpaymentCheck()
            : base()
        {

        }

        internal HisTransactionDepositEpaymentCheck(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// Validate du lieu trong truong hop xuat hien nghiep vu giao dich dien tu (giao dich qua he thong The)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisTransactionDepositSDO data, HIS_TREATMENT treatment, ref long? epaymentAmount, ref string theBranchCode, ref HIS_CARD hisCard)
        {
            try
            {
                //Neu hinh thuc thanh toan la "Mot the quoc gia"
                if (treatment != null
                    && data.Transaction != null
                    && data.Transaction.PAY_FORM_ID == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE
                    && data.Transaction.AMOUNT > 0)
                {
                    //Neu option la "auto-pay", giao dich se duoc tao boi backend ma khong can quet the thiet bi
                    if (EpaymentCFG.CASHIER_ROOM_PAYMENT_OPTION == EpaymentCFG.CashierRoomPaymentOption.AUTO_PAY)
                    {
                        if (string.IsNullOrWhiteSpace(data.LastDigitsOfBankCardCode) || data.LastDigitsOfBankCardCode.Length < MIN_LENGTH_OF_LAST_DIGITS_OF_BANK_CARD_CODE)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_ThieuThongTinCacKiTuCuoiSoTheNganHang, MIN_LENGTH_OF_LAST_DIGITS_OF_BANK_CARD_CODE.ToString());
                            return false;
                        }

                        //Lay so du cua tai khoan the cua BN
                        decimal? balance = new HisPatientBalance().GetCardBalance(treatment.PATIENT_ID, data.LastDigitsOfBankCardCode, HisPatientBalance.CardFilterOption.LAST_DIGITS_OF_BANK_CARD_CODE, ref theBranchCode, ref hisCard);

                        if (hisCard == null)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_CacKyTuCuoiSoTheNganHangKhongTonTai, MIN_LENGTH_OF_LAST_DIGITS_OF_BANK_CARD_CODE.ToString());
                            return false;
                        }

                        //So tien thuc hien giao dich cua he thong ngan hang can lam tron
                        epaymentAmount = (long)Math.Round(data.Transaction.AMOUNT, 0);
                        if (balance == null || balance < epaymentAmount)
                        {
                            string balanceAmountStr = balance == null ? "" : balance.Value.ToString();
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_TaiKhoanTheKhongDuSoDu, balanceAmountStr, epaymentAmount.ToString());
                            return false;
                        }
                    }
                    //Neu la option "manual-pay" thi giao dich sang he thong the se thuc hien tren may tram (thong qua app The.desktop), sau do client
                    //se truyen thong tin giao dich len de backend luu thong tin vao CSDL
                    else if (EpaymentCFG.CASHIER_ROOM_PAYMENT_OPTION == EpaymentCFG.CashierRoomPaymentOption.MANUAL_PAY)
                    {
                        if (string.IsNullOrWhiteSpace(data.Transaction.TIG_TRANSACTION_CODE)
                        || !data.Transaction.TIG_TRANSACTION_TIME.HasValue
                        || string.IsNullOrWhiteSpace(data.CardCode))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            LogSystem.Warn("data.PayFormId == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE nhung data.TigTransactionCode, data.TigTransactionTime hoac data.CardCode khong co gia tri");
                            return false;
                        }

                        //Ho tro ca truyen len serviceCode hoac cardCode
                        hisCard = new HisCardGet().GetByCode(data.CardCode);
                        if (hisCard == null)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCard_KhongLayDuocThongTinTheYTe, data.CardCode);
                            return false;
                        }
                        if (hisCard.PATIENT_ID != treatment.PATIENT_ID)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCard_KhongTrungThongTinVoiChuThe, treatment.TDL_PATIENT_NAME, data.CardCode);
                            return false;
                        }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }
    }
}
