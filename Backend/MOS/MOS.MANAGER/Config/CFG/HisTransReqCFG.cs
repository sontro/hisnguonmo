using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAccountBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class HisTransReqCFG
    {
        /// <summary>
        /// - 1: Lấy thông tin phòng thu ngân theo cấu hình hệ thống (MOS.HIS_TRANSACTION.QR_PAYMENT.BILL_INFO, MOS.HIS_TRANSACTION.QR_PAYMENT.DEPOSIT_INFO)
        /// - 2: Lấy thông tin phòng thu ngân dựa theo phòng thu ngân được thiết lập ở phòng tạo yêu cầu thanh toán
        /// </summary>
        public enum QrCashierRoomOption
        {
            BY_CONFIG_KEY = 1,
            BY_CASHIER_ROOM = 2,
        }
        /// <summary>
        /// Trạng thái giao dịch thanh toán Qr: 
        /// - 0: Khóa giao dịch (IS_ACTIVE = 0). Bệnh nhân sẽ không thực hiện được CLS
        /// - 1: Không khóa giao dịch (IS_ACTIVE = 1).
        /// </summary>
        public enum QrStatusOption
        {
            IS_ACTIVE_FALSE = 0,
            IS_ACTIVE_TRUE = 1,
        }
        public class ConfigVietinbank
        {
            public string payLoad { get; set; }
            public string pointOTMethod { get; set; }
            public string masterMerchant { get; set; }
            public string merchantCode { get; set; }
            public string merchantCC { get; set; }
            public string merchantName { get; set; }
            public string merchantCity { get; set; }
            public string ccy { get; set; }
            public string CounttryCode { get; set; }
            public string terminalId { get; set; }
            public string storeID { get; set; }
            public string expDate { get; set; }
        }

        public class QrPayInfo
        {
            public V_HIS_CASHIER_ROOM CashierRoom { get; set; }
            public HIS_ACCOUNT_BOOK AccountBook { get; set; }
            public string Loginname { get; set; }
            public string Username { get; set; }
        }

        public class ConfigBIDVInfo
        {
            public string TerminalLabel { get; set; }
            public string MerchantCode { get; set; }
        }

        private const string HIS_TRANS_REQ__HASH_KEY = "MOS.HIS_TRANS_REQ.HASH_KEY";
        private const string AUTO_CREATE_OPTION_CFG = "MOS.HIS_TRAN_REQ.AUTO_CREATE.OPTION";
        private const string AUTO_ROUND_AMOUNT_OPTION_CFG = "MOS.HIS_TRANS_REQ.AUTO_ROUND_AMOUNT_OPTION";
        private const string PAYMENT_QRCODE_VIETINBANK_INFO_CFG = "HIS.Desktop.Plugins.PaymentQrCode.VietinbankInfo";
        private const string QRPAYMENT_CASHIER_ROOM_OPTION_CFG = "MOS.HIS_TRANSACTION.QR_PAYMENT.CASHIER_ROOM_OPTION";
        private const string QRPAYMENT_BILL_INFO_CFG = "MOS.HIS_TRANSACTION.QR_PAYMENT.BILL_INFO";
        private const string QRPAYMENT_DEPOSIT_INFO_CFG = "MOS.HIS_TRANSACTION.QR_PAYMENT.DEPOSIT_INFO";
        private const string QRPAYMENT_STATUS_OPTION_CFG = "MOS.HIS_TRANSACTION.QR_PAYMENT.STATUS_OPTION";

        private const string PAYMENT_QRCODE_BIDV_INFO_CFG = "HIS.Desktop.Plugins.PaymentQrCode.BIDVInfo";

        internal const string BIDV__SUCCESS_CODE = "00";
        internal const string PROVIDER__BIDV = "BIDV";

        private static string hashKey;
        public static string HASH_KEY
        {
            get
            {
                if (String.IsNullOrWhiteSpace(hashKey))
                {
                    hashKey = ConfigUtil.GetStrConfig(HIS_TRANS_REQ__HASH_KEY);
                }
                return hashKey;
            }
        }

        private static QrStatusOption? qrPaymentStatusOption;
        public static QrStatusOption? QRPAYMENT_STATUS_OPTION
        {
            get
            {
                if (qrPaymentStatusOption == null)
                {
                    qrPaymentStatusOption = GetQrStatusOption();
                }
                return qrPaymentStatusOption;
            }
        }

        private static QrStatusOption? GetQrStatusOption()
        {
            try
            {
                var result = (QrStatusOption)ConfigUtil.GetIntConfig(QRPAYMENT_STATUS_OPTION_CFG);
                return result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }

        private static QrCashierRoomOption? qrPaymentCashierRoomOption;
        public static QrCashierRoomOption? QRPAYMENT_CASHIER_ROOM_OPTION
        {
            get
            {
                if (qrPaymentCashierRoomOption == null)
                {
                    qrPaymentCashierRoomOption = GetQrCashierRoomOption();
                }
                return qrPaymentCashierRoomOption;
            }
        }

        private static QrCashierRoomOption? GetQrCashierRoomOption()
        {
            try
            {
                var result = (QrCashierRoomOption)ConfigUtil.GetIntConfig(QRPAYMENT_CASHIER_ROOM_OPTION_CFG);
                return result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }

        private static ConfigVietinbank configVietinbankData;
        public static ConfigVietinbank CONFIG_VIETINBANK_DATA
        {
            get
            {
                if (configVietinbankData == null)
                {
                    configVietinbankData = GetConfigVietinbank();
                }
                return configVietinbankData;
            }
        }

        private static ConfigVietinbank GetConfigVietinbank()
        {
            try
            {
                string ConfigVietinbankValue = ConfigUtil.GetStrConfig(PAYMENT_QRCODE_VIETINBANK_INFO_CFG);
                if (!String.IsNullOrWhiteSpace(ConfigVietinbankValue))
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigVietinbank>(ConfigVietinbankValue);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return null;
        }

        private static QrPayInfo qrPaymentBillInfo;
        public static QrPayInfo QRPAYMENT_BILL_INFO
        {
            get
            {
                if (qrPaymentBillInfo == null)
                {
                    qrPaymentBillInfo = GetQrPayInfoInfo(ConfigUtil.GetStrConfig(QRPAYMENT_BILL_INFO_CFG));
                }
                return qrPaymentBillInfo;
            }
        }

        private static QrPayInfo qrPaymentDepositInfo;
        public static QrPayInfo QRPAYMENT_DEPOSIT_INFO
        {
            get
            {
                if (qrPaymentDepositInfo == null)
                {
                    qrPaymentDepositInfo = GetQrPayInfoInfo(ConfigUtil.GetStrConfig(QRPAYMENT_DEPOSIT_INFO_CFG));
                }
                return qrPaymentDepositInfo;
            }
        }

        private static QrPayInfo GetQrPayInfoInfo(string configInfo)
        {
            QrPayInfo result = new QrPayInfo();
            try
            {
                if (!String.IsNullOrWhiteSpace(configInfo))
                {
                    string[] cfArrr = configInfo.Split('|');
                    if (cfArrr.Length < 3)
                    {
                        return result;
                    }

                    result.CashierRoom = HisCashierRoomCFG.DATA.FirstOrDefault(o => o.CASHIER_ROOM_CODE == cfArrr[0].Trim());

                    string[] cashier = cfArrr[1].Split('-');
                    if (cashier.Length > 1)
                    {
                        result.Loginname = cashier[0].Trim();
                        result.Username = cashier[1].Trim();
                    }

                    if (!String.IsNullOrWhiteSpace(cfArrr[2]))
                    {
                        HisAccountBookFilterQuery filter = new HisAccountBookFilterQuery();
                        filter.ACCOUNT_BOOK_CODE__EXACT = cfArrr[2];
                        List<HIS_ACCOUNT_BOOK> accountBooks = new HisAccountBookGet().Get(filter);
                        if (accountBooks != null && accountBooks.Count > 0)
                        {
                            result.AccountBook = accountBooks.First();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private static ConfigBIDVInfo configBIDVInfo;
        public static ConfigBIDVInfo CONFIG_BIDV_INFO
        {
            get
            {
                if (configBIDVInfo == null)
                {
                    configBIDVInfo = GetConfigBIDVInfo();
                }
                return configBIDVInfo;
            }
        }

        private static ConfigBIDVInfo GetConfigBIDVInfo()
        {
            try
            {
                string ConfigBIDVValue = ConfigUtil.GetStrConfig(PAYMENT_QRCODE_BIDV_INFO_CFG);
                if (!String.IsNullOrWhiteSpace(ConfigBIDVValue))
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigBIDVInfo>(ConfigBIDVValue);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return null;
        }

        private static bool? autoCreateOption;
        public static bool AUTO_CREATE_OPTION
        {
            get
            {
                if (!autoCreateOption.HasValue)
                {
                    autoCreateOption = ConfigUtil.GetIntConfig(AUTO_CREATE_OPTION_CFG) == 1;
                }
                return autoCreateOption.Value;
            }
        }

        private static int? autoRoundAmountOption;
        /// <summary>
        /// - 1: Trong trường hợp phần thập phân lớn hơn hoặc bằng 0.5 thì làm tròn lên, nếu nhỏ hơn 0.5 thì làm tròn xuống
        /// - Khác 1: Luôn làm tròn lên số nguyên nhỏ nhất mà lớn hơn hoặc bằng giá trị được làm tròn.
        /// </summary>
        public static int AUTO_ROUND_AMOUNT_OPTION
        {
            get
            {
                if (!autoRoundAmountOption.HasValue)
                {
                    autoRoundAmountOption = ConfigUtil.GetIntConfig(AUTO_ROUND_AMOUNT_OPTION_CFG);
                }
                return autoRoundAmountOption.Value;
            }
        }

        public static void Reload()
        {
            hashKey = ConfigUtil.GetStrConfig(HIS_TRANS_REQ__HASH_KEY);
            qrPaymentStatusOption = GetQrStatusOption();
            autoCreateOption = ConfigUtil.GetIntConfig(AUTO_CREATE_OPTION_CFG) == 1;
            autoRoundAmountOption = ConfigUtil.GetIntConfig(AUTO_ROUND_AMOUNT_OPTION_CFG);
            configVietinbankData = GetConfigVietinbank();
            qrPaymentBillInfo = GetQrPayInfoInfo(ConfigUtil.GetStrConfig(QRPAYMENT_BILL_INFO_CFG));
            qrPaymentDepositInfo = GetQrPayInfoInfo(ConfigUtil.GetStrConfig(QRPAYMENT_DEPOSIT_INFO_CFG));
            qrPaymentCashierRoomOption = GetQrCashierRoomOption();
            configBIDVInfo = GetConfigBIDVInfo();
        }
    }
}
