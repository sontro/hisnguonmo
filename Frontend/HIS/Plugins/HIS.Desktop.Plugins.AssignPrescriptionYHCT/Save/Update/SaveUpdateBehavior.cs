using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT.ADO;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT.AssignPrescription;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionYHCT.Save.Update
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
            this.IsMultiDate = frmAssignPrescription.isMultiDateState;
            this.OldExpMest = frmAssignPrescription.oldExpMest;
            this.OldServiceReq = frmAssignPrescription.oldServiceReq;
            // this.RequestRoomId = (this.OldServiceReq != null ? this.OldServiceReq.REQUEST_ROOM_ID : 0);
            this.ParentServiceReqId = this.OldServiceReq.PARENT_ID ?? 0;
            this.RequestRoomId = frmAssignPrescription.currentModule.RoomId;
        }

        object ISave.Run()
        {
            return ((GlobalStore.IsTreatmentIn && !GlobalStore.IsCabinet) ? Run__In() : Run__Out());
        }
    }
}
