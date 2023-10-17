using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill.BillByDeposit
{
    class HisTransactionBillByDeposit : BusinessBase
    {
        HisTransactionBillCreate TransactionBillCreate;

        internal HisTransactionBillByDeposit(CommonParam param)
            : base(param)
        {
            TransactionBillCreate = new HisTransactionBillCreate(param);
        }

        internal bool Run(HisTransactionBillByDepositSDO data, ref HisTransactionBillResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workplace = null;
                V_HIS_TREATMENT_FEE treatmentFee = null;
                List<HIS_TRANSACTION> deposits = null;
                List<HIS_TRANSACTION> repays = null;
                List<HIS_TRANSACTION> bills = null;

                List<HIS_SERE_SERV> sereServs = null;
                List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = null;
                List<HIS_SESE_DEPO_REPAY> seseDepoRepays = null;
                List<HIS_SERE_SERV_BILL> sereServBills = null;

                List<HIS_SERE_SERV_DEPOSIT> needBillds = null;

                HisTransactionBillByDepositCheck checker = new HisTransactionBillByDepositCheck(param);
                valid = valid && checker.ValidData(data);
                valid = valid && checker.VerifyTreatmentFee(data.TreatmentId, ref treatmentFee);
                valid = valid && checker.HasWorkPlaceInfo(data.WorkingRoomId, ref workplace);
                valid = valid && checker.IsCashierRoom(workplace);
                valid = valid && checker.HasTransaction(treatmentFee.ID, ref deposits, ref repays, ref bills);
                valid = valid && checker.HasSereServ(treatmentFee.ID, deposits, repays, bills, ref sereServDeposits, ref seseDepoRepays, ref sereServBills, ref sereServs);
                valid = valid && checker.NotHasDepositNormal(deposits);
                valid = valid && checker.NotHasRapayNormal(repays);
                valid = valid && checker.HasDepositServiceNotBill(sereServDeposits, seseDepoRepays, sereServBills, sereServs, ref needBillds);

                if (valid)
                {
                    List<HIS_SERE_SERV> billSereServs = sereServs.Where(o => needBillds.Any(a => a.SERE_SERV_ID == o.ID)).ToList();
                    HisTransactionBillSDO transBillSDO = this.MakeTransBillSDO(data, workplace, billSereServs, treatmentFee, null, null);

                    if (!new HisTransactionBillCreate(param).CreateBill(transBillSDO, ref resultData))
                    {
                        throw new Exception("HisTransactionBillCreate. Ket thuc nghiep vu");
                    }
                    result = true;
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

        internal bool Run(HisTransactionBillByDepositSDO data, ref List<HisTransactionBillResultSDO> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workplace = null;
                V_HIS_TREATMENT_FEE treatmentFee = null;
                List<HIS_TRANSACTION> deposits = null;
                List<HIS_TRANSACTION> repays = null;
                List<HIS_TRANSACTION> bills = null;

                List<HIS_SERE_SERV> sereServs = null;
                List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = null;
                List<HIS_SESE_DEPO_REPAY> seseDepoRepays = null;
                List<HIS_SERE_SERV_BILL> sereServBills = null;

                List<HIS_SERE_SERV_DEPOSIT> needBillds = null;

                HisTransactionBillByDepositCheck checker = new HisTransactionBillByDepositCheck(param);
                valid = valid && checker.ValidData(data);
                valid = valid && checker.VerifyTreatmentFee(data.TreatmentId, ref treatmentFee);
                valid = valid && checker.HasWorkPlaceInfo(data.WorkingRoomId, ref workplace);
                valid = valid && checker.IsCashierRoom(workplace);
                valid = valid && checker.IsAllowAccountBook(data.AccountBookId, data.IsSplitByCashierDeposit, workplace);
                valid = valid && checker.HasTransaction(treatmentFee.ID, ref deposits, ref repays, ref bills);
                valid = valid && checker.HasSereServ(treatmentFee.ID, deposits, repays, bills, ref sereServDeposits, ref seseDepoRepays, ref sereServBills, ref sereServs);
                valid = valid && checker.NotHasDepositNormal(deposits);
                valid = valid && checker.NotHasRapayNormal(repays);
                valid = valid && checker.HasDepositServiceNotBill(sereServDeposits, seseDepoRepays, sereServBills, sereServs, ref needBillds);

                if (valid)
                {
                    List<HisTransactionBillSDO> createData = new List<HisTransactionBillSDO>();
                    if (data.IsSplitByCashierDeposit)
                    {
                        List<HIS_TRANSACTION> checkDeposit = deposits.Where(o => needBillds.Any(a => a.DEPOSIT_ID == o.ID)).ToList();
                        var groupDepositByCashier = checkDeposit.GroupBy(o => o.CASHIER_LOGINNAME).ToList();
                        foreach (var grDeposit in groupDepositByCashier)
                        {
                            List<HIS_SERE_SERV_DEPOSIT> ssNeedBillds = needBillds.Where(o => grDeposit.Any(a => a.ID == o.DEPOSIT_ID)).ToList();
                            List<HIS_SERE_SERV> billSereServs = sereServs.Where(o => ssNeedBillds.Any(a => a.SERE_SERV_ID == o.ID)).ToList();
                            HisTransactionBillSDO transBillSDO = this.MakeTransBillSDO(data, workplace, billSereServs, treatmentFee, grDeposit.First().CASHIER_LOGINNAME, grDeposit.First().CASHIER_USERNAME);
                            if (IsNotNull(transBillSDO))
                            {
                                createData.Add(transBillSDO);
                            }
                        }
                    }
                    else
                    {
                        List<HIS_SERE_SERV> billSereServs = sereServs.Where(o => needBillds.Any(a => a.SERE_SERV_ID == o.ID)).ToList();
                        HisTransactionBillSDO transBillSDO = this.MakeTransBillSDO(data, workplace, billSereServs, treatmentFee, null, null);
                        if (IsNotNull(transBillSDO))
                        {
                            createData.Add(transBillSDO);
                        }
                    }

                    resultData = new List<HisTransactionBillResultSDO>();

                    foreach (var item in createData)
                    {
                        HisTransactionBillResultSDO rsData = null;
                        if (!TransactionBillCreate.CreateBill(item, ref rsData, data.IsSplitByCashierDeposit))
                        {
                            throw new Exception("HisTransactionBillCreate. Ket thuc nghiep vu");
                        }

                        if (IsNotNull(rsData))
                            resultData.Add(rsData);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
                TransactionBillCreate.RollbackData();
            }
            return result;
        }

        private HisTransactionBillSDO MakeTransBillSDO(HisTransactionBillByDepositSDO data, WorkPlaceSDO workplace,
            List<HIS_SERE_SERV> billSereServs, V_HIS_TREATMENT_FEE treatmentFee, string cashierLoginname, string cashierUsername)
        {
            HisTransactionBillSDO sdo = new HisTransactionBillSDO();
            sdo.IsAutoRepay = false;
            sdo.PayAmount = 0;
            sdo.RequestRoomId = data.WorkingRoomId;
            sdo.Transaction = new HIS_TRANSACTION();
            sdo.Transaction.ACCOUNT_BOOK_ID = data.AccountBookId;
            sdo.Transaction.BUYER_ACCOUNT_NUMBER = treatmentFee.TDL_PATIENT_ACCOUNT_NUMBER;
            sdo.Transaction.BUYER_ADDRESS = treatmentFee.TDL_PATIENT_ADDRESS;
            sdo.Transaction.BUYER_NAME = treatmentFee.TDL_PATIENT_NAME;
            sdo.Transaction.BUYER_ORGANIZATION = treatmentFee.TDL_PATIENT_WORK_PLACE_NAME ?? treatmentFee.TDL_PATIENT_WORK_PLACE;
            sdo.Transaction.BUYER_PHONE = treatmentFee.TDL_PATIENT_PHONE;
            sdo.Transaction.BUYER_TAX_CODE = treatmentFee.TDL_PATIENT_TAX_CODE;
            sdo.Transaction.CASHIER_ROOM_ID = workplace.CashierRoomId.Value;
            sdo.Transaction.PAY_FORM_ID = data.PayformId;
            sdo.Transaction.TREATMENT_ID = treatmentFee.ID;
            sdo.Transaction.TREATMENT_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
            sdo.Transaction.AMOUNT = billSereServs.Sum(s => (s.VIR_TOTAL_PATIENT_PRICE ?? 0));
            sdo.Transaction.KC_AMOUNT = sdo.Transaction.AMOUNT;
            sdo.Transaction.TRANSACTION_TIME = data.TransactionTime;
            sdo.Transaction.NUM_ORDER = data.NumOrder ?? 0;
            sdo.Transaction.CASHIER_LOGINNAME = cashierLoginname;
            sdo.Transaction.CASHIER_USERNAME = cashierUsername;

            List<HIS_SERE_SERV_BILL> bills = billSereServs.Select(o => new HIS_SERE_SERV_BILL
            {
                SERE_SERV_ID = o.ID,
                PRICE = (o.VIR_TOTAL_PATIENT_PRICE ?? 0)
            }).ToList();

            sdo.SereServBills = bills;

            return sdo;
        }
    }
}
