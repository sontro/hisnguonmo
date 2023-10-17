using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisTrackingTDO
    {
        public long TrackingId { get; set; }

        /// <summary>
        /// Thoi gian to dieu tri yyyyMMddHHmmss
        /// </summary>
        public long TrackingTime { get; set; }

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
        /// phuong phap xu ly
        /// </summary>
        public string MedicalInstruction { get; set; }

        /// <summary>
        /// Dien bien
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Dien bien CLS
        /// </summary>
        public string SubclinicalProcesses { get; set; }

        /// <summary>
        /// Theo doi - cham soc
        /// </summary>
        public string CareInstruction { get; set; }

        /// <summary>
        /// Bieu hien chung
        /// </summary>
        public string GeneralExpression { get; set; }

        /// <summary>
        /// Stt cua to dieu tri
        /// </summary>
        public long? SheetOrder { get; set; }

        /// <summary>
        /// Tai khoan tao
        /// </summary>
        public string CreateLoginname { get; set; }

        /// <summary>
        /// Dien bien phuc hoi chuc nang
        /// </summary>
        public string RehabilitationContent { get; set; }

        /// <summary>
        /// Danh sach y lenh
        /// </summary>
        public List<HisServiceReqTDO> ServiceReqs { get; set; }
    }
}
