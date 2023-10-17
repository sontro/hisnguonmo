using HIS.Desktop.Plugins.AssignPrescriptionCLS.ADO;
using HIS.Desktop.Plugins.AssignPrescriptionCLS.AssignPrescription;
using HIS.Desktop.Plugins.AssignPrescriptionCLS.Config;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.AssignPrescriptionCLS.Save.Update
{
    partial class SaveUpdateBehavior : SaveAbstract, ISave
    {
        HIS_EXP_MEST OldExpMest { get; set; }
        HIS_SERVICE_REQ OldServiceReq { get; set; }

        internal SaveUpdateBehavior(CommonParam param,
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
            this.OldExpMest = frmAssignPrescription.oldExpMest;
            this.OldServiceReq = frmAssignPrescription.oldServiceReq;
            this.ParentServiceReqId = this.OldServiceReq.PARENT_ID ?? 0;
            this.RequestRoomId = frmAssignPrescription.currentModule.RoomId;
        }

        object ISave.Run()
        {
            return ((GlobalStore.IsTreatmentIn && !GlobalStore.IsCabinet) ? Run__In() : Run__Out());
        }
    }
}
