using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentList.ADO
{
    public class KieuBenhNhanADO
    {
        public long ID { get; set; }
        public string KieuBenhNhan { get; set; }
        public KieuBenhNhanADO(long _ID, string _KieuBenhNhan)
        {
            this.ID = _ID;
            this.KieuBenhNhan = _KieuBenhNhan;
        }
    }
}
