using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisServiceReqTDO
    {
        /// <summary>
        /// Ma Y lenh
        /// </summary>
        public string ServiceReqCode { get; set; }

        /// <summary>
        /// Ma dieu tri
        /// </summary>
        public string TreatmentCode { get; set; }

        /// <summary>
        /// Thoi gian y lenh yyyyMMddHHmmss
        /// </summary>
        public long InstructionTime { get; set; }

        /// <summary>
        /// Ngay y lenh yyyyMMdd000000
        /// </summary>
        public long InstructionDate { get; set; }

        /// <summary>
        /// Loai y lenh
        /// </summary>
        public long ServiceReqTypeId { get; set; }

        /// <summary>
        /// Ma benh chinh
        /// </summary>
        public string IcdCode { get; set; }

        /// <summary>
        /// Ten benh chinh
        /// </summary>
        public string IcdName { get; set; }

        /// <summary>
        /// Ma benh phu
        /// </summary>
        public string IcdSubCode { get; set; }

        /// <summary>
        /// Ten benh phu
        /// </summary>
        public string IcdText { get; set; }
        
        /// <summary>
        /// Mo ta
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ghi chu
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Danh sach dich vu duoc chi dinh trong y lenh
        /// </summary>
        public List<HisSereServTDO> Services { get; set; }
    }
}
