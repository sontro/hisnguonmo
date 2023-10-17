using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.Template
{
    class TemplateDetailADO
    {
        public string Display { get; set; }
        public string Unit { get; set; }
        public string ServiceCodes { get; set; }
        public string ParentServiceCodes { get; set; }
        public string ServiceTypeCodes { get; set; }
        public string HeinServiceTypeCodes { get; set; }
        public string TreatmentTypeCodes { get; set; }
        public string PatientTypeCodes { get; set; }

        public long? IsSplit { get; set; }
        public long? IsBHYT { get; set; }
        public long? NumOrder { get; set; }
        public long? IsSplitBhytPrice { get; set; }
        public long? Stt { get; set; }

        public List<long> ParentServiceIds { get; set; }
        public List<long> ServiceTypeIds { get; set; }
        public List<long> HeinServiceTypeIds { get; set; }
        public List<long> TreatmentTypeIds { get; set; }
        public List<long> PatientTypeIds { get; set; }
    }
}
