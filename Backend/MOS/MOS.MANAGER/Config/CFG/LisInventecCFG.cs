using Inventec.Common.Logging;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.Config
{
    /// <summary>
    /// Luu thong tin dia chi cua LIS Inventec tuong ung voi tung phong xet nghiem
    /// </summary>
    class LisInventecAddress
    {
        public string RoomCode { get; set; }
        public string Url { get; set; }
    }

    /// <summary>
    /// Luu cac cau hinh de tich hop voi Lis Inventec
    /// </summary>
    class LisInventecCFG
    {
        private const string LIS_INVENTEC_ADDRESS_CFG = "MOS.LIS.INVENTEC.ADDRESS";

        private static List<LisInventecAddress> addresses;
        public static List<LisInventecAddress> ADDRESSES
        {
            get
            {
                if (addresses == null)
                {
                    addresses = LisInventecCFG.ParseAddress(LIS_INVENTEC_ADDRESS_CFG);
                }
                return addresses;
            }
            set
            {
                addresses = value;
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

        private static Dictionary<string, List<long>> GetDic()
        {
            Dictionary<string, List<long>> result = new Dictionary<string, List<long>>();
            try
            {
                var address = ADDRESSES;
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

        private static List<LisInventecAddress> ParseAddress(string code)
        {
            try
            {
                List<LisInventecAddress> result = new List<LisInventecAddress>();
                string data = ConfigUtil.GetStrConfig(code);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LisInventecAddress>>(data);
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
            addresses = LisInventecCFG.ParseAddress(LIS_INVENTEC_ADDRESS_CFG);
            dicExecuteRoom = GetDic();
        }
    }
}
