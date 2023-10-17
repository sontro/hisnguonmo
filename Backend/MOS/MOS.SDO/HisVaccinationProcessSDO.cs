using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    /// <summary>
    /// Thong tin tiem vaccin
    /// </summary>
    public class HisVaccinationInjectionSDO
    {
        public long ExpMestMedicineId { get; set; }
        public long VaccinationResultId { get; set; }
    }

    /// <summary>
    /// Thong tin xu ly tiem vaccin
    /// </summary>
    public class HisVaccinationProcessSDO
    {
        public long VaccinationId { get; set; }
        public long WorkingRoomId { get; set; }
        public long ExecuteTime { get; set; }
        public string ExecuteLoginname { get; set; }
        public string ExecuteUsername { get; set; }
        public List<HisVaccinationInjectionSDO> VaccinationInjections { get; set; }
    }
}
