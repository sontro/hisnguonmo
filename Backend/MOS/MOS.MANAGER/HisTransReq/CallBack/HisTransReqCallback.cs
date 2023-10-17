using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.CallBack
{
    class HisTransReqCallback : BusinessBase
    {

        private HisTransactionCreate hisTransactionCreate;
        private HisTransReqUpdate hisTransReqUpdate;
        private HisSereServBillCreate hisSereServBillCreate;
        private HisSereServDepositCreate hisSereServDepositCreate;

        private List<HIS_TRANSACTION> recentTransactions;
        private List<HIS_TRANS_REQ> recentTransReqs;

        internal HisTransReqCallback()
            : base()
        {
            this.Init();
        }

        internal HisTransReqCallback(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisTransReqUpdate = new HisTransReqUpdate(param);
            this.hisSereServBillCreate = new HisSereServBillCreate(param);
            this.hisSereServDepositCreate = new HisSereServDepositCreate(param);
        }

        internal bool Run(HisTransReqCallbackSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                List<HIS_TRANS_REQ> listRaw = null;
                List<HIS_SESE_TRANS_REQ> seseTransReqs = null;
                List<HIS_SERE_SERV> listSereServ = null;
                HisTransReqCallbackCheck checker = new HisTransReqCallbackCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyTransReqCode(data, ref listRaw, ref seseTransReqs, ref listSereServ);
                valid = valid && checker.IsSttRequest(listRaw);
                valid = valid && treatChecker.VerifyId(listRaw.FirstOrDefault().TREATMENT_ID, ref treatment);

                if (valid)
                {
                    this.ProcessTransReq(data, listRaw);
                    this.ProcessTransaction(data, treatment);
                    this.ProcessSereServBill(seseTransReqs, listSereServ);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                this.Rollback();
                param.HasException = true;
            }
            return result;
        }

        private void ProcessTransReq(HisTransReqCallbackSDO data, List<HIS_TRANS_REQ> lstTransReq)
        {
            if (data.IsSuccessful)
            {
                lstTransReq.ForEach(o => o.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FINISHED);
            }
            else
            {
                lstTransReq.ForEach(o => o.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FAILED);
            }

            if (!this.hisTransReqUpdate.UpdateList(lstTransReq))
            {
                throw new Exception("hisTransReqUpdate. Ket thuc nghiep vu");
            }
            this.recentTransReqs = lstTransReq;
        }

        private void ProcessTransaction(HisTransReqCallbackSDO data, HIS_TREATMENT treatment)
        {
            if (data.IsSuccessful)
            {
                List<HIS_TRANSACTION> creates = new List<HIS_TRANSACTION>();

                foreach (HIS_TRANS_REQ raw in this.recentTransReqs)
                {
                    HIS_TRANSACTION tran = new HIS_TRANSACTION();
                    tran.ACCOUNT_BOOK_ID = raw.ACCOUNT_BOOK_ID;
                    tran.AMOUNT = raw.AMOUNT;
                    tran.BILL_TYPE_ID = raw.BILL_TYPE_ID;
                    tran.BUYER_ACCOUNT_NUMBER = raw.BUYER_ACCOUNT_NUMBER;
                    tran.BUYER_ADDRESS = raw.BUYER_ADDRESS;
                    tran.BUYER_NAME = raw.BUYER_NAME;
                    tran.BUYER_ORGANIZATION = raw.BUYER_ORGANIZATION;
                    tran.BUYER_TAX_CODE = raw.BUYER_TAX_CODE;
                    tran.CANCEL_LOGINNAME = raw.CANCEL_LOGINNAME;
                    tran.CANCEL_REASON = raw.CANCEL_REASON;
                    tran.CANCEL_TIME = raw.CANCEL_TIME;
                    tran.CANCEL_USERNAME = raw.CANCEL_USERNAME;
                    tran.CASHIER_LOGINNAME = raw.CASHIER_LOGINNAME;
                    tran.CASHIER_ROOM_ID = raw.CASHIER_ROOM_ID;
                    tran.CASHIER_USERNAME = raw.CASHIER_USERNAME;
                    tran.DESCRIPTION = raw.DESCRIPTION;
                    tran.EXEMPTION = raw.EXEMPTION;
                    tran.EXEMPTION_REASON = raw.EXEMPTION_REASON;
                    tran.IS_CANCEL = raw.IS_CANCEL;
                    tran.IS_DIRECTLY_BILLING = raw.IS_DIRECTLY_BILLING;
                    tran.PAY_FORM_ID = raw.PAY_FORM_ID;
                    tran.SALE_TYPE_ID = raw.SALE_TYPE_ID;
                    tran.SELLER_ACCOUNT_NUMBER = raw.SELLER_ACCOUNT_NUMBER;
                    tran.SELLER_ADDRESS = raw.SELLER_ADDRESS;
                    tran.SELLER_NAME = raw.SELLER_NAME;
                    tran.SELLER_PHONE = raw.SELLER_PHONE;
                    tran.SELLER_TAX_CODE = raw.SELLER_TAX_CODE;
                    tran.TIG_TRANSACTION_CODE = data.TigTransactionCode;
                    tran.TIG_TRANSACTION_TIME = data.TigTransactionTime;
                    tran.TRANS_REQ_ID = raw.ID;
                    tran.TRANSACTION_TIME = Inventec.Common.DateTime.Get.Now().Value;
                    tran.TRANSACTION_TYPE_ID = raw.TRANSACTION_TYPE_ID;
                    tran.TREATMENT_ID = raw.TREATMENT_ID;
                    tran.TREATMENT_TYPE_ID = raw.TREATMENT_TYPE_ID;
                    tran.WORKING_SHIFT_ID = raw.WORKING_SHIFT_ID;
                    tran.SERE_SERV_AMOUNT = tran.AMOUNT;
                    creates.Add(tran);
                }

                if (!this.hisTransactionCreate.CreateListNotSetCashier(creates, treatment))
                {
                    throw new Exception("hisTransactionCreate. Rollback du lieu. Ket thuc nghiep vu");
                }
                this.recentTransactions = creates;
            }
        }

        private void ProcessSereServBill(List<HIS_SESE_TRANS_REQ> seseTransReqs, List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(this.recentTransactions))
            {
                List<HIS_SERE_SERV_BILL> sereServBills = new List<HIS_SERE_SERV_BILL>();

                foreach (HIS_SESE_TRANS_REQ item in seseTransReqs)
                {
                    HIS_TRANSACTION tran = this.recentTransactions.FirstOrDefault(o => o.TRANS_REQ_ID == item.TRANS_REQ_ID);

                    if (tran.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
                    {
                        HIS_SERE_SERV_BILL ssBill = new HIS_SERE_SERV_BILL();
                        ssBill.BILL_ID = tran.ID;
                        ssBill.PRICE = item.PRICE;
                        ssBill.SERE_SERV_ID = item.SERE_SERV_ID;
                        ssBill.TDL_BILL_TYPE_ID = tran.BILL_TYPE_ID;
                        ssBill.TDL_TREATMENT_ID = tran.TREATMENT_ID.Value;
                        HisSereServBillUtil.SetTdl(ssBill, (sereServs != null ? sereServs.FirstOrDefault(f => f.ID == item.SERE_SERV_ID) : null));
                        sereServBills.Add(ssBill);
                    }
                }

                if (IsNotNullOrEmpty(sereServBills) && !this.hisSereServBillCreate.CreateList(sereServBills))
                {
                    throw new Exception("hisSereServBillCreate. Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessSereServDeposit(List<HIS_SESE_TRANS_REQ> seseTransReqs, List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(this.recentTransactions))
            {
                List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = new List<HIS_SERE_SERV_DEPOSIT>();

                foreach (HIS_SESE_TRANS_REQ item in seseTransReqs)
                {
                    HIS_TRANSACTION tran = this.recentTransactions.FirstOrDefault(o => o.TRANS_REQ_ID == item.TRANS_REQ_ID);
                    if (tran.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU)
                    {
                        HIS_SERE_SERV_DEPOSIT ssDeposit = new HIS_SERE_SERV_DEPOSIT();
                        ssDeposit.DEPOSIT_ID = tran.ID;
                        ssDeposit.AMOUNT = item.PRICE;
                        ssDeposit.SERE_SERV_ID = item.SERE_SERV_ID;
                        ssDeposit.TDL_TREATMENT_ID = tran.TREATMENT_ID.Value;
                        HisSereServDepositUtil.SetTdl(ssDeposit, (sereServs != null ? sereServs.FirstOrDefault(f => f.ID == item.SERE_SERV_ID) : null));
                        sereServDeposits.Add(ssDeposit);
                    }
                }

                if (IsNotNullOrEmpty(sereServDeposits) && !this.hisSereServDepositCreate.CreateList(sereServDeposits))
                {
                    throw new Exception("hisSereServDepositCreate. Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisSereServBillCreate.RollbackData();
                this.hisSereServDepositCreate.RollbackData();
                this.hisTransactionCreate.RollbackData();
                this.hisTransReqUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
