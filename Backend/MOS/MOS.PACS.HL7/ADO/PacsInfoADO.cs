using MedilinkHL7.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.PACS.HL7.ADO
{
    class PacsInfoADO
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public HL7PACS HL7Message { get; set; }
        public string AccessNumber { get; set; }
    }
}
