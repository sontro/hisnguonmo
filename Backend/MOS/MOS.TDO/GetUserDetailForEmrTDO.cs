using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class GetUserDetailForEmrTDO
    {
        public long ID { get; set; }
        public string LOGINNAME { get; set; }
        public string USERNAME { get; set; }
        public string DIPLOMA { get; set; }
        public bool IS_ADMIN { get; set; }
        public bool IS_ACTIVE { get; set; }
    }
}
