using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceReqCalendarSDO
    {
        public List<long> ServiceReqIds { get; set; }
        public long WorkingRoomId { get; set; }
        /// <summary>
        /// Trong truong hop remove ra khoi lich thi ko can truyen truong nay vao
        /// </summary>
        public long? PtttCalendarId { get; set; }
    }
}