using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarReportType
{
    class StatusHour
    {
        public string statusCode { get; set; }
        public string statusName { get; set; }

        public StatusHour(string statusCode, string statusName)
        {
            this.statusCode = statusCode;
            this.statusName = statusName;
        }
    }
}
