using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryBillTwoBook;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatient.UpdateInfo;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisServiceType;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Lock;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill.BillTwoBook
{
    class HisTransactionBillTwoBookCreate : BusinessBase
    {
        private const string TOTAL_DEPOSIT_AMOUNT = "TOTAL_DEPOSIT_AMOUNT";
        private const string DEPARTMENT_CODE = "DEPARTMENT_CODE";
        private const string DEPARTMENT_NAME = "DEPARTMENT_NAME";
        private const string MUST_PAY_AMOUNT = "MUST_PAY_AMOUNT";
        private const string AMOUNT_ = "AMOUNT_";
        private const string RECIEPT_AMOUNT = "RECIEPT_AMOUNT";
        private const string INVOICE_AMOUNT = "INVOICE_AMOUNT";
        private const string _BH = "_BH";
        private const string _VP = "_VP";
        private const string _DV = "_DV";

        private HIS_TRANSACTION recentRecieptTransaction;
        private HIS_TRANSACTION recentInvoiceTransaction;
        private HIS_TRANSACTION recentRepayTransaction;

        private HisTransactionCreate hisTransactionCreate;
        private HisSereServBillCreate hisSereServBillCreate;
        private HisTreatmentLock hisTreatmentLock;
        private HisPatientUpdateInfo hisPatientUpdate;
        private HisTransactionCreate hisTransactionAutoRepayCreate;

        private bool updateIsCreating = false;
        private HIS_TREATMENT recentTreatment = null;

        internal HisTransactionBillTwoBookCreate(CommonParam param)
            : base(param)
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisSereServBillCreate = new HisSereServBillCreate(param);
            this.hisPatientUpdate = new HisPatientUpdateInfo(param);
            this.hisTreatmentLock = new HisTreatmentLock(param);
            this.hisTransactionAutoRepayCreate = new HisTransactionCreate(param);
        }

        internal bool Run(HisTransactionBillTwoBookSDO data, ref List<V_HIS_TRANSACTION> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                long time = Inventec.Common.DateTime.Get.Now().Value;
                string sessionCode = Guid.NewGuid().ToString();
                this.SetServerTime(data, time);
                this.SetSessionCode(data, sessionCode);
                HIS_TREATMENT treatment = null;
                WorkPlaceSDO workPlace = null;
                V_HIS_ACCOUNT_BOOK recieptAccountBook = null;
                V_HIS_ACCOUNT_BOOK invoiceAccountBook = null;
                List<HIS_SERE_SERV> listSereServs = null;
                List<HIS_SERE_SERV_BILL> existsSereServBill = null;
                HisTransactionBillTwoBookCheck checker = new HisTransactionBillTwoBookCheck(param);
                HisTransactionBillCheck billChecker = new HisTransactionBillCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.Run(data, true, ref workPlace, ref recieptAccountBook, ref invoiceAccountBook, ref treatment, ref listSereServs, ref existsSereServBill);
                valid = valid && checker.IsValidCardTransaction(data);
                valid = valid && billChecker.IsValidCarerCardBorrow(listSereServs);

                if (valid)
                {
                    this.ProcessTreatment(treatment, true);
                    this.ProcessTransaction(data, treatment, workPlace, recieptAccountBook, invoiceAccountBook, listSereServs, existsSereServBill);
                    this.ProcessSereServBill(data, listSereServs);
                    this.ProcessPatient(data, treatment);
                    this.ProcessTreatment(treatment, false);
                    this.ProcessAuto(data, treatment, workPlace);

                    result = true;
                    this.PassResult(ref resultData);

                    HisTransactionLog.Run(this.recentRecieptTransaction, this.recentInvoiceTransaction, treatment, LibraryEventLog.EventLog.Enum.HisTransaction_TaoGiaoDichThanhToanHaiSo);
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                resultData = null;
                result = false;
            }
            return result;
        }

        private void ProcessTreatment(HIS_TREATMENT treatment, bool isCreate)
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

        private void ProcessTransaction(HisTransactionBillTwoBookSDO data, HIS_TREATMENT treatment, WorkPlaceSDO workPlace, V_HIS_ACCOUNT_BOOK recieptAccountBook, V_HIS_ACCOUNT_BOOK invoiceAccountBook, List<HIS_SERE_SERV> listSereServs, List<HIS_SERE_SERV_BILL> existsSereServBill)
        {
            List<HIS_TRANSACTION> creates = new List<HIS_TRANSACTION>();
            if (data.RecieptTransaction != null)
            {
                data.RecieptTransaction.CASHIER_ROOM_ID = workPlace.CashierRoomId.Value;
                data.RecieptTransaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                data.RecieptTransaction.BILL_TYPE_ID = recieptAccountBook.BILL_TYPE_ID;
                decimal fundPaidTotal = data.RecieptTransaction.HIS_BILL_FUND != null ? data.RecieptTransaction.HIS_BILL_FUND.Sum(o => o.AMOUNT) : 0;
                if (fundPaidTotal > 0)
                {
                    data.RecieptTransaction.TDL_BILL_FUND_AMOUNT = fundPaidTotal;
                }
                data.RecieptTransaction.SALE_TYPE_ID = null;
                HisTransactionUtil.SetTreatmentFeeInfo(data.RecieptTransaction);
                data.RecieptTransaction.SERE_SERV_AMOUNT = data.RecieptTransaction.AMOUNT;
                creates.Add(data.RecieptTransaction);
            }
            if (data.InvoiceTransaction != null)
            {
                data.InvoiceTransaction.CASHIER_ROOM_ID = workPlace.CashierRoomId.Value;
                data.InvoiceTransaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                data.InvoiceTransaction.BILL_TYPE_ID = invoiceAccountBook.BILL_TYPE_ID;
                decimal fundPaidTotal = data.InvoiceTransaction.HIS_BILL_FUND != null ? data.InvoiceTransaction.HIS_BILL_FUND.Sum(o => o.AMOUNT) : 0;
                if (fundPaidTotal > 0)
                {
                    data.InvoiceTransaction.TDL_BILL_FUND_AMOUNT = fundPaidTotal;
                }
                data.InvoiceTransaction.SALE_TYPE_ID = null;
                if (data.RecieptTransaction != null)
                {
                    HisTransactionUtil.SetTreatmentFeeInfo(data.InvoiceTransaction, data.RecieptTransaction);
                }
                else
                {
                    HisTransactionUtil.SetTreatmentFeeInfo(data.InvoiceTransaction);
                }
                data.InvoiceTransaction.SERE_SERV_AMOUNT = data.InvoiceTransaction.AMOUNT;
                creates.Add(data.InvoiceTransaction);
            }

            V_HIS_TREATMENT_FEE_1 treatmentFee = null;
            decimal mustPayAmount = 0;
            Dictionary<long, BillAmountSDO> dicPriceSerivceType = new Dictionary<long, BillAmountSDO>();

            this.CalcTransferAmount(data, ref treatmentFee, ref mustPayAmount);

            this.ProcessCalcPriceByAccountBook(data, listSereServs, existsSereServBill, ref dicPriceSerivceType);

            this.ProcessTransactionInfo(data, treatmentFee, dicPriceSerivceType, mustPayAmount);

            if (!this.hisTransactionCreate.CreateList(creates, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentRecieptTransaction = data.RecieptTransaction;
            this.recentInvoiceTransaction = data.InvoiceTransaction;
        }

        public void CalcTransferAmount(HisTransactionBillTwoBookSDO data, ref V_HIS_TREATMENT_FEE_1 treatmentFee, ref decimal mustPayAmount)
        {
            decimal transferAmount = 0;
            decimal recieptMustPaid = 0;
            decimal invoiceMustPaid = 0;
            decimal payAmount = 0;
            if (data.RecieptTransaction != null)
            {
                recieptMustPaid = data.RecieptTransaction.AMOUNT - ((data.RecieptTransaction.TDL_BILL_FUND_AMOUNT ?? 0) + (data.RecieptTransaction.EXEMPTION ?? 0));
                payAmount += data.RecieptPayAmount;
                data.RecieptTransaction.KC_AMOUNT = null;
            }
            if (data.InvoiceTransaction != null)
            {
                invoiceMustPaid = data.InvoiceTransaction.AMOUNT - ((data.InvoiceTransaction.TDL_BILL_FUND_AMOUNT ?? 0) + (data.InvoiceTransaction.EXEMPTION ?? 0));
                payAmount += data.InvoicePayAmount;
                data.InvoiceTransaction.KC_AMOUNT = null;
            }
            //So tien benh nhan phai tra
            decimal mustPaid = (recieptMustPaid + invoiceMustPaid);

            //Neu so tien thanh toan khac so tien can giao dich ==> co ket chuyen
            if (payAmount != mustPaid)
            {
                treatmentFee = new HisTreatmentGet(param).GetFeeView1ById(data.TreatmentId);

                //Tong so tien cho phep dung de ket chuyen (so tien Hiện dư)
                decimal? availableAmount = new HisTreatmentGet().GetAvailableAmount(treatmentFee);
                decimal calcPayAmount = 0;

                //Nếu Hiện dư >= Số tiền (tổng số tiền các dịch vụ cần thanh toán) --> "Cần thu" = 0, KC_AMOUNT = "Số tiền"
                //Nếu Hiện dư < Số tiền (tổng số tiền các dịch vụ cần thanh toán) --> "Cần thu" = Số tiền - Hiện dư, KC_AMOUNT = Hiện dư
                //Nếu Hiện dư=0 --> KC_amount=null
                if (availableAmount <= 0)
                {
                    transferAmount = 0;
                    calcPayAmount = mustPaid;
                }
                else if (availableAmount >= mustPaid)
                {
                    calcPayAmount = 0;
                    transferAmount = mustPaid;
                }
                else
                {
                    calcPayAmount = mustPaid - availableAmount.Value;
                    transferAmount = availableAmount.Value;
                }

                //Neu so tien server tinh lech so voi so tien client tinh qua 0.0001 thi ko cho thanh toan
                //(so sanh voi 0.0001 la vi de tranh truong hop client lam tron den 4 chu so sau phan thap phan)
                if (Math.Abs(calcPayAmount - payAmount) > Constant.PRICE_DIFFERENCE)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("So tien can thu do server tinh la: " + calcPayAmount + " khac voi so tien do client y/c la: " + payAmount);
                }

                if (transferAmount > 0 && data.RecieptTransaction != null)
                {
                    if (transferAmount >= recieptMustPaid)
                    {
                        data.RecieptTransaction.KC_AMOUNT = recieptMustPaid;
                        transferAmount = transferAmount - recieptMustPaid;
                    }
                    else
                    {
                        data.RecieptTransaction.KC_AMOUNT = transferAmount;
                        transferAmount = 0;
                    }
                }

                if (transferAmount > 0 && data.InvoiceTransaction != null)
                {
                    if (transferAmount >= invoiceMustPaid)
                    {
                        data.InvoiceTransaction.KC_AMOUNT = invoiceMustPaid;
                        transferAmount = transferAmount - invoiceMustPaid;
                    }
                    else
                    {
                        data.InvoiceTransaction.KC_AMOUNT = transferAmount;
                        transferAmount = 0;
                    }
                }
            }
            mustPayAmount = payAmount;
        }

        private void ProcessCalcPriceByAccountBook(HisTransactionBillTwoBookSDO data, List<HIS_SERE_SERV> listSereServs, List<HIS_SERE_SERV_BILL> existsSereServBill, ref Dictionary<long, BillAmountSDO> dicPrice)
        {
            if (!(HisTransactionCFG.BILL_TWO_BOOK_OPTION == (int)HisTransactionCFG.ENUM_BILL_OPTION.CTO_TW
                || HisTransactionCFG.BILL_TWO_BOOK_OPTION == (int)HisTransactionCFG.ENUM_BILL_OPTION.HCM_115
                || HisTransactionCFG.BILL_TWO_BOOK_OPTION == (int)HisTransactionCFG.ENUM_BILL_OPTION.QBH_CUBA))
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Khong lay duoc cau hinh thanh toan hai so");
            }
            decimal totalRecieptAmount = 0;
            decimal totalInvoiceAmount = 0;
            Dictionary<long, BillAmountSDO> dicPriceByType = new Dictionary<long, BillAmountSDO>();

            BillTwoBookPriceProcessor priceProcessor = new BillTwoBookPriceProcessor(HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT, HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE, HisPatientTypeCFG.PATIENT_TYPE_ID__SERVICE, HisPatientTypeCFG.DATA);

            if (HisTransactionCFG.BILL_TWO_BOOK_OPTION == (int)HisTransactionCFG.ENUM_BILL_OPTION.HCM_115)
            {
                foreach (var item in listSereServs)
                {
                    bool existReceipt = existsSereServBill != null && existsSereServBill.Any(a => a.SERE_SERV_ID == item.ID && (!a.TDL_BILL_TYPE_ID.HasValue || a.TDL_BILL_TYPE_ID != HisAccountBookCFG.BILL_TYPE_ID__INVOICE));
                    bool existInvoice = existsSereServBill != null && existsSereServBill.Any(a => a.SERE_SERV_ID == item.ID && a.TDL_BILL_TYPE_ID.HasValue && a.TDL_BILL_TYPE_ID.Value == HisAccountBookCFG.BILL_TYPE_ID__INVOICE);
                    decimal recieptAmount = 0;
                    decimal invoiceAmount = 0;

                    priceProcessor.Hcm115Calculator(item, ref recieptAmount, ref invoiceAmount);

                    if (!existReceipt)
                        totalRecieptAmount += recieptAmount;
                    if (!existInvoice)
                        totalInvoiceAmount += invoiceAmount;

                    if (!(existInvoice && existReceipt))
                    {
                        if (!dicPriceByType.ContainsKey(item.TDL_SERVICE_TYPE_ID))
                            dicPriceByType[item.TDL_SERVICE_TYPE_ID] = new BillAmountSDO();

                        dicPriceByType[item.TDL_SERVICE_TYPE_ID].BH += (item.VIR_TOTAL_HEIN_PRICE ?? 0);
                        if (!existReceipt)
                            dicPriceByType[item.TDL_SERVICE_TYPE_ID].VP += recieptAmount;
                        if (!existInvoice)
                            dicPriceByType[item.TDL_SERVICE_TYPE_ID].DV += invoiceAmount;
                    }
                }
            }
            else if (HisTransactionCFG.BILL_TWO_BOOK_OPTION == (int)HisTransactionCFG.ENUM_BILL_OPTION.CTO_TW)
            {
                foreach (var item in listSereServs)
                {
                    bool existReceipt = existsSereServBill != null && existsSereServBill.Any(a => a.SERE_SERV_ID == item.ID && (!a.TDL_BILL_TYPE_ID.HasValue || a.TDL_BILL_TYPE_ID != HisAccountBookCFG.BILL_TYPE_ID__INVOICE));
                    bool existInvoice = existsSereServBill != null && existsSereServBill.Any(a => a.SERE_SERV_ID == item.ID && a.TDL_BILL_TYPE_ID.HasValue && a.TDL_BILL_TYPE_ID.Value == HisAccountBookCFG.BILL_TYPE_ID__INVOICE);
                    decimal recieptAmount = 0;
                    decimal invoiceAmount = 0;

                    var recieptSSBill = data.RecieptSereServBills != null ? data.RecieptSereServBills.FirstOrDefault(o => o.SERE_SERV_ID == item.ID) : null;

                    priceProcessor.CtoTWCalcualator(item, ref recieptAmount, ref invoiceAmount, recieptSSBill);

                    if (!existReceipt)
                        totalRecieptAmount += recieptAmount;
                    if (!existInvoice)
                        totalInvoiceAmount += invoiceAmount;

                    if (!(existInvoice && existReceipt))
                    {
                        if (!dicPriceByType.ContainsKey(item.TDL_SERVICE_TYPE_ID))
                            dicPriceByType[item.TDL_SERVICE_TYPE_ID] = new BillAmountSDO();

                        dicPriceByType[item.TDL_SERVICE_TYPE_ID].BH += (item.VIR_TOTAL_HEIN_PRICE ?? 0);
                        if (!existReceipt)
                            dicPriceByType[item.TDL_SERVICE_TYPE_ID].VP += recieptAmount;
                        if (!existInvoice)
                            dicPriceByType[item.TDL_SERVICE_TYPE_ID].DV += invoiceAmount;
                    }
                }
            }
            else if (HisTransactionCFG.BILL_TWO_BOOK_OPTION == (int)HisTransactionCFG.ENUM_BILL_OPTION.QBH_CUBA)
            {
                foreach (var item in listSereServs)
                {
                    bool existReceipt = existsSereServBill != null && existsSereServBill.Any(a => a.SERE_SERV_ID == item.ID && (!a.TDL_BILL_TYPE_ID.HasValue || a.TDL_BILL_TYPE_ID != HisAccountBookCFG.BILL_TYPE_ID__INVOICE));
                    bool existInvoice = existsSereServBill != null && existsSereServBill.Any(a => a.SERE_SERV_ID == item.ID && a.TDL_BILL_TYPE_ID.HasValue && a.TDL_BILL_TYPE_ID.Value == HisAccountBookCFG.BILL_TYPE_ID__INVOICE);
                    decimal recieptAmount = 0;
                    decimal invoiceAmount = 0;

                    priceProcessor.QbhCubaCalcualator(item, ref recieptAmount, ref invoiceAmount);

                    if (!existReceipt)
                        totalRecieptAmount += recieptAmount;
                    if (!existInvoice)
                        totalInvoiceAmount += invoiceAmount;

                    if (!(existInvoice && existReceipt))
                    {
                        if (!dicPriceByType.ContainsKey(item.TDL_SERVICE_TYPE_ID))
                            dicPriceByType[item.TDL_SERVICE_TYPE_ID] = new BillAmountSDO();

                        dicPriceByType[item.TDL_SERVICE_TYPE_ID].BH += (item.VIR_TOTAL_HEIN_PRICE ?? 0);
                        if (!existReceipt)
                            dicPriceByType[item.TDL_SERVICE_TYPE_ID].VP += recieptAmount;
                        if (!existInvoice)
                            dicPriceByType[item.TDL_SERVICE_TYPE_ID].DV += invoiceAmount;
                    }
                }
            }

            if (data.RecieptTransaction != null && Math.Abs(data.RecieptTransaction.AMOUNT - totalRecieptAmount) > Constant.PRICE_DIFFERENCE)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("So tien Thanh toan bien lai cua HIS: " + data.RecieptTransaction.AMOUNT + " khong giong voi MOS: " + totalRecieptAmount);
            }

            if (data.InvoiceTransaction != null && Math.Abs(data.InvoiceTransaction.AMOUNT - totalInvoiceAmount) > Constant.PRICE_DIFFERENCE)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("So tien Thanh toan bien lai cua HIS: " + data.InvoiceTransaction.AMOUNT + " khong giong voi MOS: " + totalInvoiceAmount);
            }

            dicPrice = dicPriceByType;
        }

        private void ProcessTransactionInfo(HisTransactionBillTwoBookSDO data, V_HIS_TREATMENT_FEE_1 treatmentFee, Dictionary<long, BillAmountSDO> dicPriceByType, decimal mustPayAmount)
        {
            ExpandoObject infoObject = new ExpandoObject();
            decimal? depositAmount = new HisTreatmentGet().GetAvailableAmount(treatmentFee);
            if (depositAmount.HasValue)
            {
                HisTransactionUtil.AddProperty(infoObject, TOTAL_DEPOSIT_AMOUNT, depositAmount.Value);
            }
            V_HIS_DEPARTMENT_TRAN departTran = new HisDepartmentTranGet().GetViewLastByTreatmentId(data.TreatmentId);
            if (departTran != null)
            {
                HisTransactionUtil.AddProperty(infoObject, DEPARTMENT_CODE, departTran.DEPARTMENT_CODE);
                HisTransactionUtil.AddProperty(infoObject, DEPARTMENT_NAME, departTran.DEPARTMENT_NAME);
            }
            HisTransactionUtil.AddProperty(infoObject, MUST_PAY_AMOUNT, mustPayAmount);
            if (dicPriceByType.Count > 0)
            {
                foreach (var dic in dicPriceByType)
                {
                    BillAmountSDO a = dic.Value;
                    if (a == null) continue;
                    HIS_SERVICE_TYPE sType = HisServiceTypeCFG.DATA.FirstOrDefault(o => o.ID == dic.Key);
                    if (sType == null)
                    {
                        sType = new HisServiceTypeGet().GetById(dic.Key);
                    }

                    HisTransactionUtil.AddProperty(infoObject, AMOUNT_ + sType.SERVICE_TYPE_CODE + _BH, a.BH);
                    HisTransactionUtil.AddProperty(infoObject, AMOUNT_ + sType.SERVICE_TYPE_CODE + _VP, a.VP);
                    HisTransactionUtil.AddProperty(infoObject, AMOUNT_ + sType.SERVICE_TYPE_CODE + _DV, a.DV);
                }
            }
            if (data.RecieptTransaction != null)
            {
                HisTransactionUtil.AddProperty(infoObject, RECIEPT_AMOUNT, data.RecieptTransaction.AMOUNT);
            }
            if (data.InvoiceTransaction != null)
            {
                HisTransactionUtil.AddProperty(infoObject, INVOICE_AMOUNT, data.InvoiceTransaction.AMOUNT);
            }
            string jsonInfo = Newtonsoft.Json.JsonConvert.SerializeObject(infoObject);
            if (data.RecieptTransaction != null)
            {
                data.RecieptTransaction.TRANSACTION_INFO = jsonInfo;
            }

            if (data.InvoiceTransaction != null)
            {
                data.InvoiceTransaction.TRANSACTION_INFO = jsonInfo;
            }
        }

        private void ProcessSereServBill(HisTransactionBillTwoBookSDO data, List<HIS_SERE_SERV> sereServs)
        {
            List<HIS_SERE_SERV_BILL> sereServBills = new List<HIS_SERE_SERV_BILL>();
            if (this.recentRecieptTransaction != null)
            {
                data.RecieptSereServBills.ForEach(o =>
                {
                    o.BILL_ID = this.recentRecieptTransaction.ID;
                    o.TDL_BILL_TYPE_ID = this.recentRecieptTransaction.BILL_TYPE_ID;
                    o.TDL_TREATMENT_ID = data.TreatmentId;
                    HisSereServBillUtil.SetTdl(o, sereServs.FirstOrDefault(f => f.ID == o.SERE_SERV_ID));
                });
                sereServBills.AddRange(data.RecieptSereServBills);
            }
            if (this.recentInvoiceTransaction != null)
            {
                data.InvoiceSereServBills.ForEach(o =>
                {
                    o.BILL_ID = this.recentInvoiceTransaction.ID;
                    o.TDL_BILL_TYPE_ID = this.recentInvoiceTransaction.BILL_TYPE_ID;
                    o.TDL_TREATMENT_ID = data.TreatmentId;
                    HisSereServBillUtil.SetTdl(o, sereServs.FirstOrDefault(f => f.ID == o.SERE_SERV_ID));
                });
                sereServBills.AddRange(data.InvoiceSereServBills);
            }

            if (IsNotNullOrEmpty(sereServBills) && !this.hisSereServBillCreate.CreateList(sereServBills))
            {
                throw new Exception("Cap nhat thong tin BILL_ID cho yeu cau dich vu (His_Sere_Serv) that bai. Du lieu se bi rollback");
            }
        }

        private void ProcessAuto(HisTransactionBillTwoBookSDO data, HIS_TREATMENT treatment, WorkPlaceSDO workPlace)
        {
            V_HIS_TREATMENT_FEE_1 treatmentFee = null;
            if ((data.IsAutoRepay && treatment != null && treatment.IS_PAUSE == Constant.IS_TRUE)
                || (HisTreatmentCFG.AUTO_LOCK_AFTER_BILL && data != null))
            {
                treatmentFee = new HisTreatmentGet(param).GetFeeView1ById(treatment.ID);
            }

            bool isRepay = false;

            if (data.IsAutoRepay && treatment != null && treatment.IS_PAUSE == Constant.IS_TRUE && treatmentFee != null)
            {
                decimal? unpaid = new HisTreatmentGet().GetUnpaid(treatmentFee);
                decimal absUnpaid = Math.Abs(unpaid ?? 0);
                if (unpaid.HasValue && unpaid.Value < 0 && absUnpaid > Constant.PRICE_DIFFERENCE)
                {
                    V_HIS_ACCOUNT_BOOK repayAccount = null;
                    if (!data.RepayAccountBookId.HasValue || data.RepayAccountBookId.Value <= 0)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_KhongCoSoHoanUngTuDong);
                        throw new Exception("data.RepayAccountBookId is null");
                    }

                    HIS_TRANSACTION repayTransaction = new HIS_TRANSACTION();
                    HIS_TRANSACTION recentHisTransaction = recentRecieptTransaction != null ? recentRecieptTransaction : recentInvoiceTransaction;

                    repayTransaction.ACCOUNT_BOOK_ID = data.RepayAccountBookId.Value;
                    repayTransaction.NUM_ORDER = (data.RepayNumOrder ?? 0);
                    repayTransaction.AMOUNT = absUnpaid;
                    repayTransaction.CASHIER_ROOM_ID = workPlace.CashierRoomId.Value;
                    repayTransaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU;
                    repayTransaction.TREATMENT_ID = treatment.ID;
                    repayTransaction.CASHIER_LOGINNAME = recentHisTransaction.CASHIER_LOGINNAME;
                    repayTransaction.CASHIER_USERNAME = recentHisTransaction.CASHIER_USERNAME;
                    repayTransaction.TRANSACTION_TIME = recentHisTransaction.TRANSACTION_TIME;
                    repayTransaction.PAY_FORM_ID = recentHisTransaction.PAY_FORM_ID;

                    bool valid = true;
                    HisTransactionCheck checker = new HisTransactionCheck(param);
                    valid = valid && checker.IsUnlockAccountBook(repayTransaction.ACCOUNT_BOOK_ID, ref repayAccount);
                    valid = valid && checker.IsValidNumOrder(repayTransaction, repayAccount);
                    if (!valid)
                    {
                        throw new Exception();
                    }
                    HisTransactionUtil.SetTreatmentFeeInfo(repayTransaction);

                    if (!this.hisTransactionAutoRepayCreate.Create(repayTransaction, treatment))
                    {
                        throw new Exception("hisTransactionAutoRepayCreate. Ket thuc nghiep vu");
                    }
                    this.recentRepayTransaction = repayTransaction;
                    isRepay = true;
                }
            }

            if (HisTreatmentCFG.AUTO_LOCK_AFTER_BILL && data != null && treatment != null)
            {
                //Neu benh nhan da ket thuc dieu tri va chua duyet khoa tai chinh
                if (treatmentFee != null && treatmentFee.IS_PAUSE == MOS.UTILITY.Constant.IS_TRUE
                    && treatmentFee.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    //So tien can thu them
                    decimal? unpaid = new HisTreatmentGet().GetUnpaid(treatmentFee);

                    //Neu so tien can thu them = 0 thi tu dong duyet khoa
                    if (unpaid <= Constant.PRICE_DIFFERENCE || isRepay)
                    {
                        HIS_TREATMENT treat = new HIS_TREATMENT();

                        HisTreatmentLockSDO sdo = new HisTreatmentLockSDO();
                        sdo.RequestRoomId = data.RequestRoomId;

                        //Trong truong hop co cau hinh lay gio duyet khoa vien phi theo gio ra vien trong truong hop tu dong
                        if (HisTreatmentCFG.IS_USING_OUT_TIME_FOR_FEE_LOCK_TIME_IN_CASE_OF_AUTO
                            && treatmentFee.OUT_TIME.HasValue)
                        {
                            sdo.FeeLockTime = treatmentFee.OUT_TIME.Value;
                        }
                        else
                        {
                            sdo.FeeLockTime = Inventec.Common.DateTime.Get.Now().Value;
                        }

                        sdo.TreatmentId = treatmentFee.ID;
                        if (!this.hisTreatmentLock.Run(sdo, ref treat))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBill_KhongTuDongDuyetHoSoVienPhi);
                            LogSystem.Warn("Tu dong duyet khoa ho so dieu tri that bai. Treatment_id: " + treatment.ID);
                        }
                    }
                }
            }
        }

        private void ProcessPatient(HisTransactionBillTwoBookSDO data, HIS_TREATMENT treatment)
        {
            if (data.RecieptTransaction != null && treatment != null &&
                (!string.IsNullOrWhiteSpace(data.RecieptTransaction.BUYER_TAX_CODE)
                || !string.IsNullOrWhiteSpace(data.RecieptTransaction.BUYER_ACCOUNT_NUMBER)
                || !string.IsNullOrWhiteSpace(data.RecieptTransaction.BUYER_ORGANIZATION)))
            {
                HIS_PATIENT patient = new HisPatientGet().GetById(treatment.PATIENT_ID);
                Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();

                HIS_PATIENT old = Mapper.Map<HIS_PATIENT>(patient);
                patient.TAX_CODE = data.RecieptTransaction.BUYER_TAX_CODE;
                patient.ACCOUNT_NUMBER = data.RecieptTransaction.BUYER_ACCOUNT_NUMBER;
                patient.WORK_PLACE = data.RecieptTransaction.BUYER_ORGANIZATION;

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
            else if (data.InvoiceTransaction != null && treatment != null &&
                (!string.IsNullOrWhiteSpace(data.InvoiceTransaction.BUYER_TAX_CODE)
                || !string.IsNullOrWhiteSpace(data.InvoiceTransaction.BUYER_ACCOUNT_NUMBER)
                || !string.IsNullOrWhiteSpace(data.InvoiceTransaction.BUYER_ORGANIZATION)))
            {
                HIS_PATIENT patient = new HisPatientGet().GetById(treatment.PATIENT_ID);
                Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();

                HIS_PATIENT old = Mapper.Map<HIS_PATIENT>(patient);
                patient.TAX_CODE = data.InvoiceTransaction.BUYER_TAX_CODE;
                patient.ACCOUNT_NUMBER = data.InvoiceTransaction.BUYER_ACCOUNT_NUMBER;
                patient.WORK_PLACE = data.InvoiceTransaction.BUYER_ORGANIZATION;

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

        private void PassResult(ref List<V_HIS_TRANSACTION> resultData)
        {
            resultData = new List<V_HIS_TRANSACTION>();
            if (this.recentRecieptTransaction != null)
            {
                resultData.Add(new HisTransactionGet(param).GetViewById(this.recentRecieptTransaction.ID));
            }

            if (this.recentInvoiceTransaction != null)
            {
                resultData.Add(new HisTransactionGet(param).GetViewById(this.recentInvoiceTransaction.ID));
            }

            if (this.recentRepayTransaction != null)
            {
                resultData.Add(new HisTransactionGet(param).GetViewById(this.recentRepayTransaction.ID));
            }
        }

        private void SetServerTime(HisTransactionBillTwoBookSDO data, long time)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                if (data.RecieptTransaction != null)
                {
                    data.RecieptTransaction.TRANSACTION_TIME = time;
                }
                if (data.InvoiceTransaction != null)
                {
                    data.InvoiceTransaction.TRANSACTION_TIME = time;
                }
            }
        }

        private void SetSessionCode(HisTransactionBillTwoBookSDO data, string sessionCode)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (data != null)
            {
                if (data.RecieptTransaction != null)
                {
                    data.RecieptTransaction.SESSION_CODE = sessionCode;
                }
                if (data.InvoiceTransaction != null)
                {
                    data.InvoiceTransaction.SESSION_CODE = sessionCode;
                }
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisTransactionAutoRepayCreate.RollbackData();
                this.hisSereServBillCreate.RollbackData();
                this.hisTransactionCreate.RollbackData();
                this.RollbackTreatment();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
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
