using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.ADO
{
    public class ConfigADO
    {
        public long ID { get; set; }
        public string NAME { get; set; }
        public bool IsChecked { get; set; }
        public enum RowConfigID
        {
            KhongHienThiDonKhongLayODonThuocTH = 1
        }
    }
}
