using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPatientUpdateSDO
    {
        public HIS_PATIENT HisPatient { get; set; }
        public bool UpdateTreatment { get; set; }
        public long? TreatmentId { get; set; }
        public byte[] ImgBhytData { get; set; }
        public byte[] ImgAvatarData { get; set; }
        public bool IsNotUpdateImage { get; set; }
        public bool IsUpdateEmr { get; set; }
        public bool IsUpdateVaccinationExam { get; set; }
    }
}
