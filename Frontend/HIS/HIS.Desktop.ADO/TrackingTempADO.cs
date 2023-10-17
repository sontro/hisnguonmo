using HIS.Desktop.Common;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class TrackingTempADO
    {
        public TrackingTempADO() { }
        public DelegateSelectData DelegateSelectData { get; set; }
        public bool? IsCreatorOrPublic { get; set; }
        public HIS_TRACKING_TEMP HIS_TRACKING_TEMP { get; set; }
    }
}
