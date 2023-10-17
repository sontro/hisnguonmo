using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestUserSDO
    {
        public long ExpMestId { get; set; }
        public List<HIS_EXP_MEST_USER> ExpMestUsers { get; set; }
    }
}
