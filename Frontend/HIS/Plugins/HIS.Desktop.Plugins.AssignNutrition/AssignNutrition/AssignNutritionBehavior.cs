using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AssignNutrition;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.Plugins.AssignNutrition.ADO;
using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.AssignNutrition.AssignNutrition;
using MOS.Filter;

namespace HIS.Desktop.Plugins.AssignNutrition.AssignNutrition
{
    public sealed class AssignNutritionBehavior : Tool<IDesktopToolContext>, IAssignNutrition
    {
        long entity;
        Inventec.Desktop.Common.Modules.Module Module;
        HisTreatmentBedRoomLViewFilter treatmentBedRoomLViewFilter;

        public AssignNutritionBehavior()
            : base()
        {
        }

        public AssignNutritionBehavior(CommonParam param, long data, Inventec.Desktop.Common.Modules.Module module, HisTreatmentBedRoomLViewFilter treatmentBedRoomLViewFilter)
            : base()
        {
            this.entity = data;
            this.Module = module; 
            this.treatmentBedRoomLViewFilter = treatmentBedRoomLViewFilter;
        }

        object IAssignNutrition.Run()
        {
            try
            {
                return new frmAssignNutrition(this.Module, this.entity, this.treatmentBedRoomLViewFilter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
    }
}
