using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepositReq;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTreatment;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseTransReq;
using System.Globalization;

namespace MOS.MANAGER.HisTransReq.BIDV
{
    class QrPayment : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisTransReqUpdate hisTransReqUpdate;
        private HisSereServBillCreate hisSereServBillCreate;
        private HisSereServDepositCreate hisSereServDepositCreate;
        private HisDepositReqUpdate hisDepositReqUpdate;

        private List<HIS_TRANSACTION> recentTransactions = new List<HIS_TRANSACTION>();
        private List<HIS_TRANS_REQ> recentTransReqs = new List<HIS_TRANS_REQ>();

        internal QrPayment(CommonParam param)
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
            this.hisDepositReqUpdate = new HisDepositReqUpdate(param);
        }

        internal bool Run(TDO.PaymentBidvTDO data, ref TDO.PaymentBidvResultTDO resultData)
        {
            resultData = new TDO.PaymentBidvResultTDO();
            bool result = false;
            try
            {
                bool valid = true;
                decimal amount = 0;
                HIS_TRANS_REQ raw = null;
                QrPaymentCheck checker = new QrPaymentCheck(param);
                valid = valid && checker.ValidHisTransReq(data, resultData, ref raw);
                valid = valid && checker.ValidConfig(data, resultData, raw);
                valid = valid && checker.ValidInput(data, resultData, ref amount);
                valid = valid && checker.CheckSum(data, resultData);
                if (valid)
                {
                    V_HIS_ROOM requestRoom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == (raw.REQUEST_ROOM_ID ?? -1));
                    if (HisTransReqCFG.QRPAYMENT_CASHIER_ROOM_OPTION == HisTransReqCFG.QrCashierRoomOption.BY_CASHIER_ROOM)
                    {
                        if (requestRoom == null)
                        {
                            resultData.code = BIDVUtil.PaymentCode__PaymentExeption;
                            Inventec.Common.Logging.LogSystem.Error("Không xác định phòng tạo yêu cầu thanh toán (REQUEST_ROOM_ID): " + raw.REQUEST_ROOM_ID ?? "null");
                            return false;
                        }
                        else if (requestRoom.DEFAULT_CASHIER_ROOM_ID == null)
                        {
                            resultData.code = BIDVUtil.PaymentCode__PaymentExeption;
                            Inventec.Common.Logging.LogSystem.Error(String.Format("Phòng tạo yêu cầu thanh toán ({0} - {1}) chưa thiết lập thông tin phòng thu ngân mặc định", requestRoom.ROOM_CODE, requestRoom.ROOM_NAME));
                            return false;
                        }
                    }

                    V_HIS_TREATMENT_FEE_1 treatmentFee = null;

                    if (raw.TRANS_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FINISHED)
                    {
                        resultData.code = BIDVUtil.PaymentCode__AlreadyPaid;
                        resultData.data = new { txnId = data.txnId };
                        resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransReq_DonHangDaDuocThanhToan, param.LanguageCode);
                        Inventec.Common.Logging.LogSystem.Error("Yêu cầu thanh toán đã hoàn thành");
                        return false;
                    }
                    else if (raw.TRANS_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__CANCEL || raw.TRANS_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FAILED)
                    {
                        resultData.code = BIDVUtil.PaymentCode__ProductsOutOfStock;
                        resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransReq_KhoHangDaHetSanPham, param.LanguageCode);
                        Inventec.Common.Logging.LogSystem.Error("Yêu cầu thanh toán đã thất bại hoặc bị hủy");
                        return false;
                    }
                    else if (!checker.VerifyTreatmentFee(raw.TREATMENT_ID, ref treatmentFee))
                    {
                        raw.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FAILED;
                        resultData.code = BIDVUtil.PaymentCode__QRTimeOut;
                        string mess = MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoDaDuyetKhoaTaiChinh, param.LanguageCode);
                        resultData.message = string.Format(mess, treatmentFee.TREATMENT_CODE);
                        Inventec.Common.Logging.LogSystem.Info("Hồ sơ đã khóa viện phí: " + treatmentFee.TREATMENT_CODE);
                    }
                    else if (amount != raw.AMOUNT)
                    {
                        raw.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FAILED;
                        resultData.code = BIDVUtil.PaymentCode__IncorrectAmount;
                        resultData.data = new { amount = raw.AMOUNT };
                        resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransReq_SoTienKhongChinhXac, param.LanguageCode);
                        Inventec.Common.Logging.LogSystem.Error(string.Format("Yêu cầu với mã txnId = {0} có số tiền thanh toán không khớp. Bank_Amount: {1}, HIS_TRANS_REQ.AMOUNT: {2}", data.txnId, data.amount, raw.AMOUNT));
                    }
                    else
                    {
                        List<HIS_SESE_TRANS_REQ> hisSeseTransReq = null;
                        if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE || raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE_REQ)
                        {
                            hisSeseTransReq = new HisSeseTransReqGet().GetByTransReqId(raw.ID);
                        }
                        if (checker.ValidSeseTransReq(hisSeseTransReq))
                        {
                            List<HIS_SERE_SERV> sereServ = null;
                            HIS_DEPOSIT_REQ depositReq = null;
                            if (checker.ValidAmount(treatmentFee, raw, hisSeseTransReq, ref sereServ, ref depositReq))
                            {
                                ProcessTransaction(data, treatmentFee, raw, hisSeseTransReq, requestRoom);
                                ProcessSereServDeposit(raw, sereServ, hisSeseTransReq);
                                ProcessSereServBill(raw, sereServ);
                                ProcessDepositReq(raw, depositReq);

                                resultData.code = BIDVUtil.PaymentCode__Success;
                                resultData.data = new { txnId = data.txnId };
                                resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransReq_DatHangThanhCong, param.LanguageCode);
                                try
                                {
                                    raw.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FINISHED;
                                    raw.BANK_JSON_DATA = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                                    raw.BANK_MESSAGE = data.message;
                                }
                                catch (Exception ex)
                                {
                                    Inventec.Common.Logging.LogSystem.Error("Loi BANK_JSON_DATA");
                                    Inventec.Common.Logging.LogSystem.Error(ex);
                                }
                            }
                            else
                            {
                                raw.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FAILED;
                                resultData.code = BIDVUtil.PaymentCode__IncorrectAmount;
                                resultData.data = new { amount = raw.AMOUNT };
                                resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransReq_SoTienKhongChinhXac, param.LanguageCode);
                            }
                        }
                        else
                        {
                            raw.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FAILED;
                            resultData.code = BIDVUtil.PaymentCode__AlreadyPaid;
                            resultData.data = new { txnId = data.txnId };
                            resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransReq_DonHangDaDuocThanhToan, param.LanguageCode);
                        }
                    }

                    if (!hisTransReqUpdate.Update(raw))
                    {
                        throw new Exception("Cap nhat thong tin HisTransReq that bai.");
                    }

                    result = true;
                }
                else
                {
                    switch (resultData.code)
                    {
                        case BIDVUtil.PaymentCode__InvalidInput:
                            resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransReq_DuLieuDauVaoKhongDungDinhDang, param.LanguageCode);
                            break;
                        case BIDVUtil.PaymentCode__AuthenticationFailed:
                            resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransReq_SaiThongTinXacThuc, param.LanguageCode);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
                resultData.code = BIDVUtil.PaymentCode__PaymentExeption;
                this.Rollback();
            }
            finally
            {
                if (String.IsNullOrWhiteSpace(resultData.code))
                {
                    resultData.code = BIDVUtil.PaymentCode__TimeOut;
                }
                if (resultData.code == BIDVUtil.PaymentCode__PaymentExeption)
                {
                    resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.LoiHeThong, param.LanguageCode);
                }

                string secretKey = BIDVUtil.BIDVSecretKey;
                string[] checkList = { resultData.code, secretKey };
                string checkdata = String.Join("", checkList);
                string checksum = BIDVUtil.CreateMD5(checkdata);
                resultData.checksum = checksum;

                //kết quả thành công thì không có log nên bổ sung log dữ liệu vào ra
                if (result)
                {
                    Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData("__INPUT_DATA__", data));
                    Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData("__OUPUT_DATA__", resultData));
                }
            }
            return result;
        }

        private void ProcessSereServDeposit(HIS_TRANS_REQ raw, List<HIS_SERE_SERV> listSereServ_ForDeposit, List<HIS_SESE_TRANS_REQ> listSeseTransReq)
        {
            if (IsNotNullOrEmpty(this.recentTransactions) && IsNotNullOrEmpty(listSereServ_ForDeposit) && IsNotNullOrEmpty(listSeseTransReq))
            {
                List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = new List<HIS_SERE_SERV_DEPOSIT>();

                HIS_TRANSACTION tran = this.recentTransactions.FirstOrDefault(o => o.TRANS_REQ_ID == raw.ID);

                if (!IsNotNull(tran)) return;

                if (tran.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU)
                {
                    foreach (HIS_SESE_TRANS_REQ item in listSeseTransReq)
                    {
                        HIS_SERE_SERV sereServ = listSereServ_ForDeposit.First(o => o.ID == item.SERE_SERV_ID);
                        HIS_SERE_SERV_DEPOSIT ssDeposit = new HIS_SERE_SERV_DEPOSIT();
                        ssDeposit.DEPOSIT_ID = tran.ID;
                        ssDeposit.AMOUNT = item.PRICE;
                        ssDeposit.SERE_SERV_ID = item.SERE_SERV_ID;
                        ssDeposit.IS_ACTIVE = HisTransReqCFG.QRPAYMENT_STATUS_OPTION == HisTransReqCFG.QrStatusOption.IS_ACTIVE_FALSE ? IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE : IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        HisSereServDepositUtil.SetTdl(ssDeposit, sereServ);
                        sereServDeposits.Add(ssDeposit);
                    }
                }

                if (IsNotNullOrEmpty(sereServDeposits) && !this.hisSereServDepositCreate.CreateList(sereServDeposits))
                {
                    throw new Exception("hisSereServDepositCreate. Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessTransaction(TDO.PaymentBidvTDO data, V_HIS_TREATMENT_FEE_1 treatmentFee, HIS_TRANS_REQ raw, List<HIS_SESE_TRANS_REQ> listSeseTransReq, V_HIS_ROOM requestRoom)
        {
            List<HIS_TRANSACTION> creates = new List<HIS_TRANSACTION>();

            AutoMapper.Mapper.CreateMap<V_HIS_TREATMENT_FEE_1, HIS_TREATMENT>();
            HIS_TREATMENT treatment = AutoMapper.Mapper.Map<HIS_TREATMENT>(treatmentFee);

            HIS_TRANSACTION tran = new HIS_TRANSACTION();
            tran.BUYER_ACCOUNT_NUMBER = treatmentFee.TDL_PATIENT_ACCOUNT_NUMBER;
            tran.BUYER_ADDRESS = treatmentFee.TDL_PATIENT_ADDRESS;
            tran.BUYER_NAME = treatmentFee.TDL_PATIENT_NAME;
            tran.BUYER_TAX_CODE = treatmentFee.TDL_PATIENT_TAX_CODE;
            tran.BUYER_ORGANIZATION = treatmentFee.TDL_PATIENT_WORK_PLACE ?? treatmentFee.TDL_PATIENT_WORK_PLACE_NAME;
            tran.BUYER_WORK_PLACE_ID = treatmentFee.TDL_PATIENT_WORK_PLACE_ID;
            tran.PAY_FORM_ID = IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__QR;
            tran.TRANS_REQ_ID = raw.ID;
            tran.TRANSACTION_TIME = Inventec.Common.DateTime.Get.Now().Value;
            tran.TREATMENT_ID = raw.TREATMENT_ID;
            tran.IS_ACTIVE = HisTransReqCFG.QRPAYMENT_STATUS_OPTION == HisTransReqCFG.QrStatusOption.IS_ACTIVE_FALSE ? IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE : IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;

            tran.BANK_TRANSACTION_CODE = data.qrTrace;
            tran.BANK_TRANSACTION_TIME = Inventec.Common.TypeConvert.Parse.ToInt64(data.payDate);

            if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE || raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE_REQ)
            {
                tran.AMOUNT = raw.AMOUNT;
                tran.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU;
                tran.ACCOUNT_BOOK_ID = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.AccountBook.ID;
                tran.BILL_TYPE_ID = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.AccountBook.BILL_TYPE_ID;
                tran.CASHIER_LOGINNAME = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.Loginname;
                tran.CASHIER_USERNAME = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.Username;
                tran.CASHIER_ROOM_ID = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.CashierRoom.ID;

                tran.TDL_SERE_SERV_DEPOSIT_COUNT = listSeseTransReq != null ? listSeseTransReq.Count : 0;
            }
            else if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_DEPOSIT)
            {
                tran.AMOUNT = raw.AMOUNT;
                tran.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU;
                tran.ACCOUNT_BOOK_ID = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.AccountBook.ID;
                tran.BILL_TYPE_ID = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.AccountBook.BILL_TYPE_ID;
                tran.CASHIER_LOGINNAME = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.Loginname;
                tran.CASHIER_USERNAME = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.Username;
                tran.CASHIER_ROOM_ID = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.CashierRoom.ID;
            }
            else if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_TREATMENT)
            {
                decimal payAmount = (treatmentFee.TOTAL_DEPOSIT_AMOUNT ?? 0) - (treatmentFee.TOTAL_REPAY_AMOUNT ?? 0) - (treatmentFee.TOTAL_BILL_TRANSFER_AMOUNT ?? 0);

                tran.KC_AMOUNT = payAmount;
                tran.AMOUNT = raw.AMOUNT + payAmount;
                tran.SERE_SERV_AMOUNT = tran.AMOUNT;
                tran.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                tran.ACCOUNT_BOOK_ID = HisTransReqCFG.QRPAYMENT_BILL_INFO.AccountBook.ID;
                tran.BILL_TYPE_ID = HisTransReqCFG.QRPAYMENT_BILL_INFO.AccountBook.BILL_TYPE_ID;
                tran.CASHIER_LOGINNAME = HisTransReqCFG.QRPAYMENT_BILL_INFO.Loginname;
                tran.CASHIER_USERNAME = HisTransReqCFG.QRPAYMENT_BILL_INFO.Username;
                tran.CASHIER_ROOM_ID = HisTransReqCFG.QRPAYMENT_BILL_INFO.CashierRoom.ID;
            }
            if (HisTransReqCFG.QRPAYMENT_CASHIER_ROOM_OPTION == HisTransReqCFG.QrCashierRoomOption.BY_CASHIER_ROOM)
            {
                tran.CASHIER_ROOM_ID = requestRoom.DEFAULT_CASHIER_ROOM_ID.Value;
            }
            if (HisTransReqCFG.QRPAYMENT_STATUS_OPTION == HisTransReqCFG.QrStatusOption.IS_ACTIVE_FALSE)
            {
                tran.BEFORE_UL_CASHIER_ROOM_ID = tran.CASHIER_ROOM_ID;
                tran.BEFORE_UL_CASHIER_LOGINNAME = tran.CASHIER_LOGINNAME;
                tran.BEFORE_UL_CASHIER_USERNAME = tran.CASHIER_USERNAME;
            }

            creates.Add(tran);

            if (!this.hisTransactionCreate.CreateListNotSetCashier(creates, treatment))
            {
                throw new Exception("hisTransactionCreate. Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentTransactions = creates;
        }

        private void ProcessSereServBill(HIS_TRANS_REQ raw, List<HIS_SERE_SERV> sereServ)
        {
            if (IsNotNullOrEmpty(this.recentTransactions) && IsNotNullOrEmpty(sereServ))
            {
                List<HIS_SERE_SERV_BILL> sereServBills = new List<HIS_SERE_SERV_BILL>();

                HIS_TRANSACTION tran = this.recentTransactions.FirstOrDefault(o => o.TRANS_REQ_ID == raw.ID);

                if (!IsNotNull(tran)) return;

                if (tran.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
                {
                    foreach (HIS_SERE_SERV item in sereServ)
                    {
                        HIS_SERE_SERV_BILL ssBill = new HIS_SERE_SERV_BILL();
                        ssBill.BILL_ID = tran.ID;
                        ssBill.PRICE = item.VIR_TOTAL_PATIENT_PRICE ?? 0;
                        ssBill.SERE_SERV_ID = item.ID;
                        ssBill.TDL_BILL_TYPE_ID = tran.BILL_TYPE_ID;
                        ssBill.TDL_TREATMENT_ID = tran.TREATMENT_ID.Value;
                        HisSereServBillUtil.SetTdl(ssBill, item);
                        sereServBills.Add(ssBill);
                    }
                }

                if (IsNotNullOrEmpty(sereServBills) && !this.hisSereServBillCreate.CreateList(sereServBills))
                {
                    throw new Exception("hisSereServBillCreate. Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessDepositReq(HIS_TRANS_REQ raw, HIS_DEPOSIT_REQ depositReq)
        {
            if (IsNotNullOrEmpty(this.recentTransactions) && IsNotNull(depositReq))
            {
                HIS_TRANSACTION tran = this.recentTransactions.FirstOrDefault(o => o.TRANS_REQ_ID == raw.ID);
                if (!IsNotNull(tran)) return;

                if (tran.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU)
                {
                    depositReq.DEPOSIT_ID = tran.ID;

                    if (!this.hisDepositReqUpdate.Update(depositReq, false))
                    {
                        throw new Exception("hisDepositReqUpdate. Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisDepositReqUpdate.RollbackData();
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
