using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTreatmentSetTranPatiBookSDO
    {
        public long TreatmentId { get; set; }
        public long TranPatiBookTime { get; set; }
        public string TranPatiDoctorLoginname { get; set; }
        public string TranPatiDoctorUsername { get; set; }
        public string TranPatiDepartmentLoginname { get; set; }
        public string TranPatiDepartmentUsername { get; set; }
        public string TranPatiHospitalLoginname { get; set; }
        public string TranPatiHospitalUsername { get; set; }
    }
}
