using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignService.Config
{
    class SereServCFG
    {
        private const string SDA_CONFIG__HIS_SERE_SERV__ALLOW_ASSIGN_PRICE = "MOS.HIS_SERE_SERV.ALLOW_ASSIGN_PRICE";//Doi tuong mien phi

        public static bool AllowAssignPrice
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(SDA_CONFIG__HIS_SERE_SERV__ALLOW_ASSIGN_PRICE) == "1";
            }
        }
    }
}
