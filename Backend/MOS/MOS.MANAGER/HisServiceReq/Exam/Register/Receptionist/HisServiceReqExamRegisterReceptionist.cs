using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Token;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using AutoMapper;
using MOS.MANAGER.HisServiceReq.Exam.Register;
using MOS.MANAGER.HisServiceReq.Exam.Register.Billing;
using MOS.MANAGER.HisServiceReq.Exam.Register.Deposit;
using MOS.MANAGER.HisServiceReq.Exam.Register.Epayment;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Receptionist
{
    /// <summary>
    /// Dang ky tiep don su dung boi nhan vien tiep don (tai chuc nang "Tiep don 1", "Tiep don 2")
    /// </summary>
    class HisServiceReqExamRegisterReceptionist : BusinessBase
    {
        private HisServiceReqExamRegister hisServiceReqExamRegister;
        private BillProcessor billProcessor;
        private DepositProcessor depositProcessor;

        internal HisServiceReqExamRegisterReceptionist()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqExamRegisterReceptionist(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqExamRegister = new HisServiceReqExamRegister(param);
            this.billProcessor = new BillProcessor(param);
            this.depositProcessor = new DepositProcessor(param);
        }

        internal bool Run(HisServiceReqExamRegisterSDO data, ref HisServiceReqExamRegisterResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool isAuthorizedTransaction = false;
                bool valid = data != null;

                V_HIS_CASHIER_ROOM cashierRoom = null;
                HisServiceReqExamRegisterReceptionistCheck checker = new HisServiceReqExamRegisterReceptionistCheck(param);
                valid = valid && checker.IsValidData(data, ref isAuthorizedTransaction, ref cashierRoom);

                if (valid)
                {
                    HIS_TREATMENT treatment = null;
                    WorkPlaceSDO workPlace = null;

                    if (this.hisServiceReqExamRegister.Create(data, false, ref resultData, ref treatment, ref workPlace))
                    {
                        List<V_HIS_TRANSACTION> transactions = new List<V_HIS_TRANSACTION>();
                        List<HIS_SERE_SERV_BILL> sereServBills = new List<HIS_SERE_SERV_BILL>();
                        List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = new List<HIS_SERE_SERV_DEPOSIT>();

                        if (!this.billProcessor.Run(data, treatment, cashierRoom, resultData.SereServs, resultData.ServiceReqs, workPlace, ref transactions, ref sereServBills))
                        {
                            throw new Exception("Tu dong tao bill that bai. Du lieu se bi rollback. Ket thuc nghiep vu");
                        }

                        if (!this.depositProcessor.Run(data, treatment, cashierRoom, isAuthorizedTransaction, resultData.SereServs, resultData.ServiceReqs, ref transactions, ref sereServDeposits))
                        {
                            throw new Exception("Tu dong tao deposit that bai. Du lieu se bi rollback. Ket thuc nghiep vu");
                        }

                        resultData.Transactions = transactions;
                        resultData.SereServBills = sereServBills;
                        resultData.SereServDeposits = sereServDeposits;

                        //Xu ly nghiep vu tu dong thanh toan tien qua the
                        if (data.IsUsingEpayment)
                        {
                            List<V_HIS_TRANSACTION> collectedTransactions = null;
                            new EpaymentProcessor(param).Run(treatment, resultData.Transactions, ref collectedTransactions);

                            resultData.CollectedTransactions = collectedTransactions;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultData = null;
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Rollback du lieu
        /// </summary>
        /// <returns></returns>
        internal void RollbackData()
        {
            this.depositProcessor.Rollback();
            this.billProcessor.Rollback();
            this.hisServiceReqExamRegister.RollbackData();
        }
    }
}
