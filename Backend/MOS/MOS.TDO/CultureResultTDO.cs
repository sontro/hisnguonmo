using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class CultureResultTDO
    {
        public string ServiceReqCode { get; set; }
        public List<CultureDetailTDO> TestIndexDatas { get; set; }
    }
}
