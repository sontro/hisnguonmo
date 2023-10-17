using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisTreatmentInvoiceInfoTDO
    {
        public string TreatmentCode { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public long Dob { get; set; }
        public string Address { get; set; }
        public string OrganizationName { get; set; }
        public string TaxCode { get; set; }
        public decimal PatientPrice { get; set; }
        public decimal PatientPriceByBhyt { get; set; }
        public decimal PatientPriceByDifference { get; set; }
    }
}
