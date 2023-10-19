using ACS.EFMODEL.DataModels;
using Inventec.Token.AuthSystem;
using System;

namespace ACS.SDO
{
    public class AcsTokenSyncDeleteSDO
    {
        public AcsTokenSyncDeleteSDO() { }

        public string TokenCode { get; set; }
        public int TokenCount { get; set; }
    }
}
