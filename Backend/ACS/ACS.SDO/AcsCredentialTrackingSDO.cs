using ACS.EFMODEL.DataModels;
using AutoMapper;
using Inventec.Token.AuthSystem;
using System;

namespace ACS.SDO
{
    public class AcsCredentialTrackingSDO
    {
        public AcsCredentialTrackingSDO() { }
       
        public string ValidAddress { get; set; }
        public DateTime ExpireTime { get; set; }
        public string LoginAddress { get; set; }
        public DateTime LoginTime { get; set; }
        public string RenewCode { get; set; }
        public string TokenCode { get; set; }
        public string ApplicationCode { get; set; }
        public string Email { get; set; }
        public string GCode { get; set; }
        public string LoginName { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }

        public string VersionApp { get; set; }
        public string MachineName { get; set; }
        public DateTime LastAccessTime { get; set; }

        public string AuthenticationCode { get; set; }
        public string AuthorSystemCode { get; set; }
    }
}
