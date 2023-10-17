using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisServiceReq.Exam.Register.Kiosk.Epayment.Deposit;
using MOS.MANAGER.HisTransaction;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Kiosk
{
    /// <summary>
    /// Dang ky kham tren cay kiosk
    /// </summary>
    class HisServiceReqExamRegisterKiosk : BusinessBase
    {
        private HisServiceReqExamRegister hisServiceReqExamRegister;

        internal HisServiceReqExamRegisterKiosk()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqExamRegisterKiosk(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqExamRegister = new HisServiceReqExamRegister(param);

        }

        /// <summary>
        /// Dang ky kham tren cay kiosk
        /// </summary>
        /// <param name="tdo"></param>
        /// <param name="resultData"></param>
        /// <returns></returns>
        internal bool Run(HisExamRegisterKioskSDO tdo, ref HisServiceReqExamRegisterResultSDO resultData)
        {
            bool result = false;
            try
            {
                HisServiceReqExamRegisterSDO registerSdo = new DataPreparer(param).ToRegisterKioskSdo(tdo);
                HIS_TREATMENT treatment = null;
                WorkPlaceSDO workPlace = null;
                result = this.hisServiceReqExamRegister.Create(registerSdo, false, ref resultData, ref treatment, ref workPlace);

                if (result && resultData != null && IsNotNullOrEmpty(resultData.SereServs) && tdo.CardSDO != null && !string.IsNullOrWhiteSpace(tdo.CardSDO.ServiceCode))
                {
                    HIS_TRANSACTION transaction = null;
                    List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = null;

                    //Xu ly thanh toan va tao giao dich tam thu
                    new KioskEpaymentDepositProcessor(param).Run(tdo.RequestRoomId, tdo.CardSDO.ServiceCode, treatment, resultData.SereServs, resultData.ServiceReqs, ref transaction, ref sereServDeposits);

                    //Neu co du lieu giao dich tao ra thi cap nhat vao ket qua de tra ve font-end
                    if (transaction != null)
                    {
                        resultData.SereServDeposits = sereServDeposits;
                        resultData.DepositedSereServs = resultData.SereServs;

                        if (transaction != null)
                        {
                            V_HIS_TRANSACTION tran = new HisTransactionGet().GetViewById(transaction.ID);
                            resultData.Transactions = new List<V_HIS_TRANSACTION>() { tran };
                        }
                    }
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
    }
}
