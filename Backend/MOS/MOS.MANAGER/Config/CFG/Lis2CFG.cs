using Inventec.Common.Logging;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    /// <summary>
    /// Luu cac cau hinh de tich hop voi Lis
    /// </summary>
    class Lis2CFG
    {
        /// <summary>
        /// Chon cac he thong LIS tich hop
        /// </summary>
        public enum LisIntegrationType
        {
            /// <summary>
            /// He thong LIS do Inventec phat trien
            /// </summary>
            INVENTEC = 1,
            /// <summary>
            /// He thong roche
            /// </summary>
            ROCHE = 2,
            /// <summary>
            /// Labconn
            /// </summary>
            LABCONN = 3
        }

        /// <summary>
        /// Chon phien ban xu ly ket noi
        /// (De tranh rui ro khi len version moi, tam thoi se cho phep nguoi dung chon version de chay)
        /// </summary>
        public enum LisIntegrationVersion
        {
            /// <summary>
            /// Version 1
            /// </summary>
            V1 = 1,
            /// <summary>
            /// Version 2
            /// </summary>
            V2 = 2
        }

        /// <summary>
        /// Da gui sang LIS thanh cong
        /// </summary>
        public const long LIS_STT_ID_SEND = (long)1;

        private const string LIS_INTEGRATION_TYPE_CFG = "MOS.LIS.INTEGRATION_TYPE";
        private const string LIS_FORBID_NOT_ENOUGH_FEE_CFG = "MOS.LIS.FORBID_NOT_ENOUGH_FEE";
        private const string LIS_INTEGRATION_VERSION_CFG = "MOS.LIS.INTEGRATION_VERSION";
        private const string LIS_CALL_GENERATE_BARCODE_CFG = "MOS.LIS.IS_CALL_GENERATE_BARCODE";
        private const string LIS_LABCONN_IS_SEND_REQUEST_CFG = "MOS.LIS.LABCONN.IS_SEND_REQUEST";

        private static bool? lisForbidNotEnoughFee;
        public static bool LIS_FORBID_NOT_ENOUGH_FEE
        {
            get
            {
                if (lisForbidNotEnoughFee == null)
                {
                    lisForbidNotEnoughFee = ConfigUtil.GetIntConfig(LIS_FORBID_NOT_ENOUGH_FEE_CFG) == 1;
                }
                return lisForbidNotEnoughFee.Value;
            }
        }

        private static LisIntegrationType lisIntegrationType;
        public static LisIntegrationType LIS_INTEGRATION_TYPE
        {
            get
            {
                if (lisIntegrationType == 0)
                {
                    lisIntegrationType = (LisIntegrationType)ConfigUtil.GetIntConfig(LIS_INTEGRATION_TYPE_CFG);
                }
                return lisIntegrationType;
            }
        }

        private static LisIntegrationVersion lisIntegrationVersion;
        public static LisIntegrationVersion LIS_INTEGRATION_VERSION
        {
            get
            {
                if (lisIntegrationVersion == 0)
                {
                    lisIntegrationVersion = (LisIntegrationVersion)ConfigUtil.GetIntConfig(LIS_INTEGRATION_VERSION_CFG);
                }
                return lisIntegrationVersion;
            }
        }

        private static bool? isCallGenerateBarcode;
        public static bool IS_CALL_GENERATE_BARCODE
        {
            get
            {
                if (isCallGenerateBarcode == null)
                {
                    isCallGenerateBarcode = ConfigUtil.GetIntConfig(LIS_CALL_GENERATE_BARCODE_CFG) == 1;
                }
                return isCallGenerateBarcode.Value;
            }
        }

        private static bool? isSendRequestLabconn;
        public static bool IS_SEND_REQUEST_LABCONN
        {
            get
            {
                if (isSendRequestLabconn == null)
                {
                    isSendRequestLabconn = ConfigUtil.GetIntConfig(LIS_LABCONN_IS_SEND_REQUEST_CFG) == 1;
                }
                return isSendRequestLabconn.Value;
            }
        }

        public static void Reload()
        {
            var integrateType = (LisIntegrationType)ConfigUtil.GetIntConfig(LIS_INTEGRATION_TYPE_CFG);
            var forbidNotEnoughFee = ConfigUtil.GetIntConfig(LIS_FORBID_NOT_ENOUGH_FEE_CFG) == 1;
            lisIntegrationVersion = (LisIntegrationVersion)ConfigUtil.GetIntConfig(LIS_INTEGRATION_VERSION_CFG);
            isCallGenerateBarcode = ConfigUtil.GetIntConfig(LIS_CALL_GENERATE_BARCODE_CFG) == 1;
            isSendRequestLabconn = ConfigUtil.GetIntConfig(LIS_LABCONN_IS_SEND_REQUEST_CFG) == 1;
            lisIntegrationType = integrateType;
            lisForbidNotEnoughFee = forbidNotEnoughFee;
        }
    }
}
