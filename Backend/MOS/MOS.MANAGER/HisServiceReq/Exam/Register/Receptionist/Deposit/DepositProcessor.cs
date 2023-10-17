using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Deposit
{
    class DepositProcessor : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisSereServDepositCreate hisSereServDepositCreate;

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

        internal bool Run(HisServiceReqExamRegisterSDO data, HIS_TREATMENT treatment, V_HIS_CASHIER_ROOM cashierRoom, bool isAuthorizedTransaction, List<V_HIS_SERE_SERV> allSereServs, List<V_HIS_SERVICE_REQ> serviceReqs, ref List<V_HIS_TRANSACTION> outTransactions, ref List<HIS_SERE_SERV_DEPOSIT> outSereServDeposits)
        {
            bool result = false;
            try
            {
                if (!data.IsAutoCreateDepositForNonBhyt || data.IsNotRequireFee == Constant.IS_TRUE)
                {
                    return true;
                }

                List<V_HIS_SERE_SERV> requireFeeSereServs = null;
                HIS_TRANSACTION transaction = null;
                List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = null;

                this.ConvertToDeposit(allSereServs, serviceReqs, data, cashierRoom, ref requireFeeSereServs, ref transaction, ref sereServDeposits);
                if (transaction == null)
                {
                    return true;
                }

                V_HIS_ACCOUNT_BOOK accountBook = new HisAccountBookGet().GetViewById(data.AccountBookId.Value);
                bool valid = true;
                DepositCheck checker = new DepositCheck(param);
                valid = valid && checker.Run(transaction, accountBook);

                if (valid)
                {
                    transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU;

                    this.ProcessTransactionDeposit(transaction, sereServDeposits, treatment, isAuthorizedTransaction);
                    this.ProcessSereServDeposit(sereServDeposits, requireFeeSereServs, transaction);


                    V_HIS_TRANSACTION tran = new HisTransactionGet(param).GetViewById(transaction.ID);

                    outTransactions.Add(tran);
                    outSereServDeposits.AddRange(sereServDeposits);

                    result = true;
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_TaoGiaoDichTamUng, transaction.AMOUNT).TreatmentCode(treatment.TREATMENT_CODE).TransactionCode(transaction.TRANSACTION_CODE).Run();
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private void ProcessTransactionDeposit(HIS_TRANSACTION transaction, List<HIS_SERE_SERV_DEPOSIT> sereServDeposits, HIS_TREATMENT treatment, bool isAuthorizedTransaction)
        {
            if (IsNotNullOrEmpty(sereServDeposits))
            {
                transaction.TDL_SERE_SERV_DEPOSIT_COUNT = (long)sereServDeposits.Count;
            }
            HisTransactionUtil.SetTreatmentFeeInfo(transaction);

            if (!this.hisTransactionCreate.Create(transaction, treatment, isAuthorizedTransaction))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        private void ProcessSereServDeposit(List<HIS_SERE_SERV_DEPOSIT> sereServDeposits, List<V_HIS_SERE_SERV> requireFeeSereServs, HIS_TRANSACTION transaction)
        {
            if (IsNotNullOrEmpty(sereServDeposits))
            {
                Mapper.CreateMap<V_HIS_SERE_SERV, HIS_SERE_SERV>();
                List<HIS_SERE_SERV> sereServs = Mapper.Map<List<HIS_SERE_SERV>>(requireFeeSereServs);

                foreach (HIS_SERE_SERV_DEPOSIT d in sereServDeposits)
                {
                    d.DEPOSIT_ID = transaction.ID;
                    HisSereServDepositUtil.SetTdl(d, sereServs.FirstOrDefault(o => o.ID == d.SERE_SERV_ID));
                }

                if (!this.hisSereServDepositCreate.CreateList(sereServDeposits))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ConvertToDeposit(List<V_HIS_SERE_SERV> sereServs, List<V_HIS_SERVICE_REQ> serviceReqs, HisServiceReqExamRegisterSDO data, V_HIS_CASHIER_ROOM cashierRoom, ref List<V_HIS_SERE_SERV> requireFeeSereServs, ref HIS_TRANSACTION transaction, ref List<HIS_SERE_SERV_DEPOSIT> sereServDeposits)
        {
            if (IsNotNullOrEmpty(sereServs) && IsNotNullOrEmpty(serviceReqs) && data.CashierWorkingRoomId.HasValue && data.AccountBookId.HasValue)
            {
                //Lay ra cac chi dinh y/c dong tien
                List<long> requireFeeServiceReqIds = serviceReqs.Where(o => o.IS_NOT_REQUIRE_FEE != Constant.IS_TRUE).Select(o => o.ID).ToList();
                requireFeeSereServs = sereServs
                    .Where(o => requireFeeServiceReqIds != null
                        && requireFeeServiceReqIds.Contains(o.SERVICE_REQ_ID.Value)
                        && o.VIR_TOTAL_PATIENT_PRICE > 0
                        && o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList();
                if (IsNotNullOrEmpty(requireFeeSereServs))
                {
                    transaction = new HIS_TRANSACTION();
                    transaction.AMOUNT = requireFeeSereServs.Sum(o => (o.VIR_TOTAL_PATIENT_PRICE ?? 0));
                    transaction.CASHIER_LOGINNAME = String.IsNullOrWhiteSpace(data.CashierLoginName) ? Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName() : data.CashierLoginName;
                    transaction.CASHIER_USERNAME = String.IsNullOrWhiteSpace(data.CashierLoginName) ? Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName() : data.CashierUserName;
                    transaction.CASHIER_ROOM_ID = cashierRoom.ID;
                    transaction.TRANSACTION_TIME = Inventec.Common.DateTime.Get.Now().Value;
                    transaction.TREATMENT_ID = data.TreatmentId;
                    transaction.ACCOUNT_BOOK_ID = data.AccountBookId.Value;
                    transaction.PAY_FORM_ID = data.PayFormId.HasValue ? data.PayFormId.Value : IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__TM;

                    if (data.TransNumOrder.HasValue)
                    {
                        transaction.NUM_ORDER = data.TransNumOrder.Value;
                    }

                    sereServDeposits = new List<HIS_SERE_SERV_DEPOSIT>();
                    foreach (var ss in requireFeeSereServs)
                    {
                        HIS_SERE_SERV_DEPOSIT s = new HIS_SERE_SERV_DEPOSIT();
                        s.AMOUNT = ss.VIR_TOTAL_PATIENT_PRICE ?? 0;
                        s.SERE_SERV_ID = ss.ID;
                        sereServDeposits.Add(s);
                    }
                }
            }
        }

        internal void Rollback()
        {
            if (this.hisSereServDepositCreate != null) this.hisSereServDepositCreate.RollbackData();
            if (this.hisTransactionCreate != null) this.hisTransactionCreate.RollbackData();
        }

    }
}
