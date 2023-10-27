using ACS.EFMODEL.DataModels;
using Inventec.Token.AuthSystem;
using Inventec.Token.Core;
using System;
using System.Collections.Generic;

namespace ACS.SDO
{
    public class AcsTokenSyncInsertSDO
    {
        public AcsTokenSyncInsertSDO() { }
                
        public string AuthenticationCode { get; set; }
        public string AuthorSystemCode { get; set; }
        public DateTime ExpireTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public string LoginAddress { get; set; }
        public DateTime LoginTime { get; set; }
        public string MachineName { get; set; }
        public string RenewCode { get; set; }
        public List<RoleData> RoleDatas { get; set; }
        public string ValidAddress { get; set; }
        public string TokenCode { get; set; }
        public UserData User { get; set; }
        public string VersionApp { get; set; }
        public int TokenCount { get; set; }
    }
}
