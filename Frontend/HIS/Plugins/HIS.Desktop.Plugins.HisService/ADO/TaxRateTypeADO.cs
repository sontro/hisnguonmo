using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisService.ADO
{
    public class TaxRateTypeADO
    {
        public TaxRateTypeADO()
        { }

        public long ID { get; set; }
        public string NAME { get; set; }

        public TaxRateTypeADO(long id, string name)
        {
            this.ID = id;
            this.NAME = name;
        }
        
    }
}
