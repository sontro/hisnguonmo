using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineTypeCreate.ADO
{
    class SourceMedicineADO
    {
        public int ID { get; set; }
        public string SOURCE_MEDICINE { get; set; }

        public SourceMedicineADO(int _id, string _sourceMedicine)
        {
            this.ID = _id;
            this.SOURCE_MEDICINE = _sourceMedicine;
        }
    }
}
