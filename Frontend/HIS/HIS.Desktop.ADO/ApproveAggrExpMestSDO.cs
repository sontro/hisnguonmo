using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class ApproveAggrExpMestSDO
    {
        public long expMestId { get; set; }
        public long expMestStt { get; set; }

        public ApproveAggrExpMestSDO(long expMestId, long ExpMestStt)
        {
            this.expMestId = expMestId;
            this.expMestStt = ExpMestStt;
        }

    }
}
