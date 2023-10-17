using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockPeriod.ADO
{
    public class HisMestPeriodBloodAdo : HIS_MEST_PERIOD_BLOOD
    {
        public decimal? KK_AMOUNT { get; set; }    
      
        public decimal BEGIN_AMOUNT { get; set; }
        public string BLOOD_TYPE_CODE { get; set; }
        public long BLOOD_TYPE_ID { get; set; }
        public string BLOOD_TYPE_NAME { get; set; } 
        public decimal IN_AMOUNT { get; set; }
        public decimal? INVENTORY_AMOUNT { get; set; }  
        public string MEDI_STOCK_PERIOD_NAME { get; set; }
        public long? MEDI_STOCK_PERIOD_TIME { get; set; }
        public decimal OUT_AMOUNT { get; set; }
        public string PACKING_TYPE_NAME { get; set; }
        public string SERVICE_UNIT_CODE { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public long? TO_TIME { get; set; }
        public decimal? VIR_END_AMOUNT { get; set; }
        public decimal? VOLUME { get; set; }
        public string PACKAGE_NUMBER { get; set; }
        public long? EXPIRED_DATE { get; set; }

        public HisMestPeriodBloodAdo() { }
        public HisMestPeriodBloodAdo(HIS_MEST_PERIOD_BLOOD data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<HisMestPeriodBloodAdo>(this, data);
            }
        }
    }
}
