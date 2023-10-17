using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisBillGoods;
using MOS.MANAGER.HisFinancePeriod;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatient.UpdateInfo;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Lock;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransaction.Bill
{
    partial class HisTransactionBillCreate : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisSereServBillCreate hisSereServBillCreate;
        private HisTreatmentLock hisTreatmentLock;
        private HisPatientUpdateInfo hisPatientUpdate;
        private HisTransactionCreate hisTransactionAutoRepayCreate;

        private HIS_TRANSACTION recentHisTransaction;
        private HIS_TRANSACTION recentRepayTransaction;

        private bool updateIsCreating = false;
        private HIS_TREATMENT recentTreatment = null;

        internal HisTransactionBillCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisSereServBillCreate = new HisSereServBillCreate(param);
            this.hisTreatmentLock = new HisTreatmentLock(param);
            this.hisPatientUpdate = new HisPatientUpdateInfo(param);
            this.hisTransactionAutoRepayCreate = new HisTransactionCreate(param);
        }

        public bool CreateBill(HisTransactionBillSDO data, ref HisTransactionBillResultSDO resultData, bool isAuthorized = false)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                HIS_TREATMENT treatment = null;
                HIS_TRANSACTION originalTransaction = null;
                List<HIS_SERE_SERV> sereServs = null;
                HIS_CARD card = null;

                decimal exemption = 0;
                decimal fundPaidTotal = 0;
                decimal? transferAmount = null;
                this.SetServerTime(data);
                HisTransactionBillCheck checker = new HisTransactionBillCheck(param);
                bool valid = true;
                valid = valid && IsNotNull(data);

                valid = valid && checker.Run(data, true, ref workPlace, ref hisAccountBook, ref treatment, ref sereServs, ref originalTransaction, ref exemption, ref fundPaidTotal, ref transferAmount);
                valid = valid && checker.IsValidCardTransaction(data, treatment, ref card);
                if (valid)
                {
                    List<HIS_SERE_SERV_BILL> sereServBills = null;

                    //Co giao dich (thanh toan hoac hoan ung) hay khong
                    bool hasTransaction = data.Transaction != null || data.RepayAmount.HasValue;

                    if (hasTransaction)
                    {
                        //Neu co thong tin giao dich thanh toan
                        if (data.Transaction != null)
                        {
                            data.Transaction.CASHIER_ROOM_ID = workPlace.CashierRoomId.Value;
                            data.Transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                        }
                        //Cap nhat ho so ve trang thai "dang giao dich" de khoa khong cho phep nguoi khac thuc hien giao dich vao ho so nay
                        this.UpdateIsCreatingTransaction(treatment, true);
                        this.ProcessTransactionBill(data, hisAccountBook, treatment, sereServs, card, exemption, fundPaidTotal, transferAmount, isAuthorized, originalTransaction);
                        this.ProcessSereServBill(data, sereServs, ref sereServBills);
                        this.ProcessPatient(data, treatment);
                        this.ProcessAutoRepay(data, workPlace, treatment, card);

                        //Cap nhat lai ho so ve trang thai ko giao dich
                        this.UpdateIsCreatingTransaction(treatment, false);
                    }

                    //Xu ly tu dong khoa ho so
                    this.ProcessAutoLock(data, treatment);

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


        private void UpdateIsCreatingTransaction(HIS_TREATMENT treatment, bool isCreate)
        {
            if (treatment != null)
            {
                string sql = "";
                if (isCreate)
                {
                    sql = "UPDATE HIS_TREATMENT SET IS_CREATING_TRANSACTION = 1, PERMISION_UPDATE = 'IS_CREATING_TRANSACTION' WHERE ID = :param1";
                }
                else
                {
                    sql = "UPDATE HIS_TREATMENT SET IS_CREATING_TRANSACTION = NULL, PERMISION_UPDATE = 'IS_CREATING_TRANSACTION' WHERE ID = :param1";
                }
                if (!DAOWorker.SqlDAO.Execute(sql, treatment.ID))
                {
                    throw new Exception("Update IsCreating cho HisTreatment that bai. SQL: " + sql);
                }
                this.updateIsCreating = isCreate;
                this.recentTreatment = treatment;
            }
        }

        private void ProcessPatient(HisTransactionBillSDO data, HIS_TREATMENT treatment)
        {
            if (data.Transaction != null && treatment != null &&
                (!string.IsNullOrWhiteSpace(data.Transaction.BUYER_TAX_CODE)
                || !string.IsNullOrWhiteSpace(data.Transaction.BUYER_ACCOUNT_NUMBER)
                || !string.IsNullOrWhiteSpace(data.Transaction.BUYER_ORGANIZATION)))
            {
                HIS_PATIENT patient = new HisPatientGet().GetById(treatment.PATIENT_ID);
                Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();

                HIS_PATIENT old = Mapper.Map<HIS_PATIENT>(patient);

                if (!string.IsNullOrWhiteSpace(data.Transaction.BUYER_TAX_CODE))
                {
                    patient.TAX_CODE = data.Transaction.BUYER_TAX_CODE;
                }
                if (!string.IsNullOrWhiteSpace(data.Transaction.BUYER_ACCOUNT_NUMBER))
                {
                    patient.ACCOUNT_NUMBER = data.Transaction.BUYER_ACCOUNT_NUMBER;
                }
                if (!string.IsNullOrWhiteSpace(data.Transaction.BUYER_ORGANIZATION))
                {
                    patient.WORK_PLACE = data.Transaction.BUYER_ORGANIZATION;
                }

                if (Inventec.Common.ObjectChecker.ValueChecker.IsPrimitiveDiff<HIS_PATIENT>(old, patient))
                {
                    HisPatientUpdateSDO sdo = new HisPatientUpdateSDO();
                    sdo.HisPatient = patient;
                    sdo.IsNotUpdateImage = true;
                    sdo.TreatmentId = treatment.ID;//update lai ca treatment co chua TDL
                    HIS_PATIENT resultData = null;
                    if (!this.hisPatientUpdate.Run(sdo, ref resultData))
                    {
                        LogSystem.Warn("Cap nhat thong tin patient (thong tin ma so thue, tai khoan, to chuc) that bai");
                    }
                }
            }
        }

        private void ProcessTransactionBill(HisTransactionBillSDO data, V_HIS_ACCOUNT_BOOK hisAccountBook, HIS_TREATMENT treatment, List<HIS_SERE_SERV> sereServs, HIS_CARD card, decimal exemption, decimal fundPaidTotal, decimal? transferAmount, bool isAuthorized, HIS_TRANSACTION originalTransaction)
        {
            data.Transaction.BILL_TYPE_ID = hisAccountBook.BILL_TYPE_ID;
            if (fundPaidTotal > 0)
            {
                data.Transaction.TDL_BILL_FUND_AMOUNT = fundPaidTotal;
            }

            data.Transaction.KC_AMOUNT = transferAmount;
            data.Transaction.SALE_TYPE_ID = null;
            data.Transaction.SERE_SERV_AMOUNT = data.Transaction.AMOUNT;
            data.Transaction.PAY_FORM_ID = data.PayFormId;
            data.Transaction.TRANSACTION_TIME = data.TransactionTime;


            //Chi trong truong hop thanh toan the moi gan thong tin giao dich de tranh gan nham ma giao dich cua giao dich 
            //hoan ung (trong truong hop thanh toan tu dong hoan ung)
            if (data.Transaction.PAY_FORM_ID == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE)
            {
                string tigTransactionCode = null;
                if ((data.Transaction.AMOUNT - (data.Transaction.KC_AMOUNT ?? 0) - (data.Transaction.EXEMPTION ?? 0) - (data.Transaction.TDL_BILL_FUND_AMOUNT ?? 0)) != 0)
                {
                    tigTransactionCode = data.TigTransactionCode;
                }
                HisTransactionUtil.SetEpaymentInfo(data.Transaction, card, tigTransactionCode, data.TigTransactionTime);
            }

            List<HIS_TRANSACTION> previous = null;
            HisTransactionUtil.SetTreatmentFeeInfo(data.Transaction, ref previous);
            HisTransactionUtil.SetTransactionInfo(data.Transaction, treatment, previous, sereServs);

            // Cap nhat thong tin giao dich goc
            if (originalTransaction != null)
            {
                data.Transaction.REPLACE_REASON = data.ReplaceReason;
                data.Transaction.REPLACE_TIME = Inventec.Common.DateTime.Get.Now();
                data.Transaction.ORIGINAL_TRANSACTION_ID = originalTransaction.ID;
                data.Transaction.TDL_ORIGINAL_EI_NUM_ORDER = originalTransaction.EINVOICE_NUM_ORDER;
                data.Transaction.TDL_ORIGINAL_EI_TIME = originalTransaction.EINVOICE_TIME;
                data.Transaction.TDL_ORIGINAL_EI_CODE = originalTransaction.INVOICE_CODE;
            }

            if (!this.hisTransactionCreate.Create(data.Transaction, treatment, isAuthorized))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisTransaction = data.Transaction;
        }

        private void ProcessSereServBill(HisTransactionBillSDO data, List<HIS_SERE_SERV> sereServs, ref List<HIS_SERE_SERV_BILL> sereServBills)
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

        private void ProcessAutoRepay(HisTransactionBillSDO data, WorkPlaceSDO workPlace, HIS_TREATMENT treatment, HIS_CARD card)
        {
            if (treatment == null)
            {
                return;
            }

            if (data.IsAutoRepay && data.RepayAmount.HasValue)
            {
                HIS_TRANSACTION repayTransaction = new HIS_TRANSACTION();
                repayTransaction.ACCOUNT_BOOK_ID = data.RepayAccountBookId.Value;
                repayTransaction.NUM_ORDER = (data.RepayNumOrder ?? 0);
                repayTransaction.AMOUNT = data.RepayAmount.Value;
                repayTransaction.CASHIER_ROOM_ID = workPlace.CashierRoomId.Value;
                repayTransaction.PAY_FORM_ID = data.PayFormId;

                if (repayTransaction.PAY_FORM_ID == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE)
                {
                    HisTransactionUtil.SetEpaymentInfo(repayTransaction, card, data.TigTransactionCode, data.TigTransactionTime);
                }
                repayTransaction.TRANSACTION_TIME = data.TransactionTime;
                repayTransaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU;
                repayTransaction.TREATMENT_ID = treatment.ID;

                if (recentHisTransaction != null)
                {
                    repayTransaction.CASHIER_LOGINNAME = recentHisTransaction.CASHIER_LOGINNAME;
                    repayTransaction.CASHIER_USERNAME = recentHisTransaction.CASHIER_USERNAME;
                }
                else
                {
                    repayTransaction.CASHIER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    repayTransaction.CASHIER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                }

                HisTransactionUtil.SetTreatmentFeeInfo(repayTransaction);

                if (!this.hisTransactionAutoRepayCreate.Create(repayTransaction, treatment))
                {
                    throw new Exception("hisTransactionAutoRepayCreate. Ket thuc nghiep vu");
                }
                this.recentRepayTransaction = repayTransaction;
            }
        }

        private void ProcessAutoLock(HisTransactionBillSDO data, HIS_TREATMENT treatment)
        {
            //Neu co cau hinh tu dong khoa sau khi thanh toan
            //Hoac co nghiep vu xu ly khi tao giao dich nhung khong co thong tin thanh toan thi deu xu ly de khoa ho so
            if ((HisTreatmentCFG.AUTO_LOCK_AFTER_BILL && data.Transaction != null && data.Transaction.TREATMENT_ID.HasValue)
                || (HisTransactionCFG.IS_ALLOW_PROCESS_WITH_NO_TRANSACTION && data.Transaction == null && !IsNotNullOrEmpty(data.SereServBills) && data.TreatmentId.HasValue))
            {
                //Neu benh nhan da ket thuc dieu tri va chua duyet khoa tai chinh
                if (treatment != null && treatment.IS_PAUSE == MOS.UTILITY.Constant.IS_TRUE
                    && treatment.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    bool isPaid = false;

                    //Chi kiem tra so tien can thanh toan trong truong hop ko co nghiep vu tu dong hoan ung nham toi uu hieu nang
                    //vi neu xay ra nghiep vu hoan ung thi dong nghia voi viec tat ca da duoc thanh toan het
                    if (data.IsAutoRepay && data.RepayAmount.HasValue)
                    {
                        isPaid = true;
                    }
                    else
                    {
                        V_HIS_TREATMENT_FEE_1 treatmentFee = new HisTreatmentGet(param).GetFeeView1ById(treatment.ID);
                        //So tien can thu them
                        decimal? unpaid = new HisTreatmentGet().GetUnpaid(treatmentFee);
                        isPaid = unpaid <= Constant.PRICE_DIFFERENCE;
                    }

                    //Neu so tien can thu them = 0 thi tu dong duyet khoa
                    if (isPaid)
                    {
                        HIS_TREATMENT outTreat = new HIS_TREATMENT();

                        HisTreatmentLockSDO sdo = new HisTreatmentLockSDO();
                        sdo.RequestRoomId = data.RequestRoomId;

                        //Trong truong hop co cau hinh lay gio duyet khoa vien phi theo gio ra vien trong truong hop tu dong
                        if (HisTreatmentCFG.IS_USING_OUT_TIME_FOR_FEE_LOCK_TIME_IN_CASE_OF_AUTO
                            && treatment.OUT_TIME.HasValue)
                        {
                            sdo.FeeLockTime = treatment.OUT_TIME.Value;
                        }
                        else
                        {
                            sdo.FeeLockTime = Inventec.Common.DateTime.Get.Now().Value;
                        }

                        sdo.TreatmentId = treatment.ID;
                        if (!this.hisTreatmentLock.Run(sdo, ref outTreat))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBill_KhongTuDongDuyetHoSoVienPhi);
                            LogSystem.Warn("Tu dong duyet khoa ho so dieu tri that bai. Treatment_id: " + data.TreatmentId);
                        }
                    }
                }
            }
        }

        private void PassResult(List<HIS_SERE_SERV_BILL> sereServBills, ref HisTransactionBillResultSDO resultData)
        {
            resultData = new HisTransactionBillResultSDO();
            resultData.SereServBills = sereServBills;

            if (this.recentHisTransaction != null)
            {
                resultData.TransactionBill = new HisTransactionGet(param).GetViewById(this.recentHisTransaction.ID);
            }
            if (this.recentRepayTransaction != null)
            {
                resultData.TransactionRepay = new HisTransactionGet(param).GetViewById(this.recentRepayTransaction.ID);
            }
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
            if (this.hisTransactionAutoRepayCreate != null) this.hisTransactionAutoRepayCreate.RollbackData();
            if (this.hisSereServBillCreate != null) this.hisSereServBillCreate.RollbackData();
            if (this.hisTransactionCreate != null) this.hisTransactionCreate.RollbackData();
            this.RollbackTreatment();
        }

        private void RollbackTreatment()
        {
            if (this.recentTreatment != null && this.updateIsCreating)
            {
                if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_TREATMENT SET IS_CREATING_TRANSACTION = NULL, PERMISION_UPDATE = 'IS_CREATING_TRANSACTION' WHERE ID = :param1", this.recentTreatment.ID))
                {
                    LogSystem.Error("Update IsCreating cho HisTreatment that bai");
                }
            }
        }
    }
}
