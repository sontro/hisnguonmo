using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class AssignPrescriptionKidneyADO
    {
        public delegate void DelegateProcessDataResult(object data);

        public long? ExpMestTemplateId { get; set; }
        public long? ServiceReqParentId { get; set; }
        public DelegateProcessDataResult DgProcessDataResult { get; set; }
        public bool IsAutoCheckExpend { get; set; }
        public MOS.EFMODEL.DataModels.HIS_SERVICE_REQ ServiceReq { get; set; }
        public MOS.EFMODEL.DataModels.HIS_SERVICE_REQ_METY ServiceReqMety { get; set; }
        public AssignPrescriptionEditADO AssignPrescriptionEditADO { get; set; }

        public AssignPrescriptionKidneyADO() { }

        public AssignPrescriptionKidneyADO(MOS.EFMODEL.DataModels.HIS_SERVICE_REQ serviceReq, MOS.EFMODEL.DataModels.HIS_SERVICE_REQ_METY serviceReqMety, long? expMestTemplateId, bool isAutoCheckExpend, DelegateProcessDataResult _DgProcessDataResult, AssignPrescriptionEditADO assignPrescriptionEditADO)
        {
            this.ServiceReq = serviceReq;
            this.ServiceReqMety = serviceReqMety;
            this.IsAutoCheckExpend = isAutoCheckExpend;
            this.DgProcessDataResult = _DgProcessDataResult;
            this.ExpMestTemplateId = expMestTemplateId;
            this.AssignPrescriptionEditADO = assignPrescriptionEditADO;
        }
    }
}
