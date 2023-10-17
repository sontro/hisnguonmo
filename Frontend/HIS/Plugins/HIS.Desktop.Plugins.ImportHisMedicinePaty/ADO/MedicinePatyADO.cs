using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportHisMedicinePaty.ADO
{
    class MedicinePatyADO : MOS.EFMODEL.DataModels.HIS_MEDICINE_PATY
    {
        public string ERROR { get; set; }
        public string MAX_CAPACITY_STR { get; set; }

        public string PATIENT_TYPE_CODE { get; set; }

        public string PATIENT_TYPE_NAME { get; set; }

        public string MEDICINE_TYPE_CODE { get; set; }

        public string PACKAGE_NUMBER { get; set; }

        public string SUPPLIER_CODE { get; set; }

        public long SUPPLIER_ID { get; set; }

        public long MEDICINE_TYPE_ID { get; set; }

        public string MEDICINE_TYPE_NAME { get; set; }

        public string SUPPLIER_NAME { get; set; }
    }
}
