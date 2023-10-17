using HIS.Desktop.Plugins.AssignPrescriptionKidney.ADO;
using HIS.Desktop.Plugins.AssignPrescriptionKidney.AssignPrescription;
using Inventec.Core;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.AssignPrescriptionKidney.Save.Create
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
            this.IsMultiDate = frmAssignPrescription.isMultiDateState;
            this.RequestRoomId = frmAssignPrescription.GetRoomId();
        }

        object ISave.Run()
        {
            return Run__In();
        }
    }
}
