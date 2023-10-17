
using System.Collections.Generic;
namespace HIS.Desktop.ADO
{
    public class OtherFormAssTreatmentInputADO
    {
        public long TreatmentId { get; set; }
        public string PrintTypeCode { get; set; }
        public Dictionary<string, object> DicParam { get; set; }

        public OtherFormAssTreatmentInputADO() { }
    }
}
