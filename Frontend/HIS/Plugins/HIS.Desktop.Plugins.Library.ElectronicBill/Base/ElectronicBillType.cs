using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.Base
{
    public class ElectronicBillType
    {
        public enum ENUM
        { 
            CREATE_INVOICE,
            GET_INVOICE_LINK,
            DELETE_INVOICE,
            CANCEL_INVOICE,
            CONVERT_INVOICE,
            CREATE_INVOICE_DATA,
            GET_INVOICE_INFO,
            GET_INVOICE_SHOW
            //ImportAndPublishInv,
            //downloadInvPDFFkeyNoPay
        }
    }
}
