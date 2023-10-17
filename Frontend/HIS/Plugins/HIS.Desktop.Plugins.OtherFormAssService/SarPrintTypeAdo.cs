using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.OtherFormAssService
{
    internal class SarPrintTypeAdo : SAR_PRINT_TYPE
    {
        public string Checked { get; set; } 
        public string FILE_NAME { get; set; } 
        public string TITLE { get; set; }
    }
}
