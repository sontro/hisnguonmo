using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqList.ADO
{
    class FilterTypeADO
    {
        public long ID { get; set; }
        public string FilterTypeName { get; set; }

        public FilterTypeADO(long id, string filterTypeName)
        {
            this.ID = id;
            this.FilterTypeName = filterTypeName;
        }
    }
}
