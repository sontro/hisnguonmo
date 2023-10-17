using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepositReq;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSeseTransReq;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.Vietinbank
{
    class QrPaymentCheck : BusinessBase
    {
        internal const decimal PRICE_DIFFERENCE = 1m;
        internal QrPaymentCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool CheckConfig(TDO.PaymentVietinbankTDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (HisTransReqCFG.CONFIG_VIETINBANK_DATA == null) throw new ArgumentNullException("CONFIG_VIETINBANK_DATA");
                if (String.IsNullOrWhiteSpace(data.terminalId)) throw new ArgumentNullException("data.terminalId");
                if (String.IsNullOrWhiteSpace(data.merchantName)) throw new ArgumentNullException("data.merchantName");
                if (String.IsNullOrWhiteSpace(data.merchantId)) throw new ArgumentNullException("data.merchantId");
                if (data.terminalId != HisTransReqCFG.CONFIG_VIETINBANK_DATA.terminalId) throw new Exception("terminalId invalid");
                if (data.merchantName != HisTransReqCFG.CONFIG_VIETINBANK_DATA.merchantName) throw new Exception("merchantName invalid");
                if (data.merchantId != HisTransReqCFG.CONFIG_VIETINBANK_DATA.merchantCode) throw new Exception("merchantId invalid");
                if (HisTransReqCFG.QRPAYMENT_BILL_INFO == null) throw new ArgumentNullException("QRCODE_BILL_INFO null");
                if (HisTransReqCFG.QRPAYMENT_BILL_INFO.AccountBook == null) throw new ArgumentNullException("QRCODE_BILL_INFO.AccountBook null");
                if (HisTransReqCFG.QRPAYMENT_BILL_INFO.CashierRoom == null) throw new ArgumentNullException("QRCODE_BILL_INFO.CashierRoom null");
                if (HisTransReqCFG.QRPAYMENT_BILL_INFO.Loginname == null) throw new ArgumentNullException("QRCODE_BILL_INFO.Loginname null");
                if (HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO == null) throw new ArgumentNullException("QRCODE_DEPOSIT_INFO null");
                if (HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.AccountBook == null) throw new ArgumentNullException("QRCODE_DEPOSIT_INFO.AccountBook null");
                if (HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.CashierRoom == null) throw new ArgumentNullException("QRCODE_DEPOSIT_INFO.CashierRoom null");
                if (HisTransReqCFG.QRPAYMENT_DEPOSIT_INFO.Loginname == null) throw new ArgumentNullException("QRCODE_DEPOSIT_INFO.Loginname null");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool CheckSum(TDO.PaymentVietinbankTDO data)
        {
            bool valid = true;
            try
            {
                if (VietinbankUtil.CheckSignature == "1")
                {
                    return valid;
                }

                if (!String.IsNullOrWhiteSpace(VietinbankUtil.VietinbankFileCer) && File.Exists(VietinbankUtil.VietinbankFileCer) && !String.IsNullOrWhiteSpace(VietinbankUtil.VietinbankHashAlg) && !String.IsNullOrWhiteSpace(data.signature))
                {
                    string checkdata = data.requestId + data.merchantId + data.orderId + data.productId;

                    valid = Crypto.Verify(data.signature, checkdata, VietinbankUtil.VietinbankFileCer, VietinbankUtil.VietinbankHashAlg);

                    if (!valid)
                    {
                        Inventec.Common.Logging.LogSystem.Error("Kiểm tra dữ liệu truyền vào so với dữ liệu ký không hợp lệ");
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

        internal bool CheckOrderId(string orderId, ref HIS_TRANS_REQ raw)
        {
            bool valid = true;
            try
            {
                raw = new HisTransReqGet().GetByCode(orderId);
                valid = valid && IsNotNull(raw);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool CheckSeseTransReq(List<HIS_SESE_TRANS_REQ> hisSeseTransReq)
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
                        Inventec.Common.Logging.LogSystem.Error("Tồn tại dịch vụ đã được thanh toán. HIS_SERE_SERV.ID: " + string.Join(", ", ssId));
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

        internal bool CheckAmount(V_HIS_TREATMENT_FEE_1 treatmentFee, HIS_TRANS_REQ raw, List<HIS_SESE_TRANS_REQ> hisSeseTransReq, ref List<HIS_SERE_SERV> sereServ, ref HIS_DEPOSIT_REQ depositReq)
        {
            bool valid = true;
            try
            {
                if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE || raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE_REQ)
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
                        if (!IsNotNull(ss) || Math.Abs(sst.PRICE - (ss.VIR_TOTAL_PATIENT_PRICE ?? 0)) > PRICE_DIFFERENCE)
                        {
                            Inventec.Common.Logging.LogSystem.Error(string.Format("Có sai khác giữa chi tiết giao dịch và dịch vụ. HIS_SESE_TRANS_REQ.ID:{0}, PRICE:{1}, VIR_TOTAL_PATIENT_PRICE:{2}", sst.ID, sst.PRICE, ss.VIR_TOTAL_PATIENT_PRICE));
                            valid = false;
                            break;
                        }
                    }
                }
                else if (raw.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_DEPOSIT)
                {
                    HisDepositReqFilterQuery filter = new HisDepositReqFilterQuery();
                    filter.TRANS_REQ_ID = raw.ID;
                    List<HIS_DEPOSIT_REQ> depositReqs = new HisDepositReqGet().Get(filter);
                    if (!IsNotNullOrEmpty(depositReqs) || depositReqs.Count != 1)
                    {
                        Inventec.Common.Logging.LogSystem.Error("Không tim được yêu cầu tạm ứng hoặc có nhiều hơn 1 yêu cầu tạm ứng theo HIS_TRANS_REQ.ID: " + raw.ID);
                        return false;
                    }

                    depositReq = depositReqs.First();
                    if (!IsNotNull(depositReq) || depositReq.DEPOSIT_ID.HasValue || Math.Abs(depositReq.AMOUNT - raw.AMOUNT) > PRICE_DIFFERENCE)
                    {
                        Inventec.Common.Logging.LogSystem.Error(string.Format("Yêu cầu tạm ứng đã đóng tiền hoặc sai số tiền tạm ứng. HIS_TRANS_REQ.ID: {0}, DEPOSIT_AMOUNT: {1},REQ_AMOUNT: {2} ", raw.ID, depositReq.AMOUNT, raw.AMOUNT));
                        valid = false;
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

                    if (Math.Abs(unpaid.Value - raw.AMOUNT) > PRICE_DIFFERENCE)
                    {
                        Inventec.Common.Logging.LogSystem.Warn(string.Format("Số tiền còn lại không khớp với yêu cầu thanh toán. unpaid:{0}, HIS_TRANS_REQ.AMOUNT ", unpaid, raw.AMOUNT));
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
                else
                {
                    Inventec.Common.Logging.LogSystem.Warn(string.Format("Sai thông tin loại yêu cầu thanh toán TRANS_REQ_TYPE:{0}", raw.TRANS_REQ_TYPE));
                    valid = false;
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

        internal bool CheckCertificate()
        {
            bool valid = true;
            try
            {
                if (VietinbankUtil.CheckSignature == "1")
                {
                    return valid;
                }

                if (String.IsNullOrWhiteSpace(VietinbankUtil.VietinbankFileCer)) throw new ArgumentNullException("MOS.MANAGER.Vietinbank.CertificatePath null");
                if (String.IsNullOrWhiteSpace(VietinbankUtil.VietinbankHashAlg)) throw new ArgumentNullException("MOS.MANAGER.Vietinbank.HashAlg null");
                if (String.IsNullOrWhiteSpace(VietinbankUtil.InventecFileCer)) throw new ArgumentNullException("MOS.MANAGER.Inventec.CertificatePath null");
                if (String.IsNullOrWhiteSpace(VietinbankUtil.InventecPass)) throw new ArgumentNullException("MOS.MANAGER.Inventec.CertificatePass null");
                if (!File.Exists(VietinbankUtil.VietinbankFileCer)) throw new Exception("MOS.MANAGER.Vietinbank.CertificatePath not exists");
                if (!File.Exists(VietinbankUtil.InventecFileCer)) throw new Exception("MOS.MANAGER.Inventec.CertificatePath not exists");

                X509Certificate2 certivt = new X509Certificate2(VietinbankUtil.InventecFileCer, VietinbankUtil.InventecPass);
                if (certivt == null) throw new Exception("MOS.MANAGER.Inventec.CertificatePath invalid");

                X509Certificate2 certvtb = new X509Certificate2(VietinbankUtil.VietinbankFileCer);
                if (certvtb == null) throw new Exception("MOS.MANAGER.Vietinbank.CertificatePath invalid");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }
    }
}
