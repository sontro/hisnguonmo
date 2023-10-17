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
    public sealed class TYTFetusAbortionUpdateBehavior : Tool<IDesktopToolContext>, ITYTFetusAbortion
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        long fetusAbortionId;
        DelegateSelectData refeshData;

        public TYTFetusAbortionUpdateBehavior()
            : base()
        {
        }

        public TYTFetusAbortionUpdateBehavior(CommonParam param,long fetusAbortionId, DelegateSelectData refeshData, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
            this.fetusAbortionId = fetusAbortionId;
            this.refeshData = refeshData;
        }

        object ITYTFetusAbortion.Run()
        {
            try
            {
                return new frmTYTFetusAbortion(this.moduleData, fetusAbortionId, refeshData);
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
