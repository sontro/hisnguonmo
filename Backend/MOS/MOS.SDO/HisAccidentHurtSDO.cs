using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisAccidentHurtSDO
    {
        public long? Id { get; set; }

        public long ExecuteRoomId { get; set; }
        public long TreatmentId { get; set; }
        public string Content { get; set; }
        public long AccidentHurtTypeId { get; set; }
        public Nullable<long> AccidentBodyPartId { get; set; }
        public Nullable<long> AccidentLocationId { get; set; }
        public Nullable<long> AccidentPoisonId { get; set; }
        public Nullable<long> AccidentVehicleId { get; set; }
        public Nullable<long> AccidentHelmetId { get; set; }
        public Nullable<long> AccidentResultId { get; set; }
        public Nullable<long> AccidentCareId { get; set; }
        public Nullable<long> AccidentTime { get; set; }
        public bool IsUseAlcohol { get; set; }

        public string StatusIn { get; set; }
        public string StatusOut { get; set; }
        public string TreatmentInfo { get; set; }
        public bool AlcoholTestResult { get; set; }
        public bool NarcoticsTestResult { get; set; }

        public string HospitalizationReason { get; set; }

        public long? CccdDate { get; set; }
        public string CccdNumber { get; set; }
        public string CccdPlace { get; set; }

        public long? CmndDate { get; set; }
        public string CmndNumber { get; set; }
        public string CmndPlace { get; set; }
        public string AccidentHurtIcdCode { get; set; }
        public string AccidentHurtIcdName { get; set; }
        public string AccidentHurtIcdSubCode { get; set; }
        public string AccidentHurtIcdText { get; set; }
    }
}
