using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisFinancePeriod;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Billing
{
    partial class TransactionBillCreate : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisSereServBillCreate hisSereServBillCreate;

        private HIS_TRANSACTION recentHisTransaction;

        internal TransactionBillCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisSereServBillCreate = new HisSereServBillCreate(param);
        }

        public bool CreateBill(HisTransactionBillSDO data, List<V_HIS_SERE_SERV> sereServs, HIS_TREATMENT treatment, V_HIS_ACCOUNT_BOOK accountBook, ref HisTransactionBillResultSDO resultData)
        {
            bool result = false;
            try
            {
                this.SetServerTime(data);
                data.Transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                TransactionBillCheck checker = new TransactionBillCheck(param);

                if (checker.Run(data, accountBook))
                {
                    List<HIS_SERE_SERV_BILL> sereServBills = null;
                    this.ProcessTransactionBill(data, accountBook, treatment);
                    this.ProcessSereServBill(data, sereServs, ref sereServBills);
                    this.PassResult(sereServBills, ref resultData);
                    result = true;
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_TaoGiaoDichThanhToan, this.recentHisTransaction.AMOUNT).TreatmentCode(treatment.TREATMENT_CODE).TransactionCode(this.recentHisTransaction.TRANSACTION_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessTransactionBill(HisTransactionBillSDO data, V_HIS_ACCOUNT_BOOK hisAccountBook, HIS_TREATMENT treatment)
        {
            data.Transaction.BILL_TYPE_ID = hisAccountBook.BILL_TYPE_ID;
            data.Transaction.SALE_TYPE_ID = null;
            data.Transaction.SERE_SERV_AMOUNT = data.Transaction.AMOUNT;

            if (!this.hisTransactionCreate.Create(data.Transaction, treatment, true))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisTransaction = data.Transaction;
        }

        private void ProcessSereServBill(HisTransactionBillSDO data, List<V_HIS_SERE_SERV> sereServs, ref List<HIS_SERE_SERV_BILL> sereServBills)
        {
            if (IsNotNullOrEmpty(data.SereServBills) && this.recentHisTransaction.TREATMENT_ID.HasValue)
            {
                data.SereServBills.ForEach(o =>
                {
                    o.BILL_ID = this.recentHisTransaction.ID;
                    o.TDL_BILL_TYPE_ID = this.recentHisTransaction.BILL_TYPE_ID;
                    o.TDL_TREATMENT_ID = this.recentHisTransaction.TREATMENT_ID.Value;
                    HisSereServBillUtil.SetTdl(o, sereServs.FirstOrDefault(f => f.ID == o.SERE_SERV_ID));
                });
                if (!this.hisSereServBillCreate.CreateList(data.SereServBills))
                {
                    throw new Exception("Cap nhat thong tin BILL_ID cho yeu cau dich vu (His_Sere_Serv) that bai. Du lieu se bi rollback");
                }
                sereServBills = data.SereServBills;
            }
        }

        private void PassResult(List<HIS_SERE_SERV_BILL> sereServBills, ref HisTransactionBillResultSDO resultData)
        {
            resultData = new HisTransactionBillResultSDO();
            resultData.SereServBills = sereServBills;
            resultData.TransactionBill = new HisTransactionGet(param).GetViewById(this.recentHisTransaction.ID);
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(HisTransactionBillSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                if (data.Transaction != null)
                {
                    data.Transaction.TRANSACTION_TIME = now;
                }
            }
        }

        internal void RollbackData()
        {
            if (this.hisSereServBillCreate != null) this.hisSereServBillCreate.RollbackData();
            if (this.hisTransactionCreate != null) this.hisTransactionCreate.RollbackData();
        }
    }
}
