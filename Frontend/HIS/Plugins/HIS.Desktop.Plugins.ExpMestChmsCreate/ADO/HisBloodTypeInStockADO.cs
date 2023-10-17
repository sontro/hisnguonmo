using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestChmsCreate.ADO
{
    public class HisBloodTypeInStockADO : HisBloodTypeInStockSDO
    {
        public bool IsCheck { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public HisBloodTypeInStockADO()
        { }

        public HisBloodTypeInStockADO(HisBloodTypeInStockSDO item)
        {
            if (item != null)
            {
                this.Amount = item.Amount;
                this.BloodTypeCode = item.BloodTypeCode;
                this.BloodTypeHeinName = item.BloodTypeHeinName;
                this.BloodTypeName = item.BloodTypeName;
                this.Id = item.Id;
                this.IsActive = item.IsActive;
                this.IsLeaf = item.IsLeaf;
                this.MediStockId = item.MediStockId;
                this.NumOrder = item.NumOrder;
                this.ParentId = item.ParentId;
                this.ServiceId = item.ServiceId;
                this.Volume = item.Volume;
                this.MEDI_STOCK_ID = item.MediStockId;
            }
        }
    }
}
