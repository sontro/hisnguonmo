using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.ADO
{
    public class MedicineAcinInteractiveWarnADO 
    {
        public MedicineAcinInteractiveWarnADO() { }
        public string Key { get; set; }
        public long? GRADE { get; set; }
        public V_HIS_ACIN_INTERACTIVE B { get; set; }
        public V_HIS_MEDICINE_TYPE_ACIN A { get; set; }
        public V_HIS_MEDICINE_TYPE_ACIN Item { get; set; }
        public List<V_HIS_ACIN_INTERACTIVE> AcinInteractives { get; set; }
    }
}
