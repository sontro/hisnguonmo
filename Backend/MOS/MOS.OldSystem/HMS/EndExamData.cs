using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OldSystem.HMS
{
    public class EndExamData
    {
        public string TreatmentCode { get; set; }
        public string OutTime { get; set; }
        public SuggestionEnum Suggestion { get; set; }
        public ConclusionEnum Conclusion { get; set; }
        public string TreatmentDepartmentCode { get; set; }
        public string NextRoomCode { get; set; }
        public string IcdCode { get; set; }
        public string TransferOrgCode { get; set; }
        public string LoginName { get; set; }
    }
}
