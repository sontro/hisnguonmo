using HIS.Desktop.Plugins.AssignPrescriptionCLS.ADO;
using HIS.Desktop.Plugins.AssignPrescriptionCLS.AssignPrescription;
using HIS.Desktop.Plugins.AssignPrescriptionCLS.Config;
using Inventec.Core;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.AssignPrescriptionCLS.Save.Create
{
    partial class SaveCreateBehavior : SaveAbstract, ISave
    {
        internal SaveCreateBehavior(CommonParam param,
            List<MediMatyTypeADO> mediMatyTypeADOs,
            frmAssignPrescription frmAssignPrescription,
            int actionType,
            bool isSaveAndPrint,
            long parentServiceReqId,
            long sereServParentId)
            : base(param,
             mediMatyTypeADOs,
             frmAssignPrescription,
             actionType,
             isSaveAndPrint,
             parentServiceReqId,
             sereServParentId)
        {
            this.InstructionTimes = frmAssignPrescription.intructionTimeSelecteds;
            this.RequestRoomId = frmAssignPrescription.GetRoomId();
        }

        object ISave.Run()
        {
            return ((GlobalStore.IsTreatmentIn && !GlobalStore.IsCabinet) ? Run__In() : Run__Out());
        }
    }
}
