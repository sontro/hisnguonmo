using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.SwapService;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace Inventec.Desktop.Plugins.SwapService.SwapService
{
    public sealed class SwapServiceBehavior : Tool<IDesktopToolContext>, ISwapService
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        SwapServiceADO swapServiceADO;

        public SwapServiceBehavior()
            : base()
        {
        }

        public SwapServiceBehavior(CommonParam param, SwapServiceADO _swapServiceADO, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.swapServiceADO = _swapServiceADO;
            this.moduleData = moduleData;
        }

        object ISwapService.Run()
        {
            try
            {
                return new frmSwapService(this.moduleData, swapServiceADO.serviceReq, swapServiceADO.currentSereServ, swapServiceADO.delegateSwapService);
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
