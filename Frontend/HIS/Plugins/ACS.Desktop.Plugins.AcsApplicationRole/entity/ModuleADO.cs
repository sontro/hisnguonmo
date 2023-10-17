using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Desktop.Plugins.AcsApplicationRole.entity
{
    public class ApplicationADO : ACS_APPLICATION
    {
        public bool CheckForApplication { get; set; }
        public bool CheckForRole { get; set; }
    }
}
