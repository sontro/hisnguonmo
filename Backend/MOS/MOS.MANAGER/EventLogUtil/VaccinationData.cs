using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    class VaccinationData
    {
        public string VaccinationCode { get; set; }
        public string ExpMestCode { get; set; }
        public string RequestRoomName { get; set; }
        public string ExecuteRoomName { get; set; }

        public List<string> MedicineTypeNames { get; set; }

        public VaccinationData()
        {
        }

        public VaccinationData(string vaccinationCode, List<string> medicineTypeNames, string requestRoomName, string executeRoomName)
        {
            this.VaccinationCode = vaccinationCode;
            this.MedicineTypeNames = medicineTypeNames;
            this.RequestRoomName = requestRoomName;
            this.ExecuteRoomName = executeRoomName;
        }

        public override string ToString()
        {
            string medicineTypeName = this.MedicineTypeNames != null ?
                string.Join(",", this.MedicineTypeNames) : "";
            string vcCode = string.IsNullOrWhiteSpace(this.VaccinationCode) ?
                "" : string.Format("{0}: {1}", SimpleEventKey.VACCINATION_CODE, this.VaccinationCode);
            string expCode = string.IsNullOrWhiteSpace(this.ExpMestCode) ?
                "" : string.Format("{0}: {1}", SimpleEventKey.EXP_MEST_CODE, this.ExpMestCode);
            return string.Format("{0} {1}. {2} ==> {3}. ({4})", vcCode, expCode, this.RequestRoomName, this.ExecuteRoomName, medicineTypeName);
        }
    }
}
