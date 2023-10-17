using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisBranchWebSDO
    {
        public HIS_BRANCH Branch { get; set; }
        public byte[] ImageData { get; set; }

        public string AppCode { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
