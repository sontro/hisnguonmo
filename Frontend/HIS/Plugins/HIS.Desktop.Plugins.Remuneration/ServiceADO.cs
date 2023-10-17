using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Remuneration
{
    public class ServiceADO : V_HIS_SERVICE
    {
        public ServiceADO() { }
        public ServiceADO(V_HIS_SERVICE data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<ServiceADO>(this, data);
            }
        }

        public bool check2 { get; set; }
        public bool isKeyChoose1 { get; set; }
        public bool radio2 { get; set; }
        public decimal PRICE { get; set; }
    }
}
