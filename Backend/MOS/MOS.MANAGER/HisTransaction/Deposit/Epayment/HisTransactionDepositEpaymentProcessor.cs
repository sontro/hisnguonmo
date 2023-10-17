using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.YttDeposit;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTT.SDO;

namespace MOS.MANAGER.HisTransaction.Deposit.Epayment
{
    class HisTransactionDepositEpaymentProcessor : BusinessBase
    {
        internal HisTransactionDepositEpaymentProcessor()
            : base()
        {

        }

        internal HisTransactionDepositEpaymentProcessor(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// Xu ly nghiep vu lien quan den thanh toan dien tu (giao dich sang he thong the)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisTransactionDepositSDO data, long? epaymentAmount, long patientId, string theBranchCode, HIS_CARD hisCard)
        {
            try
            {
                //Neu hinh thuc thanh toan la "Mot the quoc gia" va co bat cau hinh "tu dong giao dich o phong thu ngan"
                if (data.Transaction != null
                    && data.Transaction.PAY_FORM_ID == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE
                    && data.Transaction.AMOUNT > 0)
                {
                    //Neu la option "manual-pay" thi giao dich sang he thong the se thuc hien tren may tram (thong qua app The.desktop), sau do client
                    //se truyen thong tin giao dich len de backend luu thong tin vao CSDL
                    if (EpaymentCFG.CASHIER_ROOM_PAYMENT_OPTION == EpaymentCFG.CashierRoomPaymentOption.MANUAL_PAY)
                    {
                        HisTransactionUtil.SetCardInfo(data.Transaction, hisCard);
                    }
                    ///Neu la option "auto-pay" thi giao dich sang he thong the se thuc hien tren backend
                    else if (EpaymentCFG.CASHIER_ROOM_PAYMENT_OPTION == EpaymentCFG.CashierRoomPaymentOption.AUTO_PAY
                        && !string.IsNullOrWhiteSpace(theBranchCode)
                        && hisCard != null
                        && epaymentAmount.HasValue && epaymentAmount > 0)
                    {
                        YttHisDepositResultSDO yttResult = new YttDepositCreate(param).Create(epaymentAmount.Value, hisCard.SERVICE_CODE, theBranchCode);

                        if (yttResult != null && yttResult.ResultCode == YttDepositCreate.SUCCESS)
                        {
                            HisTransactionUtil.SetEpaymentInfo(data.Transaction, hisCard, yttResult.TransactionCode, yttResult.TransactionTime);
                            return true;
                        }
                        else
                        {
                            string yttResultCode = yttResult != null ? yttResult.ResultCode : "";
                            string yttResultDesc = yttResult != null ? yttResult.ResultDesc : "";

                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichHeThongTheThatBai, yttResultCode, yttResultDesc);
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
