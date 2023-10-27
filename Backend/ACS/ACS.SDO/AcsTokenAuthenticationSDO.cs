using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.SDO
{
    public class AcsTokenAuthenticationSDO : ACS_USER
    {
        public string ApplicationCode { get; set; }
        public string AppVersion { get; set; }
    }
}
