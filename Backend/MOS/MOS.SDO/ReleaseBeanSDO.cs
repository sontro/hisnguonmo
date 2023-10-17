using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ReleaseBeanSDO
    {
        public long TypeId { get; set; } //medicine_type_id hoac material_type_id
        /// <summary>
        /// material_id hoac medicine_id
        /// </summary>
        public long? MameId { get; set; }
        public long MediStockId { get; set; }
        public string ClientSessionKey { get; set; }
        public List<long> BeanIds { get; set; }
    }
}
