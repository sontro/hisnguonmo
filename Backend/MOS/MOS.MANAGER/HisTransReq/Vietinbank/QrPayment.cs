using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepositReq;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSeseTransReq;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.Vietinbank
{
    class QrPayment : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisTransReqUpdate hisTransReqUpdate;
        private HisSereServBillCreate hisSereServBillCreate;
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
            this.hisDepositReqUpdate = new HisDepositReqUpdate(param);
        }

        internal bool Run(TDO.PaymentVietinbankTDO data, ref TDO.PaymentVietinbankResultTDO resultData)
        {
            resultData = new TDO.PaymentVietinbankResultTDO();
            bool result = false;
            try
            {
                bool valid = true;

                QrPaymentCheck checker = new QrPaymentCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && data.statusCode == VietinbankUtil.PaymentCode__Success;
                valid = valid && checker.CheckConfig(data);
                valid = valid && checker.CheckCertificate();
                valid = valid && checker.CheckSum(data);
                if (valid)
                {
                    HIS_TRANS_REQ raw = null;
                    if (!checker.CheckOrderId(data.orderId, ref raw))
                    {
                        resultData.paymentStatus = VietinbankUtil.PaymentCode__NotFound;
                        Inventec.Common.Logging.LogSystem.Error("Không tìm được yêu cầu với mã orderId = " + data.orderId);
                        return result;
                    }
                    decimal amount = 0;

                    if (raw.TRANS_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FINISHED)
                    {
                        resultData.paymentStatus = VietinbankUtil.PaymentCode__HasPay;
                        Inventec.Common.Logging.LogSystem.Error(string.Format("Yêu cầu với mã orderId = {0} đã được thanh toán ", data.orderId));
                        return result;
                    }
                    else if (raw.TRANS_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__CANCEL || raw.TRANS_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FAILED)
                    {
                        resultData.paymentStatus = VietinbankUtil.PaymentCode__Invalid;
                        Inventec.Common.Logging.LogSystem.Error(string.Format("Yêu cầu với mã orderId = {0} đã bị hủy hoặc thanh toán lỗi ", data.orderId));
                        return result;
                    }
                    else if (!decimal.TryParse(data.amount, NumberStyles.Any, CultureInfo.CreateSpecificCulture("en-US"), out amount) || amount != raw.AMOUNT)
                    {
                        resultData.paymentStatus = VietinbankUtil.PaymentCode__InvalidAmount;
                        raw.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FAILED;

                        Inventec.Common.Logging.LogSystem.Error(string.Format("Yêu cầu với mã orderId = {0} có số tiền thanh toán không khớp. Bank_Amount: {1}, HIS_TRANS_REQ.AMOUNT: {2}", data.orderId, data.amount, raw.AMOUNT));
                    }
                    else
                    {
                        List<HIS_SESE_TRANS_REQ> hisSeseTransReq = null;
                        if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE || raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE_REQ)
                        {
                            hisSeseTransReq = new HisSeseTransReqGet().GetByTransReqId(raw.ID);
                        }

                        if (checker.CheckSeseTransReq(hisSeseTransReq))
                        {
                            V_HIS_TREATMENT_FEE_1 hisTreatmentFee = new HisTreatmentGet().GetFeeView1ById(raw.TREATMENT_ID);
                            List<HIS_SERE_SERV> sereServ = null;
                            HIS_DEPOSIT_REQ depositReq = null;
                            if (checker.CheckAmount(hisTreatmentFee, raw, hisSeseTransReq, ref sereServ, ref depositReq))
                            {
                                ProcessTransaction(data, hisTreatmentFee, raw);
                                ProcessSereServBill(raw, sereServ);
                                ProcessDepositReq(raw, depositReq);

                                resultData.paymentStatus = VietinbankUtil.PaymentCode__Success;
                                try
                                {
                                    raw.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FINISHED;
                                    raw.BANK_JSON_DATA = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                                    raw.BANK_MESSAGE = data.statusMessage;
                                }
                                catch (Exception ex)
                                {
                                    Inventec.Common.Logging.LogSystem.Error("Loi BANK_JSON_DATA");
                                    Inventec.Common.Logging.LogSystem.Error(ex);
                                }
                            }
                            else
                            {
                                resultData.paymentStatus = VietinbankUtil.PaymentCode__InvalidAmount;
                                raw.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FAILED;
                            }
                        }
                        else
                        {
                            resultData.paymentStatus = VietinbankUtil.PaymentCode__HasPay;
                            raw.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FAILED;
                            Inventec.Common.Logging.LogSystem.Error(string.Format("Yêu cầu với mã orderId = {0} đã được thanh toán ", data.orderId));
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
                    resultData.paymentStatus = VietinbankUtil.PaymentCode__Invalid;
                    resultData.requestId = data.requestId;
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
                resultData.paymentStatus = VietinbankUtil.PaymentCode__Exception;
                this.Rollback();
            }
            finally
            {
                if (String.IsNullOrWhiteSpace(resultData.paymentStatus))
                {
                    resultData.paymentStatus = VietinbankUtil.PaymentCode__TimeOut;
                }

                if (IsNotNull(data))
                {
                    resultData.requestId = data.requestId;
                }

                resultData.signature = ProcessSign(resultData.requestId + resultData.paymentStatus);

                //kết quả thành công thì không có log nên bổ sung log dữ liệu vào ra
                if (result)
                {
                    Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData("__INPUT_DATA__", data));
                    Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData("__OUPUT_DATA__", resultData));
                }
            }
            return result;
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

        private void ProcessTransaction(TDO.PaymentVietinbankTDO data, V_HIS_TREATMENT_FEE_1 treatmentFee, HIS_TRANS_REQ raw)
        {
            List<HIS_TRANSACTION> creates = new List<HIS_TRANSACTION>();

            AutoMapper.Mapper.CreateMap<V_HIS_TREATMENT_FEE_1, HIS_TREATMENT>();
            HIS_TREATMENT treatment = AutoMapper.Mapper.Map<HIS_TREATMENT>(treatmentFee);

            HIS_TRANSACTION tran = new HIS_TRANSACTION();

            tran.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
            tran.ACCOUNT_BOOK_ID = HisTransReqCFG.QRPAYMENT_BILL_INFO.AccountBook.ID;
            tran.BILL_TYPE_ID = HisTransReqCFG.QRPAYMENT_BILL_INFO.AccountBook.BILL_TYPE_ID;
            tran.CASHIER_LOGINNAME = HisTransReqCFG.QRPAYMENT_BILL_INFO.Loginname;
            tran.CASHIER_USERNAME = HisTransReqCFG.QRPAYMENT_BILL_INFO.Username;
            tran.CASHIER_ROOM_ID = HisTransReqCFG.QRPAYMENT_BILL_INFO.CashierRoom.ID;

            tran.PAY_FORM_ID = IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__QR;

            tran.BUYER_ACCOUNT_NUMBER = treatmentFee.TDL_PATIENT_ACCOUNT_NUMBER;
            tran.BUYER_ADDRESS = treatmentFee.TDL_PATIENT_ADDRESS;
            tran.BUYER_NAME = treatmentFee.TDL_PATIENT_NAME;
            tran.BUYER_TAX_CODE = treatmentFee.TDL_PATIENT_TAX_CODE;
            tran.BUYER_ORGANIZATION = treatmentFee.TDL_PATIENT_WORK_PLACE ?? treatmentFee.TDL_PATIENT_WORK_PLACE_NAME;
            tran.BUYER_WORK_PLACE_ID = treatmentFee.TDL_PATIENT_WORK_PLACE_ID;
            tran.TREATMENT_TYPE_ID = treatmentFee.TDL_TREATMENT_TYPE_ID;

            tran.TRANS_REQ_ID = raw.ID;
            tran.AMOUNT = raw.AMOUNT;
            tran.TRANSACTION_TIME = Inventec.Common.DateTime.Get.Now().Value;
            tran.TREATMENT_ID = raw.TREATMENT_ID;
            tran.SERE_SERV_AMOUNT = raw.AMOUNT;

            tran.BANK_TRANSACTION_CODE = data.requestId;
            tran.BANK_TRANSACTION_TIME = Inventec.Common.TypeConvert.Parse.ToInt64(data.transactionDate);

            if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE || raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE_REQ)
            {
                tran.IS_DIRECTLY_BILLING = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
            }
            else if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_DEPOSIT)
            {
                tran.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU;
                tran.ACCOUNT_BOOK_ID = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.AccountBook.ID;
                tran.BILL_TYPE_ID = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.AccountBook.BILL_TYPE_ID;
                tran.CASHIER_LOGINNAME = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.Loginname;
                tran.CASHIER_USERNAME = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.Username;
                tran.CASHIER_ROOM_ID = HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.CashierRoom.ID;
                tran.SERE_SERV_AMOUNT = null;
            }
            else if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_TREATMENT)
            {
                decimal payAmount = (treatmentFee.TOTAL_DEPOSIT_AMOUNT ?? 0) - (treatmentFee.TOTAL_REPAY_AMOUNT ?? 0) - (treatmentFee.TOTAL_BILL_TRANSFER_AMOUNT ?? 0);

                tran.KC_AMOUNT = payAmount;
                tran.AMOUNT = raw.AMOUNT + payAmount;
                tran.SERE_SERV_AMOUNT = raw.AMOUNT + payAmount;
            }

            creates.Add(tran);

            if (!this.hisTransactionCreate.CreateListNotSetCashier(creates, treatment))
            {
                throw new Exception("hisTransactionCreate. Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentTransactions = creates;
        }

        private string ProcessSign(string data)
        {
            string result = "";
            try
            {
                if (!String.IsNullOrWhiteSpace(VietinbankUtil.InventecFileCer) && System.IO.File.Exists(VietinbankUtil.InventecFileCer) && !String.IsNullOrWhiteSpace(VietinbankUtil.InventecPass) && !String.IsNullOrWhiteSpace(VietinbankUtil.VietinbankHashAlg))
                {
                    result = Crypto.Sign(data, VietinbankUtil.InventecFileCer, VietinbankUtil.InventecPass, VietinbankUtil.VietinbankHashAlg);
                }
            }
            catch (Exception ex)
            {
                result = "";
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                this.hisDepositReqUpdate.RollbackData();
                this.hisSereServBillCreate.RollbackData();
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
