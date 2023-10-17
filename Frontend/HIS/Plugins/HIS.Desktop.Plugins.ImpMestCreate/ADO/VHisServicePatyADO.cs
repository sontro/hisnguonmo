using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.ADO
{
    public class VHisServicePatyADO : V_HIS_SERVICE_PATY
    {
        //public HIS_MEDICINE_PATY HisMedicinePaty { get; set; }
        //public HIS_MATERIAL_PATY HisMaterialPaty { get; set; }
        public bool IsReusable { get; set; }
        public decimal ExpVatRatio { get; set; }
        public decimal PercentProfit { get; set; }
        public decimal ExpPriceVat { get; set; }
        public bool IsNotSell { get; set; }
        public bool IsNotEdit { get; set; }
        public long ServiceId { get; set; }
        public long ServiceTypeId { get; set; }
        public decimal ExpPrice { get; set; }
        public decimal? PRE_PRICE_Str { get; set; }
        public bool IsSetExpPrice { get; set; }
        public VHisServicePatyADO() { }

    }
}
