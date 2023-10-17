using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockSummaryByExpireDate.ADO
{
    public class MyHisMediInStockSDO : MOS.SDO.HisMedicineInStockSDO
    {
        public string MY_ADO_ID { get; set; }
        public string MY_PARENT_ID { get; set; }

        public MyHisMediInStockSDO()
        {

        }

        public MyHisMediInStockSDO(MOS.SDO.HisMedicineInStockSDO data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<MOS.SDO.HisMedicineInStockSDO>(this, data);
        }
    }
}
