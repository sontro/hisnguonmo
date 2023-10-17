using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class QrCodeItemPaymentTDO
    {
        /// <summary>
        /// Ma san pham
        /// </summary>
        public string productId { get; set; }
        /// <summary>
        /// Don gia 
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// Tien Tip/Phi
        /// </summary>
        public string tipAndFee { get; set; }
        /// <summary>
        /// Ma tien te
        /// </summary>
        public string ccy { get; set; }
        /// <summary>
        /// So luong
        /// </summary>
        public string qty { get; set; }
        /// <summary>
        /// Ghi chu
        /// </summary>
        public string note { get; set; }
    }
}
