using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProviderBehavior.MOBIFONE.Model
{
    public class LoginResultData
    {
        public string token { get; set; }
        public string ma_dvcs { get; set; }
        public string wb_user_id { get; set; }
        public string error { get; set; }
    }
}
