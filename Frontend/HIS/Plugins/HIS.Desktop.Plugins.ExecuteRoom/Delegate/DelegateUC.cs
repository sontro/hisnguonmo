using DevExpress.XtraBars;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.Desktop.Plugins.ExecuteRoom.Delegate
{
    public delegate void RefeshData();
    public delegate void SelectRowOnLoadHanler(long treatmentId, long patientId, long intructionTime, long finishTime);
    public delegate void RefeshDataExt(HIS_SERE_SERV_EXT data);
}
