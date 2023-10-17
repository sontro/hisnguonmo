using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPatientVitaminASDO
    {
        public HIS_PATIENT HisPatient { get; set; }
        public HIS_VITAMIN_A HisVitaminA { get; set; }
        public HIS_VACCINATION_EXAM HisVaccinationExam { get; set; }
        public HIS_DHST HisDhst { get; set; }
        public long RequestRoomId { get; set; }
        public string CardCode { get; set; }
        public byte[] ImgBhytData { get; set; }
        public byte[] ImgAvatarData { get; set; }
    }
}
