using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class PharmacyTransExpSDO
    {
        public string AccountBookName {get;set;}
        public string TdlPatientGenderName {get;set;}
        public string BuyerTaxCode {get;set;}
        public long? AccountBookId {get;set;}
        public long? BillTypeId {get;set;}
        public string PayFormName {get;set;}
        public long? TransactionDate {get;set;}
    }
}
