using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class DrugInterventionInfoTDO
    {
        public DrugPatientInfoTDO Patient { get; set; }
        public DrugInfoValueItemTDO Pharmacist { get; set; }
        public DrugInfoValueItemTDO Doctor { get; set; }
        public bool IsUrgent { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? PrescriptionTime { get; set; }
        public string SessionID { get; set; }
    }

    public class DrugInfoValueItemTDO
    {
        public string id { get; set; }
        public string value { get; set; }
    }

    public class DrugPatientInfoTDO
    {
        public string patientID { get; set; }
        public string maYTe { get; set; }
        public string prescriptionId { get; set; }
        public int? height { get; set; }
        public decimal? weight { get; set; }
        public string icd { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public DrugInfoValueItemTDO prescriber { get; set; }
        public DateTime? prescriptionTime { get; set; }
        public DateTime? dob { get; set; }
        public DateTime? examinationDate { get; set; }
        public string maKhoa { get; set; }
        public string tenKhoa { get; set; }
        public DrugEServiceType serviceType { get; set; }
        public string phongKham { get; set; }
        public string maDoiTuong { get; set; }
    }

    public enum DrugEServiceType
    {
        Undefined = 0,
        Outpatient = 1,
        Inpatient = 2
    }
}
