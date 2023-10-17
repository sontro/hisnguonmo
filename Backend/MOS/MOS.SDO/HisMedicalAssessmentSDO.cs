using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMedicalAssessmentSDO
    {
        public HIS_MEDICAL_ASSESSMENT HisMedicalAssessement { get; set; }
        public List<HIS_ASSESSMENT_MEMBER> HisAssessmentMembers { get; set; }
        public long? TreatmentId { get; set; }
    }
}
