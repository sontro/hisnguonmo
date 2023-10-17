using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisVaccReactInfoSDO
    {
        public long VaccinationId { get; set; }
        public long VaccinationReactId { get; set; }
        public long? VaccHealthSttId { get; set; }
        public long ReactTime { get; set; }
        public string PathologicalHistory { get; set; }
        public long? DeathTime { get; set; }
        public string FollowLoginname { get; set; }
        public string FollowUsername { get; set; }
        public bool? IsReactResponse { get; set; }
        public string ReactResponser { get; set; }
        public string ReactReporter { get; set; }
        public List<HIS_VACCINATION_VRTY> HisVaccinationVrtys { get; set; }
        public List<HIS_VACCINATION_VRPL> HisVaccinationVrpls { get; set; }
    }
}
