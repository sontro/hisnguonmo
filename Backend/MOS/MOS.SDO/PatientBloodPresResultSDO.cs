using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class PatientBloodPresResultSDO
    {
        public HIS_SERVICE_REQ ServiceReq { get; set; }
        public List<HIS_EXP_MEST_BLTY_REQ> Bloods { get; set; }
        public HIS_EXP_MEST ExpMest { get; set; }
    }
}
