using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisTestConfirmNoExcuteTDO
    {
        public string ServiceReqCode { get; set; }
        public List<HisTestServiceTypeTDO> TestServiceTypeList { get; set; }
    }
}
