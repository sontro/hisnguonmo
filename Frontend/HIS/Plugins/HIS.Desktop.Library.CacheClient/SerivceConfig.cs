using HIS.Desktop.Library.CacheClient.Redis;
using System.Configuration;

namespace HIS.Desktop.Library.CacheClient
{
    public class SerivceConfig
    {
        public static string MosBaseUri { get; set; }
        public static string SdaBaseUri { get; set; }
        public static string SarBaseUri { get; set; }
        public static string AcsBaseUri { get; set; }
        public static string TokenCode { get; set; }
        public static string ApplicationCode { get; set; }
        public static long CacheType { get; set; }
        public static RedisSaveType RedisSaveType { get; set; }
        public static string PreNamespaceFolder { get; set; }

        //public static readonly int timeSync = int.Parse(ConfigurationManager.AppSettings["HIS.Service.LocalStorage.DataConfig.TimeSync"] ?? "600000");

        public const string Seperate = ",";
        public const string TableName__SHC_SYNC = "SHC_SYNC";

        public const string ID = "ID";
        public const string KEY = "KEY";
        public const string LAST_DB_MODIFY_TIME = "LAST_DB_MODIFY_TIME";
        public const string LAST_SYNC_MODIFY_TIME = "LAST_SYNC_MODIFY_TIME";
        public const string VALUE = "VALUE";
        public const string IS_MODIFIED = "IS_MODIFIED";
        public const string MODIFY_TIME = "MODIFY_TIME";

        public const string Pattern = "urn:{0}:*";
    }
}
