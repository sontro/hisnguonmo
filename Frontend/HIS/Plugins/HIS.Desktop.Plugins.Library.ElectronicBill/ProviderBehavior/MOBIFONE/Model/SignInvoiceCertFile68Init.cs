using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProviderBehavior.MOBIFONE.Model
{
    public class SignInvoiceCertFile68Init
    {
        public List<SignInvoiceCertFile68Data> data { get; set; }
    }
    public class SignInvoiceCertFile68Data
    {
        public string branch_code { get; set; }
        public string username { get; set; }
        public List<string> lsthdon_id { get; set; }
        public string cer_serial { get; set; }
        public string type_cmd { get; set; }
        public string is_api { get; set; }
    }
    public enum TypeCmd
    {
        HasCode = 200,
        NoCode = 203
    }
}
