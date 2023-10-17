using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTreatmentCounterSDO
    {
        /// <summary>
        /// Tổng bệnh nhân tiếp đón
        /// </summary>
        public long? bntd { get; set; }

        /// <summary>
        /// Bệnh nhân tiếp đón bảo hiểm
        /// </summary>
        public long? bntd_bh { get; set; }

        /// <summary>
        /// Tổng bệnh nhân khám thực tế
        /// </summary>
        public long? bnktt { get; set; }

        /// <summary>
        /// Bệnh nhân khám thực tế bảo hiểm
        /// </summary>
        public long? bnktt_bh { get; set; }

        /// <summary>
        /// Tổng bệnh nhân nhập viện nội trú
        /// </summary>
        public long? bnnv { get; set; }

        /// <summary>
        /// Bệnh nhân nhập viện nội trú bảo hiểm
        /// </summary>
        public long? bnnv_bh { get; set; }

        /// <summary>
        /// Tổng bệnh nhân cấp toa cho về
        /// </summary>
        public long? bnct { get; set; }

        /// <summary>
        /// Bệnh nhân cấp toa cho về bảo hiểm
        /// </summary>
        public long? bnct_bh { get; set; }

        /// <summary>
        /// Tổng bệnh nhân chuyển viện
        /// </summary>
        public long? bncv { get; set; }

        /// <summary>
        /// Bệnh nhân chuyển viện bảo hiểm
        /// </summary>
        public long? bncv_bh { get; set; }
    }
}
