using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RehaServiceReqExecute.ADO
{
    public class RehaTrainADO : MOS.EFMODEL.DataModels.V_HIS_REHA_TRAIN
    {
        public int Action { get; set; }
        public DateTime? TRAIN_TIME_EXT { get; set; }
        public long REHA_SERVICE_TYPE_ID { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
    }
}
