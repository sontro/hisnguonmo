﻿using AutoMapper;
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

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Kiosk.Epayment.Deposit
{
    class KioskEpaymentDepositProcessor : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisSereServDepositCreate hisSereServDepositCreate;

        private HIS_TRANSACTION recentHisTransaction;

        internal KioskEpaymentDepositProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisSereServDepositCreate = new HisSereServDepositCreate(param);
        }

        internal void Run(long requestRoomId, string serviceCode, HIS_TREATMENT treatment, List<V_HIS_SERE_SERV> sereServs, List<V_HIS_SERVICE_REQ> serviceReqs, ref HIS_TRANSACTION transaction, ref List<HIS_SERE_SERV_DEPOSIT> sereServDeposit)
        {
            try
            {
                long? cashierRoomId = null;
                long? accountBookId = null;
                string theBranchCode = null;
                HIS_CARD hisCard = null;

                bool isAutoPay = new KioskEpaymentDepositPrepareProcessor(param).Run(requestRoomId, treatment, sereServs, serviceReqs, serviceCode, ref cashierRoomId, ref accountBookId, ref theBranchCode, ref hisCard);

                if (isAutoPay && cashierRoomId.HasValue && accountBookId.HasValue && hisCard != null && !string.IsNullOrWhiteSpace(hisCard.SERVICE_CODE))
                {
                    decimal total = sereServs.Sum(o => (o.VIR_TOTAL_PATIENT_PRICE ?? 0));
                    long amount = (long) Math.Round(total, 0);
                    YttHisDepositResultSDO yttResult = new YttDepositCreate(param).Create(amount, hisCard.SERVICE_CODE, theBranchCode);
                    if (yttResult != null && yttResult.ResultCode == YttDepositCreate.SUCCESS)
                    {
                        this.ProcessTransaction(yttResult.TransactionCode, yttResult.TransactionTime, cashierRoomId.Value, accountBookId.Value, sereServs, hisCard, treatment, ref transaction);
                        this.ProcessSereServDeposit(sereServs, transaction, ref sereServDeposit);

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

        private void ProcessTransaction(string yttTransactionCode, long? yttTransactionTime, long cashierRoomId, long accountBookId, List<V_HIS_SERE_SERV> sereServs, HIS_CARD hisCard, HIS_TREATMENT treatment, ref HIS_TRANSACTION transaction)
        {
            transaction = new HIS_TRANSACTION();
            
            transaction.AMOUNT = sereServs.Sum(o => (o.VIR_TOTAL_PATIENT_PRICE ?? 0));
            transaction.CASHIER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            transaction.CASHIER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            transaction.CASHIER_ROOM_ID = cashierRoomId;
            transaction.TRANSACTION_TIME = Inventec.Common.DateTime.Get.Now().Value;
            transaction.TREATMENT_ID = treatment.ID;
            transaction.ACCOUNT_BOOK_ID = accountBookId;
            transaction.PAY_FORM_ID = IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE;
            transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU;
            transaction.TDL_SERE_SERV_DEPOSIT_COUNT = (long)sereServs.Count;

            HisTransactionUtil.SetEpaymentInfo(transaction, hisCard, yttTransactionCode, yttTransactionTime);

            HisTransactionUtil.SetTreatmentFeeInfo(transaction);

            if (!this.hisTransactionCreate.Create(transaction, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisTransaction = transaction;
        }

        private void ProcessSereServDeposit(List<V_HIS_SERE_SERV> forPaymentsSereServs, HIS_TRANSACTION transaction, ref List<HIS_SERE_SERV_DEPOSIT> sereServDeposits)
        {
            if (IsNotNullOrEmpty(forPaymentsSereServs))
            {
                sereServDeposits = new List<HIS_SERE_SERV_DEPOSIT>();
                foreach (V_HIS_SERE_SERV ss in forPaymentsSereServs)
                {
                    HIS_SERE_SERV_DEPOSIT d = new HIS_SERE_SERV_DEPOSIT();
                    d.AMOUNT = ss.VIR_TOTAL_PATIENT_PRICE ?? 0;
                    d.SERE_SERV_ID = ss.ID;
                    d.DEPOSIT_ID = transaction.ID;
                    HisSereServDepositUtil.SetTdl(d, ss);
                    sereServDeposits.Add(d);
                }

                if (!this.hisSereServDepositCreate.CreateList(sereServDeposits))
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
