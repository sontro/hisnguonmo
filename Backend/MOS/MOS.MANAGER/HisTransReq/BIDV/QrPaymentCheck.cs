using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepositReq;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseTransReq;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.BIDV
{
    class QrPaymentCheck : BusinessBase
    {
        internal QrPaymentCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool ValidHisTransReq(TDO.PaymentBidvTDO data, TDO.PaymentBidvResultTDO resultData, ref HIS_TRANS_REQ raw)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (String.IsNullOrEmpty(data.txnId)) throw new ArgumentNullException("data.txnId");
                raw = new HisTransReqGet().GetByCode(data.txnId);
                if (!IsNotNull(raw))
                {
                    resultData.code = BIDVUtil.PaymentCode__IncorrectAmount;
                    resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransReq_SoTienKhongChinhXac, param.LanguageCode);
                    Inventec.Common.Logging.LogSystem.Error("Không tồn tại thông tin yêu cầu thanh toán: " + data.txnId);
                    valid = false;
                }
            }
            catch (ArgumentNullException ex)
            {
                resultData.code = BIDVUtil.PaymentCode__InvalidInput;
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                resultData.code = BIDVUtil.PaymentCode__PaymentExeption;
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool ValidConfig(TDO.PaymentBidvTDO data, TDO.PaymentBidvResultTDO resultData, HIS_TRANS_REQ raw)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (BIDVUtil.BIDVSecretKey == null) throw new ArgumentNullException("Chua cau hinh key=\"MOS.MANAGER.BIDV.SECRET_KEY\"");
                if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_TREATMENT)
                {
                    if (HisTransReqCFG.QRPAYMENT_BILL_INFO == null) throw new ArgumentNullException("QRPAYMENT_BILL_INFO null");
                    if (HisTransReqCFG.QRPAYMENT_BILL_INFO.AccountBook == null) throw new ArgumentNullException("QRPAYMENT_BILL_INFO.AccountBook null");
                    if (HisTransReqCFG.QRPAYMENT_BILL_INFO.CashierRoom == null) throw new ArgumentNullException("QRPAYMENT_BILL_INFO.CashierRoom null");
                    if (HisTransReqCFG.QRPAYMENT_BILL_INFO.Loginname == null) throw new ArgumentNullException("QRPAYMENT_BILL_INFO.Loginname null");
                    var cashierBill = HisEmployeeCFG.DATA.Where(o => o.LOGINNAME == HisTransReqCFG.QRPAYMENT_BILL_INFO.Loginname);
                    if (cashierBill == null || cashierBill.Count() != 1) throw new ArgumentNullException("QRPAYMENT_BILL_INFO.Loginname is not valid");
                    if (HisTransReqCFG.QRPAYMENT_BILL_INFO.AccountBook.IS_NOT_GEN_TRANSACTION_ORDER == Constant.IS_TRUE) throw new ArgumentNullException(String.Format("So thu {0} khong tu dong tao so", HisTransReqCFG.QRPAYMENT_BILL_INFO.AccountBook.ACCOUNT_BOOK_CODE));
                }
                else if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE 
                    || raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE_REQ
                    || raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_DEPOSIT)
                {
                    if (HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO == null) throw new ArgumentNullException("QRPAYMENT_DEPOSIT_INFO null");
                    if (HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.AccountBook == null) throw new ArgumentNullException("QRPAYMENT_DEPOSIT_INFO.AccountBook null");
                    if (HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.CashierRoom == null) throw new ArgumentNullException("QRPAYMENT_DEPOSIT_INFO.CashierRoom null");
                    if (HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.Loginname == null) throw new ArgumentNullException("QRPAYMENT_DEPOSIT_INFO.Loginname null");
                    var cashierDeposit = HisEmployeeCFG.DATA.Where(o => o.LOGINNAME == HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.Loginname);
                    if (cashierDeposit == null || cashierDeposit.Count() != 1) throw new ArgumentNullException("QRPAYMENT_DEPOSIT_INFO.Loginname is not valid");
                    if (HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.AccountBook.IS_NOT_GEN_TRANSACTION_ORDER == Constant.IS_TRUE) throw new ArgumentNullException(String.Format("So thu {0} khong tu dong tao so", HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.AccountBook.ACCOUNT_BOOK_CODE));
                }
                if (HisTransReqCFG.CONFIG_BIDV_INFO == null) throw new ArgumentNullException("CONFIG_BIDV_INFO null");
            }
            catch (ArgumentNullException ex)
            {
                resultData.code = BIDVUtil.PaymentCode__PaymentExeption;
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                resultData.code = BIDVUtil.PaymentCode__PaymentExeption;
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool ValidInput(TDO.PaymentBidvTDO data, TDO.PaymentBidvResultTDO resultData, ref decimal amount)
        {
            bool valid = true;
            if (resultData == null)
                resultData = new TDO.PaymentBidvResultTDO();
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.code == null) throw new ArgumentNullException("data.code");
                if (data.code != BIDVUtil.PaymentCode__Success) throw new ArgumentNullException("data.code: " + data.code);
                if (String.IsNullOrEmpty(data.message)) throw new ArgumentNullException("data.message");
                if (String.IsNullOrEmpty(data.msgType)) throw new ArgumentNullException("data.msgType");
                if (String.IsNullOrEmpty(data.txnId)) throw new ArgumentNullException("data.txnId");
                if (String.IsNullOrEmpty(data.qrTrace)) throw new ArgumentNullException("data.qrTrace");
                if (String.IsNullOrEmpty(data.bankCode)) throw new ArgumentNullException("data.bankCode");
                if (String.IsNullOrEmpty(data.amount)) throw new ArgumentNullException("data.amount");
                if (String.IsNullOrEmpty(data.payDate)) throw new ArgumentNullException("data.payDate");
                if (String.IsNullOrEmpty(data.merchantCode)) throw new ArgumentNullException("data.merchantCode");
                if (String.IsNullOrEmpty(data.terminalId)) throw new ArgumentNullException("data.terminalId");
                if (String.IsNullOrEmpty(data.checksum)) throw new ArgumentNullException("data.checksum");

                if (data.terminalId != HisTransReqCFG.CONFIG_BIDV_INFO.TerminalLabel || data.merchantCode != HisTransReqCFG.CONFIG_BIDV_INFO.MerchantCode)
                    throw new ArgumentNullException("data.terminalId: " + data.terminalId + "; data.merchantCode: " + data.merchantCode);
                if (!decimal.TryParse(data.amount, NumberStyles.Any, CultureInfo.CreateSpecificCulture("en-US"), out amount)) throw new ArgumentNullException("Invalid Amount format (data.amount):" + data.amount);
            }
            catch (ArgumentNullException ex)
            {
                resultData.code = BIDVUtil.PaymentCode__InvalidInput;
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                resultData.code = BIDVUtil.PaymentCode__PaymentExeption;
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool CheckSum(TDO.PaymentBidvTDO data, TDO.PaymentBidvResultTDO resultData)
        {
            bool valid = true;
            try
            {
                string secretKey = BIDVUtil.BIDVSecretKey;
                string[] checkList = { data.code, data.msgType, data.txnId, data.qrTrace, data.bankCode, data.mobile, data.accountNo, data.amount, data.payDate, data.merchantCode, secretKey };
                string checkdata = String.Join("|",checkList);
                string checksum = BIDVUtil.CreateMD5(checkdata);
                valid = checksum == data.checksum;
                if (!valid)
                {
                    resultData.code = BIDVUtil.PaymentCode__AuthenticationFailed;
                    Inventec.Common.Logging.LogSystem.Error("Kiểm tra dữ liệu truyền vào so với dữ liệu kiểm tra không hợp lệ (data.checksum):" + data.checksum);
                }
            }
            catch (Exception ex)
            {
                resultData.code = BIDVUtil.PaymentCode__PaymentExeption;
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool ValidSeseTransReq(List<HIS_SESE_TRANS_REQ> hisSeseTransReq)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(hisSeseTransReq))
                {
                    List<long> sereServId = hisSeseTransReq.Select(s => s.SERE_SERV_ID).ToList();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * FROM HIS_SERE_SERV SESE WHERE");
                    sb.Append(" (EXISTS (SELECT 1 FROM HIS_SERE_SERV_BILL B WHERE SESE.ID = B.SERE_SERV_ID AND NVL(B.IS_CANCEL,0) = 0) ");
                    sb.Append(" OR EXISTS (SELECT 1 FROM HIS_SERE_SERV_DEPOSIT C WHERE SESE.ID = C.SERE_SERV_ID AND NVL(C.IS_CANCEL,0) = 0 ");
                    sb.Append(" AND NOT EXISTS (SELECT 1 FROM HIS_SESE_DEPO_REPAY D WHERE D.SERE_SERV_DEPOSIT_ID = C.ID AND NVL(D.IS_CANCEL,0) = 0))) ");
                    sb.Append(" AND %IN_CLAUSE% ");

                    string sql = DAOWorker.SqlDAO.AddInClause(sereServId, sb.ToString(), "SESE.ID");

                    List<HIS_SERE_SERV> ss = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(sql);
                    if (IsNotNullOrEmpty(ss))
                    {
                        List<long> ssId = ss.Select(s => s.ID).ToList();
                        Inventec.Common.Logging.LogSystem.Info("Tồn tại dịch vụ đã được thanh toán/tạm ứng. HIS_SERE_SERV.ID: " + string.Join(", ", ssId));
                        valid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool ValidAmount(V_HIS_TREATMENT_FEE_1 treatmentFee, HIS_TRANS_REQ raw, List<HIS_SESE_TRANS_REQ> hisSeseTransReq, ref List<HIS_SERE_SERV> sereServ, ref HIS_DEPOSIT_REQ depositReq)
        {
            bool valid = true;
            try
            {
                if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE_REQ || raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE)
                {
                    if (!IsNotNullOrEmpty(hisSeseTransReq))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Không có chi tiết giao dịch");
                        return false;
                    }

                    List<long> sereServId = hisSeseTransReq.Select(s => s.SERE_SERV_ID).ToList();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * FROM HIS_SERE_SERV SESE WHERE SESE.TDL_TREATMENT_ID = :param1");
                    sb.Append(" AND NOT EXISTS (SELECT 1 FROM HIS_SERE_SERV_BILL B WHERE SESE.ID = B.SERE_SERV_ID AND NVL(B.IS_CANCEL,0) = 0) ");
                    sb.Append(" AND NOT EXISTS (SELECT 1 FROM HIS_SERE_SERV_DEPOSIT C WHERE SESE.ID = C.SERE_SERV_ID AND NVL(C.IS_CANCEL,0) = 0 ");
                    sb.Append(" AND NOT EXISTS (SELECT 1 FROM HIS_SESE_DEPO_REPAY D WHERE D.SERE_SERV_DEPOSIT_ID = C.ID AND NVL(D.IS_CANCEL,0) = 0)) ");
                    sb.Append(" AND NOT EXISTS (SELECT 1 FROM HIS_SERE_SERV_DEBT E WHERE SESE.ID = E.SERE_SERV_ID AND NVL(E.IS_CANCEL,0) = 0) ");
                    sb.Append(" AND SESE.VIR_TOTAL_PATIENT_PRICE > 0 ");
                    sb.Append(" AND SESE.SERVICE_REQ_ID IS NOT NULL ");
                    sb.Append(" AND NVL(SESE.IS_DELETE,0) = 0 ");
                    sb.Append(" AND %IN_CLAUSE% ");

                    string sql = DAOWorker.SqlDAO.AddInClause(sereServId, sb.ToString(), "SESE.ID");

                    sereServ = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(sql, treatmentFee.ID);

                    if (!IsNotNullOrEmpty(sereServ) || sereServ.Count != hisSeseTransReq.Count)
                    {
                        Inventec.Common.Logging.LogSystem.Error("Có sai khác giữa chi tiết yêu cầu thanh toán và chi tiết dịch vụ. HIS_TRANS_REQ.ID: " + raw.ID);
                        return false;
                    }

                    foreach (var sst in hisSeseTransReq)
                    {
                        HIS_SERE_SERV ss = sereServ.FirstOrDefault(o => o.ID == sst.SERE_SERV_ID);
                        if (!IsNotNull(ss) || Math.Abs(sst.PRICE - (ss.VIR_TOTAL_PATIENT_PRICE ?? 0)) > BIDVUtil.PRICE_DIFFERENCE)
                        {
                            Inventec.Common.Logging.LogSystem.Error(string.Format("Có sai khác giữa chi tiết giao dịch và dịch vụ. HIS_SESE_TRANS_REQ.ID:{0}, PRICE:{1}, VIR_TOTAL_PATIENT_PRICE:{2}", sst.ID, sst.PRICE, ss.VIR_TOTAL_PATIENT_PRICE));
                            valid = false;
                            break;
                        }
                    }
                }
                else if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_DEPOSIT)
                {
                    List<HIS_DEPOSIT_REQ> depositReqs = new HisDepositReqGet().Get(new HisDepositReqFilterQuery() { TRANS_REQ_ID = raw.ID });
                    if (!IsNotNullOrEmpty(depositReqs) || depositReqs.Count != 1)
                    {
                        Inventec.Common.Logging.LogSystem.Error("Không tim được yêu cầu tạm ứng hoặc có nhiều hơn 1 yêu cầu tạm ứng theo HIS_TRANS_REQ.TRANS_REQ_CODE: " + raw.TRANS_REQ_CODE);
                        return false;
                    }
                    else
                    {
                        depositReq = depositReqs.First();
                        if (!IsNotNull(depositReq) || depositReq.DEPOSIT_ID.HasValue || Math.Abs(depositReq.AMOUNT - raw.AMOUNT) > BIDVUtil.PRICE_DIFFERENCE)
                        {
                            Inventec.Common.Logging.LogSystem.Error(string.Format("Yêu cầu tạm ứng đã đóng tiền hoặc sai số tiền tạm ứng. HIS_TRANS_REQ.ID: {0}, DEPOSIT_AMOUNT: {1},REQ_AMOUNT: {2} ", raw.ID, depositReq.AMOUNT, raw.AMOUNT));
                            valid = false;
                        }
                    }
                }
                else if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_TREATMENT)
                {
                    decimal? unpaid = new HisTreatmentGet().GetUnpaid(treatmentFee);
                    if (!unpaid.HasValue)
                    {
                        Inventec.Common.Logging.LogSystem.Warn("Loi du lieu");
                        return false;
                    }

                    if (Math.Abs(unpaid.Value - raw.AMOUNT) > BIDVUtil.PRICE_DIFFERENCE)
                    {
                        Inventec.Common.Logging.LogSystem.Info(string.Format("Số tiền còn lại không khớp với yêu cầu thanh toán. unpaid:{0}, HIS_TRANS_REQ.AMOUNT ", unpaid, raw.AMOUNT));
                        return false;
                    }
                    //thanh toán ra viện nên lấy tất cả dịch vụ chưa thanh toán.
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * FROM HIS_SERE_SERV SESE WHERE SESE.TDL_TREATMENT_ID = :param1");
                    sb.Append(" AND NOT EXISTS (SELECT 1 FROM HIS_SERE_SERV_BILL B WHERE SESE.ID = B.SERE_SERV_ID AND NVL(B.IS_CANCEL,0) = 0) ");
                    sb.Append(" AND SESE.SERVICE_REQ_ID IS NOT NULL ");
                    sb.Append(" AND NVL(SESE.IS_DELETE,0) = 0 "); ;

                    sereServ = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(sb.ToString(), treatmentFee.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool VerifyTreatmentFee(long treatmentId, ref V_HIS_TREATMENT_FEE_1 treatmentFee)
        {
            bool valid = true;
            try
            {
                treatmentFee = new HisTreatmentGet().GetFeeView1ById(treatmentId);
                if (treatmentFee == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("TreatmentId invalid: " + treatmentId);
                }

                if (treatmentFee.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoDaDuyetKhoaTaiChinh);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
