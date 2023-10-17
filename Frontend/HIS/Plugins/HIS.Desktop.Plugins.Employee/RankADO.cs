using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Employee
{
    public class RankADO
    {
        public long ID{get;set;}
        public string RANK {get;set;}
        public RankADO(long id)
        {
            this.ID = id;
            this.RANK = id.ToString();
        }
    }
}
