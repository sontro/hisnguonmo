using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYT.Desktop.Plugins.Nerves;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace TYT.Desktop.Plugins.Nerves.TYTNerves
{
    public sealed class TYTNervesUpdateBehavior : Tool<IDesktopToolContext>, ITYTNerves
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        long nervesId;
        DelegateSelectData refeshData;
        TYT.EFMODEL.DataModels.TYT_NERVES nerverADO;

        public TYTNervesUpdateBehavior()
            : base()
        {
        }

        public TYTNervesUpdateBehavior(CommonParam param,long nervesId, DelegateSelectData refeshData, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
            this.nervesId = nervesId;
            this.refeshData = refeshData;
        }

        public TYTNervesUpdateBehavior(CommonParam param, TYT.EFMODEL.DataModels.TYT_NERVES nerverADO, DelegateSelectData refeshData, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
            this.nerverADO = nerverADO;
            this.refeshData = refeshData;
        }

        object ITYTNerves.Run()
        {
            try
            {
                if (nervesId>0)
                {
                    return new frmTYTNerves(this.moduleData, nervesId, refeshData);
                }
                else
                {
                    return new frmTYTNerves(this.moduleData, nerverADO, refeshData);
                }
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
