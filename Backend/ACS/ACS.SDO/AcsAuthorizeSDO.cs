using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.SDO
{
    public class AcsAuthorizeSDO
    {
        public List<V_ACS_MODULE> ModuleInRoles { get; set; }
        public List<ACS_CONTROL> ControlInRoles { get; set; }
        public bool IsFull { get; set; }
    }
}
