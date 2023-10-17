using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTransaction.Bill;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Debt.Create
{
    class HisTransactionDebtCreate : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisSereServDebtCreate hisSereServDebtCreate;

        private HIS_TRANSACTION recentHisTransaction = null;

        internal HisTransactionDebtCreate()
            : base()
        {
            this.Init();
        }

        internal HisTransactionDebtCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisSereServDebtCreate = new HisSereServDebtCreate(param);
        }

        internal bool Run(HisTransactionDebtSDO data, ref V_HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                HIS_TREATMENT treatment = null;
                List<HIS_SERE_SERV> sereServs = null;
                List<HIS_SERE_SERV_DEBT> oldSereServDebts = null;
                this.SetServerTime(data);
                data.Transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO;
                HisTransactionDebtCheck checker = new HisTransactionDebtCheck(param);
                HisTransactionCheck commonChecker = new HisTransactionCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisSereServCheck sereServChecker = new HisSereServCheck(param);
                HisTransactionBillCheck billChecker = new HisTransactionBillCheck(param);

                bool isPause = false;

                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && commonChecker.IsCashierRoom(workPlace);
                valid = valid && commonChecker.IsUnlockAccountBook(data.Transaction.ACCOUNT_BOOK_ID, ref hisAccountBook);
                valid = valid && commonChecker.IsValidNumOrder(data.Transaction, hisAccountBook);
                valid = valid && treatmentChecker.VerifyId(data.Transaction.TREATMENT_ID.Value, ref treatment);
                valid = valid && commonChecker.HasNotFinancePeriod(data.Transaction);
                valid = valid && checker.VerifyDeptType(data, treatment, ref isPause);
                valid = valid && checker.IsValidAmount(data, !isPause);
                valid = valid && checker.IsValidSereServ(data, ref sereServs);
                valid = valid && sereServChecker.HasNoBill(sereServs);
                valid = valid && sereServChecker.HasNoDeposit(sereServs.Select(s => s.ID).ToList(), false);
                valid = valid && sereServChecker.HasNoInvoice(sereServs);
                valid = valid && checker.IsValidSereServDebt(sereServs, data.SereServDebts, ref oldSereServDebts);
                valid = valid && billChecker.IsValidCarerCardBorrow(sereServs);
                if (valid)
                {
                    data.Transaction.CASHIER_ROOM_ID = workPlace.CashierRoomId.Value;
                    data.Transaction.PAY_FORM_ID = IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__TM;
                    if (isPause)
                    {
                        data.Transaction.DEBT_TYPE = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__TREAT;
                    }
                    else
                    {
                        data.Transaction.DEBT_TYPE = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__SERVICE;
                    }

                    this.ProcessTransactionDebt(data, hisAccountBook, treatment);

                    this.ProcessSereServDebt(data, sereServs, oldSereServDebts);

                    this.PassResult(ref resultData);

                    result = true;

                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_TaoGiaoDichChotNo, this.recentHisTransaction.AMOUNT).TreatmentCode(treatment.TREATMENT_CODE).TransactionCode(this.recentHisTransaction.TRANSACTION_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void ProcessTransactionDebt(HisTransactionDebtSDO data, V_HIS_ACCOUNT_BOOK hisAccountBook, HIS_TREATMENT treatment)
        {
            HisTransactionUtil.SetTreatmentFeeInfo(data.Transaction, true);

            if (!this.hisTransactionCreate.Create(data.Transaction, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisTransaction = data.Transaction;
        }

        private void ProcessSereServDebt(HisTransactionDebtSDO data, List<HIS_SERE_SERV> sereServs, List<HIS_SERE_SERV_DEBT> oldSereServDebts)
        {
            if (IsNotNullOrEmpty(data.SereServDebts) && this.recentHisTransaction.TREATMENT_ID.HasValue)
            {
                data.SereServDebts.ForEach(o =>
                {
                    o.DEBT_ID = this.recentHisTransaction.ID;
                    o.TDL_TREATMENT_ID = this.recentHisTransaction.TREATMENT_ID.Value;
                    HisSereServDebtUtil.SetTdl(o, sereServs.FirstOrDefault(f => f.ID == o.SERE_SERV_ID));
                    HisSereServDebtUtil.SetPreviousDebtAmount(o, (oldSereServDebts != null ? oldSereServDebts.Where(s => s.SERE_SERV_ID == o.SERE_SERV_ID).ToList() : null));
                });
                if (!this.hisSereServDebtCreate.CreateList(data.SereServDebts))
                {
                    throw new Exception("Tao thong tin DEBT_ID cho yeu cau dich vu (His_Sere_Serv) that bai. Du lieu se bi rollback");
                }
            }
        }

        private void PassResult(ref V_HIS_TRANSACTION resultData)
        {
            resultData = new HisTransactionGet(param).GetViewById(this.recentHisTransaction.ID);
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(HisTransactionDebtSDO data)
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

        private void Rollback()
        {
            if (this.hisSereServDebtCreate != null) this.hisSereServDebtCreate.RollbackData();
            if (this.hisTransactionCreate != null) this.hisTransactionCreate.RollbackData();
        }
    }
}
