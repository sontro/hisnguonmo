using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceChangeReq.CashierApprove
{
    /// <summary>
    /// Xu ly nghiep vu tao yeu cau doi dich vu
    /// </summary>
    class HisServiceChangeReqCashierApprove : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisSereServUpdate hisSereServUpdate;
        private HisServiceChangeReqUpdate hisServiceChangeReqUpdate;
        private HisSeseDepoRepayCreate hisSeseDepoRepayCreate;
        private HisSereServDepositCreate hisSereServDepositCreate;

        internal HisServiceChangeReqCashierApprove()
            : base()
        {
            this.Init();
        }

        internal HisServiceChangeReqCashierApprove(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisServiceChangeReqUpdate = new HisServiceChangeReqUpdate(param);
            this.hisSeseDepoRepayCreate = new HisSeseDepoRepayCreate(param);
            this.hisSereServDepositCreate = new HisSereServDepositCreate(param);
        }

        internal bool Run(HisServiceChangeReqCashierApproveSDO data, ref HisServiceChangeReqCashierApproveResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                
                if (valid)
                {
                    HIS_SERVICE_REQ serviceReq = null;
                    HIS_SERE_SERV oldSereServ = null;
                    HIS_SERE_SERV newSereServ = null;
                    HIS_SERVICE_CHANGE_REQ serviceChangeReq = null;
                    HIS_TREATMENT hisTreatment = null;
                    HIS_SERE_SERV_DEPOSIT sereServDeposit = null;
                    V_HIS_ACCOUNT_BOOK depositAccountBook = null;
                    V_HIS_ACCOUNT_BOOK repayAccountBook = null;

                    WorkPlaceSDO workPlaceSdo = null;

                    HisServiceChangeReqCashierApproveCheck checker = new HisServiceChangeReqCashierApproveCheck(param);
                    HisSereServCheck sereServChecker = new HisSereServCheck(param);
                    HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);
                    HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                    HisTransactionCheck transactionChecker = new HisTransactionCheck(param);

                    valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlaceSdo);
                    valid = valid && checker.IsValidData(data, ref serviceChangeReq, ref serviceReq, ref oldSereServ, ref newSereServ, ref sereServDeposit);
                    valid = valid && checker.IsValidWorkingRoom(workPlaceSdo);
                    valid = valid && sereServChecker.HasNoBill(oldSereServ);
                    valid = valid && sereServChecker.HasNoInvoice(oldSereServ);
                    valid = valid && sereServChecker.HasNoDeposit(data.NewSereServId, true);

                    valid = valid && serviceReqChecker.HasExecute(serviceReq);
                    valid = valid && treatmentChecker.IsUnLock(serviceReq.TREATMENT_ID, ref hisTreatment);
                    valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                    valid = valid && treatmentChecker.IsUnpause(hisTreatment);
                    valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);

                    valid = valid && transactionChecker.HasNotFinancePeriod(workPlaceSdo.CashierRoomId.Value, data.TransactionTime);
                    valid = valid && transactionChecker.IsUnlockAccountBook(data.DepositAccountBookId, ref depositAccountBook);
                    valid = valid && (depositAccountBook == null || depositAccountBook.IS_FOR_DEPOSIT == Constant.IS_TRUE);
                    valid = valid && (depositAccountBook == null || transactionChecker.IsValidNumOrder(data.DepositNumOrder, depositAccountBook));
                    valid = valid && (!data.RepayAccountBookId.HasValue || transactionChecker.IsUnlockAccountBook(data.RepayAccountBookId.Value, ref repayAccountBook));
                    valid = valid && (repayAccountBook == null || repayAccountBook.IS_FOR_REPAY == Constant.IS_TRUE);
                    valid = valid && (repayAccountBook == null || transactionChecker.IsValidNumOrder(data.RepayNumOrder, repayAccountBook));

                    if (valid)
                    {
                        serviceChangeReq.APPROVAL_CASHIER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        serviceChangeReq.APPROVAL_CASHIER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                        if (!this.hisServiceChangeReqUpdate.Update(serviceChangeReq))
                        {
                            throw new Exception("Rollback du lieu");
                        }
                        result = true;
                        HIS_TRANSACTION repayTransaction = null;
                        HIS_TRANSACTION depositTransaction = null;
                        HIS_SESE_DEPO_REPAY seseDepoRepay = null;
                        HIS_SERE_SERV_DEPOSIT newSereServDeposit = null;

                        this.ProcessTransactionRepay(data, workPlaceSdo, hisTreatment, sereServDeposit, ref repayTransaction, ref seseDepoRepay);
                        this.ProcessTransactionDeposit(data, workPlaceSdo, hisTreatment, newSereServ, ref depositTransaction, ref newSereServDeposit);

                        resultData = new HisServiceChangeReqCashierApproveResultSDO();
                        resultData.ServiceChangeReq = serviceChangeReq;
                        resultData.SereServDeposit = newSereServDeposit;
                        resultData.SeseDepoRepay = seseDepoRepay;
                        resultData.Repay = repayTransaction;
                        resultData.Deposit = depositTransaction;

                        result = true;

                        if (repayTransaction != null)
                        {
                            new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_TaoGiaoDichHoanUng, seseDepoRepay.AMOUNT).TreatmentCode(hisTreatment.TREATMENT_CODE).TransactionCode(repayTransaction.TRANSACTION_CODE).Run();
                        }
                        if (depositTransaction != null)
                        {
                            new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_TaoGiaoDichTamUng, newSereServDeposit.AMOUNT).TreatmentCode(hisTreatment.TREATMENT_CODE).TransactionCode(depositTransaction.TRANSACTION_CODE).Run();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessTransactionRepay(HisServiceChangeReqCashierApproveSDO data, WorkPlaceSDO workPlaceSdo, HIS_TREATMENT treatment, HIS_SERE_SERV_DEPOSIT sereServDeposit, ref HIS_TRANSACTION transaction, ref HIS_SESE_DEPO_REPAY seseDepoRepay)
        {
            if (sereServDeposit != null)
            {
                transaction = new HIS_TRANSACTION();
                transaction.TREATMENT_ID = treatment.ID;
                transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU;
                transaction.CASHIER_ROOM_ID = workPlaceSdo.CashierRoomId.Value;
                transaction.ACCOUNT_BOOK_ID = data.RepayAccountBookId.Value;
                transaction.NUM_ORDER = data.RepayNumOrder.HasValue ? data.RepayNumOrder.Value : 0;
                transaction.PAY_FORM_ID = data.RepayPayFormId.Value;
                transaction.REPAY_REASON_ID = data.RepayReasonId;
                transaction.TRANSFER_AMOUNT = data.RepayTransferAmount;
                transaction.AMOUNT = data.RepayAmount.Value;
                transaction.TRANSACTION_TIME = data.TransactionTime;

                if (sereServDeposit != null)
                {
                    transaction.TDL_SESE_DEPO_REPAY_COUNT = 1;
                }
                HisTransactionUtil.SetTreatmentFeeInfo(transaction);

                if (!this.hisTransactionCreate.Create(transaction, treatment))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                HIS_SESE_DEPO_REPAY detail = new HIS_SESE_DEPO_REPAY();
                detail.REPAY_ID = transaction.ID;
                detail.AMOUNT = sereServDeposit.AMOUNT;
                detail.SERE_SERV_DEPOSIT_ID = sereServDeposit.ID;
                HisSeseDepoRepayUtil.SetTdl(detail, sereServDeposit);

                if (!this.hisSeseDepoRepayCreate.Create(detail))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
                seseDepoRepay = detail;
            }
        }

        private void ProcessTransactionDeposit(HisServiceChangeReqCashierApproveSDO data, WorkPlaceSDO workPlaceSdo, HIS_TREATMENT treatment, HIS_SERE_SERV newSereServ, ref HIS_TRANSACTION transaction, ref HIS_SERE_SERV_DEPOSIT newSereServDeposit)
        {
            if (newSereServ != null && newSereServ.VIR_TOTAL_PATIENT_PRICE > 0)
            {
                transaction = new HIS_TRANSACTION();
                transaction.TREATMENT_ID = treatment.ID;
                transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU;
                transaction.CASHIER_ROOM_ID = workPlaceSdo.CashierRoomId.Value;
                transaction.ACCOUNT_BOOK_ID = data.DepositAccountBookId;
                transaction.NUM_ORDER = data.DepositNumOrder.HasValue ? data.DepositNumOrder.Value : 0;
                transaction.PAY_FORM_ID = data.DepositPayFormId;
                transaction.AMOUNT = data.DepositAmount;
                transaction.TRANSFER_AMOUNT = data.DepositTransferAmount;
                transaction.TDL_SERE_SERV_DEPOSIT_COUNT = 1;
                transaction.TRANSACTION_TIME = data.TransactionTime;

                HisTransactionUtil.SetTreatmentFeeInfo(transaction);

                if (!this.hisTransactionCreate.Create(transaction, treatment))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }


                HIS_SERE_SERV_DEPOSIT detail = new HIS_SERE_SERV_DEPOSIT();
                detail.AMOUNT = newSereServ.VIR_TOTAL_PATIENT_PRICE.Value;
                detail.DEPOSIT_ID = transaction.ID;
                detail.SERE_SERV_ID = newSereServ.ID;
                HisSereServDepositUtil.SetTdl(detail, newSereServ);

                if (!this.hisSereServDepositCreate.Create(detail))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
                newSereServDeposit = detail;
            }
        }

        private void Rollback()
        {
            this.hisSereServDepositCreate.RollbackData();
            this.hisSeseDepoRepayCreate.RollbackData();
            this.hisServiceChangeReqUpdate.RollbackData();
        }
    }
}
