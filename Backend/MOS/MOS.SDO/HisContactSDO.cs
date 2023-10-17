using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisContactSDO : HIS_CONTACT_POINT
    {
        //Id cua nguoi benh
        public long ContactPointId { get; set; }
        public long ContactTime { get; set; }
        public string ContactPlace { get; set; }
    }
}
