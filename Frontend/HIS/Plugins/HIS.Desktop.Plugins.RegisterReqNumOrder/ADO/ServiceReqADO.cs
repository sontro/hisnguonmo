using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterReqNumOrder
{
    internal class ServiceReqADO : V_HIS_REGISTER_REQ
    {
        public decimal? countTimer { get; set; }
    }
}
