using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisAccountBook.Authority;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisCarerCardBorrow;
using MOS.MANAGER.HisFinancePeriod;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill
{
    class HisTransactionBillCheck : BusinessBase
    {
        internal HisTransactionBillCheck()
            : base()
        {

        }

        internal HisTransactionBillCheck(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// Kiem tra thuc hien giao dich thanh toan (Phuc vu giao dich bang the onelink)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisTransactionBillSDO data, bool isGenNumOrder, ref WorkPlaceSDO workPlace, ref V_HIS_ACCOUNT_BOOK hisAccountBook, ref HIS_TREATMENT hisTreatment, ref List<HIS_SERE_SERV> sereServs, ref HIS_TRANSACTION originalTransaction, ref decimal exemption, ref decimal fundPaidTotal, ref decimal? transferAmount)
        {
            bool valid = true;
            try
            {
                bool hasTransaction = data.Transaction != null || data.RepayAmount.HasValue;

                //set lai du lieu tam thoi trong truong hop front-end chua sua kip
                if (data.Transaction != null)
                {
                    data.TransactionTime = data.Transaction.TRANSACTION_TIME;
                    data.PayFormId = data.Transaction.PAY_FORM_ID;
                }

                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisFinancePeriodCheck financePeriodChecker = new HisFinancePeriodCheck();

                decimal? availableAmount = null;

                valid = valid && this.IsValidData(data, ref hisTreatment);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                //Neu ko phai duoc uy quyen thi tai khoan phai lam viec tai phong thu ngan
                valid = valid && checker.IsCashierRoom(workPlace);

                //Neu co nghiep vu tao giao dich (thanh toan hoac hoan ung) thi phai kiem tra xem ho so co dang tao giao dich nao khac ko
                valid = valid && (!hasTransaction || checker.IsNotCreatingTransaction(hisTreatment));
                valid = valid && (!hasTransaction || this.IsValidAmount(data, ref exemption, ref fundPaidTotal, ref transferAmount, ref availableAmount));
                //Neu co thong tin giao dich thi kiem tra so giao dich thanh toan
                valid = valid && (data.Transaction == null || checker.IsUnlockAccountBook(data.Transaction.ACCOUNT_BOOK_ID, ref hisAccountBook));
                valid = valid && (!hasTransaction || financePeriodChecker.HasNotFinancePeriod(workPlace.BranchId, data.TransactionTime));

                valid = valid && this.IsValidSereServ(data, ref sereServs);
                valid = valid && checker.CheckMustFinishTreatmentForBill(sereServs, hisTreatment);
                valid = valid && this.IsValidSereServBill(sereServs, data.SereServBills, data.Transaction);
                valid = valid && this.IsValidCarerCardBorrow(sereServs);
                valid = valid && checker.IsHasDocumentInfo(sereServs);
                valid = valid && (!isGenNumOrder || this.IsGetNumOrderFromOldSystem(data.Transaction, hisAccountBook));

                valid = valid && checker.IsValidNumOrder(data.Transaction, hisAccountBook);
                valid = valid && this.IsValidRepayData(data, hisTreatment, availableAmount, transferAmount);
                valid = valid && this.IsValidOriginalTransaction(data, ref originalTransaction);
                valid = valid && this.IsValidOutTime(hisTreatment, data.Transaction);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsValidOriginalTransaction(HisTransactionBillSDO data, ref HIS_TRANSACTION originalTransaction)
        {
            try
            {
                if (data.OriginalTransactionId.HasValue)
                {
                    bool valid = true;
                    HisTransactionCheck tranCheck = new HisTransactionCheck(param);
                    valid = valid && tranCheck.VerifyId(data.OriginalTransactionId.Value, ref originalTransaction);
                    valid = valid && tranCheck.IsBill(new List<HIS_TRANSACTION>() { originalTransaction });
                    valid = valid && tranCheck.IsCancel(originalTransaction);
                    valid = valid && tranCheck.HasReplaceReason(data.ReplaceReason);
                    valid = valid && tranCheck.HasNoReplaceTransaction(data.OriginalTransactionId.Value);
                    return valid;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return true;
        }

        internal bool IsValidData(HisTransactionBillSDO data, ref HIS_TREATMENT hisTreatment)
        {
            bool result = true;
            try
            {
                //Co xay ra nghiep vu thanh toan hoac co xay ra nghiep vu hoan ung thi bat buoc phai co thong tin "hinh thuc giao dich" va "thoi gian giao dich"
                if ((data.IsAutoRepay && data.RepayAmount.HasValue) || data.Transaction != null)
                {
                    if (data.PayFormId <= 0 || data.TransactionTime <= 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_ThieuThongTinHinhThucHoacThoiGian);
                        return false;
                    }
                }

                //Neu khong cho phep thuc hien nghiep vu "ko giao dich" thi bat buoc phai co thong tin giao dich
                if (!HisTransactionCFG.IS_ALLOW_PROCESS_WITH_NO_TRANSACTION && !IsNotNull(data.Transaction))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.Transaction null");
                    return false;
                }

                //Neu co thong tin ho so thi kiem tra xem ho so co hop le khong
                if (data.Transaction != null && data.Transaction.TREATMENT_ID.HasValue)
                {
                    return new HisTreatmentCheck(param).VerifyId(data.Transaction.TREATMENT_ID.Value, ref hisTreatment);
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

        internal bool IsValidRepayData(HisTransactionBillSDO data, HIS_TREATMENT treatment, decimal? availableAmount, decimal? transferAmount)
        {
            bool result = true;
            try
            {
                V_HIS_ACCOUNT_BOOK repayAccountBook = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);

                //Neu co so tien can hoan ung va co check "tu dong hoan ung" thi bat buoc phai co thong tin so hoan ung
                if (data.IsAutoRepay && data.RepayAmount.HasValue)
                {
                    if (data.RepayAmount <= 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_SoTienHoanUngBeHonHoacBang0);
                        return false;
                    }
                    if (!data.RepayAccountBookId.HasValue || data.RepayAccountBookId.Value <= 0)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_KhongCoSoHoanUngTuDong);
                        return false;
                    }
                    //Neu co so tien hoan ung thi kiem tra
                    if (data.RepayAmount.HasValue)
                    {
                        //Voi truong hop ko co ket chuyen thi can dam bao: so tien hien du = hoan ung
                        if (!transferAmount.HasValue && Math.Abs((availableAmount ?? 0) - data.RepayAmount.Value) > Constant.PRICE_DIFFERENCE)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_SoTienHoanUngKhongChoPhep);
                            LogSystem.Warn("abs(Hiện dư - Hoàn ứng) > 0.0001. Hien du: " + availableAmount + "; Hoan ung: " + data.RepayAmount.Value);
                            return false;
                        }
                        //Voi truong hop co ket chuyen thi can dam bao: hien du + can thu = ket chuyen + hoan ung
                        else if (transferAmount.HasValue && Math.Abs((availableAmount ?? 0) + data.PayAmount - (transferAmount ?? 0) - data.RepayAmount.Value) > Constant.PRICE_DIFFERENCE)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_SoTienHoanUngKhongChoPhep);
                            LogSystem.Warn("abs(Hiện dư + Cần thu - Kết chuyển - Hoàn ứng) > 0.0001. Hien du: " + availableAmount + "; Can thu: " + data.PayAmount + "; Ket chuyen: " + (transferAmount ?? 0) + "; Hoan ung: " + data.RepayAmount.Value);
                            return false;
                        }
                    }

                    HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                    //Chi cho phep thuc hien tu dong hoan ung neu ho so da ket thuc dieu tri
                    result = result && treatmentChecker.IsPause(treatment);
                    //Kiem tra them so hoan ung va so chung tu hoan ung
                    result = result && checker.IsUnlockAccountBook(data.RepayAccountBookId.Value, ref repayAccountBook);
                    result = result && checker.IsValidNumOrder(data.RepayNumOrder, repayAccountBook);
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

        /// <summary>
        /// Can tach, ko de chung trong ham "run" o tren do ham o tren co cho phep client goi de check
        /// truoc khi goi sang he thong the de thanh toan (tai thoi diem nay thi chua co thong tin
        /// giao dich bang the: TIG_TRANSACTION_CODE)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsValidCardTransaction(HisTransactionBillSDO data, HIS_TREATMENT treatment, ref HIS_CARD card)
        {
            bool result = true;
            try
            {
                if (data.PayFormId == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE)
                {
                    if ((data.PayAmount > 0 || (data.RepayAmount ?? 0) > 0) && (string.IsNullOrWhiteSpace(data.TigTransactionCode)
                        || !data.TigTransactionTime.HasValue
                        || string.IsNullOrWhiteSpace(data.CardCode)))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("data.PayFormId == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE nhung PayAmount > 0 hoac RepayAmount > 0 nhung data.TigTransactionCode, data.TigTransactionTime hoac data.CardCode khong co gia tri");
                        return false;
                    }
                    if (IsNotNullOrEmpty(data.CardCode))
                    {
                        //Ho tro ca truyen len serviceCode hoac cardCode
                        card = new HisCardGet().GetByCode(data.CardCode);
                        if (card == null)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCard_KhongLayDuocThongTinTheYTe, data.CardCode);
                            return false;
                        }
                        if (treatment != null && card.PATIENT_ID != treatment.PATIENT_ID)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCard_KhongTrungThongTinVoiChuThe, treatment.TDL_PATIENT_NAME, data.CardCode);
                            return false;
                        }
                    }
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

        internal bool IsGetNumOrderFromOldSystem(HIS_TRANSACTION data, V_HIS_ACCOUNT_BOOK accountBook)
        {
            try
            {
                if (data == null)
                {
                    return true;
                }

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

                    long? num = new OldSystem.HMS.InvoiceConsumer(OldSystemCFG.ADDRESS).Get(accountBook.TEMPLATE_CODE);
                    if (!num.HasValue)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_KhongLayDuocSoHoaDonTuPMS);
                        return false;
                    }
                    data.NUM_ORDER = num.Value;
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

        private bool IsAuthorized(HIS_TRANSACTION transaction, long workingRoomId)
        {
            V_HIS_CASHIER_ROOM cashierRoom = HisCashierRoomCFG.DATA.Where(o => o.ID == transaction.CASHIER_ROOM_ID).FirstOrDefault();
            if (cashierRoom == null)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                LogSystem.Warn("CASHIER_ROOM_ID ko hop le");
                return false;
            }
            if (!new HisAccountBookAuthorityProcessor(param).IsAuthorized(transaction.CASHIER_LOGINNAME, transaction.CASHIER_USERNAME, cashierRoom.ROOM_ID, transaction.ACCOUNT_BOOK_ID, workingRoomId))
            {
                return false;
            }
            return true;
        }

        private bool IsValidAmount(HisTransactionBillSDO data, ref decimal exemption, ref decimal fundPaidTotal, ref decimal? transferAmount, ref decimal? availableAmount)
        {
            if (data != null && data.Transaction != null)
            {
                //Neu gui len chi tiet dich vu can thanh toan thi kiem tra tong so tien cua chi tiet phai khop voi du lieu tong
                if (IsNotNullOrEmpty(data.SereServBills))
                {
                    //so tien mien giam
                    exemption = data.Transaction.EXEMPTION.HasValue ? data.Transaction.EXEMPTION.Value : 0;

                    //so tien cac quy chi tra
                    fundPaidTotal = data.Transaction.HIS_BILL_FUND != null ? data.Transaction.HIS_BILL_FUND.Sum(o => o.AMOUNT) : 0;
                    if (exemption + fundPaidTotal > data.Transaction.AMOUNT)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBill_TongSoTienMienGiamVaQuyChiTraKhongDuocLonHonTongSoTienPhaiThanhToanDichVu);
                        return false;
                    }

                    //Tong so tien trong sere_serv_bill phai bang so tien trong transaction
                    decimal sereServBillTotal = data.SereServBills.Sum(o => o.PRICE);
                    if (sereServBillTotal != data.Transaction.AMOUNT)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Tong so tien trong sere_serv_bill ko khop voi so tien trong transaction");
                        return false;
                    }
                }

                if (transferAmount > 0 && data.PayFormId == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE)
                {
                    HisTransactionFilterQuery filter = new HisTransactionFilterQuery();
                    filter.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU;
                    filter.TREATMENT_ID = data.TreatmentId;
                    filter.IS_CANCEL = false;
                    List<HIS_TRANSACTION> trans = new HisTransactionGet().Get(filter);
                    if (IsNotNullOrEmpty(trans))
                    {
                        List<HIS_TRANSACTION> payFormId = trans.Where(t => t.PAY_FORM_ID != IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE).ToList();
                        List<string> transactonCodes = payFormId.Select(o => o.TRANSACTION_CODE).ToList();
                        string transactionCodeStr = string.Join(",", transactonCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_TonTaiGiaoDichTamUngKhongSuDungThe, transactionCodeStr);
                        return false;
                    }
                }

                if (!data.Transaction.IS_DEBT_COLLECTION.HasValue || data.Transaction.IS_DEBT_COLLECTION.Value != Constant.IS_TRUE)
                {
                    //data.Transaction.KC_AMOUNT = HisTransactionBillUtil.CalcTransferAmount(param, data.Transaction.TREATMENT_ID, data.PayAmount, exemption, fundPaidTotal, data.Transaction.AMOUNT);
                    transferAmount = null;

                    //So tien benh nhan phai tra
                    decimal mustPaid = data.Transaction.AMOUNT - fundPaidTotal - exemption;

                    V_HIS_TREATMENT_FEE_1 treatmentFee = new HisTreatmentGet().GetFeeView1ById(data.Transaction.TREATMENT_ID.Value);

                    //Tong so tien cho phep dung de ket chuyen (so tien Hiện dư)
                    availableAmount = new HisTreatmentGet().GetAvailableAmount(treatmentFee);

                    //Neu so tien thanh toan khac so tien can giao dich ==> co ket chuyen
                    if (data.PayAmount != mustPaid && data.Transaction.TREATMENT_ID.HasValue)
                    {
                        decimal calcPayAmount = 0;

                        //Nếu Hiện dư >= Số tiền (tổng số tiền các dịch vụ cần thanh toán) --> "Cần thu" = 0, KC_AMOUNT = "Số tiền"
                        //Nếu Hiện dư < Số tiền (tổng số tiền các dịch vụ cần thanh toán) --> "Cần thu" = Số tiền - Hiện dư, KC_AMOUNT = Hiện dư
                        //Nếu Hiện dư=0 --> KC_amount=null
                        if (availableAmount <= 0)
                        {
                            transferAmount = null;
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
                            transferAmount = availableAmount;
                        }

                        //Neu so tien server tinh lech so voi so tien client tinh qua 0.0001 thi ko cho thanh toan
                        //(so sanh voi 0.0001 la vi de tranh truong hop client lam tron den 4 chu so sau phan thap phan)
                        if (Math.Abs(calcPayAmount - data.PayAmount) > Constant.PRICE_DIFFERENCE)
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            LogSystem.Warn("So tien can thu do server tinh la: " + calcPayAmount + " khac voi so tien do client y/c la: " + data.PayAmount);
                            return false;
                        }
                    }
                }

                return true;
            }
            return false;
        }

        private bool IsValidSereServ(HisTransactionBillSDO data, ref List<HIS_SERE_SERV> sereServs)
        {
            try
            {
                if (data.Transaction != null && data.Transaction.TREATMENT_ID.HasValue && IsNotNullOrEmpty(data.SereServBills))
                {
                    //Lay danh sach sere_serv tuong ung voi ho so
                    List<long> sereServIds = data.SereServBills.Select(o => o.SERE_SERV_ID).Distinct().ToList();
                    HisSereServFilterQuery filter = new HisSereServFilterQuery();
                    filter.IDs = sereServIds;
                    filter.TREATMENT_ID = data.Transaction.TREATMENT_ID;
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

        private bool IsValidSereServBill(List<HIS_SERE_SERV> sereServs, List<HIS_SERE_SERV_BILL> sereServBills, HIS_TRANSACTION transaction)
        {
            try
            {
                if (IsNotNullOrEmpty(sereServBills) && IsNotNullOrEmpty(sereServs))
                {
                    //Lay danh sach sere_serv tuong ung voi ho so
                    List<long> sereServIds = sereServBills.Select(o => o.SERE_SERV_ID).Distinct().ToList();

                    //Neu ko phai la giao dich "thu no", thi ko cho phep thanh toan doi voi cac dv da cho "cong no"
                    if (transaction.IS_DEBT_COLLECTION == null)
                    {
                        List<HIS_SERE_SERV_DEBT> existsDebts = new HisSereServDebtGet().GetNoCancelBySereServIds(sereServIds);
                        if (IsNotNullOrEmpty(existsDebts))
                        {
                            List<string> names = existsDebts.Select(o => o.TDL_SERVICE_NAME).Distinct().ToList();
                            string nameStr = string.Join(",", names);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaChotNo, nameStr);
                            return false;
                        }
                    }

                    //Lay danh sach thong tin thanh toan (va chua bi huy) tuong ung voi sere_serv
                    List<HIS_SERE_SERV_BILL> existsBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
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
                        if ((Math.Abs(totalBill - s.VIR_TOTAL_PATIENT_PRICE.Value)) > Constant.PRICE_DIFFERENCE)
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

        internal bool IsValidCarerCardBorrow(List<HIS_SERE_SERV> sereServs)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV> ssCarerCardBorrow = IsNotNullOrEmpty(sereServs) ? sereServs.Where(o => o.TDL_CARER_CARD_BORROW_ID.HasValue).ToList() : null;
                if (IsNotNullOrEmpty(ssCarerCardBorrow))
                {
                    HisCarerCardBorrowFilterQuery filter = new HisCarerCardBorrowFilterQuery();
                    filter.IDs = ssCarerCardBorrow.Select(o => o.TDL_CARER_CARD_BORROW_ID.Value).ToList();
                    filter.HAS_GIVE_BACK_TIME = false;
                    List<HIS_CARER_CARD_BORROW> borrows = new HisCarerCardBorrowGet().Get(filter);

                    if (IsNotNullOrEmpty(borrows))
                    {
                        var invalidSS = sereServs.Where(o => o.TDL_CARER_CARD_BORROW_ID.HasValue && borrows.Select(s => s.ID).Contains(o.TDL_CARER_CARD_BORROW_ID.Value)).ToList();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_ChuaThucHienTraTheHoacBaoMatThe, string.Join(", ", invalidSS.Select(o => o.TDL_SERVICE_NAME)));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }

            return valid;
        }

        public bool IsValidOutTime(HIS_TREATMENT treatment, HIS_TRANSACTION transaction)
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
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
