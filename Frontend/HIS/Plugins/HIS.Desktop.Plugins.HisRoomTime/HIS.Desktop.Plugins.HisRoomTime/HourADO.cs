using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisRoomTime
{
   public class HourADO
    {
       public int Hour { get; set; }
       public string HourString { get; set; }
       public string HourName { get; set; }

       public HourADO(int Hour, string HourString, string HourName)
       {
           this.Hour = Hour;
           this.HourString = HourString;
           this.HourName = HourName;
       }
    }
}
