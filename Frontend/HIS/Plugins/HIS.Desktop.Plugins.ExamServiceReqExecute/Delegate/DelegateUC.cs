using DevExpress.XtraBars;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExamServiceReqExecute.Delegate
{

    public delegate void RefeshData();
    public delegate void SelectRowOnLoadHanler(long treatmentId, long patientId, long intructionTime, long finishTime);
    public delegate void UpdatePatientStatus(MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ servicereq);
}
