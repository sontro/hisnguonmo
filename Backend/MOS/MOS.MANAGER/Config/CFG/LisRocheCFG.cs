using Inventec.Common.Logging;
using SDA.EFMODEL.DataModels;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    /// <summary>
    /// Luu thong tin dia chi cua ROCHE (giao thuc ket noi qua TCP/IP) tuong ung voi tung phong xet nghiem
    /// </summary>
    class LisRocheTcpIpAddress
    {
        public string RoomCode { get; set; }
        public string Url { get; set; }
    }

    /// <summary>
    /// Luu thong tin dia chi cua ROCHE (giao thuc ket noi qua file) tuong ung voi tung phong xet nghiem
    /// </summary>
    class LisRocheFileAddress
    {
        public string RoomCode { get; set; }
        public string Ip { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string SaveFolder { get; set; }
        public string ReadFolder { get; set; }
        public FileHandlerType FileHandlerType { get; set; }
    }

    enum RocheConnectionType
    {
        TCP_IP = 1,
        FILE = 2
    }

    enum RocheMessageFormatType
    {
        ASTM = 1,
        HL7 = 2,
        ASTM_HL7 = 3
    }

    enum PatientNameFormatOption
    {
        /// <summary>
        /// Tach rieng first-name, last-name
        /// </summary>
        NORMAL = 1,
        /// <summary>
        /// Gop chung first-name, last-name
        /// </summary>
        MERGE = 2,
        /// <summary>
        /// Su dung ten ko dau thay cho first-name
        /// </summary>
        UNSIGNED_AS_FIRST_NAME = 3
    }

    /// <summary>
    /// Luu cac cau hinh de tich hop voi Lis roche
    /// </summary>
    class LisRocheCFG
    {
        private const string CONNECTION_TYPE_CFG = "MOS.LIS.ROCHE.CONNECTION_TYPE";
        private const string ADDRESS_TCP_IP_CFG = "MOS.LIS.ROCHE.ADDRESS.TCP_IP";
        private const string ADDRESS_FILE_CFG = "MOS.LIS.ROCHE.ADDRESS.FILE";
        private const string ADDRESS_FILE_HL7_CFG = "MOS.LIS.ROCHE.ADDRESS.FILE_HL7";
        private const string SENDING_APP_CODE_CFG = "MOS.LIS.ROCHE.SENDING_APP_CODE";
        private const string RECEIVING_APP_CODE_CFG = "MOS.LIS.ROCHE.RECEIVING_APP_CODE";
        private const string MESSAGE_FORMAT_IS_USING_PATIENT_TYPE_INSTEAD_OF_DEPARTMENT_CODE_CFG = "MOS.LIS.ROCHE.MESSAGE_FORMAT.IS_USING_PATIENT_TYPE_INSTEAD_OF_DEPARTMENT_CODE";
        private const string MESSAGE_FORMAT_IS_USING_VIETNAMESE_CFG = "MOS.LIS.ROCHE.MESSAGE_FORMAT.IS_USING_VIETNAMESE";
        private const string MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_ADMINISTRATIVE_DIVISION_CFG = "MOS.LIS.ROCHE.MESSAGE_FORMAT.PATIENT_INFO.IS_HAVING_ADMINISTRATIVE_DIVISION";
        private const string MESSAGE_FORMAT_PATIENT_INFO_NAME_OPTION_CFG = "MOS.LIS.ROCHE.MESSAGE_FORMAT.PATIENT_INFO.NAME_OPTION";
        private const string MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_BHYT_NUMBER_CFG = "MOS.LIS.ROCHE.MESSAGE_FORMAT.PATIENT_INFO.IS_HAVING_BHYT_NUMBER";
        private const string MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_ADDRESS_CFG = "MOS.LIS.ROCHE.MESSAGE_FORMAT.PATIENT_INFO.IS_HAVING_ADDRESS";
        private const string MESSAGE_FORMAT_ORDER_INFO_IS_HAVING_DOCTOR_NAME_CFG = "MOS.LIS.ROCHE.MESSAGE_FORMAT.ORDER_INFO.IS_HAVING_DOCTOR_NAME";
        private const string MESSAGE_FORMAT_ORDER_INFO_ORDER_CODE_LENGTH_CFG = "MOS.LIS.ROCHE.MESSAGE_FORMAT.ORDER_INFO.ORDER_CODE_LENGTH";
        private const string FILE_FORMAT_ORDER_PREFIX_CFG = "MOS.LIS.ROCHE.FILE_FORMAT.ORDER_PREFIX";
        private const string MESSAGE_FORMAT_TYPE_CFG = "MOS.LIS.ROCHE.MESSAGE_FORMAT.TYPE";
        
        private static RocheConnectionType connectionType;
        public static RocheConnectionType CONNECTION_TYPE
        {
            get
            {
                if (connectionType == 0)
                {
                    connectionType = (RocheConnectionType)ConfigUtil.GetIntConfig(CONNECTION_TYPE_CFG);
                }
                return connectionType;
            }
        }

        private static RocheMessageFormatType messageFormatType;
        public static RocheMessageFormatType MESSAGE_FORMAT_TYPE
        {
            get
            {
                if (messageFormatType == 0)
                {
                    messageFormatType = (RocheMessageFormatType)ConfigUtil.GetIntConfig(MESSAGE_FORMAT_TYPE_CFG);
                }
                return messageFormatType;
            }
        }

        private static List<LisRocheTcpIpAddress> tcpIpAddresses;
        public static List<LisRocheTcpIpAddress> TCP_IP_ADDRESSES
        {
            get
            {
                if (tcpIpAddresses == null)
                {
                    tcpIpAddresses = LisRocheCFG.ParseTcpIpAddress(ADDRESS_TCP_IP_CFG);
                }
                return tcpIpAddresses;
            }
        }

        private static List<LisRocheFileAddress> fileAddresses;
        public static List<LisRocheFileAddress> FILE_ADDRESSES
        {
            get
            {
                if (fileAddresses == null)
                {
                    fileAddresses = LisRocheCFG.ParseFileAddress(ADDRESS_FILE_CFG);
                }
                return fileAddresses;
            }
        }

        private static List<LisRocheFileAddress> fileHl7Addresses;
        public static List<LisRocheFileAddress> FILE_HL7_ADDRESSES
        {
            get
            {
                if (fileHl7Addresses == null)
                {
                    fileHl7Addresses = LisRocheCFG.ParseFileAddress(ADDRESS_FILE_HL7_CFG);
                }
                return fileHl7Addresses;
            }
        }

        private static string sendingAppCode;
        public static string SENDING_APP_CODE
        {
            get
            {
                if (sendingAppCode == null)
                {
                    sendingAppCode = ConfigUtil.GetStrConfig(SENDING_APP_CODE_CFG);
                }
                return sendingAppCode;
            }
        }

        private static string receivingAppCode;
        public static string RECEIVING_APP_CODE
        {
            get
            {
                if (receivingAppCode == null)
                {
                    receivingAppCode = ConfigUtil.GetStrConfig(RECEIVING_APP_CODE_CFG);
                }
                return receivingAppCode;
            }
        }

        private static string fileFormatOrderPrefix;
        public static string FILE_FORMAT_ORDER_PREFIX
        {
            get
            {
                if (fileFormatOrderPrefix == null)
                {
                    fileFormatOrderPrefix = ConfigUtil.GetStrConfig(FILE_FORMAT_ORDER_PREFIX_CFG);
                }
                return fileFormatOrderPrefix;
            }
        }

        private static bool? messageFormatIsUsingPatientTypeInsteadOfDepartmentCode;
        public static bool MESSAGE_FORMAT_IS_USING_PATIENT_TYPE_INSTEAD_OF_DEPARTMENT_CODE
        {
            get
            {
                if (!messageFormatIsUsingPatientTypeInsteadOfDepartmentCode.HasValue)
                {
                    messageFormatIsUsingPatientTypeInsteadOfDepartmentCode = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_IS_USING_PATIENT_TYPE_INSTEAD_OF_DEPARTMENT_CODE_CFG) == 1;
                }
                return messageFormatIsUsingPatientTypeInsteadOfDepartmentCode.Value;
            }
        }

        private static bool? messageFormatIsUsingVietnamese;
        public static bool MESSAGE_FORMAT_IS_USING_VIETNAMESE
        {
            get
            {
                if (!messageFormatIsUsingVietnamese.HasValue)
                {
                    messageFormatIsUsingVietnamese = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_IS_USING_VIETNAMESE_CFG) == 1;
                }
                return messageFormatIsUsingVietnamese.Value;
            }
        }

        private static PatientNameFormatOption? messageFormatPatientInfoNameOption;
        public static PatientNameFormatOption MESSAGE_FORMAT_PATIENT_INFO_NAME_OPTION
        {
            get
            {
                if (!messageFormatPatientInfoNameOption.HasValue)
                {
                    messageFormatPatientInfoNameOption = (PatientNameFormatOption) ConfigUtil.GetIntConfig(MESSAGE_FORMAT_PATIENT_INFO_NAME_OPTION_CFG);
                }
                return messageFormatPatientInfoNameOption.Value;
            }
        }

        private static bool? messageFormatPatientInfoIsHavingAdministrativeDivision;
        public static bool MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_ADMINISTRATIVE_DIVISION
        {
            get
            {
                if (!messageFormatPatientInfoIsHavingAdministrativeDivision.HasValue)
                {
                    messageFormatPatientInfoIsHavingAdministrativeDivision = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_ADMINISTRATIVE_DIVISION_CFG) == 1;
                }
                return messageFormatPatientInfoIsHavingAdministrativeDivision.Value;
            }
        }

        private static bool? messageFormatPatientInfoIsHavingBhytNumber;
        public static bool MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_BHYT_NUMBER
        {
            get
            {
                if (!messageFormatPatientInfoIsHavingBhytNumber.HasValue)
                {
                    messageFormatPatientInfoIsHavingBhytNumber = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_BHYT_NUMBER_CFG) == 1;
                }
                return messageFormatPatientInfoIsHavingBhytNumber.Value;
            }
        }

        private static bool? messageFormatPatientInfoIsHavingAddress;
        public static bool MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_ADDRESS
        {
            get
            {
                if (!messageFormatPatientInfoIsHavingAddress.HasValue)
                {
                    messageFormatPatientInfoIsHavingAddress = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_ADDRESS_CFG) == 1;
                }
                return messageFormatPatientInfoIsHavingAddress.Value;
            }
        }

        private static bool? messageFormatOrderInfoIsHavingBhytNumber;
        public static bool MESSAGE_FORMAT_ORDER_INFO_IS_HAVING_DOCTOR_NAME
        {
            get
            {
                if (!messageFormatOrderInfoIsHavingBhytNumber.HasValue)
                {
                    messageFormatOrderInfoIsHavingBhytNumber = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_ORDER_INFO_IS_HAVING_DOCTOR_NAME_CFG) == 1;
                }
                return messageFormatOrderInfoIsHavingBhytNumber.Value;
            }
        }

        private static int? messageFormatOrderInfoOrderCodeLength;
        public static int MESSAGE_FORMAT_ORDER_INFO_ORDER_CODE_LENGTH
        {
            get
            {
                if (!messageFormatOrderInfoOrderCodeLength.HasValue)
                {
                    messageFormatOrderInfoOrderCodeLength = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_ORDER_INFO_ORDER_CODE_LENGTH_CFG);
                }
                return messageFormatOrderInfoOrderCodeLength.Value;
            }
        }

        private static List<LisRocheTcpIpAddress> ParseTcpIpAddress(string code)
        {
            try
            {
                List<LisRocheTcpIpAddress> result = new List<LisRocheTcpIpAddress>();
                string data = ConfigUtil.GetStrConfig(code);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LisRocheTcpIpAddress>>(data);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        private static List<LisRocheFileAddress> ParseFileAddress(string code)
        {
            try
            {
                List<LisRocheFileAddress> result = new List<LisRocheFileAddress>();
                string data = ConfigUtil.GetStrConfig(code);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LisRocheFileAddress>>(data);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static void Reload()
        {
            connectionType = (RocheConnectionType)ConfigUtil.GetIntConfig(CONNECTION_TYPE_CFG);
            tcpIpAddresses = LisRocheCFG.ParseTcpIpAddress(ADDRESS_TCP_IP_CFG);
            fileAddresses = LisRocheCFG.ParseFileAddress(ADDRESS_FILE_CFG);
            sendingAppCode = ConfigUtil.GetStrConfig(SENDING_APP_CODE_CFG);
            receivingAppCode = ConfigUtil.GetStrConfig(RECEIVING_APP_CODE_CFG);
            messageFormatIsUsingPatientTypeInsteadOfDepartmentCode = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_IS_USING_PATIENT_TYPE_INSTEAD_OF_DEPARTMENT_CODE_CFG) == 1;
            messageFormatIsUsingVietnamese = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_IS_USING_VIETNAMESE_CFG) == 1;
            messageFormatPatientInfoIsHavingAdministrativeDivision = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_ADMINISTRATIVE_DIVISION_CFG) == 1;
            messageFormatPatientInfoIsHavingBhytNumber = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_BHYT_NUMBER_CFG) == 1;
            messageFormatPatientInfoIsHavingAddress = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_ADDRESS_CFG) == 1;
            messageFormatOrderInfoIsHavingBhytNumber = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_ORDER_INFO_IS_HAVING_DOCTOR_NAME_CFG) == 1;
            messageFormatOrderInfoOrderCodeLength = ConfigUtil.GetIntConfig(MESSAGE_FORMAT_ORDER_INFO_ORDER_CODE_LENGTH_CFG);
            messageFormatPatientInfoNameOption = (PatientNameFormatOption)ConfigUtil.GetIntConfig(MESSAGE_FORMAT_PATIENT_INFO_NAME_OPTION_CFG);
            fileFormatOrderPrefix = ConfigUtil.GetStrConfig(FILE_FORMAT_ORDER_PREFIX_CFG);
            messageFormatType = (RocheMessageFormatType)ConfigUtil.GetIntConfig(MESSAGE_FORMAT_TYPE_CFG);
            fileHl7Addresses = LisRocheCFG.ParseFileAddress(ADDRESS_FILE_HL7_CFG);
        }
    }
}