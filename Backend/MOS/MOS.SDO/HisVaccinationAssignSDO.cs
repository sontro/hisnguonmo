using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class VaccinationMetySDO
    {
        public long MedicineTypeId { get; set; }
        public long MediStockId { get; set; }
        public long PatientTypeId { get; set; }
        public decimal Amount { get; set; }
        public long? VaccineTurn { get; set; } 
    }

    public class HisVaccinationAssignSDO
    {
        /// <summary>
        /// Chi truyen trong truong hop update
        /// </summary>
        public long? VaccinationId { get; set; }
        public long VaccinationExamId { get; set; }
        public long WorkingRoomId { get; set; }
        public long RequestTime { get; set; }
        public string RequestLoginname { get; set; }
        public string RequestUsername { get; set; }
        public List<VaccinationMetySDO> VaccinationMeties { get; set; }
    }
}
