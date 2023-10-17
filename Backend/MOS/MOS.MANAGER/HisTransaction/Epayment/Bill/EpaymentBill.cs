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
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisUserAccountBook;
using MOS.MANAGER.YttDeposit;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTT.SDO;

namespace MOS.MANAGER.HisTransaction.Epayment.Bill
{
    class EpaymentBill : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisSereServBillCreate hisSereServBillCreate;

        private HIS_TRANSACTION recentHisTransaction;

        internal EpaymentBill()
        {
            this.Init();
        }

        internal EpaymentBill(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisSereServBillCreate = new HisSereServBillCreate(param);
        }

        internal bool Run(EpaymentBillSDO data, ref EpaymentBillResultSDO resultData)
        {
            try
            {
                long? cashierRoomId = null;
                long? accountBookId = null;
                string theBranchCode = null;
                HIS_CARD hisCard = null;
                List<V_HIS_SERE_SERV> forPaymentsSereServs = null;

                WorkPlaceSDO workPlace = null;
                HIS_TREATMENT treatment = null;

                EpaymentBillCheck checker = new EpaymentBillCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);

                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && treatChecker.VerifyId(data.TreatmentId, ref treatment);
                valid = valid && treatChecker.IsPause(treatment);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.IsValidData(data, treatment, workPlace, ref cashierRoomId, ref accountBookId, ref theBranchCode, ref forPaymentsSereServs, ref hisCard);

                if (valid)
                {
                    if (IsNotNullOrEmpty(forPaymentsSereServs) && cashierRoomId.HasValue && accountBookId.HasValue)
                    {
                        HIS_TRANSACTION transaction = null;
                        List<HIS_SERE_SERV_BILL> sereServBills = null;

                        decimal total = forPaymentsSereServs.Sum(o => (o.VIR_TOTAL_PATIENT_PRICE ?? 0));
                        long amount = (long)Math.Round(total, 0);

                        YttHisDepositResultSDO yttResult = new YttDepositCreate(param).Create(amount, hisCard.SERVICE_CODE, theBranchCode);

                        if (yttResult != null && yttResult.ResultCode == YttDepositCreate.SUCCESS)
                        {
                            this.ProcessTransaction(yttResult.TransactionCode, yttResult.TransactionTime, cashierRoomId.Value, accountBookId.Value, forPaymentsSereServs, hisCard, treatment, ref transaction);
                            this.ProcessSereServBill(forPaymentsSereServs, transaction, ref sereServBills);

                            new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_TaoGiaoDichTamUng, this.recentHisTransaction.AMOUNT).TreatmentCode(treatment.TREATMENT_CODE).TransactionCode(this.recentHisTransaction.TRANSACTION_CODE).Run();

                            resultData = new EpaymentBillResultSDO();
                            resultData.SereServBills = sereServBills;
                            resultData.Transaction = transaction != null ? new HisTransactionGet().GetViewById(transaction.ID) : null;
                            resultData.SereServs = forPaymentsSereServs;
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
            }
            return false;
        }

        private void ProcessTransaction(string yttTransactionCode, long? yttTransactionTime, long cashierRoomId, long accountBookId, List<V_HIS_SERE_SERV> forPaymentsSereServs, HIS_CARD hisCard, HIS_TREATMENT treatment, ref HIS_TRANSACTION transaction)
        {
            transaction = new HIS_TRANSACTION();
            transaction.AMOUNT = forPaymentsSereServs.Sum(o => (o.VIR_TOTAL_PATIENT_PRICE ?? 0));
            transaction.SERE_SERV_AMOUNT = forPaymentsSereServs.Sum(o => (o.VIR_TOTAL_PATIENT_PRICE ?? 0));
            transaction.CASHIER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            transaction.CASHIER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            transaction.CASHIER_ROOM_ID = cashierRoomId;
            transaction.TRANSACTION_TIME = Inventec.Common.DateTime.Get.Now().Value;
            transaction.TREATMENT_ID = treatment.ID;
            transaction.ACCOUNT_BOOK_ID = accountBookId;
            transaction.PAY_FORM_ID = IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE;
            transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
            
            HisTransactionUtil.SetEpaymentInfo(transaction, hisCard, yttTransactionCode, yttTransactionTime);

            HisTransactionUtil.SetTreatmentFeeInfo(transaction);

            if (!this.hisTransactionCreate.Create(transaction, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisTransaction = transaction;
        }

        private void ProcessSereServBill(List<V_HIS_SERE_SERV> forPaymentsSereServs, HIS_TRANSACTION transaction, ref List<HIS_SERE_SERV_BILL> sereServBills)
        {
            if (IsNotNullOrEmpty(forPaymentsSereServs))
            {
                sereServBills = new List<HIS_SERE_SERV_BILL>();
                foreach (V_HIS_SERE_SERV ss in forPaymentsSereServs)
                {
                    HIS_SERE_SERV_BILL b = new HIS_SERE_SERV_BILL();
                    b.PRICE = ss.VIR_TOTAL_PATIENT_PRICE ?? 0;
                    b.SERE_SERV_ID = ss.ID;
                    b.BILL_ID = transaction.ID;
                    b.TDL_BILL_TYPE_ID = transaction.BILL_TYPE_ID;
                    b.TDL_TREATMENT_ID = transaction.TREATMENT_ID.Value;
                    HisSereServBillUtil.SetTdl(b, ss);
                    sereServBills.Add(b);
                }

                if (!this.hisSereServBillCreate.CreateList(sereServBills))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        internal void Rollback()
        {
            this.hisSereServBillCreate.RollbackData();
            this.hisTransactionCreate.RollbackData();
        }
    }
}
