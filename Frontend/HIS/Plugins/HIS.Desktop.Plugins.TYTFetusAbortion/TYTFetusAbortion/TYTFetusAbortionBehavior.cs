using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYT.Desktop.Plugins.FetusAbortion;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace TYT.Desktop.Plugins.FetusAbortion.TYTFetusAbortion
{
    public sealed class TYTFetusAbortionBehavior : Tool<IDesktopToolContext>, ITYTFetusAbortion
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        V_HIS_PATIENT patient { get; set; }
        public TYTFetusAbortionBehavior()
            : base()
        {
        }

        public TYTFetusAbortionBehavior(CommonParam param,V_HIS_PATIENT patient, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
            this.patient = patient;
        }

        object ITYTFetusAbortion.Run()
        {
            try
            {
                return new frmTYTFetusAbortion(this.moduleData, patient);
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
