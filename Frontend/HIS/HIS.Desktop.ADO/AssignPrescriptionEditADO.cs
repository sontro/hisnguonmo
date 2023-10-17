using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class AssignPrescriptionEditADO
    {
        public delegate void DelegateRefeshData(object data);

        public MOS.EFMODEL.DataModels.HIS_SERVICE_REQ ServiceReq { get; set; }
        public MOS.EFMODEL.DataModels.HIS_EXP_MEST ExpMest { get; set; }
        public DelegateRefeshData DgRefeshData { get; set; }

        public AssignPrescriptionEditADO(MOS.EFMODEL.DataModels.HIS_SERVICE_REQ _serviceReq, HIS.Desktop.ADO.AssignPrescriptionEditADO.DelegateRefeshData refeshData)
        {
            this.DgRefeshData = refeshData;
            this.ServiceReq = _serviceReq;
        }

        public AssignPrescriptionEditADO(MOS.EFMODEL.DataModels.HIS_SERVICE_REQ _serviceReq, MOS.EFMODEL.DataModels.HIS_EXP_MEST _expMest, HIS.Desktop.ADO.AssignPrescriptionEditADO.DelegateRefeshData refeshData)
        {
            this.DgRefeshData = refeshData;
            this.ServiceReq = _serviceReq;
            this.ExpMest = _expMest;
        }
    }
}
