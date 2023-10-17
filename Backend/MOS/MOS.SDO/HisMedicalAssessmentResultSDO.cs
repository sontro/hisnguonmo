using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMedicalAssessmentResultSDO
    {
        public V_HIS_MEDICAL_ASSESSMENT vHisMedicalAssessment { get; set; }
        public List<HIS_ASSESSMENT_MEMBER> HisAssessmentMember { get; set; }
    }
}
