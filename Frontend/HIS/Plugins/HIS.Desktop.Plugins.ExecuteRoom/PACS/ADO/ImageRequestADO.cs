using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExecuteRoom.PACS.ADO
{
    class ImageRequestADO
    {
        public ImageRequestADO() { }
        /// <summary>
        /// Service_req_code(10) + "-" + service_code(6) //max 16
        /// </summary>
        public string SoPhieu { get; set; }
    }
}
