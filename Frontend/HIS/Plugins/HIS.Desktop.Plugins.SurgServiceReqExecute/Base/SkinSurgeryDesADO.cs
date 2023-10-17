using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Base
{
    public class SkinSurgeryDesADO : HIS_SKIN_SURGERY_DESC
    {
        public long? SURGERY_POSITION_ID { get; set; }
        public bool HasValue { get; set; }
    }
}
