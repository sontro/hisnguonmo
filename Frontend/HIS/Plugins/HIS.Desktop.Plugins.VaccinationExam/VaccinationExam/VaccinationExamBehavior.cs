using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.VaccinationExam;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.VaccinationExam.VaccinationExam
{
    public sealed class VaccinationExamBehavior : Tool<IDesktopToolContext>, IVaccinationExam
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        public VaccinationExamBehavior()
            : base()
        {
        }

        public VaccinationExamBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
        }

        object IVaccinationExam.Run()
        {
            try
            {
                return new UCVaccinationExam(this.moduleData);
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
