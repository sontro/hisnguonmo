using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.Roche
{
    public class RocheAstmPatientData
    {
        public string PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public RocheAstmGender Gender { get; set; }
        public string Commune { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string HeinCardNumber { get; set; }
    }
}
