using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HipoFinanceReportSDO
    {
        /// <summary>
        /// Doanh thu
        /// </summary>
        public decimal? dt { get; set; }

        /// <summary>
        /// Doanh thu viện phí
        /// </summary>
        public decimal? dt_vp { get; set; }

        /// <summary>
        /// Doanh thu bảo hiểm (dt - dt_vp)
        /// </summary>
        public decimal? dt_bh { get; set; }

        /// <summary>
        /// Doanh thu dịch vụ
        /// </summary>
        public decimal? dt_dv { get; set; }

        /// <summary>
        /// Doanh thu tạm ứng
        /// </summary>
        public decimal? dt_tu { get; set; }
    }
}
