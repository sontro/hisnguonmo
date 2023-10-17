using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.OtherImpMestUpdate.ADO
{
    class VHisServicePatyADO : V_HIS_SERVICE_PATY
    {
        public decimal ExpVatRatio { get; set; }
        public bool IsNotSell { get; set; }
        public bool IsNotEdit { get; set; }
        public long ServiceId { get; set; }
        public long ServiceTypeId { get; set; }

        public bool IsSetExpPrice { get; set; }
    }
}
