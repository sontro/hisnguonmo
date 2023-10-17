using SAR.Desktop.Plugins.SarPrintType.ADO;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAR.Desktop.Plugins.SarPrintType
{
    public class UserProcessor
    {
        public static List<ACS_USER> AcsUsers { get; set; }

        object uc;
        public UserProcessor()
            : base()
        { }


        public UserProcessor(CommonParam paramBusiness, List<ACS_USER> _AcsUsers)
           
        {
            if (_AcsUsers != null && _AcsUsers.Count > 0)
            {
                AcsUsers = _AcsUsers.Where(p => p.IS_ACTIVE == 1).ToList();
            }

        }

      
    }
}
