using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisDepartment.ADO
{
    public class HeadADO
    {
         public string LOGINNAME { get; set; }
        public string USERNAME { get; set; }

        public HeadADO() { }

        public HeadADO(ACS.EFMODEL.DataModels.ACS_USER data)
        {
            if (data != null)
            {
                this.LOGINNAME = data.LOGINNAME;
                this.USERNAME = data.USERNAME;
            }
        }
    }
}
