using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockInventory.ADO
{
    public class VHisExpMestMedicineADO : HisMedicineInStockSDO
    {
        public decimal MOBA_AMOUNT { get; set; }
        public bool IsMoba { get; set; }
        public decimal CAN_MOBA_AMOUNT { get; set; }
        public decimal? VIR_TOTAL_IMP_PRICE { get; set; }
        public string PARENT_ID { get; set; }
        public string CHILD_ID { get; set; }
        public string EXPIRED_DATE_STR { get; set; }

        public VHisExpMestMedicineADO()
        {
        }

        public VHisExpMestMedicineADO(HisMedicineInStockSDO data)
        {
            try
            {
                if (data != null)
                {
                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<HisMedicineInStockSDO>();
                    foreach (var item in pi)
                    {
                        item.SetValue(this, item.GetValue(data));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
