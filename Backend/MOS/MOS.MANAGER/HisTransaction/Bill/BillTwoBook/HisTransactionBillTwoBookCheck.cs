using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill.BillTwoBook
{
    class HisTransactionBillTwoBookCheck : BusinessBase
    {
        internal HisTransactionBillTwoBookCheck()
            : base()
        {

        }

        internal HisTransactionBillTwoBookCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisTransactionBillTwoBookSDO data, bool isGenNumOrder, ref WorkPlaceSDO workPlace, ref V_HIS_ACCOUNT_BOOK receiptAccountBook, ref V_HIS_ACCOUNT_BOOK invoiceAccountBook, ref HIS_TREATMENT hisTreatment, ref List<HIS_SERE_SERV> sereServs, ref List<HIS_SERE_SERV_BILL> existsBills)
        {
            bool valid = true;
            try
            {
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                List<HIS_SERE_SERV> listSereServ = new List<HIS_SERE_SERV>();
                List<HIS_SERE_SERV_BILL> listSereServBill = new List<HIS_SERE_SERV_BILL>();
                valid = valid && IsNotNull(data);
                valid = valid && (IsNotNull(data.RecieptTransaction) || IsNotNull(data.InvoiceTransaction));
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.IsCashierRoom(workPlace);
                valid = valid && treatmentChecker.VerifyId(data.TreatmentId, ref hisTreatment);
                valid = valid && checker.IsNotCreatingTransaction(hisTreatment);
                if (valid && IsNotNull(data.RecieptTransaction))
                {
                    data.RecieptTransaction.TREATMENT_ID = data.TreatmentId;
                    List<HIS_SERE_SERV> ss = new List<HIS_SERE_SERV>();
                    valid = valid && IsNotNullOrEmpty(data.RecieptSereServBills);
                    valid = valid && this.IsValidAmount(data.RecieptTransaction, data.RecieptSereServBills);
                    valid = valid && checker.IsUnlockAccountBook(data.RecieptTransaction.ACCOUNT_BOOK_ID, ref receiptAccountBook);
                    valid = valid && checker.HasNotFinancePeriod(data.RecieptTransaction);
                    valid = valid && this.IsValidSereServ(data.RecieptTransaction, data.TreatmentId, data.RecieptSereServBills, ref ss);
                    valid = valid && this.IsValidOutTime(hisTreatment, data.RecieptTransaction);
                    valid = valid && this.IsGetNumOrderFromOldSystem(isGenNumOrder, data.RecieptTransaction, receiptAccountBook);
                    valid = valid && this.IsValidNumberOrder(isGenNumOrder, data.RecieptTransaction, receiptAccountBook);
                    if (valid)
                    {
                        listSereServ.AddRange(ss);
                        listSereServBill.AddRange(data.RecieptSereServBills);
                    }
                }
                if (valid && IsNotNull(data.InvoiceTransaction))
                {
                    data.InvoiceTransaction.TREATMENT_ID = data.TreatmentId;
                    List<HIS_SERE_SERV> ss = new List<HIS_SERE_SERV>();
                    valid = valid && IsNotNullOrEmpty(data.InvoiceSereServBills);
                    valid = valid && this.IsValidAmount(data.InvoiceTransaction, data.InvoiceSereServBills);
                    valid = valid && checker.IsUnlockAccountBook(data.InvoiceTransaction.ACCOUNT_BOOK_ID, ref invoiceAccountBook);
                    valid = valid && checker.HasNotFinancePeriod(data.InvoiceTransaction);
                    valid = valid && this.IsValidSereServ(data.InvoiceTransaction, data.TreatmentId, data.InvoiceSereServBills, ref ss);
                    valid = valid && this.IsValidOutTime(hisTreatment, data.InvoiceTransaction);
                    valid = valid && this.IsGetNumOrderFromOldSystem(isGenNumOrder, data.InvoiceTransaction, invoiceAccountBook);
                    valid = valid && this.IsValidNumberOrder(isGenNumOrder, data.InvoiceTransaction, invoiceAccountBook);
                    if (valid)
                    {
                        var notExists = ss.Where(o => listSereServ == null || !listSereServ.Any(a => a.ID == o.ID)).ToList();
                        if (IsNotNullOrEmpty(notExists))
                        {
                            listSereServ.AddRange(notExists);
                        }
                        listSereServBill.AddRange(data.InvoiceSereServBills);
                    }
                }
                valid = valid && checker.CheckMustFinishTreatmentForBill(listSereServ, hisTreatment);
                valid = valid && this.IsValidSereServBill(listSereServ, listSereServBill, ref existsBills);
                valid = valid && (!listSereServ.Any(a => a.MATERIAL_ID.HasValue) || checker.IsHasDocumentInfo(listSereServ.Where(o => o.MATERIAL_ID.HasValue).Select(s => s.MATERIAL_ID.Value).ToList()));
                if (valid) sereServs = listSereServ;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        private bool IsValidAmount(HIS_TRANSACTION data, List<HIS_SERE_SERV_BILL> sereServBills)
        {
            if (data != null && IsNotNullOrEmpty(sereServBills))
            {
                //so tien mien giam
                decimal exemption = data.EXEMPTION.HasValue ? data.EXEMPTION.Value : 0;

                //so tien cac quy chi tra
                decimal fundPaidTotal = data.HIS_BILL_FUND != null ? data.HIS_BILL_FUND.Sum(o => o.AMOUNT) : 0;
                if (exemption + fundPaidTotal > data.AMOUNT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBill_TongSoTienMienGiamVaQuyChiTraKhongDuocLonHonTongSoTienPhaiThanhToanDichVu);
                    return false;
                }

                //Tong so tien trong sere_serv_bill phai bang so tien trong transaction
                decimal sereServBillTotal = sereServBills.Sum(o => o.PRICE);
                if (sereServBillTotal != data.AMOUNT)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Tong so tien trong sere_serv_bill ko khop voi so tien trong transaction");
                    return false;
                }

                return true;
            }
            return false;
        }

        private bool IsValidSereServ(HIS_TRANSACTION data, long treatmentId, List<HIS_SERE_SERV_BILL> sereServBills, ref List<HIS_SERE_SERV> sereServs)
        {
            try
            {
                if (data != null && IsNotNullOrEmpty(sereServBills))
                {
                    //Lay danh sach sere_serv tuong ung voi ho so
                    List<long> sereServIds = sereServBills.Select(o => o.SERE_SERV_ID).Distinct().ToList();
                    HisSereServFilterQuery filter = new HisSereServFilterQuery();
                    filter.IDs = sereServIds;
                    filter.TREATMENT_ID = treatmentId;
                    List<HIS_SERE_SERV> ss = new HisSereServGet().Get(filter);

                    List<long> invalidIds = sereServIds != null ? sereServIds.Where(o => ss == null || !ss.Exists(t => t.ID == o)).ToList() : null;

                    if (IsNotNullOrEmpty(invalidIds))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuKhongHopLe);
                        LogSystem.Warn("Loi du lieu. Ton tai sere_serv_id gui len ko co tren he thong hoac thuoc ho so dieu tri khac");
                        return false;
                    }
                    sereServs = ss;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        private bool IsValidSereServBill(List<HIS_SERE_SERV> sereServs, List<HIS_SERE_SERV_BILL> sereServBills, ref List<HIS_SERE_SERV_BILL> existsBills)
        {
            try
            {
                if (IsNotNullOrEmpty(sereServBills) && IsNotNullOrEmpty(sereServs))
                {
                    //Lay danh sach sere_serv tuong ung voi ho so
                    List<long> sereServIds = sereServBills.Select(o => o.SERE_SERV_ID).Distinct().ToList();

                    //Lay danh sach thong tin "cong no" (va chua bi huy) tuong ung voi sere_serv
                    List<HIS_SERE_SERV_DEBT> existsDebts = new HisSereServDebtGet().GetNoCancelBySereServIds(sereServIds);
                    if (IsNotNullOrEmpty(existsDebts))
                    {
                        List<string> names = existsDebts.Select(o => o.TDL_SERVICE_NAME).Distinct().ToList();
                        string nameStr = string.Join(",", names);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaChotNo, nameStr);
                        return false;
                    }

                    //Lay danh sach thong tin thanh toan (va chua bi huy) tuong ung voi sere_serv
                    existsBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                    List<HIS_SERE_SERV_BILL> allBills = new List<HIS_SERE_SERV_BILL>();
                    allBills.AddRange(sereServBills);
                    if (IsNotNullOrEmpty(existsBills))
                    {
                        allBills.AddRange(existsBills);
                    }

                    List<string> serviceNames = new List<string>();
                    foreach (HIS_SERE_SERV s in sereServs)
                    {
                        decimal totalBill = allBills.Where(o => o.SERE_SERV_ID == s.ID).Sum(o => o.PRICE);

                        //Luu y: check lech tien voi "Constant.PRICE_DIFFERENCE", de tranh truong hop lam tron 
                        //(4 so sau phan thap phan) 
                        if (Math.Abs(totalBill - s.VIR_TOTAL_PATIENT_PRICE.Value) > Constant.PRICE_DIFFERENCE)
                        {
                            serviceNames.Add(s.TDL_SERVICE_NAME);
                        }
                    }

                    if (IsNotNullOrEmpty(serviceNames))
                    {
                        string nameStr = string.Join(",", serviceNames);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_KhacSoTienCanThanhToan, nameStr);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal bool IsValidCardTransaction(HisTransactionBillTwoBookSDO data)
        {
            bool result = true;
            try
            {
                if (data.RecieptTransaction != null && data.RecieptTransaction.PAY_FORM_ID == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE
                    && string.IsNullOrWhiteSpace(data.RecieptTransaction.TIG_TRANSACTION_CODE))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Thanh toan su dung the bat buoc phai co thong tin TIG_TRANSACTION_CODE.");
                    return false;
                }
                if (data.InvoiceTransaction != null && data.InvoiceTransaction.PAY_FORM_ID == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE
                    && string.IsNullOrWhiteSpace(data.InvoiceTransaction.TIG_TRANSACTION_CODE))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Thanh toan su dung the bat buoc phai co thong tin TIG_TRANSACTION_CODE.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool IsGetNumOrderFromOldSystem(bool isGenNumOrder, HIS_TRANSACTION data, V_HIS_ACCOUNT_BOOK accountBook)
        {
            try
            {
                if (accountBook.IS_NOT_GEN_TRANSACTION_ORDER != Constant.IS_TRUE)
                {
                    return true;
                }

                if (data.NUM_ORDER <= 0 && OldSystemCFG.INTEGRATION_TYPE != OldSystemCFG.IntegrationType.PMS)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoBienLaiKhongDuocDeTrong);
                    return false;
                }

                if (OldSystemCFG.INTEGRATION_TYPE == OldSystemCFG.IntegrationType.PMS)
                {
                    if (String.IsNullOrWhiteSpace(accountBook.TEMPLATE_CODE))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoBienLaiKhongCoMauSo, accountBook.ACCOUNT_BOOK_NAME);
                        return false;
                    }

                    //Trong truong hop xu ly check khi thanh toan dien tu thi client se chu dong goi api check truoc khi goi api luu, 
                    //vi vay, can bo qua xu ly goi cap so hoa don de tranh bi mat so hoa don do bi cap 2 lan (cap 1 lan luc api check va cap 1 lan o api luu)
                    if (isGenNumOrder)
                    {
                        long? num = new OldSystem.HMS.InvoiceConsumer(OldSystemCFG.ADDRESS).Get(accountBook.TEMPLATE_CODE);
                        if (!num.HasValue)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_KhongLayDuocSoHoaDonTuPMS);
                            return false;
                        }
                        data.NUM_ORDER = num.Value;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }

        private bool IsValidNumberOrder(bool isGenNumOrder, HIS_TRANSACTION data, V_HIS_ACCOUNT_BOOK accountBook)
        {
            //Trong truong hop xu ly check khi thanh toan dien tu thi client se chu dong goi api check truoc khi goi api luu, 
            //vi vay, can bo qua xu ly goi cap so chung tu de tranh bi mat so chung tu do bi cap 2 lan (cap 1 lan luc api check va cap 1 lan o api luu)
            //khi do, api check can bo qua nghiep vu kiem tra so chung tu
            if (accountBook.IS_NOT_GEN_TRANSACTION_ORDER == Constant.IS_TRUE && OldSystemCFG.INTEGRATION_TYPE == OldSystemCFG.IntegrationType.PMS && !isGenNumOrder)
            {
                return true;
            }
            else
            {
                return new HisTransactionCheck(param).IsValidNumOrder(data, accountBook);
            }
        }

        private bool IsValidOutTime(HIS_TREATMENT treatment, HIS_TRANSACTION transaction)
        {
            bool valid = true;
            try
            {
                HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.Where(o => o.ID == treatment.TDL_TREATMENT_TYPE_ID).FirstOrDefault();
                if (treatmentType.TRANS_TIME_OUT_TIME_OPTION == 2)
                {
                    if (transaction.TRANSACTION_TIME < treatment.OUT_TIME)
                    {
                        string transactionTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(transaction.TRANSACTION_TIME);
                        string outTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(treatment.OUT_TIME ?? 0);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_ThoiGianThanhToanNhoHonThoiGianRaVien
, transactionTime, outTime);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return valid;
        }
    }
}
