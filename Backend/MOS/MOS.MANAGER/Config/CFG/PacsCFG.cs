using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class PacsAddress
    {
        public string RoomCode { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string CloudInfo { get; set; }
    }

    public class PacsFileAddress
    {
        public string RoomCode { get; set; }
        public string Ip { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string SaveFolder { get; set; }
        public string ReadFolder { get; set; }
    }

    public class PacsApiInfo
    {
        public string AppCode { get; set; }
        public string Order { get; set; }
        public string Delete { get; set; }
        public string PatientInfo { get; set; }
        public string SendResult { get; set; }
    }

    public class SplitDescription
    {
        public string BAT_DAU { get; set; }
        public string KET_THUC { get; set; }
    }

    public class DescriptionDetail
    {
        public SplitDescription MO_TA { get; set; }
        public SplitDescription GHI_CHU { get; set; }
        public SplitDescription KET_LUAN { get; set; }
    }

    enum PacsConnectionType
    {
        TCP_IP = 1,
        FILE = 2,
        API = 3
    }

    enum PacsHL7Version
    {
        V231 = 1,
        V24 = 2,
        V27 = 3,
    }

    /// <summary>
    /// Chon cac he thong PACS tich hop
    /// </summary>
    public enum PacsIntegrateOption
    {
        /// <summary>
        /// He thong PACS do Vietsens phat trien
        /// </summary>
        PACS_VIETSENS = 1,
        /// <summary>
        /// He thong PACS cua Sency
        /// </summary>
        PACS_SANCY = 2,
        /// <summary>
        /// He thong PACS cua Bach Khoa
        /// </summary>
        PACS_BACH_KHOA = 3
    }

    /// <summary>
    /// Chon 
    /// </summary>
    public enum PacsRequestUserInfoOption
    {
        /// <summary>
        /// Su dung chung chi hanh nghe
        /// </summary>
        DIPLOMA = 1,
        /// <summary>
        /// Su dung thong tin tai khoan (ten dang nhap, ho ten)
        /// </summary>
        ACCOUNT_INFO = 2
    }

    public enum PatientInfoUpdateOption
    {
        /// <summary>
        /// Su dung ban tin ORM, va chi gui lai thong tin cua cac y lenh chua xu ly
        /// </summary>
        ORM = 1,
        /// <summary>
        /// Su dung ban tin ADTA08, va luon gui khong quan tam benh nhan co y lenh chua xu ly hay khong
        /// </summary>
        ADTA08 = 2
    }

    class PacsCFG
    {
        private const string PACS_INTEGRATE_OPTION_CFG = "MOS.PACS.INTEGRATE_OPTION";
        private const string PACS_ADDRESS_CFG = "MOS.PACS.ADDRESS";
        private const string PACS_ADDRESS_FILE_CFG = "MOS.PACS.FILE_ADDRESS";
        private const string CONNECTION_TYPE_CFG = "MOS.PACS.CONNECTION_TYPE";
        private const string HL7VERSION_CFG = "MOS.PACS.HL7VERSION";
        private const string FHIR_CONNECT_INFO_CFG = "MOS.PACS.FHIR_CONNECT_INFO";
        private const string PATIENT_CODE_PREFIX_CFG = "MOS.PACS.PATIENT_CODE_PREFIX";
        private const string PROCESS_DESCRIPTION_CFG = "MOS.PACS.PROCESS_DESCRIPTION";
        private const string REQUEST_USER_INFO_OPTION_CFG = "MOS.PACS.REQUEST_USER_INFO.OPTION";
        private const string IS_SET_BRANCH_CODE_CFG = "MOS.PACS.IS_SET_BRANCH_CODE";
        private const string EXECUTE_ROOM_OPTION_CODE_CFG = "MOS.PACS.EXECUTE_ROOM.OPTION";
        private const string REQUEST_ROOM_OPTION_CFG = "MOS.PACS.REQUEST_ROOM.OPTION";
        private const string WCF_ADDRESS_CFG = "MOS.PACS.SERVICE_PIS.WCF_ADDRESS";
        private const string PATIENT_INFO_UPDATE_OPTION_CFG = "MOS.PACS.PATIENT_INFO.UPDATE_OPTION";
        private const string SEND_WHEN_CHANGE_STATUS_CFG = "MOS.PACS.SEND_WHEN_CHANGE_STATUS";
        private const string API_INFORMATION_CFG = "MOS.PACS.API_INFORMATION";

        /// <summary>
        /// Da gui sang PACS thanh cong
        /// </summary>
        public const long PACS_STT_ID_SEND = (long)1;

        /// <summary>
        /// Da nhan ket qua tu PACS thanh cong
        /// </summary>
        public const long PACS_STT_ID_RESULT = (long)2;

        private static bool? isSetBranchCode;
        public static bool IS_SET_BRANCH_CODE
        {
            get
            {
                if (!isSetBranchCode.HasValue)
                {
                    isSetBranchCode = ConfigUtil.GetIntConfig(IS_SET_BRANCH_CODE_CFG) == 1;
                }
                return isSetBranchCode.Value;
            }
        }

        private static bool? isSetBranchCodeByMediOrgCode;
        public static bool IS_SET_BRANCH_CODE_BY_MEDI_ORG_CODE
        {
            get
            {
                if (!isSetBranchCodeByMediOrgCode.HasValue)
                {
                    isSetBranchCodeByMediOrgCode = ConfigUtil.GetIntConfig(IS_SET_BRANCH_CODE_CFG) == 2;
                }
                return isSetBranchCodeByMediOrgCode.Value;
            }
        }

        private static string fhirConnectInfo;
        public static string FHIR_CONNECT_INFO
        {
            get
            {
                if (String.IsNullOrEmpty(fhirConnectInfo))
                {
                    fhirConnectInfo = ConfigUtil.GetStrConfig(FHIR_CONNECT_INFO_CFG);
                }
                return fhirConnectInfo;
            }
            set
            {
                fhirConnectInfo = value;
            }
        }

        private static int? pacsIntegrateOption;
        public static int PACS_INTEGRATE_OPTION
        {
            get
            {
                if (!pacsIntegrateOption.HasValue)
                {
                    pacsIntegrateOption = ConfigUtil.GetIntConfig(PACS_INTEGRATE_OPTION_CFG);
                }
                return pacsIntegrateOption.Value;
            }
        }

        private static PacsRequestUserInfoOption? requestUserInfoOption;
        public static PacsRequestUserInfoOption REQUEST_USER_INFO_OPTION
        {
            get
            {
                if (!requestUserInfoOption.HasValue)
                {
                    requestUserInfoOption = (PacsRequestUserInfoOption)ConfigUtil.GetIntConfig(REQUEST_USER_INFO_OPTION_CFG);
                }
                return requestUserInfoOption.Value;
            }
        }

        private static PacsHL7Version pacsHL7Version;
        public static PacsHL7Version PACS_HL7_VERSION
        {
            get
            {
                if (pacsHL7Version == 0)
                {
                    pacsHL7Version = (PacsHL7Version)ConfigUtil.GetIntConfig(HL7VERSION_CFG);
                }
                return pacsHL7Version;
            }
        }

        private static List<PacsAddress> pacsAddress;
        internal static List<PacsAddress> PACS_ADDRESS
        {
            get
            {
                if (pacsAddress == null)
                {
                    pacsAddress = GetAddress(PACS_ADDRESS_CFG);
                }
                return pacsAddress;
            }
        }

        private static PacsConnectionType connectionType;
        public static PacsConnectionType CONNECTION_TYPE
        {
            get
            {
                if (connectionType == 0)
                {
                    connectionType = (PacsConnectionType)ConfigUtil.GetIntConfig(CONNECTION_TYPE_CFG);
                }
                return connectionType;
            }
        }

        private static List<PacsFileAddress> pacsFileAddresses;
        public static List<PacsFileAddress> PACS_FILE_ADDRESSES
        {
            get
            {
                if (pacsFileAddresses == null)
                {
                    pacsFileAddresses = PacsCFG.ParseFileAddress(PACS_ADDRESS_FILE_CFG);
                }
                return pacsFileAddresses;
            }
        }

        private static List<PacsFileAddress> ParseFileAddress(string code)
        {
            try
            {
                List<PacsFileAddress> result = new List<PacsFileAddress>();
                string data = ConfigUtil.GetStrConfig(code);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PacsFileAddress>>(data);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        private static List<PacsAddress> GetAddress(string code)
        {
            List<PacsAddress> result = new List<PacsAddress>();
            try
            {
                string value = ConfigUtil.GetStrConfig(code);
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(code);
                }
                List<PacsAddress> adds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PacsAddress>>(value);
                if (adds == null || adds.Count == 0)
                {
                    throw new AggregateException(code);
                }

                foreach (var item in adds)
                {
                    if (item.RoomCode.Contains('|'))
                    {
                        string[] roomCodes = item.RoomCode.Split('|');
                        for (int i = 0; i < roomCodes.Length; i++)
                        {
                            if (!String.IsNullOrWhiteSpace(roomCodes[i]))
                            {
                                PacsAddress newRoom = new PacsAddress();
                                newRoom.Address = item.Address;
                                newRoom.Port = item.Port;
                                newRoom.RoomCode = roomCodes[i].Trim();
                                newRoom.CloudInfo = item.CloudInfo;
                                result.Add(newRoom);
                            }
                        }
                    }
                    else
                    {
                        result.Add(item);
                    }
                }

                //result.AddRange(adds);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new List<PacsAddress>();
            }
            return result;
        }

        private static PacsApiInfo GetPacsApiInfo(string code)
        {
            PacsApiInfo result = new PacsApiInfo();
            try
            {
                string value = ConfigUtil.GetStrConfig(code);
                if (!String.IsNullOrWhiteSpace(value))
                {
                    string[] detail = value.Split('|');
                    result.AppCode = detail.Length > 0 ? detail[0] : "";
                    result.Order = detail.Length > 1 ? detail[1] : "";
                    result.Delete = detail.Length > 2 ? detail[2] : "";
                    result.PatientInfo = detail.Length > 3 ? detail[3] : "";
                    result.SendResult = detail.Length > 4 ? detail[4] : "";
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private static string patientCodePrefix;
        public static string PATIENT_CODE_PREFIX
        {
            get
            {
                if (String.IsNullOrEmpty(patientCodePrefix))
                {
                    patientCodePrefix = ConfigUtil.GetStrConfig(PATIENT_CODE_PREFIX_CFG);
                }
                return patientCodePrefix;
            }
            set
            {
                patientCodePrefix = value;
            }
        }

        private static DescriptionDetail DescriptionDetail;
        public static DescriptionDetail PROCESS_DESCRIPTION
        {
            get
            {
                if (DescriptionDetail == null)
                {
                    var cfgValue = ConfigUtil.GetStrConfig(PROCESS_DESCRIPTION_CFG);
                    if (!String.IsNullOrWhiteSpace(cfgValue))
                    {
                        DescriptionDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<DescriptionDetail>(cfgValue);
                    }
                }
                return DescriptionDetail;
            }
        }

        private static int? excuteRoomOption;
        public static int PACS_EXCUTE_ROOM_OPTION
        {
            get
            {
                if (!excuteRoomOption.HasValue)
                {
                    excuteRoomOption = ConfigUtil.GetIntConfig(EXECUTE_ROOM_OPTION_CODE_CFG);
                }
                return excuteRoomOption.Value;
            }
        }

        private static bool? isAllowRequestRoom;
        public static bool IS_ALLOW_REQUEST_ROOM
        {
            get
            {
                if (!isAllowRequestRoom.HasValue)
                {
                    isAllowRequestRoom = ConfigUtil.GetIntConfig(REQUEST_ROOM_OPTION_CFG) == 1;
                }
                return isAllowRequestRoom.Value;
            }
        }

        private static string wcfAddress;
        public static string WCF_ADDRESS
        {
            get
            {
                if (String.IsNullOrEmpty(wcfAddress))
                {
                    wcfAddress = ConfigUtil.GetStrConfig(WCF_ADDRESS_CFG);
                }
                return wcfAddress;
            }
        }

        private static PatientInfoUpdateOption? patientInfoUpdateOption;
        public static PatientInfoUpdateOption PATIENT_INFO_UPDATE_OPTION
        {
            get
            {
                if (!patientInfoUpdateOption.HasValue)
                {
                    patientInfoUpdateOption = (PatientInfoUpdateOption)ConfigUtil.GetIntConfig(PATIENT_INFO_UPDATE_OPTION_CFG);
                }
                return patientInfoUpdateOption.Value;
            }
        }

        private static PacsApiInfo apiInfo;
        public static PacsApiInfo PACS_API_INFO
        {
            get
            {
                if (apiInfo == null)
                {
                    apiInfo = GetPacsApiInfo(API_INFORMATION_CFG);
                }
                return apiInfo;
            }
        }

        private static bool? sendWhenChangeStatus;
        public static bool SEND_WHEN_CHANGE_STATUS
        {
            get
            {
                if (!sendWhenChangeStatus.HasValue)
                {
                    sendWhenChangeStatus = ConfigUtil.GetIntConfig(SEND_WHEN_CHANGE_STATUS_CFG) == 1;
                }
                return sendWhenChangeStatus.Value;
            }
        }

        private static MedilinkHL7.SDK.SendSANCY.VersionV2? libraryHL7Version;
        public static MedilinkHL7.SDK.SendSANCY.VersionV2 LIBRARY_HL7_VERSION
        {
            get
            {
                if (libraryHL7Version == null)
                {
                    switch (PACS_HL7_VERSION)
                    {
                        case PacsHL7Version.V231:
                            libraryHL7Version = MedilinkHL7.SDK.SendSANCY.VersionV2.V231;
                            break;
                        case PacsHL7Version.V24:
                            libraryHL7Version = MedilinkHL7.SDK.SendSANCY.VersionV2.V24;
                            break;
                        case PacsHL7Version.V27:
                            libraryHL7Version = MedilinkHL7.SDK.SendSANCY.VersionV2.V27;
                            break;
                        default:
                            libraryHL7Version = MedilinkHL7.SDK.SendSANCY.VersionV2.V24;
                            break;
                    }
                }
                return libraryHL7Version.Value;
            }
        }

        public static void Reload()
        {
            var integrateOption = ConfigUtil.GetIntConfig(PACS_INTEGRATE_OPTION_CFG);
            var address = GetAddress(PACS_ADDRESS_CFG);
            pacsIntegrateOption = integrateOption;
            pacsAddress = address;

            pacsFileAddresses = PacsCFG.ParseFileAddress(PACS_ADDRESS_FILE_CFG);
            connectionType = (PacsConnectionType)ConfigUtil.GetIntConfig(CONNECTION_TYPE_CFG);
            pacsHL7Version = (PacsHL7Version)ConfigUtil.GetIntConfig(HL7VERSION_CFG);
            fhirConnectInfo = ConfigUtil.GetStrConfig(FHIR_CONNECT_INFO_CFG);
            patientCodePrefix = ConfigUtil.GetStrConfig(PATIENT_CODE_PREFIX_CFG);
            requestUserInfoOption = (PacsRequestUserInfoOption)ConfigUtil.GetIntConfig(REQUEST_USER_INFO_OPTION_CFG);
            isSetBranchCode = ConfigUtil.GetIntConfig(IS_SET_BRANCH_CODE_CFG) == 1;
            excuteRoomOption = ConfigUtil.GetIntConfig(EXECUTE_ROOM_OPTION_CODE_CFG);
            isAllowRequestRoom = ConfigUtil.GetIntConfig(REQUEST_ROOM_OPTION_CFG) == 1;
            isSetBranchCodeByMediOrgCode = ConfigUtil.GetIntConfig(IS_SET_BRANCH_CODE_CFG) == 2;
            wcfAddress = ConfigUtil.GetStrConfig(WCF_ADDRESS_CFG);
            patientInfoUpdateOption = (PatientInfoUpdateOption)ConfigUtil.GetIntConfig(PATIENT_INFO_UPDATE_OPTION_CFG);
            sendWhenChangeStatus = ConfigUtil.GetIntConfig(SEND_WHEN_CHANGE_STATUS_CFG) == 1;
            apiInfo = GetPacsApiInfo(API_INFORMATION_CFG);
            libraryHL7Version = null;
        }
    }
}
