using ACS.EFMODEL.DataModels;
using System;

namespace ACS.SDO
{
    public class AcsUserUpdateLoginNameTDO
    {
        public AcsUserUpdateLoginNameTDO() { }

        public string LoginName { get; set; }
        public string Password { get; set; }
        public string SubLoginName { get; set; }
    }
}
