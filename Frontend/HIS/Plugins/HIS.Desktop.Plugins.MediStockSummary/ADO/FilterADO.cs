using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockSummary.ADO
{
    public class FilterADO
    {
        //Bắt buộc phải truyền vào kho và thuốc
        public long MEDI_STOCK_ID { get; set; }
        public long MEDICINE_TYPE_ID { get; set; }
        public long MATERIAL_TYPE_ID { get; set; }
        public long BLOOD_TYPE_ID { get; set; }
        public long? MediMateId { get; set; }
        
        public long TIME_FROM { get; set; }
        public long TIME_TO { get; set; }
    }
}
