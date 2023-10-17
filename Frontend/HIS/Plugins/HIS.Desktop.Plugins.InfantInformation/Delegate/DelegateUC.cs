using DevExpress.XtraBars;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.Desktop.Plugins.InfantInformation.Delegate
{
    public delegate void RefeshData();
    public delegate void SelectRowOnLoadHanler(long treatmentId, long patientId, long intructionTime, long finishTime);
}
