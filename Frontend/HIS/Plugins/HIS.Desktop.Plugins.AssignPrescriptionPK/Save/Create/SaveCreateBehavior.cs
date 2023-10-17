using HIS.Desktop.Plugins.AssignPrescriptionPK.ADO;
using HIS.Desktop.Plugins.AssignPrescriptionPK.AssignPrescription;
using HIS.Desktop.Plugins.AssignPrescriptionPK.Config;
using Inventec.Core;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.Save.Create
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
            this.UseTimes = frmAssignPrescription.UseTimeSelecteds;
            this.RequestRoomId = frmAssignPrescription.GetRoomId();
        }

        object ISave.Run()
        {
            return ((GlobalStore.IsTreatmentIn && !GlobalStore.IsCabinet) ? Run__In() : Run__Out());
        }
    }
}
