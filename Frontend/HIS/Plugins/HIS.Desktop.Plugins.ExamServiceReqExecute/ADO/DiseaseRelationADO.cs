using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExamServiceReqExecute.ADO
{
    public class DiseaseRelationADO : MOS.EFMODEL.DataModels.V_HIS_EXAM_SERE_DIRE
    {
        public bool CheckState { get; set; }
        public string LblMonth { get; set; }
    }
}
