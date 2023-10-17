using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServicePatyList.entity
{
    public class StatusHour
    {
        public long id { get; set; }
        public string statusCode { get; set; }
        public string statusName { get; set; }

        public StatusHour(long id,string statusCode, string statusName)
        {
            this.id = id;
            this.statusCode = statusCode;
            this.statusName = statusName;
        }
    }
}
