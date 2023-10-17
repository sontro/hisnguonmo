using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class TakeBeanByMameSDO
    {
        public List<long> ExpMestDetailIds { get; set; } //exp_mest_medicine_id/exp_mest_material_id
        /// <summary>
        /// medicine_id hoac material_id
        /// </summary>
        public long MameId { get; set; }
        public long MediStockId { get; set; }
        public long? PatientTypeId { get; set; }
        public decimal Amount { get; set; }
        public string ClientSessionKey { get; set; }
        public List<long> BeanIds { get; set; }
    }
}
