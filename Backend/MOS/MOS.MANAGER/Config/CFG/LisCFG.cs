using Inventec.Common.Logging;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.Config
{
    /// <summary>
    /// Luu cac cau hinh de tich hop voi Lis
    /// </summary>
    class LisCFG
    {
        /// <summary>
        /// Chon cac he thong LIS tich hop
        /// </summary>
        public enum LisIntegrateOption
        {
            /// <summary>
            /// He thong LIS do Inventec phat trien
            /// </summary>
            LIS = 1,
            /// <summary>
            /// He thong roche, ket noi bang TCP/IP
            /// </summary>
            ROCHE_TCP_IP = 2,
            /// <summary>
            /// He thong roche, ket noi bang file
            /// </summary>
            ROCHE_FILE = 3,
            /// <summary>
            /// Labconn
            /// </summary>
            LABCON = 4
        }

        /// <summary>
        /// Chon phien ban xu ly ket noi
        /// (De tranh rui ro khi len version moi, tam thoi se cho phep nguoi dung chon version de chay)
        /// </summary>
        public enum LisIntegerateVersion
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

        /// <summary>
        /// Da nhan ket qua tu LIS thanh cong
        /// </summary>
        public const long LIS_STT_ID_RESULT = (long)2;

        /// <summary>
        /// Luu thong tin dia chi cua LIS tuong ung voi tung phong xet nghiem
        /// </summary>
        public class LisAddress
        {
            public string RoomCode { get; set; }
            public string Url { get; set; }
        }

        private const string LIS_INTEGRATE_OPTION_CFG = "MOS.LIS.INTEGRATE_OPTION";
        private const string LIS_ADDRESS_CFG = "MOS.LIS.ADDRESS";
        private const string LIS_FORBID_NOT_ENOUGH_FEE_CFG = "MOS.LIS.FORBID_NOT_ENOUGH_FEE";
        private const string LIS_INTEGRATE_TYPE_CFG = "MOS.LIS.INTEGRATION_TYPE";
        private const string LIS_CHECK_FEE_WHEN_SPECIMEN_CFG = "MOS.LIS.CHECK_FEE_WHEN_SPECIMEN";

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
            set
            {
                lisForbidNotEnoughFee = value;
            }
        }

        private static int lisIntegrateType;
        public static int LIS_INTEGRATION_TYPE 
        {
            get
            {
                if (lisIntegrateType == 0)
                {
                    lisIntegrateType = ConfigUtil.GetIntConfig(LIS_INTEGRATE_OPTION_CFG);
                }
                return lisIntegrateType;
            }
            set
            {
                lisIntegrateType = value;
            }
        }

        private static int lisIntegrateOption;
        public static int LIS_INTEGRATE_OPTION
        {
            get
            {
                if (lisIntegrateOption == 0)
                {
                    lisIntegrateOption = ConfigUtil.GetIntConfig(LIS_INTEGRATE_TYPE_CFG);
                }
                return lisIntegrateOption;
            }
            set
            {
                lisIntegrateOption = value;
            }
        }

        private static List<LisAddress> lisAddresses;
        public static List<LisAddress> LIS_ADDRESSES
        {
            get
            {
                if (lisAddresses == null)
                {
                    lisAddresses = LisCFG.ParseLisAddress(LIS_ADDRESS_CFG);
                }
                return lisAddresses;
            }
            set
            {
                lisAddresses = value;
            }
        }

        private static Dictionary<string, List<long>> dicExecuteRoom;
        public static Dictionary<string, List<long>> DIC_EXECUTE_ROOM
        {
            get
            {
                if (dicExecuteRoom == null)
                {
                    dicExecuteRoom = GetDic();
                }
                return dicExecuteRoom;
            }
        }

        private static bool? lisCheckFeeWhenSpecimen;
        public static bool LIS_CHECK_FEE_WHEN_SPECIMEN
        {
            get
            {
                if (lisCheckFeeWhenSpecimen == null)
                {
                    lisCheckFeeWhenSpecimen = ConfigUtil.GetIntConfig(LIS_CHECK_FEE_WHEN_SPECIMEN_CFG) == 1;
                }
                return lisCheckFeeWhenSpecimen.Value;
            }
            set
            {
                lisCheckFeeWhenSpecimen = value;
            }
        }

        private static List<LisAddress> ParseLisAddress(string code)
        {
            try
            {
                List<LisAddress> result = new List<LisAddress>();
                string data = ConfigUtil.GetStrConfig(code);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LisAddress>>(data);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        private static Dictionary<string, List<long>> GetDic()
        {
            Dictionary<string, List<long>> result = new Dictionary<string, List<long>>();
            try
            {
                var address = LIS_ADDRESSES;
                address = address != null ? address.Where(o => !String.IsNullOrWhiteSpace(o.Url)).ToList() : null;
                if (address != null && address.Count > 0)
                {
                    var Groups = address.GroupBy(g => g.Url).ToList();
                    foreach (var group in Groups)
                    {
                        var executeRooms = HisExecuteRoomCFG.DATA.Where(o => group.Any(a => a.RoomCode == o.EXECUTE_ROOM_CODE)).ToList();
                        if (executeRooms != null && executeRooms.Count > 0)
                        {
                            result[group.Key] = executeRooms.Select(s => s.ROOM_ID).ToList();
                        }
                    }
                }

                if (result.Count == 0) LogSystem.Warn("khong co cau hinh dia chi LIS");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new Dictionary<string, List<long>>();
            }
            return result;
        }

        public static void Reload()
        {
            var address = LisCFG.ParseLisAddress(LIS_ADDRESS_CFG);
            var integrateOption = ConfigUtil.GetIntConfig(LIS_INTEGRATE_OPTION_CFG);
            var forbidNotEnoughFee = ConfigUtil.GetIntConfig(LIS_FORBID_NOT_ENOUGH_FEE_CFG) == 1;
            var dic = GetDic();
            lisAddresses = address;
            lisIntegrateOption = integrateOption;
            lisForbidNotEnoughFee = forbidNotEnoughFee;
            dicExecuteRoom = dic;
            lisIntegrateOption = ConfigUtil.GetIntConfig(LIS_INTEGRATE_TYPE_CFG);
            lisCheckFeeWhenSpecimen = ConfigUtil.GetIntConfig(LIS_CHECK_FEE_WHEN_SPECIMEN_CFG) == 1;
            
        }
    }
}
