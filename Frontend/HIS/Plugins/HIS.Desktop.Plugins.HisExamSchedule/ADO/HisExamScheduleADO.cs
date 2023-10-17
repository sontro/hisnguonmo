using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisExamSchedule.ADO
{
    class HisExamScheduleADO
    {
        public long ID { get; set; }
        public string ROOM { get; set; }
        public long ROOM_ID { get; set; }
        public long DEPARTMENT_ID { get; set; }
        public string THU2 { get; set; }
        public string THU3 { get; set; }
        public string THU4 { get; set; }
        public string THU5 { get; set; }
        public string THU6 { get; set; }
        public string THU7 { get; set; }
        public string CHUNHAT { get; set; }

        public HisExamScheduleADO() { }
    }
}
