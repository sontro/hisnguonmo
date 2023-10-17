using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class EpaymentCFG
    {
        private const string IS_USING_EXECUTE_ROOM_PAYMENT_CFG = "MOS.EPAYMENT.IS_USING_EXECUTE_ROOM_PAYMENT";
        private const string MUST_PAY_ALL_CFG = "MOS.EPAYMENT.MUST_PAY_ALL";
        public const string EXECUTE_ROOM_PAYMENT_OPTION_CFG = "MOS.EPAYMENT.EXECUTE_ROOM_PAYMENT_OPTION";
        private const string KIOSK_PAYMENT_OPTION_CFG = "MOS.EPAYMENT.KIOSK_PAYMENT_OPTION";
        private const string CASHIER_ROOM_PAYMENT_OPTION_CFG = "MOS.EPAYMENT.CASHIER_ROOM_PAYMENT_OPTION";

        public enum ExecuteRoomPaymentOption
        {
            OPTION1 = 1,
            OPTION2 = 2,
        }

        public enum KioskPaymentOption
        {
            //Tu dong thanh toan khi dang ky
            AUTO_PAY = 1,
            NONE = 0,
        }

        public enum CashierRoomPaymentOption
        {
            //Tu dong thanh toan khi dang ky
            AUTO_PAY = 2,
            //Thuc hien thanh toan thu cong bang hinh thuc quet the tren thiet bi thanh toan
            MANUAL_PAY = 1,
        }

        private static bool? isUsingExecuteRoomPayment;
        public static bool IS_USING_EXECUTE_ROOM_PAYMENT
        {
            get
            {
                if (!isUsingExecuteRoomPayment.HasValue)
                {
                    isUsingExecuteRoomPayment = ConfigUtil.GetIntConfig(IS_USING_EXECUTE_ROOM_PAYMENT_CFG) == 1;
                }

                return isUsingExecuteRoomPayment.Value;
            }
        }

        private static bool? mustPayAll;
        public static bool MUST_PAY_ALL
        {
            get
            {
                if (!mustPayAll.HasValue)
                {
                    mustPayAll = ConfigUtil.GetIntConfig(MUST_PAY_ALL_CFG) == 1;
                }

                return mustPayAll.Value;
            }
        }

        private static ExecuteRoomPaymentOption? executeRoomPaymentOption;
        public static ExecuteRoomPaymentOption EXECUTE_ROOM_PAYMENT_OPTION
        {
            get
            {
                if (!executeRoomPaymentOption.HasValue)
                {
                    executeRoomPaymentOption = (ExecuteRoomPaymentOption)ConfigUtil.GetIntConfig(EXECUTE_ROOM_PAYMENT_OPTION_CFG);
                }

                return executeRoomPaymentOption.Value;
            }
        }

        private static CashierRoomPaymentOption? cashierRoomPaymentOption;
        public static CashierRoomPaymentOption CASHIER_ROOM_PAYMENT_OPTION
        {
            get
            {
                if (!cashierRoomPaymentOption.HasValue)
                {
                    cashierRoomPaymentOption = (CashierRoomPaymentOption)ConfigUtil.GetIntConfig(CASHIER_ROOM_PAYMENT_OPTION_CFG);
                }

                return cashierRoomPaymentOption.Value;
            }
        }

        private static KioskPaymentOption? kioskPaymentOption;
        public static KioskPaymentOption KIOSK_PAYMENT_OPTION
        {
            get
            {
                if (!kioskPaymentOption.HasValue)
                {
                    kioskPaymentOption = (KioskPaymentOption)ConfigUtil.GetIntConfig(KIOSK_PAYMENT_OPTION_CFG);
                }

                return kioskPaymentOption.Value;
            }
        }

        public static void Reload()
        {
            isUsingExecuteRoomPayment = ConfigUtil.GetIntConfig(IS_USING_EXECUTE_ROOM_PAYMENT_CFG) == 1;
            mustPayAll = ConfigUtil.GetIntConfig(MUST_PAY_ALL_CFG) == 1;
            executeRoomPaymentOption = (ExecuteRoomPaymentOption)ConfigUtil.GetIntConfig(EXECUTE_ROOM_PAYMENT_OPTION_CFG);
            kioskPaymentOption = (KioskPaymentOption)ConfigUtil.GetIntConfig(KIOSK_PAYMENT_OPTION_CFG);
            cashierRoomPaymentOption = (CashierRoomPaymentOption)ConfigUtil.GetIntConfig(CASHIER_ROOM_PAYMENT_OPTION_CFG);
        }
    }
}
