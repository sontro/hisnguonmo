using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class AntibioticMapResultTDO
    {
        public string ServiceReqCode { get; set; }
        public List<AntibioticMapDetailTDO> TestIndexDatas { get; set; }
    }
}
