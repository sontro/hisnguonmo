using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class OddMaterialTypeSDO
    {
        public long MaterialTypeId { get; set; }
        public decimal Amount { get; set; }

        public OddMaterialTypeSDO()
        {

        }

        public OddMaterialTypeSDO(long materialTypeId, decimal amount)
        {
            this.MaterialTypeId = materialTypeId;
            this.Amount = amount;
        }
    }
}
