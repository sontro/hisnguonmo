using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.Data
{
    public class SignInitData
    {
        public Base.DelegateSignAndRelease SignAndRelease { get; set; }
        public string ContentSign { get; set; }
        public byte[] fileToBytes { get; set; }
        public string FileDownload { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
