using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignService.ADO
{
    internal class ShareCountADO
    {
        internal ShareCountADO()
        {
        }
        internal ShareCountADO(long? shareCount)
        {
            this.ShareCount = shareCount;
        }
        public long? ShareCount { get; set; }
    }
}
