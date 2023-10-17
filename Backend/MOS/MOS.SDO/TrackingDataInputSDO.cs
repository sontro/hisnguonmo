using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class TrackingDataInputSDO
    {
        public long TreatmentId { get; set; }
        public bool? IncludeMaterial { get; set; } //có lấy dữ liệu vật tư hay không
        public bool? IncludeMoveBackMediMate { get; set; } //có lấy thuốc/vật tư thu hồi hay không
        public bool? IncludeBloodPres { get; set; } // có lấy dữ liệu máu hay không
    }
}
