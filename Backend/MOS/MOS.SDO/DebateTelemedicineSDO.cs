using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class DebateTelemedicineSDO
    {
        /// <summary>
        /// Bắt buộc - Id hội chuẩn
        /// </summary>
        public long DebateId { get; set; }
        /// <summary>
        /// Không bắt buộc - Id trên hệ thống TMP - Telemedicine
        /// </summary>
        public string TmpId { get; set; }
    }
}
