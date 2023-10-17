using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisTrackingList.ADO
{
    public class TrackingListADO
    {
        public TrackingListADO() { }
        public List<V_HIS_TRACKING> Trackings { get; set; }
    }
}
