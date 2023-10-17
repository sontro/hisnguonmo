using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DispenseMedicine.ADO
{
    public class MedicinePatyADO
    {
        public long Id { get; set; } 
        public long PatientTypeId { get; set; }
        public string PatientTypeName { get; set; }
        public decimal? Price { get; set; }
        public decimal? Vat { get; set; }
        public long MedicineId { get; set; }
    }
}
