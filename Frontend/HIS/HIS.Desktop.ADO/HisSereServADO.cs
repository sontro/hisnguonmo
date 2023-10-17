using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class HisSereServADO : MOS.EFMODEL.DataModels.V_HIS_SERE_SERV
    {
        public int Action { get; set; }
        public bool? IsExpend { get; set; }
        public bool? IsKHBHYT { get; set; }
        public bool? IsOutKtcFee { get; set; }
        public string ExamServiceTypeName { get; set; }
        public string ExamRoomName { get; set; }
        public string ExamPatientTypeName { get; set; }
        public string PARENT_NAME { get; set; }
        public List<MOS.EFMODEL.DataModels.V_HIS_SERVICE_ROOM> Rooms { get; set; }
        public bool IsAssignDay { get; set; }
        public long serviceType { get; set; }
        public bool checkService { get; set; }

    }
}
