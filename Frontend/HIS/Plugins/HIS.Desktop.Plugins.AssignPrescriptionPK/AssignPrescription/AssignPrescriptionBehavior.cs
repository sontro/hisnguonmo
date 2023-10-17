using HIS.Desktop.ADO;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.Filter;
using System;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.AssignPrescription
{
    public sealed class AssignPrescriptionBehavior : Tool<IDesktopToolContext>, IAssignPrescription
    {
        AssignPrescriptionADO assignPrescriptionADO;
        Inventec.Desktop.Common.Modules.Module Module;
        HisTreatmentBedRoomLViewFilter treatmentBedRoomLViewFilter;
        public AssignPrescriptionBehavior()
            : base()
        {
        }

        public AssignPrescriptionBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, AssignPrescriptionADO data, HisTreatmentBedRoomLViewFilter treatmentBedRoomLViewFilter)
            : base()
        {
            this.assignPrescriptionADO = data;
            this.Module = module;
            this.treatmentBedRoomLViewFilter = treatmentBedRoomLViewFilter;
        }

        object IAssignPrescription.Run()
        {
            try
            {
                return new frmAssignPrescription(this.Module, assignPrescriptionADO, treatmentBedRoomLViewFilter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
