using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Desktop.Plugins.AcsModuleRole.entity
{
    public class ModuleADO : V_ACS_MODULE
    {
        public bool CheckForModule { get; set; }
        public bool CheckForRole { get; set; }
    }
}
