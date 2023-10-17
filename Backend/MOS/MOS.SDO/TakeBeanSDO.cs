using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class TakeBeanSDO
    {
        public List<long> ExpMestDetailIds { get; set; } //exp_mest_medicine_id/exp_mest_material_id
        public long TypeId { get; set; } //medicine_type_id hoac material_type_id
        public long MediStockId { get; set; }
        public long? PatientTypeId { get; set; }
        public decimal Amount { get; set; }
        public string ClientSessionKey { get; set; }
        public List<long> BeanIds { get; set; }
        public long? ExpiredDate { get; set; }
    }
}
