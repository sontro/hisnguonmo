using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisCaroAccountBook;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisUserAccountBook;
using MOS.MANAGER.YttDeposit;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTT.SDO;

namespace MOS.MANAGER.HisServiceReq.Exam.Process.Deposit
{
    class DepositProcessor : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisSereServDepositCreate hisSereServDepositCreate;

        private HIS_TRANSACTION recentHisTransaction;

        internal DepositProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisSereServDepositCreate = new HisSereServDepositCreate(param);
        }

        internal void Run(long requestRoomId, HIS_TREATMENT treatment, HIS_SERE_SERV sereServ, ref HIS_TRANSACTION transaction, ref HIS_SERE_SERV_DEPOSIT sereServDeposit)
        {
            try
            {
                long? cashierRoomId = null;
                long? accountBookId = null;
                string theBranchCode = null;
                HIS_CARD hisCard = null;

                if (new PrepareProcessor(param).IsAllowDeposit(requestRoomId, treatment, sereServ, ref cashierRoomId, ref accountBookId, ref hisCard, ref theBranchCode))
                {
                    long amount = (long)Math.Round(sereServ.VIR_TOTAL_PATIENT_PRICE.Value, 0);
                    YttHisDepositResultSDO yttResult = new YttDepositCreate(param).Create(amount, hisCard.SERVICE_CODE, theBranchCode);
                    if (yttResult != null && yttResult.ResultCode == YttDepositCreate.SUCCESS)
                    {
                        this.ProcessTransaction(yttResult.TransactionCode, yttResult.TransactionTime, cashierRoomId.Value, accountBookId.Value, sereServ, treatment, hisCard, ref transaction);
                        this.ProcessSereServDeposit(sereServ, transaction, ref sereServDeposit);

                        new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_TaoGiaoDichTamUng, this.recentHisTransaction.AMOUNT).TreatmentCode(treatment.TREATMENT_CODE).TransactionCode(this.recentHisTransaction.TRANSACTION_CODE).Run();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
            }
        }

        private void ProcessTransaction(string yttTransactionCode, long? yttTransactionTime, long cashierRoomId, long accountBookId, HIS_SERE_SERV sereServ, HIS_TREATMENT treatment, HIS_CARD hisCard, ref HIS_TRANSACTION transaction)
        {
            transaction = new HIS_TRANSACTION();
            transaction.AMOUNT = sereServ.VIR_TOTAL_PATIENT_PRICE.Value;
            transaction.CASHIER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            transaction.CASHIER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            transaction.CASHIER_ROOM_ID = cashierRoomId;
            transaction.TRANSACTION_TIME = Inventec.Common.DateTime.Get.Now().Value;
            transaction.TREATMENT_ID = treatment.ID;
            transaction.ACCOUNT_BOOK_ID = accountBookId;
            transaction.PAY_FORM_ID = IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE;
            transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU;
            transaction.TDL_SERE_SERV_DEPOSIT_COUNT = 1;

            HisTransactionUtil.SetEpaymentInfo(transaction, hisCard, yttTransactionCode, yttTransactionTime);
            HisTransactionUtil.SetTreatmentFeeInfo(transaction);

            if (!this.hisTransactionCreate.Create(transaction, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisTransaction = transaction;
        }

        private void ProcessSereServDeposit(HIS_SERE_SERV sereServ, HIS_TRANSACTION transaction, ref HIS_SERE_SERV_DEPOSIT sereServDeposit)
        {
            if (sereServ != null)
            {
                sereServDeposit = new HIS_SERE_SERV_DEPOSIT();
                sereServDeposit.AMOUNT = sereServ.VIR_TOTAL_PATIENT_PRICE ?? 0;
                sereServDeposit.SERE_SERV_ID = sereServ.ID;
                sereServDeposit.DEPOSIT_ID = transaction.ID;
                HisSereServDepositUtil.SetTdl(sereServDeposit, sereServ);

                if (!this.hisSereServDepositCreate.Create(sereServDeposit))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        internal void Rollback()
        {
            this.hisSereServDepositCreate.RollbackData();
            this.hisTransactionCreate.RollbackData();
        }
    }
}
