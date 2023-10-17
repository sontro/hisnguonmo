using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class PharmacyCashierExpInvoiceSDO
    {
        //Thong tin ke don
        public long WorkingRoomId { get; set; }

        public long ExpMestId { get; set; }
        /// <summary>
        /// Phong thu ngan
        /// </summary>
        public long CashierRoomId { get; set; }
        /// <summary>
        /// So hoa don (hoa don dich vu: bill_type_id = 2)
        /// </summary>
        public long InvoiceAccountBookId { get; set; }
        /// <summary>
        /// So chung tu cua hoa don (trong truong hop so ko tu sinh so)
        /// </summary>
        public long? InvoiceNumOrder { get; set; }
        public long PayFormId { get; set; }
    }
}
