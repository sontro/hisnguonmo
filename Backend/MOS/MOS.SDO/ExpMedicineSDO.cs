using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ExpMedicineSDO
    {
        public long MedicineId { get; set; }
        public long? PatientTypeId { get; set; }
        public long? NumOrder { get; set; }
        public long? NumOfDays { get; set; }
        public decimal Amount { get; set; }
        public decimal? Price { get; set; }
        public decimal? VatRatio { get; set; }
        public decimal? DiscountRatio { get; set; }
        public string Description { get; set; }
        public string Tutorial { get; set; }
        //trong truong hop sua lai phieu xuat ==> y.c tach bean se co thong tin cua exp_mest_medicine_id cu~
        public List<long> ExpMestMedicineIds { get; set; }
    }
}
