using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Desktop.Plugins.AcsApplicationRole.entity
{
    public class RoleADO : ACS_ROLE
    {
        public bool checkForApplication { get; set; }
        public bool checkForRole { get; set; }
    }
}
