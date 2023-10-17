using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class UpdateResponsibleUserSDO
    {
        public long RoomId { get; set; }
        public string ResponsibleLoginName { get; set; }
        public string ResponsibleUserName { get; set; }
    }
}
