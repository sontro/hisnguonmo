using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ExpMaterialSDO
    {
        public long MaterialId { get; set; }
        public long? NumOrder { get; set; }
        public decimal Amount { get; set; }
        public long? PatientTypeId { get; set; }
        public decimal? Price { get; set; }
        public decimal? VatRatio { get; set; }
        public decimal? DiscountRatio { get; set; }
        public string Description { get; set; }
        //trong truong hop sua phieu xuat thi truyen len exp_mest_material_id cua phieu xuat cu~
        public List<long> ExpMestMaterialIds { get; set; }
    }
}
