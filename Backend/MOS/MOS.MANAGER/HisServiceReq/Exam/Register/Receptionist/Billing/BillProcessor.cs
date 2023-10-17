using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisAccountBook;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Billing
{
    class BillProcessor: BusinessBase
    {
        private List<TransactionBillCreate> transactionCreates;

        internal BillProcessor()
            : base()
        {
        }

        internal BillProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        public bool Run(HisServiceReqExamRegisterSDO data, HIS_TREATMENT treatment, V_HIS_CASHIER_ROOM cashierRoom, List<V_HIS_SERE_SERV> sereServs, List<V_HIS_SERVICE_REQ> serviceReqs, WorkPlaceSDO workPlaceSDO, ref List<V_HIS_TRANSACTION> transactions, ref List<HIS_SERE_SERV_BILL> sereServBills)
        {
            bool result = false;
            try
            {
                if (data.IsAutoCreateBillForNonBhyt)
                {
                    List<HisTransactionBillSDO> sdos = this.ConvertToBillSdo(cashierRoom, sereServs, serviceReqs, data);
                    if (sdos != null)
                    {
                        V_HIS_ACCOUNT_BOOK accountBook = data.AccountBookId.HasValue ? new HisAccountBookGet().GetViewById(data.AccountBookId.Value) : null;

                        this.transactionCreates = new List<TransactionBillCreate>();

                        foreach (HisTransactionBillSDO sdo in sdos)
                        {
                            TransactionBillCreate creator = new TransactionBillCreate(param);
                            HisTransactionBillResultSDO resultSDO = null;

                            if (!creator.CreateBill(sdo, sereServs, treatment, accountBook, ref resultSDO))
                            {
                                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                            }
                            this.transactionCreates.Add(creator);

                            transactions.Add(resultSDO.TransactionBill);
                            sereServBills.AddRange(resultSDO.SereServBills);
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        public void Rollback()
        {
            if (IsNotNullOrEmpty(this.transactionCreates))
            {
                foreach (var creator in this.transactionCreates)
                {
                    creator.RollbackData();
                }
            }
        }

        private List<HisTransactionBillSDO> ConvertToBillSdo(V_HIS_CASHIER_ROOM cashierRoom, List<V_HIS_SERE_SERV> sereServs, List<V_HIS_SERVICE_REQ> serviceReqs, HisServiceReqExamRegisterSDO data)
        {
            if (IsNotNullOrEmpty(sereServs) && IsNotNullOrEmpty(serviceReqs)
                && data.AccountBookId.HasValue && data.CashierWorkingRoomId.HasValue)
            {
                //Lay ra cac chi dinh y/c dong tien
                List<long> requireFeeServiceReqIds = serviceReqs.Where(o => o.IS_NOT_REQUIRE_FEE != Constant.IS_TRUE).Select(o => o.ID).ToList();
                List<V_HIS_SERE_SERV> requireFeeSereServs = sereServs
                    .Where(o => requireFeeServiceReqIds != null
                        && requireFeeServiceReqIds.Contains(o.SERVICE_REQ_ID.Value)
                        && o.VIR_TOTAL_PATIENT_PRICE > 0
                        && o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList();

                if (IsNotNullOrEmpty(requireFeeSereServs))
                {
                    List<long> serviceReqIds = requireFeeSereServs.Select(o => o.SERVICE_REQ_ID.Value).Distinct().ToList();

                    List<HisTransactionBillSDO> sdos = new List<HisTransactionBillSDO>();

                    //Tuong ung voi moi y lenh tach ra 1 giao dich
                    foreach (long serviceReqId in serviceReqIds)
                    {
                        List<HIS_SERE_SERV_BILL> sereServBills = requireFeeSereServs
                            .Where(o => o.SERVICE_REQ_ID == serviceReqId).Select(o => new HIS_SERE_SERV_BILL
                            {
                                SERE_SERV_ID = o.ID,
                                PRICE = o.VIR_TOTAL_PATIENT_PRICE.Value
                            }).ToList();

                        HisTransactionBillSDO sdo = new HisTransactionBillSDO();
                        HIS_TRANSACTION transaction = new HIS_TRANSACTION();
                        transaction.CASHIER_LOGINNAME = data.CashierLoginName;
                        transaction.CASHIER_USERNAME = data.CashierUserName;
                        transaction.CASHIER_ROOM_ID = cashierRoom.ID;
                        transaction.TRANSACTION_TIME = Inventec.Common.DateTime.Get.Now().Value;
                        transaction.TREATMENT_ID = data.TreatmentId;
                        transaction.ACCOUNT_BOOK_ID = data.AccountBookId.Value;
                        transaction.PAY_FORM_ID = data.PayFormId.HasValue ? data.PayFormId.Value : IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__TM;
                        transaction.AMOUNT = sereServBills.Sum(o => o.PRICE);
                        transaction.IS_DIRECTLY_BILLING = Constant.IS_TRUE;//Giao dich duoc uy quyen tai chuc nang tiep don luon la "thu truc tiep"

                        sdo.SereServBills = sereServBills;
                        sdo.Transaction = transaction;
                        sdo.RequestRoomId = data.RequestRoomId;
                        sdo.PayAmount = transaction.AMOUNT;

                        sdos.Add(sdo);
                    }

                    return sdos;
                }
            }
            return null;
        }
    }
}
